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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace CommonlibHCE
{
    public partial class FrmDMKH : DevExpress.XtraEditors.XtraForm
    {
        public static int index = -1;
        public static string value = null;
        public FrmDMKH()
        {
            InitializeComponent();
        }
        static string strFormState;
        private void FrmDMKH_Load(object sender, EventArgs e)
        {
           
            btnLuu.Enabled = false;
            btnDung.Enabled = false;
            ShowAllKhachHang();
          
            strFormState = "NORMAL";
            splitContainer1.Panel1Collapsed = true;
            btnAnTT.Enabled = false;
        }
        private void ShowAllKhachHang()
        {
            string query = "SELECT * FROM tdanhmuckhachhang";
            ConnectSql.GetDataToTable1(query, "dbo.tDanhMucKhachHang");
            dgvKH.DataSource = ConnectSql.ds.Tables["dbo.tDanhMucKhachHang"];
            ChangColumn();
            dgvKH.ClearSelection();
        }
      
        private void ChangColumn()
        {
            dgvKH.Columns[0].HeaderText = "Mã khách hàng";
            dgvKH.Columns[1].HeaderText = "Tên khách hàng";
            dgvKH.Columns[2].HeaderText = "Mã số thuế";
            dgvKH.Columns[3].HeaderText = "Địa chỉ";
            dgvKH.Columns[4].HeaderText = "Tỉnh thành phố";
            dgvKH.Columns[5].HeaderText = "Điện thoại";
            dgvKH.Columns[6].HeaderText = "Fax";
        }
        private void dgvKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               
                ClassApp.vt = e.RowIndex;
                if (ClassApp.vt == -1 || ClassApp.vt == ConnectSql.ds.Tables["dbo.tDanhMucKhachHang"].Rows.Count) return;
                DataRow row = ConnectSql.ds.Tables["dbo.tDanhMucKhachHang"].Rows[ClassApp.vt];
                txtMakh.Text = row["cMaKhachHang"].ToString();
                txtTenkh.Text = row["cTenKhachHang"].ToString();
                txtmst.Text = row["cMaSoThue"].ToString();
                txtTTP.Text = row["cDiaChi"].ToString();
                txtTT.Text = row["cTinhThanhPho"].ToString();
                txtDT.Text = row["cDienThoai"].ToString();
                txtFax.Text = row["cFax"].ToString();
            }
            catch (Exception)
            {
                return;
            }
           

           
        }
      
      
        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splitContainer1.Panel1Collapsed = false;
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
            btnAnTT.Enabled = false;

            ConnectSql.XoaNoiDung(groupControl1);
        }

        private void btnXoa_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult warn = MessageBox.Show("Are you sure ?", "Warning!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (warn == DialogResult.Yes)
            {
                DataRow row = ConnectSql.ds.Tables["dbo.tDanhMucKhachHang"].Rows[ClassApp.vt];
                row.Delete();
                int rs = ConnectSql.adapter.Update(ConnectSql.ds.Tables["dbo.tDanhMucKhachHang"]);
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

        private void btnSua_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
            btnAnTT.Enabled = false;
        }
        private bool textEmpty()
        {
            if (txtMakh.Text == "") { errorProvider1.SetError(txtMakh, "Yêu cầu nhập"); return false; }
            else { errorProvider1.Clear(); }
            return true;
        }
        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (strFormState == "ADDING" && textEmpty())
            {
                DataRow row = ConnectSql.ds.Tables["dbo.tDanhMucKhachHang"].NewRow();
                row["cMaKhachHang"] = txtMakh.Text.Trim();
                row["cTenKhachHang"] = txtTenkh.Text.Trim();
                row["cMaSoThue"] = txtmst.Text.Trim();
                row["cDiaChi"] = txtTTP.Text.Trim();
                row["cTinhThanhPho"] = txtTT.Text.Trim();
                row["cDienThoai"] = txtDT.Text.Trim();
                row["cFax"] = txtFax.Text.Trim();

                try
                {
                    ConnectSql.ds.Tables["dbo.tDanhMucKhachHang"].Rows.Add(row);
                    int rs = ConnectSql.adapter.Update(ConnectSql.ds.Tables["dbo.tDanhMucKhachHang"]);
                    if (rs > 0)
                    {
                        MessageBox.Show("Thêm dữ liệu thành công!");
                        btnDung.PerformClick();
                        ConnectSql.XoaNoiDung(this);
                    }

                    else
                    {
                        MessageBox.Show("Thêm dữ liệu thất bại!");
                    }
                    ShowAllKhachHang();
                }
                catch (Exception)
                {
                    MessageBox.Show("Trùng mã Khách hàng");
                    ShowAllKhachHang();

                }
            }
            else if (strFormState == "EDITTING" && textEmpty())
            {
                DataRow row = ConnectSql.ds.Tables["dbo.tDanhMucKhachHang"].Rows[ClassApp.vt];
                row.BeginEdit();
                row["cMaKhachHang"] = txtMakh.Text.Trim();
                row["cTenKhachHang"] = txtTenkh.Text.Trim();
                row["cMaSoThue"] = txtmst.Text.Trim();
                row["cDiaChi"] = txtTTP.Text.Trim();
                row["cTinhThanhPho"] = txtTT.Text.Trim();
                row["cDienThoai"] = txtDT.Text.Trim();
                row["cFax"] = txtFax.Text.Trim();
                row.EndEdit();
                try
                {

                    int rs = ConnectSql.adapter.Update(ConnectSql.ds.Tables["dbo.tDanhMucKhachHang"]);
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
                    ShowAllKhachHang();
                }
                catch (Exception)
                {
                    MessageBox.Show("Trùng mã Khách hàng");
                    ShowAllKhachHang();

                }
            }
        }

        private void btnDung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            errorProvider1.Clear();
            btnLuu.Enabled = false;
            btnThoat.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnIn.Enabled = true;
            btnXoa.Enabled = true;
            btnSQL.Enabled = true;
            btnDung.Enabled = false;
            btnAnTT.Enabled = true;
            //  splitContainer1.Panel1Collapsed = true;
        }
        private void textboxLoad()
        {
            ClassApp.vt = index;
           
            DataRow row = ConnectSql.ds.Tables["dbo.tDanhMucKhachHang"].Rows[ClassApp.vt];
            txtMakh.Text = row["cMaKhachHang"].ToString();
            txtTenkh.Text = row["cTenKhachHang"].ToString();
            txtmst.Text = row["cMaSoThue"].ToString();
            txtTTP.Text = row["cDiaChi"].ToString();
            txtTT.Text = row["cTinhThanhPho"].ToString();
            txtDT.Text = row["cDienThoai"].ToString();
            txtFax.Text = row["cFax"].ToString();
            dgvKH.Rows[ClassApp.vt].Selected = true;
            btnAnTT.Enabled = true;
        }
        public static int x;
        private void btnSQL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmTimKiemKH frm  = new FrmTimKiemKH();
            frm.ShowDialog();
            splitContainer1.Panel1Collapsed = false;
            
            if (index > -1)
            {
                dgvKH.Rows[index].Cells[0].Selected = true;
                
                while (dgvKH.Rows[x].Cells[0].Value.ToString() != value)
                {
                    x++;
                }
                {
                    index = x;
                   //MessageBox.Show(value);
                   //MessageBox.Show(index.ToString());
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

        private void dgvKH_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            splitContainer1.Panel1Collapsed = false;
            btnAnTT.Enabled = true;
        }
        

        private void btnAnTT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splitContainer1.Panel1Collapsed = true;
            btnAnTT.Enabled = false;
        }
        

        private void FrmDMKH_Activated(object sender, EventArgs e)
        {
            ShowAllKhachHang();
        }
    }
}