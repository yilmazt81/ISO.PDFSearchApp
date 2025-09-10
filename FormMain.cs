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
using ClosedXML.Excel;




namespace ISO.PDFSearchApp
{
    public partial class FormMain : Form
    {
        List<TextBox> listInludeText = new List<TextBox>();
        List<TextBox> listNotIncludeText = new List<TextBox>();
        List<string> listIncludeString = new List<string>();
        List<string> listNotIncludeString = new List<string>();
        string notincludeLastOpenFile = string.Empty;
        string includeLastOpenFile = string.Empty;
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
            notincludeLastOpenFile = _ini.GetValue("notincludeLastOpenFile");
            includeLastOpenFile = _ini.GetValue("includeLastOpenFile");
            dataGridViewSearchResult.AutoGenerateColumns = false;

            if (!string.IsNullOrEmpty(notincludeLastOpenFile))
            {
                var extention = Path.GetExtension(notincludeLastOpenFile);
                if (extention.ToLower() == ".txt")
                {
                    listNotIncludeString = File.ReadAllLines(notincludeLastOpenFile).ToList();
                }
                else if (extention.ToLower() == ".xlsx")
                {
                    listNotIncludeString = readExcelFile(notincludeLastOpenFile);
                }
                labelOpenNotMustFileCount.Text = listNotIncludeString.Count.ToString();
            }


            if (!string.IsNullOrEmpty(includeLastOpenFile))
            {
                var extention = Path.GetExtension(includeLastOpenFile);
                if (extention.ToLower() == ".txt")
                {
                    listIncludeString = File.ReadAllLines(includeLastOpenFile).ToList();
                }
                else if (extention.ToLower() == ".xlsx")
                {
                    listIncludeString = readExcelFile(includeLastOpenFile);
                }


                labelOpenMustFileCount.Text = listIncludeString.Count.ToString();

            }

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
                var files = System.IO.Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.TopDirectoryOnly);
                _ini.WriteValue("SourceFolder", textBoxSourceFolder.Text);
                _ini.Save();
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

                    try
                    {

                        var fileFolder = Path.Combine(Path.GetDirectoryName(file), $"{Path.GetFileNameWithoutExtension(file)}");
                        if (Directory.Exists(fileFolder))
                        {
                            Directory.Delete(fileFolder, true);
                        }

                        var pageContent = ReadPdfFile(file);


                        lAllText.InsertRange(lAllText.Count, pageContent);
                        foreach (var page in pageContent)
                        {

                            Directory.CreateDirectory(fileFolder);
                            var pdfInner = Path.Combine(fileFolder, $"Page{page.PageNumber}.txt");

                            File.WriteAllText(pdfInner, page.PageText, Encoding.UTF8);
                        }
                        firstFile++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Bir Hata Oluştu {ex.Message} FileName : " + Path.GetFileName(file), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }

                }

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
                        FilePath = fileName,
                        StopShars = currentText.Contains(skippText)

                    });

                }
                pdfReader.Close();
            }
            if (pDFTexts.Count(s => s.StopShars) != 0)
            {
                var maxstopChar = pDFTexts.Where(s => s.StopShars).Max(s => s.PageNumber);
                var removeLen = pDFTexts.Count - maxstopChar;
                pDFTexts.RemoveRange(maxstopChar, removeLen);
            }


            return pDFTexts;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            List<string> mustTex = new List<string>();
            if (string.IsNullOrEmpty(includeLastOpenFile))
            {
                foreach (TextBox textBox in listInludeText)
                {
                    _ini.WriteValue(textBox.Name, textBox.Text);

                    if (!string.IsNullOrEmpty(textBox.Text))
                    {
                        mustTex.Add(textBox.Text);
                    }
                }

            }
            else
            {
                mustTex = listIncludeString;

                foreach (TextBox textBox in listInludeText)
                {
                    _ini.WriteValue(textBox.Name, textBox.Text);
                     
                }
            }
          


            List<string> notmustText = new List<string>();
            if (string.IsNullOrEmpty(notincludeLastOpenFile))
            {


                foreach (TextBox textBox in listNotIncludeText)
                {

                    _ini.WriteValue(textBox.Name, textBox.Text);

                    if (!string.IsNullOrEmpty(textBox.Text))
                    {
                        notmustText.Add(textBox.Text);
                    }
                }
            }
            else
            {
                notmustText = listNotIncludeString;

                foreach (TextBox textBox in listNotIncludeText)
                {

                    _ini.WriteValue(textBox.Name, textBox.Text);
                     
                }
            }
            _ini.WriteValue("notincludeLastOpenFile", notincludeLastOpenFile);
            _ini.WriteValue("includeLastOpenFile", includeLastOpenFile);
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


            listIncludeString.Clear();
            labelOpenMustFileCount.Text = string.Empty;



            labelOpenNotMustFileCount.Text = string.Empty;

            listNotIncludeString.Clear();



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

                string teplateexcelFilePath = Path.Combine(Application.StartupPath, "ExcelTemplate02.xlsx");

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

        private OpenFileDialog GetOpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.txt|*.xlsx",
                Multiselect = false,
            };

            return openFileDialog;
        }


        private List<string> readExcelFile(string fileName)
        {
            List<string> strings = new List<string>();

            using (var workbook = new XLWorkbook(fileName))
            {
                var worksheet = workbook.Worksheet(1); // 1. sayfa
                var range = worksheet.RangeUsed();     // kullanılan hücre aralığı

                foreach (var row in range.Rows())
                {
                    foreach (var cell in row.Cells())
                    {
                        if (cell.Value.IsBlank)
                            continue;
                        strings.Add(cell.Value.GetText());
                        // Console.Write($"{cell.Value}\t");
                    }
                    // Console.WriteLine();
                }
            }

            return strings;
        }
        private void buttonOpenMustFile_Click(object sender, EventArgs e)
        {
            var fileDialog = GetOpenFileDialog();

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                var selectedFile = fileDialog.FileName;
                var extention = Path.GetExtension(selectedFile);

                includeLastOpenFile = fileDialog.FileName;
                if (extention.ToLower() == ".txt")
                {
                    listIncludeString = File.ReadAllLines(selectedFile).ToList();
                }
                else if (extention.ToLower() == ".xlsx")
                {
                    listIncludeString = readExcelFile(selectedFile);
                }

                labelOpenMustFileCount.Text = listIncludeString.Count.ToString();
                for (int i = 0; i < listInludeText.Count; i++)
                {
                    if (i > listIncludeString.Count)
                    {
                        break;
                    }
                    listInludeText[i].Text = listIncludeString[i];

                }
            }
        }

        private void buttonOpenMustFileClear_Click(object sender, EventArgs e)
        {
            labelOpenMustFileCount.Text = string.Empty;
            
            listIncludeString.Clear();
            includeLastOpenFile = string.Empty;
            for (int i = 0; i < listInludeText.Count; i++)
            {

                listInludeText[i].Text = string.Empty;

            }
        }

        private void buttonOpenNotMustFileClear_Click(object sender, EventArgs e)
        {
            labelOpenNotMustFileCount.Text = string.Empty;
            notincludeLastOpenFile = string.Empty;
            listNotIncludeString.Clear();

            for (int i = 0; i < listNotIncludeText.Count; i++)
            {

                listNotIncludeText[i].Text = string.Empty;

            }

        }

        private void buttonOpenNotMustFile_Click(object sender, EventArgs e)
        {
            var fileDialog = GetOpenFileDialog();

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                var selectedFile = fileDialog.FileName;
                var extention = Path.GetExtension(selectedFile);
                if (extention.ToLower() == ".txt")
                {
                    listNotIncludeString = File.ReadAllLines(selectedFile).ToList();
                }
                else if (extention.ToLower() == ".xlsx")
                {
                    listNotIncludeString = readExcelFile(selectedFile);
                }

                notincludeLastOpenFile = fileDialog.FileName;
                labelOpenNotMustFileCount.Text = listNotIncludeString.Count.ToString();
                for (int i = 0; i < listNotIncludeText.Count; i++)
                {
                    if (i > listNotIncludeString.Count)
                    {
                        break;
                    }
                    listNotIncludeText[i].Text = listNotIncludeString[i];

                }
            }
        }
    }

}
