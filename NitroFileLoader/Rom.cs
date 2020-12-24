using GotaSoundIO.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NitroFileLoader {

    /// <summary>
    /// An NDS ROM.
    /// </summary>
    public class Rom : IOFile {

        /// <summary>
        /// Game name.
        /// </summary>
        public string GameName;

        /// <summary>
        /// Game code.
        /// </summary>
        public string GameCode;

        /// <summary>
        /// Read the ROM.
        /// </summary>
        /// <param name="r">The reader.</param>
        public override void Read(FileReader r) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Write the ROM.
        /// </summary>
        /// <param name="w">The writer.</param>
        public override void Write(FileWriter w) {
            throw new NotImplementedException();
        }

    }

}
