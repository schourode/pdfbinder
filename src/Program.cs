using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace PDFBinder
{
    static class Program
    {
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
                // Load application normally - display main form.
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());

                // Load file names from command line into list.
                foreach (string file in args)
                {
                    // TODO Implement command line file loading.
                }
            }
        }
    }
}
