# About Nitro Studio 2
Nitro Studio 2 is an editor for Nintendo DS sound archives (SDAT) which contain music, sound effects, and anything audio related in general. Nitro Studio 2 is a sequel to my previous DS music editor, Nitro Studio. I felt that I could add a lot more features and make the program easier and intuitive to use by starting from scratch, and I'd say I succeeded. This editor has a lot more features than the old one.

## New Features
Some new features include:
	
	* Sequence editor.
	* Sequence archive editor.
	* Sequence archive sequence playback.
	* Exporting sequences as WAV.
	* Custom MIDI importer/exporter, with the option to use the old one, or even use Nintendo's SDK importer provided you have it.
	* SF2/DLS exporting/importing.
	* SF2 <-> DLS conversion.
	* More accurate saving.
	* More accurate SDK exporting.
	* New and improved bank editor and bank generator.
	* Ability to create SWAVs and STRMs from the tools menu and decide the target encoding.
	* Batch exporting MIDI, SF2, and DLS.
	* Importing and exporting instruments includes sample data.
	* Ability to open any file standalone with Nitro Studio 2.
	* Better group editing.
	* Better usability and feel overall.
	* Trackbar to change position.
	* And more!

## Downloads
Coming soon! :}
*Cough- Betas are on my discord. -Cough*

Beta: https://cdn.discordapp.com/attachments/440493269110095876/746771176701034506/Nitro_Studio_2.rar

Known Bugs:
   * Not all buttons work.
   * Audio conversion of any kind is faulty.
   * Nitro Studio 2 sequence converters are incomplete.

## Guide
Don't know where to start? Need help figuring out what something does or how to use a particular tool? Then please read the guides below to answer your own questions.

[General Editor Controls](guide/generalEditorControls.md) - Goes over the general layout and controls of Nitro Studio 2.

[Sound Archive General Structure](guide/soundArchive.md) - Explains how the sound archive works as a whole, and the individual parameters for each item.

[File Types](guide/fileTypes.md) - Goes over the different files that Nitro Studio 2 interacts with.

[Sequence Commands](guide/sequenceCommands.md) - A list of Sequence commands and their MIDI controllers.

[Sequence Editor](guide/sequenceEditor.md) - Describes how to use the Sequence Editor.

[Sequence Archive Editor](guide/sequenceArchiveEditor.md) - Describes how to use the Sequence Archive Editor.

[Bank Editor](guide/bankEditor.md) - Describes how to use the Bank Editor.

[Wave Archive Editor](guide/waveArchiveEditor.md) - Describes how to use the Wave Archive Editor.

[Bank Generator](guide/bankGenerator.md) - Tells you how to create a new Bank from instruments in other Banks.

[Wave Creator](guide/waveCreator.md) - Shows how to create a Wave.

[Stream Creator](guide/streamCreator.md) - Shows how to create a Stream.

[DLS/SF2 Importer](guide/sf2DlsImporter.md) - Looks at importing an SF2/DLS.

[Putting It All Together: Adding A Song From The Ground Up](guide/finalTest.md) - Goes through the process of adding a new song.

## Discord
Need human support as the guides were not sufficient? Found a bug? Want a feature added? Want to get updates or early access to programs I'm working on?
[Then join my discord!](https://discord.gg/6VDPGne)

## File Specifications
In case you are a programmer and wish to develop your own tools related to the SDAT, wish to edit parts of files manually, or are just curious about the structure of the formats in general, you can look at the specifications of the different files used within the SDAT below.

[Common Structures](specs/common.md) - Common structures found throughout the sound archive and its sub-files.

[Sound Data (SDAT)](specs/soundData.md) - Sound data that contains everything.

[Sequences (SSEQ)](specs/sequence.md) - Sequences used to play music.

[Sequence Archives (SSAR)](specs/sequenceArchive.md) - Sequence archives contain a bunch of sequences used to play SFX.

[Banks (SBNK)](specs/bank.md) - Banks that contain instruments to use with sequences.

[Wave Archives (SWAR)](specs/waveArchive.md) - Wave archives used to store wave files.

[Waves (SWAV)](specs/wave.md) - Waves that are used for instrument and SFX samples.

[Streams (STRM)](specs/stream.md) - Streams to play music and SFX.

## Credits
I couldn't have done this alone of course!

	* Nintendo, Images, SDAT Info.
	* Kermalis, Sequence Player Base
	* Eugene, Testing, Suggestions, Guide.
	* Goji Goodra, Testing, Suggestions.
	* Josh, SDAT Research.
	* Crystal, SDAT Research.
	* Nintendon, SDAT Research.
	* DJ Bouche, SDAT Research.
	* VGMTrans, SDAT Research.
	* LoveEmu, SDAT Research, Tools.
	* Gota7, Nitro Studio.

©2020 Gota7
