namespace CommonlibHCE
{
    partial class FrmBaoCaoNhap
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
            this.ctrPN = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // ctrPN
            // 
            this.ctrPN.ActiveViewIndex = -1;
            this.ctrPN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrPN.Cursor = System.Windows.Forms.Cursors.Default;
            this.ctrPN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrPN.Location = new System.Drawing.Point(0, 0);
            this.ctrPN.Name = "ctrPN";
            this.ctrPN.Size = new System.Drawing.Size(502, 268);
            this.ctrPN.TabIndex = 0;
            // 
            // FrmBaoCaoNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 268);
            this.Controls.Add(this.ctrPN);
            this.Name = "FrmBaoCaoNhap";
            this.Text = "In báo cáo nhập hàng";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer ctrPN;
    }
}