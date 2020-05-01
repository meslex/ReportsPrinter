using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;
using ReportsDAL.Models;
using Humanizer;

namespace Reports
{
    public class FormReport
    {

        private static Microsoft.Office.Interop.Excel.Application xlApp;
        private static Excel.Workbook xlWorkBook;
        private static Excel.Worksheet xlWorkSheet;
        private static object misValue = System.Reflection.Missing.Value;
        private static Excel.Range chartRange;

        private static void SetColumnWidth()
        {
            chartRange = xlWorkSheet.Range["a1"];
            chartRange.ColumnWidth =2;

            chartRange = xlWorkSheet.Range["b1"];
            chartRange.ColumnWidth = 0.91;

            chartRange = xlWorkSheet.Range["c1"];
            chartRange.ColumnWidth = 9.67;

            chartRange = xlWorkSheet.Range["d1"];
            chartRange.ColumnWidth = 13.67;

            chartRange = xlWorkSheet.Range["e1"];
            chartRange.ColumnWidth = 26.33;

            chartRange = xlWorkSheet.Range["f1"];
            chartRange.ColumnWidth = 14.33;

            chartRange = xlWorkSheet.Range["g1"];
            chartRange.ColumnWidth = 11;

            chartRange = xlWorkSheet.Range["h1"];
            chartRange.ColumnWidth = 9;

            chartRange = xlWorkSheet.Range["i1"];
            chartRange.ColumnWidth = 9;

            chartRange = xlWorkSheet.Range["j1"];
            chartRange.ColumnWidth =5;

        }

        private static string GetCity()
        {

            var appSettings = ConfigurationManager.AppSettings;
            string city = appSettings["city"] ?? "Not Found";
            return city;
        }


        public static void Form(ReportData reportData)
        {


            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlApp.StandardFont = "Times New Roman";

            SetColumnWidth();

            xlWorkSheet.Range["a2", "j2"].Merge(false);
            chartRange = xlWorkSheet.Range["a2"];
            chartRange.Value = $"Акт №{reportData.Number}";
            //chartRange.Font.Name = "Times New Roman";
            chartRange.Font.Size = 11;
            chartRange.Font.Bold = true;
            chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            xlWorkSheet.Range["a3", "j3"].Merge(false);
            chartRange = xlWorkSheet.Range["a3"];
            chartRange.Value = $"прийому-передачі наданих послуг до договору № {reportData.Contract.Number}  від {reportData.Contract.From.ToShortDateString()} року";
            //chartRange.Font.Name = "Times New Roman";
            chartRange.Font.Size = 9;
            chartRange.Font.Bold = true;
            chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            xlWorkSheet.Range["a4", "c4"].Merge(false);
            chartRange = xlWorkSheet.Range["a4"];
            chartRange.Value = $"м. {GetCity()}";
            //chartRange.Font.Name = "Times New Roman";
            chartRange.Font.Size = 9;
            chartRange.Font.Bold = true;
            chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            xlWorkSheet.Range["h4", "j4"].Merge(false);
            chartRange = xlWorkSheet.Range["h4"];
            chartRange.Value = $"{reportData.Date.ToShortDateString()} року";
            //chartRange.Font.Name = "Times New Roman";
            chartRange.Font.Size = 9;
            chartRange.Font.Bold = true;
            chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            chartRange = xlWorkSheet.Range["a5"];
            chartRange.RowHeight = 7;

            xlWorkSheet.Range["a6", "j6"].Merge(false);
            chartRange = xlWorkSheet.Range["a6"];
            chartRange.RowHeight = 66;
            chartRange.Value = $"  Ми, що нижче підписалися, Виконавець - {reportData.Executor.FullName} " +
                $"та представник Замовника – Голова правління БО \"БТ \"100% ЖИТТЯ ДНІПРО\" " +
                $"{ConfigurationManager.AppSettings["chairman"]}, який діє на підставі Статуту, цим актом " +
                $"засвідчуємо що Виконавець надав, а Замовник отримав, в рамках виконання " +
                $"умов Грантової Угоди № {reportData.GrantAgreement.Number} від " +
                $"{reportData.GrantAgreement.From}р." +
                $" та проекту «{reportData.GrantAgreement.Name}», наступні послуги: ";

            chartRange.Characters[42, reportData.Executor.FullName.Length].Font.Bold = true;
            chartRange.WrapText = true;
            chartRange.Font.Size = 9;
            chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

            int row = 7;
            for(int d = 0; d < reportData.Directions.Count; ++d)
            {
                chartRange = xlWorkSheet.Range[$"a{row}", $"j{row}"];
                chartRange.Merge(false);
                chartRange.RowHeight = 21;
                chartRange.Value = $"{reportData.Directions[d].Number}: \"{reportData.Directions[d].Name}\"";
                chartRange.Characters[0, reportData.Directions[d].Number.ToString().Length + 1].Font.Bold = true;
                chartRange.WrapText = true;
                chartRange.Font.Size = 9;
                chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                row++;

                chartRange = xlWorkSheet.Range[$"a{row}", $"b{row}"];
                chartRange.Merge(false);
                chartRange.WrapText = true;
                chartRange.Value = "№ з/п";
                chartRange.Font.Bold = true;
                chartRange.Font.Size = 9;
                chartRange.RowHeight = 22.50;
                chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                chartRange = xlWorkSheet.Range[$"c{row}", $"e{row}"];
                chartRange.Merge(false);
                chartRange.WrapText = true;
                chartRange.Value = "Найменування послуг";
                chartRange.Font.Size = 9;
                chartRange.Font.Bold = true;
                chartRange.RowHeight = 22.50;
                chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                chartRange = xlWorkSheet.Range[$"f{row}"];
                chartRange.WrapText = true;
                chartRange.Value = "Одиниця виміру";
                chartRange.Font.Size = 9;
                chartRange.Font.Bold = true;
                chartRange.RowHeight = 22.50;
                chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                chartRange = xlWorkSheet.Range[$"g{row}"];
                chartRange.WrapText = true;
                chartRange.Value = "Кількість послуг";
                chartRange.Font.Size = 9;
                chartRange.Font.Bold = true;
                chartRange.RowHeight = 22.50;
                chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                chartRange = xlWorkSheet.Range[$"h{row}"];
                chartRange.WrapText = true;
                chartRange.Value = "Ціна, грн";
                chartRange.Font.Size = 9;
                chartRange.Font.Bold = true;
                chartRange.RowHeight = 22.50;
                chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                chartRange = xlWorkSheet.Range[$"i{row}", $"j{row}"];
                chartRange.Merge(false);
                chartRange.WrapText = true;
                chartRange.Value = "Вартість, грн";
                chartRange.Font.Size = 9;
                chartRange.Font.Bold = true;
                chartRange.RowHeight = 22.50;
                chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                row++;

                for(int s = 0; s < reportData.Directions[d].Services.Count; ++s)
                {
                    chartRange = xlWorkSheet.Range[$"a{row}", $"b{row}"];
                    chartRange.Merge(false);
                    chartRange.WrapText = true;
                    chartRange.Value = $"{s+1}";
                    chartRange.Font.Size = 9;
                    chartRange.RowHeight = 22.50;
                    chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                    chartRange = xlWorkSheet.Range[$"c{row}", $"e{row}"];
                    chartRange.Merge(false);
                    chartRange.WrapText = true;
                    chartRange.Value = reportData.Directions[d].Services[s].Name;
                    chartRange.Font.Size = 9;
                    chartRange.RowHeight = 22.50;
                    chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                    chartRange = xlWorkSheet.Range[$"f{row}"];
                    chartRange.WrapText = true;
                    chartRange.Value = "послуга";
                    chartRange.Font.Size = 9;
                    chartRange.RowHeight = 22.50;
                    chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                    chartRange = xlWorkSheet.Range[$"g{row}"];
                    chartRange.WrapText = true;
                    chartRange.Value = reportData.Directions[d].Services[s].Amount;
                    chartRange.Font.Size = 9;
                    chartRange.RowHeight = 22.50;
                    chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                    chartRange = xlWorkSheet.Range[$"h{row}"];
                    chartRange.WrapText = true;
                    chartRange.Value = Math.Round(reportData.Directions[d].Services[s].Price,2);
                    chartRange.Font.Size = 9;
                    chartRange.RowHeight = 22.50;
                    chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                    chartRange = xlWorkSheet.Range[$"i{row}", $"j{row}"];
                    chartRange.Merge(false);
                    chartRange.WrapText = true;
                    chartRange.Value = Math.Round(reportData.Directions[d].Services[s].Total,2);
                    chartRange.Font.Size = 9;
                    chartRange.RowHeight = 22.50;
                    chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                    row++;

                }

                chartRange = xlWorkSheet.Range[$"a{row}", $"e{row}"];
                chartRange.Merge(false);
                chartRange.WrapText = true;
                chartRange.Value = "Всього";
                chartRange.Font.Bold = true;
                chartRange.Font.Size = 9;
                chartRange.RowHeight = 22.50;
                chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                chartRange = xlWorkSheet.Range[$"f{row}"];
                chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                chartRange = xlWorkSheet.Range[$"g{row}"];
                chartRange.WrapText = true;
                chartRange.Value = $"=SUM(g{row-reportData.Directions[d].Services.Count}:g{row-1})";
                chartRange.Font.Size = 9;
                chartRange.Font.Bold = true;
                chartRange.RowHeight = 22.50;
                chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                chartRange = xlWorkSheet.Range[$"h{row}"];
                chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                chartRange = xlWorkSheet.Range[$"i{row}", $"j{row}"];
                chartRange.Merge(false);
                chartRange.WrapText = true;
                chartRange.Value = CalcValueOfServices(reportData.Directions[d].Services); // $"=SUM(i{row - reportData.Directions[d].Services.Count}:i{row - 1})";
                chartRange.Font.Size = 9;
                chartRange.Font.Bold = true;
                chartRange.RowHeight = 22.50;
                chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                row++;
            }

            chartRange = xlWorkSheet.Range[$"a{row}", $"h{row}"];
            chartRange.Merge(false);
            chartRange.WrapText = true;
            chartRange.Value = "Загальна кількість наданих послуг за цим актом прийому - передачі складає, шт";
            chartRange.Font.Size = 9;
            chartRange.Font.Bold = true;
            chartRange.RowHeight = 22.50;
            chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

            chartRange = xlWorkSheet.Range[$"i{row}", $"j{row}"];
            chartRange.Merge(false);
            chartRange.WrapText = true;
            chartRange.Value = CalcTotalAmountOfServices(reportData.Directions);
            chartRange.Font.Size = 9;
            chartRange.Font.Bold = true;
            chartRange.RowHeight = 22.50;
            chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

            row++;

            chartRange = xlWorkSheet.Range[$"a{row}", $"h{row}"];
            chartRange.Merge(false);
            chartRange.WrapText = true;
            chartRange.Value = "Загальна вартість наданих послуг за цим актом прийому-передачі становить, грн";
            chartRange.Font.Size = 9;
            chartRange.Font.Bold = true;
            chartRange.RowHeight = 22.50;
            chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

            chartRange = xlWorkSheet.Range[$"i{row}", $"j{row}"];
            chartRange.Merge(false);
            chartRange.WrapText = true;
            chartRange.Value = Math.Round(CalcTotalValueOfServices(reportData.Directions), 2);
            chartRange.Font.Size = 9;
            chartRange.Font.Bold = true;
            chartRange.RowHeight = 22.50;
            chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

            row++;

            chartRange = xlWorkSheet.Range[$"a{row}", $"j{row}"];
            chartRange.Merge(false);
            chartRange.WrapText = true;
            chartRange.Value = $"({ NumberToWords(CalcTotalValueOfServices(reportData.Directions))})  без ПДВ,";
            chartRange.Font.Size = 9;
            chartRange.Font.Bold = true;
            chartRange.RowHeight = 22.50;
            chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            row++;

            chartRange = xlWorkSheet.Range[$"a{row}", $"j{row}"];
            chartRange.Merge(false);
            chartRange.WrapText = true;
            chartRange.Value = $"в тому числі відрахування до Державних фондів: податок з доходів" +
                $" фізичних осіб {ConfigurationManager.AppSettings["tax"]}% та " +
                $"військовий збір {ConfigurationManager.AppSettings["militaryGathering"]}% із суми загальної вартості послуг.";
            chartRange.Font.Size = 9;
            chartRange.RowHeight = 26;
            chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            row++;

            chartRange = xlWorkSheet.Range[$"a{row}", $"h{row}"];
            chartRange.Merge(false);
            chartRange.WrapText = true;
            chartRange.Value = $"Замовник сплачує до Пенсійного Фонду України Єдиний" +
                $" соціальний внесок {ConfigurationManager.AppSettings["socialContribution"]}% на загальну" +
                $" суму наданих послуг за цим Договором.";
            chartRange.Font.Size = 9;
            chartRange.RowHeight = 22;
            chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            chartRange = xlWorkSheet.Range[$"i{row}", $"j{row}"];
            chartRange.Merge(false);
            chartRange.WrapText = true;
            chartRange.Value = $"{CalcSocialContribution(reportData.Directions)} грн";
            chartRange.Font.Size = 9;
            chartRange.RowHeight = 22;
            chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            row++;

            chartRange = xlWorkSheet.Range[$"a{row}", $"h{row}"];
            chartRange.Merge(false);
            chartRange.WrapText = true;
            chartRange.Value = $"Замовник, за надані за цим актом послуги,  виплачує Виконавцю суму коштів в розмірі ";
            chartRange.Font.Size = 9;
            chartRange.RowHeight = 22;
            chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            chartRange = xlWorkSheet.Range[$"i{row}", $"j{row}"];
            chartRange.Merge(false);
            chartRange.WrapText = true;
            chartRange.Value = $"{CalcTax(reportData.Directions)} грн";
            chartRange.Font.Size = 9;
            chartRange.RowHeight = 22;
            chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            row++;

            chartRange = xlWorkSheet.Range[$"a{row}", $"j{row}"];
            chartRange.Merge(false);
            chartRange.WrapText = true;
            chartRange.Value = $"Виконавцем було надано Замовнику послуги, які передбачені " +
                $"умовами Договору, якісно та у повному обсязі.";
            chartRange.Font.Size = 9;
            chartRange.RowHeight = 22;
            chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            row++;

            chartRange = xlWorkSheet.Range[$"a{row}", $"j{row}"];
            chartRange.Merge(false);
            chartRange.WrapText = true;
            chartRange.Value = $"Сторони претензій одна до одної не мають.";
            chartRange.Font.Size = 9;
            chartRange.RowHeight = 22;
            chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            row++;

            chartRange = xlWorkSheet.Range[$"a{row}"];
            chartRange.RowHeight = 7;

            row++;

            chartRange = xlWorkSheet.Range[$"a{row}", $"d{row}"];
            chartRange.Merge(false);
            chartRange.WrapText = true;
            chartRange.Value = $"Послуги надав:";
            chartRange.Font.Size = 9;
            chartRange.RowHeight = 22;
            chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            chartRange = xlWorkSheet.Range[$"g{row}", $"j{row}"];
            chartRange.Merge(false);
            chartRange.WrapText = true;
            chartRange.Value = $"Послуги прийняв: ";
            chartRange.Font.Size = 9;
            chartRange.RowHeight = 22;
            chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            row++;

            chartRange = xlWorkSheet.Range[$"d{row}"];
            chartRange.Merge(false);
            chartRange.WrapText = true;
            chartRange.Value = $"{reportData.Executor.ShortName}";
            chartRange.Font.Size = 9;
            chartRange.RowHeight = 26;
            chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            chartRange = xlWorkSheet.Range[$"g{row}", $"j{row}"];
            chartRange.Merge(false);
            chartRange.WrapText = true;
            chartRange.Value = $"Голова правління  __________  {ConfigurationManager.AppSettings["chairmanShortForm"]}";
            chartRange.Font.Size = 9;
            chartRange.RowHeight = 26;
            chartRange.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            chartRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            xlApp.Visible = true;
            xlApp.UserControl = true;
        }

        public static void Example()
        {
           /* Microsoft.Office.Interop.Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            Excel.Range chartRange;*/

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //add data 
            xlWorkSheet.Cells[4, 2] = "";
            xlWorkSheet.Cells[4, 3] = "Student1";
            xlWorkSheet.Cells[4, 4] = "Student2";
            xlWorkSheet.Cells[4, 5] = "Student3";

            xlWorkSheet.Cells[5, 2] = "Term1";
            xlWorkSheet.Cells[5, 3] = "80";
            xlWorkSheet.Cells[5, 4] = "65";
            xlWorkSheet.Cells[5, 5] = "45";

            xlWorkSheet.Cells[6, 2] = "Term2";
            xlWorkSheet.Cells[6, 3] = "78";
            xlWorkSheet.Cells[6, 4] = "72";
            xlWorkSheet.Cells[6, 5] = "60";

            xlWorkSheet.Cells[7, 2] = "Term3";
            xlWorkSheet.Cells[7, 3] = "82";
            xlWorkSheet.Cells[7, 4] = "80";
            xlWorkSheet.Cells[7, 5] = "65";

            xlWorkSheet.Cells[8, 2] = "Term4";
            xlWorkSheet.Cells[8, 3] = "75";
            xlWorkSheet.Cells[8, 4] = "82";
            xlWorkSheet.Cells[8, 5] = "68";

            xlWorkSheet.Cells[9, 2] = "Total";
            string temp = "=SUM(c5:c8)";
            chartRange = xlWorkSheet.Cells[9, 3];
            chartRange.Value = temp;


            xlWorkSheet.Cells[9, 4] = "299";
            xlWorkSheet.Cells[9, 5] = "238";

            xlWorkSheet.Range["b2", "e3"].Merge(false);

            chartRange = xlWorkSheet.Range["b2", "e3"];
            chartRange.FormulaR1C1 = "MARK LIST";
            chartRange.HorizontalAlignment = 3;
            chartRange.VerticalAlignment = 3;
            chartRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
            chartRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
            chartRange.Font.Size = 20;

            chartRange = xlWorkSheet.Range["b4", "e4"];
            chartRange.Font.Bold = true;
            chartRange = xlWorkSheet.get_Range("b9", "e9");
            chartRange.Font.Bold = true;

            chartRange = xlWorkSheet.get_Range("b2", "e9");
            chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

            chartRange = xlWorkSheet.Range["b5", "e5"];
            chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

            chartRange = xlWorkSheet.Range["b6", "e6"];
            chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

            chartRange = xlWorkSheet.Range["b7", "e7"];
            chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

            chartRange = xlWorkSheet.Range["b8", "e8"];
            chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

            chartRange = xlWorkSheet.Range["b9"];
            chartRange.ColumnWidth = 31.43;

            chartRange = xlWorkSheet.Cells[2, 2];
            //chartRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.ForestGreen);
            //chartRange.Value = "Hello World";  
            //xlWorkBook.SaveAs("csharp.net-informations.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlApp.Visible = true;
            xlApp.UserControl = true;
            //xlWorkBook.Close(true, misValue, misValue);
            //xlApp.Quit();

            //releaseObject(xlApp);
            //releaseObject(xlWorkBook);
            //releaseObject(xlWorkSheet);

            MessageBox.Show("File created !");
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private static int CalcTotalAmountOfServices(IList<Direction> _directions)
        {
            int result = 0;
            for (int i = 0; i < _directions.Count; ++i)
            {
                result += _directions[i].Services.Count;
            }

            return result;
        }

        private static double CalcValueOfServices(IList<Service> services)
        {
            float result = 0;

            for(int i = 0; i < services.Count; ++i)
            {
                result += services[i].Price;
            }

            return Math.Round(result, 2);
        }

        private static float CalcTotalValueOfServices(IList<Direction> _directions)
        {
            float result = 0;
            for (int i = 0; i < _directions.Count; ++i)
            {
                for(int c = 0; c < _directions[i].Services.Count; ++c)
                {
                    result += _directions[i].Services[c].Total;
                }
            }

            return result;
        }

        private static string NumberToWords(float number)
        {
            string currency;
            string numStr = number.ToString();
            int substringIndex = numStr.IndexOf(",");
            if(substringIndex > 0)
            {
                numStr = numStr.Substring(substringIndex - 1);
            }

            float copyNumber = Convert.ToSingle(numStr);

            if (copyNumber == 1)
                currency = "гривня";
            else if(copyNumber >= 2 && copyNumber <= 4)
                currency = "гривні";
            else
                currency = "гривень";


            string str = $"{((int)number).ToWords(GrammaticalGender.Neuter)} {currency} {FractionalPart(number)} копійок";

            return str;
        }

        private static float CalcTax(IList<Direction> _directions)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string city = appSettings["city"] ?? "Not Found";
            float tax = Convert.ToSingle(appSettings["tax"]);
            float militaryGathering = Convert.ToSingle(appSettings["militaryGathering"]);
            //float socialContribution = Convert.ToSingle(appSettings["socialContribution"]);
            float result = CalcTotalValueOfServices( _directions) * ((tax + militaryGathering) / 100);

            result = CalcTotalValueOfServices(_directions) - result;

            return (float)Math.Round(result,2);
        }

        private static float CalcSocialContribution(IList<Direction> _directions)
        {
            float socialContribution = Convert.ToSingle(ConfigurationManager.AppSettings["socialContribution"]);

            float result = CalcTotalValueOfServices(_directions) * (socialContribution / 100);

            return (float)Math.Round(result, 2);
        }

        private static int FractionalPart(float floatNumber)
        {
            double x = floatNumber - Math.Truncate(floatNumber);
            x *= 100;
            x = Math.Floor(x);
            return (int)x;
        }
    }
}
