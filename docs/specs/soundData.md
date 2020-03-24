# Sound Data (.sdat)
SDAT or Sound Data contains sound data for the game.

## The Main File
The main file contains of a File Header, and optional Symbol block, an Info block, a File Allocation Block, and a File block. The entire file is padded to 0x20 bytes.

| **Type** | **Description** |
|----------|-----------------|
|SDATFileHeader|SDAT File Header (Magic: SDAT. Always contains 3 or 4 blocks)|
|Block|Symbol Block|
|Block|Info Block|
|Block|File Allocation Block|
|Block|File Block|
|-----|Padding|

## SDAT File Header
The standard file header for the SDAT file. The header is padded by 0x20 bytes.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|char[4]|Magic identifier for the file (SDAT)|
|0x04|u16|Byte order. 0xFEFF for big endian, and 0xFFFE for little endian. Note, that these values I gave are in big endian|
|0x06|Version|Version of this file. It is always 1.0|
|0x08|u32|Size of this file|
|0x0C|u16|Size of this header|
|0x0E|u16|Number of blocks in the file|
|0x10|a32|Offset to Symbol block (can be null)|
|0x14|u32|Symbol block size (0 if null)|
|0x18|a32|Offset to Info block|
|0x1C|u32|Info block size|
|0x20|a32|Offset to File Allocation block|
|0x24|u32|File Allocation block size|
|0x28|a32|Offset to File block|
|0x2C|u32|File block size|

## Symbol Block
Contains names for entries in the Info block. This block is optional. Magic is SYMB.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|r32|Reference to sequence name table|
|0x04|r32|Reference to sequence archive name table|
|0x08|r32|Reference to bank name table|
|0x0C|r32|Reference to wave archive name table|
|0x10|r32|Reference to player name table|
|0x14|r32|Reference to group name table|
|0x18|r32|Reference to stream player name table|
|0x1C|r32|Reference to stream name table|
|0x20|u32[0x6]|Padding for alignment|

Each reference points to a Table<`r32`> where each reference leads to a null terminated string (can be null if an entry doesn't exist for that index), except for the offset for sequence archives, as they lead to something different.

### Sequence Archive Symbol Entry

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|r32|Reference to sequence archive name|
|0x04|r32|Reference to Table<`r32`> of sequence names|

## Info Block
Contains information about all the entries in the sound archive. If the symbol block exists, an info and symbol entry with the same index in a table means that the info links with that string and has that name. Magic is INFO.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|r32|Reference to sequence info table|
|0x04|r32|Reference to sequence archive info table|
|0x08|r32|Reference to bank info table|
|0x0C|r32|Reference to wave archive info table|
|0x10|r32|Reference to player info table|
|0x14|r32|Reference to group info table|
|0x18|r32|Reference to stream player info table|
|0x1C|r32|Reference to stream info table|
|0x20|u32[0x6]|Padding for alignment|

Each reference points to a Table<`r32`> where each reference leads to a its corresponding info information. If an offset to an item is 0, then there is no data for that index.

### Sequence Info
Contains information for each sequence.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|u32|File Id|
|0x04|u16|Bank Id|
|0x06|u8|Volume|
|0x07|u8|Channel Priority|
|0x08|u8|Player Priority|
|0x09|u8|Player Id|
|0x0A|u16|Padding|

### Sequence Archive Info
Contains information for each sequence archive.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|u32|File Id|

### Bank Info
Contains information for each bank.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|u32|File Id|
|0x04|s16|Wave Archive 0 (can be -1 for null)|
|0x08|s16|Wave Archive 1 (can be -1 for null)|
|0x0C|s16|Wave Archive 2 (can be -1 for null)|
|0x10|s16|Wave Archive 3 (can be -1 for null)|

### Wave Archive Info
Contains information for each wave archive.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|u32|File Id in form 0xLLFFFFFF where F is the file id, and L is a bool to whether or not the wave archive should be loaded individually|

### Player Info
Contains information for each player.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|u16|Max number of sequences|
|0x02|u16|Bitflags to allocate for each channel. A value of 0 allocates all channels automatically|
|0x04|u32|Heap size to reserve in memory|

### Group Info
Contains information for each group.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|Table<`GroupEntry`>|Group entries|

#### Group Entry
An entry to a group has the following layout:

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|EntryType|Type of entry in the group|
|0x01|u8|Bitflags of what to load. If binary anding the value with (0b1 << EntryType) is not 0, then it'll load that value. For example, 0x3 loads the sequence and bank|
|0x02|u16|Padding|
|0x04|u32|Entry Id|

##### Entry Type
An enumeration for the entry type. Each identifier is a byte.

| **Identifier** | **Description** |
|----------------|-----------------|
|0|Sequence|
|1|Bank|
|2|Wave Archive|
|3|Sequence Archive|

### Stream Player Info
Contains information for each stream player.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|u8|Number of channels (1 for mono, 2 for stereo)|
|0x01|u8|Left/Mono channel|
|0x02|u8|Right channel (if stereo)|
|0x03|u8[21]|Padding|

### Stream Info
Contains information for each stream.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|u32|File Id in form 0xLLFFFFFF where F is the file id, and L is a bool to whether or not the stream should be automatically converted to stereo|
|0x04|u8|Volume|
|0x05|u8|Priority|
|0x06|u8|Stream Player Id|
|0x07|u8[0x5]|Padding|

## File Allocation Block
Contains a table of file entries that lead to the file positions and their sizes. Magic is "FAT ".

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|Table<`FileEntry`>|File entries|

### File Entry

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|a32|Absolute offset to the file (is never null)|
|0x04|u32|File size|
|0x08|u32[2]|Padding|

## File Block
This block contains all the files. Since this is the last block, it is aligned to 0x20. The first file starts after a 0x20 alignment. A 0x20 alignment is applied after each file. Magic is FILE.