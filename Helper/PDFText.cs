using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO.PDFSearchApp.Helper
{
    public class PDFText
    {

        public int PageNumber { get; set; }

        public string PageText { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

    }

    public class PDFDocumentSearchResult
    {
        public string Id { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}
