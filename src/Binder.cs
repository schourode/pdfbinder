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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PDFBinder
{
    class Combiner : IDisposable
    {
        private MemoryStream memory;
        private Document document;
        private PdfWriter writer;

        public Combiner()
        {
            memory = new MemoryStream();
            document = new Document();
            writer = PdfWriter.GetInstance(document, memory);
            writer.CloseStream = false;
            document.Open();
        }

        public void AddFile(string fileName)
        {
            PdfReader reader = new PdfReader(fileName);
            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                PdfImportedPage inputPage = writer.GetImportedPage(reader, page);

                document.SetPageSize(inputPage.BoundingBox);
                document.NewPage();

                writer.DirectContent.AddTemplate(inputPage, 0, 0);
            }
            reader.Close();
        }

        public void SaveCombinedDocument(string outputFileName)
        {
            document.Close();
            using (Stream stream = File.Create(outputFileName))
            {
                memory.WriteTo(stream);
            }
        }

        public void Dispose()
        {
            memory.Dispose();
        }

        public static SourceTestResult TestSourceFile(string fileName)
        {
            try
            {
                PdfReader reader = new PdfReader(fileName);
                bool ok = !reader.IsEncrypted() ||
                    (reader.Permissions & PdfWriter.ALLOW_COPY) == PdfWriter.ALLOW_COPY;
                reader.Close();

                return ok ? SourceTestResult.Ok : SourceTestResult.Protected;
            }
            catch
            {
                return SourceTestResult.Unreadable;
            }
        }

        public enum SourceTestResult
        {
            Ok, Unreadable, Protected
        }
    }
}
