using GotaSoundIO.IO;
using GotaSoundIO.Sound;
using NitroFileLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NitroStudio2 {

    /// <summary>
    /// A Nitro Studio 2 Instrument.
    /// </summary>
    public class NitroStudio2Instrument : IOFile {

        /// <summary>
        /// The actual instrument.
        /// </summary>
        public Instrument Inst;

        /// <summary>
        /// Waves.
        /// </summary>
        public List<WaveEntry> Waves;

        /// <summary>
        /// Blank constructor.
        /// </summary>
        public NitroStudio2Instrument() {}

        /// <summary>
        /// Create a Nitro Studio 2 Instrument.
        /// </summary>
        /// <param name="inst">The instrument.</param>
        /// <param name="s">Sound archive.</param>
        /// <param name="war0">Wave archive 0.</param>
        /// <param name="war1">Wave archive 1.</param>
        /// <param name="war2">Wave archive 2.</param>
        /// <param name="war3">Wave archive 3.</param>
        public NitroStudio2Instrument(Instrument inst, SoundArchive s, ushort war0, ushort war1, ushort war2, ushort war3) {

            //Set instrument.
            Inst = inst;

            //No archive.
            if (s == null) { return; }

            //Load waves.
            Waves = new List<WaveEntry>();
            foreach (var n in inst.NoteInfo) {
                WaveEntry w = new WaveEntry();
                w.WaveId = n.WaveId;
                if (n.InstrumentType != InstrumentType.PCM) { continue; }
                switch (n.WarId) {
                    case 0:
                        n.WarId = w.WarId = war0;
                        break;
                    case 1:
                        n.WarId = w.WarId = war1;
                        break;
                    case 2:
                        n.WarId = w.WarId = war2;
                        break;
                    case 3:
                        n.WarId = w.WarId = war3;
                        break;
                }
                if (n.WarId != 0xFFFF) {
                    var war = s.WaveArchives.Where(x => x.Index == (int)n.WarId).FirstOrDefault();
                    if (war != null) {
                        w.Wave = war.File.Waves[n.WaveId];
                    }
                }
                Waves.Add(w);
            }
        
        }

        /// <summary>
        /// Read the instrument.
        /// </summary>
        /// <param name="r">The reader.</param>
        public override void Read(FileReader r) {

            //Skip header.
            r.ReadUInt32();

            //Get type.
            switch (r.ReadByte()) {
                case 0:
                    Inst = new DirectInstrument();
                    break;
                case 1:
                    Inst = new DrumSetInstrument();
                    break;
                case 2:
                    Inst = new KeySplitInstrument();
                    break;
            }

            //Read data.
            Inst.Read(r);
            if (Inst as DirectInstrument != null) {
                Inst.NoteInfo[0].InstrumentType = (InstrumentType)r.ReadByte();
            }

            //Read waves.
            Waves = null;
            if (!r.ReadBoolean()) { return; }
            Waves = new List<WaveEntry>();
            uint numWaves = r.ReadUInt32();
            for (uint i = 0; i < numWaves; i++) {
                Waves.Add(new WaveEntry() { WaveId = r.ReadUInt16(), WarId = r.ReadUInt16() });
                if (r.ReadBoolean()) {
                    Waves[Waves.Count - 1].Wave = new Wave();
                    Waves[Waves.Count - 1].Wave = (Wave)r.ReadFile<Wave>();
                }
            }

        }

        /// <summary>
        /// Write an instrument.
        /// </summary>
        /// <param name="bnk">The bank.</param>
        /// <param name="instrumentId">The instrument Id.</param>
        /// <param name="a">Sound archive.</param>
        /// <param name="war0">Wave archive 0.</param>
        /// <param name="war1">Wave archive 1.</param>
        /// <param name="war2">Wave archive 2.</param>
        /// <param name="war3">Wave archive 3.</param>
        public void WriteInstrument(Bank bnk, int instrumentId, SoundArchive a, ushort war0, ushort war1, ushort war2, ushort war3) {

            //Set the instrument.
            var repl = bnk.Instruments.Where(x => x.Index == instrumentId).FirstOrDefault();
            Inst.Index = instrumentId;

            //Sound archive check.
            if (a == null) {
                return;
            }

            //Get wave archives.
            WaveArchiveInfo[] wars = new WaveArchiveInfo[4];
            if (war0 != 0xFFFF) {
                wars[0] = a.WaveArchives.Where(x => x.Index == (int)war0).FirstOrDefault();
            }
            if (war1 != 0xFFFF) {
                wars[1] = a.WaveArchives.Where(x => x.Index == (int)war1).FirstOrDefault();
            }
            if (war2 != 0xFFFF) {
                wars[2] = a.WaveArchives.Where(x => x.Index == (int)war2).FirstOrDefault();
            }
            if (war3 != 0xFFFF) {
                wars[3] = a.WaveArchives.Where(x => x.Index == (int)war3).FirstOrDefault();
            }

            //Make sure there are linked wave archives.
            if (wars.Where(x => x != null).Count() < 1) {
                return;
            }

            //For each region in the instrument.
            foreach (var r in Inst.NoteInfo) {

                //PCM type.
                if (r.InstrumentType != InstrumentType.PCM) {
                    continue;
                }

                //Waves are not null.
                if (Waves == null) {
                    continue;
                }

                //Get entry.
                var e = Waves.Where(x => x.WarId == r.WarId && x.WaveId == r.WaveId).FirstOrDefault();
                if (e == null) {
                    continue;
                }

                //Wave is not null.
                if (e.Wave == null) {
                    continue;
                }

                //Get MD5SUM of wave.
                string md5 = e.Wave.Md5Sum;

                //Try and find matching wave.
                bool found = false;
                for (int i = 0; i < wars.Length; i++) {
                    if (wars[i] != null) {
                        for (int j = 0; j < wars[i].File.Waves.Count; j++) {
                            if (!found && wars[i].File.Waves[j].Md5Sum == md5) {
                                r.WaveId = (ushort)j;
                                r.WarId = (ushort)i;
                                found = true;
                            }
                        }
                    }
                }

                //Not found.
                if (!found) {
                    RiffWave riff = new RiffWave();
                    riff.FromOtherStreamFile(e.Wave);
                    WaveMapper mapper = new WaveMapper(new List<RiffWave>() { riff }, wars.Where(x => x != null).ToList(), true);
                    mapper.MinimizeBox = false;
                    mapper.ShowDialog();
                    if (mapper.WarMap == null) {
                        return;
                    }
                    a.WaveArchives.Where(x => x.Index == mapper.WarMap[0]).FirstOrDefault().File.Waves.Add(e.Wave);
                    r.WaveId = (ushort)(a.WaveArchives.Where(x => x.Index == mapper.WarMap[0]).FirstOrDefault().File.Waves.Count() - 1);
                    r.WarId = (ushort)wars.ToList().IndexOf(a.WaveArchives.Where(x => x.Index == mapper.WarMap[0]).FirstOrDefault());
                }

            }

            //Set instrument.
            bnk.Instruments[bnk.Instruments.IndexOf(repl)] = Inst;

        }

        /// <summary>
        /// Write the instrument.
        /// </summary>
        /// <param name="w">The writer.</param>
        public override void Write(FileWriter w) {

            //Write header.
            w.Write("NS2I".ToCharArray());

            //Write instrument type.
            switch (Inst.Type()) {
                case InstrumentType.DrumSet:
                    w.Write((byte)1);
                    break;
                case InstrumentType.KeySplit:
                    w.Write((byte)2);
                    break;
                default:
                    w.Write((byte)0);
                    break;
            }

            //Write instrument.
            w.Write(Inst);
            if (Inst as DirectInstrument != null) {
                w.Write((byte)Inst.NoteInfo[0].InstrumentType);
            }

            //Write waves.
            w.Write(Waves != null);
            if (Waves == null) { return; }
            w.Write((uint)Waves.Count);
            foreach (var v in Waves) {
                w.Write(v.WaveId);
                w.Write(v.WarId);
                w.Write(v.Wave != null);
                if (v.Wave != null) {
                    w.WriteFile(v.Wave);
                }
            }

        }

        /// <summary>
        /// Wave entry.
        /// </summary>
        public class WaveEntry {

            /// <summary>
            /// Wave archive Id.
            /// </summary>
            public ushort WarId;

            /// <summary>
            /// Wave Id.
            /// </summary>
            public ushort WaveId;

            /// <summary>
            /// Actual wave data.
            /// </summary>
            public Wave Wave;

        }

    }

}
