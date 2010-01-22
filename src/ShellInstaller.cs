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
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;

using Microsoft.Win32;

namespace PDFBinder
{
    [RunInstaller(true)]
    public class ShellInstaller : Installer
    {
        const string SHELL_LABEL = "Add to PDFBinder...";

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            AddShellExtension();
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
            RemoveShellExtension();
        }

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
            RemoveShellExtension();
        }

        private void AddShellExtension()
        {
            bool installed;
            using (var shell = OpenPdfShellKey(out installed))
            {
                if (shell != null && !installed)
                {
                    using (var binder = shell.CreateSubKey(SHELL_LABEL))
                    {
                        using (var command = binder.CreateSubKey("command"))
                        {
                            command.SetValue(null, string.Format("\"{0}.exe\" \"%1\"",
                                Path.Combine(Context.Parameters["pdfbinder"], typeof(ShellInstaller).Assembly.GetName().Name)));
                        }
                    }
                }
            }
        }

        private void RemoveShellExtension()
        {
            bool installed;
            using (var shell = OpenPdfShellKey(out installed))
            {
                if (installed)
                {
                    shell.DeleteSubKeyTree(SHELL_LABEL);
                }
            }
        }

        private RegistryKey OpenPdfShellKey(out bool installed)
        {
            installed = false;

            string pdfClientKey;
            using (var pdf = Registry.ClassesRoot.OpenSubKey(".pdf", false))
                pdfClientKey = pdf == null ? null : pdf.GetValue(null) as string;

            if (pdfClientKey == null)
                return null;

            var shell = Registry.ClassesRoot.OpenSubKey(pdfClientKey + "\\shell", true);

            if (shell == null)
                return null;

            using (var binder = shell.OpenSubKey(SHELL_LABEL))
                installed = (binder != null);

            return shell;
        }
    }
}
