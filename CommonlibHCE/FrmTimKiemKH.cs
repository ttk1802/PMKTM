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
    public partial class FrmTimKiemKH : DevExpress.XtraEditors.XtraForm
    {
        public FrmTimKiemKH()
        {
            InitializeComponent();
        }

        private void FrmTimKiemKH_Load(object sender, EventArgs e)
        {
            ShowAllKhachHang();
            FrmDMKH.index = -1;
            FrmDMKH.x = 0;
            ClassApp.vt = -1;
            

        }
        private void ShowAllKhachHang()
        {
            string query = "select * from dbo.tDanhMucKhachHang";
            ConnectSql.GetDataToTable1(query, "dbo.tDanhMucKhachHang");
            dgvKH.DataSource = ConnectSql.ds.Tables["dbo.tDanhMucKhachHang"];
            ChangColumn();

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
        private void TKKhachHang()
        {
            string query = "SELECT * FROM tdanhmuckhachhang where ctenkhachhang like N'%"+txtTenKH.EditValue+"%'";
            ConnectSql.GetDataToTable1(query, "dbo.tDanhMucKhachHang");
            dgvKH.DataSource = ConnectSql.ds.Tables["dbo.tDanhMucKhachHang"];
            ChangColumn();
            if (dgvKH.RowCount < 2)
            {
                MessageBox.Show("Không tìm thấy khách hàng");
                ShowAllKhachHang();
            }
        }

        private void btnTKiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TKKhachHang();
           // MessageBox.Show(txtTenKH.EditValue);
        }
        
       

        private void btnDừngTK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowAllKhachHang();
        }

        private void dgvKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClassApp.vt = e.RowIndex;
            FrmDMKH.value = dgvKH.Rows[ClassApp.vt].Cells[0].Value.ToString();

            frmPhieuNhapHangHoa.MaNb = dgvKH.Rows[ClassApp.vt].Cells[0].Value.ToString();
            frmPhieuNhapHangHoa.TenNb = dgvKH.Rows[ClassApp.vt].Cells[1].Value.ToString();
            frmPhieuNhapHangHoa.Mst = dgvKH.Rows[ClassApp.vt].Cells[2].Value.ToString();

            FrmPXHH.MaNb = dgvKH.Rows[ClassApp.vt].Cells[0].Value.ToString();
            FrmPXHH.TenNb = dgvKH.Rows[ClassApp.vt].Cells[1].Value.ToString();
            FrmPXHH.Mst = dgvKH.Rows[ClassApp.vt].Cells[2].Value.ToString();
            //MessageBox.Show(dgvKH.Rows[ClassApp.vt].Cells[0].Value.ToString());
            // if (ClassApp.vt == -1 || ClassApp.vt == ConnectSql.ds.Tables["dbo.tDanhMucKhachHang"].Rows.Count) return;
        }

        private void dgvKH_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            FrmDMKH.index = ClassApp.vt;
            Close();
            string query = "SELECT * FROM tdanhmuckhachhang";
            ConnectSql.GetDataToTable1(query, "dbo.tDanhMucKhachHang");
            dgvKH.DataSource = ConnectSql.ds.Tables["dbo.tDanhMucKhachHang"];

        }

        private void dgvKH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FrmDMKH.index = ClassApp.vt;
                Close();
                string query = "SELECT * FROM tdanhmuckhachhang";
                ConnectSql.GetDataToTable1(query, "dbo.tDanhMucKhachHang");
                dgvKH.DataSource = ConnectSql.ds.Tables["dbo.tDanhMucKhachHang"];
            }

        }

        private void FrmTimKiemKH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTKiem.PerformClick();
            }
        }

        private void txtTenKH_EditValueChanged(object sender, EventArgs e)
        {
            btnTKiem.PerformClick();
        }
    }
}