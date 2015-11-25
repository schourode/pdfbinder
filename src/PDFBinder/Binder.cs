/*
 * Copyright 2009-2011 Joern Schou-Rode
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
using System.Collections;
using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PDFBinder
{


    public class PageNumberGeneratorGenerator : IEnumerable<int>
    {
        public String pagesIntent;
        public PageNumberGeneratorGenerator(String pagesIntent)
        {
            this.pagesIntent = pagesIntent;
        }
        public IEnumerator<int> GetEnumerator() {
            return new PageNumberGenerator(this.pagesIntent, LastPage);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new PageNumberGenerator(this.pagesIntent, LastPage);
        }

        private int lastPage = 0;
        public int LastPage
        {
            get { return lastPage; }
            set { lastPage = value; }
        }
    }

    public class PageNumberGenerator : IEnumerator<int>
    {
        public class PageRange 
        {
            public int begin;
            public int end;
        }
        private int lastPage = 0;
        public int LastPage
        {
            get { return lastPage; }
            set { lastPage = value; }
        }
	
        public bool selectAll = true;
        public List<PageRange> ranges = new List<PageRange>();
        private List<PageRange>.Enumerator placeKeeper;
        private int lastPageGenerated = 0;
        public PageNumberGenerator(String pagesIntent, int lastPage)
        {
            List<long> output = new List<long>();
            string[] regions = pagesIntent.Split(',');
            LastPage = lastPage;
            foreach (string r in regions)
	        {
                if (r.Length == 0)
                {
                    continue;
                }
		        string[] endpointsStrings = r.Split('-');
                if (endpointsStrings.Length > 2) {
                    throw new ArgumentException();
                }
                PageRange pr = new PageRange();
                bool ok  = int.TryParse(endpointsStrings[0], out pr.begin);
                if (endpointsStrings.Length == 2 ) {
                    if (endpointsStrings[1] == "") {
                        pr.end = 0;
                    } else {
                        ok  &= int.TryParse(endpointsStrings[1], out pr.end);
                    }
                }
                if (!ok) {
                    throw new ArgumentException("Region couldn't be parsed " + r);
                }
                ranges.Add(pr);
                selectAll = false;
	        }
            placeKeeper = ranges.GetEnumerator();
        }
        public void Reset()
        {
            placeKeeper = ranges.GetEnumerator();
        }

        void IDisposable.Dispose()
        {
            ranges = null;
            placeKeeper.Dispose();
        }
        public int Current
        {
            get { return lastPageGenerated; }
        }
        
        object IEnumerator.Current
        {
            get { return Current;  }
        }

        public bool MoveNext()
        {
            if (selectAll)
            {
                lastPageGenerated += 1;
                return (lastPageGenerated <= LastPage);
            }
            int nextPage = lastPageGenerated + 1;
            bool bMoveForwardToNextRange = false;
            if (placeKeeper.Current == null) 
            {
                bMoveForwardToNextRange = true;

            } 
            else if (placeKeeper.Current.end == 0) // support open-ended selection "5-".
            {
                if (nextPage > LastPage)
                {
                     bMoveForwardToNextRange = true;
                }
            } else if (nextPage > placeKeeper.Current.end)
            {
                 bMoveForwardToNextRange = true;
            }

            if (bMoveForwardToNextRange)
            {
                if (placeKeeper.MoveNext())
                {
                    nextPage = placeKeeper.Current.begin;
                }
                else
                {
                    return false;
                }
            }
                            
            lastPageGenerated = nextPage;
            return true;
        }
    }


    class Combiner : IDisposable
    {
        private readonly Document _document;
        private readonly PdfCopy _pdfCopy;

        public Combiner(string outputFilePath)
        {
            var outputStream = File.Create(outputFilePath);

            _document = new Document();
            _pdfCopy = new PdfCopy(_document, outputStream);
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

                var page = _pdfCopy.GetImportedPage(reader, i);
                _pdfCopy.AddPage(page);
            }

            reader.Close();
        }

        public void AddFile(string fileName, byte[] password, String pages)
        {
            try
            {
                var reader = new PdfReader(fileName, password);
                PageNumberGeneratorGenerator png = new PageNumberGeneratorGenerator(pages);
                AddPages(reader, png);
                reader.Close();
            }
            catch (BadPasswordException bpe) 
            {
                AddFile(fileName);
                return;
            }
        }

        private void AddPage(PdfReader reader, int p) 
        {
            var size = reader.GetPageSizeWithRotation(p);
            _document.SetPageSize(size);
            _document.NewPage();
            var page = _pdfCopy.GetImportedPage(reader, p);
            if (page != null)
            {
                _pdfCopy.AddPage(page);
            } 
            else
            {
                throw new PdfException("Null page returned: " + p );
            }
        }

        private void AddPages(PdfReader reader, PageNumberGeneratorGenerator png)
        {
            png.LastPage = reader.NumberOfPages;

            try
            {
                foreach (int p in png)
                {
                    AddPage(reader, p);
                }
            } catch (Exception pdfe) {
                System.Diagnostics.Debug.Write(pdfe);
                throw new PdfException(pdfe.Message);
            }
        }

       
        public void Dispose()
        {      
            _document.Close();
        }


        public static SourceTestResult TestSourceFile(string fileName, byte[] password)
        {
            bool tryAgain = false;
            while (true)
            {
                try
                {
                    PdfReader reader;
                    if (!tryAgain) 
                    { 
                        reader = new PdfReader(fileName);
                    }
                    else
                    {
                        reader = new PdfReader(fileName, password);
                    }

                    bool ok = !reader.IsEncrypted() || reader.IsOpenedWithFullPermissions || 
                        (reader.Permissions & PdfWriter.AllowAssembly) == PdfWriter.AllowAssembly;
                    reader.Close();

                    return ok ? SourceTestResult.Ok : SourceTestResult.Protected;
                }
                catch (iTextSharp.text.pdf.BadPasswordException bpe)
                {
                    if (tryAgain)
                    {
                        return SourceTestResult.PasswordRequired;
                    }
                    else
                    {
                        tryAgain = true;
                    }
                }
                catch (Exception)
                {
                    return SourceTestResult.Unreadable ;
                }
            }
        }

        public enum SourceTestResult
        {
            Ok, Unreadable, Protected, PasswordRequired
        }
    }
}
