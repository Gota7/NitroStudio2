using GotaSoundIO.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NitroFileLoader {

    /// <summary>
    /// Stream info.
    /// </summary>
    public class StreamInfo : IReadable, IWriteable {

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
        public Stream File;

        /// <summary>
        /// Stream player.
        /// </summary>
        public StreamPlayerInfo Player;

        /// <summary>
        /// Volume.
        /// </summary>
        public byte Volume = 100;

        /// <summary>
        /// Priority.
        /// </summary>
        public byte Priority = 0x40;

        /// <summary>
        /// Reading file Id.
        /// </summary>
        public uint ReadingFileId;

        /// <summary>
        /// Player Id.
        /// </summary>
        public byte ReadingPlayerId;

        /// <summary>
        /// Mono to stereo.
        /// </summary>
        public bool MonoToStereo;

        /// <summary>
        /// Read the info.
        /// </summary>
        /// <param name="r">The reader.</param>
        public void Read(FileReader r) {
            ReadingFileId = r.ReadUInt32();
            MonoToStereo = (ReadingFileId & 0xFF000000) > 0;
            ReadingFileId &= 0xFFFFFF;
            Volume = r.ReadByte();
            Priority = r.ReadByte();
            ReadingPlayerId = r.ReadByte();
            r.ReadBytes(5);
        }

        /// <summary>
        /// Write the info.
        /// </summary>
        /// <param name="w">The writer.</param>
        public void Write(FileWriter w) {
            w.Write((uint)(ReadingFileId | (MonoToStereo ? 0x01000000 : 0)));
            w.Write(Volume);
            w.Write(Priority);
            w.Write((byte)(Player != null ? Player.Index : ReadingPlayerId));
            w.Write(new byte[5]);
        }

    }

}
