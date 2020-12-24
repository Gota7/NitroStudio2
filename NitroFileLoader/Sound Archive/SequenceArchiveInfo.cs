using GotaSoundIO.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NitroFileLoader {

    /// <summary>
    /// Sequence archive info.
    /// </summary>
    public class SequenceArchiveInfo : IReadable, IWriteable {

        /// <summary>
        /// Name.
        /// </summary>
        public string Name;

        /// <summary>
        /// Entry index.
        /// </summary>
        public int Index;

        /// <summary>
        /// Force the file to be individualized.
        /// </summary>
        public bool ForceIndividualFile;

        /// <summary>
        /// File.
        /// </summary>
        public SequenceArchive File;

        /// <summary>
        /// Reading file Id.
        /// </summary>
        public uint ReadingFileId;

        /// <summary>
        /// Read the info.
        /// </summary>
        /// <param name="r">The reader.</param>
        public void Read(FileReader r) {
            ReadingFileId = r.ReadUInt32();
        }

        /// <summary>
        /// Write the info.
        /// </summary>
        /// <param name="w">The writer.</param>
        public void Write(FileWriter w) {
            w.Write(ReadingFileId);
        }

    }

}
