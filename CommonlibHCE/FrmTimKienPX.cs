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
    public partial class FrmTimKienPX : DevExpress.XtraEditors.XtraForm
    {
        public FrmTimKienPX()
        {
            InitializeComponent();
        }

        private void FrmTimKienPX_Load(object sender, EventArgs e)
        {
            LoadData();
            FrmPXHH.x = 0;
            ClassApp.vt = -1;

        }
        private void LoadData()
        {
            string query = "select * from dbo.tPhieuXuatHangHoa where dNgayChungTu BETWEEN  '" + ClassApp.tn + "' AND '" + ClassApp.dn + "'";
            ConnectSql.GetDataToTable1(query, "dbo.tPhieuXuatHangHoa");
            dgvPN.DataSource = ConnectSql.ds.Tables["dbo.tPhieuXuatHangHoa"];
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
            dgvPN.Columns[6].HeaderText = "Mã số thuế";
            dgvPN.Columns[7].HeaderText = "Tài khoản nợ giá vốn";
            dgvPN.Columns[8].HeaderText = "Tài khoản có giá vốn";
            dgvPN.Columns[9].HeaderText = "Tài khoản nợ giá bán";
            dgvPN.Columns[10].HeaderText = "Tài khoản có giá vốn";
            dgvPN.Columns[11].HeaderText = "Tài khoản có GTGT";
            dgvPN.Columns[12].HeaderText = "Diễn giải";
            dgvPN.Columns[13].HeaderText = "Mã hàng";
            dgvPN.Columns[14].HeaderText = "Thuế xuất";
            dgvPN.Columns[15].HeaderText = "Thuế GTGT";
            dgvPN.Columns[16].HeaderText = "Số seri";
            dgvPN.Columns[17].HeaderText = "Số hóa đơn";
          
          
            
        }
        private void TKPhieuXuat()
        {
            string NHD = Convert.ToDateTime(txtTenHH.EditValue).ToString("yyyy-MM-dd") + " 00:00:00.000";
            string query = "SELECT * FROM tPhieuXuatHangHoa where dNgayChungTu = '" + NHD.ToString() + "' and dNgayChungTu BETWEEN  '" + ClassApp.tn + "' AND '" + ClassApp.dn + "' ";
            ConnectSql.GetDataToTable1(query, "dbo.tPhieuXuatHangHoa");
            dgvPN.DataSource = ConnectSql.ds.Tables["dbo.tPhieuXuatHangHoa"];
            ChangColumn();
            if (dgvPN.RowCount < 2)
            {
                MessageBox.Show("Không tìm thấy phiếu xuất");
                LoadData();
            }
        }
        private void txtTenHH_EditValueChanged(object sender, EventArgs e)
        {
            btnTKiem.PerformClick();
        }

        private void btnTKiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TKPhieuXuat();
        }

        private void btnDungTK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void dgvPN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          
            ClassApp.vt = e.RowIndex;
            FrmPXHH.value = dgvPN.Rows[ClassApp.vt].Cells[0].Value.ToString();
            Close();
            string query = "select * from dbo.tPhieuXuatHangHoa where dNgayChungTu BETWEEN  '" + ClassApp.tn + "' AND '" + ClassApp.dn + "'";
            ConnectSql.GetDataToTable1(query, "dbo.tPhieuXuatHangHoa");
            dgvPN.DataSource = ConnectSql.ds.Tables["dbo.tPhieuXuatHangHoa"];
        }
    }
}