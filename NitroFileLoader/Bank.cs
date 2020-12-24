using GotaSoundBank;
using GotaSequenceLib;
using GotaSequenceLib.Playback;
using GotaSoundIO.IO;
using GotaSoundIO.Sound;
using Kermalis.SoundFont2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GotaSoundBank.DLS;
using GotaSoundBank.SF2;

namespace NitroFileLoader {

    /// <summary>
    /// Bank.
    /// </summary>
    public class Bank : IOFile, PlayableBank {

        /// <summary>
        /// Instruments.
        /// </summary>
        public List<Instrument> Instruments = new List<Instrument>();

        /// <summary>
        /// Max order.
        /// </summary>
        public long MaxOrder => Instruments.Select(x => x.GetOrder).Max();

        /// <summary>
        /// Duplicate an instrument.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static Instrument DuplicateInstrument(Instrument i) {
            Instrument n = null;
            switch (i.Type()) {
                case InstrumentType.DrumSet:
                    n = new DrumSetInstrument() { Index = i.Index, NoteInfo = new List<NoteInfo>(), Min = ((DrumSetInstrument)i).Min };
                    foreach (var r in i.NoteInfo) {
                        n.NoteInfo.Add(r.Duplicate());
                    }
                    break;
                case InstrumentType.KeySplit:
                    n = new KeySplitInstrument() { Index = i.Index, NoteInfo = new List<NoteInfo>() };
                    foreach (var r in i.NoteInfo) {
                        n.NoteInfo.Add(r.Duplicate());
                    }
                    break;
                default:
                    n = new DirectInstrument() { Index = i.Index, NoteInfo = new List<NoteInfo>() { i.NoteInfo[0].Duplicate() } };
                    break;
            }
            return n;
        }

        /// <summary>
        /// Read the file.
        /// </summary>
        /// <param name="r">The reader.</param>
        public override void Read(FileReader r) {

            //Open block.
            r.OpenFile<NHeader>(out _);
            r.OpenBlock(0, out _, out _);

            //Read info.
            r.ReadUInt32s(8);
            uint numInsts = r.ReadUInt32();
            List<InstrumentType> records = new List<InstrumentType>();
            List<uint> offs = new List<uint>();
            for (uint i = 0; i < numInsts; i++) {
                records.Add((InstrumentType)r.ReadByte());
                offs.Add(r.ReadUInt16());
                r.ReadByte();
            }

            //Read the instrument.
            for (int i = 0; i < records.Count; i++) {

                //Switch the instrument type.
                switch (records[i]) {

                    //Blank.
                    case InstrumentType.Blank:
                        break;

                    //Direct.
                    case InstrumentType.PCM:
                    case InstrumentType.PSG:
                    case InstrumentType.Noise:
                    case InstrumentType.DirectPCM:
                    case InstrumentType.Null:
                        r.Jump(offs[i], true);
                        Instruments.Add(r.Read<DirectInstrument>());
                        Instruments[Instruments.Count - 1].Index = i;
                        Instruments[Instruments.Count - 1].NoteInfo[0].InstrumentType = records[i];
                        Instruments[Instruments.Count - 1].Order = offs[i];
                        break;

                    //Drum set.
                    case InstrumentType.DrumSet:
                        r.Jump(offs[i], true);
                        Instruments.Add(r.Read<DrumSetInstrument>());
                        Instruments[Instruments.Count - 1].Index = i;
                        Instruments[Instruments.Count - 1].Order = offs[i];
                        break;

                    //Key split.
                    case InstrumentType.KeySplit:
                        r.Jump(offs[i], true);
                        Instruments.Add(r.Read<KeySplitInstrument>());
                        Instruments[Instruments.Count - 1].Index = i;
                        Instruments[Instruments.Count - 1].Order = offs[i];
                        break;

                }

            }

        }

        /// <summary>
        /// Write the file.
        /// </summary>
        /// <param name="w">The writer.</param>
        public override void Write(FileWriter w) {

            //Write the bank.
            w.InitFile<NHeader>("SBNK", ByteOrder.LittleEndian, null, 1);
            w.InitBlock("DATA");
            w.Write(new uint[8]);

            //Instruments.
            if (Instruments.Count > 0) {
                Instruments = Instruments.OrderBy(x => x.Index).ToList();
                w.Write((uint)(Instruments.Last().Index + 1));
                Dictionary<int, long> bakPos = new Dictionary<int, long>();
                for (int i = 0; i <= Instruments.Last().Index; i++) {
                    if (Instruments.Where(x => x.Index == i).Count() > 0) {
                        var inst = Instruments.Where(x => x.Index == i).FirstOrDefault();
                        w.Write((byte)inst.Type());
                        bakPos.Add(i, w.Position);
                        w.Write((ushort)0);
                        w.Write((byte)0);
                    } else {
                        w.Write((uint)0);
                    }
                }

                //Write each instrument.
                var sortedInsts = Instruments.OrderBy(x => x.GetOrder).ToList();
                for (int i = 0; i < sortedInsts.Count; i++) {
                    long bak = w.Position;
                    w.Position = bakPos[sortedInsts[i].Index];
                    w.Write((ushort)(bak - w.FileOffset));
                    w.Position = bak;
                    w.Write(sortedInsts[i]);
                }
            } else {
                w.Write((uint)0);
            }

            //Close.
            w.Pad(4);
            w.CloseBlock();
            w.CloseFile();

        }

        /// <summary>
        /// Get note playback info.
        /// </summary>
        /// <param name="program">Program number.</param>
        /// <param name="note">Note to play.</param>
        /// <param name="velocity">Note velocity.</param>
        /// <returns>The note playback info.</returns>
        public NotePlayBackInfo GetNotePlayBackInfo(int program, Notes note, byte velocity) {

            //Has program.
            var q = Instruments.Where(x => x.Index == program).FirstOrDefault();
            if (q != null) {
                var e = q.GetNoteInfo(note);
                if (e != null) {
                    return e.ToNotePlayBackInfo();
                } else {
                    return null;
                }
            } else {
                return null;
            }

        }

        /// <summary>
        /// Convert the SBNK into a sound font.
        /// </summary>
        /// <param name="a">The sound archive.</param>
        /// <param name="b">The bank info.</param>
        /// <returns>This as a soundfont.</returns>
        public SoundFont ToSoundFont(SoundArchive a, BankInfo b) {

            //Laziness.
            return new SoundFont(ToDLS(a, b));

        }

        /// <summary>
        /// Convert the bank to downloadable sounds.
        /// </summary>
        /// <param name="a">The sound archive.</param>
        /// <param name="b">The bank info.</param>
        /// <returns>The bank as DLS.</returns>
        public DownloadableSounds ToDLS(SoundArchive a, BankInfo b) {

            //New DLS.
            DownloadableSounds d = new DownloadableSounds();

            //Wave map.
            Dictionary<uint, RiffWave> waveMap = new Dictionary<uint, RiffWave>();
            Dictionary<ushort, RiffWave> psgMap = new Dictionary<ushort, RiffWave>();
            Dictionary<ushort, RiffWave> noiseMap = new Dictionary<ushort, RiffWave>();
            d.Waves.Add(new RiffWave("Hardware/Null.wav"));

            //Add each instrument.
            foreach (var inst in Instruments) {

                //New instrument.
                GotaSoundBank.DLS.Instrument im = new GotaSoundBank.DLS.Instrument();
                im.BankId = (uint)(inst.Index / 128);
                im.InstrumentId = (uint)(inst.Index % 128);
                im.Name = "Instrument " + im.InstrumentId;

                //Add regions.
                byte lastNote = inst as DrumSetInstrument != null ? (inst as DrumSetInstrument).Min : (byte)0;
                foreach (var n in inst.NoteInfo) {

                    //New region.
                    Region r = new Region();

                    //Set note info.
                    r.VelocityLow = 0;
                    r.VelocityHigh = 127;
                    r.NoteLow = lastNote;
                    r.NoteHigh = (inst as DirectInstrument != null) ? (byte)127 : (byte)n.Key;
                    lastNote = (byte)(n.Key + 1);
                    r.ChannelFlags = 1;
                    r.DoublePlayback = true;
                    r.Layer = 1;
                    r.NoTruncation = true;
                    r.RootNote = (byte)n.BaseNote;

                    //Wave data.
                    int wavInd = 0;
                    switch (n.InstrumentType) {
                        case InstrumentType.PCM:
                            uint key = 0xFFFFFFFF;
                            try {
                                var p = b.WaveArchives[n.WarId].File.Waves[n.WaveId];
                                key = (uint)(b.WaveArchives[n.WarId].Index << 16) | n.WaveId;
                            } catch { }
                            if (key != 0xFFFFFFFF) {
                                if (!waveMap.ContainsKey(key)) {
                                    RiffWave pcm = new RiffWave();
                                    pcm.FromOtherStreamFile(b.WaveArchives[n.WarId].File.Waves[n.WaveId]);
                                    waveMap.Add(key, pcm);
                                    d.Waves.Add(pcm);
                                }
                                wavInd = d.Waves.IndexOf(waveMap[key]);
                            }
                            break;
                        case InstrumentType.PSG:
                            if (!psgMap.ContainsKey(n.WaveId)) {
                                RiffWave psg = new RiffWave("Hardware/DutyCycle" + (n.WaveId + 1) + ".wav");
                                psgMap.Add(n.WaveId, psg);
                                d.Waves.Add(psg);
                            }
                            wavInd = d.Waves.IndexOf(psgMap[n.WaveId]);
                            break;
                        case InstrumentType.Noise:
                            if (!noiseMap.ContainsKey(0)) {
                                RiffWave noise = new RiffWave("Hardware/WhiteNoise.wav");
                                noiseMap.Add(0, noise);
                                d.Waves.Add(noise);
                                wavInd = d.Waves.IndexOf(noise);
                            } else {
                                wavInd = d.Waves.IndexOf(noiseMap[0]);
                            }
                            break;
                    }

                    //Set wave data.
                    r.WaveId = (uint)wavInd;
                    r.Loops = d.Waves[wavInd].Loops;
                    if (r.Loops) {
                        r.LoopStart = d.Waves[wavInd].LoopStart;
                        r.LoopLength = d.Waves[wavInd].LoopEnd - d.Waves[wavInd].LoopStart;
                        if (r.LoopLength < 0) { r.LoopLength = 0; }
                    }

                    //Articulator.
                    Articulator ar = new Articulator();
                    ar.Connections.Add(new Connection() { DestinationConnection = DestinationConnection.EG1AttackTime, Scale = n.Attack >= 127 ? int.MinValue : MillisecondsToTimecents(AttackTable[n.Attack]) * 65536 });
                    ar.Connections.Add(new Connection() { DestinationConnection = DestinationConnection.EG1DecayTime, Scale = n.Decay >= 127 ? int.MinValue : MillisecondsToTimecents(MaxReleaseTimes[n.Decay]) * 65536 });
                    ar.Connections.Add(new Connection() { DestinationConnection = DestinationConnection.EG1SustainLevel, Scale = (int)Math.Round(Sustain2Fraction(n.Sustain) * 1000, MidpointRounding.AwayFromZero) * 65536 });
                    ar.Connections.Add(new Connection() { DestinationConnection = DestinationConnection.EG1ReleaseTime, Scale = n.Release >= 127 ? int.MinValue : MillisecondsToTimecents(MaxReleaseTimes[n.Release]) * 65536 });
                    ar.Connections.Add(new Connection() { DestinationConnection = DestinationConnection.Pan, Scale = GetPan(n.Pan) * 65536 });
                    r.Articulators.Add(ar);

                    //Add region.
                    im.Regions.Add(r);

                }

                //Add the instrument.
                d.Instruments.Add(im);
            
            }

            //Return the DLS.
            return d;

        }

        /// <summary>
        /// Get the pan.
        /// </summary>
        /// <param name="pan">Pan.</param>
        /// <returns>Pan.</returns>
        public static int GetPan(byte pan) {
            if (pan > 127) { pan = 127; }
            double ret = .5;
            if (pan != 64) {
                ret = pan / 127d;
            }
            return (int)(ret * 1000 - 500);
        }

        /// <summary>
        /// Set the pan value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The output pan.</returns>
        public static byte SetPan(int value) {
            double num = (value + 500) / 1000d;
            byte pan = 64;
            if (value != 0) {
                pan = (byte)(num * 127);
            }
            return pan;
        }

        /// <summary>
        /// Convert milliseconds to timecents.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Milliseconds to timecents.</returns>
        public static int MillisecondsToTimecents(double value) {
            return (int)Math.Round(1200 * Math.Log(value / 1000, 2), MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Convert timecents to milliseconds.
        /// </summary>
        /// <param name="value">Time cents.</param>
        /// <returns>The value in milliseconds.</returns>
        public static double TimecentsToMilliseconds(int value) {
            return Math.Pow(2, value / 1200d) * 1000;
        }

        /// <summary>
        /// Convert sustain to a fraction.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The fraction.</returns>
        public static double Sustain2Fraction(byte value) {
            return Math.Pow(value / 127d, 2);
        }

        /// <summary>
        /// Convert a fraction value to a sustain value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The sustain.</returns>
        public static byte Fraction2Sustain(double value) {
            return (byte)(Math.Sqrt(value) * 127);
        }

        /// <summary>
        /// Get the table index nearest to a value.
        /// </summary>
        /// <param name="value">Value to compare to.</param>
        /// <param name="table">Table of values.</param>
        /// <returns>Nearest index.</returns>
        public static byte GetNearestTableIndex(double value, double[] table) {
            byte ret = 127;
            double minDis = double.MaxValue;
            for (byte i = 0; i < table.Length; i++) {
                double dist = Math.Abs(value - table[i]);
                if (dist < minDis) {
                    minDis = dist;
                    ret = i;
                }
            }
            return ret;
        }

        /// <summary>
        /// Attack table to milliseconds.
        /// </summary>
        public static double[] AttackTable = new double[] {
            8606.1, 4756.3, 3339.3, 2594.4, 2130.7, 1807.7, 1573.3, 1401.4,
            1255.5, 1140.9, 1047.1, 963.8, 896.0, 838.7, 786.6, 745.0,
            703.3, 666.8, 630.4, 599.1, 578.3, 547.0, 526.2, 505.3,
            484.5, 468.9, 448.0, 437.6, 416.8, 406.4, 395.9, 385.5,
            369.9, 359.5, 349.0, 338.6, 328.2, 323.0, 312.6, 307.4,
            297.0, 291.7, 286.5, 276.1, 270.9, 265.7, 260.5, 255.3,
            250.1, 244.9, 239.6, 234.4, 229.2, 224.0, 218.8, 213.6,
            213.6, 208.4, 203.2, 203.2, 198.0, 198.0, 192.8, 192.8,
            182.3, 182.3, 177.1, 177.1, 171.9, 171.9, 166.7, 166.7,
            161.5, 161.5, 156.3, 156.3, 151.1, 151.1, 145.9, 145.9,
            145.9, 145.9, 140.7, 140.7, 140.7, 130.2, 130.2, 130.2,
            125.0, 125.0, 125.0, 125.0, 119.8, 119.8, 119.8, 114.6,
            114.6, 114.6, 114.6, 109.4, 109.4, 109.4, 109.4, 109.4,
            104.2, 104.2, 104.2, 104.2, 99.0, 93.8, 88.6, 83.4,
            78.2, 72.9, 67.7, 62.5, 57.3, 52.1, 46.9, 41.7,
            36.5, 31.3, 26.1, 20.8, 15.6, 10.4, 10.4, 0.0
        };

        /// <summary>
        /// Decay release table to milliseconds.
        /// </summary>
        public static double[] DecayReleaseTable = new double[] {
            -0.0002, -0.0005, -0.0008, -0.0011, -0.0014, -0.0017, -0.0020, -0.0023,
            -0.0026, -0.0029, -0.0032, -0.0035, -0.0038, -0.0041, -0.0044, -0.0047,
            -0.0050, -0.0053, -0.0056, -0.0059, -0.0062, -0.0065, -0.0068, -0.0071,
            -0.0074, -0.0077, -0.0080, -0.0083, -0.0086, -0.0089, -0.0092, -0.0095,
            -0.0098, -0.0101, -0.0104, -0.0107, -0.0110, -0.0113, -0.0116, -0.0119,
            -0.0122, -0.0125, -0.0128, -0.0131, -0.0134, -0.0137, -0.0140, -0.0143,
            -0.0146, -0.0149, -0.0152, -0.0154, -0.0156, -0.0158, -0.0160, -0.0163,
            -0.0165, -0.0167, -0.0170, -0.0172, -0.0175, -0.0178, -0.0180, -0.0183,
            -0.0186, -0.0189, -0.0192, -0.0196, -0.0199, -0.0202, -0.0206, -0.0210,
            -0.0214, -0.0218, -0.0222, -0.0226, -0.0231, -0.0235, -0.0240, -0.0245,
            -0.0251, -0.0256, -0.0262, -0.0268, -0.0275, -0.0281, -0.0288, -0.0296,
            -0.0304, -0.0312, -0.0321, -0.0330, -0.0339, -0.0350, -0.0361, -0.0372,
            -0.0385, -0.0398, -0.0412, -0.0427, -0.0444, -0.0462, -0.0481, -0.0502,
            -0.0524, -0.0549, -0.0577, -0.0607, -0.0641, -0.0679, -0.0721, -0.0769,
            -0.0824, -0.0888, -0.0962, -0.1049, -0.1154, -0.1282, -0.1442, -0.1648,
            -0.1923, -0.2308, -0.2885, -0.3846, -0.5769, -1.1538, -2.2897, -9.8460
        };

        /// <summary>
        /// Maximum release times in milliseconds.
        /// </summary>
        public static double[] MaxReleaseTimes = new double[] {
            481228.8, 160409.6, 96241.6, 68744.0, 53466.4, 43747.6, 37013.6, 32078.8,
            28303.6, 25324.0, 22911.2, 20919.6, 19245.2, 17820.4, 16593.2, 15522.0,
            14580.8, 13748.8, 13005.2, 12334.4, 11736.4, 11190.4, 10691.2, 10238.8,
            9817.6, 9432.8, 9079.2, 8746.4, 8439.6, 8153.6, 7888.4, 7633.6,
            7399.6, 7181.2, 6973.2, 6775.6, 6588.4, 6411.6, 6245.2, 6089.2,
            5938.4, 5792.8, 5657.6, 5527.6, 5402.8, 5283.2, 5174.0, 5064.8,
            4960.8, 4856.8, 4758.0, 4695.6, 4633.2, 4570.8, 4508.4, 4446.0,
            4383.6, 4321.2, 4258.8, 4196.4, 4134.0, 4071.6, 4009.2, 3946.8,
            3884.4, 3822.0, 3759.6, 3692.0, 3629.6, 3567.2, 3504.8, 3442.4,
            3380.0, 3317.6, 3255.2, 3192.8, 3130.4, 3068.0, 3005.6, 2943.2,
            2880.8, 2818.4, 2756.0, 2693.6, 2631.2, 2568.8, 2506.4, 2438.8,
            2376.4, 2314.0, 2251.6, 2189.2, 2126.8, 2064.4, 2002.0, 1939.6,
            1877.2, 1814.8, 1752.4, 1690.0, 1627.6, 1565.2, 1502.8, 1440.4,
            1378.0, 1315.6, 1253.2, 1185.6, 1123.2, 1060.8, 998.4, 936.0,
            873.6, 811.2, 748.8, 686.4, 624.0, 561.6, 499.2, 436.8,
            374.4, 312.0, 249.6, 187.2, 124.8, 62.4, 31.2, 5.2
        };

    }

}
