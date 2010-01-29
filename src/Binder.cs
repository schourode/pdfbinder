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
        private readonly Document _document;
        private readonly PdfWriter _writer;

        public Combiner(string outputFilePath)
        {
            var outputStream = File.Create(outputFilePath);

            _document = new Document();
            _writer = PdfWriter.GetInstance(_document, outputStream);
            _document.Open();
        }

        public void AddFile(string fileName)
        {
            var reader = new PdfReader(fileName);

            for (var i = 1; i <= reader.NumberOfPages; i++)
            {
                var size = reader.GetPageSizeWithRotation(i);
                _document.SetPageSize(size);
                _document.NewPage();

                var page = _writer.GetImportedPage(reader, i);
                var rotation = reader.GetPageRotation(i);

                switch (rotation)
                {
                    case 90:
                        _writer.DirectContent.AddTemplate(page, 0, -1, 1, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                        break;
                    // TODO case 180
                    case 270:
                        _writer.DirectContent.AddTemplate(page, 0, 1, -1, 0, reader.GetPageSizeWithRotation(i).Width, 0);
                        break;
                    default:
                        _writer.DirectContent.AddTemplate(page, 0, 0);
                        break;
                }
            }

            reader.Close();
        }

        public void Dispose()
        {
            _document.Close();
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
