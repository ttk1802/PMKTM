namespace CommonlibHCE
{
    partial class FrmBaoCaoXuatHang
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
            this.ctrPX = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // ctrPX
            // 
            this.ctrPX.ActiveViewIndex = -1;
            this.ctrPX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrPX.Cursor = System.Windows.Forms.Cursors.Default;
            this.ctrPX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrPX.Location = new System.Drawing.Point(0, 0);
            this.ctrPX.Name = "ctrPX";
            this.ctrPX.Size = new System.Drawing.Size(298, 268);
            this.ctrPX.TabIndex = 0;
            // 
            // FrmBaoCaoXuatHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 268);
            this.Controls.Add(this.ctrPX);
            this.Name = "FrmBaoCaoXuatHang";
            this.Text = "In báo cáo xuất hàng";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer ctrPX;
    }
}