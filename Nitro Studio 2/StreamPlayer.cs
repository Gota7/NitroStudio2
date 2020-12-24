using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NitroStudio2 {
    public partial class StreamPlayer : Form {
        public string Path;
        public MainWindow MainWindow;
        public StreamPlayer(MainWindow m, string path, string name) {
            InitializeComponent();
            Text = "Stream Player - " + name + ".strm";
            wmp.URL = path;
            Path = path;
            MainWindow = m;
        }
        private void onClose(object sender, EventArgs e) {
            Thread t = new Thread(delete);
            t.Start();
        }
        private void delete() {
            File.Delete(Path);
            try { MainWindow.StreamTempCount--; } catch { }
        }
    }
}
