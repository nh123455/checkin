using DevExpress.DataAccess.Excel;
using DevExpress.SpreadsheetSource;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkin.BUS
{
    public class baseBUS
    {
        string sever = "", csdl = "", user = "", pass = "";
        #region Dùng để chọn file Excel
        public static DataTable GetExcelFileDataSource(int Width, int Height)
        {
            OpenFileDialog openFD = new OpenFileDialog();
            string filePath = stringPathFile(openFD);
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(filePath))
            {
                ExcelDataSource ds = new ExcelDataSource();
                ds.FileName = filePath;
                ExcelSourceOptions excelSourceOptions1 = new ExcelSourceOptions();
                ExcelWorksheetSettings excelWorksheetSettings1 = new ExcelWorksheetSettings();
                string sheetName = Microsoft.VisualBasic.Interaction.InputBox("Nhập Sheet name của file Excel bạn đã chọn", "Sheet name", GetWorkSheetNameByIndex(0, filePath), Width / 2, Height / 2);
                try
                {
                    if (!String.IsNullOrEmpty(sheetName))
                    {
                        excelWorksheetSettings1.WorksheetName = sheetName;
                        excelSourceOptions1.ImportSettings = excelWorksheetSettings1;
                        ds.SourceOptions = excelSourceOptions1;
                        ds.Fill();

                        dt = ExcelToDataTable(ds);
                    }
                }
                catch
                {

                }
            }
            return dt;
        }
        public static string stringPathFile(OpenFileDialog openFileDialog1)
        {
            openFileDialog1.Filter = "Excel 2007->2010 (.xlsx)|*.xlsx";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.FileName = "";
            openFileDialog1.Multiselect = false;
            openFileDialog1.InitialDirectory = @"D:\";
            DialogResult dg = openFileDialog1.ShowDialog();

            if (dg == DialogResult.OK)
            {
                if (openFileDialog1.CheckPathExists == false || openFileDialog1.CheckFileExists == false)
                {
                    XtraMessageBox.Show("Đường dẫn này không tồn tại !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "";
                }
                return openFileDialog1.FileName.ToString();
            }
            else
            {
                return "";
            }
        }
        public static DataTable ExcelToDataTable(ExcelDataSource excelDataSource)
        {
            IList list = ((IListSource)excelDataSource).GetList();
            DevExpress.DataAccess.Native.Excel.DataView dataView = (DevExpress.DataAccess.Native.Excel.DataView)list;
            List<PropertyDescriptor> props = dataView.Columns.ToList<PropertyDescriptor>();
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (DevExpress.DataAccess.Native.Excel.ViewRow item in list)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
        public static string GetWorkSheetNameByIndex(int p, string fileName)
        {
            string worksheetName = "";
            using (ISpreadsheetSource spreadsheetSource = SpreadsheetSourceFactory.CreateSource(fileName))
            {
                IWorksheetCollection worksheetCollection = spreadsheetSource.Worksheets;
                worksheetName = worksheetCollection[p].Name;
            }
            return worksheetName;
        }
        #endregion

        public static void ExportGridViewToExcel(GridView grdView, DateTime Ngay, string fileName)
        {
            SaveFileDialog openFD = new SaveFileDialog();
            openFD.Filter = "Excel 2007->2010|*.xlsx";
            openFD.Title = "Save Microsoft Excel Document";
            openFD.InitialDirectory = "D:\\";
            openFD.FileName = fileName + "_" + Ngay.ToString("ddMMyyyyHHmmss");

            if (openFD.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    grdView.ExportToXlsx(openFD.FileName);
                    if (MessageBox.Show("Bạn muốn mở File Excel vừa được xuất?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        System.Diagnostics.Process.Start(openFD.FileName);
                }
                catch
                {
                    MessageBox.Show("Có lỗi phát sinh !","Thông báo", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
            }
        }

        public string LoadThongTinKetNoi()
        {
            if (File.Exists(@"Config\Config.ini"))
            {
                IniParserBUS par = new IniParserBUS(@"Config\Config.ini");

                string[] a = par.GetSetting("CONNECT", "SERVER").Split(',');
                sever = (Convert.ToString(a[0].Replace(" ", "")));

                a = par.GetSetting("CONNECT", "DATABASE").Split(',');
                csdl = (Convert.ToString(a[0].Replace(" ", "")));

                a = par.GetSetting("CONNECT", "USER").Split(',');
                user = (Convert.ToString(a[0].Replace(" ", "")));

                a = par.GetSetting("CONNECT", "PASSWORD").Split(',');
                pass = (Convert.ToString(a[0].Replace(" ", "")));
            }

            return $"Data Source ={sever}; User ID ={user} ; Password ={pass}; Database ={csdl}";
        }
    }
}
