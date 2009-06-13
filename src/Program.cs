using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace PDFBinder
{
    static class Program
    {
        public static MainForm MainForm { get; private set; }

        [STAThread]
        static void Main(string[] args)
        {
            // Start uninstallation if command line is /u {product-code}.
            if (args.Length == 2 && args[0].Equals("/u", StringComparison.OrdinalIgnoreCase))
            {
                Process msiexec = new Process();
                msiexec.StartInfo.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "msiexec.exe");
                msiexec.StartInfo.Arguments = "/i " + args[1];
                msiexec.Start();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Program.MainForm = new MainForm();

                ProcessLinker loader = new ProcessLinker();
                loader.SendFileList(args);

                if (loader.IsServer || args.Length == 0)
                {
                    Application.Run(Program.MainForm);
                }
            }
        }
    }
}
