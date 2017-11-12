using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExcelLibrary;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Configuration;
using System.ComponentModel;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

namespace PA_System.Controllers
{
    public class RatingReportController : Controller
    {
        // GET: RatingReport
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewRatingReport()
        {
            if (Request.Form["action"] != null)
            {
                // ExportListUsingEPPlus();
                DataTable dt = new DataTable();

                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("Phone", typeof(string));
                dt.Columns.Add("Days", typeof(Int32));

                dt.Rows.Add("Andi", "amadiba@futuregrowth.co.za", "021 - 659 - 3333", 4);
                dt.Rows.Add("AK", "akoka@futuregrowth.co.za", "021-659-1596", 2);
                dt.Rows.Add("Freddie", "fniekerk@futuregrowth.co.za", "021-659-3333", 15);
                dt.Rows.Add("Richard", "rforsyth@futuregrowth.co.za", "021-659-4569", 19);

                WriteExcelWithNPOI(dt, "xlsx");

            }
            return View();
        }

        public void WriteCsv<T>(IEnumerable<T> data, TextWriter output)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in props)
            {
                output.Write(prop.DisplayName); // header
                output.Write(",");
            }
            output.WriteLine();
            foreach (T item in data)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    output.Write(prop.Converter.ConvertToString(
                         prop.GetValue(item)));
                    output.Write(",");
                }
                output.WriteLine();
            }
        }

        public void WriteHtmlTable<T>(IEnumerable<T> data, TextWriter output)
        {
            //Writes markup characters and text to an ASP.NET server control output stream. This class provides formatting capabilities that ASP.NET server controls use when rendering markup to clients.
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    //  Create a form to contain the List
                    Table table = new Table();
                    TableRow row = new TableRow();
                    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
                    foreach (PropertyDescriptor prop in props)
                    {
                        TableHeaderCell hcell = new TableHeaderCell();
                        hcell.Text = prop.Name;
                        hcell.BackColor = System.Drawing.Color.Yellow;
                        row.Cells.Add(hcell);
                    }

                    table.Rows.Add(row);

                    //  add each of the data item to the table
                    foreach (T item in data)
                    {
                        row = new TableRow();
                        foreach (PropertyDescriptor prop in props)
                        {
                            TableCell cell = new TableCell();
                            cell.Text = prop.Converter.ConvertToString(prop.GetValue(item));
                            row.Cells.Add(cell);
                        }
                        table.Rows.Add(row);
                    }

                    //  render the table into the htmlwriter
                    table.RenderControl(htw);

                    //  render the htmlwriter into the response
                    output.Write(sw.ToString());
                }
            }

        }
        public void ExportListUsingEPPlus()
        {
            var data = new[]{
                              new{ Name="Andi", Email="amadiba@futuregrowth.co.za", Phone="021-659-3333", Days=4 },
                              new{ Name="AK", Email="akoka@futuregrowth.co.za", Phone="021-659-1596", Days=2 },
                              new{ Name="Freddie", Email="fniekerk@futuregrowth.co.za", Phone="021-659-3333", Days=15 },
                              new{ Name="Richard", Email="rforsyth@futuregrowth.co.za", Phone="021-659-4569", Days=19 },

                      };

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("DEV Team");
            workSheet.Cells[1, 1].LoadFromCollection(data, true);
            //from database: 
            //workSheet.Cells[1, 1].LoadFromDataTable(myDataTable, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=Leave Dates.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
        public void WriteExcelWithNPOI(DataTable dt, String extension)
        {

            IWorkbook workbook;

            if (extension == "xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (extension == "xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                throw new Exception("This format is not supported");
            }

            ISheet sheet1 = workbook.CreateSheet("Andi");

            //make a header row
            IRow row1 = sheet1.CreateRow(0);

            for (int j = 0; j < dt.Columns.Count; j++)
            {
                IFont font = workbook.CreateFont();
                font.Boldweight = (short)FontBoldWeight.Bold;
                var style = workbook.CreateCellStyle();
                style.SetFont(font);

              //  r = sheet.CreateRow(0);
              //  r.RowStyle = style;
                ICell cell = row1.CreateCell(j);
                String columnName = dt.Columns[j].ToString();
                cell.SetCellValue(columnName);
                
            }

            //loops through data
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet1.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    ICell cell = row.CreateCell(j);
                    String columnName = dt.Columns[j].ToString();
                    cell.SetCellValue(dt.Rows[i][columnName].ToString());
                }
            }

            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);
                if (extension == "xlsx") //xlsx file format
                {
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", "ContactNPOI.xlsx"));
                    Response.BinaryWrite(exportData.ToArray());
                }
                else if (extension == "xls")  //xls file format
                {
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", "ContactNPOI.xls"));
                    Response.BinaryWrite(exportData.GetBuffer());
                }
                Response.End();
            }
        }
    }
}