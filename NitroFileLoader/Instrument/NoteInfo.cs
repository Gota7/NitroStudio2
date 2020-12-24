using GotaSequenceLib;
using GotaSequenceLib.Playback;
using GotaSoundIO.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NitroFileLoader {

    /// <summary>
    /// Note info.
    /// </summary>
    public class NoteInfo : IReadable, IWriteable {

        /// <summary>
        /// Key to access the note.
        /// </summary>
        public Notes Key;

        /// <summary>
        /// Instrument type.
        /// </summary>
        public InstrumentType InstrumentType = InstrumentType.PCM;

        /// <summary>
        /// Wave Id.
        /// </summary>
        public ushort WaveId;

        /// <summary>
        /// Wave archive Id.
        /// </summary>
        public ushort WarId;

        /// <summary>
        /// Base note.
        /// </summary>
        public byte BaseNote = 60;

        /// <summary>
        /// Attack.
        /// </summary>
        public byte Attack = 127;

        /// <summary>
        /// Decay.
        /// </summary>
        public byte Decay = 127;

        /// <summary>
        /// Sustain.
        /// </summary>
        public byte Sustain = 127;

        /// <summary>
        /// Release.
        /// </summary>
        public byte Release = 127;

        /// <summary>
        /// Pan.
        /// </summary>
        public byte Pan = 0x40;

        /// <summary>
        /// Read the note info.
        /// </summary>
        /// <param name="r">The reader.</param>
        public void Read(FileReader r) {
            WaveId = r.ReadUInt16();
            WarId = r.ReadUInt16();
            BaseNote = r.ReadByte();
            Attack = r.ReadByte();
            Decay = r.ReadByte();
            Sustain = r.ReadByte();
            Release = r.ReadByte();
            Pan = r.ReadByte();
        }

        /// <summary>
        /// Write the note info.
        /// </summary>
        /// <param name="w">The writer.</param>
        public void Write(FileWriter w) {
            w.Write(WaveId);
            w.Write(WarId);
            w.Write(BaseNote);
            w.Write(Attack);
            w.Write(Decay);
            w.Write(Sustain);
            w.Write(Release);
            w.Write(Pan);
        }

        /// <summary>
        /// Convert this to note playback info.
        /// </summary>
        /// <returns>This as note playback info.</returns>
        public NotePlayBackInfo ToNotePlayBackInfo() => new NotePlayBackInfo() {
            Attack = Attack,
            Decay = Decay,
            InstrumentType = TrueType(),
            BaseKey = BaseNote,
            Pan = Pan,
            Release = Release,
            Sustain = Sustain,
            WarId = WarId,
            WaveId = WaveId
        };

        /// <summary>
        /// True instrument type.
        /// </summary>
        /// <returns></returns>
        public GotaSequenceLib.Playback.InstrumentType TrueType() {
            switch (InstrumentType) {
                case InstrumentType.PSG:
                    return GotaSequenceLib.Playback.InstrumentType.PSG;
                case InstrumentType.Noise:
                    return GotaSequenceLib.Playback.InstrumentType.Noise;
                default:
                    return GotaSequenceLib.Playback.InstrumentType.PCM;
            }
        }

        /// <summary>
        /// If the note info equals another note info.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>If the two objects are equal.</returns>
        public override bool Equals(object obj) {

            //Note info.
            var n = obj as NoteInfo;
            if (n == null) {
                return false;
            }

            //If content matches.
            return n.Attack == Attack && n.BaseNote == BaseNote && n.Decay == Decay && n.InstrumentType == InstrumentType && n.Pan == Pan && n.Release == Release && n.Sustain == Sustain && n.WarId == WarId && n.WaveId == WaveId;

        }

        /// <summary>
        /// Duplicate the note info.
        /// </summary>
        /// <returns>New note info clone.</returns>
        public NoteInfo Duplicate() {
            return new NoteInfo() { Attack = Attack, BaseNote = BaseNote, Decay = Decay, InstrumentType = InstrumentType, Key = Key, Pan = Pan, Release = Release, Sustain = Sustain, WarId = WarId, WaveId = WaveId };
        }

    }

}
