using GotaSoundIO.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NitroFileLoader {

    /// <summary>
    /// Player info.
    /// </summary>
    public class PlayerInfo : IReadable, IWriteable {

        /// <summary>
        /// Name.
        /// </summary>
        public string Name;

        /// <summary>
        /// Entry index.
        /// </summary>
        public int Index;

        /// <summary>
        /// Sequence max.
        /// </summary>
        public ushort SequenceMax;

        /// <summary>
        /// Channel flags.
        /// </summary>
        public bool[] ChannelFlags = new bool[16];

        /// <summary>
        /// Heap size.
        /// </summary>
        public uint HeapSize;

        /// <summary>
        /// Read the info.
        /// </summary>
        /// <param name="r">The reader.</param>
        public void Read(FileReader r) {
            SequenceMax = r.ReadUInt16();
            ChannelFlags = r.ReadBitFlags(2);
            if (ChannelFlags.Where(x => x == false).Count() == 16) { ChannelFlags = new bool[] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true }; }
            HeapSize = r.ReadUInt32();
        }

        /// <summary>
        /// Write the info.
        /// </summary>
        /// <param name="w">The writer.</param>
        public void Write(FileWriter w) {
            w.Write(SequenceMax);
            if (ChannelFlags.Where(x => x == true).Count() == 16) {
                w.Write((ushort)0);
            } else {
                w.WriteBitFlags(ChannelFlags, 2);
            }
            w.Write(HeapSize);
        }

        /// <summary>
        /// Get bit flags.
        /// </summary>
        public ushort BitFlags() {

            //Flags.
            ushort u = 0;
            for (int i = 0; i < ChannelFlags.Length; i++) {
                if (ChannelFlags[i]) { u |= (ushort)(0b1 << i); }
            }
            return u;

        }

    }

}
