# Common Structures
In every type of sound file, you are bound to come across at least one of these structures. Rather than redefining them for each file, it would be better to define them all here for simplicity.

## Primitive Types
Every structure is made of the following primitive types. You will come across the following:

| **Type** | **Description** |
|----------|-----------------|
|u8|8-bit Unsigned Integer|
|u16|16-bit Unsigned Integer|
|u24|32-bit Unsigned Integer|
|u32|32-bit Unsigned Integer|
|r32|32-bit Relative Offset|
|a16|16-bit Absolute Offset|
|a32|32-bit Absolute Offset|
|s8|8-bit Signed Integer|
|s16|16-bit Signed Integer|
|s32|32-bit Signed Integer|
|bool|8-bit Value That's 0 If False|
|string|Since NintendoWare was coded in C++, every string is null terminated and includes '\0' at the end. This null terminator is included in the length of the string|
|char[Array Size]|Char Array. This is like a string, except that it does not have a null terminator, and always has a fixed size|
|VL|A variable length parameter. To read it, read a byte, binary and it with 0x7F, and if binary anding the original value by 0x80 is 1, shift that value left 7 bits, and binary or it with the same process you just did for the next bytes, until anding the value with 0x80 is 0|

A relative offset is relative to the start of a block. An absolute offset is relative to the start of a file. An offset of 0 means it is a null pointer, and so contains null or no data.

## Sound File Header
This is essential for the game to know how to read the data in the file. It is always 0x10 bytes.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|char[4]|Magic identifer for the file|
|0x04|u16|Byte order. 0xFEFF for big endian, and 0xFFFE for little endian. Note, that these values I gave are in big endian|
|0x06|Version|Version of this file. It is always 1.0|
|0x08|u32|Size of this file|
|0x0C|u16|Size of this header|
|0x0E|u16|Number of blocks in the file|

### File Version
Version is in the format 0xAABB, where AA is the major version, and BB is the minor version. For example, 0x0100 is 1.0.

## Block
A block has a header and a size, and is always padded to 0x4 bytes. Any offset encountered in a block, unless otherwise specified, is relative to the start of that block.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|char[4]|Magic identifer for the block|
|0x08|u32|Size of this block (including this block header)|
|0x0C|u8[BlockSize - 8]|Block body. This is interpreted differently for every file|
|----|---|Padding for 0x4 alignment|

## Sound Encoding
Sound encoding is one byte, and has the following enumeration:

| **Identifier** | **Description** |
|----------------|-----------------|
|0|Signed PCM8|
|1|Signed PCM16|
|2|4-bit IMA-ADPCM|

## Table<`Type T`>
This contains a collection of whatever the Type T is. For example, a table could contain u8s, u32s, or anything else. Tables are always padded to 0x4 bytes.

| **Offset** | **Type** | **Description** |
|------------|----------|-----------------|
|0x00|u32|Number of entries|
|0x04|T[Number of entries]|An array of Type T, with the number of entries set to the value above|
|----|---|Padding for alignment|