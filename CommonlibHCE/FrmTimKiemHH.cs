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
using DevExpress.CodeParser;

namespace CommonlibHCE
{
    public partial class FrmTimKiemHH : DevExpress.XtraEditors.XtraForm
    {
        public FrmTimKiemHH()
        {
            InitializeComponent();
        }

        private void FrmTimKiemHH_Load(object sender, EventArgs e)
        {
            ShowAllHangHoa();
            FrmDMHH.index = -1;
            FrmDMHH.x = 0;
            ClassApp.vt = -1;
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

        private void TKHangHoa()
        {
            string query = "SELECT * FROM tDanhMucHangHoa where cTenHang like N'%" + txtTenHH.EditValue + "%'";
            ConnectSql.GetDataToTable1(query, "dbo.tDanhMucHangHoa");
            dgvHH.DataSource = ConnectSql.ds.Tables["dbo.tDanhMucHangHoa"];
            ChangColumn();
            if (dgvHH.RowCount < 2)
            {
                MessageBox.Show("Không tìm thấy hàng hóa");
                ShowAllHangHoa();
            }
        }

        private void btnTKiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TKHangHoa();
        }

      

        private void btnDừngTK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowAllHangHoa();
        }

        private void dgvHH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
           
            ClassApp.vt = e.RowIndex;

            FrmDMHH.value = dgvHH.Rows[ClassApp.vt].Cells[0].Value.ToString();

            frmPhieuNhapHangHoa.MH = dgvHH.Rows[ClassApp.vt].Cells[0].Value.ToString();
            frmPhieuNhapHangHoa.DVT = dgvHH.Rows[ClassApp.vt].Cells[3].Value.ToString();
            float DG = float.Parse(dgvHH.Rows[ClassApp.vt].Cells[5].Value.ToString()) / float.Parse(dgvHH.Rows[ClassApp.vt].Cells[4].Value.ToString());
            frmPhieuNhapHangHoa.DG = DG.ToString();

        
            //  MessageBox.Show(dgvHH.Rows[ClassApp.vt].Cells[0].Value.ToString());
            // if (ClassApp.vt == -1 || ClassApp.vt == ConnectSql.ds.Tables["dbo.tDanhMucHangHoa"].Rows.Count) return;
        }



        private void dgvHH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FrmDMHH.index = ClassApp.vt;
                Close();
                string query = "SELECT * FROM tDanhMucHangHoa";
                ConnectSql.GetDataToTable1(query, "dbo.tDanhMucHangHoa");
                dgvHH.DataSource = ConnectSql.ds.Tables["dbo.tDanhMucHangHoa"];
            }
        }

        private void dgvHH_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            FrmDMHH.index = ClassApp.vt;
            Close();
            string query = "SELECT * FROM tDanhMucHangHoa";
            ConnectSql.GetDataToTable1(query, "dbo.tDanhMucHangHoa");
            dgvHH.DataSource = ConnectSql.ds.Tables["dbo.tDanhMucHangHoa"];
        }

        private void FrmTimKiemHH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTKiem.PerformClick();
            }
        }

        private void txtTenHH_EditValueChanged(object sender, EventArgs e)
        {
            btnTKiem.PerformClick();
        }
    }
}