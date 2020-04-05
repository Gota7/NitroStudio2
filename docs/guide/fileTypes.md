# File Types
When using Nitro Studio 2, you will encounter many different file types.

| **Extension Name** | **Extension Description** | **SDAT Or Inside** | **Description** |
|--------------------|---------------------------|--------------------|-----------------|
|SDAT|Sound DATa|Yes|This is the binary format of the Sound Archive|
|SARC|Sound ARChive|No|This is the SDK's text format of a Sound Archive|
|SPRJ|Sound PRoJect|No|This is a Sound Project file that is used by NITRO-SoundMaker to compile Sound Archives|
|SSEQ|Sound SEQuence|Yes|This is the binary form of a Sequence, it's like an MIDI|
|SMFT|SMF Text|No|This is the SDK's text format of a Sequence. It is described to be like an MIDI but in plain text|
|MIDI|Musical Instrument Digital Interface|No|MIDI files don't contain sounds themselves, but rather tell instruments how to play sounds such as what notes to play|
|SSAR|Sound Sequence ARchive|Yes|This is the binary form of a Sequence Archive. It is a long Sequence file that contains multiple entry points for each Sequence within the archive. It's kind of a collection of Sequences but in one file|
|MUS|MUSic List|No|This is the SDK's text format of a Sequence Archive|
|SBNK|Sound BaNK|Yes|This is the binary form of a Bank. It contains instruments that tell the Sequence Player how to play notes. However, this only references audio samples from Wave Archives for instruments and contains no audio samples in itself|
|BNK|Bank|No|This is the SDK's text format of a Bank|
|DLS|DownLoadable Sounds|No|This is used by PC MIDI editors as an instrument bank|
|SF2|Sound Font 2|No|This is used by PC MIDI editors as an instrument bank|
|NS2I|Nitro Studio 2 Instrument|No|An instrument format used in Nitro Studio 2 that contains instrument info and audio samples to easily exchange instruments between different banks|
|NIST|Nitro Studio InSTrument|No|An instrument format used in Nitro Studio that contains instrument info, but unlike NS2I doesn't contain audio samples|
|SWAR|Sound Wave ARchive|Yes|This is the binary form of a Wave Archive. It contains Wave files|
|SWLS|Sound Wave LiSt|No|This is the SDK's text format of a Wave Archive|
|SWAV|Sound Wave|Yes|This is the binary form of a Wave. It contains audio sample data|
|WAV|WAVe|No|This is a standard wave file used by PCs|
|STRM|STReaM|Yes|This is the binary form of a Stream that contains audio sample data|

# Next
Now that you understand all the different kinds of files used, it's time to move on to:

[Sequence Commands](sequenceCommands.md)