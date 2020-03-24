# Sequences (.sseq)
SSEQs or Sound Sequences are like MIDI files, except for the NDS. They execute instructions for instruments to play.

## The Main File
The main file contains of a File Header and a Data Block.

| **Type** | **Description** |
|----------|-----------------|
|SoundFileHeader|Sound File Header (Magic: SSEQ. Always contains 1 block)|
|Block|Data Block|

## Data Block Body (Magic: DATA)
Contains sequence commands to execute.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|a32|Absolute offset from the start of the SSEQ file to the sequence data|
|0x08|u8[BlockSize - 0xC - PaddingSize]|Sequence commands. Remember that all blocks are padded to 0x4 bytes, so remove 0s from the end until you hit 0xFF to get an accurate list of data|

### Sequence Commands
Here is a table of sequence commands. Each sequence command has a u8 identifier and can be followed by parameters. P1 is parameter one, where P2 is parameter two, etc. In the event that the parameter is a sequence command, the last primary data type for it is not to be read. For example, a variable command that contains a note parameter will not have the variable length parameter (the note length) read, but after that note command is read the variable number for the variable command will be. All offsets are relative to the start of the sequence data.

| **Identifier** | **Parameters** | **Read Last Parameter In Sequence Command?** | **Command Name** | **Description ** |
|0x00 - 0x7F|u8, VL|NA|Note Command|Plays the specified key with a velocity of P1 for P2 ticks|
|0x80|VL|NA|Wait|Wait for P1 ticks before continuing|
|0x93|u8, VL|NA|Open Track|Open track P1 whose data starts at offset P2|
|0x94|u24|NA|Jump|Jump to offset P1|
|0x95|u24|NA|Call|Jump to offset P1 to execute the instructions there, then resume playback after the return command is encountered|
|0xA0|Sequence Command, s16, s16|No|Random|Replaces the last parameter of P1 with a random number between P2 and P3|
|0xA1|Sequence Command, u8|No|Variable|Replaces the last parameter of P1 with the value of variable P2|
|0xA2|Sequence Command|Yes|If|Executes P1 if the track's conditional flag is set|
|0xB0|u8, s16|NA|Set Variable|Sets variable P1 to have the value P2|
|0xB1|u8, s16|NA|Add Variable|Adds P2 to the variable P1|
|0xB2|u8, s16|NA|Subtract Variable|Subtracts P2 from the variable P1|
|0xB3|u8, s16|NA|Multiply Variable|Multiplies variable P1 by P2|
|0xB4|u8, s16|NA|Divide Variable|Divides variable P1 by P2|
|0xB5|u8, s16|NA|Shift Variable|Sets variable P1 to have the value P2|
|0xB6|u8, s16|NA|Random Variable|Sets variable P1 to have a random value between and including 0 and P2|
|0xB8|u8, s16|NA|Compare Equal|If variable P1 equals P2, set the track conditional flag, else reset the flag|
|0xB9|u8, s16|NA|Compare Greater Than Or Equal|If variable P1 is greater than or equal to P2, set the track conditional flag, else reset the flag|
|0xBA|u8, s16|NA|Compare Greater Than|If variable P1 is greater than P2, set the track conditional flag, else reset the flag|
|0xBB|u8, s16|NA|Compare Less Than Or Equal|If variable P1 is less than or equal to P2, set the track conditional flag, else reset the flag|
|0xBC|u8, s16|NA|Compare Less Than|If variable P1 is less than P2, set the track conditional flag, else reset the flag|
|0xBD|u8, s16|NA|Compare Not Equal|If variable P1 doesn't equal P2, set the track conditional flag, else reset the flag|
|0xC0|u8|NA|Pan|Sets the track panning to P1 where 0x40 is the center|
|0xC1|u8|NA|Volume|Sets the track volume to P1|
|0xC2|u8|NA|Main Volume|Sets the player volume to P1|
|0xC3|s8|NA|Transpose|Sets the track transpose to P1|
|0xC4|s8|NA|Pitchbend|Sets the track pitch bend to P1|
|0xC5|u8|NA|Bend Range|Sets the track bend range to P1|
|0xC6|u8|NA|Priority|Sets the track priority to P1|
|0xC7|bool|NA|Note Wait|Is off by default, but if on waits for a note to finish before continuing|
|0xC8|bool|NA|Tie|If on, notes don't end and new notes just change the pitch and velocity of the playing note|
|0xC9|u8|NA|Portamento|Sets track portamento to P1|
|0xCA|u8|NA|Mod Depth|Sets the track mod depth to P1|
|0xCB|u8|NA|Mod Speed|Sets the track mod speed to P1|
|0xCC|u8|NA|Mod Type|Sets the track mod type to P1|
|0xCD|u8|NA|Mod Range|Sets the track mod range to P1|
|0xCE|bool|NA|Portamento Switch|Enters or cancels portamento mode|
|0xCF|u8|NA|Portamento Time|Sets the portamento time to P1|
|0xD0|u8|NA|Attack|Sets the envelope attack to P1|
|0xD1|u8|NA|Decay|Sets the envelope decay to P1|
|0xD2|u8|NA|Sustain|Sets the envelope sustain to P1|
|0xD3|u8|NA|Release|Sets the envelope release to P1|
|0xD4|u8|NA|Loop Start|Sets the track loop start point, and loop P1 times. It is infinite if P1 is 0|
|0xD5|u8|NA|Volume 2|Sets track volume 2 to P1|
|0xD6|u8|NA|Print Variable|Print variable P1 to a debugger|
|0xE0|s16|NA|Mod Delay|Sets the track mod delay to P1|
|0xE1|s16|NA|Tempo|Sets the tempo to P1|
|0xE2|s16|NA|Sweep Pitch|Sets the track sweep pitch to P1|
|0xFC|None|NA|Loop End|End the track loop|
|0xFD|None|NA|Return|Return from a call|
|0xFE|u16|NA|Allocate Tracks|Bitflag P1 for how to allocate tracks|
|0xFF|None|NA|Fin|End the track or sequence|