using GotaSoundIO.IO;
using GotaSoundIO.Sound;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NitroFileLoader {

    /// <summary>
    /// Wave archive info.
    /// </summary>
    public class WaveArchiveInfo : IReadable, IWriteable {

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
        public WaveArchive File;

        /// <summary>
        /// Reading file Id.
        /// </summary>
        public uint ReadingFileId;

        /// <summary>
        /// Load wave archive individually.
        /// </summary>
        public bool LoadIndividually;

        /// <summary>
        /// Read the info.
        /// </summary>
        /// <param name="r">The reader.</param>
        public void Read(FileReader r) {
            ReadingFileId = r.ReadUInt32();
            LoadIndividually = (ReadingFileId & 0xFF000000) > 0;
            ReadingFileId &= 0xFFFFFF;
        }

        /// <summary>
        /// Write the info.
        /// </summary>
        /// <param name="w">The writer.</param>
        public void Write(FileWriter w) {
            w.Write((uint)(ReadingFileId | (LoadIndividually ? 0x01000000 : 0)));
        }

        /// <summary>
        /// Write the text format, and dump files.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="name">The name.</param>
        public void WriteTextFormat(string path, string name) {

            //SWLS.
            List<string> swls = new List<string>();
            int ind = 0;
            Directory.CreateDirectory(path + "/" + name);
            foreach (var w in File.Waves) {
                swls.Add(name + "/" + ind.ToString("D4") + ".adpcm.swav");
                w.Write(path + "/" + name + "/" + ind.ToString("D4") + ".adpcm.swav");
                RiffWave r = new RiffWave();
                r.FromOtherStreamFile(w);
                r.Write(path + "/" + name + "/" + ind.ToString("D4") + ".wav");
                ind++;
            }
            System.IO.File.WriteAllLines(path + "/" + name + ".swls", swls);

        }

    }

}
