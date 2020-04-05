# Streams (.strm)
STRMs or Streams are like WAV files, except that audio data is chunked and can be IMA-ADPCM encoded.

## The Main File
The main file contains of a File Header and an Info Block.

| **Type** | **Description** |
|----------|-----------------|
|SoundFileHeader|Sound File Header (Magic: STRM. Always contains 2 blocks)|
|Block|Info Block|

## Head Block (Magic: HEAD)
Contains information on how to read the audio data.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|SoundEncoding|Type of encoding for the data|
|0x01|bool|If the sound loops|
|0x02|u8|Number of audio channels|
|0x03|u8|Padding|
|0x04|u16|Sampling rate of the audio|
|0x06|u16|Clock time of the audio (523655.96875 * (1 / SampleRate)) rounded down for whatever reason|
|0x08|u32|Loop start in samples|
|0x0C|u32|Number of samples|
|0x10|a32|Absolute offset to audio data. Is always 0x68|
|0x14|u32|Number of blocks. Always 1 for encodings not IMA-ADPCM|
|0x18|u32|The size of a block, 0x200 for IMA-ADPCM and the size per audio channel for other encodings|
|0x1C|u32|The number of samples per block, 0x3F8 for IMA-ADPCM and the samples per audio channel for other encodings|
|0x20|u32|Size of the last block|
|0x24|u32|Number of samples in the last block|
|0x28|u8[0x20]|Padding|

## Data Block (Magic: DATA)
Contains the raw audio data. It is organized in a fashion such as this: { Block 0 Channel 0, Block 0 Channel 1, Block 1 Channel 0, Block 1 Channel 1, etc. } Each block is IMA-ADPCM encoded separately.