using GotaSoundIO.Sound;
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

namespace NitroStudio2 {
    public partial class WaveMapper : Form {

        /// <summary>
        /// Wave archives.
        /// </summary>
        public List<ushort> WarMap = null;

        /// <summary>
        /// Waves.
        /// </summary>
        private List<RiffWave> wavs = new List<RiffWave>();

        /// <summary>
        /// Player.
        /// </summary>
        public GotaSoundIO.Sound.Playback.StreamPlayer Player = new GotaSoundIO.Sound.Playback.StreamPlayer();

        /// <summary>
        /// Bank importer.
        /// </summary>
        /// <param name="waves">Waves.</param>
        /// <param name="wars">Wave archives.</param>
        public WaveMapper(List<RiffWave> waves, List<WaveArchiveInfo> wars, bool hideId = false) {

            //Check.
            if (wars.Count < 1) {
                MessageBox.Show("The target bank must be hooked up to at least one wave archive.");
                Close();
                return;
            }

            //Init.
            InitializeComponent();
            mapGrid.CellContentClick += new DataGridViewCellEventHandler(PlayRegionButtonClick);
            if (hideId) { mapGrid.Columns[1].Visible = false; }
            FormClosing += new FormClosingEventHandler(OnClosing);

            //Wave archive index.
            foreach (var w in wars) {
                waveArchive.Items.Add("[" + w.Index + "] " + w.Name);
            }

            //Add waves.
            int num = 0;
            wavs = waves;
            foreach (var wav in waves) {
                mapGrid.Rows.Add(new DataGridViewRow());
                var v = mapGrid.Rows[mapGrid.Rows.Count - 1];
                ((DataGridViewTextBoxCell)v.Cells[1]).Value = num++;
                ((DataGridViewComboBoxCell)v.Cells[2]).Value = waveArchive.Items[0];
            }

        }

        /// <summary>
        /// Finished.
        /// </summary>
        private void finishedButton_Click(object sender, EventArgs e) {
            WarMap = new List<ushort>();
            foreach (DataGridViewRow r in mapGrid.Rows) {
                string s = (string)((DataGridViewComboBoxCell)r.Cells[2]).Value;
                WarMap.Add(ushort.Parse(s.Split(']')[0].Split('[')[1]));
            }
            Close();
        }

        /// <summary>
        /// Play region button.
        /// </summary>
        public void PlayRegionButtonClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex != 0 || e.RowIndex < 0) {
                return;
            }
            Player.Stop();
            Player.LoadStream(wavs[e.RowIndex]);
            Player.Play();
        }

        /// <summary>
        /// On closing.
        /// </summary>
        private void OnClosing(object sender, EventArgs e) {
            Player.Dispose();
        }

    }
}
