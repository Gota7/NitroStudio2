using GotaSoundIO.IO;
using GotaSoundIO.Sound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NitroFileLoader {

    /// <summary>
    /// A wave.
    /// </summary>
    public class Wave : SoundFile {

        /// <summary>
        /// Supported encodings.
        /// </summary>
        /// <returns>The supported encodings.</returns>
        public override Type[] SupportedEncodings() => new Type[] { typeof(ImaAdpcm), typeof(PCM16), typeof(PCM8Signed) };

        /// <summary>
        /// Name.
        /// </summary>
        /// <returns>The name.</returns>
        public override string Name() => "SWAV";

        /// <summary>
        /// Extensions.
        /// </summary>
        /// <returns>The extensions.</returns>
        public override string[] Extensions() => new string[] { "SWAV" };

        /// <summary>
        /// Description.
        /// </summary>
        /// <returns>The description.</returns>
        public override string Description() => "A SWAV used in Nintendo DS games.";

        /// <summary>
        /// If the file supports tracks.
        /// </summary>
        /// <returns>It doesn't.</returns>
        public override bool SupportsTracks() => false;

        /// <summary>
        /// Preferred encoding.
        /// </summary>
        /// <returns>The preferred encoding.</returns>
        public override Type PreferredEncoding() => typeof(ImaAdpcm);

        /// <summary>
        /// Backup nTime to minimize inconsistencies.
        /// </summary>
        private ushort BackupNTime;

        /// <summary>
        /// Read the file.
        /// </summary>
        /// <param name="r">The reader.</param>
        public override void Read(FileReader r) {

            //Read header.
            r.OpenFile<NHeader>(out _);

            //Data block.
            r.OpenBlock(0, out _, out _, false);
            r.ReadUInt32();
            uint dataSize = r.ReadUInt32() - 8;
            var w = ReadShortened(r, dataSize);
            Loops = w.Loops;
            LoopStart = w.LoopStart;
            LoopEnd = w.LoopEnd;
            SampleRate = w.SampleRate;
            Audio = w.Audio;

        }

        /// <summary>
        /// Read a shortened wave.
        /// </summary>
        /// <param name="r">The reader.</param>
        /// <param name="length">The data length.</param>
        /// <returns>A wave.</returns>
        public static Wave ReadShortened(FileReader r, uint length) {

            //Set up wave.
            Wave w = new Wave();
            PcmFormat pcmFormat = (PcmFormat)r.ReadByte();
            w.Loops = r.ReadBoolean();
            int numChannels = 1;
            w.SampleRate = r.ReadUInt16();
            w.BackupNTime = r.ReadUInt16();
            w.LoopStart = r.ReadUInt16();
            r.ReadUInt32(); //Non-loop length.

            //Data size.
            uint dataSize = length - 12;
            w.LoopEnd = dataSize * 2;

            //Loop start offset is divided by 4 for some reason.
            w.LoopStart = Offset2Samples(w.LoopStart * 4, pcmFormat);
            w.LoopEnd = Offset2Samples(dataSize, pcmFormat);

            //Switch type.
            Type format = null;
            switch (pcmFormat) {
                case PcmFormat.SignedPCM8:
                    format = typeof(PCM8Signed);
                    break;
                case PcmFormat.PCM16:
                    format = typeof(PCM16);
                    break;
                case PcmFormat.Encoded:
                    format = typeof(ImaAdpcm);
                    break;
            }

            //Read channels.
            w.Audio.Read(r, format, numChannels, (int)dataSize, (int)w.LoopEnd, 0);

            //Return the wave.
            return w;

        }

        /// <summary>
        /// Write a shortened wave.
        /// </summary>
        /// <param name="w">The writer.</param>
        public void WriteShortened(FileWriter w) {

            //Format.
            PcmFormat pcmFormat = PcmFormat.Encoded;
            if (Audio.EncodingType.Equals(typeof(PCM8Signed))) {
                w.Write((byte)PcmFormat.SignedPCM8);
                pcmFormat = PcmFormat.SignedPCM8;
            } else if (Audio.EncodingType.Equals(typeof(PCM16))) {
                w.Write((byte)PcmFormat.PCM16);
                pcmFormat = PcmFormat.PCM16;
            } else if (Audio.EncodingType.Equals(typeof(ImaAdpcm))) {
                w.Write((byte)PcmFormat.Encoded);
                pcmFormat = PcmFormat.Encoded;
            } else {
                throw new Exception("Invalid channel format!");
            }

            //Data.
            w.Write(Loops);
            w.Write((ushort)SampleRate);
            ushort nTimeSampleRate = (ushort)(16756991 / SampleRate);
            if (BackupNTime != 0) { w.Write(BackupNTime); } else { w.Write(nTimeSampleRate); }
            if (Loops) { w.Write((ushort)(Sample2Offset(LoopStart, pcmFormat) / 4)); } else { w.Write((ushort)(pcmFormat == PcmFormat.Encoded ? 1 : 0)); }
            if (Loops) { w.Write((uint)((Audio.DataSize - Sample2Offset(LoopStart, pcmFormat)) / 4)); } else { w.Write((uint)((Audio.DataSize - Sample2Offset((uint)(pcmFormat == PcmFormat.Encoded ? 1 : 0), pcmFormat)) / 4)); }
            Audio.Write(w);

        }

        /// <summary>
        /// Write the file.
        /// </summary>
        /// <param name="w">The writer.</param>
        public override void Write(FileWriter w) {

            //Write the file.
            w.InitFile<NHeader>("SWAV", ByteOrder.LittleEndian, null, 1);
            w.InitBlock("DATA");
            WriteShortened(w);
            w.CloseBlock();
            w.CloseFile();

        }

        /// <summary>
        /// Mixing to mono is required.
        /// </summary>
        public override void BeforeConversion() {
            Audio.MixToMono();
            Audio.ChangeBlockSize(-1);
        }

        /// <summary>
        /// On conversion.
        /// </summary>
        public override void AfterConversion() {
            TrimAfterLoopEnd();
            LoopEnd = (uint)Audio.NumSamples;
        }

        /// <summary>
        /// Convert an offset to a sample.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="format">The format.</param>
        /// <returns>The sample number.</returns>
        public static uint Offset2Samples(uint offset, PcmFormat format) {

            //Sample.
            uint samples = offset;
            switch (format) {
                case PcmFormat.SignedPCM8:
                    return samples;
                case PcmFormat.PCM16:
                    return samples / 2;
                case PcmFormat.Encoded:
                    return samples * 2 - 8;
            }
            return 0;

        }

        /// <summary>
        /// Convert a sample to an offset.
        /// </summary>
        /// <param name="sample">The offset.</param>
        /// <param name="format">The format.</param>
        /// <returns>The offset number.</returns>
        public static uint Sample2Offset(uint sample, PcmFormat format) {

            //Offset.
            uint offset = sample;
            switch (format) {
                case PcmFormat.SignedPCM8:
                    return offset;
                case PcmFormat.PCM16:
                    return offset * 2;
                case PcmFormat.Encoded:
                    return (offset + 8) / 2;
            }
            return 0;

        }

    }

    /// <summary>
    /// Pcm format.
    /// </summary>
    public enum PcmFormat : byte {
        SignedPCM8, PCM16, Encoded
    }

}
