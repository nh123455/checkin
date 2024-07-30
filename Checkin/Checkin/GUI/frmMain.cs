using Checkin.BUS;
using Checkin.DTO;
using DevExpress.XtraEditors;
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

namespace Checkin.GUI
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
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
        baseBUS bBUS = new baseBUS();
        userBUS uBUS = new userBUS();
        int lbNameR, lbNameG, lbNameB;
        string fontStyleName;

        public frmMain()
        {
            InitializeComponent();
            this.Load += FrmMain_Load;
        }
        private void DraggControl(bool value)
        {
            ControlExtension.Draggable(picCompany, value);
            ControlExtension.Draggable(lbName, value);
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.KeyUp += FrmMain_KeyUp;
            txtQrcode.KeyUp += TxtQrcode_KeyUp;
            LoadConfig();
            LoadConnectString();
            lbThongBao.Visible = false;
        }
        public void LoadConnectString()
        {
            Properties.Settings.Default.stringConnectSql = bBUS.LoadThongTinKetNoi();
            Properties.Settings.Default.Save();
        }
        public void LoadConfig()
        {
            try
            {
                if (File.Exists(@"Config\Config.ini"))
                {
                    IniParserBUS par = new IniParserBUS(@"Config\Config.ini");

                    string[] a = par.GetSetting("PICTURE", "LOCATION").Split(',');
                    picCompany.Location = new Point(Convert.ToInt32(a[0].Replace(" ", "")), Convert.ToInt32(a[1].Replace(" ", "")));

                    a = par.GetSetting("NAME", "LOCATION").Split(',');
                    lbName.Location = new Point(Convert.ToInt32(a[0].Replace(" ", "")), Convert.ToInt32(a[1].Replace(" ", "")));

                    a = par.GetSetting("NAME", "STYLE").Split(',');
                    fontStyleName = a[0].Trim();

                    a = par.GetSetting("NAME", "R").Split(',');
                    lbNameR = Convert.ToInt32(a[0]);
                    a = par.GetSetting("NAME", "G").Split(',');
                    lbNameG = Convert.ToInt32(a[0]);
                    a = par.GetSetting("NAME", "B").Split(',');
                    lbNameB = Convert.ToInt32(a[0]);

                    if (fontStyleName == "Bold")
                    {
                        a = par.GetSetting("NAME", "FONT").Split(',');
                        lbName.Font = new Font(a[0], Convert.ToInt32(a[1].Replace(" ", "")), FontStyle.Bold);
                    }
                    else if (fontStyleName == "Regular")
                    {
                        a = par.GetSetting("NAME", "FONT").Split(',');
                        lbName.Font = new Font(a[0], Convert.ToInt32(a[1].Replace(" ", "")), FontStyle.Regular);
                    }
                    else if (fontStyleName == "Italic")
                    {
                        a = par.GetSetting("NAME", "FONT").Split(',');
                        lbName.Font = new Font(a[0], Convert.ToInt32(a[1].Replace(" ", "")), FontStyle.Italic);
                    }
                    else if (fontStyleName == "Underline")
                    {
                        a = par.GetSetting("NAME", "FONT").Split(',');
                        lbName.Font = new Font(a[0], Convert.ToInt32(a[1].Replace(" ", "")), FontStyle.Underline);
                    }
                    else
                    {
                        a = par.GetSetting("NAME", "FONT").Split(',');
                        lbName.Font = new Font(a[0], Convert.ToInt32(a[1].Replace(" ", "")), FontStyle.Bold);
                    }
                    lbName.ForeColor = Color.FromArgb(lbNameR, lbNameG, lbNameB);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            txtQrcode.Focus();
        }

        private void TxtQrcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ResetControl();
                if(txtQrcode.Text.Trim().Length > 0)
                {
                    string qrcode = txtQrcode.Text.Trim();
                    txtQrcode.ResetText();
                    lbThongBao.Visible = false;

                    try
                    {
                        List<userDTO> user = uBUS.GetUserByQrcode(qrcode, Properties.Settings.Default.stringConnectSql);
                        if(user.Count >= 1)
                        {
                            foreach(var item in user)
                            {
                                lbName.Text = item.name;
                            }
                            if (String.IsNullOrEmpty(Properties.Settings.Default.stringConnectSql)) LoadConnectString();
                            uBUS.Checkin(qrcode, Properties.Settings.Default.stringConnectSql);
                        }    
                        else
                        {
                            lbThongBao.Visible = true;
                            ResetControl();
                        }    
                    }
                    catch
                    {

                    }
                }    
            }
        }

        public void ResetControl()
        {
            lbName.Text = "";
        }

        private void FrmMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn thoát Chương trình?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                Application.Exit();
            }
            else if (e.KeyCode == Keys.F3)
            {
                frmSupport frm = new frmSupport(this);
                frm.ShowDialog();
            }
            else if (e.KeyCode == Keys.F6)
            {
                if(MessageBox.Show("Thay đổi vị trí ?","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DraggControl(true);
                }    
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                if (File.Exists(@"Config\Config.ini"))
                {
                    IniParserBUS par = new IniParserBUS(@"Config\Config.ini");
                    par.AddSetting("PICTURE", "LOCATION", picCompany.Location.X + "," + picCompany.Location.Y);
                    par.AddSetting("NAME", "LOCATION", lbName.Location.X + "," + lbName.Location.Y);
                    par.SaveSettings();
                    DraggControl(false);
                    MessageBox.Show("Lưu vị trí thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}