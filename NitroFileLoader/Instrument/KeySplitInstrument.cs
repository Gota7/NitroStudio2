using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GotaSequenceLib;
using GotaSoundIO.IO;

namespace NitroFileLoader {

    /// <summary>
    /// A key split instrument.
    /// </summary>
    public class KeySplitInstrument : Instrument {

        /// <summary>
        /// Read the instrument.
        /// </summary>
        /// <param name="r">The reader.</param>
        public override void Read(FileReader r) {

            //Get indices.
            List<byte> indices = new List<byte>();
            for (int i = 0; i < 8; i++) {
                byte b = r.ReadByte();
                if (b != 0) {
                    indices.Add(b);
                }
            }

            //Read note parameters.
            for (int i = 0; i < indices.Count; i++) {

                //Get the instrument type.
                InstrumentType t = (InstrumentType)r.ReadUInt16();
                NoteInfo.Add(r.Read<NoteInfo>());
                NoteInfo.Last().Key = (Notes)indices[i];
                NoteInfo.Last().InstrumentType = t;

            }

        }      

        /// <summary>
        /// Write the intrument.
        /// </summary>
        /// <param name="w">The writer.</param>
        public override void Write(FileWriter w) {

            //Get indices.
            var indices = NoteInfo.Select(x => (byte)x.Key).ToArray();
            w.Write(indices);
            w.Write(new byte[8 - indices.Length]);

            //Write instrument info.
            foreach (var v in NoteInfo) {
                w.Write((ushort)v.InstrumentType);
                w.Write(v);
            }

        }

        /// <summary>
        /// The instrument type.
        /// </summary>
        /// <returns>The instrument type.</returns>
        public override InstrumentType Type() => InstrumentType.KeySplit;

        /// <summary>
        /// Max instruments.
        /// </summary>
        /// <returns>The max instruments.</returns>
        public override uint MaxInstruments() => 8;

    }
}
