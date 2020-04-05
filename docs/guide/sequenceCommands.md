# Sequence Commands
When editing Sequences or Sequence Archives you are bound to run into Sequence commands. Sequence commands can also be specified via MIDI controls when the MIDI is converted to a Sequence.

## Notes
Notes are specified by note name, type, and number. For example, `cn4` is C Note 4, and `gs5` is G Sharp 5. Instead of saying B Flat, you would have to say A Sharp. The higher the number, the higher the octave. Minus 1 notes also exist, which look like `cnm1` or C Note Minus 1. You can specify a note from `cnm1` to `gn9`. Middle C is `cn4`. Here is a table that lists all the possible notes and gives their MIDI number.

| **Note** | **Number** |
|----------|------------|
|cnm1|0|
|csm1|1|
|dnm1|2|
|dsm1|3|
|enm1|4|
|fnm1|5|
|fsm1|6|
|gnm1|7|
|gsm1|8|
|anm1|9|
|asm1|10|
|bnm1|11|
|cn0|12|
|cs0|13|
|dn0|14|
|ds0|15|
|en0|16|
|fn0|17|
|fs0|18|
|gn0|19|
|gs0|20|
|an0|21|
|as0|22|
|bn0|23|
|cn1|24|
|cs1|25|
|dn1|26|
|ds1|27|
|en1|28|
|fn1|29|
|fs1|30|
|gn1|31|
|gs1|32|
|an1|33|
|as1|34|
|bn1|35|
|cn2|36|
|cs2|37|
|dn2|38|
|ds2|39|
|en2|40|
|fn2|41|
|fs2|42|
|gn2|43|
|gs2|44|
|an2|45|
|as2|46|
|bn2|47|
|cn3|48|
|cs3|49|
|dn3|50|
|ds3|51|
|en3|52|
|fn3|53|
|fs3|54|
|gn3|55|
|gs3|56|
|an3|57|
|as3|58|
|bn3|59|
|cn4|60|
|cs4|61|
|dn4|62|
|ds4|63|
|en4|64|
|fn4|65|
|fs4|66|
|gn4|67|
|gs4|68|
|an4|69|
|as4|70|
|bn4|71|
|cn5|72|
|cs5|73|
|dn5|74|
|ds5|75|
|en5|76|
|fn5|77|
|fs5|78|
|gn5|79|
|gs5|80|
|an5|81|
|as5|82|
|bn5|83|
|cn6|84|
|cs6|85|
|dn6|86|
|ds6|87|
|en6|88|
|fn6|89|
|fs6|90|
|gn6|91|
|gs6|92|
|an6|93|
|as6|94|
|bn6|95|
|cn7|96|
|cs7|97|
|dn7|98|
|ds7|99|
|en7|100|
|fn7|101|
|fs7|102|
|gn7|103|
|gs7|104|
|an7|105|
|as7|106|
|bn7|107|
|cn8|108|
|cs8|109|
|dn8|110|
|ds8|111|
|en8|112|
|fn8|113|
|fs8|114|
|gn8|115|
|gs8|116|
|an8|117|
|as8|118|
|bn8|119|
|cn9|120|
|cs9|121|
|dn9|122|
|ds9|123|
|en9|124|
|fn9|125|
|fs9|126|
|gn9|127|

## Labels
A label is used as a marker. It has its own line, and followed by a colon. For example, `Label_0:` would be a label named `Label_0`.

### Private Labels
Labels that start with `_` are private labels and can only be accessed by all commands between the last nonprivate label and the next nonprivate label. However, due to how the Sequence library is programmed, this rule is ignored.

## Standard Commands
A command is used to do something. A command can take up to 2 parameters. The equation to converting the volume to a percentage is (Volume / 127) ^ 2 * (Volume2 / 127) ^ 2 * 100.

| **Command Name** | **Parameter 1** | **Parameter 2** | **MIDI Control** | **Example** | **Description** |
|------------------|-----------------|-----------------|------------------|-------------|-----------------|
|Note|Velocity (0 - 127)|Duration In Ticks (0 - 268435455)|Note On|cn4 127, 48|Plays Middle C with velocity 127 for 48 ticks|
|Wait|Duration In Ticks (0 - 268435455)|N/A|N/A|wait 48|Waits for 48 ticks before continuing|
|Open Track|Track Number (0 - 15)|Label To Start The Track At|N/A|opentrack 3, SeqTrack3|Starts track 3 at the label `SeqTrack3`|
|Jump|Label To Jump To|N/A|Using Nitro Studio 2's converter, a marker with `jump` then followed by a space then the label name|jump SeqMiddle|Jumps to the label `SeqMiddle`. Jumps are usually used for looping|
|Call|Label To Call|N/A|N/A|call SeqPart3|Jumps to the label `SeqPart3` then returns once a Return command is found|
|Pan|Value (0 - 127) Where 64 Is Center, 127 Is All Right, 0 Is All Left|N/A|10|pan 75|Sets the track's pan slightly to the right. Default is 64|
|Volume|Value (0 - 127)|N/A|7|volume 100|Sets the track's volume to 100|
|Main Volume|Value (0 - 127)|N/A|12|main_volume 100|Set's the player's volume to 100. Default is 127|
|Transpose|Value (-64 - 63)|N/A|13|transpose 2|Makes every note 2 semitones higher. Default is 0|
|Pitch Bend|Value (-128 - 127)|N/A|Pitch Wheel|pitchbend 100|Adjusts the pitch to be (100 / 127) ^ 2 * 100 times the Bend Range semitones. Default is 0|
|Bend Range|Value (0 - 127)|N/A|20|bendrange 12|Sets the bend range to be 12 semitones. The default is two. In this example, if a pitchbend of 127 was used, the pich would go up by 12 semitones|
|Priority|Value (0 - 127)|N/A|14|prio 70|Sets the track's priority to 70. Default is 64|
|Note Wait|_on or _off|N/A|N/A|notewait_off|Does not wait for a note to finish before continuing. Default is on|
|Tie|on or off|N/A|N/A|tieon|Notes don't end and new notes just change the pitch and velocity of the playing note. Default is off|
|Portamento|Value (0 - 127)|N/A|84|porta 100|Sets the track's portamento to 100|
|MOD STUFF!!!|
