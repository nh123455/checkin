using Checkin.BUS;
using Checkin.DTO;
using Checkin.GUI.CustomUserControl;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkin.GUI
{
    public partial class frmSupport : DevExpress.XtraEditors.XtraForm
    {
        Ping ping = new Ping();
        private readonly frmMain frmck;
        userBUS usBUS = new userBUS();
        baseBUS bBUS = new baseBUS();
        string SizeName;
        protected override CreateParams CreateParams
        {
            #region Khử giật khi Form Load
            get
            {
                CreateParams handleparam = base.CreateParams;
                handleparam.ExStyle |= 0x02000000;
                return handleparam;
            }
            #endregion
        }
        public frmSupport(frmMain frm)
        {
            frmck = frm;
            InitializeComponent();
            this.Load += FrmSupport_Load;
        }
        private void LoadConfig()
        {
            try
            {
                if (File.Exists(@"Config\Config.ini"))
                {
                    IniParserBUS par = new IniParserBUS(@"Config\Config.ini");

                    string[] a = par.GetSetting("CONNECT", "SERVER").Split(',');
                    txtServer.Text = Convert.ToString(a[0].Replace(" ", ""));

                    a = par.GetSetting("CONNECT", "DATABASE").Split(',');
                    txtDatabase.Text = Convert.ToString(a[0].Replace(" ", ""));

                    a = par.GetSetting("CONNECT", "USER").Split(',');
                    txtUser.Text = Convert.ToString(a[0].Replace(" ", ""));

                    a = par.GetSetting("CONNECT", "PASSWORD").Split(',');
                    txtPassword.Text = Convert.ToString(a[0].Replace(" ", ""));                 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FrmSupport_Load(object sender, EventArgs e)
        {
            //Button tab connect
            btnGetIp.Click += BtnGetIp_Click;
            btnPing.Click += BtnPing_Click;
            btnSaveConnect.Click += BtnSaveConnect_Click;

            //Button tab Checkin
            btnListCheckin.Click += BtnListCheckin_Click;
            btnDetail.Click += BtnDetail_Click;
            btnExportReport.Click += BtnExportReport_Click;

            //Button tab Data
            btnFile.Click += BtnFile_Click;
            btnInsertData.Click += BtnInsertData_Click;
            btnResetCheckin.Click += BtnResetCheckin_Click;
            btnDeleteDataCheckin.Click += BtnDeleteDataCheckin_Click;

            //Button tab Config
            btnColorName.Click += BtnColorName_Click;
            btnFontName.Click += BtnFontName_Click;
            btnSaveCf.Click += BtnSaveCf_Click;

            gvCheckin.DoubleClick += GvCheckin_DoubleClick;
            gvCheckin.CustomDrawRowIndicator += GvCheckin_CustomDrawRowIndicator;
            LoadConfig();
        }

        private void BtnDeleteDataCheckin_Click(object sender, EventArgs e)
        {
            if (usBUS.DeleteData(bBUS.LoadThongTinKetNoi()) == true)
            {
                MessageBox.Show("Xóa data checkin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Xóa data checkin không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnResetCheckin_Click(object sender, EventArgs e)
        {
            if(usBUS.ResetCheckin(bBUS.LoadThongTinKetNoi()) == true)
            {
                MessageBox.Show("Reset checkin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }    
            else
            {
                MessageBox.Show("Reset checkin không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }    
            
        }

        private void GvCheckin_DoubleClick(object sender, EventArgs e)
        {
            userDTO user = gvCheckin.GetFocusedRow() as userDTO;
            if (user == null) return;
            if(MessageBox.Show($"Bạn muốn checkin cho: {user.name}", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if(usBUS.Checkin(user.qrcode, Properties.Settings.Default.stringConnectSql) == true)
                {
                    MessageBox.Show("Checkin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }    
                
            }    
        }

        private void GvCheckin_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            GridViewHelper.GridView_CustomDrawRowIndicator(sender, e, gcCheckin, gvCheckin);
        }

        private void BtnSaveCf_Click(object sender, EventArgs e)
        {
            if (frmck != null)
            {
                frmck.LoadConfig();
            }
            MessageBox.Show("Lưu cấu hình thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnFontName_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult fontsQrcode = fontDialog1.ShowDialog();
                if (fontsQrcode == DialogResult.OK)
                {
                    lbMau.Font = new Font(fontDialog1.Font.Name, fontDialog1.Font.Size, fontDialog1.Font.Style);

                    if (lbMau.Font.Size.ToString().Contains('.'))
                    {
                        int index = lbMau.Font.Size.ToString().IndexOf('.');
                        string result = lbMau.Font.Size.ToString().Substring(0, index);
                        SizeName = result;
                    }
                    else
                        SizeName = lbMau.Font.Size.ToString();
                }
                if (String.IsNullOrEmpty(lbMau.Font.Name.ToString()) || SizeName == null || String.IsNullOrEmpty(lbMau.Font.Style.ToString()))
                {
                    return;
                }
                else
                {
                    if (File.Exists(@"Config\Config.ini"))
                    {
                        IniParserBUS par = new IniParserBUS(@"Config\Config.ini");
                        par.AddSetting("NAME", "FONT", lbMau.Font.Name.ToString() + "," + SizeName);
                        par.AddSetting("NAME", "STYLE", lbMau.Font.Style.ToString());
                        par.SaveSettings();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Thay đổi Font không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnColorName_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult colorsName = colorDialog1.ShowDialog();
                if (colorsName == DialogResult.OK)
                {
                    lbMau.ForeColor = colorDialog1.Color;
                }
                if (File.Exists(@"Config\Config.ini"))
                {
                    if (String.IsNullOrEmpty(lbMau.ForeColor.R.ToString()) || String.IsNullOrEmpty(lbMau.ForeColor.G.ToString()) || String.IsNullOrEmpty(lbMau.ForeColor.B.ToString()))
                    {
                        return;
                    }
                    else
                    {
                        IniParserBUS par = new IniParserBUS(@"Config\Config.ini");
                        par.AddSetting("NAME", "R", lbMau.ForeColor.R.ToString());
                        par.AddSetting("NAME", "G", lbMau.ForeColor.G.ToString());
                        par.AddSetting("NAME", "B", lbMau.ForeColor.B.ToString());
                        par.SaveSettings();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Thay đổi màu không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnInsertData_Click(object sender, EventArgs e)
        {
            List<userDTO> list = new List<userDTO>();
            for (int i = 0; i < gvDataRaw.RowCount; i++)
            {
                userDTO user = new userDTO();

                user.qrcode = gvDataRaw.GetRowCellValue(i, gvDataRaw.Columns[0])?.ToString() ?? string.Empty;
                user.name = gvDataRaw.GetRowCellValue(i, gvDataRaw.Columns[1])?.ToString() ?? string.Empty;
                user.email = gvDataRaw.GetRowCellValue(i, gvDataRaw.Columns[2])?.ToString() ?? string.Empty;
                user.phone = gvDataRaw.GetRowCellValue(i, gvDataRaw.Columns[3])?.ToString() ?? string.Empty;
                user.company = gvDataRaw.GetRowCellValue(i, gvDataRaw.Columns[4])?.ToString() ?? string.Empty;
                user.type = 0;

                list.Add(user);
            }

            if(usBUS.InsertUser(list, bBUS.LoadThongTinKetNoi()) == true)
            {
                MessageBox.Show("Lưu dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }    
            else
            {
                MessageBox.Show("Lưu dữ liệu không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }    
        }

        private void BtnFile_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowDefaultWaitForm("Đang nhận dữ liệu từ file Excel", "Loading...");
                DataTable dt = BUS.baseBUS.GetExcelFileDataSource(this.Size.Width, this.Size.Height);
                int x = dt.Rows.Count;
                //bindingRawData.DataSource = dt;
                gcDataRaw.DataSource = dt;
                gvDataRaw.BestFitColumns();
                gvDataRaw.OptionsBehavior.ReadOnly = true;
                SplashScreenManager.CloseDefaultWaitForm();
                MessageBox.Show($"Đã nhận {x.ToString()}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                SplashScreenManager.CloseDefaultWaitForm();
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExportReport_Click(object sender, EventArgs e)
        {
            try
            {
                BUS.baseBUS.ExportGridViewToExcel(gvCheckin, DateTime.Now, "BaoCaoCheckin");
            }
            catch
            {
                MessageBox.Show("Xuất file báo cáo không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDetail_Click(object sender, EventArgs e)
        {
            try
            {
                gcCheckin.DataSource = null;
                gcCheckin.Refresh();
                gvCheckin.RefreshData();

                gcCheckin.DataSource = usBUS.GetUserDetail(bBUS.LoadThongTinKetNoi());

                gvCheckin.Columns[5].Visible = true;
                gvCheckin.Columns[6].Visible = true;

                gvCheckin.Columns[2].Visible = false;
                gvCheckin.Columns[3].Visible = false;
                gvCheckin.Columns[4].Visible = false;
                gvCheckin.Columns[5].Visible = false;

                GridColumn scanTimeColumn = gvCheckin.Columns["scan_time"];
                scanTimeColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                scanTimeColumn.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void BtnListCheckin_Click(object sender, EventArgs e)
        {
            try
            {
                gcCheckin.DataSource = null;
                gcCheckin.Refresh();
                gvCheckin.RefreshData();

                gcCheckin.DataSource = usBUS.GetUser(bBUS.LoadThongTinKetNoi());

                gvCheckin.Columns[2].Visible = true;
                gvCheckin.Columns[3].Visible = true;
                gvCheckin.Columns[4].Visible = true;
                gvCheckin.Columns[5].Visible = true;

                gvCheckin.Columns[5].Visible = false;
                gvCheckin.Columns[6].Visible = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }          
        }

        private void BtnSaveConnect_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtServer.Text))
            {
                MessageBox.Show("Server bắt buộc nhập", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (String.IsNullOrEmpty(txtDatabase.Text))
            {
                MessageBox.Show("Tên CSDL bắt buộc nhập", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (String.IsNullOrEmpty(txtUser.Text))
            {
                MessageBox.Show("Người dùng bắt buộc nhập", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (String.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Mật khẩu bắt buộc nhập", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (File.Exists(@"Config\Config.ini"))
                {
                    IniParserBUS par = new IniParserBUS(@"Config\Config.ini");
                    par.AddSetting("CONNECT", "SERVER", txtServer.Text.Trim());
                    par.AddSetting("CONNECT", "DATABASE", txtDatabase.Text.Trim());
                    par.AddSetting("CONNECT", "USER", txtUser.Text.Trim());
                    par.AddSetting("CONNECT", "PASSWORD", txtPassword.Text.Trim());

                    par.SaveSettings();

                    if (frmck != null)
                    {
                        frmck.LoadConnectString();
                    }                   
                }
                MessageBox.Show("Lưu thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPing_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowDefaultWaitForm("Kết nối đến Server", "Đang kết nối...");
                String str = $@"Data Source={txtServer.Text.Trim()};Initial Catalog={txtDatabase.Text.Trim()};
                                Persist Security Info=True; Connection Timeout=5;
                                User ID={txtUser.Text.Trim()};Password={txtPassword.Text.Trim()}";

                PingReply reply = ping.Send(txtServer.Text.Trim(), 5000);
                if (reply.Status.ToString() == "Success")
                {
                    SqlConnection con = new SqlConnection(str);
                    con.Open();
                    MessageBox.Show("Kết nối thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    SplashScreenManager.CloseDefaultWaitForm();
                }
                else
                {
                    SplashScreenManager.CloseDefaultWaitForm();
                    MessageBox.Show("Kết nối không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                SplashScreenManager.CloseDefaultWaitForm();
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGetIp_Click(object sender, EventArgs e)
        {
            #region Lấy Ip máy
            try
            {
                IPHostEntry host;
                string localIP = "?";
                host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily.ToString() == "InterNetwork")
                    {
                        localIP = ip.ToString();
                        txtServer.Text = localIP;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion
        }
    }
}