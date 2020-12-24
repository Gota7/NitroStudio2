using GotaSoundBank;
using GotaSequenceLib;
using GotaSequenceLib.Playback;
using GotaSoundIO.IO;
using GotaSoundIO.Sound;
using Kermalis.SoundFont2;
using Microsoft.VisualBasic;
using NitroFileLoader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GotaSoundBank.DLS;
using GotaSoundBank.SF2;

namespace NitroStudio2 {

    /// <summary>
    /// Main window.
    /// </summary>
    public class MainWindow : EditorBase {

        /// <summary>
        /// Nitro path.
        /// </summary>
        public static string NitroPath = Application.StartupPath;

        /// <summary>
        /// The sound archive.
        /// </summary>
        public SoundArchive SA => File as SoundArchive;

        /// <summary>
        /// Stream temp count.
        /// </summary>
        public int StreamTempCount = 0;

        /// <summary>
        /// Mixer.
        /// </summary>
        public Mixer Mixer = new Mixer();

        /// <summary>
        /// Player.
        /// </summary>
        public Player Player;
        
        /// <summary>
        /// Timer.
        /// </summary>
        public Timer Timer = new Timer();

        /// <summary>
        /// Position bar free.
        /// </summary>
        public bool PositionBarFree = true;

        /// <summary>
        /// Create a new main window.
        /// </summary>
        public MainWindow() : base(typeof(SoundArchive), "Sound Archive", "dat", "Nitro Studio 2", null) {
            Init();
            Text = "Nitro Studio 2";
        }

        /// <summary>
        /// Create a new main window.
        /// </summary>
        /// <param name="fileToOpen">The file to open.</param>
        public MainWindow(string fileToOpen) : base(typeof(SoundArchive), "Sound Archive", "dat", "Nitro Studio 2", fileToOpen, null) {
            Init();
        }

        /// <summary>
        /// Initialize.
        /// </summary>
        public void Init() {

            //Window stuff.
            Icon = Properties.Resources.Icon;
            FormClosing += new FormClosingEventHandler(SAClosing);
            toolsToolStripMenuItem.Visible = true;
            
            //Settings.
            writeNamesBox.CheckedChanged += new EventHandler(WriteNamesChanged);
            seqImportModeBox.SelectedIndex = 0;
            seqExportModeBox.SelectedIndex = 0;

            //Index panel.
            swapAtIndexButton.Click += new EventHandler(SwapAtIndexButtonPressed);

            //Unique Id panel.
            forceUniqueFileBox.CheckedChanged += new EventHandler(ForceUniqueIdChanged);

            //Sequence stuff.
            seqVolumeBox.ValueChanged += new EventHandler(SequenceVolumeChanged);
            seqChannelPriorityBox.ValueChanged += new EventHandler(SequenceChannelPriorityChanged);
            seqPlayerPriorityBox.ValueChanged += new EventHandler(SequencePlayerPriorityChanged);
            seqBankBox.ValueChanged += new EventHandler(SequenceBankBoxChanged);
            seqBankComboBox.SelectedIndexChanged += new EventHandler(SequenceBankComboBoxChanged);
            seqPlayerBox.ValueChanged += new EventHandler(SequencePlayerBoxChanged);
            seqPlayerComboBox.SelectedIndexChanged += new EventHandler(SequencePlayerComboBoxChanged);

            //Sequence archive stuff.
            seqArcOpenFileButton.Click += new EventHandler(OpenSeqArcFile);

            //Bank stuff.
            bnkWar0Box.ValueChanged += new EventHandler(BnkWar0BoxChanged);
            bnkWar1Box.ValueChanged += new EventHandler(BnkWar1BoxChanged);
            bnkWar2Box.ValueChanged += new EventHandler(BnkWar2BoxChanged);
            bnkWar3Box.ValueChanged += new EventHandler(BnkWar3BoxChanged);
            bnkWar0ComboBox.SelectedValueChanged += new EventHandler(BnkWar0ComboBoxChanged);
            bnkWar1ComboBox.SelectedValueChanged += new EventHandler(BnkWar1ComboBoxChanged);
            bnkWar2ComboBox.SelectedValueChanged += new EventHandler(BnkWar2ComboBoxChanged);
            bnkWar3ComboBox.SelectedValueChanged += new EventHandler(BnkWar3ComboBoxChanged);

            //Wave archive stuff.
            loadIndividuallyBox.CheckedChanged += new EventHandler(WarLoadIndividualChanged);

            //Player stuff.
            playerMaxSequencesBox.ValueChanged += new EventHandler(PlayerSequenceMaxChanged);
            playerHeapSizeBox.ValueChanged += new EventHandler(PlayerHeapSizeChanged);
            playerFlag0Box.CheckedChanged += new EventHandler(PlayerFlagsChanged);
            playerFlag1Box.CheckedChanged += new EventHandler(PlayerFlagsChanged);
            playerFlag2Box.CheckedChanged += new EventHandler(PlayerFlagsChanged);
            playerFlag3Box.CheckedChanged += new EventHandler(PlayerFlagsChanged);
            playerFlag4Box.CheckedChanged += new EventHandler(PlayerFlagsChanged);
            playerFlag5Box.CheckedChanged += new EventHandler(PlayerFlagsChanged);
            playerFlag6Box.CheckedChanged += new EventHandler(PlayerFlagsChanged);
            playerFlag7Box.CheckedChanged += new EventHandler(PlayerFlagsChanged);
            playerFlag8Box.CheckedChanged += new EventHandler(PlayerFlagsChanged);
            playerFlag9Box.CheckedChanged += new EventHandler(PlayerFlagsChanged);
            playerFlag10Box.CheckedChanged += new EventHandler(PlayerFlagsChanged);
            playerFlag11Box.CheckedChanged += new EventHandler(PlayerFlagsChanged);
            playerFlag12Box.CheckedChanged += new EventHandler(PlayerFlagsChanged);
            playerFlag13Box.CheckedChanged += new EventHandler(PlayerFlagsChanged);
            playerFlag14Box.CheckedChanged += new EventHandler(PlayerFlagsChanged);
            playerFlag15Box.CheckedChanged += new EventHandler(PlayerFlagsChanged);

            //Group stuff.
            grpEntries.CellValueChanged += new DataGridViewCellEventHandler(GroupEntriesChanged);
            grpEntries.RowsRemoved += new DataGridViewRowsRemovedEventHandler(GroupEntriesChanged);

            //Stream player stuff.
            stmPlayerChannelType.SelectedIndexChanged += new EventHandler(StreamPlayerTypeChanged);
            stmPlayerLeftChannelBox.ValueChanged += new EventHandler(StreamPlayerLeftChannelChanged);
            stmPlayerRightChannelBox.ValueChanged += new EventHandler(StreamPlayerRightChannelChanged);

            //Stream stuff.
            stmVolumeBox.ValueChanged += new EventHandler(StreamVolumeChanged);
            stmPriorityBox.ValueChanged += new EventHandler(StreamPriorityChanged);
            stmPlayerBox.ValueChanged += new EventHandler(StreamPlayerBoxChanged);
            stmPlayerComboBox.SelectedIndexChanged += new EventHandler(StreamPlayerComboBoxChanged);
            stmMonoToStereoBox.CheckedChanged += new EventHandler(StreamMonoToStereoChanged);

            //Player.
            Player = new Player(Mixer);
            kermalisPlayButton.Click += new EventHandler(PlayClick);
            kermalisPauseButton.Click += new EventHandler(PauseClick);
            kermalisStopButton.Click += new EventHandler(StopClick);
            kermalisVolumeSlider.ValueChanged += new EventHandler(VolumeChanged);
            kermalisLoopBox.CheckedChanged += new EventHandler(LoopChanged);
            kermalisPosition.MouseUp += new MouseEventHandler(PositionMouseUp);
            kermalisPosition.MouseDown += new MouseEventHandler(PositionMouseDown);
            tree.KeyPress += new KeyPressEventHandler(KeyPress);
            kermalisVolumeSlider.Value = 75;
            Mixer.Volume = .75f;
            Timer.Tick += PositionTick;
            Timer.Interval = 1000 / 30;
            Timer.Start();

        }

        /// <summary>
        /// Update nodes.
        /// </summary>
        public override void UpdateNodes() {

            //Begin update.
            BeginUpdateNodes();

            //Add waves if node doesn't exist.
            if (tree.Nodes.Count < 9) {
                tree.Nodes.RemoveAt(0);
                tree.Nodes.Add("settings", "Settings", 1, 1);
                tree.Nodes.Add("sequences", "Sound Sequences", 2, 2);
                tree.Nodes.Add("sequenceArchives", "Sequence Archives", 3, 3);
                tree.Nodes.Add("banks", "Instrument Banks", 4, 4);
                tree.Nodes.Add("waveArchives", "Wave Archives", 5, 5);
                tree.Nodes.Add("players", "Sequence Players", 6, 6);
                tree.Nodes.Add("groups", "Groups", 7, 7);
                tree.Nodes.Add("streamPlayers", "Stream Players", 8, 8);
                tree.Nodes.Add("streams", "Sound Streams", 9, 9);
            }

            //File open and not null.
            if (FileOpen && File != null) {

                //Root menus.
                for (int i = 1; i < 9; i++) {
                    tree.Nodes[i].ContextMenuStrip = rootMenu;
                }

                //Load data.
                foreach (var e in SA.Sequences) {
                    tree.Nodes["sequences"].Nodes.Add("entry" + e.Index, "[" + e.Index + "] " + e.Name, 2, 2);
                    tree.Nodes["sequences"].Nodes["entry" + e.Index].ContextMenuStrip = CreateMenuStrip(sarEntryMenu, new int[] { 0, 1, 4, 5, 6, 7 }, new EventHandler[] { new EventHandler(AddAbove), new EventHandler(AddBelow), new EventHandler(Replace), new EventHandler(Export), new EventHandler(Rename), new EventHandler(Delete) });
                }
                foreach (var e in SA.SequenceArchives) {
                    tree.Nodes["sequenceArchives"].Nodes.Add("entry" + e.Index, "[" + e.Index + "] " + e.Name, 3, 3);
                    tree.Nodes["sequenceArchives"].Nodes["entry" + e.Index].ContextMenuStrip = CreateMenuStrip(sarEntryMenu, new int[] { 0, 1, 4, 5, 6, 7 }, new EventHandler[] { new EventHandler(AddAbove), new EventHandler(AddBelow), new EventHandler(Replace), new EventHandler(Export), new EventHandler(Rename), new EventHandler(Delete) });
                    foreach (var s in e.File.Sequences) {
                        tree.Nodes["sequenceArchives"].Nodes["entry" + e.Index].Nodes.Add("entry" + s.Index, "[" + s.Index + "] " + s.Name, 2, 2);
                        tree.Nodes["sequenceArchives"].Nodes["entry" + e.Index].Nodes["entry" + s.Index].ContextMenuStrip = CreateMenuStrip(sarEntryMenu, new int[] { 5, 6 }, new EventHandler[] { new EventHandler(Export), new EventHandler(Rename) });
                    }
                }
                foreach (var e in SA.Banks) {
                    tree.Nodes["banks"].Nodes.Add("entry" + e.Index, "[" + e.Index + "] " + e.Name, 4, 4);
                    tree.Nodes["banks"].Nodes["entry" + e.Index].ContextMenuStrip = CreateMenuStrip(sarEntryMenu, new int[] { 0, 1, 4, 5, 6, 7 }, new EventHandler[] { new EventHandler(AddAbove), new EventHandler(AddBelow), new EventHandler(Replace), new EventHandler(Export), new EventHandler(Rename), new EventHandler(Delete) });
                }
                foreach (var e in SA.WaveArchives) {
                    tree.Nodes["waveArchives"].Nodes.Add("entry" + e.Index, "[" + e.Index + "] " + e.Name, 5, 5);
                    tree.Nodes["waveArchives"].Nodes["entry" + e.Index].ContextMenuStrip = CreateMenuStrip(sarEntryMenu, new int[] { 0, 1, 4, 5, 6, 7 }, new EventHandler[] { new EventHandler(AddAbove), new EventHandler(AddBelow), new EventHandler(Replace), new EventHandler(Export), new EventHandler(Rename), new EventHandler(Delete) });
                }
                foreach (var e in SA.Players) {
                    tree.Nodes["players"].Nodes.Add("entry" + e.Index, "[" + e.Index + "] " + e.Name, 6, 6);
                    tree.Nodes["players"].Nodes["entry" + e.Index].ContextMenuStrip = CreateMenuStrip(sarEntryMenu, new int[] { 0, 1, 6, 7 }, new EventHandler[] { new EventHandler(AddAbove), new EventHandler(AddBelow), new EventHandler(Rename), new EventHandler(Delete) });
                }
                foreach (var e in SA.Groups) {
                    tree.Nodes["groups"].Nodes.Add("entry" + e.Index, "[" + e.Index + "] " + e.Name, 7, 7);
                    tree.Nodes["groups"].Nodes["entry" + e.Index].ContextMenuStrip = CreateMenuStrip(sarEntryMenu, new int[] { 0, 1, 6, 7 }, new EventHandler[] { new EventHandler(AddAbove), new EventHandler(AddBelow), new EventHandler(Rename), new EventHandler(Delete) });
                }
                foreach (var e in SA.StreamPlayers) {
                    tree.Nodes["streamPlayers"].Nodes.Add("entry" + e.Index, "[" + e.Index + "] " + e.Name, 8, 8);
                    tree.Nodes["streamPlayers"].Nodes["entry" + e.Index].ContextMenuStrip = CreateMenuStrip(sarEntryMenu, new int[] { 0, 1, 6, 7 }, new EventHandler[] { new EventHandler(AddAbove), new EventHandler(AddBelow), new EventHandler(Rename), new EventHandler(Delete) });
                }
                foreach (var e in SA.Streams) {
                    tree.Nodes["streams"].Nodes.Add("entry" + e.Index, "[" + e.Index + "] " + e.Name, 9, 9);
                    tree.Nodes["streams"].Nodes["entry" + e.Index].ContextMenuStrip = CreateMenuStrip(sarEntryMenu, new int[] { 0, 1, 4, 5, 6, 7 }, new EventHandler[] { new EventHandler(AddAbove), new EventHandler(AddBelow), new EventHandler(Replace), new EventHandler(Export), new EventHandler(Rename), new EventHandler(Delete) });
                }

            } else {

                //Remove context menus.
                foreach (TreeNode n in tree.Nodes) {
                    n.ContextMenuStrip = null;
                }

            }

            //End update notes.
            EndUpdateNodes();

        }

        /// <summary>
        /// Do info stuff.
        /// </summary>
        public override void DoInfoStuff() {

            //The base.
            base.DoInfoStuff();
            WritingInfo = true;

            //Hide stuff.
            void HideStuff() {
                kermalisSoundPlayerPanel.Hide();
                indexPanel.Hide();
                forceUniqueFilePanel.Hide();
                kermalisSoundPlayerPanel.SendToBack();
                indexPanel.SendToBack();
                forceUniqueFilePanel.SendToBack();
            }

            //If file open.
            if (!FileOpen || File == null) {
                HideStuff();
                if (Player != null) { StopClick(this, null); }
                return;
            }

            //Panel selected.
            bool panelSelected = false;

            //Parent is null.
            if (tree.SelectedNode.Parent == null) {

                //If settings.
                if (tree.SelectedNode == tree.Nodes["settings"]) {
                    HideStuff();
                    settingsPanel.BringToFront();
                    settingsPanel.Show();
                    writeNamesBox.Checked = SA.SaveSymbols;
                    status.Text = "Editing Settings.";
                    panelSelected = true;
                }

            }

            //Child.
            else {

                //Panel selected.
                panelSelected = true;

                //Not double entry.
                if (tree.SelectedNode.Parent.Parent == null) {

                    //Sequence.
                    if (tree.SelectedNode.Parent.Name == "sequences") {
                        seqPanel.BringToFront();
                        indexPanel.Show();
                        forceUniqueFilePanel.Show();
                        kermalisSoundPlayerPanel.Show();
                        seqPanel.Show();
                        var e = SA.Sequences.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                        itemIndexBox.Value = e.Index;
                        forceUniqueFileBox.Checked = e.ForceIndividualFile;
                        PopulateBankBox(SA, seqBankComboBox);
                        SetBankIndex(SA, seqBankComboBox, e.Bank == null ? e.ReadingBankId : (ushort)e.Bank.Index);
                        seqBankBox.Value = e.Bank == null ? e.ReadingBankId : (ushort)e.Bank.Index;
                        seqVolumeBox.Value = e.Volume > 127 ? 127 : e.Volume;
                        seqChannelPriorityBox.Value = e.ChannelPriority;
                        seqPlayerPriorityBox.Value = e.PlayerPriority;
                        PopulatePlayerBox(SA, seqPlayerComboBox);
                        SetPlayerIndex(SA, seqPlayerComboBox, e.Player == null ? e.ReadingPlayerId : (byte)e.Player.Index);
                        seqPlayerBox.Value = e.Player == null ? e.ReadingPlayerId : (byte)e.Player.Index;
                        status.Text = "[" + e.Index + "] " + e.Name + " Selected. File Is " + GetBytesSize(e.File) + ".";
                    }

                    //Sequence archive.
                    else if (tree.SelectedNode.Parent.Name == "sequenceArchives") {
                        kermalisSoundPlayerPanel.Hide();
                        seqArcPanel.BringToFront();
                        indexPanel.Show();
                        forceUniqueFilePanel.Show();
                        seqArcPanel.Show();
                        var e = SA.SequenceArchives.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                        itemIndexBox.Value = e.Index;
                        forceUniqueFileBox.Checked = e.ForceIndividualFile;
                        status.Text = "[" + e.Index + "] " + e.Name + " Selected. File Is " + GetBytesSize(e.File) + ".";
                    }

                    //Bank.
                    else if (tree.SelectedNode.Parent.Name == "banks") {
                        kermalisSoundPlayerPanel.Hide();
                        bankPanel.BringToFront();
                        indexPanel.Show();
                        forceUniqueFilePanel.Show();
                        bankPanel.Show();
                        var e = SA.Banks.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                        itemIndexBox.Value = e.Index;
                        forceUniqueFileBox.Checked = e.ForceIndividualFile;
                        PopulateWaveArchiveBox(SA, bnkWar0ComboBox);
                        PopulateWaveArchiveBox(SA, bnkWar1ComboBox);
                        PopulateWaveArchiveBox(SA, bnkWar2ComboBox);
                        PopulateWaveArchiveBox(SA, bnkWar3ComboBox);
                        SetWaveArchiveIndex(SA, bnkWar0ComboBox, e.WaveArchives[0] == null ? e.ReadingWave0Id : (ushort)e.WaveArchives[0].Index);
                        SetWaveArchiveIndex(SA, bnkWar1ComboBox, e.WaveArchives[1] == null ? e.ReadingWave1Id : (ushort)e.WaveArchives[1].Index);
                        SetWaveArchiveIndex(SA, bnkWar2ComboBox, e.WaveArchives[2] == null ? e.ReadingWave2Id : (ushort)e.WaveArchives[2].Index);
                        SetWaveArchiveIndex(SA, bnkWar3ComboBox, e.WaveArchives[3] == null ? e.ReadingWave3Id : (ushort)e.WaveArchives[3].Index);
                        SetWaveArchiveIndex(SA, bnkWar0Box, e.WaveArchives[0] == null ? e.ReadingWave0Id : (ushort)e.WaveArchives[0].Index);
                        SetWaveArchiveIndex(SA, bnkWar1Box, e.WaveArchives[1] == null ? e.ReadingWave1Id : (ushort)e.WaveArchives[1].Index);
                        SetWaveArchiveIndex(SA, bnkWar2Box, e.WaveArchives[2] == null ? e.ReadingWave2Id : (ushort)e.WaveArchives[2].Index);
                        SetWaveArchiveIndex(SA, bnkWar3Box, e.WaveArchives[3] == null ? e.ReadingWave3Id : (ushort)e.WaveArchives[3].Index);
                        status.Text = "[" + e.Index + "] " + e.Name + " Selected. File Is " + GetBytesSize(e.File) + ".";
                    }

                    //Wave archive.
                    else if (tree.SelectedNode.Parent.Name == "waveArchives") {
                        kermalisSoundPlayerPanel.Hide();
                        warPanel.BringToFront();
                        indexPanel.Show();
                        forceUniqueFilePanel.Show();
                        warPanel.Show();
                        var e = SA.WaveArchives.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                        itemIndexBox.Value = e.Index;
                        forceUniqueFileBox.Checked = e.ForceIndividualFile;
                        loadIndividuallyBox.Checked = e.LoadIndividually;
                        status.Text = "[" + e.Index + "] " + e.Name + " Selected. File Is " + GetBytesSize(e.File) + ".";
                    }

                    //Player.
                    else if (tree.SelectedNode.Parent.Name == "players") {
                        kermalisSoundPlayerPanel.Hide();
                        playerPanel.BringToFront();
                        indexPanel.Show();
                        playerPanel.Show();
                        var e = SA.Players.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                        itemIndexBox.Value = e.Index;
                        playerMaxSequencesBox.Value = e.SequenceMax;
                        playerHeapSizeBox.Value = e.HeapSize;
                        playerFlag0Box.Checked = e.ChannelFlags[0];
                        playerFlag1Box.Checked = e.ChannelFlags[1];
                        playerFlag2Box.Checked = e.ChannelFlags[2];
                        playerFlag3Box.Checked = e.ChannelFlags[3];
                        playerFlag4Box.Checked = e.ChannelFlags[4];
                        playerFlag5Box.Checked = e.ChannelFlags[5];
                        playerFlag6Box.Checked = e.ChannelFlags[6];
                        playerFlag7Box.Checked = e.ChannelFlags[7];
                        playerFlag8Box.Checked = e.ChannelFlags[8];
                        playerFlag9Box.Checked = e.ChannelFlags[9];
                        playerFlag10Box.Checked = e.ChannelFlags[10];
                        playerFlag11Box.Checked = e.ChannelFlags[11];
                        playerFlag12Box.Checked = e.ChannelFlags[12];
                        playerFlag13Box.Checked = e.ChannelFlags[13];
                        playerFlag14Box.Checked = e.ChannelFlags[14];
                        playerFlag15Box.Checked = e.ChannelFlags[15];
                        status.Text = "[" + e.Index + "] " + e.Name + " Selected.";
                    }

                    //Group.
                    else if (tree.SelectedNode.Parent.Name == "groups") {
                        kermalisSoundPlayerPanel.Hide();
                        grpPanel.BringToFront();
                        indexPanel.Show();
                        grpPanel.Show();
                        var e = SA.Groups.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                        itemIndexBox.Value = e.Index;
                        PopulateGroupGrid(grpEntries, e);
                        status.Text = "[" + e.Index + "] " + e.Name + " Selected.";
                    }

                    //Stream player.
                    else if (tree.SelectedNode.Parent.Name == "streamPlayers") {
                        kermalisSoundPlayerPanel.Hide();
                        streamPlayerPanel.BringToFront();
                        indexPanel.Show();
                        streamPlayerPanel.Show();
                        var e = SA.StreamPlayers.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                        itemIndexBox.Value = e.Index;
                        stmPlayerChannelType.SelectedIndex = e.IsStereo ? 1 : 0;
                        stmPlayerLeftChannelBox.Value = e.LeftChannel;
                        if (e.IsStereo) {
                            leftChannelLabel.Text = "Channel:";
                            rightChannelLabel.Text = "Right Channel:";
                            stmPlayerRightChannelBox.Value = e.RightChannel;
                            rightChannelLabel.Enabled = true;
                            stmPlayerRightChannelBox.Enabled = true;
                        } else {
                            leftChannelLabel.Text = "Left Channel:";
                            rightChannelLabel.Text = "(Doesn't Exist)";
                            stmPlayerRightChannelBox.Value = 0;
                            rightChannelLabel.Enabled = false;
                            stmPlayerRightChannelBox.Enabled = false;
                        }
                        status.Text = "[" + e.Index + "] " + e.Name + " Selected.";
                    }

                    //Stream.
                    else if (tree.SelectedNode.Parent.Name == "streams") {
                        kermalisSoundPlayerPanel.Hide();
                        stmPanel.BringToFront();
                        indexPanel.Show();
                        forceUniqueFilePanel.Show();
                        stmPanel.Show();
                        var e = SA.Streams.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                        itemIndexBox.Value = e.Index;
                        forceUniqueFileBox.Checked = e.ForceIndividualFile;
                        stmMonoToStereoBox.Checked = e.MonoToStereo;
                        stmVolumeBox.Value = e.Volume;
                        stmPriorityBox.Value = e.Priority;
                        PopulateStreamPlayerBox(SA, stmPlayerComboBox);
                        SetStreamPlayerIndex(SA, stmPlayerComboBox, e.Player == null ? e.ReadingPlayerId : (byte)e.Player.Index);
                        stmPlayerBox.Value = e.Player == null ? e.ReadingPlayerId : e.Player.Index;
                        status.Text = "[" + e.Index + "] " + e.Name + " Selected. File Is " + GetBytesSize(e.File) + ".";
                    }

                }

                //Sequence archive sequence.
                else {
                    indexPanel.Hide();
                    forceUniqueFilePanel.Hide();
                    indexPanel.SendToBack();
                    forceUniqueFilePanel.SendToBack();
                    blankPanel.BringToFront();
                    kermalisSoundPlayerPanel.Show();
                    blankPanel.Show();
                    var e = SA.SequenceArchives.Where(x => x.Index == GetIdFromNode(tree.SelectedNode.Parent)).FirstOrDefault().File.Sequences.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                    status.Text = "[" + e.Index + "] " + e.Name + " Selected.";
                }
            
            }

            //No panel selected.
            if (!panelSelected) {
                HideStuff();
                noInfoPanel.BringToFront();
                noInfoPanel.Show();
                status.Text = "No Valid Info Selected!";
            }

            //Done.
            WritingInfo = false;

        }

        /// <summary>
        /// Double click a node.
        /// </summary>
        public override void NodeMouseDoubleClick() {

            //Do base.
            base.NodeMouseDoubleClick();

            //Open file.
            if (tree.SelectedNode.Parent != null) {

                //Sequence.
                if (tree.SelectedNode.Parent == tree.Nodes["sequences"]) {
                    var e = SA.Sequences.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                    SequenceEditor ed = new SequenceEditor(e.File, this, e.Name);
                    SetBankIndex(SA, ed.seqEditorBankComboBox, e.Bank == null ? e.ReadingBankId : (uint)e.Bank.Index);
                    ed.seqEditorBankBox.Value = e.Bank == null ? e.ReadingBankId : (uint)e.Bank.Index;
                    ed.Show();
                }

                //Sequence archive.
                else if (tree.SelectedNode.Parent == tree.Nodes["sequenceArchives"]) {
                    var e = SA.SequenceArchives.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                    SequenceArchiveEditor ed = new SequenceArchiveEditor(e.File, this, e.Name);
                    ed.Show();
                }

                //Bank.
                else if (tree.SelectedNode.Parent == tree.Nodes["banks"]) {
                    var e = SA.Banks.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                    BankEditor ed = new BankEditor(e.File, this, e.Name);
                    SetWaveArchiveIndex(SA, ed.war0ComboBox, e.WaveArchives[0] == null ? e.ReadingWave0Id : (ushort)e.WaveArchives[0].Index);
                    SetWaveArchiveIndex(SA, ed.war1ComboBox, e.WaveArchives[1] == null ? e.ReadingWave1Id : (ushort)e.WaveArchives[1].Index);
                    SetWaveArchiveIndex(SA, ed.war2ComboBox, e.WaveArchives[2] == null ? e.ReadingWave2Id : (ushort)e.WaveArchives[2].Index);
                    SetWaveArchiveIndex(SA, ed.war3ComboBox, e.WaveArchives[3] == null ? e.ReadingWave3Id : (ushort)e.WaveArchives[3].Index);
                    SetWaveArchiveIndex(SA, ed.war0Box, e.WaveArchives[0] == null ? e.ReadingWave0Id : (ushort)e.WaveArchives[0].Index);
                    SetWaveArchiveIndex(SA, ed.war1Box, e.WaveArchives[1] == null ? e.ReadingWave1Id : (ushort)e.WaveArchives[1].Index);
                    SetWaveArchiveIndex(SA, ed.war2Box, e.WaveArchives[2] == null ? e.ReadingWave2Id : (ushort)e.WaveArchives[2].Index);
                    SetWaveArchiveIndex(SA, ed.war3Box, e.WaveArchives[3] == null ? e.ReadingWave3Id : (ushort)e.WaveArchives[3].Index);
                    ed.LoadWaveArchives();
                    ed.Show();
                }

                //Wave archive.
                else if (tree.SelectedNode.Parent == tree.Nodes["waveArchives"]) {
                    var e = SA.WaveArchives.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                    WaveArchiveEditor ed = new WaveArchiveEditor(e.File, this, e.Name);
                    ed.Show();
                }

                //Stream.
                else if (tree.SelectedNode.Parent == tree.Nodes["streams"]) {
                    var s = SA.Streams.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                    RiffWave r = new RiffWave();
                    r.FromOtherStreamFile(s.File);
                    r.Write(MainWindow.NitroPath + "/" + "tmpStream" + StreamTempCount++ + ".wav");
                    StreamPlayer p = new StreamPlayer(this, MainWindow.NitroPath + "/" + "tmpStream" + (StreamTempCount - 1) + ".wav", s.Name);
                    p.Show();
                }

            }

        }

        /// <summary>
        /// Get an Id from a node.
        /// </summary>
        /// <param name="n">The node.</param>
        /// <returns>The Id.</returns>
        public static int GetIdFromNode(TreeNode n) { 
            return int.Parse(n.Text.Split('[')[1].Split(']')[0]);
        }

        /// <summary>
        /// Write names changed.
        /// </summary>
        public void WriteNamesChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                SA.SaveSymbols = writeNamesBox.Checked;
            }
        }

        /// <summary>
        /// Get the amount of bytes for a file.
        /// </summary>
        /// <param name="f">The file.</param>
        /// <returns>The amount of bytes.</returns>
        public static string GetBytesSize(IOFile f) {
            long byteCount = f.Write().Length;
            string[] suf = { "Bytes", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + " " + suf[place];
        }

        /// <summary>
        /// Swap the entry at the index.
        /// </summary>
        public void SwapAtIndexButtonPressed(object sender, EventArgs e) {

            //Get index.
            int index = (int)itemIndexBox.Value;
            int bakIndex = GetIdFromNode(tree.SelectedNode);

            //Get the type.
            switch (tree.SelectedNode.Parent.Name) {

                //Sequences.
                case "sequences":
                    if ((uint)index > SoundArchive.MaxSequenceId) {
                        MessageBox.Show("Index is outside the max possible Id!");
                    }
                    var prevSeq = SA.Sequences.Where(x => x.Index == index).FirstOrDefault();
                    SA.Sequences.Where(x => x.Index == bakIndex).FirstOrDefault().Index = index;
                    if (prevSeq != null) {
                        prevSeq.Index = bakIndex;
                    }
                    SA.Sequences = SA.Sequences.OrderBy(x => x.Index).ToList();
                    UpdateNodes();
                    foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                        if (n.Text.Contains("[" + index + "]")) {
                            tree.SelectedNode = n;
                        }
                    }
                    DoInfoStuff();
                    break;

                //Sequence archives.
                case "sequenceArchives":
                    if ((uint)index > SoundArchive.MaxSequenceArchiveId) {
                        MessageBox.Show("Index is outside the max possible Id!");
                    }
                    var prevSeqArc = SA.SequenceArchives.Where(x => x.Index == index).FirstOrDefault();
                    SA.SequenceArchives.Where(x => x.Index == bakIndex).FirstOrDefault().Index = index;
                    if (prevSeqArc != null) {
                        prevSeqArc.Index = bakIndex;
                    }
                    SA.SequenceArchives = SA.SequenceArchives.OrderBy(x => x.Index).ToList();
                    UpdateNodes();
                    foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                        if (n.Text.Contains("[" + index + "]")) {
                            tree.SelectedNode = n;
                        }
                    }
                    DoInfoStuff();
                    break;

                //Banks.
                case "banks":
                    if ((uint)index > SoundArchive.MaxBankId) {
                        MessageBox.Show("Index is outside the max possible Id!");
                    }
                    var prevBnk = SA.Banks.Where(x => x.Index == index).FirstOrDefault();
                    SA.Banks.Where(x => x.Index == bakIndex).FirstOrDefault().Index = index;
                    if (prevBnk != null) {
                        prevBnk.Index = bakIndex;
                    }
                    SA.Banks = SA.Banks.OrderBy(x => x.Index).ToList();
                    UpdateNodes();
                    foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                        if (n.Text.Contains("[" + index + "]")) {
                            tree.SelectedNode = n;
                        }
                    }
                    DoInfoStuff();
                    break;

                //Wave archives.
                case "waveArchives":
                    if ((uint)index > SoundArchive.MaxWaveArchiveId) {
                        MessageBox.Show("Index is outside the max possible Id!");
                    }
                    var prevWar = SA.WaveArchives.Where(x => x.Index == index).FirstOrDefault();
                    SA.WaveArchives.Where(x => x.Index == bakIndex).FirstOrDefault().Index = index;
                    if (prevWar != null) {
                        prevWar.Index = bakIndex;
                    }
                    SA.WaveArchives = SA.WaveArchives.OrderBy(x => x.Index).ToList();
                    UpdateNodes();
                    foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                        if (n.Text.Contains("[" + index + "]")) {
                            tree.SelectedNode = n;
                        }
                    }
                    DoInfoStuff();
                    break;

                //Players.
                case "players":
                    if ((uint)index > SoundArchive.MaxPlayerId) {
                        MessageBox.Show("Index is outside the max possible Id!");
                    }
                    var prevPly = SA.Players.Where(x => x.Index == index).FirstOrDefault();
                    SA.Players.Where(x => x.Index == bakIndex).FirstOrDefault().Index = index;
                    if (prevPly != null) {
                        prevPly.Index = bakIndex;
                    }
                    SA.Players = SA.Players.OrderBy(x => x.Index).ToList();
                    UpdateNodes();
                    foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                        if (n.Text.Contains("[" + index + "]")) {
                            tree.SelectedNode = n;
                        }
                    }
                    DoInfoStuff();
                    break;

                //Groups.
                case "groups":
                    if ((uint)index > SoundArchive.MaxGroupId) {
                        MessageBox.Show("Index is outside the max possible Id!");
                    }
                    var prevGrp = SA.Groups.Where(x => x.Index == index).FirstOrDefault();
                    SA.Groups.Where(x => x.Index == bakIndex).FirstOrDefault().Index = index;
                    if (prevGrp != null) {
                        prevGrp.Index = bakIndex;
                    }
                    SA.Groups = SA.Groups.OrderBy(x => x.Index).ToList();
                    UpdateNodes();
                    foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                        if (n.Text.Contains("[" + index + "]")) {
                            tree.SelectedNode = n;
                        }
                    }
                    DoInfoStuff();
                    break;

                //Stream players.
                case "streamPlayers":
                    if ((uint)index > SoundArchive.MaxStreamPlayerId) {
                        MessageBox.Show("Index is outside the max possible Id!");
                    }
                    var prevStmPly = SA.StreamPlayers.Where(x => x.Index == index).FirstOrDefault();
                    SA.StreamPlayers.Where(x => x.Index == bakIndex).FirstOrDefault().Index = index;
                    if (prevStmPly != null) {
                        prevStmPly.Index = bakIndex;
                    }
                    SA.StreamPlayers = SA.StreamPlayers.OrderBy(x => x.Index).ToList();
                    UpdateNodes();
                    foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                        if (n.Text.Contains("[" + index + "]")) {
                            tree.SelectedNode = n;
                        }
                    }
                    DoInfoStuff();
                    break;

                //Streams.
                case "streams":
                    if ((uint)index > SoundArchive.MaxStreamId) {
                        MessageBox.Show("Index is outside the max possible Id!");
                    }
                    var prevStm = SA.Streams.Where(x => x.Index == index).FirstOrDefault();
                    SA.Streams.Where(x => x.Index == bakIndex).FirstOrDefault().Index = index;
                    if (prevStm != null) {
                        prevStm.Index = bakIndex;
                    }
                    SA.Streams = SA.Streams.OrderBy(x => x.Index).ToList();
                    UpdateNodes();
                    foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                        if (n.Text.Contains("[" + index + "]")) {
                            tree.SelectedNode = n;
                        }
                    }
                    DoInfoStuff();
                    break;

            }
        
        }

        /// <summary>
        /// Force unique Id changed.
        /// </summary>
        public void ForceUniqueIdChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                switch (tree.SelectedNode.Parent.Name) {
                    case "sequences":
                        SA.Sequences.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().ForceIndividualFile = forceUniqueFileBox.Checked;
                        break;
                    case "sequenceArchives":
                        SA.SequenceArchives.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().ForceIndividualFile = forceUniqueFileBox.Checked;
                        break;
                    case "banks":
                        SA.Banks.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().ForceIndividualFile = forceUniqueFileBox.Checked;
                        break;
                    case "waveArchives":
                        SA.WaveArchives.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().ForceIndividualFile = forceUniqueFileBox.Checked;
                        break;
                    case "streams":
                        SA.Streams.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().ForceIndividualFile = forceUniqueFileBox.Checked;
                        break;
                }
            }
        }

        /// <summary>
        /// Load individual changed.
        /// </summary>
        public void WarLoadIndividualChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                SA.WaveArchives.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().LoadIndividually = loadIndividuallyBox.Checked;
            }
        }

        /// <summary>
        /// Populate a combo box with wave archives.
        /// </summary>
        /// <param name="a">The sound archive.</param>
        /// <param name="c">The combo box.</param>
        public static void PopulateWaveArchiveBox(SoundArchive a, ComboBox c) {
            c.Items.Clear();
            c.Items.Add("FFFF - Blank");
            c.Items.Add("Other Index");
            foreach (var w in a.WaveArchives) {
                c.Items.Add("[" + w.Index + "] - " + w.Name);
            }
        }

        /// <summary>
        /// Set the wave archive index properly for a combo box.
        /// </summary>
        /// <param name="a">The sound archive.</param>
        /// <param name="c">The combo box.</param>
        /// <param name="id">The Id.</param>
        public static void SetWaveArchiveIndex(SoundArchive a, ComboBox c, ushort id) {
            var e = a.WaveArchives.Where(x => x.Index == id).FirstOrDefault();
            if (e == null) {
                if (id == 0xFFFF) {
                    c.SelectedIndex = 0;
                } else {
                    c.SelectedIndex = 1;
                }
            } else {
                c.SelectedItem = "[" + e.Index + "] - " + e.Name;
            }
        }

        /// <summary>
        /// Set the wave archive index properly for a number box.
        /// </summary>
        /// <param name="a">The sound archive.</param>
        /// <param name="c">The combo box.</param>
        /// <param name="id">The Id.</param>
        public static void SetWaveArchiveIndex(SoundArchive a, NumericUpDown n, ushort id) {
            if (id == 0xFFFF) {
                n.Value = -1;
            } else {
                n.Value = id;
            }
        }

        /// <summary>
        /// Changed.
        /// </summary>
        public void BnkWar0BoxChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                SA.Banks.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().ReadingWave0Id = (ushort)(bnkWar0Box.Value == -1 ? 0xFFFF : bnkWar0Box.Value);
                WritingInfo = true;
                SetWaveArchiveIndex(SA, bnkWar0ComboBox, (ushort)(bnkWar0Box.Value == -1 ? 0xFFFF : bnkWar0Box.Value));
                WritingInfo = false;
                SetNewWaveArchiveInBank(SA, GetIdFromNode(tree.SelectedNode), 0);
            }
        }

        /// <summary>
        /// Changed.
        /// </summary>
        public void BnkWar1BoxChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                SA.Banks.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().ReadingWave1Id = (ushort)(bnkWar1Box.Value == -1 ? 0xFFFF : bnkWar1Box.Value);
                WritingInfo = true;
                SetWaveArchiveIndex(SA, bnkWar1ComboBox, (ushort)(bnkWar1Box.Value == -1 ? 0xFFFF : bnkWar1Box.Value));
                WritingInfo = false;
                SetNewWaveArchiveInBank(SA, GetIdFromNode(tree.SelectedNode), 1);
            }
        }

        /// <summary>
        /// Changed.
        /// </summary>
        public void BnkWar2BoxChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                SA.Banks.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().ReadingWave2Id = (ushort)(bnkWar2Box.Value == -1 ? 0xFFFF : bnkWar2Box.Value);
                WritingInfo = true;
                SetWaveArchiveIndex(SA, bnkWar2ComboBox, (ushort)(bnkWar2Box.Value == -1 ? 0xFFFF : bnkWar2Box.Value));
                WritingInfo = false;
                SetNewWaveArchiveInBank(SA, GetIdFromNode(tree.SelectedNode), 2);
            }
        }

        /// <summary>
        /// Changed.
        /// </summary>
        public void BnkWar3BoxChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                SA.Banks.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().ReadingWave3Id = (ushort)(bnkWar3Box.Value == -1 ? 0xFFFF : bnkWar3Box.Value);
                WritingInfo = true;
                SetWaveArchiveIndex(SA, bnkWar3ComboBox, (ushort)(bnkWar3Box.Value == -1 ? 0xFFFF : bnkWar3Box.Value));
                WritingInfo = false;
                SetNewWaveArchiveInBank(SA, GetIdFromNode(tree.SelectedNode), 3);
            }
        }

        /// <summary>
        /// Changed.
        /// </summary>
        public void BnkWar0ComboBoxChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                ushort val = (ushort)bnkWar0ComboBox.SelectedIndex;
                if (val == 0) {
                    val = 0xFFFF;
                } else if (val == 1) {
                    return;
                } else {
                    val = ushort.Parse(((string)bnkWar0ComboBox.SelectedItem).Split('[')[1].Split(']')[0]);
                }
                SA.Banks.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().ReadingWave0Id = val;
                WritingInfo = true;
                SetWaveArchiveIndex(SA, bnkWar0Box, val);
                WritingInfo = false;
                SetNewWaveArchiveInBank(SA, GetIdFromNode(tree.SelectedNode), 0);
            }
        }

        /// <summary>
        /// Changed.
        /// </summary>
        public void BnkWar1ComboBoxChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                ushort val = (ushort)bnkWar1ComboBox.SelectedIndex;
                if (val == 0) {
                    val = 0xFFFF;
                } else if (val == 1) {
                    return;
                } else {
                    val = ushort.Parse(((string)bnkWar1ComboBox.SelectedItem).Split('[')[1].Split(']')[0]);
                }
                SA.Banks.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().ReadingWave1Id = val;
                WritingInfo = true;
                SetWaveArchiveIndex(SA, bnkWar1Box, val);
                WritingInfo = false;
                SetNewWaveArchiveInBank(SA, GetIdFromNode(tree.SelectedNode), 1);
            }
        }

        /// <summary>
        /// Changed.
        /// </summary>
        public void BnkWar2ComboBoxChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                ushort val = (ushort)bnkWar2ComboBox.SelectedIndex;
                if (val == 0) {
                    val = 0xFFFF;
                } else if (val == 1) {
                    return;
                } else {
                    val = ushort.Parse(((string)bnkWar2ComboBox.SelectedItem).Split('[')[1].Split(']')[0]);
                }
                SA.Banks.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().ReadingWave2Id = val;
                WritingInfo = true;
                SetWaveArchiveIndex(SA, bnkWar2Box, val);
                WritingInfo = false;
                SetNewWaveArchiveInBank(SA, GetIdFromNode(tree.SelectedNode), 2);
            }
        }

        /// <summary>
        /// Changed.
        /// </summary>
        public void BnkWar3ComboBoxChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                ushort val = (ushort)bnkWar3ComboBox.SelectedIndex;
                if (val == 0) {
                    val = 0xFFFF;
                } else if (val == 1) {
                    return;
                } else {
                    val = ushort.Parse(((string)bnkWar3ComboBox.SelectedItem).Split('[')[1].Split(']')[0]);
                }
                SA.Banks.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().ReadingWave3Id = val;
                WritingInfo = true;
                SetWaveArchiveIndex(SA, bnkWar3Box, val);
                WritingInfo = false;
                SetNewWaveArchiveInBank(SA, GetIdFromNode(tree.SelectedNode), 3);
            }
        }

        /// <summary>
        /// Set new wave archive in bank.
        /// </summary>
        /// <param name="s">Sound archive.</param>
        /// <param name="bankId">Bank Id.</param>
        /// <param name="warId">Wave archive Id.</param>
        public static void SetNewWaveArchiveInBank(SoundArchive s, int bankId, int warId) {
            var b = s.Banks.Where(x => x.Index == bankId).FirstOrDefault();
            switch (warId) {
                case 0:
                    b.WaveArchives[warId] = s.WaveArchives.Where(x => x.Index == b.ReadingWave0Id).FirstOrDefault();
                    break;
                case 1:
                    b.WaveArchives[warId] = s.WaveArchives.Where(x => x.Index == b.ReadingWave1Id).FirstOrDefault();
                    break;
                case 2:
                    b.WaveArchives[warId] = s.WaveArchives.Where(x => x.Index == b.ReadingWave2Id).FirstOrDefault();
                    break;
                case 3:
                    b.WaveArchives[warId] = s.WaveArchives.Where(x => x.Index == b.ReadingWave3Id).FirstOrDefault();
                    break;
            }
        }

        /// <summary>
        /// Populate the group grid.
        /// </summary>
        /// <param name="v">The data grid view.</param>
        /// <param name="g">The group.</param>
        public void PopulateGroupGrid(DataGridView v, GroupInfo g) {

            //Clear.
            v.Rows.Clear();

            //Get combo box list.
            var c = (v.Columns[0] as DataGridViewComboBoxColumn);
            c.Items.Clear();
            foreach (var e in SA.Sequences) {
                c.Items.Add("[" + e.Index + "] " + e.Name + " (Sequence)");
            }
            foreach (var e in SA.SequenceArchives) {
                c.Items.Add("[" + e.Index + "] " + e.Name + " (Sequence Archive)");
            }
            foreach (var e in SA.Banks) {
                c.Items.Add("[" + e.Index + "] " + e.Name + " (Bank)");
            }
            foreach (var e in SA.WaveArchives) {
                c.Items.Add("[" + e.Index + "] " + e.Name + " (Wave Archive)");
            }

            //For each item.
            foreach (var e in g.Entries) {

                //Add row.
                v.Rows.Add(new DataGridViewRow());

                //Switch type.
                switch (e.Type) {
                    case GroupEntryType.Sequence:
                        ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[0]).Value = "[" + (e.Entry as SequenceInfo).Index + "] " + (e.Entry as SequenceInfo).Name + " (Sequence)";
                        ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Items.Add("Sequence");
                        ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Items.Add("Bank");
                        ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Items.Add("Wave Archive");
                        ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Items.Add("Sequence + Bank");
                        ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Items.Add("Sequence + Wave Archive");
                        ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Items.Add("Bank + Wave Archive");
                        ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Items.Add("Sequence + Bank + Wave Archive");
                        if (e.LoadSequence && e.LoadBank && e.LoadWaveArchive) {
                            ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Value = "Sequence + Bank + Wave Archive";
                        } else if (e.LoadBank && e.LoadWaveArchive) {
                            ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Value = "Bank + Wave Archive";
                        } else if (e.LoadSequence && e.LoadWaveArchive) {
                            ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Value = "Sequence + Wave Archive";
                        } else if (e.LoadSequence && e.LoadBank) {
                            ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Value = "Sequence + Bank";
                        } else if (e.LoadWaveArchive) {
                            ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Value = "Wave Archive";
                        } else if (e.LoadBank) {
                            ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Value = "Bank";
                        } else {
                            ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Value = "Sequence";
                        }
                        break;
                    case GroupEntryType.SequenceArchive:
                        ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[0]).Value = "[" + (e.Entry as SequenceArchiveInfo).Index + "] " + (e.Entry as SequenceArchiveInfo).Name + " (Sequence Archive)";
                        ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Items.Add("Sequence Archive");
                        ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Value = "Sequence Archive";
                        break;
                    case GroupEntryType.Bank:
                        ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[0]).Value = "[" + (e.Entry as BankInfo).Index + "] " + (e.Entry as BankInfo).Name + " (Bank)";
                        ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Items.Add("Bank");
                        ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Items.Add("Wave Archive");
                        ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Items.Add("Bank + Wave Archive");
                        if (e.LoadBank && e.LoadWaveArchive) {
                            ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Value = "Bank + Wave Archive";
                        } else if (e.LoadWaveArchive) {
                            ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Value = "Wave Archive";
                        } else {
                            ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Value = "Bank";
                        }
                        break;
                    case GroupEntryType.WaveArchive:
                        ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[0]).Value = "[" + (e.Entry as WaveArchiveInfo).Index + "] " + (e.Entry as WaveArchiveInfo).Name + " (Wave Archive)";
                        ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Items.Add("Wave Archive");
                        ((DataGridViewComboBoxCell)v.Rows[v.Rows.Count - 2].Cells[1]).Value = "Wave Archive";
                        break;
                }
            
            }

        }

        /// <summary>
        /// Group entries changed.
        /// </summary>
        public void GroupEntriesChanged(object sender, EventArgs e) {

            //If to write data.
            if (FileOpen && File != null && !WritingInfo) {

                //Writing info.
                WritingInfo = true;

                //New group info.
                List<GroupEntry> entries = new List<GroupEntry>();

                //For each row.
                for (int i = 1; i < grpEntries.Rows.Count; i++) {

                    //Get the cells.
                    var itemCell = (DataGridViewComboBoxCell)grpEntries.Rows[i - 1].Cells[0];
                    var flagsCell = (DataGridViewComboBoxCell)grpEntries.Rows[i - 1].Cells[1];

                    //Get the type and entry.
                    GroupEntryType t = GroupEntryType.WaveArchive;
                    object entry = null;
                    uint readingId = 0;
                    bool loadWar = false;
                    bool loadBnk = false;
                    bool loadSeqArc = false;
                    bool loadSeq = false;             
                    string bakFlags = "";
                    try { bakFlags = (string)flagsCell.Value; } catch { }
                    try { flagsCell.Value = flagsCell.Items[0]; } catch { bakFlags = ""; }
                    while (flagsCell.Items.Count > 1) {
                        flagsCell.Items.RemoveAt(flagsCell.Items.Count - 1);
                    }
                    switch (((string)itemCell.Value).Split('(')[1].Split(')')[0]) {
                        case "Sequence":
                            t = GroupEntryType.Sequence;
                            entry = SA.Sequences.Where(x => x.Index == int.Parse(((string)itemCell.Value).Split('[')[1].Split(']')[0])).FirstOrDefault();
                            readingId = (uint)(entry as SequenceInfo).Index;
                            if (flagsCell.Items.Count < 1) {
                                flagsCell.Items.Add("Sequence");
                            } else {
                                flagsCell.Items[0] = "Sequence";
                            }
                            flagsCell.Items.Add("Bank");
                            flagsCell.Items.Add("Wave Archive");
                            flagsCell.Items.Add("Sequence + Bank");
                            flagsCell.Items.Add("Sequence + Wave Archive");
                            flagsCell.Items.Add("Bank + Wave Archive");
                            flagsCell.Items.Add("Sequence + Bank + Wave Archive");
                            break;
                        case "Sequence Archive":
                            t = GroupEntryType.SequenceArchive;
                            entry = SA.SequenceArchives.Where(x => x.Index == int.Parse(((string)itemCell.Value).Split('[')[1].Split(']')[0])).FirstOrDefault();
                            readingId = (uint)(entry as SequenceArchiveInfo).Index;
                            if (flagsCell.Items.Count < 1) {
                                flagsCell.Items.Add("Sequence Archive");
                            } else {
                                flagsCell.Items[0] = "Sequence Archive";
                            }
                            break;
                        case "Bank":
                            t = GroupEntryType.Bank;
                            entry = SA.Banks.Where(x => x.Index == int.Parse(((string)itemCell.Value).Split('[')[1].Split(']')[0])).FirstOrDefault();
                            readingId = (uint)(entry as BankInfo).Index;
                            if (flagsCell.Items.Count < 1) {
                                flagsCell.Items.Add("Bank");
                            } else {
                                flagsCell.Items[0] = "Bank";
                            }
                            flagsCell.Items.Add("Wave Archive");
                            flagsCell.Items.Add("Bank + Wave Archive");
                            break;
                        case "Wave Archive":
                            t = GroupEntryType.WaveArchive;
                            entry = SA.WaveArchives.Where(x => x.Index == int.Parse(((string)itemCell.Value).Split('[')[1].Split(']')[0])).FirstOrDefault();
                            readingId = (uint)(entry as WaveArchiveInfo).Index;
                            if (flagsCell.Items.Count < 1) {
                                flagsCell.Items.Add("Wave Archive");
                            } else {
                                flagsCell.Items[0] = "Wave Archive";
                            }
                            break;
                    }

                    //Set flag.
                    if (flagsCell.Items.Contains(bakFlags)) {
                        flagsCell.Value = bakFlags;
                    } else { flagsCell.Value = flagsCell.Items[0]; }

                    //Flags.
                    loadSeq = ((string)flagsCell.Value).Contains("Sequence");
                    loadSeqArc = ((string)flagsCell.Value).Contains("Sequence Archive");
                    loadBnk = ((string)flagsCell.Value).Contains("Bank");
                    loadWar = ((string)flagsCell.Value).Contains("Wave Archive");

                    //Add the entry.
                    entries.Add(new GroupEntry() { Type = t, Entry = entry, ReadingId = readingId, LoadSequence = loadSeq, LoadSequenceArchive = loadSeqArc, LoadBank = loadBnk, LoadWaveArchive = loadWar });

                }

                //Set group entries.
                SA.Groups.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().Entries = entries;

                //Writing info.
                WritingInfo = false;

            }
        
        }

        /// <summary>
        /// Stream player type changed.
        /// </summary>
        public void StreamPlayerTypeChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                WritingInfo = true;
                if (stmPlayerChannelType.SelectedIndex == 0) {
                    leftChannelLabel.Text = "Channel:";
                    rightChannelLabel.Text = "(Doesn't Exist)";
                    stmPlayerRightChannelBox.Value = 0;
                    rightChannelLabel.Enabled = false;
                    stmPlayerRightChannelBox.Enabled = false;
                } else {
                    leftChannelLabel.Text = "Left Channel:";
                    rightChannelLabel.Text = "Right Channel:";
                    rightChannelLabel.Enabled = true;
                    stmPlayerRightChannelBox.Enabled = true;
                    if (SA.StreamPlayers.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().LeftChannel != 15) {
                        SA.StreamPlayers.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().RightChannel = (byte)(SA.StreamPlayers.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().LeftChannel + 1);
                    }
                    stmPlayerRightChannelBox.Value = SA.StreamPlayers.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().RightChannel;
                }
                WritingInfo = false;
                SA.StreamPlayers.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().IsStereo = stmPlayerChannelType.SelectedIndex == 1;
            }
        }

        /// <summary>
        /// Stream player channel changed.
        /// </summary>
        public void StreamPlayerLeftChannelChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                SA.StreamPlayers.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().LeftChannel = (byte)stmPlayerLeftChannelBox.Value;
            }
        }

        /// <summary>
        /// Stream player channel changed.
        /// </summary>
        public void StreamPlayerRightChannelChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                SA.StreamPlayers.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().RightChannel = (byte)stmPlayerRightChannelBox.Value;
            }
        }

        /// <summary>
        /// Populate a combo box with stream players.
        /// </summary>
        /// <param name="a">The sound archive.</param>
        /// <param name="c">The combo box.</param>
        public static void PopulateStreamPlayerBox(SoundArchive a, ComboBox c) {
            c.Items.Clear();
            c.Items.Add("Other Index");
            foreach (var w in a.StreamPlayers) {
                c.Items.Add("[" + w.Index + "] - " + w.Name);
            }
        }

        /// <summary>
        /// Set the player index properly for a combo box.
        /// </summary>
        /// <param name="a">The sound archive.</param>
        /// <param name="c">The combo box.</param>
        /// <param name="id">The Id.</param>
        public static void SetStreamPlayerIndex(SoundArchive a, ComboBox c, byte id) {
            var e = a.StreamPlayers.Where(x => x.Index == id).FirstOrDefault();
            if (e == null) {
                c.SelectedIndex = 0;
            } else {
                c.SelectedItem = "[" + e.Index + "] - " + e.Name;
            }
        }

        /// <summary>
        /// Stream volume changed.
        /// </summary>
        public void StreamVolumeChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                SA.Streams.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().Volume = (byte)stmVolumeBox.Value;
            }
        }

        /// <summary>
        /// Stream priority.
        /// </summary>
        public void StreamPriorityChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                SA.Streams.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().Priority = (byte)stmPriorityBox.Value;
            }
        }

        /// <summary>
        /// Mono to stereo changed.
        /// </summary>
        public void StreamMonoToStereoChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                SA.Streams.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().MonoToStereo = stmMonoToStereoBox.Checked;
            }
        }

        /// <summary>
        /// Stream player combo box changed.
        /// </summary>
        public void StreamPlayerComboBoxChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                if (stmPlayerComboBox.SelectedIndex != 0) {
                    WritingInfo = true;
                    byte index = byte.Parse(((string)stmPlayerComboBox.SelectedItem).Split('[')[1].Split(']')[0]);
                    stmPlayerBox.Value = index;
                    SA.Streams.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().ReadingPlayerId = index;
                    SA.Streams.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().Player = SA.StreamPlayers.Where(x => x.Index == index).FirstOrDefault();
                    WritingInfo = false;
                }
            }
        }

        /// <summary>
        /// Stream player box changed.
        /// </summary>
        public void StreamPlayerBoxChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                WritingInfo = true;
                SetStreamPlayerIndex(SA, stmPlayerComboBox, (byte)stmPlayerBox.Value);
                SA.Streams.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().Player = SA.StreamPlayers.Where(x => x.Index == (byte)stmPlayerBox.Value).FirstOrDefault();
                SA.Streams.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().ReadingPlayerId = (byte)stmPlayerBox.Value;
                WritingInfo = false;
            }
        }

        /// <summary>
        /// Player changed.
        /// </summary>
        public void PlayerSequenceMaxChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                SA.Players.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().SequenceMax = (ushort)playerMaxSequencesBox.Value;
            }
        }

        /// <summary>
        /// Player changed.
        /// </summary>
        public void PlayerHeapSizeChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                SA.Players.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().HeapSize = (uint)playerHeapSizeBox.Value;
            }
        }

        /// <summary>
        /// Player changed.
        /// </summary>
        public void PlayerFlagsChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                var p = SA.Players.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                p.ChannelFlags[0] = playerFlag0Box.Checked;
                p.ChannelFlags[1] = playerFlag1Box.Checked;
                p.ChannelFlags[2] = playerFlag2Box.Checked;
                p.ChannelFlags[3] = playerFlag3Box.Checked;
                p.ChannelFlags[4] = playerFlag4Box.Checked;
                p.ChannelFlags[5] = playerFlag5Box.Checked;
                p.ChannelFlags[6] = playerFlag6Box.Checked;
                p.ChannelFlags[7] = playerFlag7Box.Checked;
                p.ChannelFlags[8] = playerFlag8Box.Checked;
                p.ChannelFlags[9] = playerFlag9Box.Checked;
                p.ChannelFlags[10] = playerFlag10Box.Checked;
                p.ChannelFlags[11] = playerFlag11Box.Checked;
                p.ChannelFlags[12] = playerFlag12Box.Checked;
                p.ChannelFlags[13] = playerFlag13Box.Checked;
                p.ChannelFlags[14] = playerFlag14Box.Checked;
                p.ChannelFlags[15] = playerFlag15Box.Checked;
            }
        }

        /// <summary>
        /// Populate a combo box with banks.
        /// </summary>
        /// <param name="a">The sound archive.</param>
        /// <param name="c">The combo box.</param>
        public static void PopulateBankBox(SoundArchive a, ComboBox c) {
            c.Items.Clear();
            c.Items.Add("Other Index");
            foreach (var w in a.Banks) {
                c.Items.Add("[" + w.Index + "] - " + w.Name);
            }
        }

        /// <summary>
        /// Set the bank index properly for a combo box.
        /// </summary>
        /// <param name="a">The sound archive.</param>
        /// <param name="c">The combo box.</param>
        /// <param name="id">The Id.</param>
        public static void SetBankIndex(SoundArchive a, ComboBox c, uint id) {
            var e = a.Banks.Where(x => x.Index == id).FirstOrDefault();
            if (e == null) {
                c.SelectedIndex = 0;
            } else {
                c.SelectedItem = "[" + e.Index + "] - " + e.Name;
            }
        }

        /// <summary>
        /// Populate a combo box with players.
        /// </summary>
        /// <param name="a">The sound archive.</param>
        /// <param name="c">The combo box.</param>
        public static void PopulatePlayerBox(SoundArchive a, ComboBox c) {
            c.Items.Clear();
            c.Items.Add("Other Index");
            foreach (var w in a.Players) {
                c.Items.Add("[" + w.Index + "] - " + w.Name);
            }
        }

        /// <summary>
        /// Set the player index properly for a combo box.
        /// </summary>
        /// <param name="a">The sound archive.</param>
        /// <param name="c">The combo box.</param>
        /// <param name="id">The Id.</param>
        public static void SetPlayerIndex(SoundArchive a, ComboBox c, byte id) {
            var e = a.Players.Where(x => x.Index == id).FirstOrDefault();
            if (e == null) {
                c.SelectedIndex = 0;
            } else {
                c.SelectedItem = "[" + e.Index + "] - " + e.Name;
            }
        }

        /// <summary>
        /// Sequence info changed.
        /// </summary>
        public void SequenceVolumeChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                SA.Sequences.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().Volume = (byte)seqVolumeBox.Value;
            }
        }

        /// <summary>
        /// Sequence info changed.
        /// </summary>
        public void SequenceChannelPriorityChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                SA.Sequences.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().ChannelPriority = (byte)seqChannelPriorityBox.Value;
            }
        }

        /// <summary>
        /// Sequence info changed.
        /// </summary>
        public void SequencePlayerPriorityChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                SA.Sequences.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().PlayerPriority = (byte)seqPlayerPriorityBox.Value;
            }
        }

        /// <summary>
        /// Sequence info changed.
        /// </summary>
        public void SequenceBankComboBoxChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                if (seqBankComboBox.SelectedIndex != 0) {
                    WritingInfo = true;
                    ushort index = ushort.Parse(((string)seqBankComboBox.SelectedItem).Split('[')[1].Split(']')[0]);
                    seqBankBox.Value = index;
                    SA.Sequences.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().ReadingBankId = index;
                    SA.Sequences.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().Bank = SA.Banks.Where(x => x.Index == index).FirstOrDefault();
                    WritingInfo = false;
                }
            }
        }

        /// <summary>
        /// Sequence info changed.
        /// </summary>
        public void SequenceBankBoxChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                WritingInfo = true;
                SetBankIndex(SA, seqBankComboBox, (ushort)seqBankBox.Value);
                SA.Sequences.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().Bank = SA.Banks.Where(x => x.Index == (ushort)seqBankBox.Value).FirstOrDefault();
                SA.Sequences.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().ReadingBankId = (ushort)seqBankBox.Value;
                WritingInfo = false;
            }
        }

        /// <summary>
        /// Sequence info changed.
        /// </summary>
        public void SequencePlayerComboBoxChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                if (seqPlayerComboBox.SelectedIndex != 0) {
                    WritingInfo = true;
                    byte index = byte.Parse(((string)seqPlayerComboBox.SelectedItem).Split('[')[1].Split(']')[0]);
                    seqPlayerBox.Value = index;
                    SA.Sequences.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().ReadingPlayerId = index;
                    SA.Sequences.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().Player = SA.Players.Where(x => x.Index == index).FirstOrDefault();
                    WritingInfo = false;
                }
            }
        }

        /// <summary>
        /// Sequence info changed.
        /// </summary>
        public void SequencePlayerBoxChanged(object sender, EventArgs e) {
            if (FileOpen && File != null && !WritingInfo) {
                WritingInfo = true;
                SetPlayerIndex(SA, seqPlayerComboBox, (byte)seqPlayerBox.Value);
                SA.Sequences.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().Player = SA.Players.Where(x => x.Index == (byte)seqPlayerBox.Value).FirstOrDefault();
                SA.Sequences.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault().ReadingPlayerId = (byte)seqPlayerBox.Value;
                WritingInfo = false;
            }
        }

        /// <summary>
        /// Play click.
        /// </summary>
        public void PlayClick(object sender, EventArgs e) {
            if (tree.SelectedNode.Parent.Name == "sequences") {
                var s = SA.Sequences.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                try { Player.PrepareForSong(new PlayableBank[] { s.Bank.File }, s.Bank.GetAssociatedWaves()); } catch { MessageBox.Show("Sequence entry has no valid bank hooked up to it!"); return; }
                s.File.ReadCommandData();
                Player.LoadSong(s.File.Commands);
                kermalisPosition.Maximum = (int)Player.MaxTicks;
                kermalisPosition.TickFrequency = kermalisPosition.Maximum / 10;
                kermalisPosition.LargeChange = kermalisPosition.Maximum / 20;
                Player.Play();
            } else {
                var a = SA.SequenceArchives.Where(x => x.Index == GetIdFromNode(tree.SelectedNode.Parent)).FirstOrDefault();
                var s = a.File.Sequences.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                try { Player.PrepareForSong(new PlayableBank[] { s.Bank.File }, s.Bank.GetAssociatedWaves()); } catch { MessageBox.Show("Sequence Archive entry has no valid bank hooked up to it!"); return; }
                a.File.ReadCommandData(true);
                Player.LoadSong(a.File.Commands, a.File.PublicLabels.Values.ElementAt(a.File.Sequences.IndexOf(s)));
                kermalisPosition.Maximum = (int)Player.MaxTicks;
                kermalisPosition.TickFrequency = kermalisPosition.Maximum / 10;
                kermalisPosition.LargeChange = kermalisPosition.Maximum / 20;
                Player.Play();
            }
        }

        /// <summary>
        /// Position tick.
        /// </summary>
        public void PositionTick(object sender, EventArgs e) {
            if (Player != null && PositionBarFree) {
                kermalisPosition.Value = Player.GetCurrentPosition() > kermalisPosition.Maximum ? kermalisPosition.Maximum : (int)Player.GetCurrentPosition();
            }
        }

        /// <summary>
        /// Mouse down.
        /// </summary>
        public void PositionMouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                PositionBarFree = false;
            }
        }

        /// <summary>
        /// Mouse up.
        /// </summary>
        public void PositionMouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left && Player != null && Player.Events != null) {
                Player.SetCurrentPosition(kermalisPosition.Value);
                PositionBarFree = true;
            }
        }

        /// <summary>
        /// Pause click.
        /// </summary>
        public void PauseClick(object sender, EventArgs e) {
            Player.Pause();
        }

        /// <summary>
        /// Stop click.
        /// </summary>
        public void StopClick(object sender, EventArgs e) {
            Player.Stop();
        }

        /// <summary>
        /// Volume changed.
        /// </summary>
        public void VolumeChanged(object sender, EventArgs e) {
            Mixer.Volume = kermalisVolumeSlider.Value / 100f;
        }

        /// <summary>
        /// Loop changed.
        /// </summary>
        public void LoopChanged(object sender, EventArgs e) {
            Player.NumLoops = kermalisLoopBox.Checked ? 0xFFFFFFFF : 0;
        }

        /// <summary>
        /// Closing.
        /// </summary>
        public void SAClosing(object sender, FormClosingEventArgs e) {
            Player.Stop();
            Player.Dispose();
            Mixer.Dispose();
            Timer.Stop();
            Environment.Exit(Environment.ExitCode);
        }

        /// <summary>
        /// Key press.
        /// </summary>
        public void KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == ' ' && tree.SelectedNode.Parent != null) {
                if (tree.SelectedNode.Parent.Parent != null || tree.SelectedNode.Parent.Name == "sequences") {
                    PlayClick(sender, e);
                }
            }
        }

        /// <summary>
        /// Add above.
        /// </summary>
        public void AddAbove(object sender, EventArgs e) {

            //Sequences.
            if (tree.SelectedNode.Parent.Name.Equals("sequences")) {
                int ind = GetNextAvailablePreviousId(GetIdFromNode(tree.SelectedNode), SoundArchive.MaxSequenceId, tree.SelectedNode.Parent.Name);
                if (ind == -1) {
                    return;
                }
                AddSequence(ind);
                SA.Sequences = SA.Sequences.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

            //Sequence archives.
            else if (tree.SelectedNode.Parent.Name.Equals("sequenceArchives")) {
                int ind = GetNextAvailablePreviousId(GetIdFromNode(tree.SelectedNode), SoundArchive.MaxSequenceArchiveId, tree.SelectedNode.Parent.Name);
                if (ind == -1) {
                    return;
                }
                AddSequenceArchive(ind);
                SA.SequenceArchives = SA.SequenceArchives.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

            //Banks.
            else if (tree.SelectedNode.Parent.Name.Equals("banks")) {
                int ind = GetNextAvailablePreviousId(GetIdFromNode(tree.SelectedNode), SoundArchive.MaxBankId, tree.SelectedNode.Parent.Name);
                if (ind == -1) {
                    return;
                }
                AddBank(ind);
                SA.Banks = SA.Banks.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

            //Wave archives.
            else if (tree.SelectedNode.Parent.Name.Equals("waveArchives")) {
                int ind = GetNextAvailablePreviousId(GetIdFromNode(tree.SelectedNode), SoundArchive.MaxWaveArchiveId, tree.SelectedNode.Parent.Name);
                if (ind == -1) {
                    return;
                }
                AddWaveArchive(ind);
                SA.WaveArchives = SA.WaveArchives.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

            //Player.
            else if (tree.SelectedNode.Parent.Name.Equals("players")) {
                int ind = GetNextAvailablePreviousId(GetIdFromNode(tree.SelectedNode), SoundArchive.MaxPlayerId, tree.SelectedNode.Parent.Name);
                if (ind == -1) {
                    return;
                }
                AddSequencePlayer(ind);
                SA.Players = SA.Players.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

            //Group.
            else if (tree.SelectedNode.Parent.Name.Equals("groups")) {
                int ind = GetNextAvailablePreviousId(GetIdFromNode(tree.SelectedNode), SoundArchive.MaxGroupId, tree.SelectedNode.Parent.Name);
                if (ind == -1) {
                    return;
                }
                AddGroup(ind);
                SA.Groups = SA.Groups.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

            //Stream player.
            else if (tree.SelectedNode.Parent.Name.Equals("streamPlayers")) {
                int ind = GetNextAvailablePreviousId(GetIdFromNode(tree.SelectedNode), SoundArchive.MaxStreamPlayerId, tree.SelectedNode.Parent.Name);
                if (ind == -1) {
                    return;
                }
                AddStreamPlayer(ind);
                SA.StreamPlayers = SA.StreamPlayers.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

            //Stream.
            else if (tree.SelectedNode.Parent.Name.Equals("streams")) {
                int ind = GetNextAvailablePreviousId(GetIdFromNode(tree.SelectedNode), SoundArchive.MaxStreamId, tree.SelectedNode.Parent.Name);
                if (ind == -1) {
                    return;
                }
                AddStream(ind);
                SA.Streams = SA.Streams.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

        }

        /// <summary>
        /// Add below.
        /// </summary>
        public void AddBelow(object sender, EventArgs e) {

            //Sequences.
            if (tree.SelectedNode.Parent.Name.Equals("sequences")) {
                int ind = GetNextAvailableForwardId(GetIdFromNode(tree.SelectedNode), SoundArchive.MaxSequenceId, tree.SelectedNode.Parent.Name);
                if (ind == -1) {
                    return;
                }
                AddSequence(ind);
                SA.Sequences = SA.Sequences.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

            //Sequence archives.
            else if (tree.SelectedNode.Parent.Name.Equals("sequenceArchives")) {
                int ind = GetNextAvailableForwardId(GetIdFromNode(tree.SelectedNode), SoundArchive.MaxSequenceArchiveId, tree.SelectedNode.Parent.Name);
                if (ind == -1) {
                    return;
                }
                AddSequenceArchive(ind);
                SA.SequenceArchives = SA.SequenceArchives.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

            //Banks.
            else if (tree.SelectedNode.Parent.Name.Equals("banks")) {
                int ind = GetNextAvailableForwardId(GetIdFromNode(tree.SelectedNode), SoundArchive.MaxBankId, tree.SelectedNode.Parent.Name);
                if (ind == -1) {
                    return;
                }
                AddBank(ind);
                SA.Banks = SA.Banks.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

            //Wave archives.
            else if (tree.SelectedNode.Parent.Name.Equals("waveArchives")) {
                int ind = GetNextAvailableForwardId(GetIdFromNode(tree.SelectedNode), SoundArchive.MaxWaveArchiveId, tree.SelectedNode.Parent.Name);
                if (ind == -1) {
                    return;
                }
                AddWaveArchive(ind);
                SA.WaveArchives = SA.WaveArchives.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

            //Player.
            else if (tree.SelectedNode.Parent.Name.Equals("players")) {
                int ind = GetNextAvailableForwardId(GetIdFromNode(tree.SelectedNode), SoundArchive.MaxPlayerId, tree.SelectedNode.Parent.Name);
                if (ind == -1) {
                    return;
                }
                AddSequencePlayer(ind);
                SA.Players = SA.Players.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

            //Group.
            else if (tree.SelectedNode.Parent.Name.Equals("groups")) {
                int ind = GetNextAvailableForwardId(GetIdFromNode(tree.SelectedNode), SoundArchive.MaxGroupId, tree.SelectedNode.Parent.Name);
                if (ind == -1) {
                    return;
                }
                AddGroup(ind);
                SA.Groups = SA.Groups.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

            //Stream player.
            else if (tree.SelectedNode.Parent.Name.Equals("streamPlayers")) {
                int ind = GetNextAvailableForwardId(GetIdFromNode(tree.SelectedNode), SoundArchive.MaxStreamPlayerId, tree.SelectedNode.Parent.Name);
                if (ind == -1) {
                    return;
                }
                AddStreamPlayer(ind);
                SA.StreamPlayers = SA.StreamPlayers.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

            //Stream.
            else if (tree.SelectedNode.Parent.Name.Equals("streams")) {
                int ind = GetNextAvailableForwardId(GetIdFromNode(tree.SelectedNode), SoundArchive.MaxStreamId, tree.SelectedNode.Parent.Name);
                if (ind == -1) {
                    return;
                }
                AddStream(ind);
                SA.Streams = SA.Streams.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Parent.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

        }

        /// <summary>
        /// Replace.
        /// </summary>
        public void Replace(object sender, EventArgs e) {

            //Open file dialog.
            OpenFileDialog o = new OpenFileDialog();
            o.RestoreDirectory = true;
            int ind = GetIdFromNode(tree.SelectedNode);

            //Switch the type.
            switch (tree.SelectedNode.Parent.Name) {

                //Sequence.
                case "sequences":
                    o.Filter = "Supported Sound Files|*.sseq;*.smft;*.mid|Sound Sequence|*.sseq|SMF Text|*.smft|MIDI|*.mid";
                    break;

                //Sequence archive.
                case "sequenceArchives":
                    o.Filter = "Sequence Archive|*.ssar;*.mus|Sound Sequence Archive|*.ssar|Music List|*.mus";
                    break;

                //Bank.
                case "banks":
                    o.Filter = "Supported Bank Files|*.sbnk;*.sf2;*.dls|Sound Bank|*.sbnk|Soundfont|*.sf2|Downloadable Sounds|*.dls";
                    break;

                //Wave archives.
                case "waveArchives":
                    o.Filter = "Sound Wave Archive|*.swar";
                    break;

                //Streams.
                case "streams":
                    o.Filter = "Supported Sound Files|*.strm;*.swav;*.wav|Stream|*.strm|Sound Wave|*.swav|Wave|*.wav";
                    break;

            }

            //Import the file.
            if (o.ShowDialog() == DialogResult.OK) {

                //Switch extension.
                switch (Path.GetExtension(o.FileName)) {

                    //SSEQ.
                    case ".sseq":
                        SA.Sequences.Where(x => x.Index == ind).FirstOrDefault().File = new Sequence();
                        SA.Sequences.Where(x => x.Index == ind).FirstOrDefault().File.Read(o.FileName);
                        DoInfoStuff();
                        break;

                    //SMFT.
                    case ".smft":
                        var seqInfo = SA.Sequences.Where(x => x.Index == ind).FirstOrDefault();
                        seqInfo.File = new Sequence();
                        seqInfo.File.FromText(System.IO.File.ReadAllLines(o.FileName).ToList());
                        seqInfo.File.WriteCommandData();
                        break;

                    //MIDI.
                    case ".mid":
                        switch (seqImportModeBox.SelectedIndex) {

                            //Nitro Studio.
                            case 0:
                                SA.Sequences.Where(x => x.Index == ind).FirstOrDefault().File = new Sequence();
                                SA.Sequences.Where(x => x.Index == ind).FirstOrDefault().File.FromMIDI(o.FileName);
                                break;

                            //LoveEmu.
                            case 1:
                                if (!System.IO.File.Exists(NitroPath + "/midi2sseq.exe")) {
                                    MessageBox.Show("Cannot find midi2sseq.exe!");
                                    return;
                                }
                                System.IO.File.Copy(o.FileName, "temp.mid", true);
                                Process pro = new Process();
                                pro.StartInfo.FileName = NitroPath + "/midi2sseq.exe";
                                pro.StartInfo.Arguments = "temp.mid temp.sseq";
                                pro.Start();
                                pro.WaitForExit();
                                SA.Sequences.Where(x => x.Index == ind).FirstOrDefault().File = new Sequence();
                                SA.Sequences.Where(x => x.Index == ind).FirstOrDefault().File.Read("temp.sseq");
                                System.IO.File.Delete("temp.mid");
                                System.IO.File.Delete("temp.sseq");
                                break;

                            //Nintendo tools.
                            case 2:
                                if (!System.IO.File.Exists(NitroPath + "/smfconv.exe")) {
                                    MessageBox.Show("Cannot find smfconv.exe!");
                                    return;
                                }
                                if (!System.IO.File.Exists(NitroPath + "/seqconv.exe")) {
                                    MessageBox.Show("Cannot find seqconv.exe!");
                                    return;
                                }
                                System.IO.File.Copy(o.FileName, "temp.mid", true);
                                Process pr = new Process();
                                pr.StartInfo.FileName = NitroPath + "/smfconv.exe";
                                pr.StartInfo.Arguments = "temp.mid";
                                pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                pr.Start();
                                pr.WaitForExit();
                                Process p = new Process();
                                p.StartInfo.FileName = NitroPath + "/seqconv.exe";
                                p.StartInfo.Arguments = "temp.smft";
                                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                p.Start();
                                p.WaitForExit();
                                SA.Sequences.Where(x => x.Index == ind).FirstOrDefault().File = new Sequence();
                                SA.Sequences.Where(x => x.Index == ind).FirstOrDefault().File.Read("temp.sseq");
                                System.IO.File.Delete("temp.mid");
                                System.IO.File.Delete("temp.smft");
                                System.IO.File.Delete("temp.sseq");
                                break;

                        }
                        break;

                    //SSAR.
                    case ".ssar":
                        var seqArcInfo = SA.SequenceArchives.Where(x => x.Index == ind).FirstOrDefault();
                        seqArcInfo.File = new SequenceArchive();
                        seqArcInfo.File.Read(o.FileName);
                        seqArcInfo.File.ReadCommandData(true);
                        seqArcInfo.File.FromText(seqArcInfo.File.ToText().ToList(), SA);
                        UpdateNodes();
                        DoInfoStuff();
                        break;

                    //MUS.
                    case ".mus":
                        var seqArcInfo2 = SA.SequenceArchives.Where(x => x.Index == ind).FirstOrDefault();
                        seqArcInfo2.File = new SequenceArchive();
                        seqArcInfo2.File.FromText(System.IO.File.ReadAllLines(o.FileName).ToList(), SA);
                        seqArcInfo2.File.WriteCommandData();
                        UpdateNodes();
                        DoInfoStuff();
                        break;

                    //SBNK.
                    case ".sbnk":
                        SA.Banks.Where(x => x.Index == ind).FirstOrDefault().File = new Bank();
                        SA.Banks.Where(x => x.Index == ind).FirstOrDefault().File.Read(o.FileName);
                        DoInfoStuff();
                        break;

                    //SF2.
                    case ".sf2":
                        SoundFont sf2 = new SoundFont(o.FileName);
                        ReplaceBankWithSoundFont(SA.Banks.Where(x => x.Index == ind).FirstOrDefault(), sf2);
                        DoInfoStuff();
                        return;

                    //DLS.
                    case ".dls":
                        DownloadableSounds dls = new DownloadableSounds(o.FileName);
                        ReplaceBankWithDLS(SA.Banks.Where(x => x.Index == ind).FirstOrDefault(), dls);
                        DoInfoStuff();
                        return;

                    //SWAR.
                    case ".swar":
                        SA.WaveArchives.Where(x => x.Index == ind).FirstOrDefault().File = new WaveArchive();
                        SA.WaveArchives.Where(x => x.Index == ind).FirstOrDefault().File.Read(o.FileName);
                        DoInfoStuff();
                        break;

                    //STRM.
                    case ".strm":
                        SA.Streams.Where(x => x.Index == ind).FirstOrDefault().File = new NitroFileLoader.Stream();
                        SA.Streams.Where(x => x.Index == ind).FirstOrDefault().File.Read(o.FileName);
                        DoInfoStuff();
                        break;

                    //SWAV.
                    case ".swav":
                        SA.Streams.Where(x => x.Index == ind).FirstOrDefault().File = new NitroFileLoader.Stream();
                        Wave swav = new Wave();
                        swav.Read(o.FileName);
                        SA.Streams.Where(x => x.Index == ind).FirstOrDefault().File.FromOtherStreamFile(swav);
                        DoInfoStuff();
                        break;

                    //WAV.
                    case ".wav":
                        SA.Streams.Where(x => x.Index == ind).FirstOrDefault().File = new NitroFileLoader.Stream();
                        RiffWave riff = new RiffWave();
                        riff.Read(o.FileName);
                        SA.Streams.Where(x => x.Index == ind).FirstOrDefault().File.FromOtherStreamFile(riff);
                        DoInfoStuff();
                        break;

                }

            }

        }

        /// <summary>
        /// Export.
        /// </summary>
        public void Export(object sender, EventArgs e) {

            //Save file dialog.
            SaveFileDialog s = new SaveFileDialog();
            s.RestoreDirectory = true;
            s.FileName = tree.SelectedNode.Text.Substring(tree.SelectedNode.Text.IndexOf(' ') + 1);
            int ind = GetIdFromNode(tree.SelectedNode);

            //Switch the type.
            switch (tree.SelectedNode.Parent.Name) {

                //Sequence.
                case "sequences":
                    s.Filter = "Supported Sound Files|*.sseq;*.smft;*.mid;*.wav|Sound Sequence|*.sseq|SMF Text|*.smft|MIDI|*.mid|Wave|*.wav";
                    s.FileName += ".sseq";
                    break;

                //Sequence archive.
                case "sequenceArchives":
                    s.Filter = "Sequence Archive|*.ssar;*.mus|Sound Sequence Archive|*.ssar|Music List|*.mus";
                    s.FileName += ".ssar";
                    break;

                //Bank.
                case "banks":
                    s.Filter = "Supported Bank Files|*.sbnk;*.sf2;*.dls|Sound Bank|*.sbnk|Soundfont|*.sf2|Downloadable Sounds|*.dls";
                    s.FileName += ".sbnk";
                    break;

                //Wave archives.
                case "waveArchives":
                    s.Filter = "Sound Wave Archive|*.swar";
                    s.FileName += ".swar";
                    break;

                //Streams.
                case "streams":
                    s.Filter = "Supported Sound Files|*.strm;*.swav;*.wav|Stream|*.strm|Sound Wave|*.swav|Wave|*.wav";
                    s.FileName += ".strm";
                    break;

            }

            //Special case, sequence archive sequence.
            if (tree.SelectedNode.Parent.Parent != null) {
                s.Filter = "Supported Sound Files|*.sseq;*.smft;*.mid;*.wav|Sound Sequence|*.sseq|SMF Text|*.smft|MIDI|*.mid|Wave|*.wav";
                s.FileName += ".sseq";
            }

            //Export the file.
            if (s.ShowDialog() == DialogResult.OK) {

                //Switch the export type.
                switch (Path.GetExtension(s.FileName)) {

                    //SSEQ.
                    case ".sseq":
                        if (tree.SelectedNode.Parent.Parent == null) {
                            SA.Sequences.Where(x => x.Index == ind).FirstOrDefault().File.Write(s.FileName);
                        } else {
                            throw new NotImplementedException();
                        }
                        break;

                    //SMFT.
                    case ".smft":
                        if (tree.SelectedNode.Parent.Parent == null) {
                            SA.Sequences.Where(x => x.Index == ind).FirstOrDefault().File.ReadCommandData();
                            SA.Sequences.Where(x => x.Index == ind).FirstOrDefault().File.Name = SA.Sequences.Where(x => x.Index == ind).FirstOrDefault().Name;
                            System.IO.File.WriteAllLines(s.FileName, SA.Sequences.Where(x => x.Index == ind).FirstOrDefault().File.ToText());
                        } else {
                            throw new NotImplementedException();
                        }
                        break;

                    //MIDI.
                    case ".mid":
                        if (tree.SelectedNode.Parent.Parent == null) {
                            switch (seqExportModeBox.SelectedIndex) {

                                //Nitro Studio.
                                case 0:
                                    SA.Sequences.Where(x => x.Index == ind).FirstOrDefault().File.SaveMIDI(s.FileName);
                                    break;

                                //LoveEmu.
                                case 1:
                                    if (!System.IO.File.Exists(NitroPath + "/sseq2midi.exe")) {
                                        MessageBox.Show("Cannot find sseq2midi.exe!");
                                        return;
                                    }
                                    SA.Sequences.Where(x => x.Index == ind).FirstOrDefault().File.Write("temp.sseq");
                                    Process pro = new Process();
                                    pro.StartInfo.FileName = NitroPath + "/sseq2midi.exe";
                                    pro.StartInfo.Arguments = "temp.sseq";
                                    pro.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                    pro.Start();
                                    pro.WaitForExit();
                                    if (System.IO.File.Exists(s.FileName) && s.FileName != "temp.mid") { System.IO.File.Delete(s.FileName); }
                                    System.IO.File.Move("temp.mid", s.FileName);
                                    System.IO.File.Delete("temp.sseq");
                                    break;
                            }
                        } else {
                            throw new NotImplementedException();
                        }
                        break;

                    //WAV.
                    case ".wav":
                        if (tree.SelectedNode.Parent.Name == "sequences") {
                            var seq = SA.Sequences.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                            seq.File.ReadCommandData();
                            try {
                                SequenceRecorder rec = new SequenceRecorder(new PlayableBank[] { seq.Bank.File }, seq.Bank.GetAssociatedWaves(), seq.File.Commands, 0, s.FileName);
                                rec.ShowDialog();
                            } catch { MessageBox.Show("Sequence entry has no valid bank hooked up to it!"); return; }
                        } else if (tree.SelectedNode.Parent.Name == "streams") {
                            RiffWave wav = new RiffWave();
                            wav.FromOtherStreamFile(SA.Streams.Where(x => x.Index == ind).FirstOrDefault().File);
                            wav.Write(s.FileName);
                        } else {
                            var a = SA.SequenceArchives.Where(x => x.Index == GetIdFromNode(tree.SelectedNode.Parent)).FirstOrDefault();
                            var seq = a.File.Sequences.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                            a.File.ReadCommandData(true);
                            try {
                                SequenceRecorder rec = new SequenceRecorder(new PlayableBank[] { seq.Bank.File }, seq.Bank.GetAssociatedWaves(), a.File.Commands, a.File.PublicLabels.Values.ElementAt(a.File.Sequences.IndexOf(seq)), s.FileName);
                                rec.ShowDialog();
                            } catch { MessageBox.Show("Sequence entry has no valid bank hooked up to it!"); return; }
                        }
                        break;

                    //SSAR.
                    case ".ssar":
                        SA.SequenceArchives.Where(x => x.Index == ind).FirstOrDefault().File.Write(s.FileName);
                        break;

                    //MUS.
                    case ".mus":
                        SequenceArchive sa = new SequenceArchive();
                        var other = SA.SequenceArchives.Where(x => x.Index == ind).FirstOrDefault().File;
                        sa.Read(other.Write());
                        for (int i = 0; i < sa.Sequences.Count; i++) {
                            sa.Sequences[i].Name = other.Sequences[i].Name;
                            sa.Sequences[i].Bank = other.Sequences[i].Bank;
                            sa.Sequences[i].Player = other.Sequences[i].Player;
                        }
                        uint[] vals = sa.Labels.Values.ToArray();
                        string[] bakNames = sa.Labels.Keys.ToArray();
                        sa.Labels = new Dictionary<string, uint>();
                        int valInd = 0;
                        foreach (var saa in sa.Sequences) {
                            sa.Labels.Add(saa.Name == null ? bakNames[valInd] : saa.Name, vals[valInd++]);
                        }
                        sa.ReadCommandData(true);
                        sa.Name = SA.SequenceArchives.Where(x => x.Index == ind).FirstOrDefault().Name;
                        System.IO.File.WriteAllLines(s.FileName, sa.ToText());
                        break;

                    //SBNK.
                    case ".sbnk":
                        SA.Banks.Where(x => x.Index == ind).FirstOrDefault().File.Write(s.FileName);
                        break;

                    //SF2.
                    case ".sf2":
                        var sf2 = SA.Banks.Where(x => x.Index == ind).FirstOrDefault().File.ToSoundFont(SA, SA.Banks.Where(x => x.Index == ind).FirstOrDefault());
                        sf2.Write(s.FileName);
                        break;

                    //DLS.
                    case ".dls":
                        var dls = SA.Banks.Where(x => x.Index == ind).FirstOrDefault().File.ToDLS(SA, SA.Banks.Where(x => x.Index == ind).FirstOrDefault());
                        dls.Write(s.FileName);
                        break;

                    //SWAR.
                    case ".swar":
                        SA.WaveArchives.Where(x => x.Index == ind).FirstOrDefault().File.Write(s.FileName);
                        break;

                    //STRM.
                    case ".strm":
                        SA.Streams.Where(x => x.Index == ind).FirstOrDefault().File.Write(s.FileName);
                        break;

                    //SWAV.
                    case ".swav":
                        Wave swav = new Wave();
                        swav.FromOtherStreamFile(SA.Streams.Where(x => x.Index == ind).FirstOrDefault().File);
                        swav.Write(s.FileName);
                        break;

                }

            }

        }

        /// <summary>
        /// Rename.
        /// </summary>
        public void Rename(object sender, EventArgs e) {

            //Get the new name.
            string newName = Interaction.InputBox("Rename the entry:", "Renamer", tree.SelectedNode.Text.Substring(tree.SelectedNode.Text.IndexOf(' ') + 1));
            int index = GetIdFromNode(tree.SelectedNode);
            if (newName == "") { return; }
            switch (tree.SelectedNode.Parent.Name) {
                case "sequences":
                    if (SA.Sequences.Where(x => x.Name.Equals(newName)).Count() > 0) { MessageBox.Show("An entry of the same name already exists!"); return; }
                    SA.Sequences.Where(x => x.Index == index).FirstOrDefault().Name = newName;
                    break;
                case "sequenceArchives":
                    if (SA.SequenceArchives.Where(x => x.Name.Equals(newName)).Count() > 0) { MessageBox.Show("An entry of the same name already exists!"); return; }
                    SA.SequenceArchives.Where(x => x.Index == index).FirstOrDefault().Name = newName;
                    break;
                case "banks":
                    if (SA.Banks.Where(x => x.Name.Equals(newName)).Count() > 0) { MessageBox.Show("An entry of the same name already exists!"); return; }
                    SA.Banks.Where(x => x.Index == index).FirstOrDefault().Name = newName;
                    break;
                case "waveArchives":
                    if (SA.WaveArchives.Where(x => x.Name.Equals(newName)).Count() > 0) { MessageBox.Show("An entry of the same name already exists!"); return; }
                    SA.WaveArchives.Where(x => x.Index == index).FirstOrDefault().Name = newName;
                    break;
                case "players":
                    if (SA.Players.Where(x => x.Name.Equals(newName)).Count() > 0) { MessageBox.Show("An entry of the same name already exists!"); return; }
                    SA.Players.Where(x => x.Index == index).FirstOrDefault().Name = newName;
                    break;
                case "groups":
                    if (SA.Groups.Where(x => x.Name.Equals(newName)).Count() > 0) { MessageBox.Show("An entry of the same name already exists!"); return; }
                    SA.Groups.Where(x => x.Index == index).FirstOrDefault().Name = newName;
                    break;
                case "streamPlayers":
                    if (SA.StreamPlayers.Where(x => x.Name.Equals(newName)).Count() > 0) { MessageBox.Show("An entry of the same name already exists!"); return; }
                    SA.StreamPlayers.Where(x => x.Index == index).FirstOrDefault().Name = newName;
                    break;
                case "streams":
                    if (SA.Streams.Where(x => x.Name.Equals(newName)).Count() > 0) { MessageBox.Show("An entry of the same name already exists!"); return; }
                    SA.Streams.Where(x => x.Index == index).FirstOrDefault().Name = newName;
                    break;
            }
            if (tree.SelectedNode.Parent.Parent != null) {
                var sar = SA.SequenceArchives.Where(x => x.Index == GetIdFromNode(tree.SelectedNode.Parent)).FirstOrDefault();
                if (sar.File.Sequences.Where(x => x.Name.Equals(newName)).Count() > 0) { MessageBox.Show("An entry of the same name already exists!"); }
                sar.File.Sequences.Where(x => x.Index == index).FirstOrDefault().Name = newName;
            }
            UpdateNodes();
            DoInfoStuff();
        }

        /// <summary>
        /// Delete.
        /// </summary>
        public void Delete(object sender, EventArgs e) {
            switch (tree.SelectedNode.Parent.Name) {
                case "sequences":
                    var x1 = SA.Sequences.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                    for (int i = 0; i < SA.Groups.Count; i++) {
                        while (SA.Groups[i].Entries.Where(x => x.Entry == x1).Count() > 0) {
                            SA.Groups[i].Entries.Remove(SA.Groups[i].Entries.Where(x => x.Entry == x1).FirstOrDefault());
                        }
                    }
                    SA.Sequences.Remove(x1);
                    break;
                case "sequenceArchives":
                    var x2 = SA.SequenceArchives.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                    for (int i = 0; i < SA.Groups.Count; i++) {
                        while (SA.Groups[i].Entries.Where(x => x.Entry == x2).Count() > 0) {
                            SA.Groups[i].Entries.Remove(SA.Groups[i].Entries.Where(x => x.Entry == x2).FirstOrDefault());
                        }
                    }
                    SA.SequenceArchives.Remove(x2);
                    break;
                case "banks":
                    var x3 = SA.Banks.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                    for (int i = 0; i < SA.Groups.Count; i++) {
                        while (SA.Groups[i].Entries.Where(x => x.Entry == x3).Count() > 0) {
                            SA.Groups[i].Entries.Remove(SA.Groups[i].Entries.Where(x => x.Entry == x3).FirstOrDefault());
                        }
                    }
                    SA.Banks.Remove(x3);
                    break;
                case "waveArchives":
                    var x4 = SA.WaveArchives.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                    for (int i = 0; i < SA.Groups.Count; i++) {
                        while (SA.Groups[i].Entries.Where(x => x.Entry == x4).Count() > 0) {
                            SA.Groups[i].Entries.Remove(SA.Groups[i].Entries.Where(x => x.Entry == x4).FirstOrDefault());
                        }
                    }
                    SA.WaveArchives.Remove(x4);
                    break;
                case "players":
                    var x5 = SA.Players.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                    SA.Players.Remove(x5);
                    break;
                case "groups":
                    var x6 = SA.Groups.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                    SA.Groups.Remove(x6);
                    break;
                case "streamPlayers":
                    var x7 = SA.StreamPlayers.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                    SA.StreamPlayers.Remove(x7);
                    break;
                case "streams":
                    var x8 = SA.Streams.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                    SA.Streams.Remove(x8);
                    break;
            }
            UpdateNodes();
            DoInfoStuff();
        }

        /// <summary>
        /// Get the next available Id.
        /// </summary>
        /// <param name="preferredId">The Id.</param>
        /// <param name="maxId">Maximum Id.</param>
        /// <param name="root">Root Id.</param>
        /// <returns>The next available Id.</returns>
        public int GetNextAvailableForwardId(int preferredId, uint maxId, string root) {

            //Id.
            int id = preferredId;

            //Root has Id.
            bool rootHasId() {
                foreach (TreeNode n in tree.Nodes[root].Nodes) {
                    if (n.Text.Contains("[" + id + "]")) {
                        return true;
                    }
                }
                return false;
            }

            //Increment Id.
            while (id <= maxId && rootHasId()) {
                id++;
            }

            //Overflow, start at 0.
            if (id > maxId) {
                id = 0;
                while (id < preferredId && rootHasId()) {
                    id++;
                }
                if (id == preferredId) {
                    MessageBox.Show("There are no more available slots for the item!");
                    return -1;
                }
            }

            //Safe check.
            if (id < 0) {
                return -1;
            }

            //Return the Id.
            return id;

        }

        /// <summary>
        /// Get the next available Id.
        /// </summary>
        /// <param name="preferredId">The Id.</param>
        /// <param name="maxId">Maximum Id.</param>
        /// <param name="root">Root Id.</param>
        /// <returns>The next available Id.</returns>
        public int GetNextAvailablePreviousId(int preferredId, uint maxId, string root) {

            //Id.
            int id = preferredId;

            //Root has Id.
            bool rootHasId() {
                foreach (TreeNode n in tree.Nodes[root].Nodes) {
                    if (n.Text.Contains("[" + id + "]")) {
                        return true;
                    }
                }
                return false;
            }

            //Increment Id.
            while (id >= 0 && rootHasId()) {
                id--;
            }

            //Overflow, start at top.
            if (id < 0) {
                id = (int)maxId;
                while (id > preferredId && rootHasId()) {
                    id--;
                }
                if (id == preferredId) {
                    MessageBox.Show("There are no more available slots for the item!");
                    return -1;
                }
            }

            //Safe check.
            if (id < 0) {
                return -1;
            }

            //Return the Id.
            return id;

        }

        /// <summary>
        /// Root add.
        /// </summary>
        public override void RootAdd() {

            //Sequences.
            if (tree.SelectedNode.Name.Equals("sequences")) {
                int ind = GetNextAvailableForwardId(SA.Sequences.Count > 0 ? SA.Sequences.Last().Index + 1 : 0, SoundArchive.MaxSequenceId, tree.SelectedNode.Name);
                if (ind == -1) {
                    return;
                }
                AddSequence(ind);
                SA.Sequences = SA.Sequences.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

            //Sequence archives.
            else if (tree.SelectedNode.Name.Equals("sequenceArchives")) {
                int ind = GetNextAvailableForwardId(SA.SequenceArchives.Count > 0 ? SA.SequenceArchives.Last().Index + 1 : 0, SoundArchive.MaxSequenceArchiveId, tree.SelectedNode.Name);
                if (ind == -1) {
                    return;
                }
                AddSequenceArchive(ind);
                SA.SequenceArchives = SA.SequenceArchives.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

            //Banks.
            else if (tree.SelectedNode.Name.Equals("banks")) {
                int ind = GetNextAvailableForwardId(SA.Banks.Count > 0 ? SA.Banks.Last().Index + 1 : 0, SoundArchive.MaxBankId, tree.SelectedNode.Name);
                if (ind == -1) {
                    return;
                }
                AddBank(ind);
                SA.Banks = SA.Banks.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

            //Wave archives.
            else if (tree.SelectedNode.Name.Equals("waveArchives")) {
                int ind = GetNextAvailableForwardId(SA.WaveArchives.Count > 0 ? SA.WaveArchives.Last().Index + 1 : 0, SoundArchive.MaxWaveArchiveId, tree.SelectedNode.Name);
                if (ind == -1) {
                    return;
                }
                AddWaveArchive(ind);
                SA.WaveArchives = SA.WaveArchives.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

            //Player.
            else if (tree.SelectedNode.Name.Equals("players")) {
                int ind = GetNextAvailableForwardId(SA.Players.Count > 0 ? SA.Players.Last().Index + 1 : 0, SoundArchive.MaxPlayerId, tree.SelectedNode.Name);
                if (ind == -1) {
                    return;
                }
                AddSequencePlayer(ind);
                SA.Players = SA.Players.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

            //Group.
            else if (tree.SelectedNode.Name.Equals("groups")) {
                int ind = GetNextAvailableForwardId(SA.Groups.Count > 0 ? SA.Groups.Last().Index + 1 : 0, SoundArchive.MaxGroupId, tree.SelectedNode.Name);
                if (ind == -1) {
                    return;
                }
                AddGroup(ind);
                SA.Groups = SA.Groups.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

            //Stream player.
            else if (tree.SelectedNode.Name.Equals("streamPlayers")) {
                int ind = GetNextAvailableForwardId(SA.StreamPlayers.Count > 0 ? SA.StreamPlayers.Last().Index + 1 : 0, SoundArchive.MaxStreamPlayerId, tree.SelectedNode.Name);
                if (ind == -1) {
                    return;
                }
                AddStreamPlayer(ind);
                SA.StreamPlayers = SA.StreamPlayers.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

            //Stream.
            else if (tree.SelectedNode.Name.Equals("streams")) {
                int ind = GetNextAvailableForwardId(SA.Streams.Count > 0 ? SA.Streams.Last().Index + 1 : 0, SoundArchive.MaxStreamId, tree.SelectedNode.Name);
                if (ind == -1) {
                    return;
                }
                AddStream(ind);
                SA.Streams = SA.Streams.OrderBy(x => x.Index).ToList();
                UpdateNodes();
                foreach (TreeNode n in tree.SelectedNode.Nodes) {
                    if (n.Text.Contains("[" + ind + "]")) {
                        tree.SelectedNode = n;
                    }
                }
                DoInfoStuff();
            }

        }

        /// <summary>
        /// Open a sequence archive file.
        /// </summary>
        public void OpenSeqArcFile(object sender, EventArgs e) {
            var f = SA.SequenceArchives.Where(x => x.Index == GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
            SequenceArchiveEditor ed = new SequenceArchiveEditor(f.File, this, f.Name);
            ed.Show();
        }

        /// <summary>
        /// Add a sequence.
        /// </summary>
        /// <param name="index">The index.</param>
        public void AddSequence(int index) {

            //Check for banks.
            if (SA.Banks.Count < 1) {
                MessageBox.Show("There must be at least one bank in order to add a sequence.");
                return;
            }

            //Check for players.
            if (SA.Players.Count < 1) {
                MessageBox.Show("There must be at least one sequence player in order to add a sequence.");
                return;
            }

            //Add the sequence.
            SequenceInfo e = new SequenceInfo();
            e.Bank = SA.Banks[0];
            e.Player = SA.Players[0];
            e.Name = "SEQ_" + index;
            e.Index = index;
            int nameIndex = index;
            while (SA.Sequences.Where(x => x.Name.Equals("SEQ_" + nameIndex)).Count() > 0) {
                e.Name = "SEQ_" + nameIndex++;
            }
            e.File = new Sequence() { RawData = new byte[] { 0xFF }, Labels = new Dictionary<string, uint>() };
            /*var seqs = SA.Sequences.Where(x => x.Index >= index);
            foreach (var s in seqs) {
                s.Index++;
            }*/

            //Insert the sequence.
            SA.Sequences.Add(e);

        }

        /// <summary>
        /// Add a sequence archive at an index.
        /// </summary>
        /// <param name="index">The index.</param>
        public void AddSequenceArchive(int index) {

            //Check for banks.
            if (SA.Banks.Count < 1) {
                MessageBox.Show("There must be at least one bank in order to add a sequence archive.");
                return;
            }

            //Check for players.
            if (SA.Players.Count < 1) {
                MessageBox.Show("There must be at least one sequence player in order to add a sequence archive.");
                return;
            }

            //Add the sequence archive.
            SequenceArchiveInfo e = new SequenceArchiveInfo();
            e.Name = "SEQARC_" + index;
            e.Index = index;
            int nameIndex = index;
            while (SA.Sequences.Where(x => x.Name.Equals("SEQARC_" + nameIndex)).Count() > 0) {
                e.Name = "SEQARC_" + nameIndex++;
            }
            e.File = new SequenceArchive() { RawData = new byte[0], Labels = new Dictionary<string, uint>() };
            /*var seqArcs = SA.SequenceArchives.Where(x => x.Index >= index);
            foreach (var s in seqArcs) {
                s.Index++;
            }*/

            //Insert the sequence archive.
            SA.SequenceArchives.Add(e);

        }

        /// <summary>
        /// Add a bank.
        /// </summary>
        /// <param name="index">Where to add the bank.</param>
        public void AddBank(int index) {

            //Add the bank.
            BankInfo e = new BankInfo();
            e.File = new Bank();
            e.Name = "BANK_" + index;
            e.Index = index;
            int nameIndex = index;
            while (SA.Banks.Where(x => x.Name.Equals("BANK_" + nameIndex)).Count() > 0) {
                e.Name = "BANK_" + nameIndex++;
            }
            /*var banks = SA.Banks.Where(x => x.Index >= index);
            foreach (var s in banks) {
                s.Index++;
            }*/

            //Insert the bank.
            SA.Banks.Add(e);

        }

        /// <summary>
        /// Add a wave archive.
        /// </summary>
        /// <param name="index">Where to add the wave archive.</param>
        public void AddWaveArchive(int index) {

            //Add the wave archive.
            WaveArchiveInfo e = new WaveArchiveInfo();
            e.File = new WaveArchive();
            e.Name = "WAR_" + index;
            e.Index = index;
            int nameIndex = index;
            while (SA.WaveArchives.Where(x => x.Name.Equals("WAR_" + nameIndex)).Count() > 0) {
                e.Name = "WAR_" + nameIndex++;
            }
            /*var wars = SA.WaveArchives.Where(x => x.Index >= index);
            foreach (var s in wars) {
                s.Index++;
            }*/

            //Insert the wave archive.
            SA.WaveArchives.Add(e);

        }

        /// <summary>
        /// Add a sequence player.
        /// </summary>
        /// <param name="index">Where to add the sequence player.</param>
        public void AddSequencePlayer(int index) {

            //Add the sequence player.
            PlayerInfo e = new PlayerInfo();
            e.Name = "PLAYER_" + index;
            e.Index = index;
            e.ChannelFlags = new bool[] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };
            int nameIndex = index;
            while (SA.Players.Where(x => x.Name.Equals("PLAYER_" + nameIndex)).Count() > 0) {
                e.Name = "PLAYER_" + nameIndex++;
            }
            /*var plys = SA.Players.Where(x => x.Index >= index);
            foreach (var s in plys) {
                s.Index++;
            }*/

            //Insert the player.
            SA.Players.Add(e);

        }

        /// <summary>
        /// Add a group.
        /// </summary>
        /// <param name="index">Where to add the group.</param>
        public void AddGroup(int index) {

            //Add the group.
            GroupInfo e = new GroupInfo();
            e.Name = "GROUP_" + index;
            e.Index = index;
            e.Entries = new List<GroupEntry>();
            int nameIndex = index;
            while (SA.Groups.Where(x => x.Name.Equals("GROUP_" + nameIndex)).Count() > 0) {
                e.Name = "GROUP_" + nameIndex++;
            }
            /*var grps = SA.Groups.Where(x => x.Index >= index);
            foreach (var s in grps) {
                s.Index++;
            }*/

            //Insert the group.
            SA.Groups.Add(e);

        }

        /// <summary>
        /// Add a stream player.
        /// </summary>
        /// <param name="index">Where to add the stream player.</param>
        public void AddStreamPlayer(int index) {

            //Add the stream player.
            StreamPlayerInfo e = new StreamPlayerInfo();
            e.Name = "STRM_PLAYER_" + index;
            e.Index = index;
            int nameIndex = index;
            while (SA.StreamPlayers.Where(x => x.Name.Equals("STRM_PLAYER_" + nameIndex)).Count() > 0) {
                e.Name = "STRM_PLAYER_" + nameIndex++;
            }
            /*var stmPlys = SA.StreamPlayers.Where(x => x.Index >= index);
            foreach (var s in stmPlys) {
                s.Index++;
            }*/

            //Insert the stream player.
            SA.StreamPlayers.Add(e);

        }

        /// <summary>
        /// Add a stream.
        /// </summary>
        /// <param name="index">Where to add the stream.</param>
        public void AddStream(int index) {

            //Make sure stream player exists.
            if (SA.StreamPlayers.Count < 1) {
                MessageBox.Show("The must be at least one stream player in order to add a stream.");
                return;
            }

            //Get the file.
            OpenFileDialog o = new OpenFileDialog();
            o.RestoreDirectory = true;
            o.Filter = "Supported Audio Files|*.wav;*.swav;*.strm";
            o.ShowDialog();
            NitroFileLoader.Stream s = new NitroFileLoader.Stream();
            if (o.FileName != "") {
                switch (Path.GetExtension(o.FileName)) {
                    case ".wav":
                        RiffWave r = new RiffWave();
                        r.Read(o.FileName);
                        s.FromOtherStreamFile(r);
                        break;
                    case ".swav":
                        Wave w = new Wave();
                        w.Read(o.FileName);
                        s.FromOtherStreamFile(w);
                        break;
                    case ".strm":
                        s.Read(o.FileName);
                        break;
                }
            } else {
                return;
            }

            //Add the stream.
            StreamInfo e = new StreamInfo();
            e.Name = "STRM_" + index;
            e.Index = index;
            e.Player = SA.StreamPlayers[0];
            e.File = s;
            int nameIndex = index;
            while (SA.Streams.Where(x => x.Name.Equals("STRM_" + nameIndex)).Count() > 0) {
                e.Name = "STRM_" + nameIndex++;
            }
            /*var stms = SA.Streams.Where(x => x.Index >= index);
            foreach (var st in stms) {
                st.Index++;
            }*/

            //Insert the stream.
            SA.Streams.Add(e);

        }

        /// <summary>
        /// Replace a bank with a DLS file.
        /// </summary>
        /// <param name="b">Bank info.</param>
        /// <param name="d">DLS file.</param>
        public void ReplaceBankWithDLS(BankInfo b, DownloadableSounds d) {

            //Get instruments to import.
            List<RiffWave> wavSamples = new List<RiffWave>();
            List<int> instIds = new List<int>();
            List<string> instNames = new List<string>();
            foreach (var i in d.Instruments) {
                if (i.Regions.Count > 0) {
                    instIds.Add((int)(i.InstrumentId + i.BankId * 128));
                    instNames.Add(i.Name);
                    wavSamples.Add(d.Waves[(int)i.Regions[0].WaveId]);
                }
            }

            //Get instruments.
            InstrumentSelector sel = new InstrumentSelector(wavSamples, instIds, instNames);
            sel.ShowDialog();
            instIds = sel.SelectedInstruments;
            if (instIds == null) { return; }

            //Add each instrument.
            List<GotaSoundBank.DLS.Instrument> insts = new List<GotaSoundBank.DLS.Instrument>();
            foreach (var id in instIds) {
                insts.Add(d.Instruments.Where(x => x.InstrumentId == id % 128 && x.BankId == id / 128).FirstOrDefault());
            }

            //Get wave archives.
            wavSamples = new List<RiffWave>();
            List<string> md5s = new List<string>();
            List<WaveArchiveInfo> wars = b.WaveArchives.Where(x => x != null).ToList();
            Dictionary<uint, int> otherWavId = new Dictionary<uint, int>();
            foreach (var inst in insts) {
                foreach (Region r in inst.Regions) {
                    var wav = d.Waves[(int)r.WaveId];
                    wav.Loops = r.Loops;
                    wav.LoopStart = r.LoopStart;
                    wav.LoopEnd = r.LoopLength == 0 ? (uint)wav.Audio.NumSamples : r.LoopStart + r.LoopLength;
                    string md5 = wav.Md5Sum;
                    if (!md5s.Contains(md5)) {
                        wavSamples.Add(wav);
                        md5s.Add(md5);
                        otherWavId.Add(r.WaveId, otherWavId.Count);
                    } else if (!otherWavId.ContainsKey(r.WaveId)) {
                        otherWavId.Add(r.WaveId, md5s.IndexOf(md5));
                    }
                }
            }
            WaveMapper wm = new WaveMapper(wavSamples, wars);
            wm.ShowDialog();
            var warMap = wm.WarMap;
            if (warMap == null) { return; }

            //Add waves.
            Dictionary<int, Tuple<ushort, ushort>> swavMap = new Dictionary<int, Tuple<ushort, ushort>>();
            foreach (var w in wavSamples) {

                //Get wav.
                Wave wav = new Wave();
                wav.FromOtherStreamFile(w);

                //Add wave.
                var war = SA.WaveArchives.Where(x => x.Index == warMap[wavSamples.IndexOf(w)]).FirstOrDefault();
                var md5 = wav.Md5Sum;
                if (war.File.Waves.Where(x => x.Md5Sum.Equals(md5)).Count() < 1) {   
                    war.File.Waves.Add(wav);
                }
                swavMap.Add(wavSamples.IndexOf(w), new Tuple<ushort, ushort>((ushort)b.WaveArchives.ToList().IndexOf(war), (ushort)war.File.Waves.IndexOf(war.File.Waves.Where(x => x.Md5Sum.Equals(md5)).FirstOrDefault())));

            }

            //Add instruments.
            b.File.Instruments = new List<NitroFileLoader.Instrument>();
            foreach (var inst in insts) {

                //Get instrument.
                NitroFileLoader.Instrument i;
                if (inst.Regions.Count < 2 && inst.Regions.Where(x => x.NoteLow == 0).Count() > 0) {
                    i = new DirectInstrument();
                } else if (inst.Regions.Count < 9 && inst.Regions.Where(x => x.NoteLow == 0).Count() > 0) {
                    i = new KeySplitInstrument();
                } else {
                    i = new DrumSetInstrument();
                }

                //Index.
                i.Index = (int)(inst.InstrumentId + inst.BankId * 128);

                //Get regions.
                var regions = inst.Regions.OrderBy(x => x.NoteLow).ToList();
                if (regions[0].NoteLow != 0 && i as DrumSetInstrument != null) {
                    (i as DrumSetInstrument).Min = (byte)regions[0].NoteLow;
                }
                foreach (var r in regions) {

                    //Note info.
                    NoteInfo n = new NoteInfo();

                    //Set stuff up.
                    var dir = swavMap[otherWavId[r.WaveId]];
                    n.WarId = dir.Item1;
                    n.WaveId = dir.Item2;

                    //Parameters.
                    n.InstrumentType = NitroFileLoader.InstrumentType.PCM;
                    n.BaseNote = (byte)(r.RootNote + (r.Tuning / 65536d / 12));
                    n.Key = (Notes)r.NoteHigh;
                    n.Attack = 127;
                    n.Decay = 127;
                    n.Sustain = 127;
                    n.Release = 127;
                    n.Pan = 64;
                    foreach (var a in r.Articulators) {
                        foreach (var c in a.Connections) {
                            if (c.DestinationConnection == DestinationConnection.EG1AttackTime) { 
                                if (c.Scale != int.MinValue) {
                                    n.Attack = Bank.GetNearestTableIndex(Bank.TimecentsToMilliseconds(c.Scale / 65536), Bank.AttackTable);
                                }
                            }
                            if (c.DestinationConnection == DestinationConnection.EG1DecayTime) {
                                if (c.Scale != int.MinValue) {
                                    n.Decay = Bank.GetNearestTableIndex(Bank.TimecentsToMilliseconds(c.Scale / 65536), Bank.MaxReleaseTimes);
                                }
                            }
                            if (c.DestinationConnection == DestinationConnection.EG1SustainLevel) {
                                n.Sustain = Bank.Fraction2Sustain((c.Scale / 65536) / 1000d);
                            }
                            if (c.DestinationConnection == DestinationConnection.EG1ReleaseTime) {
                                if (c.Scale != int.MinValue) {
                                    n.Release= Bank.GetNearestTableIndex(Bank.TimecentsToMilliseconds(c.Scale / 65536), Bank.MaxReleaseTimes);
                                }
                            }
                            if (c.DestinationConnection == DestinationConnection.Pan) {
                                n.Pan = Bank.SetPan(c.Scale / 65536);
                            }
                        }
                    }

                    //Add note info.
                    i.NoteInfo.Add(n);

                }

                //Add instrument.
                b.File.Instruments.Add(i);
            
            }

        }

        /// <summary>
        /// Replace a bank with an SF2 file.
        /// </summary>
        /// <param name="b">Bank info.</param>
        /// <param name="s">SF2 file.</param>
        public void ReplaceBankWithSoundFont(BankInfo b, SoundFont s) {
            ReplaceBankWithDLS(b, new DownloadableSounds(s));
        }

        /// <summary>
        /// Import a file.
        /// </summary>
        public override void importFileToolStripMenuItem_Click(object sender, EventArgs e) {

            //File open test.
            if (!FileTest(sender, e, false, true)) {
                return;
            }

            //Open the file.
            OpenFileDialog o = new OpenFileDialog();
            o.RestoreDirectory = true;
            o.Filter = "Sound Archive|*.sdat;*.dsxe|All Files|*.*";

            if (o.ShowDialog() != DialogResult.OK) {
                return;
            }
            string path = o.FileName;
            File = (IOFile)Activator.CreateInstance(FileType);
            File.Read(path);

        }

        public override void exportFileToolStripMenuItem_Click(object sender, EventArgs e) {

            //File open test.
            if (!FileTest(sender, e, false, true)) {
                return;
            }

            //Export.
            SaveFileDialog s = new SaveFileDialog();
            s.RestoreDirectory = true;
            s.Filter = "Sound Archive|*.sdat;*.dsxe|All Files|*.*";
            s.OverwritePrompt = false;
            if (s.ShowDialog() == DialogResult.OK) {
                SA.Write(s.FileName);
            }

        }

    }

}