using GotaSoundIO.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NitroFileLoader {

    /// <summary>
    /// Group entry.
    /// </summary>
    public class GroupEntry : IReadable, IWriteable {

        /// <summary>
        /// Entry data.
        /// </summary>
        public object Entry;

        /// <summary>
        /// Type.
        /// </summary>
        public GroupEntryType Type;

        /// <summary>
        /// Reading Id.
        /// </summary>
        public uint ReadingId;

        /// <summary>
        /// Load sequence.
        /// </summary>
        public bool LoadSequence;

        /// <summary>
        /// Load sequence archive.
        /// </summary>
        public bool LoadSequenceArchive;

        /// <summary>
        /// Load bank.
        /// </summary>
        public bool LoadBank;

        /// <summary>
        /// Load wave archive.
        /// </summary>
        public bool LoadWaveArchive;

        /// <summary>
        /// Load flags.
        /// </summary>
        /// <param name="flags">The flags.</param>
        public void LoadFlags(byte flags) {
            LoadSequence = (flags & 0b1) > 0;
            LoadBank = (flags & 0b10) > 0;
            LoadWaveArchive = (flags & 0b100) > 0;
            LoadSequenceArchive = (flags & 0b1000) > 0;
        }

        /// <summary>
        /// Save flags.
        /// </summary>
        /// <returns>The flags as a byte.</returns>
        public byte SaveFlags() {
            byte flags = 0;
            if (LoadSequence) { flags |= 0b1; }
            if (LoadBank) { flags |= 0b10; }
            if (LoadWaveArchive) { flags |= 0b100; }
            if (LoadSequenceArchive) { flags |= 0b1000; }
            return flags;
        }

        /// <summary>
        /// Read the entry.
        /// </summary>
        /// <param name="r">The reader.</param>
        public void Read(FileReader r) {
            Type = (GroupEntryType)r.ReadByte();
            LoadFlags(r.ReadByte());
            r.ReadUInt16();
            ReadingId = r.ReadUInt32();
        }

        /// <summary>
        /// Write the entry.
        /// </summary>
        /// <param name="w">The writer</param>
        public void Write(FileWriter w) {
            w.Write((byte)Type);
            w.Write(SaveFlags());
            w.Write((ushort)0);
            switch (Type) {
                case GroupEntryType.Sequence:
                    w.Write((uint)(Entry as SequenceInfo).Index);
                    break;
                case GroupEntryType.Bank:
                    w.Write((uint)(Entry as BankInfo).Index);
                    break;
                case GroupEntryType.WaveArchive:
                    w.Write((uint)(Entry as WaveArchiveInfo).Index);
                    break;
                case GroupEntryType.SequenceArchive:
                    w.Write((uint)(Entry as SequenceArchiveInfo).Index);
                    break;
            }
        }

    }

    /// <summary>
    /// Entry type.
    /// </summary>
    public enum GroupEntryType : byte {
        Sequence, Bank, WaveArchive, SequenceArchive
    }

}
