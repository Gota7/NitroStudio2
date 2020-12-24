using GotaSequenceLib.Playback;
using NitroFileLoader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InstrumentType = NitroFileLoader.InstrumentType;

namespace NitroStudio2 {

    /// <summary>
    /// Bank generator.
    /// </summary>
    public partial class BankGenerator : Form {

        /// <summary>
        /// Sound archive.
        /// </summary>
        public SoundArchive SA => MainWindow.SA;

        /// <summary>
        /// Main window.
        /// </summary>
        public MainWindow MainWindow;

        /// <summary>
        /// Writing info.
        /// </summary>
        private bool WritingInfo;

        /// <summary>
        /// Player.
        /// </summary>
        public Player Player;

        /// <summary>
        /// Mixer.
        /// </summary>
        public Mixer Mixer = new Mixer();

        /// <summary>
        /// New bank generator.
        /// </summary>
        public BankGenerator(MainWindow m) {
            InitializeComponent();
            MainWindow = m;
            if (SA.Banks.Where(x => x.File.Instruments.Count > 0).Count() < 1) {
                MessageBox.Show("There must be at least one bank that has an instrument.");
                Close();
                return;
            }
            PopulateBankBox(SA, instruments.Columns["bank"] as DataGridViewComboBoxColumn);
            instruments.CellValueChanged += new DataGridViewCellEventHandler(InstrumentsChanged);
            instruments.RowsRemoved += new DataGridViewRowsRemovedEventHandler(InstrumentsChanged);
            instruments.CellContentClick += new DataGridViewCellEventHandler(PlayRegionButtonClick);
            Player = new Player(Mixer);
            FormClosing += new FormClosingEventHandler(OnClosing);
        }

        /// <summary>
        /// Populate a combo box with banks.
        /// </summary>
        /// <param name="a">The sound archive.</param>
        /// <param name="c">The combo box.</param>
        public static void PopulateBankBox(SoundArchive a, DataGridViewComboBoxColumn c) {
            c.Items.Clear();
            foreach (var w in a.Banks.Where(x => x.File.Instruments.Count > 0)) {
                c.Items.Add("[" + w.Index + "] - " + w.Name);
            }
        }

        /// <summary>
        /// Populate an instrument box.
        /// </summary>
        /// <param name="b">The bank.</param>
        /// <param name="c">The combo box.</param>
        public static void PopulateInstrumentBox(Bank b, DataGridViewComboBoxCell c, bool setFirst = false) {
            if (!setFirst) {
                c.Items.Clear();
                foreach (var i in b.Instruments) {
                    c.Items.Add("[" + i.Index + "] - " + i.Type().ToString());
                }
            } else {
                c.Items[0] = "[" + b.Instruments[0].Index + "] - " + b.Instruments[0].Type().ToString();
                for (int i = 1; i < b.Instruments.Count; i++) {
                    c.Items.Add("[" + b.Instruments[i].Index + "] - " + b.Instruments[i].Type().ToString());
                }
            }
        }

        /// <summary>
        /// Instruments changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void InstrumentsChanged(object sender, EventArgs e) {

            //Writing info.
            if (WritingInfo) {
                return;
            }
            WritingInfo = true;

            //Ids.
            List<int> ids = new List<int>();
            ids.Add(-1);

            //For each instrument.
            for (int i = 1; i < instruments.Rows.Count; i++) {

                //Get the cells.
                var bankCell = (DataGridViewComboBoxCell)instruments.Rows[i - 1].Cells["bank"];
                var instCell = (DataGridViewComboBoxCell)instruments.Rows[i - 1].Cells["instrument"];
                var idCell = (DataGridViewTextBoxCell)instruments.Rows[i - 1].Cells["newId"];
                var warModeCell = (DataGridViewComboBoxCell)instruments.Rows[i - 1].Cells["waveArchiveMode"];

                //Test.
                if (bankCell.Value == null) {
                    bankCell.Value = (instruments.Columns["bank"] as DataGridViewComboBoxColumn).Items[0];
                }
                if (instCell.Items.Count < 1) {
                    PopulateInstrumentBox(SA.Banks.Where(x => x.Index == int.Parse(((string)bankCell.Value).Split('[')[1].Split(']')[0])).FirstOrDefault().File, instCell, false);
                }
                string instBak = "";
                try { instBak = (string)instCell.Value; } catch { }
                instCell.Value = instCell.Items[0];
                while (instCell.Items.Count > 1) {
                    instCell.Items.RemoveAt(instCell.Items.Count - 1);
                }
                if (instBak == null) {
                    instBak = "";
                }
                PopulateInstrumentBox(SA.Banks.Where(x => x.Index == int.Parse(((string)bankCell.Value).Split('[')[1].Split(']')[0])).FirstOrDefault().File, instCell, true);
                if (instCell.Items.Contains(instBak)) {
                    instCell.Value = instBak;
                } else {
                    instCell.Value = instCell.Items[0];
                }
                if (idCell.Value == null || idCell.Value.ToString() == "") {
                    int newId = ids.Last() + 1;
                    while (ids.Contains(newId)) {
                        newId++;
                    }
                    idCell.Value = newId;
                    ids.Add(newId);
                } else {
                    if (ids.Contains(int.Parse(idCell.Value.ToString()))) {
                        int newId = ids.Last() + 1;
                        while (ids.Contains(newId)) {
                            newId++;
                        }
                        idCell.Value = newId;
                        ids.Add(newId);
                    }
                    ids.Add(int.Parse(idCell.Value.ToString()));
                }
                if (warModeCell.Value == null) {
                    warModeCell.Value = (instruments.Columns["waveArchiveMode"] as DataGridViewComboBoxColumn).Items[0];
                }

                //Set war mode cell values.
                bool warModeSecond = warModeCell.Value != warModeCell.Items[0];
                BankInfo b = SA.Banks.Where(x => x.Index == int.Parse(((string)bankCell.Value).Split('[')[1].Split(']')[0])).FirstOrDefault();
                Instrument inst = b.File.Instruments.Where(x => x.Index == int.Parse(((string)instCell.Value).Split('[')[1].Split(']')[0])).FirstOrDefault();
                List<string> wars = new List<string>();
                foreach (var n in inst.NoteInfo) {
                    if (n.InstrumentType == InstrumentType.PCM) {
                        string name = "Null";
                        try {
                            name = b.WaveArchives[n.WarId].Name;
                        } catch {}
                        if (!wars.Contains(name)) { wars.Add(name); }
                    }
                }
                string newWarModeName = "Use Existing Wave Archive" + (wars.Count > 1 ? "s" : "") + " " + "(";
                if (wars.Count < 1) {
                    newWarModeName += "None)";
                } else {
                    for (int j = 0; j < wars.Count - 1; j++) {
                        newWarModeName += wars[j] + ", ";
                    }
                    newWarModeName += wars.Last() + ")";
                }
                warModeCell.Items[1] = newWarModeName;
                if (warModeSecond) { warModeCell.Value = warModeCell.Items[1]; }

            }

            //Writing info.
            WritingInfo = false;

        }

        /// <summary>
        /// Create bank.
        /// </summary>
        private void CreateBnk_Click(object sender, EventArgs e) {

            //New bank and wave archive.
            BankInfo bnk = new BankInfo() { File = new Bank() };
            WaveArchiveInfo war = new WaveArchiveInfo() { File = new WaveArchive() };
            bool usesGen;

            //Get each instrument.
            List<InstrumentInfo> insts = new List<InstrumentInfo>();
            List<string> wars = new List<string>();
            for (int i = 0; i < instruments.Rows.Count - 1; i++) {

                //Get the cells.
                var bankCell = (DataGridViewComboBoxCell)instruments.Rows[i].Cells["bank"];
                var instCell = (DataGridViewComboBoxCell)instruments.Rows[i].Cells["instrument"];
                var idCell = (DataGridViewTextBoxCell)instruments.Rows[i].Cells["newId"];
                var warModeCell = (DataGridViewComboBoxCell)instruments.Rows[i].Cells["waveArchiveMode"];

                //Check.
                if (bankCell.Value == null || instCell.Value == null || idCell.Value == null || !int.TryParse(idCell.Value.ToString(), out _) || warModeCell.Value == null) {
                    MessageBox.Show("Grid contains invalid data.");
                    return;
                }

                //Get the bank.
                BankInfo b = SA.Banks.Where(x => x.Index == int.Parse(((string)bankCell.Value).Split('[')[1].Split(']')[0])).FirstOrDefault();
                Instrument inst = b.File.Instruments.Where(x => x.Index == int.Parse(((string)instCell.Value).Split('[')[1].Split(']')[0])).FirstOrDefault();
                int id = int.Parse(idCell.Value.ToString());
                bool useExistingWar = warModeCell.Value != ((DataGridViewComboBoxColumn)instruments.Columns["waveArchiveMode"]).Items[0];

                //Add the instrument.
                insts.Add(new InstrumentInfo() { Bank = b, Inst = Bank.DuplicateInstrument(inst), NewId = id, UseExistingWar = useExistingWar });

                //Get wars.
                foreach (var n in inst.NoteInfo) {
                    if (warModeCell.Value != warModeCell.Items[0] && n.InstrumentType == InstrumentType.PCM) {
                        string name = "Null";
                        try {
                            name = b.WaveArchives[n.WarId].Name;
                        } catch { }
                        if (!wars.Contains(name) && !name.Equals("Null")) { wars.Add(name); }
                    }
                }

            }

            //Make sure amount of WARs is not over 4.
            if (wars.Count > 4) {
                MessageBox.Show("You can't generate a new bank that uses more than 4 wave archives.");
                return;
            }

            //Uses generated instrument.
            usesGen = insts.Where(x => x.UseExistingWar == false).Count() > 0;

            //Make sure amount of WARs is not over 3 if generated instrument.
            if (wars.Count > 3) {
                MessageBox.Show("You can't generate a new bank that uses more than 3 wave archives when creating a generated wave archive.");
                return;
            }

            //Get name and index.
            try { bnk.Index = SA.Banks.Last().Index + 1; } catch { }
            while (SA.Banks.Where(x => x.Index == bnk.Index).Count() > 0) {
                bnk.Index++;
            }
            try { war.Index = SA.WaveArchives.Last().Index + 1; } catch { }
            while (SA.WaveArchives.Where(x => x.Index == war.Index).Count() > 0) {
                war.Index++;
            }
            bnk.Name = "GENERATED_BANK_" + bnk.Index;
            war.Name = "GENERATED_WAR_" + war.Index;

            //Add instruments.
            Dictionary<ushort, ushort> warLinks = new Dictionary<ushort, ushort>();
            if (usesGen) {
                warLinks.Add((ushort)war.Index, (ushort)warLinks.Count);
            }
            Dictionary<uint, ushort> swavLinks = new Dictionary<uint, ushort>();
            ushort swarNum = usesGen ? (ushort)1 : (ushort)0;
            ushort swavNum = 0;
            foreach (var i in insts) {

                //Adjust wave info for note info.
                foreach (var n in i.Inst.NoteInfo.Where(x => x.InstrumentType == InstrumentType.PCM)) {

                    //Get the hash for the true wave archive Id and wave Id.
                    uint hash;
                    try {
                        hash = (uint)(i.Bank.WaveArchives[n.WarId].Index << 16) | n.WaveId;
                    } catch { continue; }

                    //Use the original wave archive.
                    if (i.UseExistingWar) {
                        if (!warLinks.ContainsKey((ushort)i.Bank.WaveArchives[n.WarId].Index)) {
                            warLinks.Add((ushort)i.Bank.WaveArchives[n.WarId].Index, swarNum++);
                        }
                        n.WarId = warLinks[(ushort)i.Bank.WaveArchives[n.WarId].Index];
                    }

                    //Use a new wave archive.
                    else {
                        if (!swavLinks.ContainsKey(hash)) {

                            //Get the physical wave, and add it.
                            try {

                                //Add the swav.
                                war.File.Waves.Add(i.Bank.WaveArchives[n.WarId].File.Waves[n.WaveId]);

                                //Add the hash.
                                swavLinks.Add(hash, swavNum++);

                            } catch { }

                        }
                        n.WarId = 0;
                        n.WaveId = swavLinks[hash];
                    }

                }

                //Set the new index.
                i.Inst.Index = i.NewId;

                //Finally add the instrument.
                bnk.File.Instruments.Add(i.Inst);

            }

            //Add the bank.
            if (warLinks.Count() > 4) {
                MessageBox.Show("Something went wrong, and the max number of wave archives (4) has been exceeded.");
                return;
            }
            int bnkWarId = 0;
            foreach (var w in warLinks) {
                switch (bnkWarId) {
                    case 0:
                        bnk.WaveArchives[0] = SA.WaveArchives.Where(x => x.Index == w.Key).FirstOrDefault();
                        bnk.ReadingWave0Id = w.Key;
                        break;
                    case 1:
                        bnk.WaveArchives[1] = SA.WaveArchives.Where(x => x.Index == w.Key).FirstOrDefault();
                        bnk.ReadingWave1Id = w.Key;
                        break;
                    case 2:
                        bnk.WaveArchives[2] = SA.WaveArchives.Where(x => x.Index == w.Key).FirstOrDefault();
                        bnk.ReadingWave2Id = w.Key;
                        break;
                    case 3:
                        bnk.WaveArchives[3] = SA.WaveArchives.Where(x => x.Index == w.Key).FirstOrDefault();
                        bnk.ReadingWave3Id = w.Key;
                        break;
                }
                bnkWarId++;
            }
            SA.Banks.Add(bnk);

            //Add the wave archive.
            if (usesGen) {
                SA.WaveArchives.Add(war);
            }

            //Close.
            Close();
            MainWindow.UpdateNodes();
            MainWindow.DoInfoStuff();

        }

        /// <summary>
        /// Play region button.
        /// </summary>
        public void PlayRegionButtonClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex != 0 || e.RowIndex < 0) {
                return;
            }
            try {
                Player.Stop();
                var bnkCell = (DataGridViewComboBoxCell)instruments.Rows[e.RowIndex].Cells["bank"];
                var instCell = (DataGridViewComboBoxCell)instruments.Rows[e.RowIndex].Cells["instrument"];
                var bnk = SA.Banks.Where(x => x.Index == int.Parse(((string)bnkCell.Value).Split('[')[1].Split(']')[0])).FirstOrDefault();
                Instrument inst = bnk.File.Instruments.Where(x => x.Index == int.Parse(((string)instCell.Value).Split('[')[1].Split(']')[0])).FirstOrDefault();
                Player.PrepareForSong(new PlayableBank[] { bnk.File }, bnk.GetAssociatedWaves());
                Player.LoadSong(new List<GotaSequenceLib.SequenceCommand>() { new GotaSequenceLib.SequenceCommand() { CommandType = GotaSequenceLib.SequenceCommands.ProgramChange, Parameter = (uint)inst.Index }, new GotaSequenceLib.SequenceCommand() { CommandType = GotaSequenceLib.SequenceCommands.Note, Parameter = new GotaSequenceLib.NoteParameter() { Note = GotaSequenceLib.Notes.cn4, Length = 48 * 2, Velocity = 127 } }, new GotaSequenceLib.SequenceCommand() { CommandType = GotaSequenceLib.SequenceCommands.Fin } });
                Player.Play();
            } catch { }
        }

        /// <summary>
        /// On closing.
        /// </summary>
        private void OnClosing(object sender, EventArgs e) {
            Mixer.Dispose();
            Player.Dispose();
        }

        /// <summary>
        /// Instrument info.
        /// </summary>
        public struct InstrumentInfo {

            /// <summary>
            /// Bank info.
            /// </summary>
            public BankInfo Bank;

            /// <summary>
            /// Instrument.
            /// </summary>
            public Instrument Inst;

            /// <summary>
            /// New instrument Id.
            /// </summary>
            public int NewId;

            /// <summary>
            /// Use existing war.
            /// </summary>
            public bool UseExistingWar;

        }

    }

}
