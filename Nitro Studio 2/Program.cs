using GotaSoundBank.DLS;
using GotaSoundBank.SF2;
using GotaSoundIO.Sound;
using NitroFileLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NitroStudio2 {
    static class Program {

        /// <summary>
        /// Path.
        /// </summary>
        public static string NitroPath = Application.StartupPath;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            
            //Start.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Argument mode.
            if (args.Length > 0) {

                //Switch type.
                switch (Path.GetExtension(args[0])) {

                    //Sound archive.
                    case ".sdat":
                        Application.Run(new MainWindow(args[0]));
                        break;

                    //Sound sequence.
                    case ".sseq":
                        Application.Run(new SequenceEditor(args[0]));
                        break;

                    //Sound archive.
                    case ".ssar":
                        Application.Run(new SequenceArchiveEditor(args[0]));
                        break;

                    //Sound bank.
                    case ".sbnk":
                        Application.Run(new BankEditor(args[0]));
                        break;

                    //Sound wave archive.
                    case ".swar":
                        Application.Run(new WaveArchiveEditor(args[0]));
                        break;

                    //Stream.
                    case ".strm":
                        RiffWave r = new RiffWave();
                        NitroFileLoader.Stream s = new NitroFileLoader.Stream();
                        s.Read(args[0]);
                        r.FromOtherStreamFile(s);
                        r.Write(MainWindow.NitroPath + "/" + "tmpStream" + 0 + ".wav");
                        Application.Run(new StreamPlayer(null, MainWindow.NitroPath + "/" + "tmpStream" + 0 + ".wav", Path.GetFileNameWithoutExtension(args[0])));
                        break;

                }

            } else {

                //Start the editor.
                Application.Run(new MainWindow());

            }

        }
    }
}
