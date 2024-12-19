using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO.PDFSearchApp.Helper
{
    public class FileSearchHelper
    {

        public static List<PDFDocumentSearchResult> SearchDocument(string indexPath, string[] must, string[] notMust)
        {
            List<PDFDocumentSearchResult> pDFDocumentSearchResults = new List<PDFDocumentSearchResult>();
            var pdfFiles = Directory.GetFiles(Path.GetDirectoryName(indexPath), "*.pdf", SearchOption.AllDirectories);

            foreach (var oneFile in pdfFiles)
            {
                var fileFolder = Path.Combine(Path.GetDirectoryName(oneFile), $"{Path.GetFileNameWithoutExtension(oneFile)}");

                StringBuilder stringBuilder = new StringBuilder();

                var txtFiles = Directory.GetFiles(fileFolder, "*.txt");
                foreach (var txtFile in txtFiles)
                {
                    var txtAllLine = File.ReadAllText(txtFile, Encoding.UTF8);
                    stringBuilder.Append(txtAllLine);
                }

                bool findDocument = false;
                if (must.Length == 0)
                {
                    findDocument = true;
                }
                foreach (var oneMust in must)
                {
                    if (stringBuilder.ToString().ToUpper().Contains(oneMust.ToUpper()))
                    {
                        findDocument = true;
                    }
                }

                foreach (var oneNotMust in notMust)
                {
                    if (stringBuilder.ToString().ToUpper().Contains(oneNotMust.ToUpper()))
                    {
                        findDocument = false;
                    }
                }

                if (findDocument)
                {
                    pDFDocumentSearchResults.Add(new PDFDocumentSearchResult()
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        FileName = Path.GetFileName(oneFile),
                        FilePath = oneFile,
                    });
                }

            }

            return pDFDocumentSearchResults;
        }

        public static List<PDFDocumentSearchResult> GetAll(string indexPath)
        {
            List<PDFDocumentSearchResult> pDFDocumentSearchResults = new List<PDFDocumentSearchResult>();
            var pdfFiles = Directory.GetFiles(Path.GetDirectoryName(indexPath), "*.pdf", SearchOption.AllDirectories);

            foreach (var oneFile in pdfFiles)
            {
                pDFDocumentSearchResults.Add(new PDFDocumentSearchResult()
                {
                    Id = Guid.NewGuid().ToString("N"),
                    FileName = Path.GetFileName(oneFile),
                    FilePath = oneFile,
                });
            }

            return pDFDocumentSearchResults;
        }
    }
}
