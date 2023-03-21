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
    public partial class FrmTimKiemTK : DevExpress.XtraEditors.XtraForm
    {
        public FrmTimKiemTK()
        {
            InitializeComponent();
        }

        private void FrmTimKiemTK_Load(object sender, EventArgs e)
        {
            LoadData();
            frmDanhMucTaiKhoan.index = -1;
            frmDanhMucTaiKhoan.x = 0;
            ClassApp.vt = -1;
        }
        private void LoadData()
        {

            string query = "select * from dbo.tDanhMucTaiKhoan";
            ConnectSql.GetDataToTable1(query, "dbo.tDanhMucTaiKhoan");
            dgvTK.DataSource = ConnectSql.ds.Tables["dbo.tDanhMucTaiKhoan"];
            ChangColumn();

        }
        private void ChangColumn()
        {
            dgvTK.Columns[0].HeaderText = "Tài Khoản";
            dgvTK.Columns[1].HeaderText = "Tên tài khoản";
            dgvTK.Columns[2].HeaderText = "Số dư nợ đầu";
            dgvTK.Columns[3].HeaderText = "Số dư có đầu";
            dgvTK.Columns[4].HeaderText = "Có định khoản";
            dgvTK.Columns[5].HeaderText = "Cấp";
            dgvTK.Columns[6].HeaderText = "Ngày số dư";
        }
        private void TKHangHoa()
        {
            string query = "SELECT * FROM tDanhMucTaiKhoan where cTenTaiKhoan like N'%" + txtTenTK.EditValue + "%'";
            ConnectSql.GetDataToTable1(query, "dbo.tDanhMucTaiKhoan");
            dgvTK.DataSource = ConnectSql.ds.Tables["dbo.tDanhMucTaiKhoan"];
            ChangColumn();
            if (dgvTK.RowCount < 2)
            {
                MessageBox.Show("Không tìm thấy tài khoản");
                LoadData();
            }
        }
        private void btnTKiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TKHangHoa();
        }

        private void btnDungTK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void dgvTK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClassApp.vt = e.RowIndex;
            frmDanhMucTaiKhoan.value = dgvTK.Rows[ClassApp.vt].Cells[0].Value.ToString();
        }

        private void dgvTK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                frmDanhMucTaiKhoan.index = ClassApp.vt;
                Close();
                string query = "SELECT * FROM tDanhMucTaiKhoan";
                ConnectSql.GetDataToTable1(query, "dbo.tDanhMucTaiKhoan");
                dgvTK.DataSource = ConnectSql.ds.Tables["dbo.tDanhMucTaiKhoan"];
            }
        }

        private void dgvTK_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            frmDanhMucTaiKhoan.index = ClassApp.vt;
            Close();
            string query = "SELECT * FROM tDanhMucTaiKhoan";
            ConnectSql.GetDataToTable1(query, "dbo.tDanhMucTaiKhoan");
            dgvTK.DataSource = ConnectSql.ds.Tables["dbo.tDanhMucTaiKhoan"];
        }

        private void FrmTimKiemTK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTKiem.PerformClick();
            }
        }

        private void txtTenTK_EditValueChanged(object sender, EventArgs e)
        {
            btnTKiem.PerformClick();
        }
    }
}