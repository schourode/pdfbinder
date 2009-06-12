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
                document.NewPage();
                PdfImportedPage inputPage = writer.GetImportedPage(reader, page);
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
