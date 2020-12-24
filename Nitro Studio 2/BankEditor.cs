using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GotaSequenceLib;
using GotaSoundIO.IO;
using GotaSoundIO.Sound;
using Kermalis.SoundFont2;
using NitroFileLoader;

namespace NitroStudio2 {

    /// <summary>
    /// Bank editor.
    /// </summary>
    public class BankEditor : EditorBase {

        /// <summary>
        /// The bank file.
        /// </summary>
        public Bank BK => File as Bank;

        /// <summary>
        /// Mixer.
        /// </summary>
        public GotaSequenceLib.Playback.Mixer Mixer = new GotaSequenceLib.Playback.Mixer();

        /// <summary>
        /// Player.
        /// </summary>
        public GotaSequenceLib.Playback.Player Player;

        /// <summary>
        /// Random.
        /// </summary>
        public Random Random = new Random();

        /// <summary>
        /// Create a new bank editor.
        /// </summary>
        /// <param name="mainWindow">Main window.</param>
        public BankEditor(MainWindow mainWindow) : base(typeof(Bank), "Bank", "bnk", "Bank Editor", mainWindow) {
            Init();
        }

        /// <summary>
        /// Create a new bank editor.
        /// </summary>
        /// <param name="fileToOpen">The file to open.</param>
        public BankEditor(string fileToOpen) : base(typeof(Bank), "Bank", "bnk", "Bank Editor", fileToOpen, null) {
            Init();
        }

        /// <summary>
        /// Create a new bank editor.
        /// </summary>
        /// <param name="fileToOpen">The file to open.</param>
        /// <param name="mainWindow">The main window.</param>
        /// <param name="fileName">File name.</param>
        public BankEditor(IOFile fileToOpen, MainWindow mainWindow, string fileName) : base(typeof(Bank), "Bank", "bnk", "Bank Editor", fileToOpen, mainWindow, fileName) {
            Init();
        }

        /// <summary>
        /// Initialize the editor.
        /// </summary>
        public void Init() {
            Icon = Properties.Resources.Bnk;
            tree.Nodes.RemoveAt(0);
            tree.Nodes.Add("root", "Bank", 11, 11);
            Player = new GotaSequenceLib.Playback.Player(Mixer);
            bankRegions.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(bankRegions_EditingControlShowing);
            bankRegions.CellValueChanged += new DataGridViewCellEventHandler(RegionsChanged);
            bankRegions.RowsRemoved += new DataGridViewRowsRemovedEventHandler(RegionsChanged);
            swapAtIndexButton.Click += new EventHandler(SwapIndexButton);
            //insertAtIndexButton.Click += new EventHandler(InsertAtIndexButton);
            drumSetStartRangeBox.ValueChanged += new EventHandler(DrumSetRangeBoxChanged);
            drumSetStartRangeComboBox.SelectedIndexChanged += new EventHandler(DrumSetRangeComboBoxChanged);
            directBox.CheckedChanged += new EventHandler(InstrumentTypeChanged);
            drumSetBox.CheckedChanged += new EventHandler(InstrumentTypeChanged);
            keySplitBox.CheckedChanged += new EventHandler(InstrumentTypeChanged);
            bankRegions.CellContentClick += new DataGridViewCellEventHandler(PlayRegionButtonClick);
            war0ComboBox.SelectedIndexChanged += new EventHandler(war0ComboBoxChanged);
            war1ComboBox.SelectedIndexChanged += new EventHandler(war1ComboBoxChanged);
            war2ComboBox.SelectedIndexChanged += new EventHandler(war2ComboBoxChanged);
            war3ComboBox.SelectedIndexChanged += new EventHandler(war3ComboBoxChanged);
            war0Box.ValueChanged += new EventHandler(war0BoxChanged);
            war1Box.ValueChanged += new EventHandler(war1BoxChanged);
            war2Box.ValueChanged += new EventHandler(war2BoxChanged);
            war3Box.ValueChanged += new EventHandler(war3BoxChanged);
            itemIndexBox.Maximum = 32767;
            bankRegions.Columns[0].Visible = false;
            this.FormClosing += new FormClosingEventHandler(EditorClosing);
            if (MainWindow != null) {
                if (MainWindow.SA != null) {
                    pnlPianoKeys.BringToFront();
                    pnlPianoKeys.Show();
                    bankEditorWars.BringToFront();
                    bankEditorWars.Show();
                    tree.KeyPress += new KeyPressEventHandler(KeyPress);
                    MainWindow.PopulateWaveArchiveBox(MainWindow.SA, war0ComboBox);
                    MainWindow.PopulateWaveArchiveBox(MainWindow.SA, war1ComboBox);
                    MainWindow.PopulateWaveArchiveBox(MainWindow.SA, war2ComboBox);
                    MainWindow.PopulateWaveArchiveBox(MainWindow.SA, war3ComboBox);
                    war0ComboBox.SelectedIndex = 0;
                    war1ComboBox.SelectedIndex = 0;
                    war2ComboBox.SelectedIndex = 0;
                    war3ComboBox.SelectedIndex = 0;
                    war0Box.Value = -1;
                    war1Box.Value = -1;
                    war2Box.Value = -1;
                    war3Box.Value = -1;
                    bankRegions.Columns[0].Visible = true;
                    LoadWaveArchives();
                }
            }
            UpdateNodes();
        }

        /// <summary>
        /// Do info stuff.
        /// </summary>
        public override void DoInfoStuff() {

            //The base.
            base.DoInfoStuff();
            WritingInfo = true;

            //If file open.
            if (!FileOpen || File == null) {
                WritingInfo = false;
                return;
            }

            //Parent is not null.
            if (tree.SelectedNode.Parent != null) {
                bankEditorPanel.BringToFront();
                indexPanel.Show();
                bankEditorPanel.Show();
                PopulateRegionGrid();
                var e = BK.Instruments.Where(x => x.Index == MainWindow.GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                itemIndexBox.Value = e.Index;
                ColorNotes(e as DrumSetInstrument == null ? (byte)0 : (e as DrumSetInstrument).Min, e.NoteInfo);
                switch (e.Type()) {
                    case InstrumentType.DrumSet:
                        drumSetBox.Checked = true;
                        drumSetStartRangeBox.Enabled = true;
                        drumSetStartRangeComboBox.Enabled = true;
                        drumSetRangeStartLabel.Enabled = true;
                        drumSetStartRangeBox.Value = (e as DrumSetInstrument).Min;
                        drumSetStartRangeComboBox.SelectedIndex = (e as DrumSetInstrument).Min;
                        break;
                    case InstrumentType.KeySplit:
                        keySplitBox.Checked = true;
                        drumSetStartRangeBox.Enabled = false;
                        drumSetStartRangeComboBox.Enabled = false;
                        drumSetRangeStartLabel.Enabled = false;
                        drumSetStartRangeBox.Value = 0;
                        drumSetStartRangeComboBox.SelectedIndex = 0;
                        break;
                    default:
                        directBox.Checked = true;
                        drumSetStartRangeBox.Enabled = false;
                        drumSetStartRangeComboBox.Enabled = false;
                        drumSetRangeStartLabel.Enabled = false;
                        drumSetStartRangeBox.Value = 0;
                        drumSetStartRangeComboBox.SelectedIndex = 0;
                        break;
                }
                if (e.NoteInfo.Count > 1) {
                    if (e.NoteInfo.Count > 8) {
                        keySplitBox.Enabled = false;
                    } else {
                        keySplitBox.Enabled = true;
                    }
                    directBox.Enabled = false;
                } else {
                    directBox.Enabled = true;
                    keySplitBox.Enabled = true;
                }
                status.Text = "Editing " + tree.SelectedNode.Text + ".";
            } else {
                indexPanel.Hide();
                noInfoPanel.BringToFront();
                noInfoPanel.Show();
                status.Text = "No Valid Info Selected!";
            }

            //Done with info.
            WritingInfo = false;

        }

        /// <summary>
        /// Update nodes.
        /// </summary>
        public override void UpdateNodes() {

            //Begin update.
            BeginUpdateNodes();

            //File open and not null.
            if (FileOpen && File != null) {

                //Root menu.
                tree.Nodes[0].ContextMenuStrip = rootMenu;

                //Load instruments.
                foreach (var e in BK.Instruments) {
                    switch (e.Type()) {
                        case InstrumentType.PCM:
                            tree.Nodes[0].Nodes.Add("inst" + e.Index, "[" + e.Index + "] PCM Instrument", 14, 14);
                            break;
                        case InstrumentType.PSG:
                            tree.Nodes[0].Nodes.Add("inst" + e.Index, "[" + e.Index + "] PSG Instrument", 17, 17);
                            break;
                        case InstrumentType.Noise:
                            tree.Nodes[0].Nodes.Add("inst" + e.Index, "[" + e.Index + "] Noise Instrument", 18, 18);
                            break;
                        case InstrumentType.DirectPCM:
                            tree.Nodes[0].Nodes.Add("inst" + e.Index, "[" + e.Index + "] Direct PCM Instrument", 14, 14);
                            break;
                        case InstrumentType.Null:
                            tree.Nodes[0].Nodes.Add("inst" + e.Index, "[" + e.Index + "] Null Instrument", 0, 0);
                            break;
                        case InstrumentType.DrumSet:
                            tree.Nodes[0].Nodes.Add("inst" + e.Index, "[" + e.Index + "] Drum Set", 15, 15);
                            break;
                        case InstrumentType.KeySplit:
                            tree.Nodes[0].Nodes.Add("inst" + e.Index, "[" + e.Index + "] Key-Split", 16, 16);
                            break;
                    }
                    tree.Nodes[0].Nodes[tree.Nodes[0].Nodes.Count - 1].ContextMenuStrip = CreateMenuStrip(nodeMenu, new int[] { 0, 1, 4, 5, 6 }, new EventHandler[] { new EventHandler(addAboveToolStripMenuItem1_Click), new EventHandler(addBelowToolStripMenuItem1_Click), new EventHandler(replaceFileToolStripMenuItem_Click), new EventHandler(exportToolStripMenuItem1_Click), new EventHandler(deleteToolStripMenuItem1_Click) });
                }

                //Expand.
                tree.Nodes[0].Expand();

            } else {

                //Remove context menus.
                foreach (TreeNode n in tree.Nodes) {
                    n.ContextMenuStrip = null;
                }

            }

            //End update.
            EndUpdateNodes();

        }

        /// <summary>
        /// Populate the region grid.
        /// </summary>
        public void PopulateRegionGrid() {

            //Clear items.
            bankRegions.Rows.Clear();

            //Add instruments.
            foreach (var e in BK.Instruments.Where(x => x.Index == MainWindow.GetIdFromNode(tree.SelectedNode)).FirstOrDefault().NoteInfo) {

                //Add row.
                bankRegions.Rows.Add(new DataGridViewRow());
                var v = bankRegions.Rows[bankRegions.Rows.Count - 2];
                ((DataGridViewButtonCell)v.Cells[0]).UseColumnTextForButtonValue = true;
                ((DataGridViewComboBoxCell)v.Cells[1]).Value = ((DataGridViewComboBoxCell)v.Cells[1]).Items[(int)e.Key];
                switch (e.InstrumentType) {
                    case InstrumentType.PCM:
                        ((DataGridViewComboBoxCell)v.Cells[2]).Value = "PCM";
                        break;
                    case InstrumentType.PSG:
                        ((DataGridViewComboBoxCell)v.Cells[2]).Value = "PSG";
                        break;
                    case InstrumentType.Noise:
                        ((DataGridViewComboBoxCell)v.Cells[2]).Value = "Noise";
                        break;
                    case InstrumentType.DirectPCM:
                        ((DataGridViewComboBoxCell)v.Cells[2]).Value = "Direct PCM";
                        break;
                    case InstrumentType.Null:
                        ((DataGridViewComboBoxCell)v.Cells[2]).Value = "Null";
                        break;
                }
                ((DataGridViewTextBoxCell)v.Cells[3]).Value = e.WaveId;
                ((DataGridViewTextBoxCell)v.Cells[4]).Value = e.WarId;
                ((DataGridViewComboBoxCell)v.Cells[5]).Value = ((DataGridViewComboBoxCell)v.Cells[5]).Items[e.BaseNote];
                ((DataGridViewTextBoxCell)v.Cells[6]).Value = e.Attack;
                ((DataGridViewTextBoxCell)v.Cells[7]).Value = e.Decay;
                ((DataGridViewTextBoxCell)v.Cells[8]).Value = e.Sustain;
                ((DataGridViewTextBoxCell)v.Cells[9]).Value = e.Release;
                ((DataGridViewTextBoxCell)v.Cells[10]).Value = e.Pan;

            }

        }

        /// <summary>
        /// Load wave archives.
        /// </summary>
        public void LoadWaveArchives() {
            if (MainWindow == null) {
                return;
            }
            var riffs = new RiffWave[4][];
            var w0 = MainWindow.SA.WaveArchives.Where(x => x.Index == (int)war0Box.Value).FirstOrDefault();
            if (w0 != null) {
                riffs[0] = w0.File.GetWaves();
            }
            var w1 = MainWindow.SA.WaveArchives.Where(x => x.Index == (int)war1Box.Value).FirstOrDefault();
            if (w1 != null) {
                riffs[1] = w1.File.GetWaves();
            }
            var w2 = MainWindow.SA.WaveArchives.Where(x => x.Index == (int)war2Box.Value).FirstOrDefault();
            if (w2 != null) {
                riffs[2] = w2.File.GetWaves();
            }
            var w3 = MainWindow.SA.WaveArchives.Where(x => x.Index == (int)war3Box.Value).FirstOrDefault();
            if (w3 != null) {
                riffs[3] = w3.File.GetWaves();
            }
            Player.PrepareForSong(new GotaSequenceLib.Playback.PlayableBank[] { BK }, riffs);
        }

        /// <summary>
        /// On the piano press.
        /// </summary>
        public override void OnPianoPress() {
            if (tree.SelectedNode.Parent == null) { return; }
            currentNote.Text = "Playing Note " + NoteDown.ToString() + " (" + (int)(NoteDown) + ").";
            Player.Stop();
            Player.Banks[0] = BK;
            Player.LoadSong(new List<GotaSequenceLib.SequenceCommand>() { new GotaSequenceLib.SequenceCommand() { CommandType = GotaSequenceLib.SequenceCommands.ProgramChange, Parameter = (uint)MainWindow.GetIdFromNode(tree.SelectedNode) }, new GotaSequenceLib.SequenceCommand() { CommandType = GotaSequenceLib.SequenceCommands.Note, Parameter = new GotaSequenceLib.NoteParameter() { Note = NoteDown, Length = 0xFFF, Velocity = 127 } }, new SequenceCommand() { CommandType = SequenceCommands.Fin } });
            Player.Play();
        }

        /// <summary>
        /// On the piano release.
        /// </summary>
        public override void OnPianoRelease() {
            Player.Stop();
            currentNote.Text = "";
        }

        private void bankRegions_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {
            e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress);
            if (bankRegions.CurrentCell.ColumnIndex == 2 || bankRegions.CurrentCell.ColumnIndex == 3 || bankRegions.CurrentCell.ColumnIndex == 5 || bankRegions.CurrentCell.ColumnIndex == 6 || bankRegions.CurrentCell.ColumnIndex == 7 || bankRegions.CurrentCell.ColumnIndex == 8 || bankRegions.CurrentCell.ColumnIndex == 9) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null) {
                    tb.KeyPress += new KeyPressEventHandler(Column_KeyPress);
                }
            }
        }

        private void Column_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Regions changed.
        /// </summary>
        public void RegionsChanged(object sender, EventArgs e) {

            //Writing info.
            if (WritingInfo) {
                return;
            }
            WritingInfo = true;

            //Current intrument.
            var inst = BK.Instruments.Where(x => x.Index == MainWindow.GetIdFromNode(tree.SelectedNode)).FirstOrDefault();

            //Read regions.
            List<NoteInfo> regions = new List<NoteInfo>();
            for (int i = 1; i < bankRegions.Rows.Count; i++) {

                //Get the cells.
                var endNoteCell = (DataGridViewComboBoxCell)bankRegions.Rows[i - 1].Cells[1];
                var instrumentTypeCell = (DataGridViewComboBoxCell)bankRegions.Rows[i - 1].Cells[2];
                var waveCell = (DataGridViewTextBoxCell)bankRegions.Rows[i - 1].Cells[3];
                var warCell = (DataGridViewTextBoxCell)bankRegions.Rows[i - 1].Cells[4];
                var baseNote = (DataGridViewComboBoxCell)bankRegions.Rows[i - 1].Cells[5];
                var attackCell = (DataGridViewTextBoxCell)bankRegions.Rows[i - 1].Cells[6];
                var decayCell = (DataGridViewTextBoxCell)bankRegions.Rows[i - 1].Cells[7];
                var sustainCell = (DataGridViewTextBoxCell)bankRegions.Rows[i - 1].Cells[8];
                var releaseCell = (DataGridViewTextBoxCell)bankRegions.Rows[i - 1].Cells[9];
                var panCell = (DataGridViewTextBoxCell)bankRegions.Rows[i - 1].Cells[10];

                //Test.
                if (endNoteCell.Value == null || endNoteCell.Value.ToString() == "") {
                    WritingInfo = false;
                    endNoteCell.Value = endNoteCell.Items[127];
                    return;
                }
                if (instrumentTypeCell.Value == null || instrumentTypeCell.Value.ToString() == "") {
                    WritingInfo = false;
                    instrumentTypeCell.Value = instrumentTypeCell.Items[0];
                    return;
                }
                if (waveCell.Value == null || waveCell.Value.ToString() == "") {
                    WritingInfo = false;
                    waveCell.Value = 0;
                    return;
                }
                if (warCell.Value == null || warCell.Value.ToString() == "") {
                    WritingInfo = false;
                    warCell.Value = 0;
                    return;
                }
                if (baseNote.Value == null || baseNote.Value.ToString() == "") {
                    WritingInfo = false;
                    baseNote.Value = baseNote.Items[60];
                    return;
                }
                if (attackCell.Value == null || attackCell.Value.ToString() == "") {
                    WritingInfo = false;
                    attackCell.Value = 127;
                    return;
                }
                if (decayCell.Value == null || decayCell.Value.ToString() == "") {
                    WritingInfo = false;
                    decayCell.Value = 127;
                    return;
                }
                if (sustainCell.Value == null || sustainCell.Value.ToString() == "") {
                    WritingInfo = false;
                    sustainCell.Value = 127;
                    return;
                }
                if (releaseCell.Value == null || releaseCell.Value.ToString() == "") {
                    WritingInfo = false;
                    releaseCell.Value = 127;
                    return;
                }
                if (panCell.Value == null || panCell.Value.ToString() == "") {
                    WritingInfo = false;
                    panCell.Value = 64;
                    return;
                }

                //Get the info.
                NoteInfo n = new NoteInfo();
                n.Key = (Notes)Enum.Parse(typeof(Notes), ((string)endNoteCell.Value).Split(' ')[0]);
                switch ((string)instrumentTypeCell.Value) {
                    case "PCM":
                        n.InstrumentType = InstrumentType.PCM;
                        break;
                    case "PSG":
                        n.InstrumentType = InstrumentType.PSG;
                        break;
                    case "Noise":
                        n.InstrumentType = InstrumentType.Noise;
                        break;
                    case "Direct PCM":
                        n.InstrumentType = InstrumentType.DirectPCM;
                        break;
                    case "Null":
                        n.InstrumentType = InstrumentType.Null;
                        break;
                }
                if (int.Parse(waveCell.Value.ToString()) > 65535) {
                    waveCell.Value = "65535";
                }
                if (n.InstrumentType == InstrumentType.PSG && int.Parse(waveCell.Value.ToString()) > 6) {
                    waveCell.Value = 6;
                }
                n.WaveId = ushort.Parse(waveCell.Value.ToString());
                if (int.Parse(warCell.Value.ToString()) > 65535) {
                    warCell.Value = "65535";
                }
                n.WarId = ushort.Parse(warCell.Value.ToString());
                n.BaseNote = (byte)Enum.Parse(typeof(Notes), baseNote.Value.ToString().Split(' ')[0]);
                if (int.Parse(attackCell.Value.ToString()) > 127) {
                    attackCell.Value = "127";
                }
                n.Attack = byte.Parse(attackCell.Value.ToString());
                if (int.Parse(decayCell.Value.ToString()) > 127) {
                    decayCell.Value = "127";
                }
                n.Decay = byte.Parse(decayCell.Value.ToString());
                if (int.Parse(sustainCell.Value.ToString()) > 127) {
                    sustainCell.Value = "127";
                }
                n.Sustain = byte.Parse(sustainCell.Value.ToString());
                if (int.Parse(releaseCell.Value.ToString()) > 127) {
                    releaseCell.Value = "127";
                }
                n.Release = byte.Parse(releaseCell.Value.ToString());
                if (int.Parse(panCell.Value.ToString()) > 127) {
                    panCell.Value = "127";
                }
                n.Pan = byte.Parse(panCell.Value.ToString());
                regions.Add(n);

            }

            //Set note info.
            inst.NoteInfo.Clear();
            inst.NoteInfo = regions;

            //Add fallback region.
            if (inst.NoteInfo.Count < 1) {
                inst.NoteInfo.Add(new NoteInfo() { Attack = 127, BaseNote = 60, Decay = 127, InstrumentType = InstrumentType.PCM, Key = Notes.gn9, Pan = 64, Release = 127, Sustain = 127, WarId = 0, WaveId = 0 });
                UpdateNodes();
                DoInfoStuff();
            }

            //Set type.
            if (regions.Count > 1) {
                directBox.Enabled = false;
            } else {
                directBox.Enabled = true;
            }

            //Key split.
            if (regions.Count > 8) {
                keySplitBox.Enabled = false;
            } else {
                keySplitBox.Enabled = true;
            }

            //Drum set override.
            if (regions.Count > 8 && inst.Type() != InstrumentType.DrumSet) {
                BK.Instruments[BK.Instruments.IndexOf(inst)] = new DrumSetInstrument() { Min = 0, Index = inst.Index, NoteInfo = regions, Order = inst.Order };
                drumSetBox.Checked = true;
                drumSetRangeStartLabel.Enabled = true;
                drumSetStartRangeBox.Enabled = true;
                drumSetStartRangeComboBox.Enabled = true;
                drumSetStartRangeComboBox.SelectedIndex = 0;
                drumSetStartRangeBox.Value = 0;
                UpdateNodes();
                return;
            }

            //Key split override.
            if (regions.Count > 1 && inst as DirectInstrument != null) {
                BK.Instruments[BK.Instruments.IndexOf(inst)] = new KeySplitInstrument() { Index = inst.Index, NoteInfo = regions, Order = inst.Order };
                keySplitBox.Checked = true;
                UpdateNodes();
            }

            //Colors.
            ColorNotes(inst as DrumSetInstrument == null ? (byte)0 : (inst as DrumSetInstrument).Min, inst.NoteInfo);

            //Writing info.
            WritingInfo = false;
            UpdateNodes();

        }

        /// <summary>
        /// Instrument type changed.
        /// </summary>
        public void TypeChanged(object sender, EventArgs e) {

            //Writing info.
            if (WritingInfo) {
                return;
            }
            WritingInfo = true;

            //Get instrument.
            var inst = BK.Instruments.Where(x => x.Index == MainWindow.GetIdFromNode(tree.SelectedNode)).FirstOrDefault();

            //Set instrument type.
            if (directBox.Checked) {
                drumSetRangeStartLabel.Enabled = false;
                drumSetStartRangeBox.Enabled = false;
                drumSetStartRangeComboBox.Enabled = false;
                BK.Instruments[BK.Instruments.IndexOf(inst)] = new DirectInstrument() { Index = inst.Index, NoteInfo = inst.NoteInfo, Order = inst.Order };
            } else if (drumSetBox.Checked) {
                drumSetRangeStartLabel.Enabled = true;
                drumSetStartRangeBox.Enabled = true;
                drumSetStartRangeComboBox.Enabled = true;
                BK.Instruments[BK.Instruments.IndexOf(inst)] = new DrumSetInstrument() { Index = inst.Index, NoteInfo = inst.NoteInfo, Order = inst.Order, Min = (byte)drumSetStartRangeBox.Value };
            } else {
                drumSetRangeStartLabel.Enabled = false;
                drumSetStartRangeBox.Enabled = false;
                drumSetStartRangeComboBox.Enabled = false;
                BK.Instruments[BK.Instruments.IndexOf(inst)] = new KeySplitInstrument() { Index = inst.Index, NoteInfo = inst.NoteInfo, Order = inst.Order };
            }

            //Update nodes.
            UpdateNodes();

            //Writing info.
            WritingInfo = false;

        }

        /// <summary>
        /// Key press.
        /// </summary>
        public void KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == ' ' && tree.SelectedNode.Parent != null) {
                if (tree.SelectedNode.Parent == null) { return; }
                Player.Stop();
                Player.Banks[0] = BK;
                Player.LoadSong(new List<GotaSequenceLib.SequenceCommand>() { new GotaSequenceLib.SequenceCommand() { CommandType = GotaSequenceLib.SequenceCommands.ProgramChange, Parameter = (uint)MainWindow.GetIdFromNode(tree.SelectedNode) }, new GotaSequenceLib.SequenceCommand() { CommandType = GotaSequenceLib.SequenceCommands.Note, Parameter = new GotaSequenceLib.NoteParameter() { Note = Notes.cn4, Length = 48 * 2, Velocity = 127 } }, new SequenceCommand() { CommandType = SequenceCommands.Fin } });
                Player.Play();
            }
        }

        /// <summary>
        /// Root add.
        /// </summary>
        public override void RootAdd() {

            //Get new index.
            int index = 0;
            try { index = BK.Instruments.Last().Index + 1; } catch { }
            if (index > 0xFFFF) {
                for (int i = 0; i < 0xFFFF; i++) {
                    if (BK.Instruments.Where(x => x.Index == i).Count() < 1) {
                        index = i;
                        break;
                    }
                }
                MessageBox.Show("No available slots left!");
            }

            //Add the instrument.
            BK.Instruments.Add(new DirectInstrument() { Index = index, NoteInfo = new List<NoteInfo>() { new NoteInfo() { Key = Notes.gn9 } }, Order = index });
            BK.Instruments = BK.Instruments.OrderBy(x => x.Index).ToList();
            UpdateNodes();
            DoInfoStuff();

        }

        /// <summary>
        /// Add above.
        /// </summary>
        public override void NodeAddAbove() {

            //Get the instrument.
            var inst = BK.Instruments.Where(x => x.Index == MainWindow.GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
            if (BK.Instruments.Where(x => x.Index == inst.Index - 1).Count() > 0 || inst.Index == 0) {
                foreach (var i in BK.Instruments) {
                    if (i.Index >= inst.Index && i != inst) {
                        i.Index++;
                    }
                }
            }

            //Add instrument.
            BK.Instruments.Add(new DirectInstrument() { Index = inst.Index - 1, NoteInfo = new List<NoteInfo>() { new NoteInfo() { Key = Notes.gn9 } }, Order = inst.Order - 1 });
            BK.Instruments = BK.Instruments.OrderBy(x => x.Index).ToList();

            //Update.
            UpdateNodes();
            DoInfoStuff();

        }

        /// <summary>
        /// Add below.
        /// </summary>
        public override void NodeAddBelow() {

            //Get the instrument.
            var inst = BK.Instruments.Where(x => x.Index == MainWindow.GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
            if (BK.Instruments.Where(x => x.Index == inst.Index + 1).Count() > 0 || inst.Index == 0) {
                foreach (var i in BK.Instruments) {
                    if (i.Index >= inst.Index && i != inst) {
                        i.Index++;
                    }
                }
            }

            //Add instrument.
            BK.Instruments.Add(new DirectInstrument() { Index = inst.Index + 1, NoteInfo = new List<NoteInfo>() { new NoteInfo() { Key = Notes.gn9 } }, Order = inst.Order - 1 });
            BK.Instruments = BK.Instruments.OrderBy(x => x.Index).ToList();

            //Update.
            UpdateNodes();
            DoInfoStuff();

        }

        /// <summary>
        /// Replace the instrument.
        /// </summary>
        public override void NodeReplace() {

            //Replace the instrument.
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Nitro Studio Instrument|*.ns2i;*.nist";
            o.RestoreDirectory = true;
            o.ShowDialog();
            if (o.FileName != "") {
                switch (Path.GetExtension(o.FileName)) {
                    case ".ns2i":
                        NitroStudio2Instrument i = new NitroStudio2Instrument();
                        i.Read(o.FileName);
                        i.WriteInstrument(BK, MainWindow.GetIdFromNode(tree.SelectedNode), MainWindow != null ? MainWindow.SA : null, war0Box.Value == -1 ? (ushort)0xFFFF : (ushort)war0Box.Value, war1Box.Value == -1 ? (ushort)0xFFFF : (ushort)war1Box.Value, war2Box.Value == -1 ? (ushort)0xFFFF : (ushort)war2Box.Value, war3Box.Value == -1 ? (ushort)0xFFFF : (ushort)war3Box.Value);
                        LoadWaveArchives();
                        break;
                    case ".nist":
                        NitroStudioInstrument s = new NitroStudioInstrument();
                        s.Read(o.FileName);
                        if (s.Inst == null) { return; }
                        s.Inst.Index = MainWindow.GetIdFromNode(tree.SelectedNode);
                        BK.Instruments[BK.Instruments.IndexOf(BK.Instruments.Where(x => x.Index == MainWindow.GetIdFromNode(tree.SelectedNode)).FirstOrDefault())] = s.Inst;
                        break;
                }
                UpdateNodes();
                DoInfoStuff();
            }

        }

        /// <summary>
        /// Export.
        /// </summary>
        public override void NodeExport() {

            //Export the instrument.
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "Nitro Studio Instrument|*.ns2i;*.nist";
            s.RestoreDirectory = true;
            s.FileName = "Instrument " + MainWindow.GetIdFromNode(tree.SelectedNode) + ".ns2i";
            s.ShowDialog();
            if (s.FileName != "") {
                switch (Path.GetExtension(s.FileName)) {
                    case ".ns2i":
                        NitroStudio2Instrument i = new NitroStudio2Instrument(BK.Instruments.Where(x => x.Index == MainWindow.GetIdFromNode(tree.SelectedNode)).FirstOrDefault(), MainWindow != null ? MainWindow.SA : null, war0Box.Value == -1 ? (ushort)0xFFFF : (ushort)war0Box.Value, war1Box.Value == -1 ? (ushort)0xFFFF : (ushort)war1Box.Value, war2Box.Value == -1 ? (ushort)0xFFFF : (ushort)war2Box.Value, war3Box.Value == -1 ? (ushort)0xFFFF : (ushort)war3Box.Value);
                        i.Write(s.FileName);
                        break;
                    case ".nist":
                        NitroStudioInstrument n = new NitroStudioInstrument() { Inst = BK.Instruments.Where(x => x.Index == MainWindow.GetIdFromNode(tree.SelectedNode)).FirstOrDefault() };
                        n.Write(s.FileName);
                        break;
                }

            }

        }

        /// <summary>
        /// Delete the node.
        /// </summary>
        public override void NodeDelete() {
            BK.Instruments.Remove(BK.Instruments.Where(x => x.Index == MainWindow.GetIdFromNode(tree.SelectedNode)).FirstOrDefault());
            UpdateNodes();
            DoInfoStuff();
        }

        /// <summary>
        /// Swap index button.
        /// </summary>
        public void SwapIndexButton(object sender, EventArgs e) {

            //See if instrument exists.
            bool instExists = BK.Instruments.Where(x => x.Index == itemIndexBox.Value).Count() > 0;
            var inst = BK.Instruments.Where(x => x.Index == MainWindow.GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
            if (instExists) {
                var inst2 = BK.Instruments.Where(x => x.Index == itemIndexBox.Value).FirstOrDefault();
                inst2.Index = inst.Index;
            }
            inst.Index = (int)itemIndexBox.Value;
            BK.Instruments = BK.Instruments.OrderBy(x => x.Index).ToList();
            tree.SelectedNode = tree.Nodes[0].Nodes[BK.Instruments.IndexOf(inst)];
            UpdateNodes();
            DoInfoStuff();

        }

        /// <summary>
        /// Insert index button.
        /// </summary>
        public void InsertAtIndexButton(object sender, EventArgs e) {

            //See if instrument exists.
            var inst = BK.Instruments.Where(x => x.Index == MainWindow.GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
            foreach (var i in BK.Instruments) {
                if (i.Index >= (int)itemIndexBox.Value) {
                    i.Index++;
                }
            }
            inst.Index = (int)itemIndexBox.Value;
            BK.Instruments = BK.Instruments.OrderBy(x => x.Index).ToList();
            tree.SelectedNode = tree.Nodes[0].Nodes[BK.Instruments.IndexOf(inst)];
            UpdateNodes();
            DoInfoStuff();

        }

        public void DrumSetRangeBoxChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                WritingInfo = true;
                drumSetStartRangeComboBox.SelectedIndex = (int)drumSetStartRangeBox.Value;
                (BK.Instruments.Where(x => x.Index == MainWindow.GetIdFromNode(tree.SelectedNode)).FirstOrDefault() as DrumSetInstrument).Min = (byte)drumSetStartRangeBox.Value;
                WritingInfo = false;
            }
        }

        public void DrumSetRangeComboBoxChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                WritingInfo = true;
                drumSetStartRangeBox.Value = drumSetStartRangeComboBox.SelectedIndex;
                (BK.Instruments.Where(x => x.Index == MainWindow.GetIdFromNode(tree.SelectedNode)).FirstOrDefault() as DrumSetInstrument).Min = (byte)drumSetStartRangeBox.Value;
                WritingInfo = false;
            }
        }

        public void InstrumentTypeChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                var inst = BK.Instruments.Where(x => x.Index == MainWindow.GetIdFromNode(tree.SelectedNode)).FirstOrDefault();
                if (directBox.Checked) {
                    BK.Instruments[BK.Instruments.IndexOf(inst)] = new DirectInstrument() { Index = inst.Index, Order = inst.Order, NoteInfo = inst.NoteInfo };
                    drumSetRangeStartLabel.Enabled = false;
                    drumSetStartRangeBox.Enabled = false;
                    drumSetStartRangeComboBox.Enabled = false;
                } else if (drumSetBox.Checked) {
                    BK.Instruments[BK.Instruments.IndexOf(inst)] = new DrumSetInstrument() { Index = inst.Index, Order = inst.Order, NoteInfo = inst.NoteInfo, Min = (byte)drumSetStartRangeBox.Value };
                    drumSetRangeStartLabel.Enabled = true;
                    drumSetStartRangeBox.Enabled = true;
                    drumSetStartRangeComboBox.Enabled = true;
                } else {
                    BK.Instruments[BK.Instruments.IndexOf(inst)] = new KeySplitInstrument() { Index = inst.Index, Order = inst.Order, NoteInfo = inst.NoteInfo };
                    drumSetRangeStartLabel.Enabled = false;
                    drumSetStartRangeBox.Enabled = false;
                    drumSetStartRangeComboBox.Enabled = false;
                }
                UpdateNodes();
                DoInfoStuff();
            }
        }

        /// <summary>
        /// Play region button.
        /// </summary>
        public void PlayRegionButtonClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex != 0 && e.RowIndex >= 0) {
                return;
            }
            if (MainWindow != null && MainWindow.SA != null) {
                if (tree.SelectedNode.Parent == null) { return; }
                int regionInd = e.RowIndex;
                if (regionInd > BK.Instruments.Where(x => x.Index == MainWindow.GetIdFromNode(tree.SelectedNode)).FirstOrDefault().NoteInfo.Count - 1) {
                    return;
                }
                byte prevNote = 0;
                if (BK.Instruments.Where(x => x.Index == MainWindow.GetIdFromNode(tree.SelectedNode)).FirstOrDefault() as DrumSetInstrument != null) {
                    prevNote = (BK.Instruments.Where(x => x.Index == MainWindow.GetIdFromNode(tree.SelectedNode)).FirstOrDefault() as DrumSetInstrument).Min;
                }
                byte nextNote = (byte)BK.Instruments.Where(x => x.Index == MainWindow.GetIdFromNode(tree.SelectedNode)).FirstOrDefault().NoteInfo[regionInd].Key;
                if (regionInd > 0) {
                    prevNote = (byte)BK.Instruments.Where(x => x.Index == MainWindow.GetIdFromNode(tree.SelectedNode)).FirstOrDefault().NoteInfo[regionInd - 1].Key;
                }
                Notes note = (Notes)((byte)((prevNote + nextNote) / 2));
                Player.Stop();
                Player.Banks[0] = BK;
                Player.LoadSong(new List<GotaSequenceLib.SequenceCommand>() { new GotaSequenceLib.SequenceCommand() { CommandType = GotaSequenceLib.SequenceCommands.ProgramChange, Parameter = (uint)MainWindow.GetIdFromNode(tree.SelectedNode) }, new GotaSequenceLib.SequenceCommand() { CommandType = GotaSequenceLib.SequenceCommands.Note, Parameter = new GotaSequenceLib.NoteParameter() { Note = note, Length = 48 * 2, Velocity = 127 } }, new SequenceCommand() { CommandType = SequenceCommands.Fin } });
                Player.Play();
            }
        }

        /// <summary>
        /// Color notes.
        /// </summary>
        /// <param name="n">The note info.</param>
        public void ColorNotes(byte start, List<NoteInfo> n) {

            //Color.
            int num = 0;
            foreach (var e in n) {
                if (num == 0) {
                    ColorRegion(Color.White, start, (byte)e.Key);               
                } else {
                    ColorRegion(Color.FromArgb(Random.Next(75, 256), Random.Next(75, 256), Random.Next(75, 256)), start, (byte)e.Key);
                }
                start = (byte)(e.Key + 1);
                num++;
            }

        }

        /// <summary>
        /// Changed.
        /// </summary>
        public void war0BoxChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                WritingInfo = true;
                MainWindow.SetWaveArchiveIndex(MainWindow.SA, war0ComboBox, (ushort)(war0Box.Value == -1 ? 0xFFFF : war0Box.Value));
                WritingInfo = false;
                var w0 = MainWindow.SA.WaveArchives.Where(x => x.Index == (int)war0Box.Value).FirstOrDefault();
                if (w0 != null) {
                    Player.WaveArchives[0] = w0.File.GetWaves();
                }
            }
        }

        /// <summary>
        /// Changed.
        /// </summary>
        public void war1BoxChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                WritingInfo = true;
                MainWindow.SetWaveArchiveIndex(MainWindow.SA, war1ComboBox, (ushort)(war1Box.Value == -1 ? 0xFFFF : war1Box.Value));
                WritingInfo = false;
                var w1 = MainWindow.SA.WaveArchives.Where(x => x.Index == (int)war1Box.Value).FirstOrDefault();
                if (w1 != null) {
                    Player.WaveArchives[1] = w1.File.GetWaves();
                }
            }
        }

        /// <summary>
        /// Changed.
        /// </summary>
        public void war2BoxChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                WritingInfo = true;
                MainWindow.SetWaveArchiveIndex(MainWindow.SA, war2ComboBox, (ushort)(war2Box.Value == -1 ? 0xFFFF : war2Box.Value));
                WritingInfo = false;
                var w2 = MainWindow.SA.WaveArchives.Where(x => x.Index == (int)war2Box.Value).FirstOrDefault();
                if (w2 != null) {
                    Player.WaveArchives[2] = w2.File.GetWaves();
                }
            }
        }

        /// <summary>
        /// Changed.
        /// </summary>
        public void war3BoxChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                WritingInfo = true;
                MainWindow.SetWaveArchiveIndex(MainWindow.SA, war3ComboBox, (ushort)(war3Box.Value == -1 ? 0xFFFF : war3Box.Value));
                WritingInfo = false;
                var w3 = MainWindow.SA.WaveArchives.Where(x => x.Index == (int)war3Box.Value).FirstOrDefault();
                if (w3 != null) {
                    Player.WaveArchives[3] = w3.File.GetWaves();
                }
            }
        }

        /// <summary>
        /// Changed.
        /// </summary>
        public void war0ComboBoxChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                ushort val = (ushort)war0ComboBox.SelectedIndex;
                if (val == 0) {
                    val = 0xFFFF;
                } else if (val == 1) {
                    return;
                } else {
                    val = ushort.Parse(((string)war0ComboBox.SelectedItem).Split('[')[1].Split(']')[0]);
                }
                WritingInfo = true;
                MainWindow.SetWaveArchiveIndex(MainWindow.SA, war0Box, val);
                WritingInfo = false;
                var w0 = MainWindow.SA.WaveArchives.Where(x => x.Index == (int)war0Box.Value).FirstOrDefault();
                if (w0 != null) {
                    Player.WaveArchives[0] = w0.File.GetWaves();
                }
            }
        }

        /// <summary>
        /// Changed.
        /// </summary>
        public void war1ComboBoxChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                ushort val = (ushort)war1ComboBox.SelectedIndex;
                if (val == 0) {
                    val = 0xFFFF;
                } else if (val == 1) {
                    return;
                } else {
                    val = ushort.Parse(((string)war1ComboBox.SelectedItem).Split('[')[1].Split(']')[0]);
                }
                WritingInfo = true;
                MainWindow.SetWaveArchiveIndex(MainWindow.SA, war1Box, val);
                WritingInfo = false;
                var w1 = MainWindow.SA.WaveArchives.Where(x => x.Index == (int)war1Box.Value).FirstOrDefault();
                if (w1 != null) {
                    Player.WaveArchives[1] = w1.File.GetWaves();
                }
            }
        }

        /// <summary>
        /// Changed.
        /// </summary>
        public void war2ComboBoxChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                ushort val = (ushort)war2ComboBox.SelectedIndex;
                if (val == 0) {
                    val = 0xFFFF;
                } else if (val == 1) {
                    return;
                } else {
                    val = ushort.Parse(((string)war2ComboBox.SelectedItem).Split('[')[1].Split(']')[0]);
                }
                WritingInfo = true;
                MainWindow.SetWaveArchiveIndex(MainWindow.SA, war2Box, val);
                WritingInfo = false;
                var w2 = MainWindow.SA.WaveArchives.Where(x => x.Index == (int)war2Box.Value).FirstOrDefault();
                if (w2 != null) {
                    Player.WaveArchives[2] = w2.File.GetWaves();
                }
            }
        }

        /// <summary>
        /// Changed.
        /// </summary>
        public void war3ComboBoxChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                ushort val = (ushort)war3ComboBox.SelectedIndex;
                if (val == 0) {
                    val = 0xFFFF;
                } else if (val == 1) {
                    return;
                } else {
                    val = ushort.Parse(((string)war3ComboBox.SelectedItem).Split('[')[1].Split(']')[0]);
                }
                WritingInfo = true;
                MainWindow.SetWaveArchiveIndex(MainWindow.SA, war3Box, val);
                WritingInfo = false;
                var w3 = MainWindow.SA.WaveArchives.Where(x => x.Index == (int)war3Box.Value).FirstOrDefault();
                if (w3 != null) {
                    Player.WaveArchives[3] = w3.File.GetWaves();
                }
            }
        }

        /// <summary>
        /// Closing.
        /// </summary>
        public void EditorClosing(object sender, EventArgs e) {
            Mixer.Dispose();
            Player.Dispose();
        }

    }

}
