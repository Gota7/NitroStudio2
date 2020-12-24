using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;
using System.Diagnostics;
using GotaSequenceLib;
using GotaSoundIO.IO;
using Multimedia.UI;
using System.Drawing;
using NitroFileLoader;
using GotaSoundBank.SF2;
using GotaSoundBank.DLS;

namespace NitroStudio2 {

    /// <summary>
    /// An editor base for files.
    /// </summary>
    public abstract class EditorBase : Form {

        /// <summary>
        /// File type to use.
        /// </summary>
        public IOFile File;

        /// <summary>
        /// External file.
        /// </summary>
        public IOFile ExtFile;

        /// <summary>
        /// File name.
        /// </summary>
        public string FileName;

        /// <summary>
        /// File path.
        /// </summary>
        public string FilePath;

        /// <summary>
        /// Description of the extension.
        /// </summary>
        public string ExtensionDescription;

        /// <summary>
        /// Extension of format.
        /// </summary>
        public string Extension;

        /// <summary>
        /// Editor name.
        /// </summary>
        public string EditorName;

        /// <summary>
        /// Current file open.
        /// </summary>
        public bool FileOpen;

        /// <summary>
        /// File type.
        /// </summary>
        public Type FileType;

        /// <summary>
        /// Other editor.
        /// </summary>
        public EditorBase OtherEditor;

        /// <summary>
        /// Writing info.
        /// </summary>
        public bool WritingInfo;

        /// <summary>
        /// The current note that is down.
        /// </summary>
        public Notes NoteDown;
        public Panel pnlPianoKeys;
        private Multimedia.UI.PianoKey pkeyC7;
        private Multimedia.UI.PianoKey pkeyE7;
        private Multimedia.UI.PianoKey pkeyCSharp7;
        private Multimedia.UI.PianoKey pkeyD7;
        private Multimedia.UI.PianoKey pkeyDSharp7;
        private Multimedia.UI.PianoKey pkeyF7;
        private Multimedia.UI.PianoKey pkeyFSharp7;
        private Multimedia.UI.PianoKey pkeyG7;
        private Multimedia.UI.PianoKey pkeyGSharp7;
        private Multimedia.UI.PianoKey pkeyA7;
        private Multimedia.UI.PianoKey pkeyASharp7;
        private Multimedia.UI.PianoKey pkeyB7;
        private Multimedia.UI.PianoKey pkeyC6;
        private Multimedia.UI.PianoKey pkeyE6;
        private Multimedia.UI.PianoKey pkeyCSharp6;
        private Multimedia.UI.PianoKey pkeyD6;
        private Multimedia.UI.PianoKey pkeyDSharp6;
        private Multimedia.UI.PianoKey pkeyF6;
        private Multimedia.UI.PianoKey pkeyFSharp6;
        private Multimedia.UI.PianoKey pkeyG6;
        private Multimedia.UI.PianoKey pkeyGSharp6;
        private Multimedia.UI.PianoKey pkeyA6;
        private Multimedia.UI.PianoKey pkeyASharp6;
        private Multimedia.UI.PianoKey pkeyB6;
        private Multimedia.UI.PianoKey pkeyC1;
        private Multimedia.UI.PianoKey pkeyCSharp1;
        private Multimedia.UI.PianoKey pkeyD1;
        private Multimedia.UI.PianoKey pkeyDSharp1;
        private Multimedia.UI.PianoKey pkeyE1;
        private Multimedia.UI.PianoKey pkeyF1;
        private Multimedia.UI.PianoKey pkeyFSharp1;
        private Multimedia.UI.PianoKey pkeyG1;
        private Multimedia.UI.PianoKey pkeyGSharp1;
        private Multimedia.UI.PianoKey pkeyA1;
        private Multimedia.UI.PianoKey pkeyASharp1;
        private Multimedia.UI.PianoKey pkeyB1;
        private Multimedia.UI.PianoKey pkeyC2;
        private Multimedia.UI.PianoKey pkeyCSharp2;
        private Multimedia.UI.PianoKey pkeyD2;
        private Multimedia.UI.PianoKey pkeyDSharp2;
        private Multimedia.UI.PianoKey pkeyE2;
        private Multimedia.UI.PianoKey pkeyF2;
        private Multimedia.UI.PianoKey pkeyFSharp2;
        private Multimedia.UI.PianoKey pkeyG2;
        private Multimedia.UI.PianoKey pkeyGSharp2;
        private Multimedia.UI.PianoKey pkeyA2;
        private Multimedia.UI.PianoKey pkeyASharp2;
        private Multimedia.UI.PianoKey pkeyB2;
        private Multimedia.UI.PianoKey pkeyC3;
        private Multimedia.UI.PianoKey pkeyCSharp3;
        private Multimedia.UI.PianoKey pkeyD3;
        private Multimedia.UI.PianoKey pkeyDSharp3;
        private Multimedia.UI.PianoKey pkeyE3;
        private Multimedia.UI.PianoKey pkeyF3;
        private Multimedia.UI.PianoKey pkeyFSharp3;
        private Multimedia.UI.PianoKey pkeyG3;
        private Multimedia.UI.PianoKey pkeyGSharp3;
        private Multimedia.UI.PianoKey pkeyA3;
        private Multimedia.UI.PianoKey pkeyASharp3;
        private Multimedia.UI.PianoKey pkeyB3;
        private Multimedia.UI.PianoKey pkeyC4;
        private Multimedia.UI.PianoKey pkeyCSharp4;
        private Multimedia.UI.PianoKey pkeyD4;
        private Multimedia.UI.PianoKey pkeyDSharp4;
        private Multimedia.UI.PianoKey pkeyE4;
        private Multimedia.UI.PianoKey pkeyF4;
        private Multimedia.UI.PianoKey pkeyFSharp4;
        private Multimedia.UI.PianoKey pkeyG4;
        private Multimedia.UI.PianoKey pkeyGSharp4;
        private Multimedia.UI.PianoKey pkeyA4;
        private Multimedia.UI.PianoKey pkeyASharp4;
        private Multimedia.UI.PianoKey pkeyB4;
        private Multimedia.UI.PianoKey pkeyC5;
        private Multimedia.UI.PianoKey pkeyCSharp5;
        private Multimedia.UI.PianoKey pkeyD5;
        private Multimedia.UI.PianoKey pkeyDSharp5;
        private Multimedia.UI.PianoKey pkeyE5;
        private Multimedia.UI.PianoKey pkeyF5;
        private Multimedia.UI.PianoKey pkeyFSharp5;
        private Multimedia.UI.PianoKey pkeyG5;
        private Multimedia.UI.PianoKey pkeyGSharp5;
        private Multimedia.UI.PianoKey pkeyA5;
        private Multimedia.UI.PianoKey pkeyASharp5;
        private Multimedia.UI.PianoKey pkeyB5;
        private Multimedia.UI.PianoKey pkeyC8;
        public ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem getHelpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem aboutNitroStudio2ToolStripMenuItem;
        private ToolStripMenuItem bankGeneratorToolStripMenuItem;
        private ToolStripMenuItem exportSDKProjectToolStripMenuItem;
        public Panel settingsPanel;
        private Label label2;
        public CheckBox writeNamesBox;
        private Label label3;
        public ComboBox seqImportModeBox;
        public ComboBox seqExportModeBox;
        private Label label4;
        public Panel indexPanel;
        private Label label5;
        public NumericUpDown itemIndexBox;
        public Button swapAtIndexButton;
        public Panel forceUniqueFilePanel;
        public CheckBox forceUniqueFileBox;
        private Label label8;
        public Panel warPanel;
        public CheckBox loadIndividuallyBox;
        private Label label9;
        public Panel blankPanel;
        public Panel bankPanel;
        private TableLayoutPanel tableLayoutPanel2;
        public ComboBox bnkWar0ComboBox;
        private Label label6;
        public NumericUpDown bnkWar0Box;
        private TableLayoutPanel tableLayoutPanel5;
        public ComboBox bnkWar3ComboBox;
        public NumericUpDown bnkWar3Box;
        private Label label11;
        private TableLayoutPanel tableLayoutPanel4;
        public ComboBox bnkWar2ComboBox;
        public NumericUpDown bnkWar2Box;
        private Label label10;
        private TableLayoutPanel tableLayoutPanel3;
        public ComboBox bnkWar1ComboBox;
        public NumericUpDown bnkWar1Box;
        private Label label7;
        public Panel grpPanel;
        public DataGridView grpEntries;
        private DataGridViewComboBoxColumn item;
        private DataGridViewComboBoxColumn loadFlags;
        public Panel streamPlayerPanel;
        private TableLayoutPanel tableLayoutPanel6;
        public Label leftChannelLabel;
        public ComboBox stmPlayerChannelType;
        private Label label12;
        public Label rightChannelLabel;
        public NumericUpDown stmPlayerLeftChannelBox;
        public NumericUpDown stmPlayerRightChannelBox;
        public Panel stmPanel;
        private Label label13;
        public NumericUpDown stmPriorityBox;
        private Label label14;
        public NumericUpDown stmVolumeBox;
        private Label label15;
        private TableLayoutPanel tableLayoutPanel7;
        public ComboBox stmPlayerComboBox;
        public NumericUpDown stmPlayerBox;
        public CheckBox stmMonoToStereoBox;
        private Label label16;
        public Panel playerPanel;
        public NumericUpDown playerMaxSequencesBox;
        private Label label17;
        public NumericUpDown playerHeapSizeBox;
        private Label label18;
        private Label label19;
        private TableLayoutPanel tableLayoutPanel8;
        public CheckBox playerFlag15Box;
        public CheckBox playerFlag14Box;
        public CheckBox playerFlag13Box;
        public CheckBox playerFlag12Box;
        public CheckBox playerFlag11Box;
        public CheckBox playerFlag10Box;
        public CheckBox playerFlag9Box;
        public CheckBox playerFlag8Box;
        public CheckBox playerFlag7Box;
        public CheckBox playerFlag6Box;
        public CheckBox playerFlag5Box;
        public CheckBox playerFlag4Box;
        public CheckBox playerFlag3Box;
        public CheckBox playerFlag2Box;
        public CheckBox playerFlag1Box;
        public CheckBox playerFlag0Box;
        public Panel kermalisSoundPlayerPanel;
        public Label soundPlayerLabel;
        private TableLayoutPanel tableLayoutPanel9;
        public Button kermalisStopButton;
        public Button kermalisPauseButton;
        public TrackBar kermalisVolumeSlider;
        public Button kermalisPlayButton;
        private Label label21;
        public CheckBox kermalisLoopBox;
        private Label label22;
        public Panel seqPanel;
        private Label label23;
        private TableLayoutPanel tableLayoutPanel10;
        public ComboBox seqBankComboBox;
        public NumericUpDown seqBankBox;
        private Label label24;
        public NumericUpDown seqVolumeBox;
        public NumericUpDown seqChannelPriorityBox;
        private Label label25;
        public NumericUpDown seqPlayerPriorityBox;
        private Label label26;
        private Label label27;
        private TableLayoutPanel tableLayoutPanel11;
        public ComboBox seqPlayerComboBox;
        public NumericUpDown seqPlayerBox;
        public Panel seqBankPanel;
        private Label label28;
        private TableLayoutPanel tableLayoutPanel12;
        public ComboBox seqEditorBankComboBox;
        public NumericUpDown seqEditorBankBox;
        public Panel seqArcPanel;
        public Button seqArcOpenFileButton;
        public Panel seqArcSeqPanel;
        private Label label29;
        private TableLayoutPanel tableLayoutPanel13;
        public ComboBox seqArcSeqComboBox;
        public NumericUpDown seqArcSeqBox;
        public Panel bankEditorPanel;
        private Label label30;
        private TableLayoutPanel tableLayoutPanel14;
        public RadioButton directBox;
        public RadioButton keySplitBox;
        public RadioButton drumSetBox;
        public Label drumSetRangeStartLabel;
        private TableLayoutPanel tableLayoutPanel15;
        public ComboBox drumSetStartRangeComboBox;
        public NumericUpDown drumSetStartRangeBox;
        private Label label32;
        public DataGridView bankRegions;
        public Panel bankEditorWars;
        private TableLayoutPanel tableLayoutPanel16;
        public ComboBox war3ComboBox;
        public NumericUpDown war3Box;
        private Label label31;
        private TableLayoutPanel tableLayoutPanel17;
        public ComboBox war2ComboBox;
        public NumericUpDown war2Box;
        private Label label33;
        private TableLayoutPanel tableLayoutPanel18;
        public ComboBox war1ComboBox;
        public NumericUpDown war1Box;
        private Label label34;
        private TableLayoutPanel tableLayoutPanel19;
        public ComboBox war0ComboBox;
        public NumericUpDown war0Box;
        private Label label35;
        private ToolStripMenuItem sequenceEditorToolStripMenuItem;
        private ToolStripMenuItem sequenceArchiveEditorToolStripMenuItem;
        private ToolStripMenuItem bankEditorToolStripMenuItem;
        private ToolStripMenuItem createStreamToolStripMenuItem;
        private ToolStripMenuItem creaveWaveToolStripMenuItem;
        private ToolStripMenuItem waveArchiveEditorToolStripMenuItem;
        public ToolStripStatusLabel currentNote;
        private TableLayoutPanel tableLayoutPanel1;
        public CheckBox track0Box;
        public PictureBox track0Picture;
        public Button track0Solo;
        private TableLayoutPanel tableLayoutPanel20;
        private TableLayoutPanel tableLayoutPanel27;
        public CheckBox track12Box;
        public PictureBox track12Picture;
        public Button track12Solo;
        private TableLayoutPanel tableLayoutPanel26;
        public CheckBox track10Box;
        public PictureBox track10Picture;
        public Button track10Solo;
        private TableLayoutPanel tableLayoutPanel25;
        public CheckBox track8Box;
        public PictureBox track8Picture;
        public Button track8Solo;
        private TableLayoutPanel tableLayoutPanel24;
        public CheckBox track6Box;
        public PictureBox track6Picture;
        public Button track6Solo;
        private TableLayoutPanel tableLayoutPanel23;
        public CheckBox track4Box;
        public PictureBox track4Picture;
        public Button track4Solo;
        private TableLayoutPanel tableLayoutPanel22;
        public CheckBox track2Box;
        public PictureBox track2Picture;
        public Button track2Solo;
        private TableLayoutPanel tableLayoutPanel21;
        public CheckBox track1Box;
        public PictureBox track1Picture;
        public Button track1Solo;
        private TableLayoutPanel tableLayoutPanel35;
        public CheckBox track15Box;
        public PictureBox track15Picture;
        public Button track15Solo;
        private TableLayoutPanel tableLayoutPanel34;
        public CheckBox track13Box;
        public PictureBox track13Picture;
        public Button track13Solo;
        private TableLayoutPanel tableLayoutPanel33;
        public CheckBox track11Box;
        public PictureBox track11Picture;
        public Button track11Solo;
        private TableLayoutPanel tableLayoutPanel32;
        public CheckBox track9Box;
        public PictureBox track9Picture;
        public Button track9Solo;
        private TableLayoutPanel tableLayoutPanel31;
        public CheckBox track7Box;
        public PictureBox track7Picture;
        public Button track7Solo;
        private TableLayoutPanel tableLayoutPanel30;
        public CheckBox track5Box;
        public PictureBox track5Picture;
        public Button track5Solo;
        private TableLayoutPanel tableLayoutPanel29;
        public CheckBox track3Box;
        public PictureBox track3Picture;
        public Button track3Solo;
        private TableLayoutPanel tableLayoutPanel28;
        public CheckBox track14Box;
        public PictureBox track14Picture;
        public Button track14Solo;
        private DataGridViewButtonColumn playSampleButton;
        private DataGridViewComboBoxColumn endNote;
        private DataGridViewComboBoxColumn instrumentType;
        private DataGridViewTextBoxColumn waveId;
        private DataGridViewTextBoxColumn waveArchiveId;
        private DataGridViewComboBoxColumn baseNote;
        private DataGridViewTextBoxColumn attack;
        private DataGridViewTextBoxColumn decay;
        private DataGridViewTextBoxColumn sustain;
        private DataGridViewTextBoxColumn release;
        private DataGridViewTextBoxColumn pan;
        private ToolStripMenuItem sF2ToDLSToolStripMenuItem;
        private ToolStripMenuItem dLSToSF2ToolStripMenuItem;
        public TrackBar kermalisPosition;
        private ToolStripMenuItem batchExportMIDIDLSSF2ToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel36;
        public Button exportWavButton;
        public Button exportMidiButton;

        /// <summary>
        /// Main window.
        /// </summary>
        public static MainWindow MainWindow;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fileType">Type of file.</param>
        /// <param name="extensionDescription">Description of the extension.</param>
        /// <param name="extension">File extension type.</param>
        /// <param name="editorName">Editor name.</param>
        /// <param name="mainWindow">Main window.</param>
        public EditorBase(Type fileType, string extensionDescription, string extension, string editorName, MainWindow mainWindow) {

            //Initialize component.
            InitializeComponent();

            //Set main window.
            MainWindow = mainWindow;

            //Set file type.
            FileType = fileType;

            //Extension description.
            ExtensionDescription = extensionDescription;

            //Extension.
            Extension = extension;

            //Editor name.
            EditorName = editorName;
            Text = EditorName;

            //Update nodes.
            UpdateNodes();

            //Do info stuff.
            DoInfoStuff();

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fileType">Type of file.</param>
        /// <param name="extensionDescription">Description of the extension.</param>
        /// <param name="extension">File extension type.</param>
        /// <param name="editorName">Editor name.</param>
        /// <param name="fileToOpen">File to open.</param>
        /// <param name="mainWindow">Main window.</param>
        public EditorBase(Type fileType, string extensionDescription, string extension, string editorName, string fileToOpen, MainWindow mainWindow) {

            //Initialize component.
            InitializeComponent();

            //Set main window.
            MainWindow = mainWindow;

            //Set file type.
            FileType = fileType;

            //Extension description.
            ExtensionDescription = extensionDescription;

            //Extension.
            Extension = extension;

            //Editor name.
            EditorName = editorName;

            //Open file.
            File = (IOFile)Activator.CreateInstance(FileType);
            FilePath = fileToOpen;
            Text = EditorName + " - " + Path.GetFileName(fileToOpen);
            FileOpen = true;
            FileName = Path.GetFileNameWithoutExtension(FilePath);

            //Read data.
            File.Read(fileToOpen);

            //Update.
            UpdateNodes();
            DoInfoStuff();

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fileType">Type of file.</param>
        /// <param name="extensionDescription">Description of the extension.</param>
        /// <param name="extension">File extension type.</param>
        /// <param name="editorName">Editor name.</param>
        /// <param name="fileToOpen">File to open.</param>
        /// <param name="mainWindow">Main window.</param>
        /// <param name="fileName">Filename.</param>
        public EditorBase(Type fileType, string extensionDescription, string extension, string editorName, IOFile fileToOpen, MainWindow mainWindow, string fileName) {

            //Initialize component.
            InitializeComponent();

            //Set main window.
            MainWindow = mainWindow;

            //Set file type.
            FileType = fileType;

            //Extension description.
            ExtensionDescription = extensionDescription;

            //Extension.
            Extension = extension;

            //Editor name.
            EditorName = editorName;

            //Open file.
            ExtFile = fileToOpen;
            File = (IOFile)Activator.CreateInstance(ExtFile.GetType());
            File.Read(ExtFile.Write());
            FilePath = "";
            string name = fileName;
            if (name == null) {
                name = "{ Null File Name }";
            }
            Text = EditorName + " - " + name + ".s" + extension;
            FileOpen = true;
            FileName = fileName;

            //Update.
            UpdateNodes();
            DoInfoStuff();

        }
        public MenuStrip menuStrip;
        public ToolStripMenuItem newToolStripMenuItem;
        public ToolStripMenuItem openToolStripMenuItem;
        public ToolStripMenuItem saveToolStripMenuItem;
        public ToolStripMenuItem saveAsToolStripMenuItem;
        public ToolStripMenuItem closeToolStripMenuItem;
        public ToolStripMenuItem quitToolStripMenuItem;
        public ToolStripMenuItem editToolStripMenuItem;
        public ToolStripMenuItem blankFileToolStripMenuItem;
        public ToolStripMenuItem importFileToolStripMenuItem;
        public ToolStripMenuItem exportFileToolStripMenuItem;
        public SplitContainer splitContainer1;
        public TreeView tree;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private StatusStrip statusStrip;
        public ToolStripStatusLabel status;
        public ImageList treeIcons;
        private System.ComponentModel.IContainer components;
        public ContextMenuStrip rootMenu;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem expandToolStripMenuItem;
        private ToolStripMenuItem collapseToolStripMenuItem;
        public Panel noInfoPanel;
        private Label label1;
        private ToolTip toolTip;
        public ToolStripMenuItem fileMenu;
        public ContextMenuStrip nodeMenu;
        private ToolStripMenuItem addAboveToolStripMenuItem1;
        private ToolStripMenuItem addBelowToolStripMenuItem1;
        private ToolStripMenuItem moveUpToolStripMenuItem1;
        private ToolStripMenuItem moveDownToolStripMenuItem1;
        private ToolStripMenuItem replaceFileToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem1;
        private ToolStripMenuItem exportToolStripMenuItem1;
        public Panel sequenceEditorPanel;
        public Scintilla sequenceEditor;
        private BindingSource bindingSource1;
        public ContextMenuStrip sarEntryMenu;
        private ToolStripMenuItem sarAddAbove;
        private ToolStripMenuItem sarAddBelow;
        private ToolStripMenuItem sarMoveUp;
        private ToolStripMenuItem sarMoveDown;
        private ToolStripMenuItem sarReplace;
        private ToolStripMenuItem sarExport;
        private ToolStripMenuItem sarRename;
        private ToolStripMenuItem sarDelete;

        public void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorBase));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("File Information", 10, 10);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blankFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sequenceEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sequenceArchiveEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bankEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waveArchiveEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bankGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creaveWaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createStreamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSDKProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sF2ToDLSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dLSToSF2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batchExportMIDIDLSSF2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutNitroStudio2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.seqBankPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel36 = new System.Windows.Forms.TableLayoutPanel();
            this.exportWavButton = new System.Windows.Forms.Button();
            this.exportMidiButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel20 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel35 = new System.Windows.Forms.TableLayoutPanel();
            this.track15Box = new System.Windows.Forms.CheckBox();
            this.track15Picture = new System.Windows.Forms.PictureBox();
            this.track15Solo = new System.Windows.Forms.Button();
            this.tableLayoutPanel34 = new System.Windows.Forms.TableLayoutPanel();
            this.track13Box = new System.Windows.Forms.CheckBox();
            this.track13Picture = new System.Windows.Forms.PictureBox();
            this.track13Solo = new System.Windows.Forms.Button();
            this.tableLayoutPanel33 = new System.Windows.Forms.TableLayoutPanel();
            this.track11Box = new System.Windows.Forms.CheckBox();
            this.track11Picture = new System.Windows.Forms.PictureBox();
            this.track11Solo = new System.Windows.Forms.Button();
            this.tableLayoutPanel32 = new System.Windows.Forms.TableLayoutPanel();
            this.track9Box = new System.Windows.Forms.CheckBox();
            this.track9Picture = new System.Windows.Forms.PictureBox();
            this.track9Solo = new System.Windows.Forms.Button();
            this.tableLayoutPanel31 = new System.Windows.Forms.TableLayoutPanel();
            this.track7Box = new System.Windows.Forms.CheckBox();
            this.track7Picture = new System.Windows.Forms.PictureBox();
            this.track7Solo = new System.Windows.Forms.Button();
            this.tableLayoutPanel30 = new System.Windows.Forms.TableLayoutPanel();
            this.track5Box = new System.Windows.Forms.CheckBox();
            this.track5Picture = new System.Windows.Forms.PictureBox();
            this.track5Solo = new System.Windows.Forms.Button();
            this.tableLayoutPanel29 = new System.Windows.Forms.TableLayoutPanel();
            this.track3Box = new System.Windows.Forms.CheckBox();
            this.track3Picture = new System.Windows.Forms.PictureBox();
            this.track3Solo = new System.Windows.Forms.Button();
            this.tableLayoutPanel28 = new System.Windows.Forms.TableLayoutPanel();
            this.track14Box = new System.Windows.Forms.CheckBox();
            this.track14Picture = new System.Windows.Forms.PictureBox();
            this.track14Solo = new System.Windows.Forms.Button();
            this.tableLayoutPanel27 = new System.Windows.Forms.TableLayoutPanel();
            this.track12Box = new System.Windows.Forms.CheckBox();
            this.track12Picture = new System.Windows.Forms.PictureBox();
            this.track12Solo = new System.Windows.Forms.Button();
            this.tableLayoutPanel26 = new System.Windows.Forms.TableLayoutPanel();
            this.track10Box = new System.Windows.Forms.CheckBox();
            this.track10Picture = new System.Windows.Forms.PictureBox();
            this.track10Solo = new System.Windows.Forms.Button();
            this.tableLayoutPanel25 = new System.Windows.Forms.TableLayoutPanel();
            this.track8Box = new System.Windows.Forms.CheckBox();
            this.track8Picture = new System.Windows.Forms.PictureBox();
            this.track8Solo = new System.Windows.Forms.Button();
            this.tableLayoutPanel24 = new System.Windows.Forms.TableLayoutPanel();
            this.track6Box = new System.Windows.Forms.CheckBox();
            this.track6Picture = new System.Windows.Forms.PictureBox();
            this.track6Solo = new System.Windows.Forms.Button();
            this.tableLayoutPanel23 = new System.Windows.Forms.TableLayoutPanel();
            this.track4Box = new System.Windows.Forms.CheckBox();
            this.track4Picture = new System.Windows.Forms.PictureBox();
            this.track4Solo = new System.Windows.Forms.Button();
            this.tableLayoutPanel22 = new System.Windows.Forms.TableLayoutPanel();
            this.track2Box = new System.Windows.Forms.CheckBox();
            this.track2Picture = new System.Windows.Forms.PictureBox();
            this.track2Solo = new System.Windows.Forms.Button();
            this.tableLayoutPanel21 = new System.Windows.Forms.TableLayoutPanel();
            this.track1Box = new System.Windows.Forms.CheckBox();
            this.track1Picture = new System.Windows.Forms.PictureBox();
            this.track1Solo = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.track0Box = new System.Windows.Forms.CheckBox();
            this.track0Picture = new System.Windows.Forms.PictureBox();
            this.track0Solo = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.seqEditorBankComboBox = new System.Windows.Forms.ComboBox();
            this.seqEditorBankBox = new System.Windows.Forms.NumericUpDown();
            this.bankEditorPanel = new System.Windows.Forms.Panel();
            this.bankRegions = new System.Windows.Forms.DataGridView();
            this.playSampleButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.endNote = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.instrumentType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.waveId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.waveArchiveId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baseNote = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.attack = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.decay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sustain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.release = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label32 = new System.Windows.Forms.Label();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.drumSetStartRangeComboBox = new System.Windows.Forms.ComboBox();
            this.drumSetStartRangeBox = new System.Windows.Forms.NumericUpDown();
            this.drumSetRangeStartLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.keySplitBox = new System.Windows.Forms.RadioButton();
            this.drumSetBox = new System.Windows.Forms.RadioButton();
            this.directBox = new System.Windows.Forms.RadioButton();
            this.label30 = new System.Windows.Forms.Label();
            this.seqArcSeqPanel = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.seqArcSeqComboBox = new System.Windows.Forms.ComboBox();
            this.seqArcSeqBox = new System.Windows.Forms.NumericUpDown();
            this.seqArcPanel = new System.Windows.Forms.Panel();
            this.seqArcOpenFileButton = new System.Windows.Forms.Button();
            this.seqPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.seqPlayerComboBox = new System.Windows.Forms.ComboBox();
            this.seqPlayerBox = new System.Windows.Forms.NumericUpDown();
            this.label27 = new System.Windows.Forms.Label();
            this.seqPlayerPriorityBox = new System.Windows.Forms.NumericUpDown();
            this.label26 = new System.Windows.Forms.Label();
            this.seqChannelPriorityBox = new System.Windows.Forms.NumericUpDown();
            this.label25 = new System.Windows.Forms.Label();
            this.seqVolumeBox = new System.Windows.Forms.NumericUpDown();
            this.label24 = new System.Windows.Forms.Label();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.seqBankComboBox = new System.Windows.Forms.ComboBox();
            this.seqBankBox = new System.Windows.Forms.NumericUpDown();
            this.label23 = new System.Windows.Forms.Label();
            this.playerPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.playerFlag15Box = new System.Windows.Forms.CheckBox();
            this.playerFlag14Box = new System.Windows.Forms.CheckBox();
            this.playerFlag13Box = new System.Windows.Forms.CheckBox();
            this.playerFlag12Box = new System.Windows.Forms.CheckBox();
            this.playerFlag11Box = new System.Windows.Forms.CheckBox();
            this.playerFlag10Box = new System.Windows.Forms.CheckBox();
            this.playerFlag9Box = new System.Windows.Forms.CheckBox();
            this.playerFlag8Box = new System.Windows.Forms.CheckBox();
            this.playerFlag7Box = new System.Windows.Forms.CheckBox();
            this.playerFlag6Box = new System.Windows.Forms.CheckBox();
            this.playerFlag5Box = new System.Windows.Forms.CheckBox();
            this.playerFlag4Box = new System.Windows.Forms.CheckBox();
            this.playerFlag3Box = new System.Windows.Forms.CheckBox();
            this.playerFlag2Box = new System.Windows.Forms.CheckBox();
            this.playerFlag1Box = new System.Windows.Forms.CheckBox();
            this.playerFlag0Box = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.playerHeapSizeBox = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.playerMaxSequencesBox = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.stmPanel = new System.Windows.Forms.Panel();
            this.stmMonoToStereoBox = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.stmPlayerComboBox = new System.Windows.Forms.ComboBox();
            this.stmPlayerBox = new System.Windows.Forms.NumericUpDown();
            this.stmPriorityBox = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.stmVolumeBox = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.streamPlayerPanel = new System.Windows.Forms.Panel();
            this.stmPlayerChannelType = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.stmPlayerLeftChannelBox = new System.Windows.Forms.NumericUpDown();
            this.stmPlayerRightChannelBox = new System.Windows.Forms.NumericUpDown();
            this.rightChannelLabel = new System.Windows.Forms.Label();
            this.leftChannelLabel = new System.Windows.Forms.Label();
            this.grpPanel = new System.Windows.Forms.Panel();
            this.grpEntries = new System.Windows.Forms.DataGridView();
            this.item = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.loadFlags = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.bankPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.bnkWar3ComboBox = new System.Windows.Forms.ComboBox();
            this.bnkWar3Box = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.bnkWar2ComboBox = new System.Windows.Forms.ComboBox();
            this.bnkWar2Box = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.bnkWar1ComboBox = new System.Windows.Forms.ComboBox();
            this.bnkWar1Box = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.bnkWar0ComboBox = new System.Windows.Forms.ComboBox();
            this.bnkWar0Box = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.blankPanel = new System.Windows.Forms.Panel();
            this.warPanel = new System.Windows.Forms.Panel();
            this.loadIndividuallyBox = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.forceUniqueFilePanel = new System.Windows.Forms.Panel();
            this.forceUniqueFileBox = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.indexPanel = new System.Windows.Forms.Panel();
            this.swapAtIndexButton = new System.Windows.Forms.Button();
            this.itemIndexBox = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.seqExportModeBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.seqImportModeBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.writeNamesBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.noInfoPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.kermalisSoundPlayerPanel = new System.Windows.Forms.Panel();
            this.kermalisPosition = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.kermalisStopButton = new System.Windows.Forms.Button();
            this.kermalisPauseButton = new System.Windows.Forms.Button();
            this.kermalisVolumeSlider = new System.Windows.Forms.TrackBar();
            this.kermalisLoopBox = new System.Windows.Forms.CheckBox();
            this.kermalisPlayButton = new System.Windows.Forms.Button();
            this.soundPlayerLabel = new System.Windows.Forms.Label();
            this.pnlPianoKeys = new System.Windows.Forms.Panel();
            this.pkeyC7 = new Multimedia.UI.PianoKey();
            this.pkeyE7 = new Multimedia.UI.PianoKey();
            this.pkeyCSharp7 = new Multimedia.UI.PianoKey();
            this.pkeyD7 = new Multimedia.UI.PianoKey();
            this.pkeyDSharp7 = new Multimedia.UI.PianoKey();
            this.pkeyF7 = new Multimedia.UI.PianoKey();
            this.pkeyFSharp7 = new Multimedia.UI.PianoKey();
            this.pkeyG7 = new Multimedia.UI.PianoKey();
            this.pkeyGSharp7 = new Multimedia.UI.PianoKey();
            this.pkeyA7 = new Multimedia.UI.PianoKey();
            this.pkeyASharp7 = new Multimedia.UI.PianoKey();
            this.pkeyB7 = new Multimedia.UI.PianoKey();
            this.pkeyC6 = new Multimedia.UI.PianoKey();
            this.pkeyE6 = new Multimedia.UI.PianoKey();
            this.pkeyCSharp6 = new Multimedia.UI.PianoKey();
            this.pkeyD6 = new Multimedia.UI.PianoKey();
            this.pkeyDSharp6 = new Multimedia.UI.PianoKey();
            this.pkeyF6 = new Multimedia.UI.PianoKey();
            this.pkeyFSharp6 = new Multimedia.UI.PianoKey();
            this.pkeyG6 = new Multimedia.UI.PianoKey();
            this.pkeyGSharp6 = new Multimedia.UI.PianoKey();
            this.pkeyA6 = new Multimedia.UI.PianoKey();
            this.pkeyASharp6 = new Multimedia.UI.PianoKey();
            this.pkeyB6 = new Multimedia.UI.PianoKey();
            this.pkeyC1 = new Multimedia.UI.PianoKey();
            this.pkeyCSharp1 = new Multimedia.UI.PianoKey();
            this.pkeyD1 = new Multimedia.UI.PianoKey();
            this.pkeyDSharp1 = new Multimedia.UI.PianoKey();
            this.pkeyE1 = new Multimedia.UI.PianoKey();
            this.pkeyF1 = new Multimedia.UI.PianoKey();
            this.pkeyFSharp1 = new Multimedia.UI.PianoKey();
            this.pkeyG1 = new Multimedia.UI.PianoKey();
            this.pkeyGSharp1 = new Multimedia.UI.PianoKey();
            this.pkeyA1 = new Multimedia.UI.PianoKey();
            this.pkeyASharp1 = new Multimedia.UI.PianoKey();
            this.pkeyB1 = new Multimedia.UI.PianoKey();
            this.pkeyC2 = new Multimedia.UI.PianoKey();
            this.pkeyCSharp2 = new Multimedia.UI.PianoKey();
            this.pkeyD2 = new Multimedia.UI.PianoKey();
            this.pkeyDSharp2 = new Multimedia.UI.PianoKey();
            this.pkeyE2 = new Multimedia.UI.PianoKey();
            this.pkeyF2 = new Multimedia.UI.PianoKey();
            this.pkeyFSharp2 = new Multimedia.UI.PianoKey();
            this.pkeyG2 = new Multimedia.UI.PianoKey();
            this.pkeyGSharp2 = new Multimedia.UI.PianoKey();
            this.pkeyA2 = new Multimedia.UI.PianoKey();
            this.pkeyASharp2 = new Multimedia.UI.PianoKey();
            this.pkeyB2 = new Multimedia.UI.PianoKey();
            this.pkeyC3 = new Multimedia.UI.PianoKey();
            this.pkeyCSharp3 = new Multimedia.UI.PianoKey();
            this.pkeyD3 = new Multimedia.UI.PianoKey();
            this.pkeyDSharp3 = new Multimedia.UI.PianoKey();
            this.pkeyE3 = new Multimedia.UI.PianoKey();
            this.pkeyF3 = new Multimedia.UI.PianoKey();
            this.pkeyFSharp3 = new Multimedia.UI.PianoKey();
            this.pkeyG3 = new Multimedia.UI.PianoKey();
            this.pkeyGSharp3 = new Multimedia.UI.PianoKey();
            this.pkeyA3 = new Multimedia.UI.PianoKey();
            this.pkeyASharp3 = new Multimedia.UI.PianoKey();
            this.pkeyB3 = new Multimedia.UI.PianoKey();
            this.pkeyC4 = new Multimedia.UI.PianoKey();
            this.pkeyCSharp4 = new Multimedia.UI.PianoKey();
            this.pkeyD4 = new Multimedia.UI.PianoKey();
            this.pkeyDSharp4 = new Multimedia.UI.PianoKey();
            this.pkeyE4 = new Multimedia.UI.PianoKey();
            this.pkeyF4 = new Multimedia.UI.PianoKey();
            this.pkeyFSharp4 = new Multimedia.UI.PianoKey();
            this.pkeyG4 = new Multimedia.UI.PianoKey();
            this.pkeyGSharp4 = new Multimedia.UI.PianoKey();
            this.pkeyA4 = new Multimedia.UI.PianoKey();
            this.pkeyASharp4 = new Multimedia.UI.PianoKey();
            this.pkeyB4 = new Multimedia.UI.PianoKey();
            this.pkeyC5 = new Multimedia.UI.PianoKey();
            this.pkeyCSharp5 = new Multimedia.UI.PianoKey();
            this.pkeyD5 = new Multimedia.UI.PianoKey();
            this.pkeyDSharp5 = new Multimedia.UI.PianoKey();
            this.pkeyE5 = new Multimedia.UI.PianoKey();
            this.pkeyF5 = new Multimedia.UI.PianoKey();
            this.pkeyFSharp5 = new Multimedia.UI.PianoKey();
            this.pkeyG5 = new Multimedia.UI.PianoKey();
            this.pkeyGSharp5 = new Multimedia.UI.PianoKey();
            this.pkeyA5 = new Multimedia.UI.PianoKey();
            this.pkeyASharp5 = new Multimedia.UI.PianoKey();
            this.pkeyB5 = new Multimedia.UI.PianoKey();
            this.pkeyC8 = new Multimedia.UI.PianoKey();
            this.bankEditorWars = new System.Windows.Forms.Panel();
            this.tableLayoutPanel16 = new System.Windows.Forms.TableLayoutPanel();
            this.war3ComboBox = new System.Windows.Forms.ComboBox();
            this.war3Box = new System.Windows.Forms.NumericUpDown();
            this.label31 = new System.Windows.Forms.Label();
            this.tableLayoutPanel17 = new System.Windows.Forms.TableLayoutPanel();
            this.war2ComboBox = new System.Windows.Forms.ComboBox();
            this.war2Box = new System.Windows.Forms.NumericUpDown();
            this.label33 = new System.Windows.Forms.Label();
            this.tableLayoutPanel18 = new System.Windows.Forms.TableLayoutPanel();
            this.war1ComboBox = new System.Windows.Forms.ComboBox();
            this.war1Box = new System.Windows.Forms.NumericUpDown();
            this.label34 = new System.Windows.Forms.Label();
            this.tableLayoutPanel19 = new System.Windows.Forms.TableLayoutPanel();
            this.war0ComboBox = new System.Windows.Forms.ComboBox();
            this.war0Box = new System.Windows.Forms.NumericUpDown();
            this.label35 = new System.Windows.Forms.Label();
            this.tree = new System.Windows.Forms.TreeView();
            this.treeIcons = new System.Windows.Forms.ImageList(this.components);
            this.sequenceEditorPanel = new System.Windows.Forms.Panel();
            this.sequenceEditor = new ScintillaNET.Scintilla();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.currentNote = new System.Windows.Forms.ToolStripStatusLabel();
            this.rootMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.nodeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addAboveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addBelowToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.moveUpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.sarEntryMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sarAddAbove = new System.Windows.Forms.ToolStripMenuItem();
            this.sarAddBelow = new System.Windows.Forms.ToolStripMenuItem();
            this.sarMoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.sarMoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.sarReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.sarExport = new System.Windows.Forms.ToolStripMenuItem();
            this.sarRename = new System.Windows.Forms.ToolStripMenuItem();
            this.sarDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.seqBankPanel.SuspendLayout();
            this.tableLayoutPanel36.SuspendLayout();
            this.tableLayoutPanel20.SuspendLayout();
            this.tableLayoutPanel35.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track15Picture)).BeginInit();
            this.tableLayoutPanel34.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track13Picture)).BeginInit();
            this.tableLayoutPanel33.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track11Picture)).BeginInit();
            this.tableLayoutPanel32.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track9Picture)).BeginInit();
            this.tableLayoutPanel31.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track7Picture)).BeginInit();
            this.tableLayoutPanel30.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track5Picture)).BeginInit();
            this.tableLayoutPanel29.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track3Picture)).BeginInit();
            this.tableLayoutPanel28.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track14Picture)).BeginInit();
            this.tableLayoutPanel27.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track12Picture)).BeginInit();
            this.tableLayoutPanel26.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track10Picture)).BeginInit();
            this.tableLayoutPanel25.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track8Picture)).BeginInit();
            this.tableLayoutPanel24.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track6Picture)).BeginInit();
            this.tableLayoutPanel23.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track4Picture)).BeginInit();
            this.tableLayoutPanel22.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track2Picture)).BeginInit();
            this.tableLayoutPanel21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track1Picture)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track0Picture)).BeginInit();
            this.tableLayoutPanel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seqEditorBankBox)).BeginInit();
            this.bankEditorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bankRegions)).BeginInit();
            this.tableLayoutPanel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drumSetStartRangeBox)).BeginInit();
            this.tableLayoutPanel14.SuspendLayout();
            this.seqArcSeqPanel.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seqArcSeqBox)).BeginInit();
            this.seqArcPanel.SuspendLayout();
            this.seqPanel.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seqPlayerBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seqPlayerPriorityBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seqChannelPriorityBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seqVolumeBox)).BeginInit();
            this.tableLayoutPanel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seqBankBox)).BeginInit();
            this.playerPanel.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playerHeapSizeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerMaxSequencesBox)).BeginInit();
            this.stmPanel.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stmPlayerBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stmPriorityBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stmVolumeBox)).BeginInit();
            this.streamPlayerPanel.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stmPlayerLeftChannelBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stmPlayerRightChannelBox)).BeginInit();
            this.grpPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpEntries)).BeginInit();
            this.bankPanel.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bnkWar3Box)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bnkWar2Box)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bnkWar1Box)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bnkWar0Box)).BeginInit();
            this.warPanel.SuspendLayout();
            this.forceUniqueFilePanel.SuspendLayout();
            this.indexPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemIndexBox)).BeginInit();
            this.settingsPanel.SuspendLayout();
            this.noInfoPanel.SuspendLayout();
            this.kermalisSoundPlayerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kermalisPosition)).BeginInit();
            this.tableLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kermalisVolumeSlider)).BeginInit();
            this.pnlPianoKeys.SuspendLayout();
            this.bankEditorWars.SuspendLayout();
            this.tableLayoutPanel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.war3Box)).BeginInit();
            this.tableLayoutPanel17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.war2Box)).BeginInit();
            this.tableLayoutPanel18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.war1Box)).BeginInit();
            this.tableLayoutPanel19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.war0Box)).BeginInit();
            this.sequenceEditorPanel.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.rootMenu.SuspendLayout();
            this.nodeMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.sarEntryMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(984, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = global::NitroStudio2.Properties.Resources.New;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::NitroStudio2.Properties.Resources.Open;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::NitroStudio2.Properties.Resources.Save;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Image = global::NitroStudio2.Properties.Resources.Save_As;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Image = global::NitroStudio2.Properties.Resources.Close;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Image = global::NitroStudio2.Properties.Resources.Quit;
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blankFileToolStripMenuItem,
            this.importFileToolStripMenuItem,
            this.exportFileToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // blankFileToolStripMenuItem
            // 
            this.blankFileToolStripMenuItem.Image = global::NitroStudio2.Properties.Resources.Rename;
            this.blankFileToolStripMenuItem.Name = "blankFileToolStripMenuItem";
            this.blankFileToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.blankFileToolStripMenuItem.Text = "Blank File";
            this.blankFileToolStripMenuItem.Click += new System.EventHandler(this.blankFileToolStripMenuItem_Click);
            // 
            // importFileToolStripMenuItem
            // 
            this.importFileToolStripMenuItem.Image = global::NitroStudio2.Properties.Resources.Import;
            this.importFileToolStripMenuItem.Name = "importFileToolStripMenuItem";
            this.importFileToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.importFileToolStripMenuItem.Text = "Import File";
            this.importFileToolStripMenuItem.Click += new System.EventHandler(this.importFileToolStripMenuItem_Click);
            // 
            // exportFileToolStripMenuItem
            // 
            this.exportFileToolStripMenuItem.Image = global::NitroStudio2.Properties.Resources.Export;
            this.exportFileToolStripMenuItem.Name = "exportFileToolStripMenuItem";
            this.exportFileToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.exportFileToolStripMenuItem.Text = "Export File";
            this.exportFileToolStripMenuItem.Click += new System.EventHandler(this.exportFileToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sequenceEditorToolStripMenuItem,
            this.sequenceArchiveEditorToolStripMenuItem,
            this.bankEditorToolStripMenuItem,
            this.waveArchiveEditorToolStripMenuItem,
            this.bankGeneratorToolStripMenuItem,
            this.creaveWaveToolStripMenuItem,
            this.createStreamToolStripMenuItem,
            this.exportSDKProjectToolStripMenuItem,
            this.sF2ToDLSToolStripMenuItem,
            this.dLSToSF2ToolStripMenuItem,
            this.batchExportMIDIDLSSF2ToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            this.toolsToolStripMenuItem.Visible = false;
            // 
            // sequenceEditorToolStripMenuItem
            // 
            this.sequenceEditorToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sequenceEditorToolStripMenuItem.Image")));
            this.sequenceEditorToolStripMenuItem.Name = "sequenceEditorToolStripMenuItem";
            this.sequenceEditorToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.sequenceEditorToolStripMenuItem.Text = "Sequence Editor";
            this.sequenceEditorToolStripMenuItem.Click += new System.EventHandler(this.SequenceEditorToolStripMenuItem_Click);
            // 
            // sequenceArchiveEditorToolStripMenuItem
            // 
            this.sequenceArchiveEditorToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sequenceArchiveEditorToolStripMenuItem.Image")));
            this.sequenceArchiveEditorToolStripMenuItem.Name = "sequenceArchiveEditorToolStripMenuItem";
            this.sequenceArchiveEditorToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.sequenceArchiveEditorToolStripMenuItem.Text = "Sequence Archive Editor";
            this.sequenceArchiveEditorToolStripMenuItem.Click += new System.EventHandler(this.SequenceArchiveEditorToolStripMenuItem_Click);
            // 
            // bankEditorToolStripMenuItem
            // 
            this.bankEditorToolStripMenuItem.Image = global::NitroStudio2.Properties.Resources.Bank;
            this.bankEditorToolStripMenuItem.Name = "bankEditorToolStripMenuItem";
            this.bankEditorToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.bankEditorToolStripMenuItem.Text = "Bank Editor";
            this.bankEditorToolStripMenuItem.Click += new System.EventHandler(this.BankEditorToolStripMenuItem_Click);
            // 
            // waveArchiveEditorToolStripMenuItem
            // 
            this.waveArchiveEditorToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("waveArchiveEditorToolStripMenuItem.Image")));
            this.waveArchiveEditorToolStripMenuItem.Name = "waveArchiveEditorToolStripMenuItem";
            this.waveArchiveEditorToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.waveArchiveEditorToolStripMenuItem.Text = "Wave Archive Editor";
            this.waveArchiveEditorToolStripMenuItem.Click += new System.EventHandler(this.WaveArchiveEditorToolStripMenuItem_Click);
            // 
            // bankGeneratorToolStripMenuItem
            // 
            this.bankGeneratorToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("bankGeneratorToolStripMenuItem.Image")));
            this.bankGeneratorToolStripMenuItem.Name = "bankGeneratorToolStripMenuItem";
            this.bankGeneratorToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.bankGeneratorToolStripMenuItem.Text = "Bank Generator";
            this.bankGeneratorToolStripMenuItem.Click += new System.EventHandler(this.BankGeneratorToolStripMenuItem_Click);
            // 
            // creaveWaveToolStripMenuItem
            // 
            this.creaveWaveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("creaveWaveToolStripMenuItem.Image")));
            this.creaveWaveToolStripMenuItem.Name = "creaveWaveToolStripMenuItem";
            this.creaveWaveToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.creaveWaveToolStripMenuItem.Text = "Creave Wave";
            this.creaveWaveToolStripMenuItem.Click += new System.EventHandler(this.CreaveWaveToolStripMenuItem_Click);
            // 
            // createStreamToolStripMenuItem
            // 
            this.createStreamToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("createStreamToolStripMenuItem.Image")));
            this.createStreamToolStripMenuItem.Name = "createStreamToolStripMenuItem";
            this.createStreamToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.createStreamToolStripMenuItem.Text = "Create Stream";
            this.createStreamToolStripMenuItem.Click += new System.EventHandler(this.CreateStreamToolStripMenuItem_Click);
            // 
            // exportSDKProjectToolStripMenuItem
            // 
            this.exportSDKProjectToolStripMenuItem.Image = global::NitroStudio2.Properties.Resources.NSM;
            this.exportSDKProjectToolStripMenuItem.Name = "exportSDKProjectToolStripMenuItem";
            this.exportSDKProjectToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.exportSDKProjectToolStripMenuItem.Text = "Export SDK Project";
            this.exportSDKProjectToolStripMenuItem.Click += new System.EventHandler(this.ExportSDKProjectToolStripMenuItem_Click);
            // 
            // sF2ToDLSToolStripMenuItem
            // 
            this.sF2ToDLSToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sF2ToDLSToolStripMenuItem.Image")));
            this.sF2ToDLSToolStripMenuItem.Name = "sF2ToDLSToolStripMenuItem";
            this.sF2ToDLSToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.sF2ToDLSToolStripMenuItem.Text = "SF2 To DLS";
            this.sF2ToDLSToolStripMenuItem.Click += new System.EventHandler(this.sF2ToDLSToolStripMenuItem_Click);
            // 
            // dLSToSF2ToolStripMenuItem
            // 
            this.dLSToSF2ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("dLSToSF2ToolStripMenuItem.Image")));
            this.dLSToSF2ToolStripMenuItem.Name = "dLSToSF2ToolStripMenuItem";
            this.dLSToSF2ToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.dLSToSF2ToolStripMenuItem.Text = "DLS To SF2";
            this.dLSToSF2ToolStripMenuItem.Click += new System.EventHandler(this.dLSToSF2ToolStripMenuItem_Click);
            // 
            // batchExportMIDIDLSSF2ToolStripMenuItem
            // 
            this.batchExportMIDIDLSSF2ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("batchExportMIDIDLSSF2ToolStripMenuItem.Image")));
            this.batchExportMIDIDLSSF2ToolStripMenuItem.Name = "batchExportMIDIDLSSF2ToolStripMenuItem";
            this.batchExportMIDIDLSSF2ToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.batchExportMIDIDLSSF2ToolStripMenuItem.Text = "Batch Export MIDI/DLS/SF2";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getHelpToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // getHelpToolStripMenuItem
            // 
            this.getHelpToolStripMenuItem.Image = global::NitroStudio2.Properties.Resources.Help;
            this.getHelpToolStripMenuItem.Name = "getHelpToolStripMenuItem";
            this.getHelpToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.getHelpToolStripMenuItem.Text = "Get Help";
            this.getHelpToolStripMenuItem.Click += new System.EventHandler(this.GetHelpToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutNitroStudio2ToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // aboutNitroStudio2ToolStripMenuItem
            // 
            this.aboutNitroStudio2ToolStripMenuItem.Image = global::NitroStudio2.Properties.Resources.Ico;
            this.aboutNitroStudio2ToolStripMenuItem.Name = "aboutNitroStudio2ToolStripMenuItem";
            this.aboutNitroStudio2ToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.aboutNitroStudio2ToolStripMenuItem.Text = "About Nitro Studio 2";
            this.aboutNitroStudio2ToolStripMenuItem.Click += new System.EventHandler(this.AboutNitroStudio2ToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.seqBankPanel);
            this.splitContainer1.Panel1.Controls.Add(this.bankEditorPanel);
            this.splitContainer1.Panel1.Controls.Add(this.seqArcSeqPanel);
            this.splitContainer1.Panel1.Controls.Add(this.seqArcPanel);
            this.splitContainer1.Panel1.Controls.Add(this.seqPanel);
            this.splitContainer1.Panel1.Controls.Add(this.playerPanel);
            this.splitContainer1.Panel1.Controls.Add(this.stmPanel);
            this.splitContainer1.Panel1.Controls.Add(this.streamPlayerPanel);
            this.splitContainer1.Panel1.Controls.Add(this.grpPanel);
            this.splitContainer1.Panel1.Controls.Add(this.bankPanel);
            this.splitContainer1.Panel1.Controls.Add(this.blankPanel);
            this.splitContainer1.Panel1.Controls.Add(this.warPanel);
            this.splitContainer1.Panel1.Controls.Add(this.forceUniqueFilePanel);
            this.splitContainer1.Panel1.Controls.Add(this.indexPanel);
            this.splitContainer1.Panel1.Controls.Add(this.settingsPanel);
            this.splitContainer1.Panel1.Controls.Add(this.noInfoPanel);
            this.splitContainer1.Panel1.Controls.Add(this.kermalisSoundPlayerPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlPianoKeys);
            this.splitContainer1.Panel2.Controls.Add(this.bankEditorWars);
            this.splitContainer1.Panel2.Controls.Add(this.tree);
            this.splitContainer1.Panel2.Controls.Add(this.sequenceEditorPanel);
            this.splitContainer1.Size = new System.Drawing.Size(984, 540);
            this.splitContainer1.SplitterDistance = 327;
            this.splitContainer1.TabIndex = 1;
            // 
            // seqBankPanel
            // 
            this.seqBankPanel.Controls.Add(this.tableLayoutPanel36);
            this.seqBankPanel.Controls.Add(this.tableLayoutPanel20);
            this.seqBankPanel.Controls.Add(this.label28);
            this.seqBankPanel.Controls.Add(this.tableLayoutPanel12);
            this.seqBankPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seqBankPanel.Location = new System.Drawing.Point(0, 334);
            this.seqBankPanel.Name = "seqBankPanel";
            this.seqBankPanel.Size = new System.Drawing.Size(325, 204);
            this.seqBankPanel.TabIndex = 18;
            this.seqBankPanel.Visible = false;
            // 
            // tableLayoutPanel36
            // 
            this.tableLayoutPanel36.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel36.ColumnCount = 2;
            this.tableLayoutPanel36.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel36.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel36.Controls.Add(this.exportWavButton, 0, 0);
            this.tableLayoutPanel36.Controls.Add(this.exportMidiButton, 0, 0);
            this.tableLayoutPanel36.Location = new System.Drawing.Point(14, 244);
            this.tableLayoutPanel36.Name = "tableLayoutPanel36";
            this.tableLayoutPanel36.RowCount = 1;
            this.tableLayoutPanel36.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel36.Size = new System.Drawing.Size(298, 25);
            this.tableLayoutPanel36.TabIndex = 30;
            // 
            // exportWavButton
            // 
            this.exportWavButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exportWavButton.Location = new System.Drawing.Point(149, 0);
            this.exportWavButton.Margin = new System.Windows.Forms.Padding(0);
            this.exportWavButton.Name = "exportWavButton";
            this.exportWavButton.Size = new System.Drawing.Size(149, 25);
            this.exportWavButton.TabIndex = 5;
            this.exportWavButton.Text = "Export WAV";
            this.exportWavButton.UseVisualStyleBackColor = true;
            // 
            // exportMidiButton
            // 
            this.exportMidiButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exportMidiButton.Location = new System.Drawing.Point(0, 0);
            this.exportMidiButton.Margin = new System.Windows.Forms.Padding(0);
            this.exportMidiButton.Name = "exportMidiButton";
            this.exportMidiButton.Size = new System.Drawing.Size(149, 25);
            this.exportMidiButton.TabIndex = 4;
            this.exportMidiButton.Text = "Export MIDI";
            this.exportMidiButton.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel20
            // 
            this.tableLayoutPanel20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel20.ColumnCount = 2;
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel20.Controls.Add(this.tableLayoutPanel35, 1, 7);
            this.tableLayoutPanel20.Controls.Add(this.tableLayoutPanel34, 1, 6);
            this.tableLayoutPanel20.Controls.Add(this.tableLayoutPanel33, 1, 5);
            this.tableLayoutPanel20.Controls.Add(this.tableLayoutPanel32, 1, 4);
            this.tableLayoutPanel20.Controls.Add(this.tableLayoutPanel31, 1, 3);
            this.tableLayoutPanel20.Controls.Add(this.tableLayoutPanel30, 1, 2);
            this.tableLayoutPanel20.Controls.Add(this.tableLayoutPanel29, 1, 1);
            this.tableLayoutPanel20.Controls.Add(this.tableLayoutPanel28, 0, 7);
            this.tableLayoutPanel20.Controls.Add(this.tableLayoutPanel27, 0, 6);
            this.tableLayoutPanel20.Controls.Add(this.tableLayoutPanel26, 0, 5);
            this.tableLayoutPanel20.Controls.Add(this.tableLayoutPanel25, 0, 4);
            this.tableLayoutPanel20.Controls.Add(this.tableLayoutPanel24, 0, 3);
            this.tableLayoutPanel20.Controls.Add(this.tableLayoutPanel23, 0, 2);
            this.tableLayoutPanel20.Controls.Add(this.tableLayoutPanel22, 0, 1);
            this.tableLayoutPanel20.Controls.Add(this.tableLayoutPanel21, 1, 0);
            this.tableLayoutPanel20.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel20.Location = new System.Drawing.Point(14, 62);
            this.tableLayoutPanel20.Name = "tableLayoutPanel20";
            this.tableLayoutPanel20.RowCount = 8;
            this.tableLayoutPanel20.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel20.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel20.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel20.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel20.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel20.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel20.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel20.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel20.Size = new System.Drawing.Size(298, 176);
            this.tableLayoutPanel20.TabIndex = 28;
            // 
            // tableLayoutPanel35
            // 
            this.tableLayoutPanel35.ColumnCount = 3;
            this.tableLayoutPanel35.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel35.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel35.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel35.Controls.Add(this.track15Box, 0, 0);
            this.tableLayoutPanel35.Controls.Add(this.track15Picture, 2, 0);
            this.tableLayoutPanel35.Controls.Add(this.track15Solo, 1, 0);
            this.tableLayoutPanel35.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel35.Location = new System.Drawing.Point(149, 154);
            this.tableLayoutPanel35.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel35.Name = "tableLayoutPanel35";
            this.tableLayoutPanel35.RowCount = 1;
            this.tableLayoutPanel35.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel35.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel35.Size = new System.Drawing.Size(149, 22);
            this.tableLayoutPanel35.TabIndex = 41;
            // 
            // track15Box
            // 
            this.track15Box.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.track15Box.Checked = true;
            this.track15Box.CheckState = System.Windows.Forms.CheckState.Checked;
            this.track15Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track15Box.Location = new System.Drawing.Point(3, 3);
            this.track15Box.Name = "track15Box";
            this.track15Box.Size = new System.Drawing.Size(68, 16);
            this.track15Box.TabIndex = 2;
            this.track15Box.Text = "Track 15:";
            this.track15Box.UseVisualStyleBackColor = true;
            // 
            // track15Picture
            // 
            this.track15Picture.BackgroundImage = global::NitroStudio2.Properties.Resources.Idle;
            this.track15Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.track15Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track15Picture.Location = new System.Drawing.Point(111, 0);
            this.track15Picture.Margin = new System.Windows.Forms.Padding(0);
            this.track15Picture.Name = "track15Picture";
            this.track15Picture.Size = new System.Drawing.Size(38, 22);
            this.track15Picture.TabIndex = 0;
            this.track15Picture.TabStop = false;
            // 
            // track15Solo
            // 
            this.track15Solo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track15Solo.Location = new System.Drawing.Point(74, 0);
            this.track15Solo.Margin = new System.Windows.Forms.Padding(0);
            this.track15Solo.Name = "track15Solo";
            this.track15Solo.Size = new System.Drawing.Size(37, 22);
            this.track15Solo.TabIndex = 3;
            this.track15Solo.Text = "Solo";
            this.track15Solo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel34
            // 
            this.tableLayoutPanel34.ColumnCount = 3;
            this.tableLayoutPanel34.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel34.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel34.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel34.Controls.Add(this.track13Box, 0, 0);
            this.tableLayoutPanel34.Controls.Add(this.track13Picture, 2, 0);
            this.tableLayoutPanel34.Controls.Add(this.track13Solo, 1, 0);
            this.tableLayoutPanel34.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel34.Location = new System.Drawing.Point(149, 132);
            this.tableLayoutPanel34.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel34.Name = "tableLayoutPanel34";
            this.tableLayoutPanel34.RowCount = 1;
            this.tableLayoutPanel34.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel34.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel34.Size = new System.Drawing.Size(149, 22);
            this.tableLayoutPanel34.TabIndex = 40;
            // 
            // track13Box
            // 
            this.track13Box.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.track13Box.Checked = true;
            this.track13Box.CheckState = System.Windows.Forms.CheckState.Checked;
            this.track13Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track13Box.Location = new System.Drawing.Point(3, 3);
            this.track13Box.Name = "track13Box";
            this.track13Box.Size = new System.Drawing.Size(68, 16);
            this.track13Box.TabIndex = 2;
            this.track13Box.Text = "Track 13:";
            this.track13Box.UseVisualStyleBackColor = true;
            // 
            // track13Picture
            // 
            this.track13Picture.BackgroundImage = global::NitroStudio2.Properties.Resources.Idle;
            this.track13Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.track13Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track13Picture.Location = new System.Drawing.Point(111, 0);
            this.track13Picture.Margin = new System.Windows.Forms.Padding(0);
            this.track13Picture.Name = "track13Picture";
            this.track13Picture.Size = new System.Drawing.Size(38, 22);
            this.track13Picture.TabIndex = 0;
            this.track13Picture.TabStop = false;
            // 
            // track13Solo
            // 
            this.track13Solo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track13Solo.Location = new System.Drawing.Point(74, 0);
            this.track13Solo.Margin = new System.Windows.Forms.Padding(0);
            this.track13Solo.Name = "track13Solo";
            this.track13Solo.Size = new System.Drawing.Size(37, 22);
            this.track13Solo.TabIndex = 3;
            this.track13Solo.Text = "Solo";
            this.track13Solo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel33
            // 
            this.tableLayoutPanel33.ColumnCount = 3;
            this.tableLayoutPanel33.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel33.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel33.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel33.Controls.Add(this.track11Box, 0, 0);
            this.tableLayoutPanel33.Controls.Add(this.track11Picture, 2, 0);
            this.tableLayoutPanel33.Controls.Add(this.track11Solo, 1, 0);
            this.tableLayoutPanel33.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel33.Location = new System.Drawing.Point(149, 110);
            this.tableLayoutPanel33.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel33.Name = "tableLayoutPanel33";
            this.tableLayoutPanel33.RowCount = 1;
            this.tableLayoutPanel33.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel33.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel33.Size = new System.Drawing.Size(149, 22);
            this.tableLayoutPanel33.TabIndex = 39;
            // 
            // track11Box
            // 
            this.track11Box.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.track11Box.Checked = true;
            this.track11Box.CheckState = System.Windows.Forms.CheckState.Checked;
            this.track11Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track11Box.Location = new System.Drawing.Point(3, 3);
            this.track11Box.Name = "track11Box";
            this.track11Box.Size = new System.Drawing.Size(68, 16);
            this.track11Box.TabIndex = 2;
            this.track11Box.Text = "Track 11:";
            this.track11Box.UseVisualStyleBackColor = true;
            // 
            // track11Picture
            // 
            this.track11Picture.BackgroundImage = global::NitroStudio2.Properties.Resources.Idle;
            this.track11Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.track11Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track11Picture.Location = new System.Drawing.Point(111, 0);
            this.track11Picture.Margin = new System.Windows.Forms.Padding(0);
            this.track11Picture.Name = "track11Picture";
            this.track11Picture.Size = new System.Drawing.Size(38, 22);
            this.track11Picture.TabIndex = 0;
            this.track11Picture.TabStop = false;
            // 
            // track11Solo
            // 
            this.track11Solo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track11Solo.Location = new System.Drawing.Point(74, 0);
            this.track11Solo.Margin = new System.Windows.Forms.Padding(0);
            this.track11Solo.Name = "track11Solo";
            this.track11Solo.Size = new System.Drawing.Size(37, 22);
            this.track11Solo.TabIndex = 3;
            this.track11Solo.Text = "Solo";
            this.track11Solo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel32
            // 
            this.tableLayoutPanel32.ColumnCount = 3;
            this.tableLayoutPanel32.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel32.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel32.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel32.Controls.Add(this.track9Box, 0, 0);
            this.tableLayoutPanel32.Controls.Add(this.track9Picture, 2, 0);
            this.tableLayoutPanel32.Controls.Add(this.track9Solo, 1, 0);
            this.tableLayoutPanel32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel32.Location = new System.Drawing.Point(149, 88);
            this.tableLayoutPanel32.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel32.Name = "tableLayoutPanel32";
            this.tableLayoutPanel32.RowCount = 1;
            this.tableLayoutPanel32.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel32.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel32.Size = new System.Drawing.Size(149, 22);
            this.tableLayoutPanel32.TabIndex = 38;
            // 
            // track9Box
            // 
            this.track9Box.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.track9Box.Checked = true;
            this.track9Box.CheckState = System.Windows.Forms.CheckState.Checked;
            this.track9Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track9Box.Location = new System.Drawing.Point(3, 3);
            this.track9Box.Name = "track9Box";
            this.track9Box.Size = new System.Drawing.Size(68, 16);
            this.track9Box.TabIndex = 2;
            this.track9Box.Text = "Track 9:";
            this.track9Box.UseVisualStyleBackColor = true;
            // 
            // track9Picture
            // 
            this.track9Picture.BackgroundImage = global::NitroStudio2.Properties.Resources.Idle;
            this.track9Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.track9Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track9Picture.Location = new System.Drawing.Point(111, 0);
            this.track9Picture.Margin = new System.Windows.Forms.Padding(0);
            this.track9Picture.Name = "track9Picture";
            this.track9Picture.Size = new System.Drawing.Size(38, 22);
            this.track9Picture.TabIndex = 0;
            this.track9Picture.TabStop = false;
            // 
            // track9Solo
            // 
            this.track9Solo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track9Solo.Location = new System.Drawing.Point(74, 0);
            this.track9Solo.Margin = new System.Windows.Forms.Padding(0);
            this.track9Solo.Name = "track9Solo";
            this.track9Solo.Size = new System.Drawing.Size(37, 22);
            this.track9Solo.TabIndex = 3;
            this.track9Solo.Text = "Solo";
            this.track9Solo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel31
            // 
            this.tableLayoutPanel31.ColumnCount = 3;
            this.tableLayoutPanel31.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel31.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel31.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel31.Controls.Add(this.track7Box, 0, 0);
            this.tableLayoutPanel31.Controls.Add(this.track7Picture, 2, 0);
            this.tableLayoutPanel31.Controls.Add(this.track7Solo, 1, 0);
            this.tableLayoutPanel31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel31.Location = new System.Drawing.Point(149, 66);
            this.tableLayoutPanel31.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel31.Name = "tableLayoutPanel31";
            this.tableLayoutPanel31.RowCount = 1;
            this.tableLayoutPanel31.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel31.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel31.Size = new System.Drawing.Size(149, 22);
            this.tableLayoutPanel31.TabIndex = 37;
            // 
            // track7Box
            // 
            this.track7Box.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.track7Box.Checked = true;
            this.track7Box.CheckState = System.Windows.Forms.CheckState.Checked;
            this.track7Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track7Box.Location = new System.Drawing.Point(3, 3);
            this.track7Box.Name = "track7Box";
            this.track7Box.Size = new System.Drawing.Size(68, 16);
            this.track7Box.TabIndex = 2;
            this.track7Box.Text = "Track 7:";
            this.track7Box.UseVisualStyleBackColor = true;
            // 
            // track7Picture
            // 
            this.track7Picture.BackgroundImage = global::NitroStudio2.Properties.Resources.Idle;
            this.track7Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.track7Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track7Picture.Location = new System.Drawing.Point(111, 0);
            this.track7Picture.Margin = new System.Windows.Forms.Padding(0);
            this.track7Picture.Name = "track7Picture";
            this.track7Picture.Size = new System.Drawing.Size(38, 22);
            this.track7Picture.TabIndex = 0;
            this.track7Picture.TabStop = false;
            // 
            // track7Solo
            // 
            this.track7Solo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track7Solo.Location = new System.Drawing.Point(74, 0);
            this.track7Solo.Margin = new System.Windows.Forms.Padding(0);
            this.track7Solo.Name = "track7Solo";
            this.track7Solo.Size = new System.Drawing.Size(37, 22);
            this.track7Solo.TabIndex = 3;
            this.track7Solo.Text = "Solo";
            this.track7Solo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel30
            // 
            this.tableLayoutPanel30.ColumnCount = 3;
            this.tableLayoutPanel30.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel30.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel30.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel30.Controls.Add(this.track5Box, 0, 0);
            this.tableLayoutPanel30.Controls.Add(this.track5Picture, 2, 0);
            this.tableLayoutPanel30.Controls.Add(this.track5Solo, 1, 0);
            this.tableLayoutPanel30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel30.Location = new System.Drawing.Point(149, 44);
            this.tableLayoutPanel30.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel30.Name = "tableLayoutPanel30";
            this.tableLayoutPanel30.RowCount = 1;
            this.tableLayoutPanel30.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel30.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel30.Size = new System.Drawing.Size(149, 22);
            this.tableLayoutPanel30.TabIndex = 36;
            // 
            // track5Box
            // 
            this.track5Box.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.track5Box.Checked = true;
            this.track5Box.CheckState = System.Windows.Forms.CheckState.Checked;
            this.track5Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track5Box.Location = new System.Drawing.Point(3, 3);
            this.track5Box.Name = "track5Box";
            this.track5Box.Size = new System.Drawing.Size(68, 16);
            this.track5Box.TabIndex = 2;
            this.track5Box.Text = "Track 5:";
            this.track5Box.UseVisualStyleBackColor = true;
            // 
            // track5Picture
            // 
            this.track5Picture.BackgroundImage = global::NitroStudio2.Properties.Resources.Idle;
            this.track5Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.track5Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track5Picture.Location = new System.Drawing.Point(111, 0);
            this.track5Picture.Margin = new System.Windows.Forms.Padding(0);
            this.track5Picture.Name = "track5Picture";
            this.track5Picture.Size = new System.Drawing.Size(38, 22);
            this.track5Picture.TabIndex = 0;
            this.track5Picture.TabStop = false;
            // 
            // track5Solo
            // 
            this.track5Solo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track5Solo.Location = new System.Drawing.Point(74, 0);
            this.track5Solo.Margin = new System.Windows.Forms.Padding(0);
            this.track5Solo.Name = "track5Solo";
            this.track5Solo.Size = new System.Drawing.Size(37, 22);
            this.track5Solo.TabIndex = 3;
            this.track5Solo.Text = "Solo";
            this.track5Solo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel29
            // 
            this.tableLayoutPanel29.ColumnCount = 3;
            this.tableLayoutPanel29.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel29.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel29.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel29.Controls.Add(this.track3Box, 0, 0);
            this.tableLayoutPanel29.Controls.Add(this.track3Picture, 2, 0);
            this.tableLayoutPanel29.Controls.Add(this.track3Solo, 1, 0);
            this.tableLayoutPanel29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel29.Location = new System.Drawing.Point(149, 22);
            this.tableLayoutPanel29.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel29.Name = "tableLayoutPanel29";
            this.tableLayoutPanel29.RowCount = 1;
            this.tableLayoutPanel29.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel29.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel29.Size = new System.Drawing.Size(149, 22);
            this.tableLayoutPanel29.TabIndex = 35;
            // 
            // track3Box
            // 
            this.track3Box.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.track3Box.Checked = true;
            this.track3Box.CheckState = System.Windows.Forms.CheckState.Checked;
            this.track3Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track3Box.Location = new System.Drawing.Point(3, 3);
            this.track3Box.Name = "track3Box";
            this.track3Box.Size = new System.Drawing.Size(68, 16);
            this.track3Box.TabIndex = 2;
            this.track3Box.Text = "Track 3:";
            this.track3Box.UseVisualStyleBackColor = true;
            // 
            // track3Picture
            // 
            this.track3Picture.BackgroundImage = global::NitroStudio2.Properties.Resources.Idle;
            this.track3Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.track3Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track3Picture.Location = new System.Drawing.Point(111, 0);
            this.track3Picture.Margin = new System.Windows.Forms.Padding(0);
            this.track3Picture.Name = "track3Picture";
            this.track3Picture.Size = new System.Drawing.Size(38, 22);
            this.track3Picture.TabIndex = 0;
            this.track3Picture.TabStop = false;
            // 
            // track3Solo
            // 
            this.track3Solo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track3Solo.Location = new System.Drawing.Point(74, 0);
            this.track3Solo.Margin = new System.Windows.Forms.Padding(0);
            this.track3Solo.Name = "track3Solo";
            this.track3Solo.Size = new System.Drawing.Size(37, 22);
            this.track3Solo.TabIndex = 3;
            this.track3Solo.Text = "Solo";
            this.track3Solo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel28
            // 
            this.tableLayoutPanel28.ColumnCount = 3;
            this.tableLayoutPanel28.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel28.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel28.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel28.Controls.Add(this.track14Box, 0, 0);
            this.tableLayoutPanel28.Controls.Add(this.track14Picture, 2, 0);
            this.tableLayoutPanel28.Controls.Add(this.track14Solo, 1, 0);
            this.tableLayoutPanel28.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel28.Location = new System.Drawing.Point(0, 154);
            this.tableLayoutPanel28.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel28.Name = "tableLayoutPanel28";
            this.tableLayoutPanel28.RowCount = 1;
            this.tableLayoutPanel28.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel28.Size = new System.Drawing.Size(149, 22);
            this.tableLayoutPanel28.TabIndex = 34;
            // 
            // track14Box
            // 
            this.track14Box.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.track14Box.Checked = true;
            this.track14Box.CheckState = System.Windows.Forms.CheckState.Checked;
            this.track14Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track14Box.Location = new System.Drawing.Point(3, 3);
            this.track14Box.Name = "track14Box";
            this.track14Box.Size = new System.Drawing.Size(68, 16);
            this.track14Box.TabIndex = 2;
            this.track14Box.Text = "Track 14:";
            this.track14Box.UseVisualStyleBackColor = true;
            // 
            // track14Picture
            // 
            this.track14Picture.BackgroundImage = global::NitroStudio2.Properties.Resources.Idle;
            this.track14Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.track14Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track14Picture.Location = new System.Drawing.Point(111, 0);
            this.track14Picture.Margin = new System.Windows.Forms.Padding(0);
            this.track14Picture.Name = "track14Picture";
            this.track14Picture.Size = new System.Drawing.Size(38, 22);
            this.track14Picture.TabIndex = 0;
            this.track14Picture.TabStop = false;
            // 
            // track14Solo
            // 
            this.track14Solo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track14Solo.Location = new System.Drawing.Point(74, 0);
            this.track14Solo.Margin = new System.Windows.Forms.Padding(0);
            this.track14Solo.Name = "track14Solo";
            this.track14Solo.Size = new System.Drawing.Size(37, 22);
            this.track14Solo.TabIndex = 3;
            this.track14Solo.Text = "Solo";
            this.track14Solo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel27
            // 
            this.tableLayoutPanel27.ColumnCount = 3;
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel27.Controls.Add(this.track12Box, 0, 0);
            this.tableLayoutPanel27.Controls.Add(this.track12Picture, 2, 0);
            this.tableLayoutPanel27.Controls.Add(this.track12Solo, 1, 0);
            this.tableLayoutPanel27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel27.Location = new System.Drawing.Point(0, 132);
            this.tableLayoutPanel27.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel27.Name = "tableLayoutPanel27";
            this.tableLayoutPanel27.RowCount = 1;
            this.tableLayoutPanel27.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel27.Size = new System.Drawing.Size(149, 22);
            this.tableLayoutPanel27.TabIndex = 33;
            // 
            // track12Box
            // 
            this.track12Box.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.track12Box.Checked = true;
            this.track12Box.CheckState = System.Windows.Forms.CheckState.Checked;
            this.track12Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track12Box.Location = new System.Drawing.Point(3, 3);
            this.track12Box.Name = "track12Box";
            this.track12Box.Size = new System.Drawing.Size(68, 16);
            this.track12Box.TabIndex = 2;
            this.track12Box.Text = "Track 12:";
            this.track12Box.UseVisualStyleBackColor = true;
            // 
            // track12Picture
            // 
            this.track12Picture.BackgroundImage = global::NitroStudio2.Properties.Resources.Idle;
            this.track12Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.track12Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track12Picture.Location = new System.Drawing.Point(111, 0);
            this.track12Picture.Margin = new System.Windows.Forms.Padding(0);
            this.track12Picture.Name = "track12Picture";
            this.track12Picture.Size = new System.Drawing.Size(38, 22);
            this.track12Picture.TabIndex = 0;
            this.track12Picture.TabStop = false;
            // 
            // track12Solo
            // 
            this.track12Solo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track12Solo.Location = new System.Drawing.Point(74, 0);
            this.track12Solo.Margin = new System.Windows.Forms.Padding(0);
            this.track12Solo.Name = "track12Solo";
            this.track12Solo.Size = new System.Drawing.Size(37, 22);
            this.track12Solo.TabIndex = 3;
            this.track12Solo.Text = "Solo";
            this.track12Solo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel26
            // 
            this.tableLayoutPanel26.ColumnCount = 3;
            this.tableLayoutPanel26.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel26.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel26.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel26.Controls.Add(this.track10Box, 0, 0);
            this.tableLayoutPanel26.Controls.Add(this.track10Picture, 2, 0);
            this.tableLayoutPanel26.Controls.Add(this.track10Solo, 1, 0);
            this.tableLayoutPanel26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel26.Location = new System.Drawing.Point(0, 110);
            this.tableLayoutPanel26.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel26.Name = "tableLayoutPanel26";
            this.tableLayoutPanel26.RowCount = 1;
            this.tableLayoutPanel26.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel26.Size = new System.Drawing.Size(149, 22);
            this.tableLayoutPanel26.TabIndex = 32;
            // 
            // track10Box
            // 
            this.track10Box.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.track10Box.Checked = true;
            this.track10Box.CheckState = System.Windows.Forms.CheckState.Checked;
            this.track10Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track10Box.Location = new System.Drawing.Point(3, 3);
            this.track10Box.Name = "track10Box";
            this.track10Box.Size = new System.Drawing.Size(68, 16);
            this.track10Box.TabIndex = 2;
            this.track10Box.Text = "Track 10:";
            this.track10Box.UseVisualStyleBackColor = true;
            // 
            // track10Picture
            // 
            this.track10Picture.BackgroundImage = global::NitroStudio2.Properties.Resources.Idle;
            this.track10Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.track10Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track10Picture.Location = new System.Drawing.Point(111, 0);
            this.track10Picture.Margin = new System.Windows.Forms.Padding(0);
            this.track10Picture.Name = "track10Picture";
            this.track10Picture.Size = new System.Drawing.Size(38, 22);
            this.track10Picture.TabIndex = 0;
            this.track10Picture.TabStop = false;
            // 
            // track10Solo
            // 
            this.track10Solo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track10Solo.Location = new System.Drawing.Point(74, 0);
            this.track10Solo.Margin = new System.Windows.Forms.Padding(0);
            this.track10Solo.Name = "track10Solo";
            this.track10Solo.Size = new System.Drawing.Size(37, 22);
            this.track10Solo.TabIndex = 3;
            this.track10Solo.Text = "Solo";
            this.track10Solo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel25
            // 
            this.tableLayoutPanel25.ColumnCount = 3;
            this.tableLayoutPanel25.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel25.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel25.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel25.Controls.Add(this.track8Box, 0, 0);
            this.tableLayoutPanel25.Controls.Add(this.track8Picture, 2, 0);
            this.tableLayoutPanel25.Controls.Add(this.track8Solo, 1, 0);
            this.tableLayoutPanel25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel25.Location = new System.Drawing.Point(0, 88);
            this.tableLayoutPanel25.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel25.Name = "tableLayoutPanel25";
            this.tableLayoutPanel25.RowCount = 1;
            this.tableLayoutPanel25.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel25.Size = new System.Drawing.Size(149, 22);
            this.tableLayoutPanel25.TabIndex = 31;
            // 
            // track8Box
            // 
            this.track8Box.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.track8Box.Checked = true;
            this.track8Box.CheckState = System.Windows.Forms.CheckState.Checked;
            this.track8Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track8Box.Location = new System.Drawing.Point(3, 3);
            this.track8Box.Name = "track8Box";
            this.track8Box.Size = new System.Drawing.Size(68, 16);
            this.track8Box.TabIndex = 2;
            this.track8Box.Text = "Track 8:";
            this.track8Box.UseVisualStyleBackColor = true;
            // 
            // track8Picture
            // 
            this.track8Picture.BackgroundImage = global::NitroStudio2.Properties.Resources.Idle;
            this.track8Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.track8Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track8Picture.Location = new System.Drawing.Point(111, 0);
            this.track8Picture.Margin = new System.Windows.Forms.Padding(0);
            this.track8Picture.Name = "track8Picture";
            this.track8Picture.Size = new System.Drawing.Size(38, 22);
            this.track8Picture.TabIndex = 0;
            this.track8Picture.TabStop = false;
            // 
            // track8Solo
            // 
            this.track8Solo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track8Solo.Location = new System.Drawing.Point(74, 0);
            this.track8Solo.Margin = new System.Windows.Forms.Padding(0);
            this.track8Solo.Name = "track8Solo";
            this.track8Solo.Size = new System.Drawing.Size(37, 22);
            this.track8Solo.TabIndex = 3;
            this.track8Solo.Text = "Solo";
            this.track8Solo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel24
            // 
            this.tableLayoutPanel24.ColumnCount = 3;
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel24.Controls.Add(this.track6Box, 0, 0);
            this.tableLayoutPanel24.Controls.Add(this.track6Picture, 2, 0);
            this.tableLayoutPanel24.Controls.Add(this.track6Solo, 1, 0);
            this.tableLayoutPanel24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel24.Location = new System.Drawing.Point(0, 66);
            this.tableLayoutPanel24.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel24.Name = "tableLayoutPanel24";
            this.tableLayoutPanel24.RowCount = 1;
            this.tableLayoutPanel24.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel24.Size = new System.Drawing.Size(149, 22);
            this.tableLayoutPanel24.TabIndex = 30;
            // 
            // track6Box
            // 
            this.track6Box.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.track6Box.Checked = true;
            this.track6Box.CheckState = System.Windows.Forms.CheckState.Checked;
            this.track6Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track6Box.Location = new System.Drawing.Point(3, 3);
            this.track6Box.Name = "track6Box";
            this.track6Box.Size = new System.Drawing.Size(68, 16);
            this.track6Box.TabIndex = 2;
            this.track6Box.Text = "Track 6:";
            this.track6Box.UseVisualStyleBackColor = true;
            // 
            // track6Picture
            // 
            this.track6Picture.BackgroundImage = global::NitroStudio2.Properties.Resources.Idle;
            this.track6Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.track6Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track6Picture.Location = new System.Drawing.Point(111, 0);
            this.track6Picture.Margin = new System.Windows.Forms.Padding(0);
            this.track6Picture.Name = "track6Picture";
            this.track6Picture.Size = new System.Drawing.Size(38, 22);
            this.track6Picture.TabIndex = 0;
            this.track6Picture.TabStop = false;
            // 
            // track6Solo
            // 
            this.track6Solo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track6Solo.Location = new System.Drawing.Point(74, 0);
            this.track6Solo.Margin = new System.Windows.Forms.Padding(0);
            this.track6Solo.Name = "track6Solo";
            this.track6Solo.Size = new System.Drawing.Size(37, 22);
            this.track6Solo.TabIndex = 3;
            this.track6Solo.Text = "Solo";
            this.track6Solo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel23
            // 
            this.tableLayoutPanel23.ColumnCount = 3;
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel23.Controls.Add(this.track4Box, 0, 0);
            this.tableLayoutPanel23.Controls.Add(this.track4Picture, 2, 0);
            this.tableLayoutPanel23.Controls.Add(this.track4Solo, 1, 0);
            this.tableLayoutPanel23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel23.Location = new System.Drawing.Point(0, 44);
            this.tableLayoutPanel23.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel23.Name = "tableLayoutPanel23";
            this.tableLayoutPanel23.RowCount = 1;
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel23.Size = new System.Drawing.Size(149, 22);
            this.tableLayoutPanel23.TabIndex = 29;
            // 
            // track4Box
            // 
            this.track4Box.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.track4Box.Checked = true;
            this.track4Box.CheckState = System.Windows.Forms.CheckState.Checked;
            this.track4Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track4Box.Location = new System.Drawing.Point(3, 3);
            this.track4Box.Name = "track4Box";
            this.track4Box.Size = new System.Drawing.Size(68, 16);
            this.track4Box.TabIndex = 2;
            this.track4Box.Text = "Track 4:";
            this.track4Box.UseVisualStyleBackColor = true;
            // 
            // track4Picture
            // 
            this.track4Picture.BackgroundImage = global::NitroStudio2.Properties.Resources.Idle;
            this.track4Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.track4Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track4Picture.Location = new System.Drawing.Point(111, 0);
            this.track4Picture.Margin = new System.Windows.Forms.Padding(0);
            this.track4Picture.Name = "track4Picture";
            this.track4Picture.Size = new System.Drawing.Size(38, 22);
            this.track4Picture.TabIndex = 0;
            this.track4Picture.TabStop = false;
            // 
            // track4Solo
            // 
            this.track4Solo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track4Solo.Location = new System.Drawing.Point(74, 0);
            this.track4Solo.Margin = new System.Windows.Forms.Padding(0);
            this.track4Solo.Name = "track4Solo";
            this.track4Solo.Size = new System.Drawing.Size(37, 22);
            this.track4Solo.TabIndex = 3;
            this.track4Solo.Text = "Solo";
            this.track4Solo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel22
            // 
            this.tableLayoutPanel22.ColumnCount = 3;
            this.tableLayoutPanel22.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel22.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel22.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel22.Controls.Add(this.track2Box, 0, 0);
            this.tableLayoutPanel22.Controls.Add(this.track2Picture, 2, 0);
            this.tableLayoutPanel22.Controls.Add(this.track2Solo, 1, 0);
            this.tableLayoutPanel22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel22.Location = new System.Drawing.Point(0, 22);
            this.tableLayoutPanel22.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel22.Name = "tableLayoutPanel22";
            this.tableLayoutPanel22.RowCount = 1;
            this.tableLayoutPanel22.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel22.Size = new System.Drawing.Size(149, 22);
            this.tableLayoutPanel22.TabIndex = 28;
            // 
            // track2Box
            // 
            this.track2Box.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.track2Box.Checked = true;
            this.track2Box.CheckState = System.Windows.Forms.CheckState.Checked;
            this.track2Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track2Box.Location = new System.Drawing.Point(3, 3);
            this.track2Box.Name = "track2Box";
            this.track2Box.Size = new System.Drawing.Size(68, 16);
            this.track2Box.TabIndex = 2;
            this.track2Box.Text = "Track 2:";
            this.track2Box.UseVisualStyleBackColor = true;
            // 
            // track2Picture
            // 
            this.track2Picture.BackgroundImage = global::NitroStudio2.Properties.Resources.Idle;
            this.track2Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.track2Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track2Picture.Location = new System.Drawing.Point(111, 0);
            this.track2Picture.Margin = new System.Windows.Forms.Padding(0);
            this.track2Picture.Name = "track2Picture";
            this.track2Picture.Size = new System.Drawing.Size(38, 22);
            this.track2Picture.TabIndex = 0;
            this.track2Picture.TabStop = false;
            // 
            // track2Solo
            // 
            this.track2Solo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track2Solo.Location = new System.Drawing.Point(74, 0);
            this.track2Solo.Margin = new System.Windows.Forms.Padding(0);
            this.track2Solo.Name = "track2Solo";
            this.track2Solo.Size = new System.Drawing.Size(37, 22);
            this.track2Solo.TabIndex = 3;
            this.track2Solo.Text = "Solo";
            this.track2Solo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel21
            // 
            this.tableLayoutPanel21.ColumnCount = 3;
            this.tableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel21.Controls.Add(this.track1Box, 0, 0);
            this.tableLayoutPanel21.Controls.Add(this.track1Picture, 2, 0);
            this.tableLayoutPanel21.Controls.Add(this.track1Solo, 1, 0);
            this.tableLayoutPanel21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel21.Location = new System.Drawing.Point(149, 0);
            this.tableLayoutPanel21.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel21.Name = "tableLayoutPanel21";
            this.tableLayoutPanel21.RowCount = 1;
            this.tableLayoutPanel21.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel21.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel21.Size = new System.Drawing.Size(149, 22);
            this.tableLayoutPanel21.TabIndex = 27;
            // 
            // track1Box
            // 
            this.track1Box.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.track1Box.Checked = true;
            this.track1Box.CheckState = System.Windows.Forms.CheckState.Checked;
            this.track1Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track1Box.Location = new System.Drawing.Point(3, 3);
            this.track1Box.Name = "track1Box";
            this.track1Box.Size = new System.Drawing.Size(68, 16);
            this.track1Box.TabIndex = 2;
            this.track1Box.Text = "Track 1:";
            this.track1Box.UseVisualStyleBackColor = true;
            // 
            // track1Picture
            // 
            this.track1Picture.BackgroundImage = global::NitroStudio2.Properties.Resources.Idle;
            this.track1Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.track1Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track1Picture.Location = new System.Drawing.Point(111, 0);
            this.track1Picture.Margin = new System.Windows.Forms.Padding(0);
            this.track1Picture.Name = "track1Picture";
            this.track1Picture.Size = new System.Drawing.Size(38, 22);
            this.track1Picture.TabIndex = 0;
            this.track1Picture.TabStop = false;
            // 
            // track1Solo
            // 
            this.track1Solo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track1Solo.Location = new System.Drawing.Point(74, 0);
            this.track1Solo.Margin = new System.Windows.Forms.Padding(0);
            this.track1Solo.Name = "track1Solo";
            this.track1Solo.Size = new System.Drawing.Size(37, 22);
            this.track1Solo.TabIndex = 3;
            this.track1Solo.Text = "Solo";
            this.track1Solo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.track0Box, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.track0Picture, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.track0Solo, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(149, 22);
            this.tableLayoutPanel1.TabIndex = 26;
            // 
            // track0Box
            // 
            this.track0Box.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.track0Box.Checked = true;
            this.track0Box.CheckState = System.Windows.Forms.CheckState.Checked;
            this.track0Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track0Box.Location = new System.Drawing.Point(3, 3);
            this.track0Box.Name = "track0Box";
            this.track0Box.Size = new System.Drawing.Size(68, 16);
            this.track0Box.TabIndex = 2;
            this.track0Box.Text = "Track 0:";
            this.track0Box.UseVisualStyleBackColor = true;
            // 
            // track0Picture
            // 
            this.track0Picture.BackgroundImage = global::NitroStudio2.Properties.Resources.Idle;
            this.track0Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.track0Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track0Picture.Location = new System.Drawing.Point(111, 0);
            this.track0Picture.Margin = new System.Windows.Forms.Padding(0);
            this.track0Picture.Name = "track0Picture";
            this.track0Picture.Size = new System.Drawing.Size(38, 22);
            this.track0Picture.TabIndex = 0;
            this.track0Picture.TabStop = false;
            // 
            // track0Solo
            // 
            this.track0Solo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track0Solo.Location = new System.Drawing.Point(74, 0);
            this.track0Solo.Margin = new System.Windows.Forms.Padding(0);
            this.track0Solo.Name = "track0Solo";
            this.track0Solo.Size = new System.Drawing.Size(37, 22);
            this.track0Solo.TabIndex = 3;
            this.track0Solo.Text = "Solo";
            this.track0Solo.UseVisualStyleBackColor = true;
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.Location = new System.Drawing.Point(11, 3);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(301, 20);
            this.label28.TabIndex = 25;
            this.label28.Text = "Preview Bank:";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel12.ColumnCount = 2;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel12.Controls.Add(this.seqEditorBankComboBox, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.seqEditorBankBox, 1, 0);
            this.tableLayoutPanel12.Location = new System.Drawing.Point(14, 25);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 1;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(298, 31);
            this.tableLayoutPanel12.TabIndex = 24;
            // 
            // seqEditorBankComboBox
            // 
            this.seqEditorBankComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seqEditorBankComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.seqEditorBankComboBox.FormattingEnabled = true;
            this.seqEditorBankComboBox.Location = new System.Drawing.Point(3, 3);
            this.seqEditorBankComboBox.Name = "seqEditorBankComboBox";
            this.seqEditorBankComboBox.Size = new System.Drawing.Size(247, 21);
            this.seqEditorBankComboBox.TabIndex = 6;
            this.toolTip.SetToolTip(this.seqEditorBankComboBox, "Bank to use with the sequence.");
            // 
            // seqEditorBankBox
            // 
            this.seqEditorBankBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seqEditorBankBox.Location = new System.Drawing.Point(256, 3);
            this.seqEditorBankBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.seqEditorBankBox.Name = "seqEditorBankBox";
            this.seqEditorBankBox.Size = new System.Drawing.Size(39, 20);
            this.seqEditorBankBox.TabIndex = 7;
            this.toolTip.SetToolTip(this.seqEditorBankBox, "Id of the bank to use with the sequence.");
            // 
            // bankEditorPanel
            // 
            this.bankEditorPanel.Controls.Add(this.bankRegions);
            this.bankEditorPanel.Controls.Add(this.label32);
            this.bankEditorPanel.Controls.Add(this.tableLayoutPanel15);
            this.bankEditorPanel.Controls.Add(this.drumSetRangeStartLabel);
            this.bankEditorPanel.Controls.Add(this.tableLayoutPanel14);
            this.bankEditorPanel.Controls.Add(this.label30);
            this.bankEditorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bankEditorPanel.Location = new System.Drawing.Point(0, 334);
            this.bankEditorPanel.Name = "bankEditorPanel";
            this.bankEditorPanel.Size = new System.Drawing.Size(325, 204);
            this.bankEditorPanel.TabIndex = 21;
            this.bankEditorPanel.Visible = false;
            // 
            // bankRegions
            // 
            this.bankRegions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bankRegions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.bankRegions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bankRegions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.playSampleButton,
            this.endNote,
            this.instrumentType,
            this.waveId,
            this.waveArchiveId,
            this.baseNote,
            this.attack,
            this.decay,
            this.sustain,
            this.release,
            this.pan});
            this.bankRegions.Location = new System.Drawing.Point(14, 141);
            this.bankRegions.Name = "bankRegions";
            this.bankRegions.Size = new System.Drawing.Size(298, 54);
            this.bankRegions.TabIndex = 26;
            // 
            // playSampleButton
            // 
            this.playSampleButton.HeaderText = "Play";
            this.playSampleButton.Name = "playSampleButton";
            this.playSampleButton.Text = "Play";
            this.playSampleButton.UseColumnTextForButtonValue = true;
            this.playSampleButton.Width = 33;
            // 
            // endNote
            // 
            this.endNote.FillWeight = 50F;
            this.endNote.HeaderText = "End Note";
            this.endNote.Items.AddRange(new object[] {
            "cnm1 (0)",
            "csm1 (1)",
            "dnm1 (2)",
            "dsm1 (3)",
            "enm1 (4)",
            "fnm1 (5)",
            "fsm1 (6)",
            "gnm1 (7)",
            "gsm1 (8)",
            "anm1 (9)",
            "asm1 (10)",
            "bnm1 (11)",
            "cn0 (12)",
            "cs0 (13)",
            "dn0 (14)",
            "ds0 (15)",
            "en0 (16)",
            "fn0 (17)",
            "fs0 (18)",
            "gn0 (19)",
            "gs0 (20)",
            "an0 (21)",
            "as0 (22)",
            "bn0 (23)",
            "cn1 (24)",
            "cs1 (25)",
            "dn1 (26)",
            "ds1 (27)",
            "en1 (28)",
            "fn1 (29)",
            "fs1 (30)",
            "gn1 (31)",
            "gs1 (32)",
            "an1 (33)",
            "as1 (34)",
            "bn1 (35)",
            "cn2 (36)",
            "cs2 (37)",
            "dn2 (38)",
            "ds2 (39)",
            "en2 (40)",
            "fn2 (41)",
            "fs2 (42)",
            "gn2 (43)",
            "gs2 (44)",
            "an2 (45)",
            "as2 (46)",
            "bn2 (47)",
            "cn3 (48)",
            "cs3 (49)",
            "dn3 (50)",
            "ds3 (51)",
            "en3 (52)",
            "fn3 (53)",
            "fs3 (54)",
            "gn3 (55)",
            "gs3 (56)",
            "an3 (57)",
            "as3 (58)",
            "bn3 (59)",
            "cn4 (60)",
            "cs4 (61)",
            "dn4 (62)",
            "ds4 (63)",
            "en4 (64)",
            "fn4 (65)",
            "fs4 (66)",
            "gn4 (67)",
            "gs4 (68)",
            "an4 (69)",
            "as4 (70)",
            "bn4 (71)",
            "cn5 (72)",
            "cs5 (73)",
            "dn5 (74)",
            "ds5 (75)",
            "en5 (76)",
            "fn5 (77)",
            "fs5 (78)",
            "gn5 (79)",
            "gs5 (80)",
            "an5 (81)",
            "as5 (82)",
            "bn5 (83)",
            "cn6 (84)",
            "cs6 (85)",
            "dn6 (86)",
            "ds6 (87)",
            "en6 (88)",
            "fn6 (89)",
            "fs6 (90)",
            "gn6 (91)",
            "gs6 (92)",
            "an6 (93)",
            "as6 (94)",
            "bn6 (95)",
            "cn7 (96)",
            "cs7 (97)",
            "dn7 (98)",
            "ds7 (99)",
            "en7 (100)",
            "fn7 (101)",
            "fs7 (102)",
            "gn7 (103)",
            "gs7 (104)",
            "an7 (105)",
            "as7 (106)",
            "bn7 (107)",
            "cn8 (108)",
            "cs8 (109)",
            "dn8 (110)",
            "ds8 (111)",
            "en8 (112)",
            "fn8 (113)",
            "fs8 (114)",
            "gn8 (115)",
            "gs8 (116)",
            "an8 (117)",
            "as8 (118)",
            "bn8 (119)",
            "cn9 (120)",
            "cs9 (121)",
            "dn9 (122)",
            "ds9 (123)",
            "en9 (124)",
            "fn9 (125)",
            "fs9 (126)",
            "gn9 (127)"});
            this.endNote.Name = "endNote";
            this.endNote.Width = 52;
            // 
            // instrumentType
            // 
            this.instrumentType.HeaderText = "Instrument Type";
            this.instrumentType.Items.AddRange(new object[] {
            "PCM",
            "PSG",
            "Noise",
            "Direct PCM",
            "Null"});
            this.instrumentType.Name = "instrumentType";
            this.instrumentType.Width = 80;
            // 
            // waveId
            // 
            this.waveId.HeaderText = "Wave Id/PSG Cycle";
            this.waveId.MaxInputLength = 5;
            this.waveId.Name = "waveId";
            this.waveId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.waveId.Width = 99;
            // 
            // waveArchiveId
            // 
            this.waveArchiveId.HeaderText = "Wave Archive Id";
            this.waveArchiveId.MaxInputLength = 5;
            this.waveArchiveId.Name = "waveArchiveId";
            this.waveArchiveId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.waveArchiveId.Width = 76;
            // 
            // baseNote
            // 
            this.baseNote.HeaderText = "Base Note";
            this.baseNote.Items.AddRange(new object[] {
            "cnm1 (0)",
            "csm1 (1)",
            "dnm1 (2)",
            "dsm1 (3)",
            "enm1 (4)",
            "fnm1 (5)",
            "fsm1 (6)",
            "gnm1 (7)",
            "gsm1 (8)",
            "anm1 (9)",
            "asm1 (10)",
            "bnm1 (11)",
            "cn0 (12)",
            "cs0 (13)",
            "dn0 (14)",
            "ds0 (15)",
            "en0 (16)",
            "fn0 (17)",
            "fs0 (18)",
            "gn0 (19)",
            "gs0 (20)",
            "an0 (21)",
            "as0 (22)",
            "bn0 (23)",
            "cn1 (24)",
            "cs1 (25)",
            "dn1 (26)",
            "ds1 (27)",
            "en1 (28)",
            "fn1 (29)",
            "fs1 (30)",
            "gn1 (31)",
            "gs1 (32)",
            "an1 (33)",
            "as1 (34)",
            "bn1 (35)",
            "cn2 (36)",
            "cs2 (37)",
            "dn2 (38)",
            "ds2 (39)",
            "en2 (40)",
            "fn2 (41)",
            "fs2 (42)",
            "gn2 (43)",
            "gs2 (44)",
            "an2 (45)",
            "as2 (46)",
            "bn2 (47)",
            "cn3 (48)",
            "cs3 (49)",
            "dn3 (50)",
            "ds3 (51)",
            "en3 (52)",
            "fn3 (53)",
            "fs3 (54)",
            "gn3 (55)",
            "gs3 (56)",
            "an3 (57)",
            "as3 (58)",
            "bn3 (59)",
            "cn4 (60)",
            "cs4 (61)",
            "dn4 (62)",
            "ds4 (63)",
            "en4 (64)",
            "fn4 (65)",
            "fs4 (66)",
            "gn4 (67)",
            "gs4 (68)",
            "an4 (69)",
            "as4 (70)",
            "bn4 (71)",
            "cn5 (72)",
            "cs5 (73)",
            "dn5 (74)",
            "ds5 (75)",
            "en5 (76)",
            "fn5 (77)",
            "fs5 (78)",
            "gn5 (79)",
            "gs5 (80)",
            "an5 (81)",
            "as5 (82)",
            "bn5 (83)",
            "cn6 (84)",
            "cs6 (85)",
            "dn6 (86)",
            "ds6 (87)",
            "en6 (88)",
            "fn6 (89)",
            "fs6 (90)",
            "gn6 (91)",
            "gs6 (92)",
            "an6 (93)",
            "as6 (94)",
            "bn6 (95)",
            "cn7 (96)",
            "cs7 (97)",
            "dn7 (98)",
            "ds7 (99)",
            "en7 (100)",
            "fn7 (101)",
            "fs7 (102)",
            "gn7 (103)",
            "gs7 (104)",
            "an7 (105)",
            "as7 (106)",
            "bn7 (107)",
            "cn8 (108)",
            "cs8 (109)",
            "dn8 (110)",
            "ds8 (111)",
            "en8 (112)",
            "fn8 (113)",
            "fs8 (114)",
            "gn8 (115)",
            "gs8 (116)",
            "an8 (117)",
            "as8 (118)",
            "bn8 (119)",
            "cn9 (120)",
            "cs9 (121)",
            "dn9 (122)",
            "ds9 (123)",
            "en9 (124)",
            "fn9 (125)",
            "fs9 (126)",
            "gn9 (127)"});
            this.baseNote.Name = "baseNote";
            this.baseNote.Width = 57;
            // 
            // attack
            // 
            this.attack.HeaderText = "Attack";
            this.attack.MaxInputLength = 3;
            this.attack.Name = "attack";
            this.attack.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.attack.Width = 44;
            // 
            // decay
            // 
            this.decay.HeaderText = "Decay";
            this.decay.MaxInputLength = 3;
            this.decay.Name = "decay";
            this.decay.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.decay.Width = 44;
            // 
            // sustain
            // 
            this.sustain.HeaderText = "Sustain";
            this.sustain.MaxInputLength = 3;
            this.sustain.Name = "sustain";
            this.sustain.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sustain.Width = 48;
            // 
            // release
            // 
            this.release.HeaderText = "Release";
            this.release.MaxInputLength = 3;
            this.release.Name = "release";
            this.release.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.release.Width = 52;
            // 
            // pan
            // 
            this.pan.HeaderText = "Pan";
            this.pan.MaxInputLength = 3;
            this.pan.Name = "pan";
            this.pan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.pan.Width = 32;
            // 
            // label32
            // 
            this.label32.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label32.Location = new System.Drawing.Point(11, 118);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(301, 20);
            this.label32.TabIndex = 25;
            this.label32.Text = "Regions:";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel15.ColumnCount = 2;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel15.Controls.Add(this.drumSetStartRangeComboBox, 0, 0);
            this.tableLayoutPanel15.Controls.Add(this.drumSetStartRangeBox, 1, 0);
            this.tableLayoutPanel15.Location = new System.Drawing.Point(14, 84);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 1;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(298, 31);
            this.tableLayoutPanel15.TabIndex = 24;
            // 
            // drumSetStartRangeComboBox
            // 
            this.drumSetStartRangeComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drumSetStartRangeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drumSetStartRangeComboBox.FormattingEnabled = true;
            this.drumSetStartRangeComboBox.Items.AddRange(new object[] {
            "cnm1",
            "csm1",
            "dnm1",
            "dsm1",
            "enm1",
            "fnm1",
            "fsm1",
            "gnm1",
            "gsm1",
            "anm1",
            "asm1",
            "bnm1",
            "cn0",
            "cs0",
            "dn0",
            "ds0",
            "en0",
            "fn0",
            "fs0",
            "gn0",
            "gs0",
            "an0",
            "as0",
            "bn0",
            "cn1",
            "cs1",
            "dn1",
            "ds1",
            "en1",
            "fn1",
            "fs1",
            "gn1",
            "gs1",
            "an1",
            "as1",
            "bn1",
            "cn2",
            "cs2",
            "dn2",
            "ds2",
            "en2",
            "fn2",
            "fs2",
            "gn2",
            "gs2",
            "an2",
            "as2",
            "bn2",
            "cn3",
            "cs3",
            "dn3",
            "ds3",
            "en3",
            "fn3",
            "fs3",
            "gn3",
            "gs3",
            "an3",
            "as3",
            "bn3",
            "cn4",
            "cs4",
            "dn4",
            "ds4",
            "en4",
            "fn4",
            "fs4",
            "gn4",
            "gs4",
            "an4",
            "as4",
            "bn4",
            "cn5",
            "cs5",
            "dn5",
            "ds5",
            "en5",
            "fn5",
            "fs5",
            "gn5",
            "gs5",
            "an5",
            "as5",
            "bn5",
            "cn6",
            "cs6",
            "dn6",
            "ds6",
            "en6",
            "fn6",
            "fs6",
            "gn6",
            "gs6",
            "an6",
            "as6",
            "bn6",
            "cn7",
            "cs7",
            "dn7",
            "ds7",
            "en7",
            "fn7",
            "fs7",
            "gn7",
            "gs7",
            "an7",
            "as7",
            "bn7",
            "cn8",
            "cs8",
            "dn8",
            "ds8",
            "en8",
            "fn8",
            "fs8",
            "gn8",
            "gs8",
            "an8",
            "as8",
            "bn8",
            "cn9",
            "cs9",
            "dn9",
            "ds9",
            "en9",
            "fn9",
            "fs9",
            "gn9"});
            this.drumSetStartRangeComboBox.Location = new System.Drawing.Point(3, 3);
            this.drumSetStartRangeComboBox.Name = "drumSetStartRangeComboBox";
            this.drumSetStartRangeComboBox.Size = new System.Drawing.Size(247, 21);
            this.drumSetStartRangeComboBox.TabIndex = 6;
            this.toolTip.SetToolTip(this.drumSetStartRangeComboBox, "What note to start the drum set range at.");
            // 
            // drumSetStartRangeBox
            // 
            this.drumSetStartRangeBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drumSetStartRangeBox.Location = new System.Drawing.Point(256, 3);
            this.drumSetStartRangeBox.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.drumSetStartRangeBox.Name = "drumSetStartRangeBox";
            this.drumSetStartRangeBox.Size = new System.Drawing.Size(39, 20);
            this.drumSetStartRangeBox.TabIndex = 7;
            this.toolTip.SetToolTip(this.drumSetStartRangeBox, "What note to start the drum set range at.");
            // 
            // drumSetRangeStartLabel
            // 
            this.drumSetRangeStartLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drumSetRangeStartLabel.Location = new System.Drawing.Point(8, 61);
            this.drumSetRangeStartLabel.Name = "drumSetRangeStartLabel";
            this.drumSetRangeStartLabel.Size = new System.Drawing.Size(301, 20);
            this.drumSetRangeStartLabel.TabIndex = 3;
            this.drumSetRangeStartLabel.Text = "Drum Set Range Start:";
            this.drumSetRangeStartLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel14.ColumnCount = 3;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel14.Controls.Add(this.keySplitBox, 2, 0);
            this.tableLayoutPanel14.Controls.Add(this.drumSetBox, 1, 0);
            this.tableLayoutPanel14.Controls.Add(this.directBox, 0, 0);
            this.tableLayoutPanel14.Location = new System.Drawing.Point(14, 28);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 1;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(298, 28);
            this.tableLayoutPanel14.TabIndex = 2;
            this.toolTip.SetToolTip(this.tableLayoutPanel14, "Type of instrument.");
            // 
            // keySplitBox
            // 
            this.keySplitBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keySplitBox.Location = new System.Drawing.Point(201, 3);
            this.keySplitBox.Name = "keySplitBox";
            this.keySplitBox.Size = new System.Drawing.Size(94, 22);
            this.keySplitBox.TabIndex = 2;
            this.keySplitBox.TabStop = true;
            this.keySplitBox.Text = "Key Split";
            this.keySplitBox.UseVisualStyleBackColor = true;
            // 
            // drumSetBox
            // 
            this.drumSetBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drumSetBox.Location = new System.Drawing.Point(102, 3);
            this.drumSetBox.Name = "drumSetBox";
            this.drumSetBox.Size = new System.Drawing.Size(93, 22);
            this.drumSetBox.TabIndex = 1;
            this.drumSetBox.TabStop = true;
            this.drumSetBox.Text = "Drum Set";
            this.drumSetBox.UseVisualStyleBackColor = true;
            // 
            // directBox
            // 
            this.directBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.directBox.Location = new System.Drawing.Point(3, 3);
            this.directBox.Name = "directBox";
            this.directBox.Size = new System.Drawing.Size(93, 22);
            this.directBox.TabIndex = 0;
            this.directBox.TabStop = true;
            this.directBox.Text = "Direct";
            this.directBox.UseVisualStyleBackColor = true;
            // 
            // label30
            // 
            this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label30.Location = new System.Drawing.Point(11, 3);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(301, 20);
            this.label30.TabIndex = 1;
            this.label30.Text = "Instrument Type:";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // seqArcSeqPanel
            // 
            this.seqArcSeqPanel.Controls.Add(this.label29);
            this.seqArcSeqPanel.Controls.Add(this.tableLayoutPanel13);
            this.seqArcSeqPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.seqArcSeqPanel.Location = new System.Drawing.Point(0, 270);
            this.seqArcSeqPanel.Name = "seqArcSeqPanel";
            this.seqArcSeqPanel.Size = new System.Drawing.Size(325, 64);
            this.seqArcSeqPanel.TabIndex = 20;
            this.seqArcSeqPanel.Visible = false;
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.Location = new System.Drawing.Point(11, 3);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(301, 20);
            this.label29.TabIndex = 25;
            this.label29.Text = "Preview Sequence:";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel13.ColumnCount = 2;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel13.Controls.Add(this.seqArcSeqComboBox, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.seqArcSeqBox, 1, 0);
            this.tableLayoutPanel13.Location = new System.Drawing.Point(14, 25);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 1;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(298, 31);
            this.tableLayoutPanel13.TabIndex = 24;
            // 
            // seqArcSeqComboBox
            // 
            this.seqArcSeqComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seqArcSeqComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.seqArcSeqComboBox.FormattingEnabled = true;
            this.seqArcSeqComboBox.Location = new System.Drawing.Point(3, 3);
            this.seqArcSeqComboBox.Name = "seqArcSeqComboBox";
            this.seqArcSeqComboBox.Size = new System.Drawing.Size(247, 21);
            this.seqArcSeqComboBox.TabIndex = 6;
            this.toolTip.SetToolTip(this.seqArcSeqComboBox, "Sequence to play.");
            // 
            // seqArcSeqBox
            // 
            this.seqArcSeqBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seqArcSeqBox.Location = new System.Drawing.Point(256, 3);
            this.seqArcSeqBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.seqArcSeqBox.Name = "seqArcSeqBox";
            this.seqArcSeqBox.Size = new System.Drawing.Size(39, 20);
            this.seqArcSeqBox.TabIndex = 7;
            this.toolTip.SetToolTip(this.seqArcSeqBox, "Id of the sequence to play.");
            // 
            // seqArcPanel
            // 
            this.seqArcPanel.Controls.Add(this.seqArcOpenFileButton);
            this.seqArcPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seqArcPanel.Location = new System.Drawing.Point(0, 270);
            this.seqArcPanel.Name = "seqArcPanel";
            this.seqArcPanel.Size = new System.Drawing.Size(325, 268);
            this.seqArcPanel.TabIndex = 19;
            this.seqArcPanel.Visible = false;
            // 
            // seqArcOpenFileButton
            // 
            this.seqArcOpenFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seqArcOpenFileButton.Location = new System.Drawing.Point(14, 6);
            this.seqArcOpenFileButton.Name = "seqArcOpenFileButton";
            this.seqArcOpenFileButton.Size = new System.Drawing.Size(298, 25);
            this.seqArcOpenFileButton.TabIndex = 1;
            this.seqArcOpenFileButton.Text = "Open File";
            this.seqArcOpenFileButton.UseVisualStyleBackColor = true;
            // 
            // seqPanel
            // 
            this.seqPanel.Controls.Add(this.tableLayoutPanel11);
            this.seqPanel.Controls.Add(this.label27);
            this.seqPanel.Controls.Add(this.seqPlayerPriorityBox);
            this.seqPanel.Controls.Add(this.label26);
            this.seqPanel.Controls.Add(this.seqChannelPriorityBox);
            this.seqPanel.Controls.Add(this.label25);
            this.seqPanel.Controls.Add(this.seqVolumeBox);
            this.seqPanel.Controls.Add(this.label24);
            this.seqPanel.Controls.Add(this.tableLayoutPanel10);
            this.seqPanel.Controls.Add(this.label23);
            this.seqPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seqPanel.Location = new System.Drawing.Point(0, 270);
            this.seqPanel.Name = "seqPanel";
            this.seqPanel.Size = new System.Drawing.Size(325, 268);
            this.seqPanel.TabIndex = 17;
            this.seqPanel.Visible = false;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel11.ColumnCount = 2;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel11.Controls.Add(this.seqPlayerComboBox, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.seqPlayerBox, 1, 0);
            this.tableLayoutPanel11.Location = new System.Drawing.Point(14, 220);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(298, 31);
            this.tableLayoutPanel11.TabIndex = 23;
            // 
            // seqPlayerComboBox
            // 
            this.seqPlayerComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seqPlayerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.seqPlayerComboBox.FormattingEnabled = true;
            this.seqPlayerComboBox.Location = new System.Drawing.Point(3, 3);
            this.seqPlayerComboBox.Name = "seqPlayerComboBox";
            this.seqPlayerComboBox.Size = new System.Drawing.Size(247, 21);
            this.seqPlayerComboBox.TabIndex = 6;
            this.toolTip.SetToolTip(this.seqPlayerComboBox, "Player to play the sequence.");
            // 
            // seqPlayerBox
            // 
            this.seqPlayerBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seqPlayerBox.Location = new System.Drawing.Point(256, 3);
            this.seqPlayerBox.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.seqPlayerBox.Name = "seqPlayerBox";
            this.seqPlayerBox.Size = new System.Drawing.Size(39, 20);
            this.seqPlayerBox.TabIndex = 7;
            this.toolTip.SetToolTip(this.seqPlayerBox, "Id of the player to play the sequence.");
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.Location = new System.Drawing.Point(11, 198);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(301, 22);
            this.label27.TabIndex = 22;
            this.label27.Text = "Player:";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // seqPlayerPriorityBox
            // 
            this.seqPlayerPriorityBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seqPlayerPriorityBox.Location = new System.Drawing.Point(14, 175);
            this.seqPlayerPriorityBox.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.seqPlayerPriorityBox.Name = "seqPlayerPriorityBox";
            this.seqPlayerPriorityBox.Size = new System.Drawing.Size(298, 20);
            this.seqPlayerPriorityBox.TabIndex = 21;
            this.toolTip.SetToolTip(this.seqPlayerPriorityBox, "If the sounds can not all be played at once, the one with the highest priority wi" +
        "ll play.");
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.Location = new System.Drawing.Point(11, 152);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(301, 22);
            this.label26.TabIndex = 20;
            this.label26.Text = "Player Priority:";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // seqChannelPriorityBox
            // 
            this.seqChannelPriorityBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seqChannelPriorityBox.Location = new System.Drawing.Point(14, 129);
            this.seqChannelPriorityBox.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.seqChannelPriorityBox.Name = "seqChannelPriorityBox";
            this.seqChannelPriorityBox.Size = new System.Drawing.Size(298, 20);
            this.seqChannelPriorityBox.TabIndex = 19;
            this.toolTip.SetToolTip(this.seqChannelPriorityBox, "If the sounds can not all be played at once, the one with the highest priority wi" +
        "ll play.");
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.Location = new System.Drawing.Point(11, 106);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(301, 22);
            this.label25.TabIndex = 18;
            this.label25.Text = "Channel Priority:";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // seqVolumeBox
            // 
            this.seqVolumeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seqVolumeBox.Location = new System.Drawing.Point(14, 82);
            this.seqVolumeBox.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.seqVolumeBox.Name = "seqVolumeBox";
            this.seqVolumeBox.Size = new System.Drawing.Size(298, 20);
            this.seqVolumeBox.TabIndex = 17;
            this.toolTip.SetToolTip(this.seqVolumeBox, "The volume of the sequence.");
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.Location = new System.Drawing.Point(11, 59);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(301, 22);
            this.label24.TabIndex = 16;
            this.label24.Text = "Volume:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel10.Controls.Add(this.seqBankComboBox, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.seqBankBox, 1, 0);
            this.tableLayoutPanel10.Location = new System.Drawing.Point(14, 25);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(298, 31);
            this.tableLayoutPanel10.TabIndex = 15;
            // 
            // seqBankComboBox
            // 
            this.seqBankComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seqBankComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.seqBankComboBox.FormattingEnabled = true;
            this.seqBankComboBox.Location = new System.Drawing.Point(3, 3);
            this.seqBankComboBox.Name = "seqBankComboBox";
            this.seqBankComboBox.Size = new System.Drawing.Size(247, 21);
            this.seqBankComboBox.TabIndex = 6;
            this.toolTip.SetToolTip(this.seqBankComboBox, "Bank to use with the sequence.");
            // 
            // seqBankBox
            // 
            this.seqBankBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seqBankBox.Location = new System.Drawing.Point(256, 3);
            this.seqBankBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.seqBankBox.Name = "seqBankBox";
            this.seqBankBox.Size = new System.Drawing.Size(39, 20);
            this.seqBankBox.TabIndex = 7;
            this.toolTip.SetToolTip(this.seqBankBox, "Id of the bank to use with the sequence.");
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.Location = new System.Drawing.Point(11, 3);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(301, 22);
            this.label23.TabIndex = 2;
            this.label23.Text = "Bank:";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playerPanel
            // 
            this.playerPanel.Controls.Add(this.tableLayoutPanel8);
            this.playerPanel.Controls.Add(this.label19);
            this.playerPanel.Controls.Add(this.playerHeapSizeBox);
            this.playerPanel.Controls.Add(this.label18);
            this.playerPanel.Controls.Add(this.playerMaxSequencesBox);
            this.playerPanel.Controls.Add(this.label17);
            this.playerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playerPanel.Location = new System.Drawing.Point(0, 270);
            this.playerPanel.Name = "playerPanel";
            this.playerPanel.Size = new System.Drawing.Size(325, 268);
            this.playerPanel.TabIndex = 15;
            this.playerPanel.Visible = false;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel8.ColumnCount = 4;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel8.Controls.Add(this.playerFlag15Box, 3, 3);
            this.tableLayoutPanel8.Controls.Add(this.playerFlag14Box, 2, 3);
            this.tableLayoutPanel8.Controls.Add(this.playerFlag13Box, 1, 3);
            this.tableLayoutPanel8.Controls.Add(this.playerFlag12Box, 0, 3);
            this.tableLayoutPanel8.Controls.Add(this.playerFlag11Box, 3, 2);
            this.tableLayoutPanel8.Controls.Add(this.playerFlag10Box, 2, 2);
            this.tableLayoutPanel8.Controls.Add(this.playerFlag9Box, 1, 2);
            this.tableLayoutPanel8.Controls.Add(this.playerFlag8Box, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.playerFlag7Box, 3, 1);
            this.tableLayoutPanel8.Controls.Add(this.playerFlag6Box, 2, 1);
            this.tableLayoutPanel8.Controls.Add(this.playerFlag5Box, 1, 1);
            this.tableLayoutPanel8.Controls.Add(this.playerFlag4Box, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.playerFlag3Box, 3, 0);
            this.tableLayoutPanel8.Controls.Add(this.playerFlag2Box, 2, 0);
            this.tableLayoutPanel8.Controls.Add(this.playerFlag1Box, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.playerFlag0Box, 0, 0);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(14, 118);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 4;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(298, 100);
            this.tableLayoutPanel8.TabIndex = 10;
            this.toolTip.SetToolTip(this.tableLayoutPanel8, "Which channels the player is allowed to use.");
            // 
            // playerFlag15Box
            // 
            this.playerFlag15Box.AutoSize = true;
            this.playerFlag15Box.Location = new System.Drawing.Point(225, 78);
            this.playerFlag15Box.Name = "playerFlag15Box";
            this.playerFlag15Box.Size = new System.Drawing.Size(38, 17);
            this.playerFlag15Box.TabIndex = 15;
            this.playerFlag15Box.Text = "15";
            this.playerFlag15Box.UseVisualStyleBackColor = true;
            // 
            // playerFlag14Box
            // 
            this.playerFlag14Box.AutoSize = true;
            this.playerFlag14Box.Location = new System.Drawing.Point(151, 78);
            this.playerFlag14Box.Name = "playerFlag14Box";
            this.playerFlag14Box.Size = new System.Drawing.Size(38, 17);
            this.playerFlag14Box.TabIndex = 14;
            this.playerFlag14Box.Text = "14";
            this.playerFlag14Box.UseVisualStyleBackColor = true;
            // 
            // playerFlag13Box
            // 
            this.playerFlag13Box.AutoSize = true;
            this.playerFlag13Box.Location = new System.Drawing.Point(77, 78);
            this.playerFlag13Box.Name = "playerFlag13Box";
            this.playerFlag13Box.Size = new System.Drawing.Size(38, 17);
            this.playerFlag13Box.TabIndex = 13;
            this.playerFlag13Box.Text = "13";
            this.playerFlag13Box.UseVisualStyleBackColor = true;
            // 
            // playerFlag12Box
            // 
            this.playerFlag12Box.AutoSize = true;
            this.playerFlag12Box.Location = new System.Drawing.Point(3, 78);
            this.playerFlag12Box.Name = "playerFlag12Box";
            this.playerFlag12Box.Size = new System.Drawing.Size(38, 17);
            this.playerFlag12Box.TabIndex = 12;
            this.playerFlag12Box.Text = "12";
            this.playerFlag12Box.UseVisualStyleBackColor = true;
            // 
            // playerFlag11Box
            // 
            this.playerFlag11Box.AutoSize = true;
            this.playerFlag11Box.Location = new System.Drawing.Point(225, 53);
            this.playerFlag11Box.Name = "playerFlag11Box";
            this.playerFlag11Box.Size = new System.Drawing.Size(38, 17);
            this.playerFlag11Box.TabIndex = 11;
            this.playerFlag11Box.Text = "11";
            this.playerFlag11Box.UseVisualStyleBackColor = true;
            // 
            // playerFlag10Box
            // 
            this.playerFlag10Box.AutoSize = true;
            this.playerFlag10Box.Location = new System.Drawing.Point(151, 53);
            this.playerFlag10Box.Name = "playerFlag10Box";
            this.playerFlag10Box.Size = new System.Drawing.Size(38, 17);
            this.playerFlag10Box.TabIndex = 10;
            this.playerFlag10Box.Text = "10";
            this.playerFlag10Box.UseVisualStyleBackColor = true;
            // 
            // playerFlag9Box
            // 
            this.playerFlag9Box.AutoSize = true;
            this.playerFlag9Box.Location = new System.Drawing.Point(77, 53);
            this.playerFlag9Box.Name = "playerFlag9Box";
            this.playerFlag9Box.Size = new System.Drawing.Size(32, 17);
            this.playerFlag9Box.TabIndex = 9;
            this.playerFlag9Box.Text = "9";
            this.playerFlag9Box.UseVisualStyleBackColor = true;
            // 
            // playerFlag8Box
            // 
            this.playerFlag8Box.AutoSize = true;
            this.playerFlag8Box.Location = new System.Drawing.Point(3, 53);
            this.playerFlag8Box.Name = "playerFlag8Box";
            this.playerFlag8Box.Size = new System.Drawing.Size(32, 17);
            this.playerFlag8Box.TabIndex = 8;
            this.playerFlag8Box.Text = "8";
            this.playerFlag8Box.UseVisualStyleBackColor = true;
            // 
            // playerFlag7Box
            // 
            this.playerFlag7Box.AutoSize = true;
            this.playerFlag7Box.Location = new System.Drawing.Point(225, 28);
            this.playerFlag7Box.Name = "playerFlag7Box";
            this.playerFlag7Box.Size = new System.Drawing.Size(32, 17);
            this.playerFlag7Box.TabIndex = 7;
            this.playerFlag7Box.Text = "7";
            this.playerFlag7Box.UseVisualStyleBackColor = true;
            // 
            // playerFlag6Box
            // 
            this.playerFlag6Box.AutoSize = true;
            this.playerFlag6Box.Location = new System.Drawing.Point(151, 28);
            this.playerFlag6Box.Name = "playerFlag6Box";
            this.playerFlag6Box.Size = new System.Drawing.Size(32, 17);
            this.playerFlag6Box.TabIndex = 6;
            this.playerFlag6Box.Text = "6";
            this.playerFlag6Box.UseVisualStyleBackColor = true;
            // 
            // playerFlag5Box
            // 
            this.playerFlag5Box.AutoSize = true;
            this.playerFlag5Box.Location = new System.Drawing.Point(77, 28);
            this.playerFlag5Box.Name = "playerFlag5Box";
            this.playerFlag5Box.Size = new System.Drawing.Size(32, 17);
            this.playerFlag5Box.TabIndex = 5;
            this.playerFlag5Box.Text = "5";
            this.playerFlag5Box.UseVisualStyleBackColor = true;
            // 
            // playerFlag4Box
            // 
            this.playerFlag4Box.AutoSize = true;
            this.playerFlag4Box.Location = new System.Drawing.Point(3, 28);
            this.playerFlag4Box.Name = "playerFlag4Box";
            this.playerFlag4Box.Size = new System.Drawing.Size(32, 17);
            this.playerFlag4Box.TabIndex = 4;
            this.playerFlag4Box.Text = "4";
            this.playerFlag4Box.UseVisualStyleBackColor = true;
            // 
            // playerFlag3Box
            // 
            this.playerFlag3Box.AutoSize = true;
            this.playerFlag3Box.Location = new System.Drawing.Point(225, 3);
            this.playerFlag3Box.Name = "playerFlag3Box";
            this.playerFlag3Box.Size = new System.Drawing.Size(32, 17);
            this.playerFlag3Box.TabIndex = 3;
            this.playerFlag3Box.Text = "3";
            this.playerFlag3Box.UseVisualStyleBackColor = true;
            // 
            // playerFlag2Box
            // 
            this.playerFlag2Box.AutoSize = true;
            this.playerFlag2Box.Location = new System.Drawing.Point(151, 3);
            this.playerFlag2Box.Name = "playerFlag2Box";
            this.playerFlag2Box.Size = new System.Drawing.Size(32, 17);
            this.playerFlag2Box.TabIndex = 2;
            this.playerFlag2Box.Text = "2";
            this.playerFlag2Box.UseVisualStyleBackColor = true;
            // 
            // playerFlag1Box
            // 
            this.playerFlag1Box.AutoSize = true;
            this.playerFlag1Box.Location = new System.Drawing.Point(77, 3);
            this.playerFlag1Box.Name = "playerFlag1Box";
            this.playerFlag1Box.Size = new System.Drawing.Size(32, 17);
            this.playerFlag1Box.TabIndex = 1;
            this.playerFlag1Box.Text = "1";
            this.playerFlag1Box.UseVisualStyleBackColor = true;
            // 
            // playerFlag0Box
            // 
            this.playerFlag0Box.AutoSize = true;
            this.playerFlag0Box.Location = new System.Drawing.Point(3, 3);
            this.playerFlag0Box.Name = "playerFlag0Box";
            this.playerFlag0Box.Size = new System.Drawing.Size(32, 17);
            this.playerFlag0Box.TabIndex = 0;
            this.playerFlag0Box.Text = "0";
            this.playerFlag0Box.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.Location = new System.Drawing.Point(11, 93);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(301, 22);
            this.label19.TabIndex = 9;
            this.label19.Text = "Channel Flags:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playerHeapSizeBox
            // 
            this.playerHeapSizeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playerHeapSizeBox.Hexadecimal = true;
            this.playerHeapSizeBox.Location = new System.Drawing.Point(14, 70);
            this.playerHeapSizeBox.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.playerHeapSizeBox.Name = "playerHeapSizeBox";
            this.playerHeapSizeBox.Size = new System.Drawing.Size(298, 20);
            this.playerHeapSizeBox.TabIndex = 8;
            this.toolTip.SetToolTip(this.playerHeapSizeBox, "How much memory to reserve in the sound heap for the player.");
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.Location = new System.Drawing.Point(11, 48);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(301, 22);
            this.label18.TabIndex = 7;
            this.label18.Text = "Heap Size:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playerMaxSequencesBox
            // 
            this.playerMaxSequencesBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playerMaxSequencesBox.Location = new System.Drawing.Point(14, 25);
            this.playerMaxSequencesBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.playerMaxSequencesBox.Name = "playerMaxSequencesBox";
            this.playerMaxSequencesBox.Size = new System.Drawing.Size(298, 20);
            this.playerMaxSequencesBox.TabIndex = 6;
            this.toolTip.SetToolTip(this.playerMaxSequencesBox, "Max number of sequences the player can play.");
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.Location = new System.Drawing.Point(11, 3);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(301, 22);
            this.label17.TabIndex = 5;
            this.label17.Text = "Max Sequences:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // stmPanel
            // 
            this.stmPanel.Controls.Add(this.stmMonoToStereoBox);
            this.stmPanel.Controls.Add(this.label16);
            this.stmPanel.Controls.Add(this.label15);
            this.stmPanel.Controls.Add(this.tableLayoutPanel7);
            this.stmPanel.Controls.Add(this.stmPriorityBox);
            this.stmPanel.Controls.Add(this.label14);
            this.stmPanel.Controls.Add(this.stmVolumeBox);
            this.stmPanel.Controls.Add(this.label13);
            this.stmPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stmPanel.Location = new System.Drawing.Point(0, 270);
            this.stmPanel.Name = "stmPanel";
            this.stmPanel.Size = new System.Drawing.Size(325, 268);
            this.stmPanel.TabIndex = 14;
            this.stmPanel.Visible = false;
            // 
            // stmMonoToStereoBox
            // 
            this.stmMonoToStereoBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stmMonoToStereoBox.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.stmMonoToStereoBox.Location = new System.Drawing.Point(11, 171);
            this.stmMonoToStereoBox.Name = "stmMonoToStereoBox";
            this.stmMonoToStereoBox.Size = new System.Drawing.Size(301, 24);
            this.stmMonoToStereoBox.TabIndex = 17;
            this.toolTip.SetToolTip(this.stmMonoToStereoBox, "If the stream is mono, play it through two channels.");
            this.stmMonoToStereoBox.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.Location = new System.Drawing.Point(11, 149);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(301, 22);
            this.label16.TabIndex = 16;
            this.label16.Text = "Mono To Stereo:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.Location = new System.Drawing.Point(11, 93);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(301, 22);
            this.label15.TabIndex = 15;
            this.label15.Text = "Player:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel7.Controls.Add(this.stmPlayerComboBox, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.stmPlayerBox, 1, 0);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(14, 115);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(298, 31);
            this.tableLayoutPanel7.TabIndex = 14;
            // 
            // stmPlayerComboBox
            // 
            this.stmPlayerComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stmPlayerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stmPlayerComboBox.FormattingEnabled = true;
            this.stmPlayerComboBox.Location = new System.Drawing.Point(3, 3);
            this.stmPlayerComboBox.Name = "stmPlayerComboBox";
            this.stmPlayerComboBox.Size = new System.Drawing.Size(247, 21);
            this.stmPlayerComboBox.TabIndex = 6;
            this.toolTip.SetToolTip(this.stmPlayerComboBox, "The player to play the stream.");
            // 
            // stmPlayerBox
            // 
            this.stmPlayerBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stmPlayerBox.Location = new System.Drawing.Point(256, 3);
            this.stmPlayerBox.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.stmPlayerBox.Name = "stmPlayerBox";
            this.stmPlayerBox.Size = new System.Drawing.Size(39, 20);
            this.stmPlayerBox.TabIndex = 7;
            this.toolTip.SetToolTip(this.stmPlayerBox, "Id of the player to play the stream.");
            // 
            // stmPriorityBox
            // 
            this.stmPriorityBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stmPriorityBox.Location = new System.Drawing.Point(14, 70);
            this.stmPriorityBox.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.stmPriorityBox.Name = "stmPriorityBox";
            this.stmPriorityBox.Size = new System.Drawing.Size(298, 20);
            this.stmPriorityBox.TabIndex = 7;
            this.toolTip.SetToolTip(this.stmPriorityBox, "If the sounds can not all be played at once, the one with the highest priority wi" +
        "ll play.");
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.Location = new System.Drawing.Point(11, 48);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(301, 22);
            this.label14.TabIndex = 6;
            this.label14.Text = "Priority:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // stmVolumeBox
            // 
            this.stmVolumeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stmVolumeBox.Location = new System.Drawing.Point(14, 25);
            this.stmVolumeBox.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.stmVolumeBox.Name = "stmVolumeBox";
            this.stmVolumeBox.Size = new System.Drawing.Size(298, 20);
            this.stmVolumeBox.TabIndex = 5;
            this.toolTip.SetToolTip(this.stmVolumeBox, "The volume of the stream.");
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.Location = new System.Drawing.Point(11, 3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(301, 22);
            this.label13.TabIndex = 4;
            this.label13.Text = "Volume:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // streamPlayerPanel
            // 
            this.streamPlayerPanel.Controls.Add(this.stmPlayerChannelType);
            this.streamPlayerPanel.Controls.Add(this.label12);
            this.streamPlayerPanel.Controls.Add(this.tableLayoutPanel6);
            this.streamPlayerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.streamPlayerPanel.Location = new System.Drawing.Point(0, 270);
            this.streamPlayerPanel.Name = "streamPlayerPanel";
            this.streamPlayerPanel.Size = new System.Drawing.Size(325, 268);
            this.streamPlayerPanel.TabIndex = 13;
            this.streamPlayerPanel.Visible = false;
            // 
            // stmPlayerChannelType
            // 
            this.stmPlayerChannelType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stmPlayerChannelType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stmPlayerChannelType.FormattingEnabled = true;
            this.stmPlayerChannelType.Items.AddRange(new object[] {
            "Mono",
            "Stereo"});
            this.stmPlayerChannelType.Location = new System.Drawing.Point(14, 28);
            this.stmPlayerChannelType.Name = "stmPlayerChannelType";
            this.stmPlayerChannelType.Size = new System.Drawing.Size(298, 21);
            this.stmPlayerChannelType.TabIndex = 4;
            this.toolTip.SetToolTip(this.stmPlayerChannelType, "If the stream is stereo or mono.");
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.Location = new System.Drawing.Point(11, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(301, 22);
            this.label12.TabIndex = 3;
            this.label12.Text = "Channel Type:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.stmPlayerLeftChannelBox, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.stmPlayerRightChannelBox, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.rightChannelLabel, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.leftChannelLabel, 0, 0);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(14, 55);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(298, 45);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // stmPlayerLeftChannelBox
            // 
            this.stmPlayerLeftChannelBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stmPlayerLeftChannelBox.Location = new System.Drawing.Point(3, 25);
            this.stmPlayerLeftChannelBox.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.stmPlayerLeftChannelBox.Name = "stmPlayerLeftChannelBox";
            this.stmPlayerLeftChannelBox.Size = new System.Drawing.Size(143, 20);
            this.stmPlayerLeftChannelBox.TabIndex = 4;
            this.toolTip.SetToolTip(this.stmPlayerLeftChannelBox, "Channel to use for the stream.");
            // 
            // stmPlayerRightChannelBox
            // 
            this.stmPlayerRightChannelBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stmPlayerRightChannelBox.Location = new System.Drawing.Point(152, 25);
            this.stmPlayerRightChannelBox.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.stmPlayerRightChannelBox.Name = "stmPlayerRightChannelBox";
            this.stmPlayerRightChannelBox.Size = new System.Drawing.Size(143, 20);
            this.stmPlayerRightChannelBox.TabIndex = 3;
            this.toolTip.SetToolTip(this.stmPlayerRightChannelBox, "Channel to use for the stream.");
            // 
            // rightChannelLabel
            // 
            this.rightChannelLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightChannelLabel.Location = new System.Drawing.Point(152, 0);
            this.rightChannelLabel.Name = "rightChannelLabel";
            this.rightChannelLabel.Size = new System.Drawing.Size(143, 22);
            this.rightChannelLabel.TabIndex = 2;
            this.rightChannelLabel.Text = "Right Channel:";
            this.rightChannelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // leftChannelLabel
            // 
            this.leftChannelLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftChannelLabel.Location = new System.Drawing.Point(3, 0);
            this.leftChannelLabel.Name = "leftChannelLabel";
            this.leftChannelLabel.Size = new System.Drawing.Size(143, 22);
            this.leftChannelLabel.TabIndex = 1;
            this.leftChannelLabel.Text = "Left Channel:";
            this.leftChannelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpPanel
            // 
            this.grpPanel.Controls.Add(this.grpEntries);
            this.grpPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPanel.Location = new System.Drawing.Point(0, 270);
            this.grpPanel.Name = "grpPanel";
            this.grpPanel.Size = new System.Drawing.Size(325, 268);
            this.grpPanel.TabIndex = 12;
            this.grpPanel.Visible = false;
            // 
            // grpEntries
            // 
            this.grpEntries.AllowUserToOrderColumns = true;
            this.grpEntries.AllowUserToResizeRows = false;
            this.grpEntries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grpEntries.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.item,
            this.loadFlags});
            this.grpEntries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEntries.Location = new System.Drawing.Point(0, 0);
            this.grpEntries.Name = "grpEntries";
            this.grpEntries.Size = new System.Drawing.Size(325, 268);
            this.grpEntries.TabIndex = 0;
            // 
            // item
            // 
            this.item.FillWeight = 1750F;
            this.item.HeaderText = "Item";
            this.item.Name = "item";
            this.item.Width = 175;
            // 
            // loadFlags
            // 
            this.loadFlags.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.loadFlags.HeaderText = "Load Flags";
            this.loadFlags.Name = "loadFlags";
            this.loadFlags.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.loadFlags.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // bankPanel
            // 
            this.bankPanel.Controls.Add(this.tableLayoutPanel5);
            this.bankPanel.Controls.Add(this.label11);
            this.bankPanel.Controls.Add(this.tableLayoutPanel4);
            this.bankPanel.Controls.Add(this.label10);
            this.bankPanel.Controls.Add(this.tableLayoutPanel3);
            this.bankPanel.Controls.Add(this.label7);
            this.bankPanel.Controls.Add(this.tableLayoutPanel2);
            this.bankPanel.Controls.Add(this.label6);
            this.bankPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bankPanel.Location = new System.Drawing.Point(0, 270);
            this.bankPanel.Name = "bankPanel";
            this.bankPanel.Size = new System.Drawing.Size(325, 268);
            this.bankPanel.TabIndex = 11;
            this.bankPanel.Visible = false;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel5.Controls.Add(this.bnkWar3ComboBox, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.bnkWar3Box, 1, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(14, 193);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(298, 31);
            this.tableLayoutPanel5.TabIndex = 13;
            // 
            // bnkWar3ComboBox
            // 
            this.bnkWar3ComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bnkWar3ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bnkWar3ComboBox.FormattingEnabled = true;
            this.bnkWar3ComboBox.Location = new System.Drawing.Point(3, 3);
            this.bnkWar3ComboBox.Name = "bnkWar3ComboBox";
            this.bnkWar3ComboBox.Size = new System.Drawing.Size(247, 21);
            this.bnkWar3ComboBox.TabIndex = 6;
            this.toolTip.SetToolTip(this.bnkWar3ComboBox, "Wave archive to be used for the bank.");
            // 
            // bnkWar3Box
            // 
            this.bnkWar3Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bnkWar3Box.Location = new System.Drawing.Point(256, 3);
            this.bnkWar3Box.Maximum = new decimal(new int[] {
            65534,
            0,
            0,
            0});
            this.bnkWar3Box.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.bnkWar3Box.Name = "bnkWar3Box";
            this.bnkWar3Box.Size = new System.Drawing.Size(39, 20);
            this.bnkWar3Box.TabIndex = 7;
            this.toolTip.SetToolTip(this.bnkWar3Box, "Id of the wave archive to use for this bank.");
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.Location = new System.Drawing.Point(11, 171);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(301, 22);
            this.label11.TabIndex = 12;
            this.label11.Text = "Wave Archive 3:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel4.Controls.Add(this.bnkWar2ComboBox, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.bnkWar2Box, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(14, 137);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(298, 31);
            this.tableLayoutPanel4.TabIndex = 11;
            // 
            // bnkWar2ComboBox
            // 
            this.bnkWar2ComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bnkWar2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bnkWar2ComboBox.FormattingEnabled = true;
            this.bnkWar2ComboBox.Location = new System.Drawing.Point(3, 3);
            this.bnkWar2ComboBox.Name = "bnkWar2ComboBox";
            this.bnkWar2ComboBox.Size = new System.Drawing.Size(247, 21);
            this.bnkWar2ComboBox.TabIndex = 6;
            this.toolTip.SetToolTip(this.bnkWar2ComboBox, "Wave archive to be used for the bank.");
            // 
            // bnkWar2Box
            // 
            this.bnkWar2Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bnkWar2Box.Location = new System.Drawing.Point(256, 3);
            this.bnkWar2Box.Maximum = new decimal(new int[] {
            65534,
            0,
            0,
            0});
            this.bnkWar2Box.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.bnkWar2Box.Name = "bnkWar2Box";
            this.bnkWar2Box.Size = new System.Drawing.Size(39, 20);
            this.bnkWar2Box.TabIndex = 7;
            this.toolTip.SetToolTip(this.bnkWar2Box, "Id of the wave archive to use for this bank.");
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.Location = new System.Drawing.Point(11, 115);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(301, 22);
            this.label10.TabIndex = 10;
            this.label10.Text = "Wave Archive 2:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.Controls.Add(this.bnkWar1ComboBox, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.bnkWar1Box, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(14, 81);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(298, 31);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // bnkWar1ComboBox
            // 
            this.bnkWar1ComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bnkWar1ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bnkWar1ComboBox.FormattingEnabled = true;
            this.bnkWar1ComboBox.Location = new System.Drawing.Point(3, 3);
            this.bnkWar1ComboBox.Name = "bnkWar1ComboBox";
            this.bnkWar1ComboBox.Size = new System.Drawing.Size(247, 21);
            this.bnkWar1ComboBox.TabIndex = 6;
            this.toolTip.SetToolTip(this.bnkWar1ComboBox, "Wave archive to be used for the bank.");
            // 
            // bnkWar1Box
            // 
            this.bnkWar1Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bnkWar1Box.Location = new System.Drawing.Point(256, 3);
            this.bnkWar1Box.Maximum = new decimal(new int[] {
            65534,
            0,
            0,
            0});
            this.bnkWar1Box.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.bnkWar1Box.Name = "bnkWar1Box";
            this.bnkWar1Box.Size = new System.Drawing.Size(39, 20);
            this.bnkWar1Box.TabIndex = 7;
            this.toolTip.SetToolTip(this.bnkWar1Box, "Id of the wave archive to use for this bank.");
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Location = new System.Drawing.Point(11, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(301, 22);
            this.label7.TabIndex = 8;
            this.label7.Text = "Wave Archive 1:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.Controls.Add(this.bnkWar0ComboBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.bnkWar0Box, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(14, 25);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(298, 31);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // bnkWar0ComboBox
            // 
            this.bnkWar0ComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bnkWar0ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bnkWar0ComboBox.FormattingEnabled = true;
            this.bnkWar0ComboBox.Location = new System.Drawing.Point(3, 3);
            this.bnkWar0ComboBox.Name = "bnkWar0ComboBox";
            this.bnkWar0ComboBox.Size = new System.Drawing.Size(247, 21);
            this.bnkWar0ComboBox.TabIndex = 6;
            this.toolTip.SetToolTip(this.bnkWar0ComboBox, "Wave archive to be used for the bank.");
            // 
            // bnkWar0Box
            // 
            this.bnkWar0Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bnkWar0Box.Location = new System.Drawing.Point(256, 3);
            this.bnkWar0Box.Maximum = new decimal(new int[] {
            65534,
            0,
            0,
            0});
            this.bnkWar0Box.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.bnkWar0Box.Name = "bnkWar0Box";
            this.bnkWar0Box.Size = new System.Drawing.Size(39, 20);
            this.bnkWar0Box.TabIndex = 7;
            this.toolTip.SetToolTip(this.bnkWar0Box, "Id of the wave archive to use for this bank.");
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Location = new System.Drawing.Point(11, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(301, 22);
            this.label6.TabIndex = 2;
            this.label6.Text = "Wave Archive 0:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // blankPanel
            // 
            this.blankPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blankPanel.Location = new System.Drawing.Point(0, 270);
            this.blankPanel.Name = "blankPanel";
            this.blankPanel.Size = new System.Drawing.Size(325, 268);
            this.blankPanel.TabIndex = 10;
            this.blankPanel.Visible = false;
            // 
            // warPanel
            // 
            this.warPanel.Controls.Add(this.loadIndividuallyBox);
            this.warPanel.Controls.Add(this.label9);
            this.warPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.warPanel.Location = new System.Drawing.Point(0, 270);
            this.warPanel.Name = "warPanel";
            this.warPanel.Size = new System.Drawing.Size(325, 268);
            this.warPanel.TabIndex = 9;
            this.warPanel.Visible = false;
            // 
            // loadIndividuallyBox
            // 
            this.loadIndividuallyBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadIndividuallyBox.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.loadIndividuallyBox.Location = new System.Drawing.Point(11, 25);
            this.loadIndividuallyBox.Name = "loadIndividuallyBox";
            this.loadIndividuallyBox.Size = new System.Drawing.Size(301, 24);
            this.loadIndividuallyBox.TabIndex = 1;
            this.toolTip.SetToolTip(this.loadIndividuallyBox, "If the wave archive should be loaded individually.");
            this.loadIndividuallyBox.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.Location = new System.Drawing.Point(11, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(301, 22);
            this.label9.TabIndex = 0;
            this.label9.Text = "Load Individually:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // forceUniqueFilePanel
            // 
            this.forceUniqueFilePanel.Controls.Add(this.forceUniqueFileBox);
            this.forceUniqueFilePanel.Controls.Add(this.label8);
            this.forceUniqueFilePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.forceUniqueFilePanel.Location = new System.Drawing.Point(0, 231);
            this.forceUniqueFilePanel.Name = "forceUniqueFilePanel";
            this.forceUniqueFilePanel.Size = new System.Drawing.Size(325, 39);
            this.forceUniqueFilePanel.TabIndex = 8;
            this.forceUniqueFilePanel.Visible = false;
            // 
            // forceUniqueFileBox
            // 
            this.forceUniqueFileBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.forceUniqueFileBox.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.forceUniqueFileBox.Location = new System.Drawing.Point(11, 19);
            this.forceUniqueFileBox.Name = "forceUniqueFileBox";
            this.forceUniqueFileBox.Size = new System.Drawing.Size(301, 18);
            this.forceUniqueFileBox.TabIndex = 1;
            this.toolTip.SetToolTip(this.forceUniqueFileBox, "Write this file in the sound archive as its own file, even if it has the exact sa" +
        "me data as another one. If this is not checked, files will be shared between ent" +
        "ries for efficiency.");
            this.forceUniqueFileBox.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Location = new System.Drawing.Point(11, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(301, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "Force Unique File:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // indexPanel
            // 
            this.indexPanel.Controls.Add(this.swapAtIndexButton);
            this.indexPanel.Controls.Add(this.itemIndexBox);
            this.indexPanel.Controls.Add(this.label5);
            this.indexPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.indexPanel.Location = new System.Drawing.Point(0, 150);
            this.indexPanel.Name = "indexPanel";
            this.indexPanel.Size = new System.Drawing.Size(325, 81);
            this.indexPanel.TabIndex = 0;
            this.indexPanel.Visible = false;
            // 
            // swapAtIndexButton
            // 
            this.swapAtIndexButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.swapAtIndexButton.Location = new System.Drawing.Point(14, 49);
            this.swapAtIndexButton.Name = "swapAtIndexButton";
            this.swapAtIndexButton.Size = new System.Drawing.Size(298, 25);
            this.swapAtIndexButton.TabIndex = 0;
            this.swapAtIndexButton.Text = "Swap With Index";
            this.toolTip.SetToolTip(this.swapAtIndexButton, "Swap this entry with the one at the new index. If that entry doesn\'t exist, simpl" +
        "y just change the index.");
            this.swapAtIndexButton.UseVisualStyleBackColor = true;
            // 
            // itemIndexBox
            // 
            this.itemIndexBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemIndexBox.Location = new System.Drawing.Point(14, 23);
            this.itemIndexBox.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.itemIndexBox.Name = "itemIndexBox";
            this.itemIndexBox.Size = new System.Drawing.Size(298, 20);
            this.itemIndexBox.TabIndex = 1;
            this.toolTip.SetToolTip(this.itemIndexBox, "The index of the item as referenced to by the game.");
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(11, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(301, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Item Index:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // settingsPanel
            // 
            this.settingsPanel.Controls.Add(this.seqExportModeBox);
            this.settingsPanel.Controls.Add(this.label4);
            this.settingsPanel.Controls.Add(this.seqImportModeBox);
            this.settingsPanel.Controls.Add(this.label3);
            this.settingsPanel.Controls.Add(this.writeNamesBox);
            this.settingsPanel.Controls.Add(this.label2);
            this.settingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPanel.Location = new System.Drawing.Point(0, 150);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(325, 388);
            this.settingsPanel.TabIndex = 1;
            this.settingsPanel.Visible = false;
            // 
            // seqExportModeBox
            // 
            this.seqExportModeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seqExportModeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.seqExportModeBox.FormattingEnabled = true;
            this.seqExportModeBox.Items.AddRange(new object[] {
            "Nitro Studio",
            "Sseq2Midi"});
            this.seqExportModeBox.Location = new System.Drawing.Point(11, 126);
            this.seqExportModeBox.Name = "seqExportModeBox";
            this.seqExportModeBox.Size = new System.Drawing.Size(301, 21);
            this.seqExportModeBox.TabIndex = 5;
            this.toolTip.SetToolTip(this.seqExportModeBox, "What program should be used to export sequences. Nitro Studio is my custom export" +
        "er, while Sseq2Midi is the exe included. I recommend you use my exporter.");
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(11, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(301, 22);
            this.label4.TabIndex = 4;
            this.label4.Text = "Sequence Export Mode:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // seqImportModeBox
            // 
            this.seqImportModeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seqImportModeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.seqImportModeBox.FormattingEnabled = true;
            this.seqImportModeBox.Items.AddRange(new object[] {
            "Nitro Studio",
            "Midi2Sseq",
            "Nintendo Tools"});
            this.seqImportModeBox.Location = new System.Drawing.Point(11, 77);
            this.seqImportModeBox.Name = "seqImportModeBox";
            this.seqImportModeBox.Size = new System.Drawing.Size(301, 21);
            this.seqImportModeBox.TabIndex = 3;
            this.toolTip.SetToolTip(this.seqImportModeBox, resources.GetString("seqImportModeBox.ToolTip"));
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(11, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(301, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "Sequence Import Mode:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // writeNamesBox
            // 
            this.writeNamesBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.writeNamesBox.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.writeNamesBox.Location = new System.Drawing.Point(11, 25);
            this.writeNamesBox.Name = "writeNamesBox";
            this.writeNamesBox.Size = new System.Drawing.Size(301, 24);
            this.writeNamesBox.TabIndex = 1;
            this.toolTip.SetToolTip(this.writeNamesBox, "If the editor should export names for the sound archive.");
            this.writeNamesBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(11, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(301, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "Write Names:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // noInfoPanel
            // 
            this.noInfoPanel.Controls.Add(this.label1);
            this.noInfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noInfoPanel.Location = new System.Drawing.Point(0, 150);
            this.noInfoPanel.Name = "noInfoPanel";
            this.noInfoPanel.Size = new System.Drawing.Size(325, 388);
            this.noInfoPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(325, 388);
            this.label1.TabIndex = 0;
            this.label1.Text = "No Valid Info Selected!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kermalisSoundPlayerPanel
            // 
            this.kermalisSoundPlayerPanel.Controls.Add(this.kermalisPosition);
            this.kermalisSoundPlayerPanel.Controls.Add(this.tableLayoutPanel9);
            this.kermalisSoundPlayerPanel.Controls.Add(this.kermalisPlayButton);
            this.kermalisSoundPlayerPanel.Controls.Add(this.soundPlayerLabel);
            this.kermalisSoundPlayerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.kermalisSoundPlayerPanel.Location = new System.Drawing.Point(0, 0);
            this.kermalisSoundPlayerPanel.Name = "kermalisSoundPlayerPanel";
            this.kermalisSoundPlayerPanel.Size = new System.Drawing.Size(325, 150);
            this.kermalisSoundPlayerPanel.TabIndex = 16;
            this.kermalisSoundPlayerPanel.Visible = false;
            // 
            // kermalisPosition
            // 
            this.kermalisPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kermalisPosition.LargeChange = 20;
            this.kermalisPosition.Location = new System.Drawing.Point(14, 118);
            this.kermalisPosition.Maximum = 100;
            this.kermalisPosition.Name = "kermalisPosition";
            this.kermalisPosition.Size = new System.Drawing.Size(298, 45);
            this.kermalisPosition.TabIndex = 5;
            this.kermalisPosition.TickFrequency = 5;
            this.toolTip.SetToolTip(this.kermalisPosition, "Sound position.");
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Controls.Add(this.label22, 1, 1);
            this.tableLayoutPanel9.Controls.Add(this.label21, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.kermalisStopButton, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.kermalisPauseButton, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.kermalisVolumeSlider, 0, 2);
            this.tableLayoutPanel9.Controls.Add(this.kermalisLoopBox, 1, 2);
            this.tableLayoutPanel9.Location = new System.Drawing.Point(14, 49);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 3;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(298, 63);
            this.tableLayoutPanel9.TabIndex = 4;
            // 
            // label22
            // 
            this.label22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label22.Location = new System.Drawing.Point(152, 27);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(143, 15);
            this.label22.TabIndex = 5;
            this.label22.Text = "Loop:";
            this.label22.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label21
            // 
            this.label21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label21.Location = new System.Drawing.Point(3, 27);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(143, 15);
            this.label21.TabIndex = 4;
            this.label21.Text = "Volume:";
            this.label21.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // kermalisStopButton
            // 
            this.kermalisStopButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kermalisStopButton.Location = new System.Drawing.Point(152, 3);
            this.kermalisStopButton.Name = "kermalisStopButton";
            this.kermalisStopButton.Size = new System.Drawing.Size(143, 21);
            this.kermalisStopButton.TabIndex = 1;
            this.kermalisStopButton.Text = "Stop";
            this.kermalisStopButton.UseVisualStyleBackColor = true;
            // 
            // kermalisPauseButton
            // 
            this.kermalisPauseButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kermalisPauseButton.Location = new System.Drawing.Point(3, 3);
            this.kermalisPauseButton.Name = "kermalisPauseButton";
            this.kermalisPauseButton.Size = new System.Drawing.Size(143, 21);
            this.kermalisPauseButton.TabIndex = 0;
            this.kermalisPauseButton.Text = "Pause / Resume";
            this.kermalisPauseButton.UseVisualStyleBackColor = true;
            // 
            // kermalisVolumeSlider
            // 
            this.kermalisVolumeSlider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kermalisVolumeSlider.LargeChange = 10;
            this.kermalisVolumeSlider.Location = new System.Drawing.Point(3, 45);
            this.kermalisVolumeSlider.Maximum = 100;
            this.kermalisVolumeSlider.Name = "kermalisVolumeSlider";
            this.kermalisVolumeSlider.Size = new System.Drawing.Size(143, 21);
            this.kermalisVolumeSlider.SmallChange = 5;
            this.kermalisVolumeSlider.TabIndex = 2;
            this.kermalisVolumeSlider.TickFrequency = 10;
            this.kermalisVolumeSlider.Value = 75;
            // 
            // kermalisLoopBox
            // 
            this.kermalisLoopBox.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.kermalisLoopBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kermalisLoopBox.Location = new System.Drawing.Point(152, 45);
            this.kermalisLoopBox.Name = "kermalisLoopBox";
            this.kermalisLoopBox.Size = new System.Drawing.Size(143, 21);
            this.kermalisLoopBox.TabIndex = 3;
            this.kermalisLoopBox.UseVisualStyleBackColor = true;
            // 
            // kermalisPlayButton
            // 
            this.kermalisPlayButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kermalisPlayButton.Location = new System.Drawing.Point(14, 25);
            this.kermalisPlayButton.Name = "kermalisPlayButton";
            this.kermalisPlayButton.Size = new System.Drawing.Size(298, 20);
            this.kermalisPlayButton.TabIndex = 3;
            this.kermalisPlayButton.Text = "Play";
            this.kermalisPlayButton.UseVisualStyleBackColor = true;
            // 
            // soundPlayerLabel
            // 
            this.soundPlayerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.soundPlayerLabel.Location = new System.Drawing.Point(11, 3);
            this.soundPlayerLabel.Name = "soundPlayerLabel";
            this.soundPlayerLabel.Size = new System.Drawing.Size(301, 22);
            this.soundPlayerLabel.TabIndex = 1;
            this.soundPlayerLabel.Text = "Kermalis Sound Player:";
            this.soundPlayerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlPianoKeys
            // 
            this.pnlPianoKeys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPianoKeys.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnlPianoKeys.Controls.Add(this.pkeyC7);
            this.pnlPianoKeys.Controls.Add(this.pkeyE7);
            this.pnlPianoKeys.Controls.Add(this.pkeyCSharp7);
            this.pnlPianoKeys.Controls.Add(this.pkeyD7);
            this.pnlPianoKeys.Controls.Add(this.pkeyDSharp7);
            this.pnlPianoKeys.Controls.Add(this.pkeyF7);
            this.pnlPianoKeys.Controls.Add(this.pkeyFSharp7);
            this.pnlPianoKeys.Controls.Add(this.pkeyG7);
            this.pnlPianoKeys.Controls.Add(this.pkeyGSharp7);
            this.pnlPianoKeys.Controls.Add(this.pkeyA7);
            this.pnlPianoKeys.Controls.Add(this.pkeyASharp7);
            this.pnlPianoKeys.Controls.Add(this.pkeyB7);
            this.pnlPianoKeys.Controls.Add(this.pkeyC6);
            this.pnlPianoKeys.Controls.Add(this.pkeyE6);
            this.pnlPianoKeys.Controls.Add(this.pkeyCSharp6);
            this.pnlPianoKeys.Controls.Add(this.pkeyD6);
            this.pnlPianoKeys.Controls.Add(this.pkeyDSharp6);
            this.pnlPianoKeys.Controls.Add(this.pkeyF6);
            this.pnlPianoKeys.Controls.Add(this.pkeyFSharp6);
            this.pnlPianoKeys.Controls.Add(this.pkeyG6);
            this.pnlPianoKeys.Controls.Add(this.pkeyGSharp6);
            this.pnlPianoKeys.Controls.Add(this.pkeyA6);
            this.pnlPianoKeys.Controls.Add(this.pkeyASharp6);
            this.pnlPianoKeys.Controls.Add(this.pkeyB6);
            this.pnlPianoKeys.Controls.Add(this.pkeyC1);
            this.pnlPianoKeys.Controls.Add(this.pkeyCSharp1);
            this.pnlPianoKeys.Controls.Add(this.pkeyD1);
            this.pnlPianoKeys.Controls.Add(this.pkeyDSharp1);
            this.pnlPianoKeys.Controls.Add(this.pkeyE1);
            this.pnlPianoKeys.Controls.Add(this.pkeyF1);
            this.pnlPianoKeys.Controls.Add(this.pkeyFSharp1);
            this.pnlPianoKeys.Controls.Add(this.pkeyG1);
            this.pnlPianoKeys.Controls.Add(this.pkeyGSharp1);
            this.pnlPianoKeys.Controls.Add(this.pkeyA1);
            this.pnlPianoKeys.Controls.Add(this.pkeyASharp1);
            this.pnlPianoKeys.Controls.Add(this.pkeyB1);
            this.pnlPianoKeys.Controls.Add(this.pkeyC2);
            this.pnlPianoKeys.Controls.Add(this.pkeyCSharp2);
            this.pnlPianoKeys.Controls.Add(this.pkeyD2);
            this.pnlPianoKeys.Controls.Add(this.pkeyDSharp2);
            this.pnlPianoKeys.Controls.Add(this.pkeyE2);
            this.pnlPianoKeys.Controls.Add(this.pkeyF2);
            this.pnlPianoKeys.Controls.Add(this.pkeyFSharp2);
            this.pnlPianoKeys.Controls.Add(this.pkeyG2);
            this.pnlPianoKeys.Controls.Add(this.pkeyGSharp2);
            this.pnlPianoKeys.Controls.Add(this.pkeyA2);
            this.pnlPianoKeys.Controls.Add(this.pkeyASharp2);
            this.pnlPianoKeys.Controls.Add(this.pkeyB2);
            this.pnlPianoKeys.Controls.Add(this.pkeyC3);
            this.pnlPianoKeys.Controls.Add(this.pkeyCSharp3);
            this.pnlPianoKeys.Controls.Add(this.pkeyD3);
            this.pnlPianoKeys.Controls.Add(this.pkeyDSharp3);
            this.pnlPianoKeys.Controls.Add(this.pkeyE3);
            this.pnlPianoKeys.Controls.Add(this.pkeyF3);
            this.pnlPianoKeys.Controls.Add(this.pkeyFSharp3);
            this.pnlPianoKeys.Controls.Add(this.pkeyG3);
            this.pnlPianoKeys.Controls.Add(this.pkeyGSharp3);
            this.pnlPianoKeys.Controls.Add(this.pkeyA3);
            this.pnlPianoKeys.Controls.Add(this.pkeyASharp3);
            this.pnlPianoKeys.Controls.Add(this.pkeyB3);
            this.pnlPianoKeys.Controls.Add(this.pkeyC4);
            this.pnlPianoKeys.Controls.Add(this.pkeyCSharp4);
            this.pnlPianoKeys.Controls.Add(this.pkeyD4);
            this.pnlPianoKeys.Controls.Add(this.pkeyDSharp4);
            this.pnlPianoKeys.Controls.Add(this.pkeyE4);
            this.pnlPianoKeys.Controls.Add(this.pkeyF4);
            this.pnlPianoKeys.Controls.Add(this.pkeyFSharp4);
            this.pnlPianoKeys.Controls.Add(this.pkeyG4);
            this.pnlPianoKeys.Controls.Add(this.pkeyGSharp4);
            this.pnlPianoKeys.Controls.Add(this.pkeyA4);
            this.pnlPianoKeys.Controls.Add(this.pkeyASharp4);
            this.pnlPianoKeys.Controls.Add(this.pkeyB4);
            this.pnlPianoKeys.Controls.Add(this.pkeyC5);
            this.pnlPianoKeys.Controls.Add(this.pkeyCSharp5);
            this.pnlPianoKeys.Controls.Add(this.pkeyD5);
            this.pnlPianoKeys.Controls.Add(this.pkeyDSharp5);
            this.pnlPianoKeys.Controls.Add(this.pkeyE5);
            this.pnlPianoKeys.Controls.Add(this.pkeyF5);
            this.pnlPianoKeys.Controls.Add(this.pkeyFSharp5);
            this.pnlPianoKeys.Controls.Add(this.pkeyG5);
            this.pnlPianoKeys.Controls.Add(this.pkeyGSharp5);
            this.pnlPianoKeys.Controls.Add(this.pkeyA5);
            this.pnlPianoKeys.Controls.Add(this.pkeyASharp5);
            this.pnlPianoKeys.Controls.Add(this.pkeyB5);
            this.pnlPianoKeys.Controls.Add(this.pkeyC8);
            this.pnlPianoKeys.Location = new System.Drawing.Point(44, 478);
            this.pnlPianoKeys.Name = "pnlPianoKeys";
            this.pnlPianoKeys.Size = new System.Drawing.Size(565, 46);
            this.pnlPianoKeys.TabIndex = 6;
            this.pnlPianoKeys.Visible = false;
            // 
            // pkeyC7
            // 
            this.pkeyC7.KeyOffColor = System.Drawing.Color.White;
            this.pkeyC7.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyC7.Location = new System.Drawing.Point(466, 2);
            this.pkeyC7.Name = "pkeyC7";
            this.pkeyC7.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyC7.Shape = Multimedia.UI.PianoKeyShape.LShape;
            this.pkeyC7.Size = new System.Drawing.Size(12, 42);
            this.pkeyC7.TabIndex = 83;
            this.pkeyC7.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyE7
            // 
            this.pkeyE7.KeyOffColor = System.Drawing.Color.White;
            this.pkeyE7.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyE7.Location = new System.Drawing.Point(488, 2);
            this.pkeyE7.Name = "pkeyE7";
            this.pkeyE7.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyE7.Shape = Multimedia.UI.PianoKeyShape.LShapeBackwards;
            this.pkeyE7.Size = new System.Drawing.Size(12, 42);
            this.pkeyE7.TabIndex = 84;
            this.pkeyE7.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyCSharp7
            // 
            this.pkeyCSharp7.BackColor = System.Drawing.Color.Black;
            this.pkeyCSharp7.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyCSharp7.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyCSharp7.Location = new System.Drawing.Point(474, 2);
            this.pkeyCSharp7.Name = "pkeyCSharp7";
            this.pkeyCSharp7.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyCSharp7.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyCSharp7.Size = new System.Drawing.Size(8, 28);
            this.pkeyCSharp7.TabIndex = 74;
            this.pkeyCSharp7.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyD7
            // 
            this.pkeyD7.KeyOffColor = System.Drawing.Color.White;
            this.pkeyD7.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyD7.Location = new System.Drawing.Point(477, 2);
            this.pkeyD7.Name = "pkeyD7";
            this.pkeyD7.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyD7.Shape = Multimedia.UI.PianoKeyShape.TShape;
            this.pkeyD7.Size = new System.Drawing.Size(12, 42);
            this.pkeyD7.TabIndex = 73;
            this.pkeyD7.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyDSharp7
            // 
            this.pkeyDSharp7.BackColor = System.Drawing.Color.Black;
            this.pkeyDSharp7.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyDSharp7.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyDSharp7.Location = new System.Drawing.Point(485, 2);
            this.pkeyDSharp7.Name = "pkeyDSharp7";
            this.pkeyDSharp7.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyDSharp7.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyDSharp7.Size = new System.Drawing.Size(8, 29);
            this.pkeyDSharp7.TabIndex = 75;
            this.pkeyDSharp7.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyF7
            // 
            this.pkeyF7.KeyOffColor = System.Drawing.Color.White;
            this.pkeyF7.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyF7.Location = new System.Drawing.Point(499, 2);
            this.pkeyF7.Name = "pkeyF7";
            this.pkeyF7.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyF7.Shape = Multimedia.UI.PianoKeyShape.LShape;
            this.pkeyF7.Size = new System.Drawing.Size(12, 42);
            this.pkeyF7.TabIndex = 76;
            this.pkeyF7.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyFSharp7
            // 
            this.pkeyFSharp7.BackColor = System.Drawing.Color.Black;
            this.pkeyFSharp7.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyFSharp7.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyFSharp7.Location = new System.Drawing.Point(507, 2);
            this.pkeyFSharp7.Name = "pkeyFSharp7";
            this.pkeyFSharp7.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyFSharp7.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyFSharp7.Size = new System.Drawing.Size(8, 28);
            this.pkeyFSharp7.TabIndex = 78;
            this.pkeyFSharp7.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyG7
            // 
            this.pkeyG7.KeyOffColor = System.Drawing.Color.White;
            this.pkeyG7.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyG7.Location = new System.Drawing.Point(510, 2);
            this.pkeyG7.Name = "pkeyG7";
            this.pkeyG7.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyG7.Shape = Multimedia.UI.PianoKeyShape.TShape;
            this.pkeyG7.Size = new System.Drawing.Size(12, 42);
            this.pkeyG7.TabIndex = 77;
            this.pkeyG7.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyGSharp7
            // 
            this.pkeyGSharp7.BackColor = System.Drawing.Color.Black;
            this.pkeyGSharp7.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyGSharp7.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyGSharp7.Location = new System.Drawing.Point(518, 2);
            this.pkeyGSharp7.Name = "pkeyGSharp7";
            this.pkeyGSharp7.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyGSharp7.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyGSharp7.Size = new System.Drawing.Size(8, 29);
            this.pkeyGSharp7.TabIndex = 79;
            this.pkeyGSharp7.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyA7
            // 
            this.pkeyA7.KeyOffColor = System.Drawing.Color.White;
            this.pkeyA7.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyA7.Location = new System.Drawing.Point(521, 2);
            this.pkeyA7.Name = "pkeyA7";
            this.pkeyA7.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyA7.Shape = Multimedia.UI.PianoKeyShape.TShape;
            this.pkeyA7.Size = new System.Drawing.Size(12, 42);
            this.pkeyA7.TabIndex = 80;
            this.pkeyA7.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyASharp7
            // 
            this.pkeyASharp7.BackColor = System.Drawing.Color.Black;
            this.pkeyASharp7.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyASharp7.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyASharp7.Location = new System.Drawing.Point(529, 2);
            this.pkeyASharp7.Name = "pkeyASharp7";
            this.pkeyASharp7.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyASharp7.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyASharp7.Size = new System.Drawing.Size(8, 29);
            this.pkeyASharp7.TabIndex = 81;
            this.pkeyASharp7.Text = "pianoKey13";
            this.pkeyASharp7.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyB7
            // 
            this.pkeyB7.KeyOffColor = System.Drawing.Color.White;
            this.pkeyB7.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyB7.Location = new System.Drawing.Point(532, 2);
            this.pkeyB7.Name = "pkeyB7";
            this.pkeyB7.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyB7.Shape = Multimedia.UI.PianoKeyShape.LShapeBackwards;
            this.pkeyB7.Size = new System.Drawing.Size(12, 42);
            this.pkeyB7.TabIndex = 82;
            this.pkeyB7.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyC6
            // 
            this.pkeyC6.KeyOffColor = System.Drawing.Color.White;
            this.pkeyC6.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyC6.Location = new System.Drawing.Point(389, 2);
            this.pkeyC6.Name = "pkeyC6";
            this.pkeyC6.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyC6.Shape = Multimedia.UI.PianoKeyShape.LShape;
            this.pkeyC6.Size = new System.Drawing.Size(12, 42);
            this.pkeyC6.TabIndex = 71;
            this.pkeyC6.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyE6
            // 
            this.pkeyE6.KeyOffColor = System.Drawing.Color.White;
            this.pkeyE6.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyE6.Location = new System.Drawing.Point(411, 2);
            this.pkeyE6.Name = "pkeyE6";
            this.pkeyE6.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyE6.Shape = Multimedia.UI.PianoKeyShape.LShapeBackwards;
            this.pkeyE6.Size = new System.Drawing.Size(12, 42);
            this.pkeyE6.TabIndex = 72;
            this.pkeyE6.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyCSharp6
            // 
            this.pkeyCSharp6.BackColor = System.Drawing.Color.Black;
            this.pkeyCSharp6.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyCSharp6.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyCSharp6.Location = new System.Drawing.Point(397, 2);
            this.pkeyCSharp6.Name = "pkeyCSharp6";
            this.pkeyCSharp6.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyCSharp6.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyCSharp6.Size = new System.Drawing.Size(8, 28);
            this.pkeyCSharp6.TabIndex = 62;
            this.pkeyCSharp6.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyD6
            // 
            this.pkeyD6.KeyOffColor = System.Drawing.Color.White;
            this.pkeyD6.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyD6.Location = new System.Drawing.Point(400, 2);
            this.pkeyD6.Name = "pkeyD6";
            this.pkeyD6.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyD6.Shape = Multimedia.UI.PianoKeyShape.TShape;
            this.pkeyD6.Size = new System.Drawing.Size(12, 42);
            this.pkeyD6.TabIndex = 61;
            this.pkeyD6.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyDSharp6
            // 
            this.pkeyDSharp6.BackColor = System.Drawing.Color.Black;
            this.pkeyDSharp6.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyDSharp6.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyDSharp6.Location = new System.Drawing.Point(408, 2);
            this.pkeyDSharp6.Name = "pkeyDSharp6";
            this.pkeyDSharp6.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyDSharp6.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyDSharp6.Size = new System.Drawing.Size(8, 29);
            this.pkeyDSharp6.TabIndex = 63;
            this.pkeyDSharp6.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyF6
            // 
            this.pkeyF6.KeyOffColor = System.Drawing.Color.White;
            this.pkeyF6.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyF6.Location = new System.Drawing.Point(422, 2);
            this.pkeyF6.Name = "pkeyF6";
            this.pkeyF6.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyF6.Shape = Multimedia.UI.PianoKeyShape.LShape;
            this.pkeyF6.Size = new System.Drawing.Size(12, 42);
            this.pkeyF6.TabIndex = 64;
            this.pkeyF6.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyFSharp6
            // 
            this.pkeyFSharp6.BackColor = System.Drawing.Color.Black;
            this.pkeyFSharp6.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyFSharp6.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyFSharp6.Location = new System.Drawing.Point(430, 2);
            this.pkeyFSharp6.Name = "pkeyFSharp6";
            this.pkeyFSharp6.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyFSharp6.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyFSharp6.Size = new System.Drawing.Size(8, 28);
            this.pkeyFSharp6.TabIndex = 66;
            this.pkeyFSharp6.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyG6
            // 
            this.pkeyG6.KeyOffColor = System.Drawing.Color.White;
            this.pkeyG6.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyG6.Location = new System.Drawing.Point(433, 2);
            this.pkeyG6.Name = "pkeyG6";
            this.pkeyG6.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyG6.Shape = Multimedia.UI.PianoKeyShape.TShape;
            this.pkeyG6.Size = new System.Drawing.Size(12, 42);
            this.pkeyG6.TabIndex = 65;
            this.pkeyG6.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyGSharp6
            // 
            this.pkeyGSharp6.BackColor = System.Drawing.Color.Black;
            this.pkeyGSharp6.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyGSharp6.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyGSharp6.Location = new System.Drawing.Point(441, 2);
            this.pkeyGSharp6.Name = "pkeyGSharp6";
            this.pkeyGSharp6.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyGSharp6.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyGSharp6.Size = new System.Drawing.Size(8, 29);
            this.pkeyGSharp6.TabIndex = 67;
            this.pkeyGSharp6.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyA6
            // 
            this.pkeyA6.KeyOffColor = System.Drawing.Color.White;
            this.pkeyA6.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyA6.Location = new System.Drawing.Point(444, 2);
            this.pkeyA6.Name = "pkeyA6";
            this.pkeyA6.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyA6.Shape = Multimedia.UI.PianoKeyShape.TShape;
            this.pkeyA6.Size = new System.Drawing.Size(12, 42);
            this.pkeyA6.TabIndex = 68;
            this.pkeyA6.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyASharp6
            // 
            this.pkeyASharp6.BackColor = System.Drawing.Color.Black;
            this.pkeyASharp6.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyASharp6.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyASharp6.Location = new System.Drawing.Point(452, 2);
            this.pkeyASharp6.Name = "pkeyASharp6";
            this.pkeyASharp6.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyASharp6.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyASharp6.Size = new System.Drawing.Size(8, 29);
            this.pkeyASharp6.TabIndex = 69;
            this.pkeyASharp6.Text = "pianoKey13";
            this.pkeyASharp6.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyB6
            // 
            this.pkeyB6.KeyOffColor = System.Drawing.Color.White;
            this.pkeyB6.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyB6.Location = new System.Drawing.Point(455, 2);
            this.pkeyB6.Name = "pkeyB6";
            this.pkeyB6.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyB6.Shape = Multimedia.UI.PianoKeyShape.LShapeBackwards;
            this.pkeyB6.Size = new System.Drawing.Size(12, 42);
            this.pkeyB6.TabIndex = 70;
            this.pkeyB6.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyC1
            // 
            this.pkeyC1.KeyOffColor = System.Drawing.Color.White;
            this.pkeyC1.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyC1.Location = new System.Drawing.Point(4, 2);
            this.pkeyC1.Name = "pkeyC1";
            this.pkeyC1.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyC1.Shape = Multimedia.UI.PianoKeyShape.LShape;
            this.pkeyC1.Size = new System.Drawing.Size(12, 42);
            this.pkeyC1.TabIndex = 0;
            this.pkeyC1.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyCSharp1
            // 
            this.pkeyCSharp1.BackColor = System.Drawing.Color.Black;
            this.pkeyCSharp1.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyCSharp1.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyCSharp1.Location = new System.Drawing.Point(12, 2);
            this.pkeyCSharp1.Name = "pkeyCSharp1";
            this.pkeyCSharp1.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyCSharp1.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyCSharp1.Size = new System.Drawing.Size(8, 28);
            this.pkeyCSharp1.TabIndex = 3;
            this.pkeyCSharp1.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyD1
            // 
            this.pkeyD1.KeyOffColor = System.Drawing.Color.White;
            this.pkeyD1.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyD1.Location = new System.Drawing.Point(15, 2);
            this.pkeyD1.Name = "pkeyD1";
            this.pkeyD1.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyD1.Shape = Multimedia.UI.PianoKeyShape.TShape;
            this.pkeyD1.Size = new System.Drawing.Size(12, 42);
            this.pkeyD1.TabIndex = 1;
            this.pkeyD1.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyDSharp1
            // 
            this.pkeyDSharp1.BackColor = System.Drawing.Color.Black;
            this.pkeyDSharp1.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyDSharp1.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyDSharp1.Location = new System.Drawing.Point(23, 2);
            this.pkeyDSharp1.Name = "pkeyDSharp1";
            this.pkeyDSharp1.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyDSharp1.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyDSharp1.Size = new System.Drawing.Size(8, 29);
            this.pkeyDSharp1.TabIndex = 4;
            this.pkeyDSharp1.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyE1
            // 
            this.pkeyE1.KeyOffColor = System.Drawing.Color.White;
            this.pkeyE1.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyE1.Location = new System.Drawing.Point(26, 2);
            this.pkeyE1.Name = "pkeyE1";
            this.pkeyE1.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyE1.Shape = Multimedia.UI.PianoKeyShape.LShapeBackwards;
            this.pkeyE1.Size = new System.Drawing.Size(12, 42);
            this.pkeyE1.TabIndex = 2;
            this.pkeyE1.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyF1
            // 
            this.pkeyF1.KeyOffColor = System.Drawing.Color.White;
            this.pkeyF1.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyF1.Location = new System.Drawing.Point(37, 2);
            this.pkeyF1.Name = "pkeyF1";
            this.pkeyF1.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyF1.Shape = Multimedia.UI.PianoKeyShape.LShape;
            this.pkeyF1.Size = new System.Drawing.Size(12, 42);
            this.pkeyF1.TabIndex = 5;
            this.pkeyF1.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyFSharp1
            // 
            this.pkeyFSharp1.BackColor = System.Drawing.Color.Black;
            this.pkeyFSharp1.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyFSharp1.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyFSharp1.Location = new System.Drawing.Point(45, 2);
            this.pkeyFSharp1.Name = "pkeyFSharp1";
            this.pkeyFSharp1.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyFSharp1.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyFSharp1.Size = new System.Drawing.Size(8, 28);
            this.pkeyFSharp1.TabIndex = 7;
            this.pkeyFSharp1.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyG1
            // 
            this.pkeyG1.KeyOffColor = System.Drawing.Color.White;
            this.pkeyG1.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyG1.Location = new System.Drawing.Point(48, 2);
            this.pkeyG1.Name = "pkeyG1";
            this.pkeyG1.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyG1.Shape = Multimedia.UI.PianoKeyShape.TShape;
            this.pkeyG1.Size = new System.Drawing.Size(12, 42);
            this.pkeyG1.TabIndex = 6;
            this.pkeyG1.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyGSharp1
            // 
            this.pkeyGSharp1.BackColor = System.Drawing.Color.Black;
            this.pkeyGSharp1.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyGSharp1.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyGSharp1.Location = new System.Drawing.Point(56, 2);
            this.pkeyGSharp1.Name = "pkeyGSharp1";
            this.pkeyGSharp1.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyGSharp1.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyGSharp1.Size = new System.Drawing.Size(8, 29);
            this.pkeyGSharp1.TabIndex = 8;
            this.pkeyGSharp1.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyA1
            // 
            this.pkeyA1.KeyOffColor = System.Drawing.Color.White;
            this.pkeyA1.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyA1.Location = new System.Drawing.Point(59, 2);
            this.pkeyA1.Name = "pkeyA1";
            this.pkeyA1.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyA1.Shape = Multimedia.UI.PianoKeyShape.TShape;
            this.pkeyA1.Size = new System.Drawing.Size(12, 42);
            this.pkeyA1.TabIndex = 9;
            this.pkeyA1.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyASharp1
            // 
            this.pkeyASharp1.BackColor = System.Drawing.Color.Black;
            this.pkeyASharp1.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyASharp1.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyASharp1.Location = new System.Drawing.Point(67, 2);
            this.pkeyASharp1.Name = "pkeyASharp1";
            this.pkeyASharp1.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyASharp1.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyASharp1.Size = new System.Drawing.Size(8, 29);
            this.pkeyASharp1.TabIndex = 10;
            this.pkeyASharp1.Text = "pianoKey13";
            this.pkeyASharp1.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyB1
            // 
            this.pkeyB1.KeyOffColor = System.Drawing.Color.White;
            this.pkeyB1.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyB1.Location = new System.Drawing.Point(70, 2);
            this.pkeyB1.Name = "pkeyB1";
            this.pkeyB1.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyB1.Shape = Multimedia.UI.PianoKeyShape.LShapeBackwards;
            this.pkeyB1.Size = new System.Drawing.Size(12, 42);
            this.pkeyB1.TabIndex = 11;
            this.pkeyB1.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyC2
            // 
            this.pkeyC2.KeyOffColor = System.Drawing.Color.White;
            this.pkeyC2.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyC2.Location = new System.Drawing.Point(81, 2);
            this.pkeyC2.Name = "pkeyC2";
            this.pkeyC2.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyC2.Shape = Multimedia.UI.PianoKeyShape.LShape;
            this.pkeyC2.Size = new System.Drawing.Size(12, 42);
            this.pkeyC2.TabIndex = 12;
            this.pkeyC2.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyCSharp2
            // 
            this.pkeyCSharp2.BackColor = System.Drawing.Color.Black;
            this.pkeyCSharp2.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyCSharp2.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyCSharp2.Location = new System.Drawing.Point(89, 2);
            this.pkeyCSharp2.Name = "pkeyCSharp2";
            this.pkeyCSharp2.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyCSharp2.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyCSharp2.Size = new System.Drawing.Size(8, 28);
            this.pkeyCSharp2.TabIndex = 15;
            this.pkeyCSharp2.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyD2
            // 
            this.pkeyD2.KeyOffColor = System.Drawing.Color.White;
            this.pkeyD2.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyD2.Location = new System.Drawing.Point(92, 2);
            this.pkeyD2.Name = "pkeyD2";
            this.pkeyD2.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyD2.Shape = Multimedia.UI.PianoKeyShape.TShape;
            this.pkeyD2.Size = new System.Drawing.Size(12, 42);
            this.pkeyD2.TabIndex = 13;
            this.pkeyD2.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyDSharp2
            // 
            this.pkeyDSharp2.BackColor = System.Drawing.Color.Black;
            this.pkeyDSharp2.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyDSharp2.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyDSharp2.Location = new System.Drawing.Point(100, 2);
            this.pkeyDSharp2.Name = "pkeyDSharp2";
            this.pkeyDSharp2.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyDSharp2.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyDSharp2.Size = new System.Drawing.Size(8, 29);
            this.pkeyDSharp2.TabIndex = 16;
            this.pkeyDSharp2.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyE2
            // 
            this.pkeyE2.KeyOffColor = System.Drawing.Color.White;
            this.pkeyE2.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyE2.Location = new System.Drawing.Point(103, 2);
            this.pkeyE2.Name = "pkeyE2";
            this.pkeyE2.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyE2.Shape = Multimedia.UI.PianoKeyShape.LShapeBackwards;
            this.pkeyE2.Size = new System.Drawing.Size(12, 42);
            this.pkeyE2.TabIndex = 14;
            this.pkeyE2.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyF2
            // 
            this.pkeyF2.KeyOffColor = System.Drawing.Color.White;
            this.pkeyF2.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyF2.Location = new System.Drawing.Point(114, 2);
            this.pkeyF2.Name = "pkeyF2";
            this.pkeyF2.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyF2.Shape = Multimedia.UI.PianoKeyShape.LShape;
            this.pkeyF2.Size = new System.Drawing.Size(12, 42);
            this.pkeyF2.TabIndex = 17;
            this.pkeyF2.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyFSharp2
            // 
            this.pkeyFSharp2.BackColor = System.Drawing.Color.Black;
            this.pkeyFSharp2.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyFSharp2.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyFSharp2.Location = new System.Drawing.Point(122, 2);
            this.pkeyFSharp2.Name = "pkeyFSharp2";
            this.pkeyFSharp2.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyFSharp2.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyFSharp2.Size = new System.Drawing.Size(8, 28);
            this.pkeyFSharp2.TabIndex = 19;
            this.pkeyFSharp2.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyG2
            // 
            this.pkeyG2.KeyOffColor = System.Drawing.Color.White;
            this.pkeyG2.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyG2.Location = new System.Drawing.Point(125, 2);
            this.pkeyG2.Name = "pkeyG2";
            this.pkeyG2.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyG2.Shape = Multimedia.UI.PianoKeyShape.TShape;
            this.pkeyG2.Size = new System.Drawing.Size(12, 42);
            this.pkeyG2.TabIndex = 18;
            this.pkeyG2.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyGSharp2
            // 
            this.pkeyGSharp2.BackColor = System.Drawing.Color.Black;
            this.pkeyGSharp2.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyGSharp2.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyGSharp2.Location = new System.Drawing.Point(133, 2);
            this.pkeyGSharp2.Name = "pkeyGSharp2";
            this.pkeyGSharp2.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyGSharp2.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyGSharp2.Size = new System.Drawing.Size(8, 29);
            this.pkeyGSharp2.TabIndex = 20;
            this.pkeyGSharp2.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyA2
            // 
            this.pkeyA2.KeyOffColor = System.Drawing.Color.White;
            this.pkeyA2.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyA2.Location = new System.Drawing.Point(136, 2);
            this.pkeyA2.Name = "pkeyA2";
            this.pkeyA2.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyA2.Shape = Multimedia.UI.PianoKeyShape.TShape;
            this.pkeyA2.Size = new System.Drawing.Size(12, 42);
            this.pkeyA2.TabIndex = 21;
            this.pkeyA2.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyASharp2
            // 
            this.pkeyASharp2.BackColor = System.Drawing.Color.Black;
            this.pkeyASharp2.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyASharp2.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyASharp2.Location = new System.Drawing.Point(144, 2);
            this.pkeyASharp2.Name = "pkeyASharp2";
            this.pkeyASharp2.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyASharp2.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyASharp2.Size = new System.Drawing.Size(8, 29);
            this.pkeyASharp2.TabIndex = 22;
            this.pkeyASharp2.Text = "pianoKey13";
            this.pkeyASharp2.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyB2
            // 
            this.pkeyB2.KeyOffColor = System.Drawing.Color.White;
            this.pkeyB2.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyB2.Location = new System.Drawing.Point(147, 2);
            this.pkeyB2.Name = "pkeyB2";
            this.pkeyB2.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyB2.Shape = Multimedia.UI.PianoKeyShape.LShapeBackwards;
            this.pkeyB2.Size = new System.Drawing.Size(12, 42);
            this.pkeyB2.TabIndex = 23;
            this.pkeyB2.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyC3
            // 
            this.pkeyC3.KeyOffColor = System.Drawing.Color.White;
            this.pkeyC3.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyC3.Location = new System.Drawing.Point(158, 2);
            this.pkeyC3.Name = "pkeyC3";
            this.pkeyC3.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyC3.Shape = Multimedia.UI.PianoKeyShape.LShape;
            this.pkeyC3.Size = new System.Drawing.Size(12, 42);
            this.pkeyC3.TabIndex = 24;
            this.pkeyC3.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyCSharp3
            // 
            this.pkeyCSharp3.BackColor = System.Drawing.Color.Black;
            this.pkeyCSharp3.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyCSharp3.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyCSharp3.Location = new System.Drawing.Point(166, 2);
            this.pkeyCSharp3.Name = "pkeyCSharp3";
            this.pkeyCSharp3.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyCSharp3.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyCSharp3.Size = new System.Drawing.Size(8, 28);
            this.pkeyCSharp3.TabIndex = 27;
            this.pkeyCSharp3.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyD3
            // 
            this.pkeyD3.KeyOffColor = System.Drawing.Color.White;
            this.pkeyD3.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyD3.Location = new System.Drawing.Point(169, 2);
            this.pkeyD3.Name = "pkeyD3";
            this.pkeyD3.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyD3.Shape = Multimedia.UI.PianoKeyShape.TShape;
            this.pkeyD3.Size = new System.Drawing.Size(12, 42);
            this.pkeyD3.TabIndex = 25;
            this.pkeyD3.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyDSharp3
            // 
            this.pkeyDSharp3.BackColor = System.Drawing.Color.Black;
            this.pkeyDSharp3.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyDSharp3.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyDSharp3.Location = new System.Drawing.Point(177, 2);
            this.pkeyDSharp3.Name = "pkeyDSharp3";
            this.pkeyDSharp3.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyDSharp3.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyDSharp3.Size = new System.Drawing.Size(8, 29);
            this.pkeyDSharp3.TabIndex = 28;
            this.pkeyDSharp3.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyE3
            // 
            this.pkeyE3.KeyOffColor = System.Drawing.Color.White;
            this.pkeyE3.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyE3.Location = new System.Drawing.Point(180, 2);
            this.pkeyE3.Name = "pkeyE3";
            this.pkeyE3.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyE3.Shape = Multimedia.UI.PianoKeyShape.LShapeBackwards;
            this.pkeyE3.Size = new System.Drawing.Size(12, 42);
            this.pkeyE3.TabIndex = 26;
            this.pkeyE3.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyF3
            // 
            this.pkeyF3.KeyOffColor = System.Drawing.Color.White;
            this.pkeyF3.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyF3.Location = new System.Drawing.Point(191, 2);
            this.pkeyF3.Name = "pkeyF3";
            this.pkeyF3.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyF3.Shape = Multimedia.UI.PianoKeyShape.LShape;
            this.pkeyF3.Size = new System.Drawing.Size(12, 42);
            this.pkeyF3.TabIndex = 29;
            this.pkeyF3.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyFSharp3
            // 
            this.pkeyFSharp3.BackColor = System.Drawing.Color.Black;
            this.pkeyFSharp3.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyFSharp3.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyFSharp3.Location = new System.Drawing.Point(199, 2);
            this.pkeyFSharp3.Name = "pkeyFSharp3";
            this.pkeyFSharp3.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyFSharp3.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyFSharp3.Size = new System.Drawing.Size(8, 28);
            this.pkeyFSharp3.TabIndex = 31;
            this.pkeyFSharp3.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyG3
            // 
            this.pkeyG3.KeyOffColor = System.Drawing.Color.White;
            this.pkeyG3.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyG3.Location = new System.Drawing.Point(202, 2);
            this.pkeyG3.Name = "pkeyG3";
            this.pkeyG3.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyG3.Shape = Multimedia.UI.PianoKeyShape.TShape;
            this.pkeyG3.Size = new System.Drawing.Size(12, 42);
            this.pkeyG3.TabIndex = 30;
            this.pkeyG3.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyGSharp3
            // 
            this.pkeyGSharp3.BackColor = System.Drawing.Color.Black;
            this.pkeyGSharp3.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyGSharp3.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyGSharp3.Location = new System.Drawing.Point(210, 2);
            this.pkeyGSharp3.Name = "pkeyGSharp3";
            this.pkeyGSharp3.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyGSharp3.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyGSharp3.Size = new System.Drawing.Size(8, 29);
            this.pkeyGSharp3.TabIndex = 32;
            this.pkeyGSharp3.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyA3
            // 
            this.pkeyA3.KeyOffColor = System.Drawing.Color.White;
            this.pkeyA3.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyA3.Location = new System.Drawing.Point(213, 2);
            this.pkeyA3.Name = "pkeyA3";
            this.pkeyA3.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyA3.Shape = Multimedia.UI.PianoKeyShape.TShape;
            this.pkeyA3.Size = new System.Drawing.Size(12, 42);
            this.pkeyA3.TabIndex = 33;
            this.pkeyA3.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyASharp3
            // 
            this.pkeyASharp3.BackColor = System.Drawing.Color.Black;
            this.pkeyASharp3.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyASharp3.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyASharp3.Location = new System.Drawing.Point(221, 2);
            this.pkeyASharp3.Name = "pkeyASharp3";
            this.pkeyASharp3.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyASharp3.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyASharp3.Size = new System.Drawing.Size(8, 29);
            this.pkeyASharp3.TabIndex = 34;
            this.pkeyASharp3.Text = "pianoKey13";
            this.pkeyASharp3.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyB3
            // 
            this.pkeyB3.KeyOffColor = System.Drawing.Color.White;
            this.pkeyB3.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyB3.Location = new System.Drawing.Point(224, 2);
            this.pkeyB3.Name = "pkeyB3";
            this.pkeyB3.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyB3.Shape = Multimedia.UI.PianoKeyShape.LShapeBackwards;
            this.pkeyB3.Size = new System.Drawing.Size(12, 42);
            this.pkeyB3.TabIndex = 35;
            this.pkeyB3.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyC4
            // 
            this.pkeyC4.KeyOffColor = System.Drawing.Color.White;
            this.pkeyC4.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyC4.Location = new System.Drawing.Point(235, 2);
            this.pkeyC4.Name = "pkeyC4";
            this.pkeyC4.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyC4.Shape = Multimedia.UI.PianoKeyShape.LShape;
            this.pkeyC4.Size = new System.Drawing.Size(12, 42);
            this.pkeyC4.TabIndex = 36;
            this.pkeyC4.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyCSharp4
            // 
            this.pkeyCSharp4.BackColor = System.Drawing.Color.Black;
            this.pkeyCSharp4.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyCSharp4.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyCSharp4.Location = new System.Drawing.Point(243, 2);
            this.pkeyCSharp4.Name = "pkeyCSharp4";
            this.pkeyCSharp4.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyCSharp4.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyCSharp4.Size = new System.Drawing.Size(8, 28);
            this.pkeyCSharp4.TabIndex = 39;
            this.pkeyCSharp4.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyD4
            // 
            this.pkeyD4.KeyOffColor = System.Drawing.Color.White;
            this.pkeyD4.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyD4.Location = new System.Drawing.Point(246, 2);
            this.pkeyD4.Name = "pkeyD4";
            this.pkeyD4.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyD4.Shape = Multimedia.UI.PianoKeyShape.TShape;
            this.pkeyD4.Size = new System.Drawing.Size(12, 42);
            this.pkeyD4.TabIndex = 37;
            this.pkeyD4.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyDSharp4
            // 
            this.pkeyDSharp4.BackColor = System.Drawing.Color.Black;
            this.pkeyDSharp4.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyDSharp4.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyDSharp4.Location = new System.Drawing.Point(254, 2);
            this.pkeyDSharp4.Name = "pkeyDSharp4";
            this.pkeyDSharp4.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyDSharp4.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyDSharp4.Size = new System.Drawing.Size(8, 29);
            this.pkeyDSharp4.TabIndex = 40;
            this.pkeyDSharp4.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyE4
            // 
            this.pkeyE4.KeyOffColor = System.Drawing.Color.White;
            this.pkeyE4.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyE4.Location = new System.Drawing.Point(257, 2);
            this.pkeyE4.Name = "pkeyE4";
            this.pkeyE4.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyE4.Shape = Multimedia.UI.PianoKeyShape.LShapeBackwards;
            this.pkeyE4.Size = new System.Drawing.Size(12, 42);
            this.pkeyE4.TabIndex = 38;
            this.pkeyE4.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyF4
            // 
            this.pkeyF4.KeyOffColor = System.Drawing.Color.White;
            this.pkeyF4.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyF4.Location = new System.Drawing.Point(268, 2);
            this.pkeyF4.Name = "pkeyF4";
            this.pkeyF4.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyF4.Shape = Multimedia.UI.PianoKeyShape.LShape;
            this.pkeyF4.Size = new System.Drawing.Size(12, 42);
            this.pkeyF4.TabIndex = 41;
            this.pkeyF4.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyFSharp4
            // 
            this.pkeyFSharp4.BackColor = System.Drawing.Color.Black;
            this.pkeyFSharp4.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyFSharp4.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyFSharp4.Location = new System.Drawing.Point(276, 2);
            this.pkeyFSharp4.Name = "pkeyFSharp4";
            this.pkeyFSharp4.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyFSharp4.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyFSharp4.Size = new System.Drawing.Size(8, 28);
            this.pkeyFSharp4.TabIndex = 43;
            this.pkeyFSharp4.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyG4
            // 
            this.pkeyG4.KeyOffColor = System.Drawing.Color.White;
            this.pkeyG4.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyG4.Location = new System.Drawing.Point(279, 2);
            this.pkeyG4.Name = "pkeyG4";
            this.pkeyG4.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyG4.Shape = Multimedia.UI.PianoKeyShape.TShape;
            this.pkeyG4.Size = new System.Drawing.Size(12, 42);
            this.pkeyG4.TabIndex = 42;
            this.pkeyG4.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyGSharp4
            // 
            this.pkeyGSharp4.BackColor = System.Drawing.Color.Black;
            this.pkeyGSharp4.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyGSharp4.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyGSharp4.Location = new System.Drawing.Point(287, 2);
            this.pkeyGSharp4.Name = "pkeyGSharp4";
            this.pkeyGSharp4.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyGSharp4.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyGSharp4.Size = new System.Drawing.Size(8, 29);
            this.pkeyGSharp4.TabIndex = 44;
            this.pkeyGSharp4.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyA4
            // 
            this.pkeyA4.KeyOffColor = System.Drawing.Color.White;
            this.pkeyA4.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyA4.Location = new System.Drawing.Point(290, 2);
            this.pkeyA4.Name = "pkeyA4";
            this.pkeyA4.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyA4.Shape = Multimedia.UI.PianoKeyShape.TShape;
            this.pkeyA4.Size = new System.Drawing.Size(12, 42);
            this.pkeyA4.TabIndex = 45;
            this.pkeyA4.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyASharp4
            // 
            this.pkeyASharp4.BackColor = System.Drawing.Color.Black;
            this.pkeyASharp4.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyASharp4.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyASharp4.Location = new System.Drawing.Point(298, 2);
            this.pkeyASharp4.Name = "pkeyASharp4";
            this.pkeyASharp4.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyASharp4.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyASharp4.Size = new System.Drawing.Size(8, 29);
            this.pkeyASharp4.TabIndex = 46;
            this.pkeyASharp4.Text = "pianoKey13";
            this.pkeyASharp4.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyB4
            // 
            this.pkeyB4.KeyOffColor = System.Drawing.Color.White;
            this.pkeyB4.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyB4.Location = new System.Drawing.Point(301, 2);
            this.pkeyB4.Name = "pkeyB4";
            this.pkeyB4.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyB4.Shape = Multimedia.UI.PianoKeyShape.LShapeBackwards;
            this.pkeyB4.Size = new System.Drawing.Size(12, 42);
            this.pkeyB4.TabIndex = 47;
            this.pkeyB4.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyC5
            // 
            this.pkeyC5.KeyOffColor = System.Drawing.Color.White;
            this.pkeyC5.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyC5.Location = new System.Drawing.Point(312, 2);
            this.pkeyC5.Name = "pkeyC5";
            this.pkeyC5.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyC5.Shape = Multimedia.UI.PianoKeyShape.LShape;
            this.pkeyC5.Size = new System.Drawing.Size(12, 42);
            this.pkeyC5.TabIndex = 48;
            this.pkeyC5.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyCSharp5
            // 
            this.pkeyCSharp5.BackColor = System.Drawing.Color.Black;
            this.pkeyCSharp5.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyCSharp5.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyCSharp5.Location = new System.Drawing.Point(320, 2);
            this.pkeyCSharp5.Name = "pkeyCSharp5";
            this.pkeyCSharp5.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyCSharp5.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyCSharp5.Size = new System.Drawing.Size(8, 28);
            this.pkeyCSharp5.TabIndex = 51;
            this.pkeyCSharp5.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyD5
            // 
            this.pkeyD5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pkeyD5.KeyOffColor = System.Drawing.Color.White;
            this.pkeyD5.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyD5.Location = new System.Drawing.Point(323, 2);
            this.pkeyD5.Name = "pkeyD5";
            this.pkeyD5.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyD5.Shape = Multimedia.UI.PianoKeyShape.TShape;
            this.pkeyD5.Size = new System.Drawing.Size(12, 42);
            this.pkeyD5.TabIndex = 49;
            this.pkeyD5.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyDSharp5
            // 
            this.pkeyDSharp5.BackColor = System.Drawing.Color.Black;
            this.pkeyDSharp5.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyDSharp5.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyDSharp5.Location = new System.Drawing.Point(331, 2);
            this.pkeyDSharp5.Name = "pkeyDSharp5";
            this.pkeyDSharp5.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyDSharp5.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyDSharp5.Size = new System.Drawing.Size(8, 29);
            this.pkeyDSharp5.TabIndex = 52;
            this.pkeyDSharp5.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyE5
            // 
            this.pkeyE5.KeyOffColor = System.Drawing.Color.White;
            this.pkeyE5.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyE5.Location = new System.Drawing.Point(334, 2);
            this.pkeyE5.Name = "pkeyE5";
            this.pkeyE5.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyE5.Shape = Multimedia.UI.PianoKeyShape.LShapeBackwards;
            this.pkeyE5.Size = new System.Drawing.Size(12, 42);
            this.pkeyE5.TabIndex = 50;
            this.pkeyE5.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyF5
            // 
            this.pkeyF5.KeyOffColor = System.Drawing.Color.White;
            this.pkeyF5.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyF5.Location = new System.Drawing.Point(345, 2);
            this.pkeyF5.Name = "pkeyF5";
            this.pkeyF5.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyF5.Shape = Multimedia.UI.PianoKeyShape.LShape;
            this.pkeyF5.Size = new System.Drawing.Size(12, 42);
            this.pkeyF5.TabIndex = 53;
            this.pkeyF5.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyFSharp5
            // 
            this.pkeyFSharp5.BackColor = System.Drawing.Color.Black;
            this.pkeyFSharp5.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyFSharp5.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyFSharp5.Location = new System.Drawing.Point(353, 2);
            this.pkeyFSharp5.Name = "pkeyFSharp5";
            this.pkeyFSharp5.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyFSharp5.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyFSharp5.Size = new System.Drawing.Size(8, 28);
            this.pkeyFSharp5.TabIndex = 55;
            this.pkeyFSharp5.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyG5
            // 
            this.pkeyG5.KeyOffColor = System.Drawing.Color.White;
            this.pkeyG5.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyG5.Location = new System.Drawing.Point(356, 2);
            this.pkeyG5.Name = "pkeyG5";
            this.pkeyG5.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyG5.Shape = Multimedia.UI.PianoKeyShape.TShape;
            this.pkeyG5.Size = new System.Drawing.Size(12, 42);
            this.pkeyG5.TabIndex = 54;
            this.pkeyG5.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyGSharp5
            // 
            this.pkeyGSharp5.BackColor = System.Drawing.Color.Black;
            this.pkeyGSharp5.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyGSharp5.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyGSharp5.Location = new System.Drawing.Point(364, 2);
            this.pkeyGSharp5.Name = "pkeyGSharp5";
            this.pkeyGSharp5.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyGSharp5.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyGSharp5.Size = new System.Drawing.Size(8, 29);
            this.pkeyGSharp5.TabIndex = 56;
            this.pkeyGSharp5.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyA5
            // 
            this.pkeyA5.KeyOffColor = System.Drawing.Color.White;
            this.pkeyA5.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyA5.Location = new System.Drawing.Point(367, 2);
            this.pkeyA5.Name = "pkeyA5";
            this.pkeyA5.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyA5.Shape = Multimedia.UI.PianoKeyShape.TShape;
            this.pkeyA5.Size = new System.Drawing.Size(12, 42);
            this.pkeyA5.TabIndex = 57;
            this.pkeyA5.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyASharp5
            // 
            this.pkeyASharp5.BackColor = System.Drawing.Color.Black;
            this.pkeyASharp5.KeyOffColor = System.Drawing.Color.Black;
            this.pkeyASharp5.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyASharp5.Location = new System.Drawing.Point(375, 2);
            this.pkeyASharp5.Name = "pkeyASharp5";
            this.pkeyASharp5.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyASharp5.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyASharp5.Size = new System.Drawing.Size(8, 29);
            this.pkeyASharp5.TabIndex = 58;
            this.pkeyASharp5.Text = "pianoKey13";
            this.pkeyASharp5.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyB5
            // 
            this.pkeyB5.KeyOffColor = System.Drawing.Color.White;
            this.pkeyB5.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyB5.Location = new System.Drawing.Point(378, 2);
            this.pkeyB5.Name = "pkeyB5";
            this.pkeyB5.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyB5.Shape = Multimedia.UI.PianoKeyShape.LShapeBackwards;
            this.pkeyB5.Size = new System.Drawing.Size(12, 42);
            this.pkeyB5.TabIndex = 59;
            this.pkeyB5.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // pkeyC8
            // 
            this.pkeyC8.KeyOffColor = System.Drawing.Color.White;
            this.pkeyC8.KeyOnColor = System.Drawing.Color.Blue;
            this.pkeyC8.Location = new System.Drawing.Point(543, 2);
            this.pkeyC8.Name = "pkeyC8";
            this.pkeyC8.Orientation = Multimedia.UI.PianoKeyOrientation.Vertical;
            this.pkeyC8.Shape = Multimedia.UI.PianoKeyShape.RectShape;
            this.pkeyC8.Size = new System.Drawing.Size(12, 42);
            this.pkeyC8.TabIndex = 60;
            this.pkeyC8.StateChanged += new System.EventHandler(this.PianoChanged);
            // 
            // bankEditorWars
            // 
            this.bankEditorWars.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bankEditorWars.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.bankEditorWars.Controls.Add(this.tableLayoutPanel16);
            this.bankEditorWars.Controls.Add(this.label31);
            this.bankEditorWars.Controls.Add(this.tableLayoutPanel17);
            this.bankEditorWars.Controls.Add(this.label33);
            this.bankEditorWars.Controls.Add(this.tableLayoutPanel18);
            this.bankEditorWars.Controls.Add(this.label34);
            this.bankEditorWars.Controls.Add(this.tableLayoutPanel19);
            this.bankEditorWars.Controls.Add(this.label35);
            this.bankEditorWars.Location = new System.Drawing.Point(315, 13);
            this.bankEditorWars.Name = "bankEditorWars";
            this.bankEditorWars.Size = new System.Drawing.Size(325, 253);
            this.bankEditorWars.TabIndex = 13;
            this.bankEditorWars.Visible = false;
            // 
            // tableLayoutPanel16
            // 
            this.tableLayoutPanel16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel16.ColumnCount = 2;
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel16.Controls.Add(this.war3ComboBox, 0, 0);
            this.tableLayoutPanel16.Controls.Add(this.war3Box, 1, 0);
            this.tableLayoutPanel16.Location = new System.Drawing.Point(14, 193);
            this.tableLayoutPanel16.Name = "tableLayoutPanel16";
            this.tableLayoutPanel16.RowCount = 1;
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel16.Size = new System.Drawing.Size(298, 31);
            this.tableLayoutPanel16.TabIndex = 13;
            // 
            // war3ComboBox
            // 
            this.war3ComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.war3ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.war3ComboBox.FormattingEnabled = true;
            this.war3ComboBox.Location = new System.Drawing.Point(3, 3);
            this.war3ComboBox.Name = "war3ComboBox";
            this.war3ComboBox.Size = new System.Drawing.Size(247, 21);
            this.war3ComboBox.TabIndex = 6;
            this.toolTip.SetToolTip(this.war3ComboBox, "Wave archive to be used for the bank.");
            // 
            // war3Box
            // 
            this.war3Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.war3Box.Location = new System.Drawing.Point(256, 3);
            this.war3Box.Maximum = new decimal(new int[] {
            65534,
            0,
            0,
            0});
            this.war3Box.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.war3Box.Name = "war3Box";
            this.war3Box.Size = new System.Drawing.Size(39, 20);
            this.war3Box.TabIndex = 7;
            this.toolTip.SetToolTip(this.war3Box, "Id of the wave archive to use for this bank.");
            // 
            // label31
            // 
            this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label31.Location = new System.Drawing.Point(11, 171);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(301, 22);
            this.label31.TabIndex = 12;
            this.label31.Text = "Wave Archive 3:";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel17
            // 
            this.tableLayoutPanel17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel17.ColumnCount = 2;
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel17.Controls.Add(this.war2ComboBox, 0, 0);
            this.tableLayoutPanel17.Controls.Add(this.war2Box, 1, 0);
            this.tableLayoutPanel17.Location = new System.Drawing.Point(14, 137);
            this.tableLayoutPanel17.Name = "tableLayoutPanel17";
            this.tableLayoutPanel17.RowCount = 1;
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel17.Size = new System.Drawing.Size(298, 31);
            this.tableLayoutPanel17.TabIndex = 11;
            // 
            // war2ComboBox
            // 
            this.war2ComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.war2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.war2ComboBox.FormattingEnabled = true;
            this.war2ComboBox.Location = new System.Drawing.Point(3, 3);
            this.war2ComboBox.Name = "war2ComboBox";
            this.war2ComboBox.Size = new System.Drawing.Size(247, 21);
            this.war2ComboBox.TabIndex = 6;
            this.toolTip.SetToolTip(this.war2ComboBox, "Wave archive to be used for the bank.");
            // 
            // war2Box
            // 
            this.war2Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.war2Box.Location = new System.Drawing.Point(256, 3);
            this.war2Box.Maximum = new decimal(new int[] {
            65534,
            0,
            0,
            0});
            this.war2Box.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.war2Box.Name = "war2Box";
            this.war2Box.Size = new System.Drawing.Size(39, 20);
            this.war2Box.TabIndex = 7;
            this.toolTip.SetToolTip(this.war2Box, "Id of the wave archive to use for this bank.");
            // 
            // label33
            // 
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label33.Location = new System.Drawing.Point(11, 115);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(301, 22);
            this.label33.TabIndex = 10;
            this.label33.Text = "Wave Archive 2:";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel18
            // 
            this.tableLayoutPanel18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel18.ColumnCount = 2;
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel18.Controls.Add(this.war1ComboBox, 0, 0);
            this.tableLayoutPanel18.Controls.Add(this.war1Box, 1, 0);
            this.tableLayoutPanel18.Location = new System.Drawing.Point(14, 81);
            this.tableLayoutPanel18.Name = "tableLayoutPanel18";
            this.tableLayoutPanel18.RowCount = 1;
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel18.Size = new System.Drawing.Size(298, 31);
            this.tableLayoutPanel18.TabIndex = 9;
            // 
            // war1ComboBox
            // 
            this.war1ComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.war1ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.war1ComboBox.FormattingEnabled = true;
            this.war1ComboBox.Location = new System.Drawing.Point(3, 3);
            this.war1ComboBox.Name = "war1ComboBox";
            this.war1ComboBox.Size = new System.Drawing.Size(247, 21);
            this.war1ComboBox.TabIndex = 6;
            this.toolTip.SetToolTip(this.war1ComboBox, "Wave archive to be used for the bank.");
            // 
            // war1Box
            // 
            this.war1Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.war1Box.Location = new System.Drawing.Point(256, 3);
            this.war1Box.Maximum = new decimal(new int[] {
            65534,
            0,
            0,
            0});
            this.war1Box.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.war1Box.Name = "war1Box";
            this.war1Box.Size = new System.Drawing.Size(39, 20);
            this.war1Box.TabIndex = 7;
            this.toolTip.SetToolTip(this.war1Box, "Id of the wave archive to use for this bank.");
            // 
            // label34
            // 
            this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label34.Location = new System.Drawing.Point(11, 59);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(301, 22);
            this.label34.TabIndex = 8;
            this.label34.Text = "Wave Archive 1:";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel19
            // 
            this.tableLayoutPanel19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel19.ColumnCount = 2;
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel19.Controls.Add(this.war0ComboBox, 0, 0);
            this.tableLayoutPanel19.Controls.Add(this.war0Box, 1, 0);
            this.tableLayoutPanel19.Location = new System.Drawing.Point(14, 25);
            this.tableLayoutPanel19.Name = "tableLayoutPanel19";
            this.tableLayoutPanel19.RowCount = 1;
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel19.Size = new System.Drawing.Size(298, 31);
            this.tableLayoutPanel19.TabIndex = 7;
            // 
            // war0ComboBox
            // 
            this.war0ComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.war0ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.war0ComboBox.FormattingEnabled = true;
            this.war0ComboBox.Location = new System.Drawing.Point(3, 3);
            this.war0ComboBox.Name = "war0ComboBox";
            this.war0ComboBox.Size = new System.Drawing.Size(247, 21);
            this.war0ComboBox.TabIndex = 6;
            this.toolTip.SetToolTip(this.war0ComboBox, "Wave archive to be used for the bank.");
            // 
            // war0Box
            // 
            this.war0Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.war0Box.Location = new System.Drawing.Point(256, 3);
            this.war0Box.Maximum = new decimal(new int[] {
            65534,
            0,
            0,
            0});
            this.war0Box.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.war0Box.Name = "war0Box";
            this.war0Box.Size = new System.Drawing.Size(39, 20);
            this.war0Box.TabIndex = 7;
            this.toolTip.SetToolTip(this.war0Box, "Id of the wave archive to use for this bank.");
            // 
            // label35
            // 
            this.label35.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label35.Location = new System.Drawing.Point(11, 3);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(301, 22);
            this.label35.TabIndex = 2;
            this.label35.Text = "Wave Archive 0:";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tree
            // 
            this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree.ImageIndex = 0;
            this.tree.ImageList = this.treeIcons;
            this.tree.Indent = 12;
            this.tree.Location = new System.Drawing.Point(0, 0);
            this.tree.Name = "tree";
            treeNode1.ImageIndex = 10;
            treeNode1.Name = "fileInfo";
            treeNode1.SelectedImageIndex = 10;
            treeNode1.Text = "File Information";
            this.tree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tree.SelectedImageIndex = 0;
            this.tree.ShowLines = false;
            this.tree.Size = new System.Drawing.Size(651, 538);
            this.tree.TabIndex = 0;
            this.tree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tree_NodeMouseClick);
            this.tree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tree_NodeMouseDoubleClick);
            this.tree.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tree_NodeKey);
            // 
            // treeIcons
            // 
            this.treeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeIcons.ImageStream")));
            this.treeIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.treeIcons.Images.SetKeyName(0, "blank.png");
            this.treeIcons.Images.SetKeyName(1, "version.png");
            this.treeIcons.Images.SetKeyName(2, "sseq.png");
            this.treeIcons.Images.SetKeyName(3, "seqArc.png");
            this.treeIcons.Images.SetKeyName(4, "bank.png");
            this.treeIcons.Images.SetKeyName(5, "waveArchive.png");
            this.treeIcons.Images.SetKeyName(6, "player.png");
            this.treeIcons.Images.SetKeyName(7, "group.png");
            this.treeIcons.Images.SetKeyName(8, "streamPlayer.png");
            this.treeIcons.Images.SetKeyName(9, "strm.png");
            this.treeIcons.Images.SetKeyName(10, "record.png");
            this.treeIcons.Images.SetKeyName(11, "recordArc.png");
            this.treeIcons.Images.SetKeyName(12, "lookup.png");
            this.treeIcons.Images.SetKeyName(13, "recordRegion.png");
            this.treeIcons.Images.SetKeyName(14, "wave.png");
            this.treeIcons.Images.SetKeyName(15, "ranged.png");
            this.treeIcons.Images.SetKeyName(16, "regional.png");
            this.treeIcons.Images.SetKeyName(17, "psg.png");
            this.treeIcons.Images.SetKeyName(18, "whiteNoise.png");
            // 
            // sequenceEditorPanel
            // 
            this.sequenceEditorPanel.Controls.Add(this.sequenceEditor);
            this.sequenceEditorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sequenceEditorPanel.Location = new System.Drawing.Point(0, 0);
            this.sequenceEditorPanel.Name = "sequenceEditorPanel";
            this.sequenceEditorPanel.Size = new System.Drawing.Size(651, 538);
            this.sequenceEditorPanel.TabIndex = 3;
            this.sequenceEditorPanel.Visible = false;
            // 
            // sequenceEditor
            // 
            this.sequenceEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sequenceEditor.Location = new System.Drawing.Point(0, 0);
            this.sequenceEditor.Name = "sequenceEditor";
            this.sequenceEditor.Size = new System.Drawing.Size(651, 538);
            this.sequenceEditor.TabIndex = 0;
            // 
            // openFileDialog
            // 
            this.openFileDialog.RestoreDirectory = true;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status,
            this.currentNote});
            this.statusStrip.Location = new System.Drawing.Point(0, 564);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(984, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(125, 17);
            this.status.Text = "No Valid Info Selected!";
            // 
            // currentNote
            // 
            this.currentNote.Name = "currentNote";
            this.currentNote.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.currentNote.Size = new System.Drawing.Size(0, 17);
            // 
            // rootMenu
            // 
            this.rootMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.expandToolStripMenuItem,
            this.collapseToolStripMenuItem});
            this.rootMenu.Name = "rootMenu";
            this.rootMenu.Size = new System.Drawing.Size(120, 70);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Image = global::NitroStudio2.Properties.Resources.New;
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // expandToolStripMenuItem
            // 
            this.expandToolStripMenuItem.Image = global::NitroStudio2.Properties.Resources.Save;
            this.expandToolStripMenuItem.Name = "expandToolStripMenuItem";
            this.expandToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.expandToolStripMenuItem.Text = "Expand";
            this.expandToolStripMenuItem.Click += new System.EventHandler(this.expandToolStripMenuItem_Click);
            // 
            // collapseToolStripMenuItem
            // 
            this.collapseToolStripMenuItem.Image = global::NitroStudio2.Properties.Resources.Save_As;
            this.collapseToolStripMenuItem.Name = "collapseToolStripMenuItem";
            this.collapseToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.collapseToolStripMenuItem.Text = "Collapse";
            this.collapseToolStripMenuItem.Click += new System.EventHandler(this.collapseToolStripMenuItem_Click);
            // 
            // nodeMenu
            // 
            this.nodeMenu.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.nodeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAboveToolStripMenuItem1,
            this.addBelowToolStripMenuItem1,
            this.moveUpToolStripMenuItem1,
            this.moveDownToolStripMenuItem1,
            this.replaceFileToolStripMenuItem,
            this.exportToolStripMenuItem1,
            this.deleteToolStripMenuItem1});
            this.nodeMenu.Name = "contextMenuStrip1";
            this.nodeMenu.Size = new System.Drawing.Size(139, 158);
            // 
            // addAboveToolStripMenuItem1
            // 
            this.addAboveToolStripMenuItem1.Image = global::NitroStudio2.Properties.Resources.New;
            this.addAboveToolStripMenuItem1.Name = "addAboveToolStripMenuItem1";
            this.addAboveToolStripMenuItem1.Size = new System.Drawing.Size(138, 22);
            this.addAboveToolStripMenuItem1.Text = "Add Above";
            this.addAboveToolStripMenuItem1.Click += new System.EventHandler(this.addAboveToolStripMenuItem1_Click);
            // 
            // addBelowToolStripMenuItem1
            // 
            this.addBelowToolStripMenuItem1.Image = global::NitroStudio2.Properties.Resources.Open;
            this.addBelowToolStripMenuItem1.Name = "addBelowToolStripMenuItem1";
            this.addBelowToolStripMenuItem1.Size = new System.Drawing.Size(138, 22);
            this.addBelowToolStripMenuItem1.Text = "Add Below";
            this.addBelowToolStripMenuItem1.Click += new System.EventHandler(this.addBelowToolStripMenuItem1_Click);
            // 
            // moveUpToolStripMenuItem1
            // 
            this.moveUpToolStripMenuItem1.Image = global::NitroStudio2.Properties.Resources.Save;
            this.moveUpToolStripMenuItem1.Name = "moveUpToolStripMenuItem1";
            this.moveUpToolStripMenuItem1.Size = new System.Drawing.Size(138, 22);
            this.moveUpToolStripMenuItem1.Text = "Move Up";
            this.moveUpToolStripMenuItem1.Click += new System.EventHandler(this.moveUpToolStripMenuItem1_Click);
            // 
            // moveDownToolStripMenuItem1
            // 
            this.moveDownToolStripMenuItem1.Image = global::NitroStudio2.Properties.Resources.Save_As;
            this.moveDownToolStripMenuItem1.Name = "moveDownToolStripMenuItem1";
            this.moveDownToolStripMenuItem1.Size = new System.Drawing.Size(138, 22);
            this.moveDownToolStripMenuItem1.Text = "Move Down";
            this.moveDownToolStripMenuItem1.Click += new System.EventHandler(this.moveDownToolStripMenuItem1_Click);
            // 
            // replaceFileToolStripMenuItem
            // 
            this.replaceFileToolStripMenuItem.Image = global::NitroStudio2.Properties.Resources.Import;
            this.replaceFileToolStripMenuItem.Name = "replaceFileToolStripMenuItem";
            this.replaceFileToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.replaceFileToolStripMenuItem.Text = "Replace";
            this.replaceFileToolStripMenuItem.Click += new System.EventHandler(this.replaceFileToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem1
            // 
            this.exportToolStripMenuItem1.Image = global::NitroStudio2.Properties.Resources.Export;
            this.exportToolStripMenuItem1.Name = "exportToolStripMenuItem1";
            this.exportToolStripMenuItem1.Size = new System.Drawing.Size(138, 22);
            this.exportToolStripMenuItem1.Text = "Export";
            this.exportToolStripMenuItem1.Click += new System.EventHandler(this.exportToolStripMenuItem1_Click);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Image = global::NitroStudio2.Properties.Resources.Close;
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(138, 22);
            this.deleteToolStripMenuItem1.Text = "Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click);
            // 
            // sarEntryMenu
            // 
            this.sarEntryMenu.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.sarEntryMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sarAddAbove,
            this.sarAddBelow,
            this.sarMoveUp,
            this.sarMoveDown,
            this.sarReplace,
            this.sarExport,
            this.sarRename,
            this.sarDelete});
            this.sarEntryMenu.Name = "contextMenuStrip1";
            this.sarEntryMenu.Size = new System.Drawing.Size(139, 180);
            // 
            // sarAddAbove
            // 
            this.sarAddAbove.Image = global::NitroStudio2.Properties.Resources.New;
            this.sarAddAbove.Name = "sarAddAbove";
            this.sarAddAbove.Size = new System.Drawing.Size(138, 22);
            this.sarAddAbove.Text = "Add Above";
            this.sarAddAbove.Click += new System.EventHandler(this.SarAddAbove_Click);
            // 
            // sarAddBelow
            // 
            this.sarAddBelow.Image = global::NitroStudio2.Properties.Resources.Open;
            this.sarAddBelow.Name = "sarAddBelow";
            this.sarAddBelow.Size = new System.Drawing.Size(138, 22);
            this.sarAddBelow.Text = "Add Below";
            this.sarAddBelow.Click += new System.EventHandler(this.SarAddBelow_Click);
            // 
            // sarMoveUp
            // 
            this.sarMoveUp.Image = global::NitroStudio2.Properties.Resources.Save;
            this.sarMoveUp.Name = "sarMoveUp";
            this.sarMoveUp.Size = new System.Drawing.Size(138, 22);
            this.sarMoveUp.Text = "Move Up";
            this.sarMoveUp.Click += new System.EventHandler(this.SarMoveUp_Click);
            // 
            // sarMoveDown
            // 
            this.sarMoveDown.Image = global::NitroStudio2.Properties.Resources.Save_As;
            this.sarMoveDown.Name = "sarMoveDown";
            this.sarMoveDown.Size = new System.Drawing.Size(138, 22);
            this.sarMoveDown.Text = "Move Down";
            this.sarMoveDown.Click += new System.EventHandler(this.SarMoveDown_Click);
            // 
            // sarReplace
            // 
            this.sarReplace.Image = global::NitroStudio2.Properties.Resources.Import;
            this.sarReplace.Name = "sarReplace";
            this.sarReplace.Size = new System.Drawing.Size(138, 22);
            this.sarReplace.Text = "Replace";
            this.sarReplace.Click += new System.EventHandler(this.SarReplace_Click);
            // 
            // sarExport
            // 
            this.sarExport.Image = global::NitroStudio2.Properties.Resources.Export;
            this.sarExport.Name = "sarExport";
            this.sarExport.Size = new System.Drawing.Size(138, 22);
            this.sarExport.Text = "Export";
            this.sarExport.Click += new System.EventHandler(this.SarExport_Click);
            // 
            // sarRename
            // 
            this.sarRename.Image = global::NitroStudio2.Properties.Resources.Rename;
            this.sarRename.Name = "sarRename";
            this.sarRename.Size = new System.Drawing.Size(138, 22);
            this.sarRename.Text = "Rename";
            this.sarRename.Click += new System.EventHandler(this.SarRename_Click);
            // 
            // sarDelete
            // 
            this.sarDelete.Image = global::NitroStudio2.Properties.Resources.Close;
            this.sarDelete.Name = "sarDelete";
            this.sarDelete.Size = new System.Drawing.Size(138, 22);
            this.sarDelete.Text = "Delete";
            this.sarDelete.Click += new System.EventHandler(this.SarDelete_Click);
            // 
            // EditorBase
            // 
            this.ClientSize = new System.Drawing.Size(984, 586);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.statusStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "EditorBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.form_Close);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.seqBankPanel.ResumeLayout(false);
            this.tableLayoutPanel36.ResumeLayout(false);
            this.tableLayoutPanel20.ResumeLayout(false);
            this.tableLayoutPanel35.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.track15Picture)).EndInit();
            this.tableLayoutPanel34.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.track13Picture)).EndInit();
            this.tableLayoutPanel33.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.track11Picture)).EndInit();
            this.tableLayoutPanel32.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.track9Picture)).EndInit();
            this.tableLayoutPanel31.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.track7Picture)).EndInit();
            this.tableLayoutPanel30.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.track5Picture)).EndInit();
            this.tableLayoutPanel29.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.track3Picture)).EndInit();
            this.tableLayoutPanel28.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.track14Picture)).EndInit();
            this.tableLayoutPanel27.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.track12Picture)).EndInit();
            this.tableLayoutPanel26.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.track10Picture)).EndInit();
            this.tableLayoutPanel25.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.track8Picture)).EndInit();
            this.tableLayoutPanel24.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.track6Picture)).EndInit();
            this.tableLayoutPanel23.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.track4Picture)).EndInit();
            this.tableLayoutPanel22.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.track2Picture)).EndInit();
            this.tableLayoutPanel21.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.track1Picture)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.track0Picture)).EndInit();
            this.tableLayoutPanel12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.seqEditorBankBox)).EndInit();
            this.bankEditorPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bankRegions)).EndInit();
            this.tableLayoutPanel15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.drumSetStartRangeBox)).EndInit();
            this.tableLayoutPanel14.ResumeLayout(false);
            this.seqArcSeqPanel.ResumeLayout(false);
            this.tableLayoutPanel13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.seqArcSeqBox)).EndInit();
            this.seqArcPanel.ResumeLayout(false);
            this.seqPanel.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.seqPlayerBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seqPlayerPriorityBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seqChannelPriorityBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seqVolumeBox)).EndInit();
            this.tableLayoutPanel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.seqBankBox)).EndInit();
            this.playerPanel.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playerHeapSizeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerMaxSequencesBox)).EndInit();
            this.stmPanel.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stmPlayerBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stmPriorityBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stmVolumeBox)).EndInit();
            this.streamPlayerPanel.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stmPlayerLeftChannelBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stmPlayerRightChannelBox)).EndInit();
            this.grpPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpEntries)).EndInit();
            this.bankPanel.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bnkWar3Box)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bnkWar2Box)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bnkWar1Box)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bnkWar0Box)).EndInit();
            this.warPanel.ResumeLayout(false);
            this.forceUniqueFilePanel.ResumeLayout(false);
            this.indexPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.itemIndexBox)).EndInit();
            this.settingsPanel.ResumeLayout(false);
            this.noInfoPanel.ResumeLayout(false);
            this.kermalisSoundPlayerPanel.ResumeLayout(false);
            this.kermalisSoundPlayerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kermalisPosition)).EndInit();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kermalisVolumeSlider)).EndInit();
            this.pnlPianoKeys.ResumeLayout(false);
            this.bankEditorWars.ResumeLayout(false);
            this.tableLayoutPanel16.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.war3Box)).EndInit();
            this.tableLayoutPanel17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.war2Box)).EndInit();
            this.tableLayoutPanel18.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.war1Box)).EndInit();
            this.tableLayoutPanel19.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.war0Box)).EndInit();
            this.sequenceEditorPanel.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.rootMenu.ResumeLayout(false);
            this.nodeMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.sarEntryMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        /// <summary>
        /// Get the path for opening a file.
        /// </summary>
        /// <param name="description">File description.</param>
        /// <param name="extension">File extension.</param>
        /// <returns>Path of the file to open.</returns>
        public string GetFileOpenerPath(string description, string extension) {

            //Set filer.
            openFileDialog.FileName = "";
            openFileDialog.Filter = description + "|" + "*.s" + extension.ToLower();
            openFileDialog.ShowDialog();

            //Return the file name.
            return openFileDialog.FileName;

        }

        /// <summary>
        /// Get the path for saving a file.
        /// </summary>
        /// <param name="description">File description.</param>
        /// <param name="extension">File extension.</param>
        /// <returns>Path of the file to save.</returns>
        public string GetFileSaverPath(string description, string extension) {

            //Set filer.
            saveFileDialog.FileName = "";
            saveFileDialog.Filter = description + "|" + "*.s" + extension.ToLower();
            saveFileDialog.ShowDialog();            

            //Set write mode.
            if (saveFileDialog.FileName != "") {

                //Fix extension.
                if (Path.GetExtension(saveFileDialog.FileName) == "") {
                    saveFileDialog.FileName += ".s" + extension.ToLower();
                }

            }

            //Return the file name.
            return saveFileDialog.FileName;

        }

        /// <summary>
        /// On closing hook.
        /// </summary>
        private void form_Close(object sender, FormClosingEventArgs e) {
            OnClosing();
        }

        /// <summary>
        /// On closing.
        /// </summary>
        public virtual void OnClosing() {}

        public ContextMenuStrip CreateMenuStrip(ContextMenuStrip orig, int[] indices, EventHandler[] eventHandlers) {

            ContextMenuStrip c = new ContextMenuStrip();

            int num = 0;
            foreach (int ind in indices) {

                var i = orig.Items[ind];
                c.Items.Add(i.Text, i.Image, eventHandlers[num++]);

            }

            return c;

        }

        //Updating.
        #region Updating

        /// <summary>
        /// Do the info stuff on node selected.
        /// </summary>
        public virtual void DoInfoStuff() {

            //Fix selected node issue.
            if (tree.SelectedNode == null) {
                tree.SelectedNode = tree.Nodes[0];
            }

            //Not file open.
            if (!FileOpen) {

                //Show no info panel.
                noInfoPanel.BringToFront();
                noInfoPanel.Show();

                //Update status.
                status.Text = "No Valid Info Selected!";

            }

        }

        Stack<int> nodeIndices;
        List<string> expandedNodes;

        /// <summary>
        /// Begin the updating of nodes.
        /// </summary>
        public void BeginUpdateNodes() {

            //Begin update.
            tree.BeginUpdate();

            //Get list of expanded nodes.
            expandedNodes = collectExpandedNodes(tree.Nodes);

            //Safety.
            if (tree.SelectedNode == null) {
                tree.SelectedNode = tree.Nodes[0];
            }

            //Get the selected index.
            nodeIndices = new Stack<int>();
            nodeIndices.Push(tree.SelectedNode.Index);
            while (tree.SelectedNode.Parent != null) {
                tree.SelectedNode = tree.SelectedNode.Parent;
                nodeIndices.Push(tree.SelectedNode.Index);
            }

            //Clear each node.
            for (int i = 0; i < tree.Nodes.Count; i++) {
                tree.Nodes[i].Nodes.Clear();
            }

        }

        /// <summary>
        /// Update the nodes in the tree. THIS MUST CONTAIN THE BEGIN AND END UPDATE NODES!
        /// </summary>
        public abstract void UpdateNodes();

        /// <summary>
        /// Complete the updating of nodes.
        /// </summary>
        public void EndUpdateNodes() {

            //Restore the expanded nodes if they exist.
            if (expandedNodes.Count > 0) {
                TreeNode IamExpandedNode;
                for (int i = 0; i < expandedNodes.Count; i++) {
                    IamExpandedNode = FindNodeByName(tree.Nodes, expandedNodes[i]);
                    expandNodePath(IamExpandedNode);
                }

            }

            //Set the selected node.
            tree.SelectedNode = tree.Nodes[nodeIndices.Pop()];
            while (nodeIndices.Count > 0) {
                try {
                    tree.SelectedNode = tree.SelectedNode.Nodes[nodeIndices.Pop()];
                } catch {
                    nodeIndices.Clear();
                }
            }
            tree.SelectedNode.EnsureVisible();

            //End update.
            tree.EndUpdate();

        }

        #endregion

        //File menu.
        #region fileMenu

        //New.
        public virtual void newToolStripMenuItem_Click(object sender, EventArgs e) {

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

            //Update.
            UpdateNodes();
            DoInfoStuff();

        }

        //Open.
        public virtual void openToolStripMenuItem_Click(object sender, EventArgs e) {

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
                DoInfoStuff();

            }

        }

        //Save.
        public virtual void saveToolStripMenuItem_Click(object sender, EventArgs e) {

            //File open test.
            if (!FileTest(sender, e, false, true)) {
                return;
            }

            //No where to save.
            if (ExtFile == null && FilePath == "") {

                //Save as.
                saveAsToolStripMenuItem_Click(sender, e);

                //Return.
                return;

            }

            //External file is not null.
            if (ExtFile != null) {

                //Set the file.
                ExtFile.Read(File.Write());

                //Update the main window.
                if (MainWindow != null) {
                    MainWindow.UpdateNodes();
                    MainWindow.DoInfoStuff();
                }
                if (OtherEditor != null) {
                    OtherEditor.UpdateNodes();
                    OtherEditor.DoInfoStuff();
                }

            }

            //External file is null.
            else {

                //Write the file.
                File.Write(FilePath);

            }

        }

        //Save as.
        public void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {

            //File open test.
            if (!FileTest(sender, e, false, true)) {
                return;
            }

            //Get the save file name.
            string path = GetFileSaverPath(ExtensionDescription, Extension);

            //If the path is valid.
            if (path != "") {

                //Set values.
                FilePath = path;
                ExtFile = null;
                Text = EditorName + " - " + Path.GetFileName(path);

                //Save the file.
                saveToolStripMenuItem_Click(sender, e);

            }

        }

        //Close.
        public virtual void closeToolStripMenuItem_Click(object sender, EventArgs e) {

            //File open and can save test.
            if (!FileTest(sender, e, true, true)) {
                return;
            }

            //Close the file.
            File = null;
            ExtFile = null;
            FilePath = "";
            FileOpen = false;
            Text = EditorName;

            //Update.
            UpdateNodes();
            DoInfoStuff();

        }

        //Quit.
        private void quitToolStripMenuItem_Click(object sender, EventArgs e) {

            //Quit if wanted.
            if (FileOpen) {
                SaveQuitDialog q = new SaveQuitDialog(this);
                q.ShowDialog();
            } else {
                Close();
            }

        }

        #endregion

        //Edit menu.
        #region editMenu

        //Blank the file.
        public virtual void blankFileToolStripMenuItem_Click(object sender, EventArgs e) {

            //File open save test.
            if (!FileTest(sender, e, false, true)) {
                return;
            }

            //Create instance of file.
            File = (IOFile)Activator.CreateInstance(FileType);

            //Update.
            UpdateNodes();
            DoInfoStuff();

        }

        //Import data from another file.
        public virtual void importFileToolStripMenuItem_Click(object sender, EventArgs e) {

            //File open test.
            if (!FileTest(sender, e, false, true)) {
                return;
            }

            //Open the file.
            string path = GetFileOpenerPath(ExtensionDescription, Extension);

            //File is not null.
            if (path != "") {

                //Set value.
                File = (IOFile)Activator.CreateInstance(FileType);

                //Read data.
                File.Read(path);

                //Update.
                UpdateNodes();
                DoInfoStuff();

            }

        }

        //Export data to somewhere.
        public virtual void exportFileToolStripMenuItem_Click(object sender, EventArgs e) {

            //File open.
            if (!FileTest(sender, e, false, true)) {
                return;
            }

            //Get the save file name.
            string path = GetFileSaverPath(ExtensionDescription, Extension);

            //If the path is valid.
            if (path != "") {

                //Write the file.
                File.Write(path);

            }

        }

        //Set file data to null.
        private void nullifyFileToolStripMenuItem_Click(object sender, EventArgs e) {

            //File open test.
            if (!FileTest(sender, e, false, true)) {
                return;
            }

            //External file cannot be null.
            if (ExtFile == null) {

                //Call out the user.
                MessageBox.Show("You can't nullify data that is not in a parent file!", "Notice:");
                return;

            }

            //Nullify file.
            File = null;

            //Update.
            UpdateNodes();
            DoInfoStuff();

        }

        /// <summary>
        /// Returns true if the user wants to continue.
        /// </summary>
        public bool FileTest(object sender, EventArgs e, bool save, bool forceOpen = false) {

            //File is open.
            if (FileOpen) {

                //Ask user if they want to save.
                if (save) {

                    SaveCloseDialog c = new SaveCloseDialog();
                    switch (c.getValue()) {

                        //Save.
                        case 0:
                            saveToolStripMenuItem_Click(sender, e);
                            return true;

                        //No button.
                        case 1:
                            return true;

                        //Cancel.
                        default:
                            return false;

                    }

                }

                //Passed test.
                return true;

            } else {

                if (forceOpen) {
                    MessageBox.Show("There must be a file open to do this!", "Notice:");
                    return false;
                } else {
                    return true;
                }

            }

        }

        #endregion


        //Node shit.
        #region nodeShit

        //Expand node and parents.
        void expandNodePath(TreeNode node) {
            if (node == null)
                return;
            if (node.Level != 0) //check if it is not root
            {
                node.Expand();
                expandNodePath(node.Parent);
            } else {
                node.Expand(); // this is root 
            }



        }

        //Make right click actually select, and show infoViewer.
        void tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                // Select the clicked node
                tree.SelectedNode = tree.GetNodeAt(e.X, e.Y);
            } else if (e.Button == MouseButtons.Left) {
                // Select the clicked node
                tree.SelectedNode = tree.GetNodeAt(e.X, e.Y);
            }

            DoInfoStuff();

        }

        void tree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {

            //Select.
            if (e.Button == MouseButtons.Right) {
                // Select the clicked node
                tree.SelectedNode = tree.GetNodeAt(e.X, e.Y);
            } else if (e.Button == MouseButtons.Left) {
                // Select the clicked node
                tree.SelectedNode = tree.GetNodeAt(e.X, e.Y);
            }

            //Do info stuff.
            DoInfoStuff();

            //Do double click action.
            NodeMouseDoubleClick();

        }

        public virtual void NodeMouseDoubleClick() {}

        void tree_NodeKey(object sender, KeyEventArgs e) {

            DoInfoStuff();

        }

        //Get expanded nodes.
        List<string> collectExpandedNodes(TreeNodeCollection Nodes) {
            List<string> _lst = new List<string>();
            foreach (TreeNode checknode in Nodes) {
                if (checknode.IsExpanded)
                    _lst.Add(checknode.Name);
                if (checknode.Nodes.Count > 0)
                    _lst.AddRange(collectExpandedNodes(checknode.Nodes));
            }
            return _lst;
        }


        /// <summary>
        /// Find nodes by name.
        /// </summary>
        /// <param name="NodesCollection"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        TreeNode FindNodeByName(TreeNodeCollection NodesCollection, string Name) {
            TreeNode returnNode = null; // Default value to return
            foreach (TreeNode checkNode in NodesCollection) {
                if (checkNode.Name == Name)  //checks if this node name is correct
                    returnNode = checkNode;
                else if (checkNode.Nodes.Count > 0) //node has child
                {
                    returnNode = FindNodeByName(checkNode.Nodes, Name);
                }

                if (returnNode != null) //check if founded do not continue and break
                {
                    return returnNode;
                }

            }
            //not found
            return returnNode;
        }

        #endregion


        //Sound player deluxe.
        #region soundPlayerDeluxe

        //Play.
        private void playSoundTrack_Click(object sender, EventArgs e) {
            Play();
        }

        //Pause.
        private void pauseSoundTrack_Click(object sender, EventArgs e) {
            Pause();
        }

        //Stop.
        private void stopSoundTrack_Click(object sender, EventArgs e) {
            Stop();
        }

        public virtual void Play() {}
        public virtual void Pause() {}
        public virtual void Stop() {}

        #endregion


        //Other buttons.
        #region otherButtons

        private void forceWaveVersionButton_Click(object sender, EventArgs e) {
            ForceWaveVersionButtonClick();
        }

        public virtual void ForceWaveVersionButtonClick() {}

        #endregion


        //Root menu.
        #region rootMenu

        private void addToolStripMenuItem_Click(object sender, EventArgs e) {
            RootAdd();
        }

        public void expandToolStripMenuItem_Click(object sender, EventArgs e) {
            tree.SelectedNode.Expand();
        }

        public void collapseToolStripMenuItem_Click(object sender, EventArgs e) {
            tree.SelectedNode.Collapse();
        }

        public virtual void RootAdd() {}

        #endregion


        //Node menu.
        #region nodeMenu

        public void addAboveToolStripMenuItem1_Click(object sender, EventArgs e) {
            NodeAddAbove();
        }

        public void addBelowToolStripMenuItem1_Click(object sender, EventArgs e) {
            NodeAddBelow();
        }

        public void moveUpToolStripMenuItem1_Click(object sender, EventArgs e) {
            NodeMoveUp();
        }

        public void moveDownToolStripMenuItem1_Click(object sender, EventArgs e) {
            NodeMoveDown();
        }

        public void blankToolStripMenuItem_Click(object sender, EventArgs e) {
            NodeBlank();
        }

        public void replaceFileToolStripMenuItem_Click(object sender, EventArgs e) {
            NodeReplace();
        }

        public void exportToolStripMenuItem1_Click(object sender, EventArgs e) {
            NodeExport();
        }

        public void nullifyToolStripMenuItem1_Click(object sender, EventArgs e) {
            NodeNullify();
        }

        public void deleteToolStripMenuItem1_Click(object sender, EventArgs e) {
            NodeDelete();
        }

        public virtual void NodeAddAbove() {}
        public virtual void NodeAddBelow() {}
        public virtual void NodeMoveUp() {}
        public virtual void NodeMoveDown() {}
        public virtual void NodeBlank() {}
        public virtual void NodeReplace() {}
        public virtual void NodeExport() {}
        public virtual void NodeNullify() {}
        public virtual void NodeDelete() {}

        /// <summary>
        /// Swap the a and b objects.
        /// </summary>
        /// <param name="objects">Objects list.</param>
        /// <param name="a">Object a to swap.</param>
        /// <param name="b">Object b to swap.</param>
        public bool Swap<T>(IList<T> objects, int a, int b) {

            //Make sure it is possible.
            if (a < 0 || a >= objects.Count || b < 0 || b >= objects.Count) {
                return false;
            }

            //Swap objects.
            T temp = objects[a];
            objects[a] = objects[b];
            objects[b] = temp;
            return true;

        }

        #endregion


        //War boxes.
        #region warBoxes

        private void vMajBoxWar_ValueChanged(object sender, EventArgs e) {
            BoxWarMajChanged();
        }

        private void vMinBoxWar_ValueChanged(object sender, EventArgs e) {
            BoxWarMinChanged();
        }

        private void vRevBoxWar_ValueChanged(object sender, EventArgs e) {
            BoxWarRevChanged();
        }

        private void vWavMajBox_ValueChanged(object sender, EventArgs e) {
            BoxWavMajChanged();
        }

        private void vWavMinBox_ValueChanged(object sender, EventArgs e) {
            BoxWavMinChanged();
        }

        private void vWavRevBox_ValueChanged(object sender, EventArgs e) {
            BoxWavRevChanged();
        }

        public virtual void BoxWarMajChanged() {}
        public virtual void BoxWarMinChanged() {}
        public virtual void BoxWarRevChanged() {}
        public virtual void BoxWavMajChanged() {}
        public virtual void BoxWavMinChanged() {}
        public virtual void BoxWavRevChanged() {}

        #endregion


        //War tools.
        #region warTools

        private void batchExtractWavesToolStripMenuItem_Click(object sender, EventArgs e) {
            WarExtractWave();
        }

        private void batchExtract3dsWavesToolStripMenuItem_Click(object sender, EventArgs e) {
            WarExtractWave3ds();
        }

        private void batchExtractWiiUWavesToolStripMenuItem_Click(object sender, EventArgs e) {
            WarExtractWaveWiiU();
        }

        private void batchExtractSwitchWavesToolStripMenuItem_Click(object sender, EventArgs e) {
            WarExtractWaveSwitch();
        }

        private void batchImportToolStripMenuItem_Click(object sender, EventArgs e) {
            WarBatchImport();
        }

        public virtual void WarExtractWave() {}
        public virtual void WarExtractWave3ds() {}
        public virtual void WarExtractWaveWiiU() {}
        public virtual void WarExtractWaveSwitch() {}
        public virtual void WarBatchImport() {}

        #endregion


        //Group versions.
        #region grpVersions

        private void grpSeqForceButton_Click(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupForceSequenceVersion();
            }
        }

        private void grpBnkForceButton_Click(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupForceBankVersion();
            }
        }

        private void grpWarForceButton_Click(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupForceWaveArchiveVersion();
            }
        }

        private void grpWsdForceButton_Click(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupForceWaveSoundDataVersion();
            }
        }

        private void grpStpForceButton_Click(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupForcePrefetchVersion();
            }
        }

        private void grpMajBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupVersionChanged();
            }
        }

        private void grpMinBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupVersionChanged();
            }
        }

        private void grpRevBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupVersionChanged();
            }
        }

        private void grpSeqMajBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupVersionChanged();
            }
        }

        private void grpSeqMinBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupVersionChanged();
            }
        }

        private void grpSeqRevBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupVersionChanged();
            }
        }

        private void grpBnkMajBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupVersionChanged();
            }
        }

        private void grpBnkMinBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupVersionChanged();
            }
        }

        private void grpBnkRevBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupVersionChanged();
            }
        }

        private void grpWarMajBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupVersionChanged();
            }
        }

        private void grpWarMinBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupVersionChanged();
            }
        }

        private void grpWarRevBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupVersionChanged();
            }
        }

        private void grpWsdMajBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupVersionChanged();
            }
        }

        private void grpWsdMinBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupVersionChanged();
            }
        }

        private void grpWsdRevBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupVersionChanged();
            }
        }

        private void grpStpMajBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupVersionChanged();
            }
        }

        private void grpStpMinBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupVersionChanged();
            }
        }

        private void grpStpRevBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupVersionChanged();
            }
        }

        public virtual void GroupForceSequenceVersion() {}
        public virtual void GroupForceBankVersion() {}
        public virtual void GroupForceWaveArchiveVersion() {}
        public virtual void GroupForceWaveSoundDataVersion() {}
        public virtual void GroupForcePrefetchVersion() {}
        public virtual void GroupVersionChanged() {}

        #endregion


        //Group file data.
        #region grpFile

        private void grpFileIdComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupFileIdComboChanged();
            }
        }

        private void grpFileIdBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupFileIdNumBoxChanged();
            }
        }

        private void grpEmbedModeBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupFileIdEmbedModeChanged();
            }
        }

        public virtual void GroupFileIdComboChanged() {}
        public virtual void GroupFileIdNumBoxChanged() {}
        public virtual void GroupFileIdEmbedModeChanged() {}

        #endregion


        //Group dependency.
        #region grpDependency

        private void grpDepEntryTypeBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupDependencyTypeChanged();
            }
        }

        private void grpDepEntryNumComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupDependencyEntryComboChanged();
            }
        }

        private void grpDepEntryNumBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupDependencyEntryNumBoxChanged();
            }
        }

        private void grpDepLoadFlagsBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                GroupDependencyFlagsChanged();
            }
        }

        public virtual void GroupDependencyTypeChanged() {}
        public virtual void GroupDependencyEntryComboChanged() {}
        public virtual void GroupDependencyEntryNumBoxChanged() {}
        public virtual void GroupDependencyFlagsChanged() {}

        #endregion

        //SAR Project info.
        #region SARProjectInfo

        private void MaxSeqNumBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                SarProjectInfoUpdated();
            }
        }

        private void MaxSeqTrackNumBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                SarProjectInfoUpdated();
            }
        }

        private void MaxStreamNumBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                SarProjectInfoUpdated();
            }
        }

        private void MaxStreamNumTracksBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                SarProjectInfoUpdated();
            }
        }

        private void MaxStreamNumChannelsBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                SarProjectInfoUpdated();
            }
        }

        private void MaxWaveNumBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                SarProjectInfoUpdated();
            }
        }

        private void MaxWaveNumTracksBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                SarProjectInfoUpdated();
            }
        }

        private void StreamBufferTimesBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                SarProjectInfoUpdated();
            }
        }

        private void OptionsPIBox_ValueChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                SarProjectInfoUpdated();
            }
        }

        private void SarIncludeStringBlock_CheckedChanged(object sender, EventArgs e) {
            if (!WritingInfo) {
                SarProjectInfoUpdated();
            }
        }

        public virtual void SarProjectInfoUpdated() { }

        #endregion

        public virtual void StmSound3dButton_Click(object sender, EventArgs e) {}

        public virtual void WsdSound3dButton_Click(object sender, EventArgs e) {}

        public virtual void SeqEditSound3dInfoButton_Click(object sender, EventArgs e) {}

        public virtual void SeqEditSoundInfoButton_Click(object sender, EventArgs e) {}

        public virtual void WsdEditSoundInfoButton_Click(object sender, EventArgs e) {}

        public virtual void StmSoundInfoButton_Click(object sender, EventArgs e) {}

        public virtual void FileTypeBox_SelectedIndexChanged(object sender, EventArgs e) {}

        public virtual void PlayerSoundLimitBox_ValueChanged(object sender, EventArgs e) {}

        public virtual void PlayerEnableSoundLimitBox_CheckedChanged(object sender, EventArgs e) {}

        public virtual void PlayerHeapSizeBox_ValueChanged(object sender, EventArgs e) {}

        public virtual void SarGrpFileIdBox_SelectedIndexChanged(object sender, EventArgs e) {}

        public virtual void SarWarFileIdBox_SelectedIndexChanged(object sender, EventArgs e) {}

        public virtual void WarLoadIndividuallyBox_CheckedChanged(object sender, EventArgs e) {}

        public virtual void WarIncludeWaveCountBox_CheckedChanged(object sender, EventArgs e) {}

        public virtual void SarBnkFileIdBox_SelectedIndexChanged(object sender, EventArgs e) {}

        public virtual void BnkWarsChanged(object sender, EventArgs e) {}

        public virtual void SoundGrpStartIndex_ValueChanged(object sender, EventArgs e) {}

        public virtual void SoundGrpEndIndex_ValueChanged(object sender, EventArgs e) {}

        public virtual void SoundGroupFilesChanged(object sender, EventArgs e) {}

        public virtual void SoundGroupWarsChanged(object sender, EventArgs e) {}

        public virtual void SarSeqFileIdBox_SelectedIndexChanged(object sender, EventArgs e) {}

        public virtual void SarSeqPlay_Click(object sender, EventArgs e) {}

        public virtual void SarSeqPause_Click(object sender, EventArgs e) {}

        public virtual void SarSeqStop_Click(object sender, EventArgs e) {}

        public virtual void SeqSound3dInfoExists_CheckedChanged(object sender, EventArgs e) {}

        public virtual void SeqBank0Box_SelectedIndexChanged(object sender, EventArgs e) {}

        public virtual void SeqBank1Box_SelectedIndexChanged(object sender, EventArgs e) {}

        public virtual void SeqBank2Box_SelectedIndexChanged(object sender, EventArgs e) {}

        public virtual void SeqBank3Box_SelectedIndexChanged(object sender, EventArgs e) {}

        public virtual void SeqOffsetFromLabelButton_CheckedChanged(object sender, EventArgs e) {}

        public virtual void SeqOffsetManualButton_CheckedChanged(object sender, EventArgs e) {}

        public virtual void SeqOffsetFromLabelBox_SelectedIndexChanged(object sender, EventArgs e) {}

        public virtual void SeqOffsetManualBox_ValueChanged(object sender, EventArgs e) {}

        public virtual void SeqChannelPriorityBox_ValueChanged(object sender, EventArgs e) {}

        public virtual void SeqIsReleasePriorityBox_CheckedChanged(object sender, EventArgs e) {}

        public virtual void SeqC0_CheckedChanged(object sender, EventArgs e) {}

        public virtual void SeqC1_CheckedChanged(object sender, EventArgs e) {}

        public virtual void SeqC2_CheckedChanged(object sender, EventArgs e) {}

        public virtual void SeqC3_CheckedChanged(object sender, EventArgs e) {}

        public virtual void SeqC4_CheckedChanged(object sender, EventArgs e) {}

        public virtual void SeqC5_CheckedChanged(object sender, EventArgs e) {}

        public virtual void SeqC6_CheckedChanged(object sender, EventArgs e) {}

        public virtual void SeqC7_CheckedChanged(object sender, EventArgs e) {}

        public virtual void SeqC8_CheckedChanged(object sender, EventArgs e) {}

        public virtual void SeqC9_CheckedChanged(object sender, EventArgs e) {}

        public virtual void SeqC10_CheckedChanged(object sender, EventArgs e) {}

        public virtual void SeqC11_CheckedChanged(object sender, EventArgs e) {}

        public virtual void SeqC12_CheckedChanged(object sender, EventArgs e) {}

        public virtual void SeqC13_CheckedChanged(object sender, EventArgs e) {}

        public virtual void SeqC14_CheckedChanged(object sender, EventArgs e) {}

        public virtual void SeqC15_CheckedChanged(object sender, EventArgs e) {}

        public virtual void SarWsdFileIdBox_SelectedIndexChanged(object sender, EventArgs e) {}

        public virtual void SarWsdPlay_Click(object sender, EventArgs e) {}

        public virtual void SarWsdPause_Click(object sender, EventArgs e) {}

        public virtual void SarWsdStop_Click(object sender, EventArgs e) {}

        public virtual void WsdSound3dEnable_CheckedChanged(object sender, EventArgs e) {}

        public virtual void WsdWaveIndex_ValueChanged(object sender, EventArgs e) {}

        public virtual void WsdTracksToAllocate_ValueChanged(object sender, EventArgs e) {}

        public virtual void WsdCopyCount_Click(object sender, EventArgs e) {}

        public virtual void WsdChannelPriority_ValueChanged(object sender, EventArgs e) {}

        public virtual void WsdFixPriority_CheckedChanged(object sender, EventArgs e) {}

        public virtual void StmFileIdBox_SelectedIndexChanged(object sender, EventArgs e) {}

        public virtual void StmPlay_Click(object sender, EventArgs e) {}

        public virtual void StmPause_Click(object sender, EventArgs e) {}

        public virtual void StmStop_Click(object sender, EventArgs e) {}

        public virtual void StmSound3dEnable_CheckedChanged(object sender, EventArgs e) {}

        public virtual void StmWriteTrackInfo_CheckedChanged(object sender, EventArgs e) {}

        public virtual void StmUpdateTrackInfo_Click(object sender, EventArgs e) {}

        public virtual void StmTrack0_CheckedChanged(object sender, EventArgs e) {}

        public virtual void StmTrack1_CheckedChanged(object sender, EventArgs e) {}

        public virtual void StmTrack2_CheckedChanged(object sender, EventArgs e) {}

        public virtual void StmTrack3_CheckedChanged(object sender, EventArgs e) {}

        public virtual void StmTrack4_CheckedChanged(object sender, EventArgs e) {}

        public virtual void StmTrack5_CheckedChanged(object sender, EventArgs e) {}

        public virtual void StmTrack6_CheckedChanged(object sender, EventArgs e) {}

        public virtual void StmTrack7_CheckedChanged(object sender, EventArgs e) {}

        public virtual void StmTrack8_CheckedChanged(object sender, EventArgs e) {}

        public virtual void StmTrack9_CheckedChanged(object sender, EventArgs e) {}

        public virtual void StmTrack10_CheckedChanged(object sender, EventArgs e) {}

        public virtual void StmTrack11_CheckedChanged(object sender, EventArgs e) {}

        public virtual void StmTrack12_CheckedChanged(object sender, EventArgs e) {}

        public virtual void StmTrack13_CheckedChanged(object sender, EventArgs e) {}

        public virtual void StmTrack14_CheckedChanged(object sender, EventArgs e) {}

        public virtual void StmTrack15_CheckedChanged(object sender, EventArgs e) {}

        public virtual void StmStreamType_SelectedIndexChanged(object sender, EventArgs e) {}

        public virtual void StmAllocateChannelsNum_ValueChanged(object sender, EventArgs e) {}

        public virtual void StmCopyChannelCountFromFile_Click(object sender, EventArgs e) {}

        public virtual void StmPitch_ValueChanged(object sender, EventArgs e) {}

        public virtual void StmIncludeExtension_CheckedChanged(object sender, EventArgs e) {}

        public virtual void StmLoopStartFrame_ValueChanged(object sender, EventArgs e) {}

        public virtual void StmLoopEndFrame_ValueChanged(object sender, EventArgs e) {}

        public virtual void StmCopyExtensionFromFile_Click(object sender, EventArgs e) {}

        public virtual void StmGeneratePrefetch_CheckedChanged(object sender, EventArgs e) {}

        public virtual void StmPrefetchFileIdBox_SelectedIndexChanged(object sender, EventArgs e) {}

        public virtual void StmUpdatePrefetchInfo_Click(object sender, EventArgs e) {}

        public virtual void StmCreateUniquePrefetchFile_Click(object sender, EventArgs e) {}

        public virtual void StmSendMain_ValueChanged(object sender, EventArgs e) {}

        public virtual void StmSendA_ValueChanged(object sender, EventArgs e) {}

        public virtual void StmSendB_ValueChanged(object sender, EventArgs e) {}

        public virtual void StmSendC_ValueChanged(object sender, EventArgs e) {}

        public virtual void TrackVolume_ValueChanged(object sender, EventArgs e) {}

        public virtual void TrackPan_ValueChanged(object sender, EventArgs e) {}

        public virtual void TrackSpan_ValueChanged(object sender, EventArgs e) {}

        public virtual void TrackSurround_CheckedChanged(object sender, EventArgs e) {}

        public virtual void TrackLPFFrequency_ValueChanged(object sender, EventArgs e) {}

        public virtual void TrackBiquadType_SelectedIndexChanged(object sender, EventArgs e) {}

        public virtual void TrackBiquadValue_ValueChanged(object sender, EventArgs e) {}

        public virtual void TrackSendMain_ValueChanged(object sender, EventArgs e) {}

        public virtual void TrackSendA_ValueChanged(object sender, EventArgs e) {}

        public virtual void TrackSendB_ValueChanged(object sender, EventArgs e) {}

        public virtual void TrackSendC_ValueChanged(object sender, EventArgs e) {}

        public virtual void TrackChannelsChanged(object sender, EventArgs e) {}

        public virtual void ByteOrderBox_SelectedIndexChanged(object sender, EventArgs e) {}

        public virtual void VersionMax_ValueChanged(object sender, EventArgs e) {}

        public virtual void VersionMin_ValueChanged(object sender, EventArgs e) {}

        public virtual void VersionRev_ValueChanged(object sender, EventArgs e) {}

        public virtual void SeqVersionUpdate_Click(object sender, EventArgs e) {}

        public virtual void BankVersionUpdate_Click(object sender, EventArgs e) {}

        public virtual void WarVersionUpdate_Click(object sender, EventArgs e) {}

        public virtual void WsdVersionUpdate_Click(object sender, EventArgs e) {}

        public virtual void GrpVersionUpdate_Click(object sender, EventArgs e) {}

        public virtual void StmVersionUpdate_Click(object sender, EventArgs e) {}

        public virtual void StpVersionUpdate_Click(object sender, EventArgs e) {}

        public virtual void FilesIncludeGroups_CheckedChanged(object sender, EventArgs e) {}

        public virtual void FilesGroupGridCellChanged(object sender, EventArgs e) {}

        public virtual void ReplaceToolStripMenuItem_Click(object sender, EventArgs e) {}

        public virtual void ExportToolStripMenuItem_Click(object sender, EventArgs e) {}

        public virtual void ChangeExternalPathToolStripMenuItem_Click(object sender, EventArgs e) {}

        public virtual void SarAddAbove_Click(object sender, EventArgs e) {}

        public virtual void SarAddBelow_Click(object sender, EventArgs e) {}

        public virtual void SarAddInside_Click(object sender, EventArgs e) {}

        public virtual void SarMoveUp_Click(object sender, EventArgs e) {}

        public virtual void SarMoveDown_Click(object sender, EventArgs e) {}

        public virtual void SarReplace_Click(object sender, EventArgs e) {}

        public virtual void SarExport_Click(object sender, EventArgs e) {}

        public virtual void SarRename_Click(object sender, EventArgs e) {}

        public virtual void SarNullify_Click(object sender, EventArgs e) {}

        public virtual void SarDelete_Click(object sender, EventArgs e) {}

        //Wsd editor.
        #region WsdEditor

        public virtual void WsdTrackPlay_Click(object sender, EventArgs e) {}

        public virtual void WsdTrackPause_Click(object sender, EventArgs e) {}

        public virtual void WsdTrackStop_Click(object sender, EventArgs e) {}

        public virtual void WsdTrackPlayOnce_CheckedChanged(object sender, EventArgs e) {}

        public virtual void WsdTrackPlayLoop_CheckedChanged(object sender, EventArgs e) {}

        public virtual void WsdPlayNext_CheckedChanged(object sender, EventArgs e) {}

        public virtual void WsdEventGrid_CellChange(object sende, EventArgs e) {}

        public virtual void WsdEntryPlay_Click(object sender, EventArgs e) {}

        public virtual void WsdEntryPause_Click(object sender, EventArgs e) {}

        public virtual void WsdEntryStop_Click(object sender, EventArgs e) {}

        public virtual void WsdEntryPlayOnce_CheckedChanged(object sender, EventArgs e) {}

        public virtual void WsdEntryPlayLoop_CheckedChanged(object sender, EventArgs e) {}

        public virtual void WsdEntryPlayNext_CheckedChanged(object sender, EventArgs e) {}

        public virtual void WsdAttack_ValueChanged(object sender, EventArgs e) {}

        public virtual void WsdDecay_ValueChanged(object sender, EventArgs e) {}

        public virtual void WsdSustain_ValueChanged(object sender, EventArgs e) {}

        public virtual void WsdRelease_ValueChanged(object sender, EventArgs e) {}

        public virtual void WsdHold_ValueChanged(object sender, EventArgs e) {}

        public virtual void WsdLPF_ValueChanged(object sender, EventArgs e) {}

        public virtual void WsdBiquadType_SelectedIndexChanged(object sender, EventArgs e) {}

        public virtual void WsdBiquadValue_ValueChanged(object sender, EventArgs e) {}

        public virtual void WsdSendMain_ValueChanged(object sender, EventArgs e) {}

        public virtual void WsdSendA_ValueChanged(object sender, EventArgs e) {}

        public virtual void WsdSendB_ValueChanged(object sender, EventArgs e) {}

        public virtual void WsdSendC_ValueChanged(object sender, EventArgs e) {}

        public virtual void WsdPan_ValueChanged(object sender, EventArgs e) {}

        public virtual void WsdSpan_ValueChanged(object sender, EventArgs e) {}

        public virtual void WsdPitch_ValueChanged(object sender, EventArgs e) {}

        public virtual void WsdReference_CellChanged(object sender, EventArgs e) {}

        public virtual void WsdReferencePlay_Click(object sender, EventArgs e) {}

        public virtual void WsdReferencePause_Click(object sender, EventArgs e) {}

        public virtual void WsdReferenceStop_Click(object sender, EventArgs e) {}

        public virtual void WsdReferencePlayOnce_CheckedChanged(object sender, EventArgs e) {}

        public virtual void WsdReferencePlayLoop_CheckedChanged(object sender, EventArgs e) {}

        public virtual void WsdReferencePlayNext_CheckedChanged(object sender, EventArgs e) {}

        public virtual void WsdRefArchiveCombo_SelectedIndexChanged(object sender, EventArgs e) {}

        public virtual void WsdRefArchiveBox_ValueChanged(object sender, EventArgs e) {}

        public virtual void WsdRefWaveCombo_SelectedIndexChanged(object sender, EventArgs e) {}

        public virtual void WsdRefWaveBox_ValueChanged(object sender, EventArgs e) {}

        #endregion

        //Version change.
        #region VersionChange

        public virtual void VMajBox_ValueChanged(object sender, EventArgs e) {}

        public virtual void VMinBox_ValueChanged(object sender, EventArgs e) {}

        public virtual void VRevBox_ValueChanged(object sender, EventArgs e) {}

        #endregion

        //Note info.
        #region NoteInfo

        public virtual void NoteReferenceWave_SelectedIndexChanged(object sender, EventArgs e) {}

        public virtual void NoteInterpolationType_SelectedIndexChanged(object sender, EventArgs e) {}

        public virtual void NotePercussionMode_CheckedChanged(object sender, EventArgs e) {}

        public virtual void NotePitchSemitones_ValueChanged(object sender, EventArgs e) {}

        public virtual void NotePitchCents_ValueChanged(object sender, EventArgs e) {}

        public virtual void NoteVolume_ValueChanged(object sender, EventArgs e) {}

        public virtual void NotePan_ValueChanged(object sender, EventArgs e) {}

        public virtual void NoteSurroundPan_ValueChanged(object sender, EventArgs e) {}

        public virtual void NoteOriginalKey_ValueChanged(object sender, EventArgs e) {}

        public virtual void NoteKeyGroup_SelectedIndexChanged(object sender, EventArgs e) {}

        public virtual void NoteAttack_ValueChanged(object sender, EventArgs e) {}

        public virtual void NoteDecay_ValueChanged(object sender, EventArgs e) {}

        public virtual void NoteSustain_ValueChanged(object sender, EventArgs e) {}

        public virtual void NoteRelease_ValueChanged(object sender, EventArgs e) {}

        public virtual void NoteHold_ValueChanged(object sender, EventArgs e) {}

        public virtual void InstrumentApplyChanges_Click(object sender, EventArgs e) {}

        #endregion

        private void PianoChanged(object sender, EventArgs e) {
            if (pkeyC1.IsKeyOn()) {
                NoteDown = Notes.cn1;
                OnPianoPress();
            } else if (pkeyC2.IsKeyOn()) {
                NoteDown = Notes.cn2;
                OnPianoPress();
            } else if (pkeyC3.IsKeyOn()) {
                NoteDown = Notes.cn3;
                OnPianoPress();
            } else if (pkeyC4.IsKeyOn()) {
                NoteDown = Notes.cn4;
                OnPianoPress();
            } else if (pkeyC5.IsKeyOn()) {
                NoteDown = Notes.cn5;
                OnPianoPress();
            } else if (pkeyC6.IsKeyOn()) {
                NoteDown = Notes.cn6;
                OnPianoPress();
            } else if (pkeyC7.IsKeyOn()) {
                NoteDown = Notes.cn7;
                OnPianoPress();
            } else if (pkeyC8.IsKeyOn()) {
                NoteDown = Notes.cn8;
                OnPianoPress();
            } else if (pkeyD1.IsKeyOn()) {
                NoteDown = Notes.dn1;
                OnPianoPress();
            } else if (pkeyD2.IsKeyOn()) {
                NoteDown = Notes.dn2;
                OnPianoPress();
            } else if (pkeyD3.IsKeyOn()) {
                NoteDown = Notes.dn3;
                OnPianoPress();
            } else if (pkeyD4.IsKeyOn()) {
                NoteDown = Notes.dn4;
                OnPianoPress();
            } else if (pkeyD5.IsKeyOn()) {
                NoteDown = Notes.dn5;
                OnPianoPress();
            } else if (pkeyD6.IsKeyOn()) {
                NoteDown = Notes.dn6;
                OnPianoPress();
            } else if (pkeyD7.IsKeyOn()) {
                NoteDown = Notes.dn7;
                OnPianoPress();
            } else if (pkeyE1.IsKeyOn()) {
                NoteDown = Notes.en1;
                OnPianoPress();
            } else if (pkeyE2.IsKeyOn()) {
                NoteDown = Notes.en2;
                OnPianoPress();
            } else if (pkeyE3.IsKeyOn()) {
                NoteDown = Notes.en3;
                OnPianoPress();
            } else if (pkeyE4.IsKeyOn()) {
                NoteDown = Notes.en4;
                OnPianoPress();
            } else if (pkeyE5.IsKeyOn()) {
                NoteDown = Notes.en5;
                OnPianoPress();
            } else if (pkeyE6.IsKeyOn()) {
                NoteDown = Notes.en6;
                OnPianoPress();
            } else if (pkeyE7.IsKeyOn()) {
                NoteDown = Notes.en7;
                OnPianoPress();
            } else if (pkeyF1.IsKeyOn()) {
                NoteDown = Notes.fn1;
                OnPianoPress();
            } else if (pkeyF2.IsKeyOn()) {
                NoteDown = Notes.fn2;
                OnPianoPress();
            } else if (pkeyF3.IsKeyOn()) {
                NoteDown = Notes.fn3;
                OnPianoPress();
            } else if (pkeyF4.IsKeyOn()) {
                NoteDown = Notes.fn4;
                OnPianoPress();
            } else if (pkeyF5.IsKeyOn()) {
                NoteDown = Notes.fn5;
                OnPianoPress();
            } else if (pkeyF6.IsKeyOn()) {
                NoteDown = Notes.fn6;
                OnPianoPress();
            } else if (pkeyF7.IsKeyOn()) {
                NoteDown = Notes.fn7;
                OnPianoPress();
            } else if (pkeyG1.IsKeyOn()) {
                NoteDown = Notes.gn1;
                OnPianoPress();
            } else if (pkeyG2.IsKeyOn()) {
                NoteDown = Notes.gn2;
                OnPianoPress();
            } else if (pkeyG3.IsKeyOn()) {
                NoteDown = Notes.gn3;
                OnPianoPress();
            } else if (pkeyG4.IsKeyOn()) {
                NoteDown = Notes.gn4;
                OnPianoPress();
            } else if (pkeyG5.IsKeyOn()) {
                NoteDown = Notes.gn5;
                OnPianoPress();
            } else if (pkeyG6.IsKeyOn()) {
                NoteDown = Notes.gn6;
                OnPianoPress();
            } else if (pkeyG7.IsKeyOn()) {
                NoteDown = Notes.gn7;
                OnPianoPress();
            } else if (pkeyA1.IsKeyOn()) {
                NoteDown = Notes.an1;
                OnPianoPress();
            } else if (pkeyA2.IsKeyOn()) {
                NoteDown = Notes.an2;
                OnPianoPress();
            } else if (pkeyA3.IsKeyOn()) {
                NoteDown = Notes.an3;
                OnPianoPress();
            } else if (pkeyA4.IsKeyOn()) {
                NoteDown = Notes.an4;
                OnPianoPress();
            } else if (pkeyA5.IsKeyOn()) {
                NoteDown = Notes.an5;
                OnPianoPress();
            } else if (pkeyA6.IsKeyOn()) {
                NoteDown = Notes.an6;
                OnPianoPress();
            } else if (pkeyA7.IsKeyOn()) {
                NoteDown = Notes.an7;
                OnPianoPress();
            } else if (pkeyB1.IsKeyOn()) {
                NoteDown = Notes.bn1;
                OnPianoPress();
            } else if (pkeyB2.IsKeyOn()) {
                NoteDown = Notes.bn2;
                OnPianoPress();
            } else if (pkeyB3.IsKeyOn()) {
                NoteDown = Notes.bn3;
                OnPianoPress();
            } else if (pkeyB4.IsKeyOn()) {
                NoteDown = Notes.bn4;
                OnPianoPress();
            } else if (pkeyB5.IsKeyOn()) {
                NoteDown = Notes.bn5;
                OnPianoPress();
            } else if (pkeyB6.IsKeyOn()) {
                NoteDown = Notes.bn6;
                OnPianoPress();
            } else if (pkeyB7.IsKeyOn()) {
                NoteDown = Notes.bn7;
                OnPianoPress();
            } else if (pkeyCSharp1.IsKeyOn()) {
                NoteDown = Notes.cs1;
                OnPianoPress();
            } else if (pkeyCSharp2.IsKeyOn()) {
                NoteDown = Notes.cs2;
                OnPianoPress();
            } else if (pkeyCSharp3.IsKeyOn()) {
                NoteDown = Notes.cs3;
                OnPianoPress();
            } else if (pkeyCSharp4.IsKeyOn()) {
                NoteDown = Notes.cs4;
                OnPianoPress();
            } else if (pkeyCSharp5.IsKeyOn()) {
                NoteDown = Notes.cs5;
                OnPianoPress();
            } else if (pkeyCSharp6.IsKeyOn()) {
                NoteDown = Notes.cs6;
                OnPianoPress();
            } else if (pkeyCSharp7.IsKeyOn()) {
                NoteDown = Notes.cs7;
                OnPianoPress();
            } else if (pkeyDSharp1.IsKeyOn()) {
                NoteDown = Notes.ds1;
                OnPianoPress();
            } else if (pkeyDSharp2.IsKeyOn()) {
                NoteDown = Notes.ds2;
                OnPianoPress();
            } else if (pkeyDSharp3.IsKeyOn()) {
                NoteDown = Notes.ds3;
                OnPianoPress();
            } else if (pkeyDSharp4.IsKeyOn()) {
                NoteDown = Notes.ds4;
                OnPianoPress();
            } else if (pkeyDSharp5.IsKeyOn()) {
                NoteDown = Notes.ds5;
                OnPianoPress();
            } else if (pkeyDSharp6.IsKeyOn()) {
                NoteDown = Notes.ds6;
                OnPianoPress();
            } else if (pkeyDSharp7.IsKeyOn()) {
                NoteDown = Notes.ds7;
                OnPianoPress();
            } else if (pkeyFSharp1.IsKeyOn()) {
                NoteDown = Notes.fs1;
                OnPianoPress();
            } else if (pkeyFSharp2.IsKeyOn()) {
                NoteDown = Notes.fs2;
                OnPianoPress();
            } else if (pkeyFSharp3.IsKeyOn()) {
                NoteDown = Notes.fs3;
                OnPianoPress();
            } else if (pkeyFSharp4.IsKeyOn()) {
                NoteDown = Notes.fs4;
                OnPianoPress();
            } else if (pkeyFSharp5.IsKeyOn()) {
                NoteDown = Notes.fs5;
                OnPianoPress();
            } else if (pkeyFSharp6.IsKeyOn()) {
                NoteDown = Notes.fs6;
                OnPianoPress();
            } else if (pkeyFSharp7.IsKeyOn()) {
                NoteDown = Notes.fs7;
                OnPianoPress();
            } else if (pkeyGSharp1.IsKeyOn()) {
                NoteDown = Notes.gs1;
                OnPianoPress();
            } else if (pkeyGSharp2.IsKeyOn()) {
                NoteDown = Notes.gs2;
                OnPianoPress();
            } else if (pkeyGSharp3.IsKeyOn()) {
                NoteDown = Notes.gs3;
                OnPianoPress();
            } else if (pkeyGSharp4.IsKeyOn()) {
                NoteDown = Notes.gs4;
                OnPianoPress();
            } else if (pkeyGSharp5.IsKeyOn()) {
                NoteDown = Notes.gs5;
                OnPianoPress();
            } else if (pkeyGSharp6.IsKeyOn()) {
                NoteDown = Notes.gs6;
                OnPianoPress();
            } else if (pkeyGSharp7.IsKeyOn()) {
                NoteDown = Notes.gs7;
                OnPianoPress();
            } else if (pkeyASharp1.IsKeyOn()) {
                NoteDown = Notes.as1;
                OnPianoPress();
            } else if (pkeyASharp2.IsKeyOn()) {
                NoteDown = Notes.as2;
                OnPianoPress();
            } else if (pkeyASharp3.IsKeyOn()) {
                NoteDown = Notes.as3;
                OnPianoPress();
            } else if (pkeyASharp4.IsKeyOn()) {
                NoteDown = Notes.as4;
                OnPianoPress();
            } else if (pkeyASharp5.IsKeyOn()) {
                NoteDown = Notes.as5;
                OnPianoPress();
            } else if (pkeyASharp6.IsKeyOn()) {
                NoteDown = Notes.as6;
                OnPianoPress();
            } else if (pkeyASharp7.IsKeyOn()) {
                NoteDown = Notes.as7;
                OnPianoPress();
            } else {
                OnPianoRelease();
            }
        }

        public virtual void OnPianoPress() {}

        public virtual void OnPianoRelease() {}

        private void ExportStringsToolStripMenuItem_Click(object sender, EventArgs e) {
            ExportStrings();
        }

        public virtual void ExportStrings() {}

        private void SequenceEditorToolStripMenuItem_Click(object sender, EventArgs e) {
            var ed = new SequenceEditor(this as MainWindow);
            ed.Show();
        }

        private void SequenceArchiveEditorToolStripMenuItem_Click(object sender, EventArgs e) {
            var ed = new SequenceArchiveEditor(this as MainWindow);
            ed.Show();
        }

        private void BankEditorToolStripMenuItem_Click(object sender, EventArgs e) {
            var ed = new BankEditor(this as MainWindow);
            ed.Show();
        }

        private void WaveArchiveEditorToolStripMenuItem_Click(object sender, EventArgs e) {
            var ed = new WaveArchiveEditor(this as MainWindow);
            ed.Show();
        }

        private void BankGeneratorToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!FileOpen || File == null) {
                MessageBox.Show("There must be a file open to do this!");
                return;
            }
            var ed = new BankGenerator(this as MainWindow);
            ed.Show();
        }

        private void CreaveWaveToolStripMenuItem_Click(object sender, EventArgs e) {
            CreateStreamTool ed = new CreateStreamTool(true);
            ed.Show();
        }

        private void CreateStreamToolStripMenuItem_Click(object sender, EventArgs e) {
            CreateStreamTool ed = new CreateStreamTool(false);
            ed.Show();
        }

        private void ExportSDKProjectToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "Sound Project|*.sprj";
            if (FilePath != null && FilePath != "") { s.FileName = Path.GetFileNameWithoutExtension(FilePath) + ".sprj"; }
            s.RestoreDirectory = true;
            if (s.ShowDialog() == DialogResult.OK) {
                (File as SoundArchive).ExportSDKProject(Path.GetDirectoryName(s.FileName), Path.GetFileNameWithoutExtension(s.FileName));
            }
        }

        private void GetHelpToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                System.Diagnostics.Process.Start("https://gota7.github.io/NitroStudio2/#guide");
            } catch { }
        }

        /// <summary>
        /// Get the color region.
        /// </summary>
        /// <param name="color">Color.</param>
        /// <param name="start">Start note.</param>
        /// <param name="end">End note.</param>
        public void ColorRegion(Color color, byte start, byte end) {
            for (byte b = start; b <= end; b++) {
                var n = GetKey((Notes)b);
                if (n != null) {
                    if (n.Shape == PianoKeyShape.RectShape && n != pkeyC8) {
                        n.KeyOffColor = Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
                    } else {
                        n.KeyOffColor = color;
                    }
                }
            }
        }

        /// <summary>
        /// Get the piano key.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public PianoKey GetKey(Notes n) {
            switch (n) {
                case Notes.an1:
                    return pkeyA1;
                case Notes.an2:
                    return pkeyA2;
                case Notes.an3:
                    return pkeyA3;
                case Notes.an4:
                    return pkeyA4;
                case Notes.an5:
                    return pkeyA5;
                case Notes.an6:
                    return pkeyA6;
                case Notes.an7:
                    return pkeyA7;
                case Notes.bn1:
                    return pkeyB1;
                case Notes.bn2:
                    return pkeyB2;
                case Notes.bn3:
                    return pkeyB3;
                case Notes.bn4:
                    return pkeyB4;
                case Notes.bn5:
                    return pkeyB5;
                case Notes.bn6:
                    return pkeyB6;
                case Notes.bn7:
                    return pkeyB7;
                case Notes.cn1:
                    return pkeyC1;
                case Notes.cn2:
                    return pkeyC2;
                case Notes.cn3:
                    return pkeyC3;
                case Notes.cn4:
                    return pkeyC4;
                case Notes.cn5:
                    return pkeyC5;
                case Notes.cn6:
                    return pkeyC6;
                case Notes.cn7:
                    return pkeyC7;
                case Notes.cn8:
                    return pkeyC8;
                case Notes.dn1:
                    return pkeyD1;
                case Notes.dn2:
                    return pkeyD2;
                case Notes.dn3:
                    return pkeyD3;
                case Notes.dn4:
                    return pkeyD4;
                case Notes.dn5:
                    return pkeyD5;
                case Notes.dn6:
                    return pkeyD6;
                case Notes.dn7:
                    return pkeyD7;
                case Notes.en1:
                    return pkeyE1;
                case Notes.en2:
                    return pkeyE2;
                case Notes.en3:
                    return pkeyE3;
                case Notes.en4:
                    return pkeyE4;
                case Notes.en5:
                    return pkeyE5;
                case Notes.en6:
                    return pkeyE6;
                case Notes.en7:
                    return pkeyE7;
                case Notes.fn1:
                    return pkeyF1;
                case Notes.fn2:
                    return pkeyF2;
                case Notes.fn3:
                    return pkeyF3;
                case Notes.fn4:
                    return pkeyF4;
                case Notes.fn5:
                    return pkeyF5;
                case Notes.fn6:
                    return pkeyF6;
                case Notes.fn7:
                    return pkeyF7;
                case Notes.gn1:
                    return pkeyG1;
                case Notes.gn2:
                    return pkeyG2;
                case Notes.gn3:
                    return pkeyG3;
                case Notes.gn4:
                    return pkeyG4;
                case Notes.gn5:
                    return pkeyG5;
                case Notes.gn6:
                    return pkeyG6;
                case Notes.gn7:
                    return pkeyG7;
                case Notes.as1:
                    return pkeyASharp1;
                case Notes.as2:
                    return pkeyASharp2;
                case Notes.as3:
                    return pkeyASharp3;
                case Notes.as4:
                    return pkeyASharp4;
                case Notes.as5:
                    return pkeyASharp5;
                case Notes.as6:
                    return pkeyASharp6;
                case Notes.as7:
                    return pkeyASharp7;
                case Notes.cs1:
                    return pkeyCSharp1;
                case Notes.cs2:
                    return pkeyCSharp2;
                case Notes.cs3:
                    return pkeyCSharp3;
                case Notes.cs4:
                    return pkeyCSharp4;
                case Notes.cs5:
                    return pkeyCSharp5;
                case Notes.cs6:
                    return pkeyCSharp6;
                case Notes.cs7:
                    return pkeyCSharp7;
                case Notes.ds1:
                    return pkeyDSharp1;
                case Notes.ds2:
                    return pkeyDSharp2;
                case Notes.ds3:
                    return pkeyDSharp3;
                case Notes.ds4:
                    return pkeyDSharp4;
                case Notes.ds5:
                    return pkeyDSharp5;
                case Notes.ds6:
                    return pkeyDSharp6;
                case Notes.ds7:
                    return pkeyDSharp7;
                case Notes.fs1:
                    return pkeyFSharp1;
                case Notes.fs2:
                    return pkeyFSharp2;
                case Notes.fs3:
                    return pkeyFSharp3;
                case Notes.fs4:
                    return pkeyFSharp4;
                case Notes.fs5:
                    return pkeyFSharp5;
                case Notes.fs6:
                    return pkeyFSharp6;
                case Notes.fs7:
                    return pkeyFSharp7;
                case Notes.gs1:
                    return pkeyGSharp1;
                case Notes.gs2:
                    return pkeyGSharp2;
                case Notes.gs3:
                    return pkeyGSharp3;
                case Notes.gs4:
                    return pkeyGSharp4;
                case Notes.gs5:
                    return pkeyGSharp5;
                case Notes.gs6:
                    return pkeyGSharp6;
                case Notes.gs7:
                    return pkeyGSharp7;
                default:
                    return null;
            }
        }

        private void AboutNitroStudio2ToolStripMenuItem_Click(object sender, EventArgs e) {
            AboutWindow a = new AboutWindow();
            a.ShowDialog();
        }

        private void sF2ToDLSToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog o = new OpenFileDialog();
            o.RestoreDirectory = true;
            o.Filter = "Sound Font|*.sf2";
            if (o.ShowDialog() == DialogResult.OK) {
                SaveFileDialog s = new SaveFileDialog();
                s.Filter = "Downloadable Sounds|*.dls";
                s.RestoreDirectory = true;
                s.FileName = Path.GetFileNameWithoutExtension(o.FileName) + ".dls";
                if (s.ShowDialog() == DialogResult.OK) {
                    var h = new SoundFont(o.FileName);
                    new DownloadableSounds(h).Write(s.FileName);
                }
            }
        }

        private void dLSToSF2ToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog o = new OpenFileDialog();
            o.RestoreDirectory = true;
            o.Filter = "Downloadable Sounds|*.dls";
            if (o.ShowDialog() == DialogResult.OK) {
                SaveFileDialog s = new SaveFileDialog();
                s.Filter = "Sound Font|*.sf2";
                s.RestoreDirectory = true;
                s.FileName = Path.GetFileNameWithoutExtension(o.FileName) + ".sf2";
                if (s.ShowDialog() == DialogResult.OK) {
                    var h = new DownloadableSounds(o.FileName);
                    new SoundFont(h).Write(s.FileName);
                }
            }
        }

    }

}