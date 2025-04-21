using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Lucene.Net.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ISO.PDFSearchApp.Helper
{
    public class ExcelHelper
    {

        public void WriteFile(string sourceFile, string targetFile, List<PDFDocumentSearchResult> pDFDocuments)
        {
            File.Copy(sourceFile, targetFile, true);
            using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(targetFile, true))
            {
                WorksheetPart worksheetPart = GetWorksheetPartByName(spreadSheet, "ExportData");

                uint rowIndex = 2;
                foreach (var onePDF in pDFDocuments)
                {
                    var nssns = GetNSN2(onePDF.FilePath);
                    var nSN2 = FormatNSN2(nssns);
                    var nsn2Group = string.Empty;
                    if (!string.IsNullOrEmpty(nSN2))
                    {
                        var nsnPart = nSN2.Split('-');
                        if (nsnPart.Length > 1)
                        {
                            nsn2Group = nsnPart[0];
                        }
                    }
                    int productSummer = 0;
                    int adressCount = 0;
                    string ui = string.Empty;
                    string inspectation = string.Empty;
                    var productSum = ReadProductLine(onePDF.FilePath,ref productSummer,ref adressCount,ref ui,ref inspectation);

                    SetCellValue(worksheetPart, rowIndex, "S", productSum);
                    SetCellValue(worksheetPart, rowIndex, "I", ui);
                    SetCellValue(worksheetPart, rowIndex, "M", inspectation);
                    SetCellValue(worksheetPart, rowIndex, "H", adressCount.ToString());
                    SetCellValue(worksheetPart, rowIndex, "A", GetNAICS(onePDF.FilePath));
                    SetCellValue(worksheetPart, rowIndex, "B", nsn2Group);
                    SetCellValue(worksheetPart, rowIndex, "C", nSN2);
                    SetCellValue(worksheetPart, rowIndex, "D", nssns);
                    SetCellValue(worksheetPart, rowIndex, "E", Path.GetFileNameWithoutExtension(onePDF.FileName));
                 //   SetCellValue(worksheetPart, rowIndex, "F", GetItemDescription(onePDF.FilePath));

                    SetCellValue(worksheetPart, rowIndex, "G", GetQuantity(onePDF.FilePath));



                    SetCellValue(worksheetPart, rowIndex, "K", GetDeliveryInDays(onePDF.FilePath));
                    SetCellValue(worksheetPart, rowIndex, "L", GetDeliveryFob(onePDF.FilePath));
                    SetCellValue(worksheetPart, rowIndex, "O", GetPartPieceNumber(onePDF.FilePath));

                    bool noHistory = CheckNoHistoryAvailable(onePDF.FilePath);
                    if (!noHistory)
                    {
                        string histroyQantity = string.Empty;
                        string unitCost = string.Empty;
                        string awdDate = string.Empty;
                        SetCellValue(worksheetPart, rowIndex, "R", GetCage(onePDF.FilePath, ref histroyQantity, ref unitCost, ref awdDate));
                        SetCellValue(worksheetPart, rowIndex, "S", histroyQantity);
                        SetCellValue(worksheetPart, rowIndex, "T", unitCost);
                        SetCellValue(worksheetPart, rowIndex, "V", awdDate);
                    }

                    rowIndex++;
                }

                worksheetPart.Worksheet.Save();
                spreadSheet.Save();
            }

        }
        private int Yuvarla(string t)
        {
            if (t.IndexOf(".") > -1)
            {
                var index = t.IndexOf(".");

                var newT = t.Substring(0,index);

                return int.Parse(newT);
            }
            else
            {
               return int.Parse(t);

            }
        }
        private string ReadProductLine(string pdfFilePath,ref int productSummer,ref int adressCount,ref string ui,ref string inspectationPoint)
        {
            var productLine = System.Configuration.ConfigurationManager.AppSettings["ProductLineParam"];
            var adressLineParams = System.Configuration.ConfigurationManager.AppSettings["AdressLineParam"].Split('|');
            var inspectationPointParam = System.Configuration.ConfigurationManager.AppSettings["InspectationPointParam"];

            var pdfIndexPath = Path.Combine(Path.GetDirectoryName(pdfFilePath), Path.GetFileNameWithoutExtension(pdfFilePath));

            var txtFiles = Directory.GetFiles(pdfIndexPath);

            var findAdress = false;
            foreach (var txtFile in txtFiles)
            {
                var findProductLine = false;
                var txtLines = File.ReadAllLines(txtFile, Encoding.UTF8);
                foreach (var txtLine in txtLines)
                {
                    if (txtLine.Contains(productLine))
                    {
                        findProductLine = true;
                        continue;
                    }
                    if (findProductLine)
                    {
                        var txtParts = txtLine.Split(' ');
                        if (txtParts.Length>18)
                        {
                            try
                            {
                              
                                productSummer +=  Yuvarla(txtParts[19].Replace(",", ""));
                                ui = txtParts[15];
                            }
                            catch (Exception)
                            {
                                System.Diagnostics.Debug.WriteLine("ddd");
                            } 
                        } 
                         
                    } 
                    
                    if (findProductLine)
                    {
                        foreach (var adressLine in adressLineParams)
                        {
                            if (txtLine.Contains(adressLine))
                            {
                                findProductLine = true;
                                adressCount++;
                            }
                        }
                    }

                    if (findProductLine)
                    {
                        var findInspect = txtLine.Contains(inspectationPointParam);
                        if (findInspect)
                        {
                            var split = txtLine.Trim().Split(' ').Where(s => !string.IsNullOrEmpty(s)).ToArray();


                            inspectationPoint = split[2];
                            inspectationPoint = inspectationPoint.Substring(0, 4);
                        }
                    }
                } 
            }

            return "";

        }

        private string GetNAICS(string pdfFilePath)
        {
            var NAICSParam = System.Configuration.ConfigurationManager.AppSettings["NAICSParam"];
            var NAICS = string.Empty;
            var pdfIndexPath = Path.Combine(Path.GetDirectoryName(pdfFilePath), Path.GetFileNameWithoutExtension(pdfFilePath));

            var txtFiles = Directory.GetFiles(pdfIndexPath);
            try
            {
                foreach (var txtFile in txtFiles)
                {
                    var txtLines = File.ReadAllLines(txtFile, Encoding.UTF8);
                    foreach (var txtLine in txtLines)
                    {
                        if (txtLine.Contains(NAICSParam))
                        {
                            var startIndex = NAICSParam.Length + 1;
                            var lastIndex = txtLine.Length - startIndex;

                            NAICS = txtLine.Substring(startIndex, lastIndex).Trim();
                            break;
                        }
                    }
                    if (!string.IsNullOrEmpty(NAICS))
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error";
            }

            return NAICS;
        }

        private string GetItemDescription(string pdfFilePath)
        {
            var itemDescriptionParam = System.Configuration.ConfigurationManager.AppSettings["ItemDescriptionParam"];
            var itemDescription = string.Empty;
            var pdfIndexPath = Path.Combine(Path.GetDirectoryName(pdfFilePath), Path.GetFileNameWithoutExtension(pdfFilePath));

            var txtFiles = Directory.GetFiles(pdfIndexPath);

            try
            {
                foreach (var txtFile in txtFiles)
                {
                    var txtLines = File.ReadAllLines(txtFile, Encoding.UTF8);
                    bool findItemDescription = false;
                    foreach (var txtLine in txtLines)
                    {
                        if (txtLine.Contains(itemDescriptionParam) && !txtLine.Contains("PLEASE CONTACT THE BUYER SHOWN ABOVE."))
                        {
                            findItemDescription = true;
                            continue;

                        }
                        if (findItemDescription)
                        {
                            itemDescription = txtLine.Trim();
                            break;
                        }
                    }
                    if (!string.IsNullOrEmpty(itemDescription))
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error";
            }

            return itemDescription;
        }

        private string GetQuantity(string pdfFilePath)
        {
            var itemDescriptionParam = System.Configuration.ConfigurationManager.AppSettings["QuantityParam"];
            var itemDescription = string.Empty;
            var pdfIndexPath = Path.Combine(Path.GetDirectoryName(pdfFilePath), Path.GetFileNameWithoutExtension(pdfFilePath));

            var txtFiles = Directory.GetFiles(pdfIndexPath);

            try
            {
                foreach (var txtFile in txtFiles)
                {
                    var txtLines = File.ReadAllLines(txtFile, Encoding.UTF8);
                    bool findItemDescription = false;
                    int quantityIndex = 0;
                    /*
                    if (Path.GetFileNameWithoutExtension(pdfIndexPath)== "SPE4A624T598A" && Path.GetFileNameWithoutExtension(txtFile)== "Page7")
                    {
                        System.Diagnostics.Debug.WriteLine("ddd");
                    }*/
                    foreach (var txtLine in txtLines)
                    {
                        if (txtLine.Contains(itemDescriptionParam))
                        {
                            findItemDescription = true;
                            quantityIndex = txtLine.IndexOf("QUANTITY");
                            if (quantityIndex == -1)
                            {
                                findItemDescription = false;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        if (findItemDescription)
                        {
                            var tempStr = txtLine.Substring(quantityIndex, 8).Trim();
                            var split = tempStr.Split('.');
                            if (split.Length == 1)
                            {
                                itemDescription = tempStr;
                            }
                            else
                            {
                                itemDescription = split[0];
                            }
                            break;
                        }
                    }
                    if (!string.IsNullOrEmpty(itemDescription))
                    {
                        break;
                    }
                }
            }
            catch (Exception)
            {

                return "Error";
            }


            return itemDescription;
        }
        private string GetPartPieceNumber(string pdfFilePath)
        {

            var partypieceNumberParam = System.Configuration.ConfigurationManager.AppSettings["PartPieceNumber"];

            var pdfIndexPath = Path.Combine(Path.GetDirectoryName(pdfFilePath), Path.GetFileNameWithoutExtension(pdfFilePath));

            var txtFiles = Directory.GetFiles(pdfIndexPath);
            var partPieceNumber = "";
            foreach (var txtFile in txtFiles)
            {
                var txtLines = File.ReadAllLines(txtFile, Encoding.UTF8);
                bool findItemDescription = false;
                int quantityIndex = 0;

                foreach (var txtLine in txtLines)
                {
                    if (string.IsNullOrEmpty(txtLine.Trim()))
                        continue;
                    if (txtLine.Contains(partypieceNumberParam))
                    {
                        findItemDescription = true;
                        

                    }
                    if (findItemDescription)
                    {
                        var split = txtLine.Trim().Split(' ').Where(s => !string.IsNullOrEmpty(s)).ToArray();

                        if (split.Length == 4)
                        {
                            partPieceNumber = split[3];

                        }

                        break;
                    }
                }
                if (!string.IsNullOrEmpty(partPieceNumber))
                {
                    break;
                }
            } 
            return partPieceNumber;

        }
        private string GetDeliveryFob(string pdfFilePath)
        {

            var itemDeliveryFobParam = System.Configuration.ConfigurationManager.AppSettings["DeliveryFob"];

            var pdfIndexPath = Path.Combine(Path.GetDirectoryName(pdfFilePath), Path.GetFileNameWithoutExtension(pdfFilePath));

            var txtFiles = Directory.GetFiles(pdfIndexPath);
            var deliveryFob = "";
            foreach (var txtFile in txtFiles)
            {
                var txtLines = File.ReadAllLines(txtFile, Encoding.UTF8);
                bool findItemDescription = false;
                int quantityIndex = 0;

                foreach (var txtLine in txtLines)
                {
                    if (string.IsNullOrEmpty(txtLine.Trim()))
                        continue;
                    if (txtLine.Contains(itemDeliveryFobParam))
                    {
                        findItemDescription = true;
                        //continue;

                    }
                    if (findItemDescription)
                    {
                        var split = txtLine.Trim().Split(' ').Where(s => !string.IsNullOrEmpty(s)).ToArray();

                     
                          deliveryFob = split[2]; 

                        break;
                    }
                }
                if (!string.IsNullOrEmpty(deliveryFob))
                {
                    break;
                }
            }
            if (!string.IsNullOrEmpty(deliveryFob))
            {
                deliveryFob = deliveryFob.Substring(0, 4);
            }
            return deliveryFob;

        }
        private string GetCage(string pdfFilePath, ref string historyQantity, ref string unitCost, ref string awdDate)
        {
            var itemDescriptionParam = System.Configuration.ConfigurationManager.AppSettings["CageParam"];
            var itemDescription = string.Empty;
            var pdfIndexPath = Path.Combine(Path.GetDirectoryName(pdfFilePath), Path.GetFileNameWithoutExtension(pdfFilePath));

            var txtFiles = Directory.GetFiles(pdfIndexPath);
            foreach (var txtFile in txtFiles)
            {
                var txtLines = File.ReadAllLines(txtFile, Encoding.UTF8);
                bool findItemDescription = false;
                int quantityIndex = 0;
            
                foreach (var txtLine in txtLines)
                {
                    if (string.IsNullOrEmpty(txtLine.Trim()))
                        continue;
                    if (txtLine.Contains(itemDescriptionParam))
                    {
                        findItemDescription = true;
                        continue;

                    }
                    if (findItemDescription && !txtLine.Contains("CAGE") && txtLine.Trim() != ".")
                    {
                        var split = txtLine.Trim().Split(' ').Where(s => !string.IsNullOrEmpty(s)).ToArray();

                        itemDescription = split[0];
                        var splitQ = split[2].Split('.');
                        unitCost = Rounding(split[3]);
                        awdDate = split[4];
                        if (splitQ.Length != 1)
                        {
                            historyQantity = splitQ[0];
                        }
                        else
                        {
                            historyQantity = split[2];

                        }


                        break;
                    }
                }
                if (!string.IsNullOrEmpty(itemDescription))
                {
                    break;
                }
            }

            if (string.IsNullOrEmpty(itemDescription))
            {
                System.Diagnostics.Debug.Write("ddd");
            }
            return itemDescription;
        }
        private string Rounding(string part)
        {
            var indexDot = part.Split('.');
            if (indexDot.Length == 1)
            {
                return part;
            }
            else
            {
                var newValue = indexDot[0] + "." + (indexDot[1].Length == 2 ? indexDot[1] : indexDot[1].Substring(0, 2));

                return newValue;
            }
        }

        private string FormatNSN2(string NSN2)
        {
            //5310-00-494-1771
            //3100-11-835-111
            try
            {
                var str1 = NSN2.Substring(0, 4);
                var str2 = NSN2.Substring(4, 2);
                var str3 = NSN2.Substring(6, 3);
                var str4 = NSN2.Substring(9, 4);

                return $"{str1}-{str2}-{str3}-{str4}";

            }
            catch (Exception)
            {
                return "Error";

            }
        }
        private string GetNSN2(string pdfFilePath)
        {
            var NSN2Param = System.Configuration.ConfigurationManager.AppSettings["NSN2Param"];
            var NSN2 = string.Empty;
            var pdfIndexPath = Path.Combine(Path.GetDirectoryName(pdfFilePath), Path.GetFileNameWithoutExtension(pdfFilePath));

            var txtFiles = Directory.GetFiles(pdfIndexPath);
            foreach (var txtFile in txtFiles)
            {
                var txtLines = File.ReadAllLines(txtFile, Encoding.UTF8);
                foreach (var txtLine in txtLines)
                {
                    if (txtLine.Contains(NSN2Param))
                    {
                        var startIndex = NSN2Param.Length;
                        var lastIndex = txtLine.Length - startIndex;

                        NSN2 = txtLine.Substring(startIndex, lastIndex).Trim();
                        break;
                    }
                }
                if (!string.IsNullOrEmpty(NSN2))
                {
                    break;
                }
            }

            return NSN2;
        }

        private bool CheckNoHistoryAvailable(string pdfFilePath)
        {

            var NSN2 = string.Empty;
            var pdfIndexPath = Path.Combine(Path.GetDirectoryName(pdfFilePath), Path.GetFileNameWithoutExtension(pdfFilePath));
            var noHistory = "No History Available";
            var txtFiles = Directory.GetFiles(pdfIndexPath);
            foreach (var txtFile in txtFiles)
            {
                var txtLines = File.ReadAllLines(txtFile, Encoding.UTF8);
                foreach (var txtLine in txtLines)
                {
                    if (txtLine.Contains(noHistory))
                    {

                        return true;
                    }
                }
            }

            return false;
        }

        private string GetDeliveryInDays(string pdfFilePath)
        {
            var deliveryParam = System.Configuration.ConfigurationManager.AppSettings["DeliveryInDay"];
            var deliveryIndDay = string.Empty;
            var pdfIndexPath = Path.Combine(Path.GetDirectoryName(pdfFilePath), Path.GetFileNameWithoutExtension(pdfFilePath));

            var txtFiles = Directory.GetFiles(pdfIndexPath);
            foreach (var txtFile in txtFiles)
            {
                var txtLines = File.ReadAllLines(txtFile, Encoding.UTF8);
                foreach (var txtLine in txtLines)
                {
                    if (txtLine.Contains(deliveryParam))
                    {
                        var startIndex = deliveryParam.Length;
                        var lastIndex = txtLine.Length - startIndex;

                        deliveryIndDay = txtLine.Substring(startIndex, lastIndex).Trim();
                        break;
                    }
                }


                if (!string.IsNullOrEmpty(deliveryIndDay))
                {

                    try
                    {
                        return int.Parse(deliveryIndDay).ToString();

                    }
                    catch
                    {
                        return "Error";
                    }
                }
            }

            return deliveryIndDay;
        }

        private static Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart, Row row)
        {
            Worksheet worksheet = worksheetPart.Worksheet;

            string cellReference = columnName + rowIndex;

            // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
            Cell refCell = null;
            foreach (Cell cell in row.Elements<Cell>())
            {
                if (cell.CellReference.Value.Length == cellReference.Length)
                {
                    if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                    {
                        refCell = cell;
                        break;
                    }
                }
            }

            Cell newCell = new Cell() { CellReference = cellReference };
            row.InsertBefore(newCell, refCell);

            worksheet.Save();
            return newCell;

        }
        public static Row InsertRow(WorksheetPart worksheetPart)
        {
            SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
            Row lastRow = sheetData.Elements<Row>().LastOrDefault();

            if (lastRow != null)
            {
                var newRow = new Row() { RowIndex = (lastRow.RowIndex + 1) };
                sheetData.InsertAfter(newRow, lastRow);
                return newRow;
            }
            else
            {
                var newrow = new Row() { RowIndex = 0 };
                sheetData.Append(newrow);
                return newrow;
            }


        }
        private static Row GetRow(Worksheet worksheet, uint rowIndex)
        {
            var rowList = worksheet.GetFirstChild<SheetData>().Elements<Row>().ToList();

            var selectedRow = rowList.Where(r => r.RowIndex == rowIndex).ToList();
            if (selectedRow.Count == 0)
            {
                return InsertRow(worksheet.WorksheetPart);
            }
            else
            {

                return selectedRow.First();
            }
        }
        private static Cell GetCell(WorksheetPart worksheetPart, Worksheet worksheet,
        string columnName, uint rowIndex)
        {
            Row row = GetRow(worksheet, rowIndex);

            if (row == null)
                return null;

            var cell = row.Elements<Cell>().ToArray();

            var cellList = row.Elements<Cell>().Where(c => string.Compare(c.CellReference.Value, columnName + rowIndex, true) == 0).ToList();

            return (cellList.Count == 0 ? InsertCellInWorksheet(columnName, rowIndex, worksheetPart, row) : cellList.First());
        }
        private decimal ConvertStr(string v)
        {
            var outV = decimal.Zero;
            v = v.Replace(",", ".");
            if (decimal.TryParse(v, out outV))
            {
                return outV;
            }
            else
            {
                return 0;
            }
        }
        private void SetCellValue(WorksheetPart worksheetPart, uint rowIndex, string columnName, string fieldValue)
        {
            try
            {
                Cell cell = GetCell(worksheetPart, worksheetPart.Worksheet,
                                  columnName, rowIndex);

                cell.CellValue = new CellValue(fieldValue);

                var cellValue = ConvertStr(fieldValue);
                cell.DataType =
                       new EnumValue<CellValues>(CellValues.String);

                if (cellValue == 0)
                {


                }
                else
                {

                    /* cell.DataType =
                         new EnumValue<CellValues>(CellValues.);
                     */
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }




        private WorksheetPart
         GetWorksheetPartByName(SpreadsheetDocument document,
         string sheetName)
        {
            IEnumerable<Sheet> sheets =
               document.WorkbookPart.Workbook.GetFirstChild<Sheets>().
               Elements<Sheet>().Where(s => s.Name == sheetName);

            if (sheets.Count() == 0)
            {
                // The specified worksheet does not exist.

                return null;
            }

            string relationshipId = sheets.First().Id.Value;
            WorksheetPart worksheetPart = (WorksheetPart)
                 document.WorkbookPart.GetPartById(relationshipId);
            return worksheetPart;

        }
    }
}
