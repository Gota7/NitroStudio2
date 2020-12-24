using GotaSequenceLib;
using GotaSequenceLib.Playback;
using GotaSoundIO.Sound;
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
    public partial class SequenceRecorder : Form {

        /// <summary>
        /// Mixer.
        /// </summary>
        public Mixer Mixer = new Mixer();

        /// <summary>
        /// Player.
        /// </summary>
        Player Player;

        /// <summary>
        /// Commands.
        /// </summary>
        private List<SequenceCommand> commands;

        /// <summary>
        /// Sequence start.
        /// </summary>
        private int seqStart;

        /// <summary>
        /// File path.
        /// </summary>
        private string filePath;

        /// <summary>
        /// Create a new sequence recorder.
        /// </summary>
        /// <param name="banks">The banks.</param>
        /// <param name="wars">Wave archives.</param>
        /// <param name="commands">Sequence commands.</param>
        /// <param name="startIndex">Start index.</param>
        public SequenceRecorder(PlayableBank[] banks, RiffWave[][] wars, List<SequenceCommand> commands, int startIndex, string filePath) {

            //Init.
            InitializeComponent();

            //Load stuff.
            Player = new Player(Mixer);
            Player.PrepareForSong(banks, wars);
            this.commands = commands;
            this.seqStart = startIndex;
            this.filePath = filePath;

        }

        /// <summary>
        /// Record the sequence.
        /// </summary>
        private void exportButton_Click(object sender, EventArgs e) {

            //Save.
            Player.LoadSong(commands, seqStart);
            Player.NumLoops = (long)loopsBox.Value;
            Player.DontFadeSong = !fadeBox.Checked;
            Player.Record(filePath);
            Close();

        }

    }
}
