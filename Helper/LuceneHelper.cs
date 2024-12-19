using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iTextSharp.text;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;



namespace ISO.PDFSearchApp.Helper
{
    public class LuceneHelper
    {

        public static void CreateFolderIndex(string indexPath, List<PDFText> pdfFiles)
        {
            Lucene.Net.Store.Directory directory = FSDirectory.Open(new DirectoryInfo(indexPath));

            var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            var writer = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED);
            var distinctFiles = pdfFiles.Select(s => s.FileName).Distinct().ToList();

            foreach (var distinctFile in distinctFiles)
            {
                var txtFiles = pdfFiles.Where(s => s.FileName == distinctFile).ToArray();
                var txtAllText = string.Empty;
                string filePath = string.Empty;
                foreach (var oneFile in txtFiles)
                {
                    txtAllText += oneFile.PageText;
                    filePath = oneFile.FilePath;
                }
                AddDocument(writer, txtAllText, distinctFile, filePath);
            }
            writer.Commit(); 
            writer.Dispose();

        }
      
        private static BooleanQuery GetQuery(string[] term, string[] exts, StandardAnalyzer analyzer)
        {
            string[] fields = { "content" };
            MultiFieldQueryParser parser = new MultiFieldQueryParser( Lucene.Net.Util.Version.LUCENE_30,fields, analyzer);
            BooleanQuery combinedQuery = new BooleanQuery();

            foreach (var field in term)
            {
                Query multiFieldQuery = parser.Parse(field);
                TermQuery extQuery = new TermQuery(new Term("content",field));
                combinedQuery.Add(multiFieldQuery, Occur.MUST);
            }


            foreach (var field in exts)
            {
                Query multiFieldQuery = parser.Parse(field);
                TermQuery extQuery = new TermQuery(new Term("content", field));
                combinedQuery.Add(multiFieldQuery, Occur.MUST_NOT);
            }

            

            return combinedQuery;
        }
        public static List<PDFDocumentSearchResult> SearchDocument(string indexPath, string[] must, string[] notMust)
        {
            Lucene.Net.Store.Directory directory = FSDirectory.Open(new DirectoryInfo(indexPath));

            var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);

            var searcher = new IndexSearcher(directory, true);
            var parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "content", analyzer);

            var booleanQuery = GetQuery(must, notMust, analyzer);
            /*
            foreach (var mustOne in must)
            {
                var mustQuery = new TermQuery(new Term("content", mustOne));
                booleanQuery.Add(mustQuery,  Occur.MUST);
            }
            foreach (var notMustOne in notMust)
            {
                var mustNotQuery = new TermQuery(new Term("content", notMustOne));
                 
                booleanQuery.Add(mustNotQuery, Occur.MUST_NOT);  
            }*/

            List<PDFDocumentSearchResult> pDFDocumentSearchResults = new List<PDFDocumentSearchResult>();
            // 4. Sorguyu çalıştır

           /* Query query = parser.Parse(must[0]);
            var results = searcher.Search(query, 500);
            */
            var results = searcher.Search(booleanQuery, 500);

            foreach (var hit in results.ScoreDocs)
            {
                var foundDoc = searcher.Doc(hit.Doc);
                pDFDocumentSearchResults.Add(new PDFDocumentSearchResult()
                {
                    FileName = foundDoc.Get("fileName"),
                    FilePath= foundDoc.Get("filePath"),
                    Id= foundDoc.Get("id"),
                });
                Console.WriteLine($"ID: {foundDoc.Get("id")}, İçerik: {foundDoc.Get("content")}");
            }


            searcher.Dispose();
            directory.Dispose();

            return pDFDocumentSearchResults;
        }
        static void AddDocument(IndexWriter writer, string content, string fileName, string filePath)
        {
            var doc = new Lucene.Net.Documents.Document();
            doc.Add(new Field("id", Guid.NewGuid().ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("fileName", fileName, Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("filePath", filePath, Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("content", content, Field.Store.YES, Field.Index.ANALYZED));
            writer.AddDocument(doc);
        }
    }
}
