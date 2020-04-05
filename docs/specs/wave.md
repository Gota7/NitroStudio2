# Waves (.swav)
SWAVs or Sound Waves are like WAV files, except are always only one channel and can be IMA-ADPCM encoded.

## The Main File
The main file contains of a File Header and an Data Block.

| **Type** | **Description** |
|----------|-----------------|
|SoundFileHeader|Sound File Header (Magic: SWAV. Always contains 1 block)|
|Block|Data Block|

## Data Block Body (Magic: DATA)
Contains audio information and how to read it.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|SoundEncoding|Type of encoding for the data|
|0x01|bool|If the sound loops|
|0x02|u16|Sampling rate of the audio|
|0x04|u16|Clock time of the audio (16756991 / SampleRate)|
|0x06|u16|Loop start offset. If audio does not loop, this is 1 for IMA-ADPCM and 0 for the other encodings|
|0x08|u32|Loop end offset. If audio does not loop, this is 1 for IMA-ADPCM and 0 for the other encodings|
|0x0C|AudioData|The rest of the block data is audio data for the channel. Its size is BlockSize - 0xC - 0x8|

### Converting Data Offset To Sample Position

| **Encoding** | **Algorithm** |
|--------------|---------------|
|Signed PCM8|Offset|
|Signed PCM16|Offset / 2|
|IMA-ADPCM|Offset * 2 - 8|