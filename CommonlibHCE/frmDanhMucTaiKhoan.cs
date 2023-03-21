using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CommonlibHCE
{
    public partial class frmDanhMucTaiKhoan : DevExpress.XtraEditors.XtraForm
    {
        public frmDanhMucTaiKhoan()
        {
            InitializeComponent();
        }
        static string strFormState;
        private void FrmDMTK_Load(object sender, EventArgs e)
        {
          
            splitContainer1.Panel1Collapsed = true;
            btnLuu.Enabled = false;
            btnDung.Enabled = false;
            btnAnTT.Enabled = false;
            ShowAllTaiKhoan();
            strFormState = "NORMAL";
        }
        private void ShowAllTaiKhoan()
        {
            string query = "select * from dbo.tDanhMucTaiKhoan where bCoDinhKhoan = 1";
            ConnectSql.GetDataToTable1(query, "dbo.tDanhMucTaiKhoan");
            dgvKT.DataSource = ConnectSql.ds.Tables["dbo.tDanhMucTaiKhoan"];
            ChangColumn();
        }
        
        private void ChangColumn()
        {
            dgvKT.Columns[0].HeaderText = "Tài Khoản";
            dgvKT.Columns[1].HeaderText = "Tên tài khoản";
            dgvKT.Columns[2].HeaderText = "Số dư nợ đầu";
            dgvKT.Columns[3].HeaderText = "Số dư có đầu";
            dgvKT.Columns[4].HeaderText = "Có định khoản";
            dgvKT.Columns[5].HeaderText = "Cấp";
            dgvKT.Columns[6].HeaderText = "Ngày số dư";
        }
        private void dgvKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            try
            {
            ClassApp.vt = e.RowIndex;
                
            if (ClassApp.vt == -1 || ClassApp.vt == ConnectSql.ds.Tables["dbo.tDanhMucTaiKhoan"].Rows.Count) return;
              
            DataRow row = ConnectSql.ds.Tables["dbo.tDanhMucTaiKhoan"].Rows[ClassApp.vt];
            txtMatk.Text = row["cTaiKhoan"].ToString();
            txtTentk.Text = row["cTenTaiKhoan"].ToString();
            txtSdnd.Text = row["nSoDuNoDau"].ToString();
            txtSdcd.Text = row["nSoDuCoDau"].ToString();
            cbCDK.Checked = bool.Parse(row["bCoDinhKhoan"].ToString());
            txtCap.Text = row["cCap"].ToString();
            txtNsd.Text = row["dNgaySoDu"].ToString();
          
            }
            catch (Exception)
            {
             
                return;
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splitContainer1.Panel1Collapsed = false;
            ConnectSql.XoaNoiDung(groupControl1);
            btnSQL.Enabled = false;
            btnThoat.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnIn.Enabled = false;
            btnXoa.Enabled = false;
            btnSQL.Enabled = false;
            strFormState = "ADDING";

            btnLuu.Enabled = true;
            btnDung.Enabled = true;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult warn = MessageBox.Show("Are you sure ?", "Warning!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (warn == DialogResult.Yes)
            {
                DataRow row = ConnectSql.ds.Tables["dbo.tDanhMucTaiKhoan"].Rows[ClassApp.vt];
                row.Delete();
                int rs = ConnectSql.adapter.Update(ConnectSql.ds.Tables["dbo.tDanhMucTaiKhoan"]);
                if (rs > 0)
                {
                    MessageBox.Show("Xóa dữ liệu thành công!");
                    ConnectSql.XoaNoiDung(this);
                }
                else
                {
                    MessageBox.Show("Xóa dữ liệu thất bại!");
                }
            }
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splitContainer1.Panel1Collapsed = false;
            btnThoat.Enabled = false;
            btnSQL.Enabled = false;
            btnSua.Enabled = false;
            btnThem.Enabled = false;
            btnIn.Enabled = false;
            btnXoa.Enabled = false;
            btnSQL.Enabled = false;
            strFormState = "EDITTING";

            btnLuu.Enabled = true;
            btnDung.Enabled = true;
        }
        private bool textEmpty()
        {
            float SDND;
            float SDCD;
            if (txtMatk.Text == "") { errorProvider1.SetError(txtMatk, "Yêu cầu nhập"); return false; }
            else { errorProvider1.Clear(); }
            if (!float.TryParse(txtSdnd.Text, out SDND))
            {
                errorProvider1.SetError(txtSdnd, "Số lượng phải là số");
                return false;
            }
            else { errorProvider1.Clear(); }
            if (!float.TryParse(txtSdcd.Text, out SDCD))
            {
                errorProvider1.SetError(txtSdcd, "Thành tiền phải là số");
                return false;
            }
            else { errorProvider1.Clear(); }
            if (txtNsd.Text == maskedTextBox1.Text)
            {
                errorProvider1.SetError(txtNsd, "Yêu cầu nhập");
                return false;
            }
            else { errorProvider1.Clear(); }
            return true;
        }
        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (strFormState == "ADDING" && textEmpty())
            {
                string NSD;
                NSD = Convert.ToDateTime(txtNsd.Text).ToString("yyyy-MM-dd");
                DataRow row = ConnectSql.ds.Tables["dbo.tDanhMucTaiKhoan"].NewRow();
                row["cTaiKhoan"] = txtMatk.Text.Trim();
                row["cTenTaiKhoan"] = txtTentk.Text.Trim();
                row["nSoDuNoDau"] = txtSdnd.Text.Trim();
                row["nSoDuCoDau"] = txtSdcd.Text.Trim();
                row["bCoDinhKhoan"] = cbCDK.Checked;
                row["cCap"] = txtCap.Text.Trim();
                row["dNgaySoDu"] = NSD;

                try
                {
                    ConnectSql.ds.Tables["dbo.tDanhMucTaiKhoan"].Rows.Add(row);
                    int rs = ConnectSql.adapter.Update(ConnectSql.ds.Tables["dbo.tDanhMucTaiKhoan"]);
                    if (rs > 0)
                    {
                        MessageBox.Show("Thêm dữ liệu thành công!");
                        ConnectSql.XoaNoiDung(this);
                        btnDung.PerformClick();
                    }

                    else
                    {
                        MessageBox.Show("Thêm dữ liệu thất bại!");
                    }
                    ShowAllTaiKhoan();
                }
                catch (Exception)
                {
                    MessageBox.Show("Trùng mã Khách hàng");
                    ShowAllTaiKhoan();

                }
            }
            else if (strFormState == "EDITTING" && textEmpty())
            {
                string NSD;
                NSD = Convert.ToDateTime(txtNsd.Text).ToString("yyyy-MM-dd");
                DataRow row = ConnectSql.ds.Tables["dbo.tDanhMucTaiKhoan"].Rows[ClassApp.vt];
                row.BeginEdit();
                row["cTaiKhoan"] = txtMatk.Text.Trim();
                row["cTenTaiKhoan"] = txtTentk.Text.Trim();
                row["nSoDuNoDau"] = txtSdnd.Text.Trim();
                row["nSoDuCoDau"] = txtSdcd.Text.Trim();
                row["bCoDinhKhoan"] = cbCDK.Checked;
                row["cCap"] = txtCap.Text.Trim();
                row["dNgaySoDu"] = NSD;
                row.EndEdit();
                try
                {

                    int rs = ConnectSql.adapter.Update(ConnectSql.ds.Tables["dbo.tDanhMucTaiKhoan"]);
                    if (rs > 0)
                    {
                        MessageBox.Show("Chỉnh sửa liệu thành công!!");
                        ConnectSql.XoaNoiDung(this);
                        btnDung.PerformClick();
                    }

                    else
                    {
                        MessageBox.Show("Chỉnh sửa dữ liệu thất bại!i!");
                    }
                    ShowAllTaiKhoan();
                }
                catch (Exception)
                {
                    MessageBox.Show("Trùng mã Khách hàng");
                    ShowAllTaiKhoan();

                }
            }
        }

        private void btnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnDung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            errorProvider1.Clear();
            btnThoat.Enabled = true;
            btnDung.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnIn.Enabled = true;
            btnXoa.Enabled = true;
            btnSQL.Enabled = true;
            btnLuu.Enabled = false;
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmLocKH frm = new FrmLocKH();
            frm.ShowDialog();
        }

        private void dgvKT_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            splitContainer1.Panel1Collapsed = false;
            btnAnTT.Enabled = true;
        }

        private void btnAnTT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splitContainer1.Panel1Collapsed = true;
            btnAnTT.Enabled = false;
        }
        public static int x;
        public static int index = -1;
        public static string value = null;
        private void textboxLoad()
        {
            ClassApp.vt = index;
            DataRow row = ConnectSql.ds.Tables["dbo.tDanhMucTaiKhoan"].Rows[ClassApp.vt];          
            txtMatk.Text = row["cTaiKhoan"].ToString();
            txtTentk.Text = row["cTenTaiKhoan"].ToString();
            txtSdnd.Text = row["nSoDuNoDau"].ToString();
            txtSdcd.Text = row["nSoDuCoDau"].ToString();
            cbCDK.Checked = bool.Parse(row["bCoDinhKhoan"].ToString());
            txtCap.Text = row["cCap"].ToString();
            txtNsd.Text = row["dNgaySoDu"].ToString();
            dgvKT.Rows[ClassApp.vt].Selected = true;
        }
        private void btnSQL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmTimKiemTK frm = new FrmTimKiemTK();
            frm.ShowDialog();
            splitContainer1.Panel1Collapsed = false;
            if (index > -1)
            {
                dgvKT.Rows[index].Cells[0].Selected = true;
                while (dgvKT.Rows[x].Cells[0].Value.ToString() != value)
                {
                    x++;
                }
                {
                    index = x;
                    textboxLoad();
                }
            }
        }

        private void FrmDMTK_Activated(object sender, EventArgs e)
        {
            ShowAllTaiKhoan();
        }
    }
}