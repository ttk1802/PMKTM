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
    public partial class FrmTimKiemPN : DevExpress.XtraEditors.XtraForm
    {
        public FrmTimKiemPN()
        {
            InitializeComponent();
        }

        private void FrmTimKiemPN_Load(object sender, EventArgs e)
        {
            LoadData();
            frmPhieuNhapHangHoa.x = 0;
            ClassApp.vt = -1;
        }
        private void LoadData()
        {
            string query = "select * from dbo.tPhieuNhapHangHoa where dNgayChungTu BETWEEN  '" + ClassApp.tn + "' AND '" + ClassApp.dn + "'";
            ConnectSql.GetDataToTable1(query, "dbo.tPhieuNhapHangHoa");
            dgvPN.DataSource = ConnectSql.ds.Tables["dbo.tPhieuNhapHangHoa"];
            ChangColumn();

        }
        private void ChangColumn()
        {
            dgvPN.Columns[0].HeaderText = "Mã chứng từ";
            dgvPN.Columns[1].HeaderText = "Loại chứng từ";
            dgvPN.Columns[2].HeaderText = "Ngày chứng từ";
            dgvPN.Columns[3].HeaderText = "Số chứng từ";
            dgvPN.Columns[4].HeaderText = "Mã người bán";
            dgvPN.Columns[5].HeaderText = "Tên người bán";
            dgvPN.Columns[6].HeaderText = "Mã số thuế người bán";
            dgvPN.Columns[7].HeaderText = "Tài khoản nợ";
            dgvPN.Columns[8].HeaderText = "Tài khoản nợ GTGT";
            dgvPN.Columns[9].HeaderText = "Tài khoản có";
            dgvPN.Columns[10].HeaderText = "Diễn giải";
            dgvPN.Columns[11].HeaderText = "Số seri";
            dgvPN.Columns[12].HeaderText = "Số hóa đơn";
            dgvPN.Columns[13].HeaderText = "Ngày hóa đơn";
            dgvPN.Columns[14].HeaderText = "Thuế xuất";
            dgvPN.Columns[15].HeaderText = "Thuế GTGT";
            dgvPN.Columns[16].HeaderText = "Mã hàng";
        }
        private void TKPhieuNhap()
        {
            string NHD = Convert.ToDateTime(txtTenHH.EditValue).ToString("yyyy-MM-dd") + " 00:00:00.000";
            string query = "SELECT * FROM tPhieuNhapHangHoa where dNgayChungTu = '" + NHD.ToString() + "' and dNgayChungTu BETWEEN  '" + ClassApp.tn + "' AND '" + ClassApp.dn + "' ";
            ConnectSql.GetDataToTable1(query, "dbo.tPhieuNhapHangHoa");
            dgvPN.DataSource = ConnectSql.ds.Tables["dbo.tPhieuNhapHangHoa"];
            ChangColumn();
            if (dgvPN.RowCount < 2)
            {
                MessageBox.Show("Không tìm thấy phiếu nhập");
                LoadData();
            }
        }

        private void btnTKiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TKPhieuNhap();
        }

        private void btnDungTK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void dgvPN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClassApp.vt = e.RowIndex;
            frmPhieuNhapHangHoa.value = dgvPN.Rows[ClassApp.vt].Cells[0].Value.ToString();
            //MessageBox.Show(dgvPN.Rows[ClassApp.vt].Cells[0].Value.ToString());
        }

        private void dgvPN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               
                Close();
                string query = "select * from dbo.tPhieuNhapHangHoa where dNgayChungTu BETWEEN  '" + ClassApp.tn + "' AND '" + ClassApp.dn + "'";
                ConnectSql.GetDataToTable1(query, "dbo.tPhieuNhapHangHoa");
                dgvPN.DataSource = ConnectSql.ds.Tables["dbo.tPhieuNhapHangHoa"];
            }
        }

        private void dgvPN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            

         
            Close();
            string query = "select * from dbo.tPhieuNhapHangHoa where dNgayChungTu BETWEEN  '" + ClassApp.tn + "' AND '" + ClassApp.dn + "'";
            ConnectSql.GetDataToTable1(query, "dbo.tPhieuNhapHangHoa");
            dgvPN.DataSource = ConnectSql.ds.Tables["dbo.tPhieuNhapHangHoa"];
        }

        private void txtTenHH_EditValueChanged(object sender, EventArgs e)
        {
            btnTKiem.PerformClick();
        }

        private void txtTenHH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}