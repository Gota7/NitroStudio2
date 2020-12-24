using System;
using System.Windows.Forms;

namespace NitroStudio2 {
    public partial class SaveQuitDialog : Form
    {

        EditorBase parentTwo;

        public SaveQuitDialog(EditorBase parent2)
        {
            InitializeComponent();
            parentTwo = parent2;
        }

        private void SaveQuitDialog_Load(object sender, EventArgs e)
        {

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            try { parentTwo.Close(); } catch { }
        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            //Save application
            //try { parent.save(); } catch { }
            //try { parentTwo.save(); } catch { }
            try { parentTwo.saveToolStripMenuItem_Click(sender, e); } catch { }

            //Exit application
            try { parentTwo.Close(); } catch { }

        }
    }
}
