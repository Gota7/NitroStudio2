# Banks (.sbnk)
SBNKs or Sound Banks contain instrument information.

## The Main File
The main file contains of a File Header and a Data Block.

| **Type** | **Description** |
|----------|-----------------|
|SoundFileHeader|Sound File Header (Magic: SBNK. Always contains 1 block)|
|Block|Data Block|

## Data Block Body (Magic: DATA)
Contains a series of headerless wave files. Note that the data for the waves is only the content in Data Block Body.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|u32[8]|Padding|
|0x20|Table<`InstrumentEntry`>|Instrument Entries|
|----|Instrument[NumInstruments]|Instruments|

### Instrument Entry
Each instrument has an entry as follows. If the offset is 0, then the instrument is null and has no data.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|InstrumentType|Instrument Type|
|0x01|a16|Absolute offset to instrument data|
|0x03|u8|Padding|

#### Instrument Type
An enumeration of the instrument type. Each type is a byte each.

| **Identifier** | **Description** |
|----------------|-----------------|
|0|Null (no note info whatsoever)|
|1|PCM|
|2|PSG|
|3|White Noise|
|4|Direct PCM (Unused)|
|5|Null (has note info but 0'd out)|
|16|Drum Set|
|17|Key Split|

## Instruments
Depending on the type of instrument, its data will differ.

### Direct Instrument
This is the default instrument type and has a single note info for the entire key range. Instrument type is decided from the instrument entry linking to this.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|NoteInfo|Note Info|

### Drum Set Instrument
This defines instruments individually for a key range. It can not contain another drum set or key split instrument.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|u8|Lower Key|
|0x01|u8|Upper Key|
|0x02|ContainedDirectInstrument[UpperKey - LowerKey + 1]|Instrument data for each key|

### Key Split Instrument
This defines instruments to certain regions of the keyboard. It can not contain another drum set or key split instrument.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|u8[8]|Regions. A region works by the key to the byte specified. For example, { 40, 57, 60, 90, 127, 0, 0, 0 } means that there are 5 regions: 0-40, 41-57, 58-60, 61-90, 91-127|
|0x08|ContainedDirectInstrument[NumActualRegions]|Instrument data for each region|

### Contained Direct Instrument
A direct instrument inside of a drum set or key split instrument in it has an identifer.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|u8|Padding|
|0x01|InstrumentType|Instrument Type|
|0x02|NoteInfo|Note Info|

### Note Info
This is how you play back a note.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|u16|Wave Id (PCM) or Duty Cycle Type (PSG)|
|0x02|u16|Wave Archive Id (PCM) is hooked up to the wave archives specified by the sound data archive for the bank info that uses this file|
|0x04|u8|Base Note|
|0x05|u8|Attack|
|0x06|u8|Decay|
|0x07|u8|Sustain|
|0x08|u8|Release|
|0x09|u8|Pan|