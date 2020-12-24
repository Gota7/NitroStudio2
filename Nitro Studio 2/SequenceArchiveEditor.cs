using GotaSequenceLib;
using GotaSequenceLib.Playback;
using GotaSoundIO.IO;
using GotaSoundIO.Sound;
using NitroFileLoader;
using ScintillaNET;
using ScintillaNET_FindReplaceDialog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GotaSequenceLib.Playback.Player;

namespace NitroStudio2 {

    /// <summary>
    /// Sequence editor.
    /// </summary>
    public class SequenceArchiveEditor : EditorBase {

        public Player Player;
        public Mixer Mixer = new Mixer();
        private const int BACK_COLOR = 0x2F2F2F;
        private const int FORE_COLOR = 0xB7B7B7;
        public SequenceArchive SA => File as SequenceArchive;
        int prevLine = -1;
        bool prevLineBlank = true;
        int seqTableStartLine = -1;
        private FindReplace MyFindReplace;
        public bool PositionBarFree = true;
        public Timer Timer = new Timer();

        /// <summary>
        /// Create a new sequence editor.
        /// </summary>
        /// <param name="mainWindow">The main window.</param>
        public SequenceArchiveEditor(MainWindow mainWindow) : base(typeof(SequenceArchive), "Sequence Archive", "sar", "Sequence Archive Editor", mainWindow) {
            Init();
            LoadSequenceText();
        }

        /// <summary>
        /// Create a new sequence editor.
        /// </summary>
        /// <param name="fileToOpen">File to open.</param>
        public SequenceArchiveEditor(string fileToOpen) : base(typeof(SequenceArchive), "Sequence Archive", "sar", "Sequence Archive Editor", fileToOpen, null) {
            Init();
            LoadSequenceText(Path.GetFileNameWithoutExtension(fileToOpen));
            PopulateSequenceComboBox();
        }

        /// <summary>
        /// Create a new sequence editor.
        /// </summary>
        /// <param name="fileToOpen">File to open.</param>
        /// <param name="mainWindow">Main window.</param>
        /// <param name="fileName">The file name.</param>
        public SequenceArchiveEditor(IOFile fileToOpen, MainWindow mainWindow, string fileName) : base(typeof(SequenceArchive), "Sequence Archive", "sar", "Sequence Archive Editor", fileToOpen, mainWindow, fileName) {
            Init();
            CopyOtherPropertiesFromFile(SA, fileToOpen as SequenceArchive);
            LoadSequenceText(fileName);
            PopulateSequenceComboBox();
        }

        /// <summary>
        /// Init.
        /// </summary>
        public void Init() {
            Icon = Properties.Resources.Seq;
            tree.SendToBack();
            tree.Hide();
            sequenceEditorPanel.BringToFront();
            sequenceEditorPanel.Show();
            seqArcSeqPanel.BringToFront();
            seqArcSeqPanel.Show();
            seqBankPanel.BringToFront();
            seqBankPanel.Show();
            kermalisSoundPlayerPanel.Show();
            Player = new Player(Mixer);
            Player.NotePressed += new NotePressedHandler(NotePressed);
            Player.NoteReleased += new NotePressedHandler(NoteReleased);
            track0Box.CheckedChanged += new EventHandler(Track0CheckChanged);
            track1Box.CheckedChanged += new EventHandler(Track1CheckChanged);
            track2Box.CheckedChanged += new EventHandler(Track2CheckChanged);
            track3Box.CheckedChanged += new EventHandler(Track3CheckChanged);
            track4Box.CheckedChanged += new EventHandler(Track4CheckChanged);
            track5Box.CheckedChanged += new EventHandler(Track5CheckChanged);
            track6Box.CheckedChanged += new EventHandler(Track6CheckChanged);
            track7Box.CheckedChanged += new EventHandler(Track7CheckChanged);
            track8Box.CheckedChanged += new EventHandler(Track8CheckChanged);
            track9Box.CheckedChanged += new EventHandler(Track9CheckChanged);
            track10Box.CheckedChanged += new EventHandler(Track10CheckChanged);
            track11Box.CheckedChanged += new EventHandler(Track11CheckChanged);
            track12Box.CheckedChanged += new EventHandler(Track12CheckChanged);
            track13Box.CheckedChanged += new EventHandler(Track13CheckChanged);
            track14Box.CheckedChanged += new EventHandler(Track14CheckChanged);
            track15Box.CheckedChanged += new EventHandler(Track15CheckChanged);
            track0Solo.Click += new EventHandler(Track0Solo);
            track1Solo.Click += new EventHandler(Track1Solo);
            track2Solo.Click += new EventHandler(Track2Solo);
            track3Solo.Click += new EventHandler(Track3Solo);
            track4Solo.Click += new EventHandler(Track4Solo);
            track5Solo.Click += new EventHandler(Track5Solo);
            track6Solo.Click += new EventHandler(Track6Solo);
            track7Solo.Click += new EventHandler(Track7Solo);
            track8Solo.Click += new EventHandler(Track8Solo);
            track9Solo.Click += new EventHandler(Track9Solo);
            track10Solo.Click += new EventHandler(Track10Solo);
            track11Solo.Click += new EventHandler(Track11Solo);
            track12Solo.Click += new EventHandler(Track12Solo);
            track13Solo.Click += new EventHandler(Track13Solo);
            track14Solo.Click += new EventHandler(Track14Solo);
            track15Solo.Click += new EventHandler(Track15Solo);
            Mixer.Volume = .75f;
            sequenceEditor.Insert += scintilla_Insert;
            sequenceEditor.Delete += scintilla_Delete;
            kermalisPlayButton.Click += new EventHandler(PlayClick);
            kermalisPauseButton.Click += new EventHandler(PauseClick);
            kermalisStopButton.Click += new EventHandler(StopClick);
            kermalisVolumeSlider.ValueChanged += new EventHandler(VolumeChanged);
            kermalisLoopBox.CheckedChanged += new EventHandler(LoopChanged);
            kermalisPosition.MouseUp += new MouseEventHandler(PositionMouseUp);
            kermalisPosition.MouseDown += new MouseEventHandler(PositionMouseDown);
            FormClosing += new FormClosingEventHandler(SEClosing);
            Load += new System.EventHandler(this.SequenceEditor_Load);
            seqEditorBankBox.ValueChanged += new EventHandler(BankBoxChanged);
            seqEditorBankComboBox.SelectedIndexChanged += new EventHandler(BankComboChanged);
            seqArcSeqBox.ValueChanged += new EventHandler(SeqArcBoxChanged);
            seqArcSeqComboBox.SelectedIndexChanged += new EventHandler(SeqArcComboBoxChanged);
            status.Text = "Editing A Sequence Archive.";
            MyFindReplace = new FindReplace();
            MyFindReplace.Scintilla = sequenceEditor;
            sequenceEditor.KeyDown += new KeyEventHandler(genericScintilla_KeyDown);
            splitContainer1.SplitterDistance += 20;
            Timer.Tick += PositionTick;
            Timer.Interval = 1000 / 30;
            Timer.Start();
            //MyFindReplace.FindAllResults += MyFindReplace_FindAllResults;
            seqEditorBankBox.Minimum = -1;
            if (MainWindow == null || MainWindow.SA == null) {
                seqEditorBankComboBox.Enabled = false;
                seqEditorBankBox.Enabled = false;
                splitContainer1.SplitterDistance = 0;
                splitContainer1.IsSplitterFixed = true;
            } else {
                MainWindow.PopulateBankBox(MainWindow.SA, seqEditorBankComboBox);
                WritingInfo = true;
                seqEditorBankComboBox.Items.Insert(0, "Determined By Sequence");
                seqEditorBankComboBox.SelectedIndex = 0;
                seqEditorBankBox.Value = -1;
                WritingInfo = false;
            }
        }

        /// <summary>
        /// Copy other properies from a file.
        /// </summary>
        /// <param name="dest">Destination sequence archive.</param>
        /// <param name="other">The other file.</param>
        public static void CopyOtherPropertiesFromFile(SequenceArchive dest, SequenceArchive other) {
            dest.Labels.Clear();
            foreach (var l in other.Labels) {
                dest.Labels.Add(l.Key, l.Value);
            }
            for (int i = 0; i < dest.Sequences.Count; i++) {
                dest.Sequences[i].Name = other.Sequences[i].Name;
                dest.Sequences[i].Bank = other.Sequences[i].Bank;
                dest.Sequences[i].Player = other.Sequences[i].Player;
            }
        }

        /// <summary>
        /// Update nodes.
        /// </summary>
        public override void UpdateNodes() {

            //File open.
            if (FileOpen && File != null) {
                if (MainWindow != null && MainWindow.SA != null) {
                    splitContainer1.Panel1.Show();
                }
                sequenceEditor.Enabled = true;
            } else {
                splitContainer1.Panel1.Hide();
                sequenceEditor.Enabled = false;
            }

        }

        /// <summary>
        /// Do nothing.
        /// </summary>
        public override void DoInfoStuff() {}

        private void MyFindReplace_KeyPressed(object sender, KeyEventArgs e) {
            genericScintilla_KeyDown(sender, e);
        }

        private void MyFindReplace_FindAllResults(object sender, FindResultsEventArgs FindAllResults) {
            // Pass on find results
            //findAllResultsPanel1.UpdateFindAllResults(FindAllResults.FindReplace, FindAllResults.FindAllResults);
        }

        private void GotoButton_Click(object sender, EventArgs e) {
            // Use the FindReplace Scintilla as this will change based on focus
            GoTo MyGoTo = new GoTo(MyFindReplace.Scintilla);
            MyGoTo.ShowGoToDialog();
        }

        /// <summary>
		/// Key down event for each Scintilla. Tie each Scintilla to this event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void genericScintilla_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control && e.KeyCode == Keys.F) {
                MyFindReplace.ShowFind();
                e.SuppressKeyPress = true;
            } else if (e.Shift && e.KeyCode == Keys.F3) {
                MyFindReplace.Window.FindPrevious();
                e.SuppressKeyPress = true;
            } else if (e.KeyCode == Keys.F3) {
                MyFindReplace.Window.FindNext();
                e.SuppressKeyPress = true;
            } else if (e.Control && e.KeyCode == Keys.H) {
                MyFindReplace.ShowReplace();
                e.SuppressKeyPress = true;
            } else if (e.Control && e.KeyCode == Keys.I) {
                MyFindReplace.ShowIncrementalSearch();
                e.SuppressKeyPress = true;
            } else if (e.Control && e.KeyCode == Keys.G) {
                GoTo MyGoTo = new GoTo((Scintilla)sender);
                MyGoTo.ShowGoToDialog();
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Enter event tied to each Scintilla that will share a FindReplace dialog.
        /// Tie each Scintilla to this event.
        /// </summary>
        /// <param name="sender">The Scintilla receiving focus</param>
        /// <param name="e"></param>
        private void genericScintilla1_Enter(object sender, EventArgs e) {
            MyFindReplace.Scintilla = (Scintilla)sender;
        }

        /// <summary>
        /// Load the editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SequenceEditor_Load(object sender, EventArgs e) {

            //Init style.
            sequenceEditor.Dock = DockStyle.Fill;
            sequenceEditor.WrapMode = WrapMode.None;
            sequenceEditor.IndentationGuides = IndentView.LookBoth;
            sequenceEditor.StyleResetDefault();
            sequenceEditor.Styles[Style.Default].Font = "Consolas";
            sequenceEditor.Styles[Style.Default].Size = 11;
            sequenceEditor.Styles[Style.Default].BackColor = IntToColor(0x212121);
            sequenceEditor.Styles[Style.Default].ForeColor = IntToColor(0xE7E7E7);
            sequenceEditor.CaretForeColor = IntToColor(0xFFFFFF);
            sequenceEditor.StyleClearAll();
            sequenceEditor.ScrollWidth = 1;
            sequenceEditor.ScrollWidthTracking = true;
            sequenceEditor.Styles[Style.LineNumber].BackColor = IntToColor(BACK_COLOR);
            sequenceEditor.Styles[Style.LineNumber].ForeColor = IntToColor(FORE_COLOR);
            sequenceEditor.Styles[Style.IndentGuide].ForeColor = IntToColor(FORE_COLOR);
            sequenceEditor.Styles[Style.IndentGuide].BackColor = IntToColor(BACK_COLOR);
            sequenceEditor.Lexer = Lexer.Container;
            sequenceEditor.StyleNeeded += new EventHandler<StyleNeededEventArgs>(this.SEQ_StyleNeeded);
            //sequenceEditor.UpdateUI += new EventHandler<UpdateUIEventArgs>(this.SEQ_Changed);
            sequenceEditor.TextChanged += new EventHandler(this.SEQ_ChangedText);
            StyleSeq(0, sequenceEditor.Text.Length);
            UpdateLineNumbers(0, sequenceEditor.Text.Length);
            SEQ_ChangedText(null, null);

        }

        /// <summary>
        /// Sequence changed text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEQ_ChangedText(object sender, EventArgs e) {

            //Remove comment area.
            string s = sequenceEditor.Lines[sequenceEditor.CurrentLine].Text;
            if (s.Contains(";")) {
                s = s.Split(';')[0];
            }

            //Remove spaces.
            var ss = sequenceEditor.Lines[sequenceEditor.CurrentLine].Text.Replace(" ", "").Replace("\t", "").Replace("\r", "").Replace("\n", "");
            if (sequenceEditor.CurrentLine != prevLine || (prevLineBlank != (ss.EndsWith(":") || ss == ""))) {
                if (sequenceEditor.CurrentLine <= seqTableStartLine || seqTableStartLine == -1) {
                    var l = sequenceEditor.Lines.Where(x => x.Text.StartsWith("@SEQ_DATA")).FirstOrDefault();
                    if (l != null) {
                        seqTableStartLine = l.Index;
                    } else {
                        seqTableStartLine = -1;
                    }
                }
                UpdateLineNumbers(sequenceEditor.CurrentLine, sequenceEditor.Lines.Count);
                PopulateSequenceComboBox();
                if (seqArcSeqComboBox.Items.Count > 0 && seqArcSeqComboBox.SelectedIndex == -1) { seqArcSeqComboBox.SelectedIndex = 0; }
                prevLine = sequenceEditor.CurrentLine;
                prevLineBlank = (ss.EndsWith(":") || ss == ""); 
            }

        }

        /// <summary>
        /// Update sequence.
        /// </summary>
        public void UpdateSequence() {

            //Update all.
            try {
                List<SequenceCommand> commands = new List<SequenceCommand>();
                SA.FromText(sequenceEditor.Text.Replace('\r', '\n').Split('\n').ToList(), MainWindow.SA);
                UpdateNodes();
            } catch (Exception exe) { MessageBox.Show(exe.Message); }

        }

        /// <summary>
        /// SEQ needs style.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEQ_StyleNeeded(object sender, StyleNeededEventArgs e) {
            var startPos = sequenceEditor.GetEndStyled();
            var endPos = e.Position;

            if (startPos >= 500) { startPos -= 500; } else { startPos = 0; }
            if ((sequenceEditor.Text.Length - endPos) >= 500) { endPos += 500; } else { endPos = sequenceEditor.Text.Length; }

            StyleSeq(startPos, endPos);
        }

        /// <summary>
        /// Style the SEQ.
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        public void StyleSeq(int startPos, int endPos) {

            //Syntax highlighting.
            sequenceEditor.Styles[(int)CommandStyleType.Regular].ForeColor = IntToColor(0xE7E7E7);
            sequenceEditor.Styles[(int)CommandStyleType.Comment].ForeColor = IntToColor(0xAEAEAE);
            sequenceEditor.Styles[(int)CommandStyleType.SeqArc].ForeColor = IntToColor(0x4AF0B6);
            sequenceEditor.Styles[(int)CommandStyleType.Label].ForeColor = IntToColor(0xE7BB00);
            sequenceEditor.Styles[(int)CommandStyleType.Prefix].ForeColor = IntToColor(0x4AF0B6);
            sequenceEditor.Styles[(int)CommandStyleType.Value0].ForeColor = Color.Red;
            sequenceEditor.Styles[(int)CommandStyleType.Value1].ForeColor = Color.Orange;
            sequenceEditor.Styles[(int)CommandStyleType.Value2].ForeColor = Color.Yellow;
            sequenceEditor.Styles[(int)CommandStyleType.Value3].ForeColor = Color.LimeGreen;
            sequenceEditor.Styles[(int)CommandStyleType.Value4].ForeColor = Color.LightBlue;
            sequenceEditor.Styles[(int)CommandStyleType.Value5].ForeColor = Color.PaleVioletRed;

            //Sum.
            int pos = startPos;
            if (endPos > sequenceEditor.Text.Length) {
                endPos = sequenceEditor.Text.Length;
            }
            CommandStyleType style = CommandStyleType.Regular;
            string[] lines = sequenceEditor.Text.Substring(startPos, endPos - startPos).Split('\n');
            foreach (string s in lines) {

                //Do each char.
                style = CommandStyleType.Regular;
                bool initialSpaceCut = false;
                string withoutInitialSpace = s.Replace("\t", " ");
                int numWhiteSpace = 0;
                for (int j = 0; j < s.Length; j++) {

                    //Convert tabs to spaces.
                    string l = s.Replace("\t", " ");

                    //Label.
                    if (l.Contains(":") && j == 0) {
                        sequenceEditor.StartStyling(pos);
                        sequenceEditor.SetStyling(l.IndexOf(':') + 1, (int)CommandStyleType.Label);
                        j += l.IndexOf(':') + 1;
                        if (j >= l.Length) {
                            break;
                        }
                    }

                    //Jump to cut off intro spaces.
                    bool kill = false;
                    while ((l[j] == ' ') && !initialSpaceCut) {
                        j++;
                        if (j >= l.Length) {
                            kill = true;
                            break;
                        } else {
                            withoutInitialSpace = l.Substring(j, l.Length - j);
                            numWhiteSpace = j;
                        }
                    }
                    initialSpaceCut = true;
                    if (kill) {
                        break;
                    }

                    //Get char and index.
                    char c = l[j];
                    int ind = j + pos;

                    //Comment.
                    if (c == ';') {
                        sequenceEditor.StartStyling(ind);
                        sequenceEditor.SetStyling(l.Length - j, (int)CommandStyleType.Comment);
                        break;
                    }

                    //SeqArc.
                    if (c == '@') {
                        sequenceEditor.StartStyling(ind);
                        sequenceEditor.SetStyling(l.Length - j, (int)CommandStyleType.SeqArc);
                        break;
                    }

                    //Prefix.
                    if (c == '_') {

                        //Check prefix.
                        string p = l.Substring(j, l.Length - j).Split(' ')[0];
                        bool afterSpace = false;
                        if (withoutInitialSpace.Contains(" ")) {
                            if (j > withoutInitialSpace.IndexOf(" ") + numWhiteSpace) { afterSpace = true; }
                        }
                        if (!afterSpace && (p.Contains("_if ") || p.Contains("_v ") || p.Contains("_r ") || p.Contains("_t ") || p.Contains("_tr ") || p.Contains("_tv ") || p.EndsWith("_if") || p.EndsWith("_v") || p.EndsWith("_t") || p.EndsWith("_tv") || p.EndsWith("_tr") || p.EndsWith("_r"))) {
                            style = CommandStyleType.Prefix;
                        }

                    }

                    //Space.
                    if (c == ' ') {
                        if (j > 0) {
                            if (l[j - 1] != ' ') {
                                if (style < CommandStyleType.Prefix) {
                                    style = CommandStyleType.Prefix;
                                }
                                style++;
                            }
                        }
                    }

                    //Do style.
                    sequenceEditor.StartStyling(ind);
                    sequenceEditor.SetStyling(1, (int)style);

                }

                //Add position, plus one for \r.
                pos += s.Length + 1;

            }

        }

        /// <summary>
        /// Command style.
        /// </summary>
        public enum CommandStyleType {
            Null, Regular, Comment, SeqArc, Label, Prefix, Value0, Value1, Value2, Value3, Value4, Value5
        }

        /// <summary>
        /// Int to color.
        /// </summary>
        /// <param name="rgb"></param>
        /// <returns></returns>
        public static Color IntToColor(int rgb) {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        /// <summary>
        /// Load sequence text.
        /// </summary>
        public void LoadSequenceText(string name = "Sequence") {

            //Why not.
            sequenceEditor.Margins[0].Type = MarginType.RightText;
            sequenceEditor.Margins[0].Width = 35;

            //File is not null.
            if (File != null) {

                //Not read only.
                sequenceEditor.ReadOnly = false;

                //Load commands.
                SA.ReadCommandData(true);
                SA.Name = name;

                //Set the text.
                sequenceEditor.Text = String.Join("\n", SA.ToText());

            }

            //File is null.
            else {
                sequenceEditor.Text = "{ NULL FILE INFO }";
            }

            //Update line numbers.
            UpdateLineNumbers(0, sequenceEditor.Lines.Count);

        }

        /// <summary>
        /// Update line numbers.
        /// </summary>
        /// <param name="startingAtLine">Starting at line.</param>
        /// <param name="endingAtLine">Ending line.</param>
        private void UpdateLineNumbers(int startingAtLine, int endingAtLine) {

            //Get previous number.
            int pastNum = 0;
            if (startingAtLine != 0) {
                pastNum = int.Parse(sequenceEditor.Lines[startingAtLine - 1].MarginText);
                var ss = sequenceEditor.Lines[startingAtLine - 1].Text.Replace(" ", "").Replace("\t", "").Replace("\r", "").Replace("\n", "");
                if (ss != "" && !ss.EndsWith(":")) {
                    pastNum++;
                }
            }

            //Add each command length.
            int sum = pastNum;
            if (endingAtLine > sequenceEditor.Lines.Count) {
                endingAtLine = sequenceEditor.Lines.Count;
            }
            for (int i = startingAtLine; i < endingAtLine; i++) {

                //Past sequence data start.
                if (i > seqTableStartLine) {

                    //Set style.
                    sequenceEditor.Lines[i].MarginStyle = Style.LineNumber;

                    //Remove comment area.
                    string s = sequenceEditor.Lines[i].Text;
                    if (s.Contains(";")) {
                        s = s.Split(';')[0];
                    }

                    //Remove spaces.
                    s = s.Replace(" ", "").Replace("\t", "").Replace("\r", "").Replace("\n", "");

                    //Get num.
                    sequenceEditor.Lines[i].MarginText = "" + sum;

                    //Number to add.
                    if (s != "" && !s.EndsWith(":")) { sum += 1; }

                } else {
                    sequenceEditor.Lines[i].MarginText = "0";
                }

            }

        }

        /// <summary>
        /// Get sequence names.
        /// </summary>
        /// <returns>The sequence names.</returns>
        public List<Tuple<string, int>> GetSequenceNames() {

            //New list.
            var ret = new List<Tuple<string, int>>();

            //Get the start of the sequence table.
            var sL = sequenceEditor.Lines.Where(x => x.Text.StartsWith("@SEQ_TABLE")).FirstOrDefault();
            var sD = sequenceEditor.Lines.Where(x => x.Text.StartsWith("@SEQ_DATA")).FirstOrDefault();
            if (sL == null || sD == null) {
                return ret;
            }
            int seqTableStart = sL.Index;
            int seqDataStart = sD.Index;

            //Run through the list.
            int ind = 0;
            for (int i = seqTableStart + 1; i < seqDataStart; i++) {
                string s = sequenceEditor.Lines[i].Text;
                if (s.Contains(';')) { s = s.Substring(s.IndexOf(';')); }
                s = s.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                if (s.Equals("")) {
                    continue;
                }
                s = s.Split(':')[0];
                int num = ind;
                if (s.Contains("=")) {
                    num = int.Parse(s.Split('=')[1]);
                    ind = num;
                    s = s.Split('=')[0];
                } else if (int.TryParse(s, out _)) {
                    num = int.Parse(s);
                    ind = num;
                    s = "Sequence_" + ind;
                }
                ret.Add(new Tuple<string, int>(s, ind++));
            }

            //Return the list.
            return ret;
        
        }

        /// <summary>
        /// Populate the combo box.
        /// </summary>
        public void PopulateSequenceComboBox() {
            var l = GetSequenceNames();
            string oldName = null;
            if (seqArcSeqComboBox.SelectedItem != null) {
                oldName = ((string)seqArcSeqComboBox.SelectedItem).Substring(((string)seqArcSeqComboBox.SelectedItem).IndexOf(" "));
            }
            WritingInfo = true;
            seqArcSeqComboBox.Items.Clear();
            foreach (var s in l) {
                seqArcSeqComboBox.Items.Add("[" + s.Item2 + "] " + s.Item1);
                if (s.Item1.Equals(oldName)) { seqArcSeqComboBox.SelectedIndex = seqArcSeqComboBox.Items.Count - 1; }
            }
            if (seqArcSeqComboBox.SelectedIndex == -1) { try { seqArcSeqComboBox.SelectedIndex = 0; } catch { } }
            seqArcSeqBox.Maximum = l.Count - 1;
            if (l.Count > 0) {
                seqArcSeqBox.Minimum = 0;
            } else {
                seqArcSeqBox.Minimum = -1;
            }
            try { seqArcSeqBox.Value = seqArcSeqComboBox.SelectedIndex; } catch { seqArcSeqBox.Value = 0; }
            WritingInfo = false;
        }

        private void scintilla_Insert(object sender, ModificationEventArgs e) {
            // Only update line numbers if the number of lines changed
            if (e.LinesAdded != 0)
                UpdateLineNumbers(0, sequenceEditor.Lines.Count);
        }

        private void scintilla_Delete(object sender, ModificationEventArgs e) {
            // Only update line numbers if the number of lines changed
            if (e.LinesAdded != 0)
                UpdateLineNumbers(0, sequenceEditor.Lines.Count);
        }

        public override void newToolStripMenuItem_Click(object sender, EventArgs e) {

            //File open and can save test.
            if (!FileTest(sender, e, true)) {
                return;
            }

            //Create instance of file.
            File = (IOFile)Activator.CreateInstance(FileType);

            //Reset values.
            FilePath = "";
            FileOpen = true;
            ExtFile = null;
            Text = EditorName + " - New " + ExtensionDescription + ".s" + Extension;
            UpdateNodes();
            LoadSequenceText("New Sequence Archive");

        }

        public override void openToolStripMenuItem_Click(object sender, EventArgs e) {

            //File open and save test.
            if (!FileTest(sender, e, true)) {
                return;
            }

            //Open the file.
            string path = GetFileOpenerPath(ExtensionDescription, Extension);

            //File is not null.
            if (path != "") {

                //Set value.
                File = (IOFile)Activator.CreateInstance(FileType);
                ExtFile = null;
                FilePath = path;
                Text = EditorName + " - " + Path.GetFileName(path);
                FileOpen = true;

                //Read data.
                File.Read(path);

                //Update.
                UpdateNodes();
                LoadSequenceText(Path.GetFileNameWithoutExtension(path));

            }

        }

        public override void saveToolStripMenuItem_Click(object sender, EventArgs e) {

            //Update.
            UpdateSequence();
            if (!SA.WritingCommandSuccess) {
                return;
            }
            SA.WriteCommandData();

            //Do base.
            base.saveToolStripMenuItem_Click(sender, e);
            if (ExtFile != null) { CopyOtherPropertiesFromFile(ExtFile as SequenceArchive, SA); }

            //Main window.
            if (MainWindow != null) {
                MainWindow.UpdateNodes();
                MainWindow.DoInfoStuff();
            }

        }

        public override void importFileToolStripMenuItem_Click(object sender, EventArgs e) {

            //File open test.
            if (!FileTest(sender, e, false, true)) {
                return;
            }

            //Open the file.
            OpenFileDialog o = new OpenFileDialog();
            o.RestoreDirectory = true;
            o.Filter = "Supported Sound Files|*.ssar;*.mus|Sound Sequence Archive|*.ssar|Music List|*.mus";

            if (o.ShowDialog() != DialogResult.OK) {
                return;
            }
            string path = o.FileName;

            //File is not null.
            if (path.EndsWith(".ssar")) {

                //Set value.
                string name = SA.Name;
                File = (IOFile)Activator.CreateInstance(FileType);
                SA.Name = name;

                //Read data.
                File.Read(path);

                //Update.
                LoadSequenceText(name);

            } else {
                sequenceEditor.Text = System.IO.File.ReadAllText(path);
            }

        }

        public override void exportFileToolStripMenuItem_Click(object sender, EventArgs e) {

            //File open test.
            if (!FileTest(sender, e, false, true)) {
                return;
            }

            //Update.
            UpdateSequence();
            if (!SA.WritingCommandSuccess) {
                return;
            }
            SA.WriteCommandData();

            //Export.
            SaveFileDialog s = new SaveFileDialog();
            s.RestoreDirectory = true;
            s.Filter = "Supported Sound Files|*.ssar;*.mus|Sound Sequence Archive|*.ssar|Music List|*.mus";
            s.OverwritePrompt = false;
            if (s.ShowDialog() == DialogResult.OK) {
                if (s.FileName.EndsWith(".mus")) {
                    System.IO.File.WriteAllText(s.FileName, sequenceEditor.Text);
                } else {
                    SA.Write(s.FileName);
                }
            }

        }

        public override void blankFileToolStripMenuItem_Click(object sender, EventArgs e) {

            //File open save test.
            if (!FileTest(sender, e, false, true)) {
                return;
            }

            //Create instance of file.
            string name = SA.Name;
            File = (IOFile)Activator.CreateInstance(FileType);
            SA.RawData = new byte[0];
            LoadSequenceText(name);

        }

        /// <summary>
        /// Play click.
        /// </summary>
        public void PlayClick(object sender, EventArgs e) {
            UpdateSequence();
            if (!SA.WritingCommandSuccess) {
                return;
            }
            if (MainWindow == null) {
                MessageBox.Show("There must be an SDAT connected to this file to play it.");
                return;
            }
            var s = SA.Sequences.Where(x => x.Index == seqArcSeqBox.Value).FirstOrDefault();
            if (s == null) {
                MessageBox.Show("The given preview sequence does not exist.");
                return;
            }
            if (seqEditorBankBox.Value == -1) {
                if (s.Bank == null && MainWindow.SA.Banks.Where(x => x.Index == s.ReadingBankId).Count() < 1) {
                    MessageBox.Show("The bank hooked up to the preview sequence doesn't exist.");
                    return;
                }
            }
            BankInfo bnk = null;
            if (seqEditorBankBox.Value == -1) {
                bnk = s.Bank != null ? s.Bank : MainWindow.SA.Banks.Where(x => x.Index == s.ReadingBankId).FirstOrDefault();
            } else {
                if (MainWindow.SA.Banks.Where(x => x.Index == seqEditorBankBox.Value).Count() < 1) {
                    MessageBox.Show("The bank hooked up to the preview sequence doesn't exist.");
                    return;
                }
                bnk = MainWindow.SA.Banks.Where(x => x.Index == seqEditorBankBox.Value).FirstOrDefault();
            }
            Player.PrepareForSong(new PlayableBank[] { bnk.File }, bnk.GetAssociatedWaves());
            if (!SA.PublicLabels.Keys.Contains(s.Name)) {
                MessageBox.Show("Label \"" + s.LabelName + "\" not defined!");
                return;
            }
            Player.LoadSong(SA.Commands, SA.PublicLabels[s.Name]);
            kermalisPosition.Maximum = (int)Player.MaxTicks;
            kermalisPosition.TickFrequency = kermalisPosition.Maximum / 10;
            kermalisPosition.LargeChange = kermalisPosition.Maximum / 20;
            Player.Play();
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
            track0Picture.BackgroundImage = Properties.Resources.Idle;
            track1Picture.BackgroundImage = Properties.Resources.Idle;
            track2Picture.BackgroundImage = Properties.Resources.Idle;
            track3Picture.BackgroundImage = Properties.Resources.Idle;
            track4Picture.BackgroundImage = Properties.Resources.Idle;
            track5Picture.BackgroundImage = Properties.Resources.Idle;
            track6Picture.BackgroundImage = Properties.Resources.Idle;
            track7Picture.BackgroundImage = Properties.Resources.Idle;
            track8Picture.BackgroundImage = Properties.Resources.Idle;
            track9Picture.BackgroundImage = Properties.Resources.Idle;
            track10Picture.BackgroundImage = Properties.Resources.Idle;
            track11Picture.BackgroundImage = Properties.Resources.Idle;
            track12Picture.BackgroundImage = Properties.Resources.Idle;
            track13Picture.BackgroundImage = Properties.Resources.Idle;
            track14Picture.BackgroundImage = Properties.Resources.Idle;
            track15Picture.BackgroundImage = Properties.Resources.Idle;
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
        public void SEClosing(object sender, FormClosingEventArgs e) {
            Player.Stop();
            Player.Dispose();
            Mixer.Dispose();
            Timer.Stop();
        }

        public void SeqArcComboBoxChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                WritingInfo = true;
                seqArcSeqBox.Value = int.Parse(((string)seqArcSeqComboBox.SelectedItem).Split('[')[1].Split(']')[0]);
                WritingInfo = false;
            }
        }

        public void SeqArcBoxChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                WritingInfo = true;
                for (int i = 1; i < seqEditorBankComboBox.Items.Count; i++) {
                    if ((int)seqArcSeqBox.Value == int.Parse(((string)seqArcSeqComboBox.Items[i]).Split('[')[1].Split(']')[0])) {
                        seqArcSeqComboBox.SelectedIndex = i;
                        WritingInfo = false;
                        return;
                    }
                }
                seqArcSeqComboBox.SelectedIndex = 0;
                WritingInfo = false;
            }
        }

        public void BankComboChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                WritingInfo = true;
                if (seqEditorBankComboBox.SelectedIndex != 1) {
                    if (seqEditorBankComboBox.SelectedIndex != 0) {
                        seqEditorBankBox.Value = int.Parse(((string)seqEditorBankComboBox.SelectedItem).Split('[')[1].Split(']')[0]);
                    } else {
                        seqEditorBankBox.Value = -1;
                    }
                }
                WritingInfo = false;
            }
        }

        public void BankBoxChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                WritingInfo = true;
                for (int i = 2; i < seqEditorBankComboBox.Items.Count; i++) {
                    if ((int)seqEditorBankBox.Value == int.Parse(((string)seqEditorBankComboBox.Items[i]).Split('[')[1].Split(']')[0])) {
                        seqEditorBankComboBox.SelectedIndex = i;
                        WritingInfo = false;
                        return;
                    }
                }
                if (seqEditorBankBox.Value == -1) {
                    seqEditorBankComboBox.SelectedIndex = 0;
                } else {
                    seqEditorBankComboBox.SelectedIndex = 1;
                }
                WritingInfo = false;
            }
        }

        public override void closeToolStripMenuItem_Click(object sender, EventArgs e) {

            //Do base.
            base.closeToolStripMenuItem_Click(sender, e);

            //Clear text.
            sequenceEditor.Text = "";

        }

        private void NotePressed(object sender, NoteEventArgs e) {
            switch (e.TrackId) {
                case 0:
                    if (track0Box.Checked) {
                        track0Picture.BackgroundImage = Properties.Resources.NoteDown;
                    }
                    break;
                case 1:
                    if (track1Box.Checked) {
                        track1Picture.BackgroundImage = Properties.Resources.NoteDown;
                    }
                    break;
                case 2:
                    if (track2Box.Checked) {
                        track2Picture.BackgroundImage = Properties.Resources.NoteDown;
                    }
                    break;
                case 3:
                    if (track3Box.Checked) {
                        track3Picture.BackgroundImage = Properties.Resources.NoteDown;
                    }
                    break;
                case 4:
                    if (track4Box.Checked) {
                        track4Picture.BackgroundImage = Properties.Resources.NoteDown;
                    }
                    break;
                case 5:
                    if (track5Box.Checked) {
                        track5Picture.BackgroundImage = Properties.Resources.NoteDown;
                    }
                    break;
                case 6:
                    if (track6Box.Checked) {
                        track6Picture.BackgroundImage = Properties.Resources.NoteDown;
                    }
                    break;
                case 7:
                    if (track7Box.Checked) {
                        track7Picture.BackgroundImage = Properties.Resources.NoteDown;
                    }
                    break;
                case 8:
                    if (track8Box.Checked) {
                        track8Picture.BackgroundImage = Properties.Resources.NoteDown;
                    }
                    break;
                case 9:
                    if (track9Box.Checked) {
                        track9Picture.BackgroundImage = Properties.Resources.NoteDown;
                    }
                    break;
                case 10:
                    if (track10Box.Checked) {
                        track10Picture.BackgroundImage = Properties.Resources.NoteDown;
                    }
                    break;
                case 11:
                    if (track11Box.Checked) {
                        track11Picture.BackgroundImage = Properties.Resources.NoteDown;
                    }
                    break;
                case 12:
                    if (track12Box.Checked) {
                        track12Picture.BackgroundImage = Properties.Resources.NoteDown;
                    }
                    break;
                case 13:
                    if (track13Box.Checked) {
                        track13Picture.BackgroundImage = Properties.Resources.NoteDown;
                    }
                    break;
                case 14:
                    if (track14Box.Checked) {
                        track14Picture.BackgroundImage = Properties.Resources.NoteDown;
                    }
                    break;
                case 15:
                    if (track15Box.Checked) {
                        track15Picture.BackgroundImage = Properties.Resources.NoteDown;
                    }
                    break;
            }
        }

        private void NoteReleased(object sender, NoteEventArgs e) {
            switch (e.TrackId) {
                case 0:
                    if (track0Box.Checked) {
                        track0Picture.BackgroundImage = Properties.Resources.Idle;
                    }
                    break;
                case 1:
                    if (track1Box.Checked) {
                        track1Picture.BackgroundImage = Properties.Resources.Idle;
                    }
                    break;
                case 2:
                    if (track2Box.Checked) {
                        track2Picture.BackgroundImage = Properties.Resources.Idle;
                    }
                    break;
                case 3:
                    if (track3Box.Checked) {
                        track3Picture.BackgroundImage = Properties.Resources.Idle;
                    }
                    break;
                case 4:
                    if (track4Box.Checked) {
                        track4Picture.BackgroundImage = Properties.Resources.Idle;
                    }
                    break;
                case 5:
                    if (track5Box.Checked) {
                        track5Picture.BackgroundImage = Properties.Resources.Idle;
                    }
                    break;
                case 6:
                    if (track6Box.Checked) {
                        track6Picture.BackgroundImage = Properties.Resources.Idle;
                    }
                    break;
                case 7:
                    if (track7Box.Checked) {
                        track7Picture.BackgroundImage = Properties.Resources.Idle;
                    }
                    break;
                case 8:
                    if (track8Box.Checked) {
                        track8Picture.BackgroundImage = Properties.Resources.Idle;
                    }
                    break;
                case 9:
                    if (track9Box.Checked) {
                        track9Picture.BackgroundImage = Properties.Resources.Idle;
                    }
                    break;
                case 10:
                    if (track10Box.Checked) {
                        track10Picture.BackgroundImage = Properties.Resources.Idle;
                    }
                    break;
                case 11:
                    if (track11Box.Checked) {
                        track11Picture.BackgroundImage = Properties.Resources.Idle;
                    }
                    break;
                case 12:
                    if (track12Box.Checked) {
                        track12Picture.BackgroundImage = Properties.Resources.Idle;
                    }
                    break;
                case 13:
                    if (track13Box.Checked) {
                        track13Picture.BackgroundImage = Properties.Resources.Idle;
                    }
                    break;
                case 14:
                    if (track14Box.Checked) {
                        track14Picture.BackgroundImage = Properties.Resources.Idle;
                    }
                    break;
                case 15:
                    if (track15Box.Checked) {
                        track15Picture.BackgroundImage = Properties.Resources.Idle;
                    }
                    break;
            }
        }

        private void Track0CheckChanged(object sender, EventArgs e) {
            bool check = track0Box.Checked;
            if (check) {
                track0Picture.BackgroundImage = Properties.Resources.Idle;
            } else {
                track0Picture.BackgroundImage = Properties.Resources.Mute;
            }
            Mixer.Mutes[0] = !check;
        }

        private void Track1CheckChanged(object sender, EventArgs e) {
            bool check = track1Box.Checked;
            if (check) {
                track1Picture.BackgroundImage = Properties.Resources.Idle;
            } else {
                track1Picture.BackgroundImage = Properties.Resources.Mute;
            }
            Mixer.Mutes[1] = !check;
        }

        private void Track2CheckChanged(object sender, EventArgs e) {
            bool check = track2Box.Checked;
            if (check) {
                track2Picture.BackgroundImage = Properties.Resources.Idle;
            } else {
                track2Picture.BackgroundImage = Properties.Resources.Mute;
            }
            Mixer.Mutes[2] = !check;
        }

        private void Track3CheckChanged(object sender, EventArgs e) {
            bool check = track3Box.Checked;
            if (check) {
                track3Picture.BackgroundImage = Properties.Resources.Idle;
            } else {
                track3Picture.BackgroundImage = Properties.Resources.Mute;
            }
            Mixer.Mutes[3] = !check;
        }

        private void Track4CheckChanged(object sender, EventArgs e) {
            bool check = track4Box.Checked;
            if (check) {
                track4Picture.BackgroundImage = Properties.Resources.Idle;
            } else {
                track4Picture.BackgroundImage = Properties.Resources.Mute;
            }
            Mixer.Mutes[4] = !check;
        }

        private void Track5CheckChanged(object sender, EventArgs e) {
            bool check = track5Box.Checked;
            if (check) {
                track5Picture.BackgroundImage = Properties.Resources.Idle;
            } else {
                track5Picture.BackgroundImage = Properties.Resources.Mute;
            }
            Mixer.Mutes[5] = !check;
        }

        private void Track6CheckChanged(object sender, EventArgs e) {
            bool check = track6Box.Checked;
            if (check) {
                track6Picture.BackgroundImage = Properties.Resources.Idle;
            } else {
                track6Picture.BackgroundImage = Properties.Resources.Mute;
            }
            Mixer.Mutes[6] = !check;
        }

        private void Track7CheckChanged(object sender, EventArgs e) {
            bool check = track7Box.Checked;
            if (check) {
                track7Picture.BackgroundImage = Properties.Resources.Idle;
            } else {
                track7Picture.BackgroundImage = Properties.Resources.Mute;
            }
            Mixer.Mutes[7] = !check;
        }

        private void Track8CheckChanged(object sender, EventArgs e) {
            bool check = track8Box.Checked;
            if (check) {
                track8Picture.BackgroundImage = Properties.Resources.Idle;
            } else {
                track8Picture.BackgroundImage = Properties.Resources.Mute;
            }
            Mixer.Mutes[8] = !check;
        }

        private void Track9CheckChanged(object sender, EventArgs e) {
            bool check = track9Box.Checked;
            if (check) {
                track9Picture.BackgroundImage = Properties.Resources.Idle;
            } else {
                track9Picture.BackgroundImage = Properties.Resources.Mute;
            }
            Mixer.Mutes[9] = !check;
        }

        private void Track10CheckChanged(object sender, EventArgs e) {
            bool check = track10Box.Checked;
            if (check) {
                track10Picture.BackgroundImage = Properties.Resources.Idle;
            } else {
                track10Picture.BackgroundImage = Properties.Resources.Mute;
            }
            Mixer.Mutes[10] = !check;
        }

        private void Track11CheckChanged(object sender, EventArgs e) {
            bool check = track11Box.Checked;
            if (check) {
                track11Picture.BackgroundImage = Properties.Resources.Idle;
            } else {
                track11Picture.BackgroundImage = Properties.Resources.Mute;
            }
            Mixer.Mutes[11] = !check;
        }

        private void Track12CheckChanged(object sender, EventArgs e) {
            bool check = track12Box.Checked;
            if (check) {
                track12Picture.BackgroundImage = Properties.Resources.Idle;
            } else {
                track12Picture.BackgroundImage = Properties.Resources.Mute;
            }
            Mixer.Mutes[12] = !check;
        }

        private void Track13CheckChanged(object sender, EventArgs e) {
            bool check = track13Box.Checked;
            if (check) {
                track13Picture.BackgroundImage = Properties.Resources.Idle;
            } else {
                track13Picture.BackgroundImage = Properties.Resources.Mute;
            }
            Mixer.Mutes[13] = !check;
        }

        private void Track14CheckChanged(object sender, EventArgs e) {
            bool check = track14Box.Checked;
            if (check) {
                track14Picture.BackgroundImage = Properties.Resources.Idle;
            } else {
                track14Picture.BackgroundImage = Properties.Resources.Mute;
            }
            Mixer.Mutes[14] = !check;
        }

        private void Track15CheckChanged(object sender, EventArgs e) {
            bool check = track15Box.Checked;
            if (check) {
                track15Picture.BackgroundImage = Properties.Resources.Idle;
            } else {
                track15Picture.BackgroundImage = Properties.Resources.Mute;
            }
            Mixer.Mutes[15] = !check;
        }

        private void Track0Solo(object sender, EventArgs e) {

            //Already solo'd.
            if (track0Box.Checked && !(track1Box.Checked || track2Box.Checked || track3Box.Checked || track4Box.Checked || track5Box.Checked || track6Box.Checked || track7Box.Checked || track8Box.Checked || track9Box.Checked || track10Box.Checked || track11Box.Checked || track12Box.Checked || track13Box.Checked || track14Box.Checked || track15Box.Checked)) {
                track0Box.Checked = true;
                track1Box.Checked = true;
                track2Box.Checked = true;
                track3Box.Checked = true;
                track4Box.Checked = true;
                track5Box.Checked = true;
                track6Box.Checked = true;
                track7Box.Checked = true;
                track8Box.Checked = true;
                track9Box.Checked = true;
                track10Box.Checked = true;
                track11Box.Checked = true;
                track12Box.Checked = true;
                track13Box.Checked = true;
                track14Box.Checked = true;
                track15Box.Checked = true;
            } else {
                track0Box.Checked = true;
                track1Box.Checked = false;
                track2Box.Checked = false;
                track3Box.Checked = false;
                track4Box.Checked = false;
                track5Box.Checked = false;
                track6Box.Checked = false;
                track7Box.Checked = false;
                track8Box.Checked = false;
                track9Box.Checked = false;
                track10Box.Checked = false;
                track11Box.Checked = false;
                track12Box.Checked = false;
                track13Box.Checked = false;
                track14Box.Checked = false;
                track15Box.Checked = false;
            }

        }

        private void Track1Solo(object sender, EventArgs e) {

            //Already solo'd.
            if (track1Box.Checked && !(track0Box.Checked || track2Box.Checked || track3Box.Checked || track4Box.Checked || track5Box.Checked || track6Box.Checked || track7Box.Checked || track8Box.Checked || track9Box.Checked || track10Box.Checked || track11Box.Checked || track12Box.Checked || track13Box.Checked || track14Box.Checked || track15Box.Checked)) {
                track0Box.Checked = true;
                track1Box.Checked = true;
                track2Box.Checked = true;
                track3Box.Checked = true;
                track4Box.Checked = true;
                track5Box.Checked = true;
                track6Box.Checked = true;
                track7Box.Checked = true;
                track8Box.Checked = true;
                track9Box.Checked = true;
                track10Box.Checked = true;
                track11Box.Checked = true;
                track12Box.Checked = true;
                track13Box.Checked = true;
                track14Box.Checked = true;
                track15Box.Checked = true;
            } else {
                track0Box.Checked = false;
                track1Box.Checked = true;
                track2Box.Checked = false;
                track3Box.Checked = false;
                track4Box.Checked = false;
                track5Box.Checked = false;
                track6Box.Checked = false;
                track7Box.Checked = false;
                track8Box.Checked = false;
                track9Box.Checked = false;
                track10Box.Checked = false;
                track11Box.Checked = false;
                track12Box.Checked = false;
                track13Box.Checked = false;
                track14Box.Checked = false;
                track15Box.Checked = false;
            }

        }

        private void Track2Solo(object sender, EventArgs e) {

            //Already solo'd.
            if (track2Box.Checked && !(track0Box.Checked || track1Box.Checked || track3Box.Checked || track4Box.Checked || track5Box.Checked || track6Box.Checked || track7Box.Checked || track8Box.Checked || track9Box.Checked || track10Box.Checked || track11Box.Checked || track12Box.Checked || track13Box.Checked || track14Box.Checked || track15Box.Checked)) {
                track0Box.Checked = true;
                track1Box.Checked = true;
                track2Box.Checked = true;
                track3Box.Checked = true;
                track4Box.Checked = true;
                track5Box.Checked = true;
                track6Box.Checked = true;
                track7Box.Checked = true;
                track8Box.Checked = true;
                track9Box.Checked = true;
                track10Box.Checked = true;
                track11Box.Checked = true;
                track12Box.Checked = true;
                track13Box.Checked = true;
                track14Box.Checked = true;
                track15Box.Checked = true;
            } else {
                track0Box.Checked = false;
                track1Box.Checked = false;
                track2Box.Checked = true;
                track3Box.Checked = false;
                track4Box.Checked = false;
                track5Box.Checked = false;
                track6Box.Checked = false;
                track7Box.Checked = false;
                track8Box.Checked = false;
                track9Box.Checked = false;
                track10Box.Checked = false;
                track11Box.Checked = false;
                track12Box.Checked = false;
                track13Box.Checked = false;
                track14Box.Checked = false;
                track15Box.Checked = false;
            }

        }

        private void Track3Solo(object sender, EventArgs e) {

            //Already solo'd.
            if (track3Box.Checked && !(track0Box.Checked || track1Box.Checked || track2Box.Checked || track4Box.Checked || track5Box.Checked || track6Box.Checked || track7Box.Checked || track8Box.Checked || track9Box.Checked || track10Box.Checked || track11Box.Checked || track12Box.Checked || track13Box.Checked || track14Box.Checked || track15Box.Checked)) {
                track0Box.Checked = true;
                track1Box.Checked = true;
                track2Box.Checked = true;
                track3Box.Checked = true;
                track4Box.Checked = true;
                track5Box.Checked = true;
                track6Box.Checked = true;
                track7Box.Checked = true;
                track8Box.Checked = true;
                track9Box.Checked = true;
                track10Box.Checked = true;
                track11Box.Checked = true;
                track12Box.Checked = true;
                track13Box.Checked = true;
                track14Box.Checked = true;
                track15Box.Checked = true;
            } else {
                track0Box.Checked = false;
                track1Box.Checked = false;
                track2Box.Checked = false;
                track3Box.Checked = true;
                track4Box.Checked = false;
                track5Box.Checked = false;
                track6Box.Checked = false;
                track7Box.Checked = false;
                track8Box.Checked = false;
                track9Box.Checked = false;
                track10Box.Checked = false;
                track11Box.Checked = false;
                track12Box.Checked = false;
                track13Box.Checked = false;
                track14Box.Checked = false;
                track15Box.Checked = false;
            }

        }

        private void Track4Solo(object sender, EventArgs e) {

            //Already solo'd.
            if (track4Box.Checked && !(track0Box.Checked || track1Box.Checked || track2Box.Checked || track3Box.Checked || track5Box.Checked || track6Box.Checked || track7Box.Checked || track8Box.Checked || track9Box.Checked || track10Box.Checked || track11Box.Checked || track12Box.Checked || track13Box.Checked || track14Box.Checked || track15Box.Checked)) {
                track0Box.Checked = true;
                track1Box.Checked = true;
                track2Box.Checked = true;
                track3Box.Checked = true;
                track4Box.Checked = true;
                track5Box.Checked = true;
                track6Box.Checked = true;
                track7Box.Checked = true;
                track8Box.Checked = true;
                track9Box.Checked = true;
                track10Box.Checked = true;
                track11Box.Checked = true;
                track12Box.Checked = true;
                track13Box.Checked = true;
                track14Box.Checked = true;
                track15Box.Checked = true;
            } else {
                track0Box.Checked = false;
                track1Box.Checked = false;
                track2Box.Checked = false;
                track3Box.Checked = false;
                track4Box.Checked = true;
                track5Box.Checked = false;
                track6Box.Checked = false;
                track7Box.Checked = false;
                track8Box.Checked = false;
                track9Box.Checked = false;
                track10Box.Checked = false;
                track11Box.Checked = false;
                track12Box.Checked = false;
                track13Box.Checked = false;
                track14Box.Checked = false;
                track15Box.Checked = false;
            }

        }

        private void Track5Solo(object sender, EventArgs e) {

            //Already solo'd.
            if (track5Box.Checked && !(track0Box.Checked || track1Box.Checked || track2Box.Checked || track3Box.Checked || track4Box.Checked || track6Box.Checked || track7Box.Checked || track8Box.Checked || track9Box.Checked || track10Box.Checked || track11Box.Checked || track12Box.Checked || track13Box.Checked || track14Box.Checked || track15Box.Checked)) {
                track0Box.Checked = true;
                track1Box.Checked = true;
                track2Box.Checked = true;
                track3Box.Checked = true;
                track4Box.Checked = true;
                track5Box.Checked = true;
                track6Box.Checked = true;
                track7Box.Checked = true;
                track8Box.Checked = true;
                track9Box.Checked = true;
                track10Box.Checked = true;
                track11Box.Checked = true;
                track12Box.Checked = true;
                track13Box.Checked = true;
                track14Box.Checked = true;
                track15Box.Checked = true;
            } else {
                track0Box.Checked = false;
                track1Box.Checked = false;
                track2Box.Checked = false;
                track3Box.Checked = false;
                track4Box.Checked = false;
                track5Box.Checked = true;
                track6Box.Checked = false;
                track7Box.Checked = false;
                track8Box.Checked = false;
                track9Box.Checked = false;
                track10Box.Checked = false;
                track11Box.Checked = false;
                track12Box.Checked = false;
                track13Box.Checked = false;
                track14Box.Checked = false;
                track15Box.Checked = false;
            }

        }

        private void Track6Solo(object sender, EventArgs e) {

            //Already solo'd.
            if (track6Box.Checked && !(track0Box.Checked || track1Box.Checked || track2Box.Checked || track3Box.Checked || track4Box.Checked || track5Box.Checked || track7Box.Checked || track8Box.Checked || track9Box.Checked || track10Box.Checked || track11Box.Checked || track12Box.Checked || track13Box.Checked || track14Box.Checked || track15Box.Checked)) {
                track0Box.Checked = true;
                track1Box.Checked = true;
                track2Box.Checked = true;
                track3Box.Checked = true;
                track4Box.Checked = true;
                track5Box.Checked = true;
                track6Box.Checked = true;
                track7Box.Checked = true;
                track8Box.Checked = true;
                track9Box.Checked = true;
                track10Box.Checked = true;
                track11Box.Checked = true;
                track12Box.Checked = true;
                track13Box.Checked = true;
                track14Box.Checked = true;
                track15Box.Checked = true;
            } else {
                track0Box.Checked = false;
                track1Box.Checked = false;
                track2Box.Checked = false;
                track3Box.Checked = false;
                track4Box.Checked = false;
                track5Box.Checked = false;
                track6Box.Checked = true;
                track7Box.Checked = false;
                track8Box.Checked = false;
                track9Box.Checked = false;
                track10Box.Checked = false;
                track11Box.Checked = false;
                track12Box.Checked = false;
                track13Box.Checked = false;
                track14Box.Checked = false;
                track15Box.Checked = false;
            }

        }

        private void Track7Solo(object sender, EventArgs e) {

            //Already solo'd.
            if (track7Box.Checked && !(track0Box.Checked || track1Box.Checked || track2Box.Checked || track3Box.Checked || track4Box.Checked || track5Box.Checked || track6Box.Checked || track8Box.Checked || track9Box.Checked || track10Box.Checked || track11Box.Checked || track12Box.Checked || track13Box.Checked || track14Box.Checked || track15Box.Checked)) {
                track0Box.Checked = true;
                track1Box.Checked = true;
                track2Box.Checked = true;
                track3Box.Checked = true;
                track4Box.Checked = true;
                track5Box.Checked = true;
                track6Box.Checked = true;
                track7Box.Checked = true;
                track8Box.Checked = true;
                track9Box.Checked = true;
                track10Box.Checked = true;
                track11Box.Checked = true;
                track12Box.Checked = true;
                track13Box.Checked = true;
                track14Box.Checked = true;
                track15Box.Checked = true;
            } else {
                track0Box.Checked = false;
                track1Box.Checked = false;
                track2Box.Checked = false;
                track3Box.Checked = false;
                track4Box.Checked = false;
                track5Box.Checked = false;
                track6Box.Checked = false;
                track7Box.Checked = true;
                track8Box.Checked = false;
                track9Box.Checked = false;
                track10Box.Checked = false;
                track11Box.Checked = false;
                track12Box.Checked = false;
                track13Box.Checked = false;
                track14Box.Checked = false;
                track15Box.Checked = false;
            }

        }

        private void Track8Solo(object sender, EventArgs e) {

            //Already solo'd.
            if (track8Box.Checked && !(track0Box.Checked || track1Box.Checked || track2Box.Checked || track3Box.Checked || track4Box.Checked || track5Box.Checked || track6Box.Checked || track7Box.Checked || track9Box.Checked || track10Box.Checked || track11Box.Checked || track12Box.Checked || track13Box.Checked || track14Box.Checked || track15Box.Checked)) {
                track0Box.Checked = true;
                track1Box.Checked = true;
                track2Box.Checked = true;
                track3Box.Checked = true;
                track4Box.Checked = true;
                track5Box.Checked = true;
                track6Box.Checked = true;
                track7Box.Checked = true;
                track8Box.Checked = true;
                track9Box.Checked = true;
                track10Box.Checked = true;
                track11Box.Checked = true;
                track12Box.Checked = true;
                track13Box.Checked = true;
                track14Box.Checked = true;
                track15Box.Checked = true;
            } else {
                track0Box.Checked = false;
                track1Box.Checked = false;
                track2Box.Checked = false;
                track3Box.Checked = false;
                track4Box.Checked = false;
                track5Box.Checked = false;
                track6Box.Checked = false;
                track7Box.Checked = false;
                track8Box.Checked = true;
                track9Box.Checked = false;
                track10Box.Checked = false;
                track11Box.Checked = false;
                track12Box.Checked = false;
                track13Box.Checked = false;
                track14Box.Checked = false;
                track15Box.Checked = false;
            }

        }

        private void Track9Solo(object sender, EventArgs e) {

            //Already solo'd.
            if (track9Box.Checked && !(track0Box.Checked || track1Box.Checked || track2Box.Checked || track3Box.Checked || track4Box.Checked || track5Box.Checked || track6Box.Checked || track7Box.Checked || track8Box.Checked || track10Box.Checked || track11Box.Checked || track12Box.Checked || track13Box.Checked || track14Box.Checked || track15Box.Checked)) {
                track0Box.Checked = true;
                track1Box.Checked = true;
                track2Box.Checked = true;
                track3Box.Checked = true;
                track4Box.Checked = true;
                track5Box.Checked = true;
                track6Box.Checked = true;
                track7Box.Checked = true;
                track8Box.Checked = true;
                track9Box.Checked = true;
                track10Box.Checked = true;
                track11Box.Checked = true;
                track12Box.Checked = true;
                track13Box.Checked = true;
                track14Box.Checked = true;
                track15Box.Checked = true;
            } else {
                track0Box.Checked = false;
                track1Box.Checked = false;
                track2Box.Checked = false;
                track3Box.Checked = false;
                track4Box.Checked = false;
                track5Box.Checked = false;
                track6Box.Checked = false;
                track7Box.Checked = false;
                track8Box.Checked = false;
                track9Box.Checked = true;
                track10Box.Checked = false;
                track11Box.Checked = false;
                track12Box.Checked = false;
                track13Box.Checked = false;
                track14Box.Checked = false;
                track15Box.Checked = false;
            }

        }

        private void Track10Solo(object sender, EventArgs e) {

            //Already solo'd.
            if (track10Box.Checked && !(track0Box.Checked || track1Box.Checked || track2Box.Checked || track3Box.Checked || track4Box.Checked || track5Box.Checked || track6Box.Checked || track7Box.Checked || track8Box.Checked || track9Box.Checked || track11Box.Checked || track12Box.Checked || track13Box.Checked || track14Box.Checked || track15Box.Checked)) {
                track0Box.Checked = true;
                track1Box.Checked = true;
                track2Box.Checked = true;
                track3Box.Checked = true;
                track4Box.Checked = true;
                track5Box.Checked = true;
                track6Box.Checked = true;
                track7Box.Checked = true;
                track8Box.Checked = true;
                track9Box.Checked = true;
                track10Box.Checked = true;
                track11Box.Checked = true;
                track12Box.Checked = true;
                track13Box.Checked = true;
                track14Box.Checked = true;
                track15Box.Checked = true;
            } else {
                track0Box.Checked = false;
                track1Box.Checked = false;
                track2Box.Checked = false;
                track3Box.Checked = false;
                track4Box.Checked = false;
                track5Box.Checked = false;
                track6Box.Checked = false;
                track7Box.Checked = false;
                track8Box.Checked = false;
                track9Box.Checked = false;
                track10Box.Checked = true;
                track11Box.Checked = false;
                track12Box.Checked = false;
                track13Box.Checked = false;
                track14Box.Checked = false;
                track15Box.Checked = false;
            }

        }

        private void Track11Solo(object sender, EventArgs e) {

            //Already solo'd.
            if (track11Box.Checked && !(track0Box.Checked || track1Box.Checked || track2Box.Checked || track3Box.Checked || track4Box.Checked || track5Box.Checked || track6Box.Checked || track7Box.Checked || track8Box.Checked || track9Box.Checked || track10Box.Checked || track12Box.Checked || track13Box.Checked || track14Box.Checked || track15Box.Checked)) {
                track0Box.Checked = true;
                track1Box.Checked = true;
                track2Box.Checked = true;
                track3Box.Checked = true;
                track4Box.Checked = true;
                track5Box.Checked = true;
                track6Box.Checked = true;
                track7Box.Checked = true;
                track8Box.Checked = true;
                track9Box.Checked = true;
                track10Box.Checked = true;
                track11Box.Checked = true;
                track12Box.Checked = true;
                track13Box.Checked = true;
                track14Box.Checked = true;
                track15Box.Checked = true;
            } else {
                track0Box.Checked = false;
                track1Box.Checked = false;
                track2Box.Checked = false;
                track3Box.Checked = false;
                track4Box.Checked = false;
                track5Box.Checked = false;
                track6Box.Checked = false;
                track7Box.Checked = false;
                track8Box.Checked = false;
                track9Box.Checked = false;
                track10Box.Checked = false;
                track11Box.Checked = true;
                track12Box.Checked = false;
                track13Box.Checked = false;
                track14Box.Checked = false;
                track15Box.Checked = false;
            }

        }

        private void Track12Solo(object sender, EventArgs e) {

            //Already solo'd.
            if (track12Box.Checked && !(track0Box.Checked || track1Box.Checked || track2Box.Checked || track3Box.Checked || track4Box.Checked || track5Box.Checked || track6Box.Checked || track7Box.Checked || track8Box.Checked || track9Box.Checked || track10Box.Checked || track11Box.Checked || track13Box.Checked || track14Box.Checked || track15Box.Checked)) {
                track0Box.Checked = true;
                track1Box.Checked = true;
                track2Box.Checked = true;
                track3Box.Checked = true;
                track4Box.Checked = true;
                track5Box.Checked = true;
                track6Box.Checked = true;
                track7Box.Checked = true;
                track8Box.Checked = true;
                track9Box.Checked = true;
                track10Box.Checked = true;
                track11Box.Checked = true;
                track12Box.Checked = true;
                track13Box.Checked = true;
                track14Box.Checked = true;
                track15Box.Checked = true;
            } else {
                track0Box.Checked = false;
                track1Box.Checked = false;
                track2Box.Checked = false;
                track3Box.Checked = false;
                track4Box.Checked = false;
                track5Box.Checked = false;
                track6Box.Checked = false;
                track7Box.Checked = false;
                track8Box.Checked = false;
                track9Box.Checked = false;
                track10Box.Checked = false;
                track11Box.Checked = false;
                track12Box.Checked = true;
                track13Box.Checked = false;
                track14Box.Checked = false;
                track15Box.Checked = false;
            }

        }

        private void Track13Solo(object sender, EventArgs e) {

            //Already solo'd.
            if (track13Box.Checked && !(track0Box.Checked || track1Box.Checked || track2Box.Checked || track3Box.Checked || track4Box.Checked || track5Box.Checked || track6Box.Checked || track7Box.Checked || track8Box.Checked || track9Box.Checked || track10Box.Checked || track11Box.Checked || track12Box.Checked || track14Box.Checked || track15Box.Checked)) {
                track0Box.Checked = true;
                track1Box.Checked = true;
                track2Box.Checked = true;
                track3Box.Checked = true;
                track4Box.Checked = true;
                track5Box.Checked = true;
                track6Box.Checked = true;
                track7Box.Checked = true;
                track8Box.Checked = true;
                track9Box.Checked = true;
                track10Box.Checked = true;
                track11Box.Checked = true;
                track12Box.Checked = true;
                track13Box.Checked = true;
                track14Box.Checked = true;
                track15Box.Checked = true;
            } else {
                track0Box.Checked = false;
                track1Box.Checked = false;
                track2Box.Checked = false;
                track3Box.Checked = false;
                track4Box.Checked = false;
                track5Box.Checked = false;
                track6Box.Checked = false;
                track7Box.Checked = false;
                track8Box.Checked = false;
                track9Box.Checked = false;
                track10Box.Checked = false;
                track11Box.Checked = false;
                track12Box.Checked = false;
                track13Box.Checked = true;
                track14Box.Checked = false;
                track15Box.Checked = false;
            }

        }

        private void Track14Solo(object sender, EventArgs e) {

            //Already solo'd.
            if (track14Box.Checked && !(track0Box.Checked || track1Box.Checked || track2Box.Checked || track3Box.Checked || track4Box.Checked || track5Box.Checked || track6Box.Checked || track7Box.Checked || track8Box.Checked || track9Box.Checked || track10Box.Checked || track11Box.Checked || track12Box.Checked || track13Box.Checked || track15Box.Checked)) {
                track0Box.Checked = true;
                track1Box.Checked = true;
                track2Box.Checked = true;
                track3Box.Checked = true;
                track4Box.Checked = true;
                track5Box.Checked = true;
                track6Box.Checked = true;
                track7Box.Checked = true;
                track8Box.Checked = true;
                track9Box.Checked = true;
                track10Box.Checked = true;
                track11Box.Checked = true;
                track12Box.Checked = true;
                track13Box.Checked = true;
                track14Box.Checked = true;
                track15Box.Checked = true;
            } else {
                track0Box.Checked = false;
                track1Box.Checked = false;
                track2Box.Checked = false;
                track3Box.Checked = false;
                track4Box.Checked = false;
                track5Box.Checked = false;
                track6Box.Checked = false;
                track7Box.Checked = false;
                track8Box.Checked = false;
                track9Box.Checked = false;
                track10Box.Checked = false;
                track11Box.Checked = false;
                track12Box.Checked = false;
                track13Box.Checked = false;
                track14Box.Checked = true;
                track15Box.Checked = false;
            }

        }

        private void Track15Solo(object sender, EventArgs e) {

            //Already solo'd.
            if (track15Box.Checked && !(track0Box.Checked || track1Box.Checked || track2Box.Checked || track3Box.Checked || track4Box.Checked || track5Box.Checked || track6Box.Checked || track7Box.Checked || track8Box.Checked || track9Box.Checked || track10Box.Checked || track11Box.Checked || track12Box.Checked || track13Box.Checked || track14Box.Checked)) {
                track0Box.Checked = true;
                track1Box.Checked = true;
                track2Box.Checked = true;
                track3Box.Checked = true;
                track4Box.Checked = true;
                track5Box.Checked = true;
                track6Box.Checked = true;
                track7Box.Checked = true;
                track8Box.Checked = true;
                track9Box.Checked = true;
                track10Box.Checked = true;
                track11Box.Checked = true;
                track12Box.Checked = true;
                track13Box.Checked = true;
                track14Box.Checked = true;
                track15Box.Checked = true;
            } else {
                track0Box.Checked = false;
                track1Box.Checked = false;
                track2Box.Checked = false;
                track3Box.Checked = false;
                track4Box.Checked = false;
                track5Box.Checked = false;
                track6Box.Checked = false;
                track7Box.Checked = false;
                track8Box.Checked = false;
                track9Box.Checked = false;
                track10Box.Checked = false;
                track11Box.Checked = false;
                track12Box.Checked = false;
                track13Box.Checked = false;
                track14Box.Checked = false;
                track15Box.Checked = true;
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

    }

}
