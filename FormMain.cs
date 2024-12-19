using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Path = System.IO.Path;
using ISO.PDFSearchApp.Helper;
using System.Xml.Linq;
using Directory = System.IO.Directory;
using System.Diagnostics;
using DocumentFormat.OpenXml.Drawing.Charts;



namespace ISO.PDFSearchApp
{
    public partial class FormMain : Form
    {
        List<TextBox> listInludeText = new List<TextBox>();
        List<TextBox> listNotIncludeText = new List<TextBox>();
        string indexFolder = string.Empty;
        List<PDFDocumentSearchResult> pDFDocumentSearchResults = new List<PDFDocumentSearchResult>();
        Ini _ini = null;
        string pdfId = "";
        public FormMain()
        {
            InitializeComponent();

            _ini = new Ini(Path.Combine(Application.StartupPath, "app.ini"));

            textBoxSourceFolder.Text = _ini.GetValue("SourceFolder");
            textBoxTargetFolder.Text = _ini.GetValue("TargetFolder");

            // var maxTextSize = _ini.GetValue("MaxTextSize");
            /* if (string.IsNullOrEmpty(maxTextSize))
             {
                 maxTextSize = "25";
                 _ini.WriteValue("MaxTextSize", maxTextSize);
                 _ini.Save();
             }*/
            dataGridViewSearchResult.AutoGenerateColumns = false;


            foreach (var control in groupBoxInclude.Controls)
            {
                if (control is TextBox)
                {
                    var textBox = (TextBox)control;
                    listInludeText.Add(textBox);

                    textBox.Text = _ini.GetValue(textBox.Name);
                    //textBox.MaxLength = int.Parse(maxTextSize);

                }
            }

            foreach (var control in groupBoxNotInclude.Controls)
            {
                if (control is TextBox)
                {
                    var textBox = (TextBox)control;
                    listNotIncludeText.Add(textBox);
                    //  textBox.MaxLength = int.Parse(maxTextSize);
                    textBox.Text = _ini.GetValue(textBox.Name);
                }
            }
        }



        private void buttonBrowser_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBoxSourceFolder.Text = dialog.SelectedPath;

                CreateFileIndex(textBoxSourceFolder.Text);
            }
        }

        private void CreateFileIndex(string sourceFolder)
        {
            try
            {
                var files = System.IO.Directory.GetFiles(sourceFolder, "*.pdf",SearchOption.AllDirectories);

                indexFolder = Path.Combine(sourceFolder, "indexfolder");
                if (Directory.Exists(indexFolder))
                {
                    Directory.Delete(indexFolder, true);
                }

                Directory.CreateDirectory(indexFolder);

                var messageResult = MessageBox.Show($"{files.Length} Adet dosya içeriği okunacaktır bu işlem zaman alabilir \n Devam etmek istiyor musunuz ? ", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (messageResult == DialogResult.No)
                {
                    return;
                }

                List<PDFText> lAllText = new List<PDFText>();
                var firstFile = 0;
                foreach (var file in files)
                {
                    // var pdfComplated = Path.ChangeExtension(file, ".txt");


                    var fileFolder = Path.Combine(Path.GetDirectoryName(file), $"{Path.GetFileNameWithoutExtension(file)}");
                    if (Directory.Exists(fileFolder))
                    {
                        Directory.Delete(fileFolder, true);
                    }

                    var pageContent = ReadPdfFile(file);

                    if (firstFile == 0)
                    {
                        var difst = pageContent.FirstOrDefault();
                        difst.PageText = difst.PageText + " mustafa".ToUpper();

                    }
                    lAllText.InsertRange(lAllText.Count, pageContent);
                    foreach (var page in pageContent)
                    {

                        Directory.CreateDirectory(fileFolder);
                        var pdfInner = Path.Combine(fileFolder, $"Page{page.PageNumber}.txt");

                        File.WriteAllText(pdfInner, page.PageText, Encoding.UTF8);
                    }
                    firstFile++;
                    // File.WriteAllText(pdfComplated, "Complated");
                }
                //Helper.LuceneHelper.CreateFolderIndex(indexFolder, lAllText);
                _ini.WriteValue("SourceFolder", textBoxSourceFolder.Text);
                _ini.Save();
                MessageBox.Show("İşlem bitti , arama yapabilirsiniz", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir Hata Oluştu {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<PDFText> ReadPdfFile(string fileName)
        {
            List<PDFText> pDFTexts = new List<PDFText>();
            var skippText = System.Configuration.ConfigurationManager.AppSettings["PDFStopShars"];
            bool findSkipText = false;
            if (File.Exists(fileName))
            {
                PdfReader pdfReader = new PdfReader(fileName);

                for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);


                    currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));

                    pDFTexts.Add(new PDFText()
                    {
                        PageNumber = page,
                        PageText = currentText,
                        FileName = Path.GetFileName(fileName),
                        FilePath = fileName
                    });

                    if (!string.IsNullOrEmpty(skippText) && currentText.Contains(skippText))
                    {
                        break;
                    }
                }
                pdfReader.Close();
            }
            return pDFTexts;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            List<string> mustTex = new List<string>();
            foreach (TextBox textBox in listInludeText)
            {
                _ini.WriteValue(textBox.Name, textBox.Text);

                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    mustTex.Add(textBox.Text);
                }
            }
            List<string> notmustText = new List<string>();
            foreach (TextBox textBox in listNotIncludeText)
            {

                _ini.WriteValue(textBox.Name, textBox.Text);

                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    notmustText.Add(textBox.Text);
                }
            }
            _ini.Save();

            try
            {
                indexFolder = Path.Combine(textBoxSourceFolder.Text, "indexfolder");

                if (!Directory.Exists(indexFolder))
                {
                    MessageBox.Show("Yenile butonuna basınız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (mustTex.Count == 0 && notmustText.Count == 0)
                {
                    pDFDocumentSearchResults = FileSearchHelper.GetAll(indexFolder);
                }
                else
                {
                    pDFDocumentSearchResults = FileSearchHelper.SearchDocument(indexFolder, mustTex.ToArray(), notmustText.ToArray());
                }

                dataGridViewSearchResult.DataSource = pDFDocumentSearchResults;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir Hata Oluştu {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            foreach (TextBox textBox in listInludeText)
            {
                textBox.Text = string.Empty;
            }
            foreach (TextBox textBox in listNotIncludeText)
            {
                textBox.Text = string.Empty;
            }
        }

        private void buttonRefreshFolder_Click(object sender, EventArgs e)
        {
            CreateFileIndex(textBoxSourceFolder.Text);
        }

        private void buttonBrowstarget_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBoxTargetFolder.Text = dialog.SelectedPath;

            }
        }

        private void buttonStartCopy_Click(object sender, EventArgs e)
        {
            if (pDFDocumentSearchResults.Count == 0)
            {
                MessageBox.Show("Arama yapılmadı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                string targetFolder = textBoxTargetFolder.Text;
                if (!Directory.Exists(targetFolder))
                {
                    Directory.CreateDirectory(targetFolder);
                }

                foreach (var oneFile in pDFDocumentSearchResults)
                {
                    var newFilePath = Path.Combine(targetFolder, oneFile.FileName);
                    File.Copy(oneFile.FilePath, newFilePath, true);
                }

                string teplateexcelFilePath = Path.Combine(Application.StartupPath, "EmptyExportTemplate.xlsx");

                string newExcelFilePath = Path.Combine(textBoxTargetFolder.Text, "Export_" + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmmss") + ".xlsx");

                ExcelHelper excelHelper = new ExcelHelper();
                excelHelper.WriteFile(teplateexcelFilePath, newExcelFilePath, pDFDocumentSearchResults);

                _ini.WriteValue("TargetFolder", targetFolder);
                _ini.Save();

                MessageBox.Show("Kopyalama Bitmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir Hata Oluştu {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void MenuItemOpenPDF_Click(object sender, EventArgs e)
        {
            if (dataGridViewSearchResult.SelectedRows.Count == 0)
            {
                return;
            }
            try
            {
                var selectedPDF = pDFDocumentSearchResults.FirstOrDefault(s => s.Id == pdfId);
                if (selectedPDF != null)
                {
                    Process.Start(selectedPDF.FilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir Hata Oluştu {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewSearchResult_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewSearchResult.SelectedRows)
            {
                pdfId = row.Cells[0].Value.ToString();

            }
        }
    }
}
