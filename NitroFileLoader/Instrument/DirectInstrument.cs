using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GotaSoundIO.IO;

namespace NitroFileLoader {

    /// <summary>
    /// Direct instrument.
    /// </summary>
    public class DirectInstrument : Instrument {

        /// <summary>
        /// Get the instrument type.
        /// </summary>
        /// <returns>The instrument type.</returns>
        public override InstrumentType Type() => NoteInfo[0].InstrumentType;

        /// <summary>
        /// Max instruments.
        /// </summary>
        /// <returns>The max instruments.</returns>
        public override uint MaxInstruments() => 1;

        /// <summary>
        /// Read the instrument.
        /// </summary>
        /// <param name="r">The reader.</param>
        public override void Read(FileReader r) {
            NoteInfo.Add(r.Read<NoteInfo>());
            NoteInfo.Last().Key = GotaSequenceLib.Notes.gn9;
        }

        /// <summary>
        /// Write the instrument.
        /// </summary>
        /// <param name="w">The writer.</param>
        public override void Write(FileWriter w) {
            w.Write(NoteInfo[0]);
        }

    }

}
