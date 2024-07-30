
namespace Checkin.GUI
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.txtQrcode = new DevExpress.XtraEditors.TextEdit();
            this.picCompany = new System.Windows.Forms.PictureBox();
            this.lbName = new DevExpress.XtraEditors.LabelControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbThongBao = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtQrcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCompany)).BeginInit();
            this.SuspendLayout();
            // 
            // txtQrcode
            // 
            this.txtQrcode.Location = new System.Drawing.Point(0, 0);
            this.txtQrcode.Name = "txtQrcode";
            this.txtQrcode.Properties.AutoHeight = false;
            this.txtQrcode.Size = new System.Drawing.Size(411, 0);
            this.txtQrcode.TabIndex = 0;
            // 
            // picCompany
            // 
            this.picCompany.BackColor = System.Drawing.Color.Transparent;
            this.picCompany.Image = ((System.Drawing.Image)(resources.GetObject("picCompany.Image")));
            this.picCompany.Location = new System.Drawing.Point(12, 12);
            this.picCompany.Name = "picCompany";
            this.picCompany.Size = new System.Drawing.Size(470, 289);
            this.picCompany.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCompany.TabIndex = 2;
            this.picCompany.TabStop = false;
            // 
            // lbName
            // 
            this.lbName.Appearance.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.Appearance.ForeColor = System.Drawing.Color.White;
            this.lbName.Appearance.Options.UseFont = true;
            this.lbName.Appearance.Options.UseForeColor = true;
            this.lbName.Appearance.Options.UseTextOptions = true;
            this.lbName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lbName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbName.Location = new System.Drawing.Point(12, 560);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(1769, 119);
            this.lbName.TabIndex = 4;
            this.lbName.Text = "Trần Đình Toàn";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbThongBao
            // 
            this.lbThongBao.Appearance.Font = new System.Drawing.Font("Tahoma", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbThongBao.Appearance.ForeColor = System.Drawing.Color.White;
            this.lbThongBao.Appearance.Options.UseFont = true;
            this.lbThongBao.Appearance.Options.UseForeColor = true;
            this.lbThongBao.Appearance.Options.UseTextOptions = true;
            this.lbThongBao.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lbThongBao.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbThongBao.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbThongBao.Location = new System.Drawing.Point(0, 668);
            this.lbThongBao.Name = "lbThongBao";
            this.lbThongBao.Size = new System.Drawing.Size(1790, 60);
            this.lbThongBao.TabIndex = 5;
            this.lbThongBao.Text = "Mã Qrcode không chính xác!";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1790, 728);
            this.Controls.Add(this.lbThongBao);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.picCompany);
            this.Controls.Add(this.txtQrcode);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.txtQrcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCompany)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtQrcode;
        private System.Windows.Forms.PictureBox picCompany;
        private DevExpress.XtraEditors.LabelControl lbName;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.LabelControl lbThongBao;
    }
}