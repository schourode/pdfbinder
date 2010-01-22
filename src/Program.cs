/*
 * Copyright 2009, 2010 Joern Schou-Rode
 * 
 * This file is part of PDFBinder.
 *
 * PDFBinder is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.

 * PDFBinder is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with PDFBinder.  If not, see <http://www.gnu.org/licenses/>.
 */

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
