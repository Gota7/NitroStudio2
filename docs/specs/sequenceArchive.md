# Sequence Archives (.ssar)
An SSAR or sound sequence archive is like a giant sequence that contains multiple entry points. Each of those entry points contain sequence info similar to that found in the main sound data archive.

## The Main File
The main file contains of a File Header and a Data Block.

| **Type** | **Description** |
|----------|-----------------|
|SoundFileHeader|Sound File Header (Magic: SSAR. Always contains 1 block)|
|Block|Data Block|

## Data Block Body (Magic: DATA)
Contains sequence entries, and sequence commands to execute.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|a32|Absolute offset from the start of the SSAR file to the sequence data. Formula for calulating is 0x20 + 12 * NumSequences|
|0x04|Table<`SequenceEntry`>|Sequences|
|----|u8[-]|See the SSEQ specification on how to parse the sequence commands. Just be sure to separate it from padding by removing 0s until 0xFF is reached|

### Sequence Entry
Each sequence inside the sequence archive has the following layout:

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|u32|Offset to start of the sequence's sequence data relative to the start of the sequence command data. The rest of the information is 0'd out if this is 0 as that means it is a null entry|
|0x04|u16|Bank Id|
|0x06|u8|Volume|
|0x07|u8|Channel Priority|
|0x08|u8|Player Priority|
|0x09|u8|Player Id|
|0x0A|u16|Padding|