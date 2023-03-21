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
    public partial class FrmDMHH : DevExpress.XtraEditors.XtraForm
    {
        public FrmDMHH()
        {
            InitializeComponent();
        }
        public static int index = -1;
        public static int x;
        static string strFormState;
        public static string value = null;
        private void FrmHH_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnDung.Enabled = false;
            ShowAllHangHoa();
            strFormState = "NORMAL";
            splitContainer1.Panel1Collapsed = true;
        }
        private void ShowAllHangHoa()
        {
            string query = "select * from dbo.tDanhMucHangHoa";
            ConnectSql.GetDataToTable1(query, "dbo.tDanhMucHangHoa");
            dgvHH.DataSource = ConnectSql.ds.Tables["dbo.tDanhMucHangHoa"];
            ChangColumn();
        }
      
        private void ChangColumn()
        {
            dgvHH.Columns[0].HeaderText = "Mã Hàng";
            dgvHH.Columns[1].HeaderText = "Tên hàng";
            dgvHH.Columns[2].HeaderText = "Nhóm hàng";
            dgvHH.Columns[3].HeaderText = "Đơn vị tính";
            dgvHH.Columns[4].HeaderText = "Số lượng tồn đầu";
            dgvHH.Columns[5].HeaderText = "Thành tiền tồn đầu";
            dgvHH.Columns[6].HeaderText = "Ngày tồn đầu";
        }
        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splitContainer1.Panel1Collapsed = false;
            btnThoat.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnIn.Enabled = false;
            btnXoa.Enabled = false;
            strFormState = "ADDING";

            btnLuu.Enabled = true;
            btnDung.Enabled = true;
            ConnectSql.XoaNoiDung(groupControl1);
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult warn = MessageBox.Show("Are you sure ?", "Warning!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (warn == DialogResult.Yes)
            {
                DataRow row = ConnectSql.ds.Tables["dbo.tDanhMucHangHoa"].Rows[ClassApp.vt];
                row.Delete();
                int rs = ConnectSql.adapter.Update(ConnectSql.ds.Tables["dbo.tDanhMucHangHoa"]);
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
            btnSua.Enabled = false;
            btnThem.Enabled = false;
            btnIn.Enabled = false;
            btnXoa.Enabled = false;
            strFormState = "EDITTING";

            btnLuu.Enabled = true;
            btnDung.Enabled = true;
        }
       
        private bool textEmpty()
        {
            float sl;
            float tt;
            if (txtMahh.Text == "") { errorProvider1.SetError(txtMahh, "Yêu cầu nhập"); return false; }
            else { errorProvider1.Clear();  }
            if (!float.TryParse(txtSltd.Text, out sl))
            {
                errorProvider1.SetError(txtSltd, "Số lượng phải là số");
                return false;
            }
            else { errorProvider1.Clear(); }
            if (!float.TryParse(txtTttd.Text, out tt))
            {
                errorProvider1.SetError(txtTttd, "Thành tiền phải là số");
                return false;
            }
            else { errorProvider1.Clear(); }
            if (txtNtd.Text == maskedTextBox1.Text)
            {
                errorProvider1.SetError(txtNtd, "Yêu cầu nhập");
                return false;
            }
            else { errorProvider1.Clear(); }
            return true;
        }
        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (strFormState == "ADDING" && textEmpty())
            {
                string NTD; 
                NTD = Convert.ToDateTime(txtNtd.Text).ToString("yyyy-MM-dd");
                DataRow row = ConnectSql.ds.Tables["dbo.tDanhMucHangHoa"].NewRow();
                row["cMaHang"] = txtMahh.Text.Trim();
                row["cTenHang"] = txtTenhh.Text.Trim();
                row["cNhomHang"] = txtnh.Text.Trim();
                row["cDonViTinh"] = txtdvt.Text.Trim();
                row["nSoLuongTonDau"] = txtSltd.Text.Trim();
                row["nThanhTienTonDau"] = txtTttd.Text.Trim();
                row["dNgayTonDau"] = NTD;
                try
                {
                    ConnectSql.ds.Tables["dbo.tDanhMucHangHoa"].Rows.Add(row);
                    int rs = ConnectSql.adapter.Update(ConnectSql.ds.Tables["dbo.tDanhMucHangHoa"]);
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
                    ShowAllHangHoa();
                }
                catch (Exception)
                {
                    MessageBox.Show("Trùng mã hàng hóa");
                    ShowAllHangHoa();

                }
            }
            else if (strFormState == "EDITTING" && textEmpty())
            {
                DataRow row = ConnectSql.ds.Tables["dbo.tDanhMucHangHoa"].Rows[ClassApp.vt];
                row.BeginEdit();
                string NTD;
                NTD = Convert.ToDateTime(txtNtd.Text).ToString("yyyy-MM-dd");
                row["cMaHang"] = txtMahh.Text.Trim();
                row["cTenHang"] = txtTenhh.Text.Trim();
                row["cNhomHang"] = txtnh.Text.Trim();
                row["cDonViTinh"] = txtdvt.Text.Trim();
                row["nSoLuongTonDau"] = txtSltd.Text.Trim();
                row["nThanhTienTonDau"] = txtTttd.Text.Trim();
                row["dNgayTonDau"] = NTD;

                row.EndEdit();
                try
                {

                    int rs = ConnectSql.adapter.Update(ConnectSql.ds.Tables["dbo.tDanhMucHangHoa"]);
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
                    ShowAllHangHoa();
                }
                catch (Exception)
                {
                    MessageBox.Show("Trùng mã hàng hóa");
                    ShowAllHangHoa();

                }
            }
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
        private void textboxLoad()
        {
            ClassApp.vt = index;
            DataRow row = ConnectSql.ds.Tables["dbo.tDanhMucHangHoa"].Rows[ClassApp.vt];
            txtMahh.Text = row["cMaHang"].ToString();
            txtTenhh.Text = row["cTenHang"].ToString();
            txtnh.Text = row["cNhomHang"].ToString();
            txtdvt.Text = row["cDonViTinh"].ToString();
            txtSltd.Text = row["nSoLuongTonDau"].ToString();
            txtTttd.Text = row["nThanhTienTonDau"].ToString();
            dgvHH.Rows[ClassApp.vt].Selected = true;
            txtNtd.Text = row["dNgayTonDau"].ToString();
        }
        private void btnSQL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmTimKiemHH frm = new FrmTimKiemHH();
            frm.ShowDialog();
            splitContainer1.Panel1Collapsed = false;
            if (index > -1)
            {
                dgvHH.Rows[index].Cells[0].Selected = true;
                while (dgvHH.Rows[x].Cells[0].Value.ToString() != value)
                {
                    x++;
                }
                {
                    index = x;
                    textboxLoad();
                }
            }
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmLocKH frm = new FrmLocKH();
            frm.ShowDialog();
        }

        private void dgvHH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ClassApp.vt = e.RowIndex;
                if (ClassApp.vt == -1 || ClassApp.vt == ConnectSql.ds.Tables["dbo.tDanhMucHangHoa"].Rows.Count) return;
                DataRow row = ConnectSql.ds.Tables["dbo.tDanhMucHangHoa"].Rows[ClassApp.vt];
                txtMahh.Text = row["cMaHang"].ToString();
                txtTenhh.Text = row["cTenHang"].ToString();
                txtnh.Text = row["cNhomHang"].ToString();
                txtdvt.Text = row["cDonViTinh"].ToString();
                txtSltd.Text = row["nSoLuongTonDau"].ToString();
                txtTttd.Text = row["nThanhTienTonDau"].ToString();
                txtNtd.Text = row["dNgayTonDau"].ToString();
            }
            catch (Exception)
            {
                return;
            }
         
           
        }

        private void btnAnTT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splitContainer1.Panel1Collapsed = true;
            btnAnTT.Enabled = false;
        }

        private void dgvHH_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            splitContainer1.Panel1Collapsed = false;
            btnAnTT.Enabled = true;
        }

        private void FrmDMHH_Activated(object sender, EventArgs e)
        {
            ShowAllHangHoa();
        }
    }
}