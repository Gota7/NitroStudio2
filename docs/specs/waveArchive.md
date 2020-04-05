# Wave Archives (.swar)
SWARs or Sound Wave Archives are like folders for wave files.

## The Main File
The main file contains of a File Header and a Data Block.

| **Type** | **Description** |
|----------|-----------------|
|SoundFileHeader|Sound File Header (Magic: SWAR. Always contains 1 block)|
|Block|Data Block|

## Data Block Body (Magic: DATA)
Contains a series of headerless wave files. Note that the data for the waves is only the content in Data Block Body.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|u32[8]|Padding|
|0x20|Table<`a32`>|Wave offsets|
|----|WaveBlockBody[NumWaves]|Wave file data|