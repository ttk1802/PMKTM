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
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;

namespace CommonlibHCE
{
    public partial class frmPhieuNhapHangHoa : DevExpress.XtraEditors.XtraForm
    {
        public frmPhieuNhapHangHoa()
        {
            InitializeComponent();
        }
        static string strFormState;
        static string strMainState;
        public static int x = 0;
        public static string value = null;
        private void FrmPNHH_Load(object sender, EventArgs e)
        {
            try
            {
                Binding();
            }
            catch (Exception)
            {
                return;
            }
           
            LoadData();
            strFormState = "NORMAL";
            strMainState = "NORMAL";
            ConnectSql.bs.Position = 0;
            txtP.EditValue = (ConnectSql.bs.Position + 1) + "/" + ConnectSql.bs.Count;
            ConnectSql.LockControlValues(groupControl1);
            ConnectSql.LockControlValues(groupControl2);
            splitContainer2.Panel1Collapsed = true;
            barButtonItem2.Enabled = false;
            btnLuuP.Enabled = false;
            btnDungP.Enabled = false;

            //sub
            btnDung.Enabled = false;
            btnLuu.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
           

        }
        private void LoadData()
        {
            string query = "select * from dbo.tPhieuNhapHangHoaChiTiet where cMaChungTu = '"+ txtMact.Text +"' " ;
            ConnectSql.GetDataToTable1(query, "dbo.tPhieuNhapHangHoaChiTiet");
            dgvPN.DataSource = ConnectSql.ds.Tables["dbo.tPhieuNhapHangHoaChiTiet"];
            ChangColumn();
        }

        private void LoadData1()
        {
            string query = "select * from dbo.tPhieuNhapHangHoaChitiet";
            ConnectSql.GetDataToTable1(query, "dbo.tPhieuNhapHangHoaChitiet");
           
        }
        void Clearbinding()
        {
         
            txtMact.DataBindings.Clear();
            txtLoaiCT.DataBindings.Clear();
            txtNgayCT.DataBindings.Clear();
            txtSCT.DataBindings.Clear();
            txtMaNB.DataBindings.Clear();
            txtTenNB.DataBindings.Clear();
            txtMaST.DataBindings.Clear();
            txtTKN.DataBindings.Clear();
            txtTKGTT.DataBindings.Clear();
            txtTKC.DataBindings.Clear();
            txtDienGiai.DataBindings.Clear();
            txtSosr.DataBindings.Clear();
            txtSoHD.DataBindings.Clear();
            txtNgayHD.DataBindings.Clear();
            txtThueXuat.DataBindings.Clear();
            txtThueGTGT.DataBindings.Clear();
            txtMH.DataBindings.Clear();
        }
        private void Binding()
        {
            string query = "select * from dbo.tPhieuNhapHangHoa where dNgayChungTu BETWEEN  '" + ClassApp.tn + "' AND '" + ClassApp.dn + "'";
            ConnectSql.GetDataToTable1(query, "dbo.tPhieuNhapHangHoa");
            ConnectSql.bs = new BindingSource(ConnectSql.ds, "dbo.tPhieuNhapHangHoa");
            txtMact.DataBindings.Add("Text", ConnectSql.bs, "cMaChungTu");
            txtLoaiCT.DataBindings.Add("Text", ConnectSql.bs, "cLoaiChungTu");
            txtNgayCT.DataBindings.Add("Text", ConnectSql.bs, "dNgayChungTu") ;
            txtSCT.DataBindings.Add("Text", ConnectSql.bs, "cSoChungTu");
            txtMaNB.DataBindings.Add("Text", ConnectSql.bs, "cMaNguoiBan");
            txtTenNB.DataBindings.Add("Text", ConnectSql.bs, "cTenNguoiBan");
            txtMaST.DataBindings.Add("Text", ConnectSql.bs, "cMaSoThueNguoiBan");
            txtTKN.DataBindings.Add("Text", ConnectSql.bs, "cTaiKhoanNo");
            txtTKGTT.DataBindings.Add("Text", ConnectSql.bs, "cTaiKhoanNoGTGT");
            txtTKC.DataBindings.Add("Text", ConnectSql.bs, "cTaiKhoanCo");
            txtDienGiai.DataBindings.Add("Text", ConnectSql.bs, "cDienGiai");
            txtSosr.DataBindings.Add("Text", ConnectSql.bs, "cSoSeRi");
            txtSoHD.DataBindings.Add("Text", ConnectSql.bs, "cSoHoaDon");
            txtNgayHD.DataBindings.Add("Text", ConnectSql.bs, "dNgayHoaDon");
            txtThueXuat.DataBindings.Add("Text", ConnectSql.bs, "nThueSuat");
            txtThueGTGT.DataBindings.Add("Text", ConnectSql.bs, "nThueGTGT");
            txtMH.DataBindings.Add("Text", ConnectSql.bs, "cMatHang");
        }
        
        private void ChangColumn()
        {
            dgvPN.Columns[0].HeaderText = "Mã chứng từ";
            dgvPN.Columns[1].HeaderText = "Mã số";  
            dgvPN.Columns[2].HeaderText = "Mã hàng";
            dgvPN.Columns[3].HeaderText = "Đơn vị tính";
            dgvPN.Columns[4].HeaderText = "Số lượng";
            dgvPN.Columns[5].HeaderText = "Đơn giá";
            dgvPN.Columns[6].HeaderText = "Thành tiền";
        }
        private bool textEmpty()
        {
            string empty = maskedTextBox1.Text;
            float tx;
            if (txtLoaiCT.Text == "") { errorProvider1.SetError(txtLoaiCT, "Yêu cầu nhập"); txtLoaiCT.Focus();  return false; }
            else errorProvider1.Clear();
            if (txtNgayCT.Text == empty) { errorProvider1.SetError(txtNgayCT, "Yêu cầu nhập"); txtNgayCT.Focus();  return false; }
            else errorProvider1.Clear();
            if (txtSCT.Text == "") { errorProvider1.SetError(txtSCT, "Yêu cầu nhập"); txtSCT.Focus(); return false; }
            else errorProvider1.Clear();
            if (txtNgayHD.Text == empty) { errorProvider1.SetError(txtNgayHD, "Yêu cầu nhập"); txtNgayHD.Focus(); return false; }
            else errorProvider1.Clear();    
            if (!float.TryParse(txtThueXuat.Text, out tx)) { errorProvider1.SetError(txtThueXuat, "Yêu cầu nhập"); txtThueXuat.Focus(); return false; }
            else errorProvider1.Clear();
            if (!float.TryParse(txtThueGTGT.Text, out tx)) { errorProvider1.SetError(txtThueGTGT, "Yêu cầu nhập"); txtThueGTGT.Focus(); return false; }
            else errorProvider1.Clear();
            if (txtMahh.Text == "") { errorProvider1.SetError(txtMahh, "Yêu cầu nhập"); txtMahh.Focus(); return false; }
            else errorProvider1.Clear();
            if (txtDonVitinh.Text == "") { errorProvider1.SetError(txtDonVitinh, "Yêu cầu nhập"); txtDonVitinh.Focus(); return false; }
            else errorProvider1.Clear();
            if (!float.TryParse(txtSL.Text, out tx)) { errorProvider1.SetError(txtSL, "Yêu cầu nhập số"); txtSL.Focus(); return false; }
            else errorProvider1.Clear();
            if (!float.TryParse(txtDG.Text, out tx)) { errorProvider1.SetError(txtDG, "Yêu cầu nhập số"); txtDG.Focus(); return false; }
            else errorProvider1.Clear();

            return true;
        }
        private bool textEmptyMain()
        {

            float tx;
            if (!float.TryParse(txtThueXuat.Text, out tx)) { errorProvider1.SetError(txtThueXuat, "Yêu cầu nhập"); txtThueXuat.Focus(); return false; }
            else errorProvider1.Clear();
            if (!float.TryParse(txtThueGTGT.Text, out tx)) { errorProvider1.SetError(txtThueGTGT, "Yêu cầu nhập"); txtThueGTGT.Focus(); return false; }
            else errorProvider1.Clear();
            return true;
        }
        private bool textEmptySub()
        {
            float tx;
            if (txtMahh.Text == "") { errorProvider1.SetError(txtMahh, "Yêu cầu nhập"); txtMahh.Focus(); return false; }
            else errorProvider1.Clear();
            if (txtDonVitinh.Text == "") { errorProvider1.SetError(txtDonVitinh, "Yêu cầu nhập"); txtDonVitinh.Focus(); return false; }
            else errorProvider1.Clear();
            if (!float.TryParse(txtSL.Text, out tx)) { errorProvider1.SetError(txtSL, "Yêu cầu nhập số"); txtSL.Focus(); return false; }
            else errorProvider1.Clear();
            if (!float.TryParse(txtDG.Text, out tx)) { errorProvider1.SetError(txtDG, "Yêu cầu nhập số"); txtDG.Focus(); return false; }
            else errorProvider1.Clear();
            return true;
        }
        
        private void dgvPN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            try
            {
                ClassApp.vt = e.RowIndex;
                if (ClassApp.vt == -1 || ClassApp.vt > ConnectSql.ds.Tables["dbo.tPhieuNhapHangHoaChiTiet"].Rows.Count) return;
                DataRow row = ConnectSql.ds.Tables["dbo.tPhieuNhapHangHoaChiTiet"].Rows[ClassApp.vt];
                txtMahh.Text = row["cMaHang"].ToString();
                txtDonVitinh.Text = row["cDonViTinh"].ToString();
                txtSL.Text = row["nSoLuong"].ToString();
                txtDG.Text = row["nDonGia"].ToString();
                txtTT.Text = row["nThanhTien"].ToString();
            }
            catch (Exception)
            {
                return;
            }
        }

        private void dgvPN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            splitContainer2.Panel1Collapsed = false;

            //sub
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            toolStrip1.Enabled = true;
        }
        
        public static string MaNb;
        public static string TenNb;
        public static string Mst;
        public static int index;
        private void btnTim_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmTimKiemPN frm = new FrmTimKiemPN();
            frm.ShowDialog();
             ConnectSql.bs.Position = x;
            if (index > -1)
            {
                while (txtMact.Text != value)
                {
                    x++;
                    ConnectSql.bs.Position = x;
                }
                {
                    index = x;
                    ConnectSql.bs.Position = index;
                }
            }
          

              txtP.EditValue = (index + 1) + "/" + ConnectSql.bs.Count;
            LoadData();
        }

        private void txtMaNB_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (strFormState == "ADDING" && strMainState == "Turn on")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    FrmTimKiemKH frm = new FrmTimKiemKH();
                    frm.ShowDialog();
                    txtMaNB.Text = MaNb;
                    txtTenNB.Text = TenNb;
                    txtMaST.Text = Mst;
                }
            }

            if (strFormState == "EDITTING" && strMainState == "Turn on")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    FrmTimKiemKH frm = new FrmTimKiemKH();
                    frm.ShowDialog();
                    txtMaNB.Text = MaNb;
                    txtTenNB.Text = TenNb;
                    txtMaST.Text = Mst;
                }
            }

        }

       //Main
        private void btnThemP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtNgayHD.Text = DateTime.Today.ToShortDateString();
            strFormState = "ADDING";
            strMainState = "Turn on";
            btnF.Enabled = false;
            btnP.Enabled = false;
            btnN.Enabled = false;
            btnL.Enabled = false;
            btnThemP.Enabled = false;
            btnSuaP.Enabled = false;
            btnThoat.Enabled = false;
            btnIn.Enabled = false;
            btnDungP.Enabled = true;
            btnLuuP.Enabled = true;
            splitContainer2.Panel1Collapsed = false;
            ConnectSql.UnLockControlValues(groupControl1);
            ConnectSql.UnLockControlValues(groupControl2);
            ConnectSql.XoaNoiDung(groupControl1);
            ConnectSql.XoaNoiDung(groupControl2);
            dgvPN.DataSource = null;
            toolStrip1.Enabled = false;
            txtMact.ReadOnly = false;
            txtSCT.ReadOnly = false;
            txtNgayCT.ReadOnly = false;
            txtLoaiCT.ReadOnly = false;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnSuaP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            strFormState = "EDITTING";
            strMainState = "Turn on";
            btnF.Enabled = false;
            btnP.Enabled = false;
            btnN.Enabled = false;
            btnL.Enabled = false;
            btnThemP.Enabled = false;
            btnSuaP.Enabled = false;
            btnThoat.Enabled = false;
            btnIn.Enabled = false;

            btnDungP.Enabled = true;
            btnLuuP.Enabled = true;
            ConnectSql.UnLockControlValues(groupControl1);
            ConnectSql.UnLockControlValues(groupControl2);
            txtMact.ReadOnly = true;
            txtSCT.ReadOnly = true;
            txtNgayCT.ReadOnly = true;
            txtLoaiCT.ReadOnly = true;
        }

        private void btnLuuP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (strFormState == "ADDING" && strMainState == "Turn on")
            {
                if (textEmpty() == true)
                {
                    string day = DateTime.Parse(txtNgayCT.Text).Date.ToShortDateString();
                    string dt = day.Substring(0, 6);
                    string dt2 = day.Substring(8);
                    string dt3 = dt + dt2;
                    string NCT = Convert.ToDateTime(txtNgayCT.Text).ToString("yyyy-MM-dd");
                    string NHD = Convert.ToDateTime(txtNgayHD.Text).ToString("yyyy-MM-dd");
                    

                    string MaCT = txtLoaiCT.Text + "-" + txtSCT.Text + "-" + dt3;
                    txtMact.Text = MaCT;
                    
                    float TS = float.Parse(txtThueXuat.Text.Trim().Replace(".", ","));
                    float TGTGT = float.Parse(txtThueGTGT.Text.Trim().Replace(".", ","));
                    string sql = "INSERT INTO tPhieuNhapHangHoa(cMaChungTu,cLoaiChungTu,dNgayChungTu," +
                        "cSoChungTu,cMaNguoiBan,cTenNguoiBan, cMaSoThueNguoiBan, cTaiKhoanNo, cTaiKhoanNoGTGT, cTaiKhoanCo, " +
                        " cDienGiai, cSoSeRi, cSoHoaDon, dNgayHoaDon, nThueSuat, nThueGTGT, cMatHang" +
                        ") VALUES (N'" + MaCT + "',N'" + txtLoaiCT.Text.Trim() + "',N'" + NCT +
                        "',N'" + txtSCT.Text.Trim() + "',N'" + txtMaNB.Text.Trim() + "',N'" + txtTenNB.Text.Trim() + "',N'" +
                        txtMaST.Text.Trim() + "',N'"
                        + txtTKN.Text.Trim() + "',N'" + txtTKGTT.Text.Trim() + "',N'" + txtTKC.Text.Trim() + "',N'"
                        + txtDienGiai.Text.Trim() + "',N'" + txtSosr.Text.Trim() + "',N'" + txtSoHD.Text.Trim() + "',N'"
                        + NHD + "','" + TS.ToString() + "','" + TGTGT.ToString() +
                        "',N'" + txtMH.Text.Trim() + "')";
                    ConnectSql.RunSQL(sql);
                    //ADD SUB
                    if (ConnectSql.RunSql == true)
                    {
                        double SL = double.Parse(txtSL.Text.Trim());
                        double DG = double.Parse(txtDG.Text.Trim());
                        LoadData1();
                        DataRow row = ConnectSql.ds.Tables["dbo.tPhieuNhapHangHoaChiTiet"].NewRow();
                        row["cMaChungTu"] = txtMact.Text.Trim();
                        row["nMaSo"] = ConnectSql.ds.Tables["dbo.tPhieuNhapHangHoaChitiet"].Rows.Count + 1;
                        row["cMaHang"] = txtMahh.Text.Trim();
                        row["cDonViTinh"] = txtDonVitinh.Text.Trim();
                        row["nSoLuong"] = txtSL.Text.Trim();
                        row["nDonGia"] = txtDG.Text.Trim();
                        row["nThanhTien"] = SL * DG;

                        try
                        {
                            ConnectSql.ds.Tables["dbo.tPhieuNhapHangHoaChiTiet"].Rows.Add(row);
                            int rs = ConnectSql.adapter.Update(ConnectSql.ds.Tables["dbo.tPhieuNhapHangHoaChiTiet"]);
                            if (rs > 0)
                            {
                                MessageBox.Show("Thêm dữ liệu thành công!");
                                btnDungP.PerformClick();
                                ConnectSql.XoaNoiDung(this.groupControl2);
                                LoadData();

                            }

                            else
                            {
                                MessageBox.Show("Thêm dữ liệu thất bại!");
                               
                            }
                            LoadData();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Trùng mã");
                            LoadData();

                        }
                        ConnectSql.XoaNoiDung(this.groupControl2);
                       
                       
                        MessageBox.Show("Thêm phiếu nhập thành công!");
                    }
                }

               
            }
          

            else if (strFormState == "EDITTING" && strMainState == "Turn on")
            {
                if (textEmptyMain() == true)
                {
                    string day = DateTime.Parse(txtNgayCT.Text).Date.ToShortDateString();
                    string dt = day.Substring(0, 6);
                    string dt2 = day.Substring(8);
                    string dt3 = dt + dt2;
                    string NCT = Convert.ToDateTime(txtNgayCT.Text).ToString("yyyy-MM-dd");
                    string NHD = Convert.ToDateTime(txtNgayHD.Text).ToString("yyyy-MM-dd");

                    string MaCT = txtLoaiCT.Text + "-" + txtSCT.Text + "-" + dt3;
                    txtMact.Text = MaCT;

                    float TS = float.Parse(txtThueXuat.Text.Replace(",", "."));
                    float TGTGT = float.Parse(txtThueGTGT.Text.Replace(",", "."));
                    string ts = TS.ToString("P");
                   // MessageBox.Show(ts);
                    string sql = "UPDATE tPhieuNhapHangHoa SET cLoaiChungTu = N'" + txtLoaiCT.Text.Trim() + "'" +
                      ",dNgayChungTu = N'" + NCT + "'" +
                      ",cSoChungTu = N'" + txtSCT.Text.Trim() + "'" +
                      ",cMaNguoiBan = N'" + txtMaNB.Text.Trim() + "'" +
                      ",cTenNguoiBan = N'" + txtTenNB.Text.Trim() + "'" +
                      ",cMaSoThueNguoiBan = N'" + txtMaST.Text.Trim() + "'" +
                      ",cTaiKhoanNo = N'" + txtTKN.Text.Trim() + "'" +
                      ",cTaiKhoanNoGTGT = N'" + txtTKGTT.Text.Trim() + "'" +
                      ",cTaiKhoanCo = N'" + txtTKC.Text.Trim() + "'" +
                      ",cDienGiai = N'" + txtDienGiai.Text.Trim() + "'" +
                      ",cSoSeRi = N'" + txtSosr.Text.Trim() + "'" +
                      ",cSoHoaDon = N'" + txtSoHD.Text.Trim() + "'" +
                      ",dNgayHoaDon = N'" + NHD + "'" +
                      ",nThueSuat = '" + ts + "'" +
                      ",nThueGTGT = '" + TGTGT.ToString() + "'" +
                      ",cMatHang = N'" + txtMH.Text.Trim() + "'" +
                      " where cMaChungTu = N'" + txtMact.Text.Trim() + "'";
                    ConnectSql.RunSQL(sql);
                    if (ConnectSql.RunSql == true)
                    {
                        MessageBox.Show("Chỉnh sửa phiếu nhập thành công");
                        btnDungP.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Chỉnh sửa phiếu nhập Thất bại");
                    }
                }

            }
          
            ConnectSql.bs.Dispose();
            Clearbinding();
            Binding();
            txtP.EditValue = (ConnectSql.bs.Position + 1) + "/" + ConnectSql.bs.Count;

        }

      

        private void btnDungP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnThemP.Enabled = true;
            btnSuaP.Enabled = true;
            btnThoat.Enabled = true;
            btnIn.Enabled = true;
            btnLuuP.Enabled = false;
            btnDungP.Enabled = false;
            btnF.Enabled = true;
            btnP.Enabled = true;
            btnN.Enabled = true;
            btnL.Enabled = true;
            
            ConnectSql.LockControlValues(groupControl1);
            ConnectSql.LockControlValues(groupControl2);
            //   ConnectSql.bs.ResetBindings(true);
          //  x = ConnectSql.bs.Position;
          //  ConnectSql.bs.ResetItem(x);
            ConnectSql.bs.ResetCurrentItem();
            errorProvider1.Clear();
            LoadData();
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        //sub
        private void btnSua_Click(object sender, EventArgs e)
        {
            btnTim.Enabled = true;
            strMainState = "Turn off";
            strFormState = "EDITTING";
            btnSua.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnDung.Enabled = true;
            splitContainer2.Panel1Collapsed = false;
            ConnectSql.UnLockControlValues(groupControl2);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnTim.Enabled = true;
            strFormState = "ADDING";
            strMainState = "Turn off";
            txtMahh.Focus();
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnDung.Enabled = true;
            splitContainer2.Panel1Collapsed = false;
            ConnectSql.UnLockControlValues(groupControl2);
            ConnectSql.XoaNoiDung(groupControl2);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult warn = MessageBox.Show("Bạn có thực sự muốn xóa dòng này không?", "Cảnh báo!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (warn == DialogResult.Yes)
            {
                try
                {
                    DataRow row = ConnectSql.ds.Tables["dbo.tPhieuNhapHangHoaChiTiet"].Rows[ClassApp.vt];
                    row.Delete();
                    int rs = ConnectSql.adapter.Update(ConnectSql.ds.Tables["dbo.tPhieuNhapHangHoaChiTiet"]);
                    if (rs > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!");
                        ConnectSql.XoaNoiDung(this.groupControl2);
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Xóa dữ liệu thất bại!");
                }

            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Sub
            if (strFormState == "ADDING" && strMainState == "Turn off")
            {
                if (textEmptySub() == true)
                {
                    LoadData1();
                    double SL = double.Parse(txtSL.Text.Trim());
                    double DG = double.Parse(txtDG.Text.Trim());
                    DataRow row = ConnectSql.ds.Tables["dbo.tPhieuNhapHangHoaChiTiet"].NewRow();
                    row["cMaChungTu"] = txtMact.Text.Trim();
                    row["nMaSo"] = ConnectSql.ds.Tables["dbo.tPhieuNhapHangHoaChitiet"].Rows.Count + 1;
                    row["cMaHang"] = txtMahh.Text.Trim();
                    row["cDonViTinh"] = txtDonVitinh.Text.Trim();
                    row["nSoLuong"] = txtSL.Text.Trim();
                    row["nDonGia"] = txtDG.Text.Trim();
                    row["nThanhTien"] = SL * DG;
                    try
                    {
                        ConnectSql.ds.Tables["dbo.tPhieuNhapHangHoaChiTiet"].Rows.Add(row);
                        int rs = ConnectSql.adapter.Update(ConnectSql.ds.Tables["dbo.tPhieuNhapHangHoaChiTiet"]);
                        if (rs > 0)
                        {
                            MessageBox.Show("Thêm dữ liệu thành công!");

                            ConnectSql.XoaNoiDung(this.groupControl2);
                            btnDung.PerformClick();
                        }

                        else
                        {
                            MessageBox.Show("Thêm dữ liệu thất bại!");
                        }
                        LoadData();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Trùng mã");
                        LoadData();

                    }
                }
            }


            else if (strFormState == "EDITTING" && strMainState == "Turn off")
            {
                if (textEmptySub() == true)
                {
                    double SL = double.Parse(txtSL.Text.Trim());
                    double DG = double.Parse(txtDG.Text.Trim());
                    DataRow row = ConnectSql.ds.Tables["dbo.tPhieuNhapHangHoaChiTiet"].Rows[ClassApp.vt];
                    row.BeginEdit();
                    row["cMaHang"] = txtMahh.Text.Trim();
                    row["cDonViTinh"] = txtDonVitinh.Text.Trim();
                    row["nSoLuong"] = txtSL.Text.Trim();
                    row["nDonGia"] = txtDG.Text.Trim();
                    row["nThanhTien"] = SL * DG;
                    row.EndEdit();
                    try
                    {

                        int rs = ConnectSql.adapter.Update(ConnectSql.ds.Tables["dbo.tPhieuNhapHangHoaChiTiet"]);
                        if (rs > 0)
                        {
                            MessageBox.Show("Chỉnh sửa liệu thành công!!");
                            ConnectSql.XoaNoiDung(this.groupControl2);
                            btnDung.PerformClick();
                        }

                        else
                        {
                            MessageBox.Show("Chỉnh sửa dữ liệu thất bại!!");
                        }
                        LoadData();
                    }
                    catch (Exception)
                    {
                        // throw;
                        MessageBox.Show("Trùng mã");
                        LoadData();

                    }
                }
            }
        }

        private void btnDung_Click(object sender, EventArgs e)
        {
            if (strMainState == "Turn off")
            {
                btnDung.Enabled = false;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                ConnectSql.LockControlValues(groupControl2);
                btnTim.Enabled = false;
                LoadData();
            }
        }

       
  

        //pos
       // int x;
        private void btnF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            ConnectSql.bs.Position = 0;
            txtP.EditValue = (ConnectSql.bs.Position + 1) + "/" + ConnectSql.bs.Count;
            LoadData();
        }

        private void btnP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ConnectSql.bs.Position > 0)
                ConnectSql.bs.Position--;
            txtP.EditValue = (ConnectSql.bs.Position + 1) + "/" + ConnectSql.bs.Count;
            LoadData();
        }

        private void btnN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ConnectSql.bs.Position < ConnectSql.bs.Count - 1)
                ConnectSql.bs.Position++;
            txtP.EditValue = (ConnectSql.bs.Position + 1) + "/" + ConnectSql.bs.Count;
            LoadData();

        }

        private void btnL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConnectSql.bs.Position = ConnectSql.bs.Count - 1;
            txtP.EditValue = (ConnectSql.bs.Position + 1) + "/" + ConnectSql.bs.Count;
            LoadData();
        }

        private void btnCNgay_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmNSL fr = new FrmNSL();
            fr.ShowDialog();
            frmPhieuNhapHangHoa frm = new frmPhieuNhapHangHoa();
            frm.MdiParent = this.MdiParent;
            frm.Show();
            this.Close();
        }
        public static string MH;
        public static string DVT;
        public static string DG;
        private void txtMahh_KeyDown(object sender, KeyEventArgs e)
        {
            if (strFormState == "ADDING")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    FrmTimKiemHH frm = new FrmTimKiemHH();
                    frm.ShowDialog();
                    txtMahh.Text = MH;
                    txtDonVitinh.Text = DVT;
                    txtDG.Text = DG;
                    if (txtDG.Text == "NaN")
                    {
                        txtDG.Text = "0";
                    }
                }
            }

            if (strFormState == "EDITTING")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    FrmTimKiemHH frm = new FrmTimKiemHH();
                    frm.ShowDialog();
                    txtMahh.Text = MH;
                    txtDonVitinh.Text = DVT;
                    txtDG.Text = DG;
                    if (txtDG.Text == "NaN")
                    {
                        txtDG.Text = "0";
                    }
                }
            }
        }
        double tongtien;
        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //lấy dữ liệu cho reporrt 
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.Connection = ConnectSql.Con; //Gán kết nối
            string query = "select tPhieuNhapHangHoa.cMaChungTu,cSoChungTu,dNgayChungTu,nThueGTGT,"
                + "cTenNguoiBan,cMaSoThueNguoiBan,cDienGiai" +
                ", tPhieuNhapHangHoaChiTiet.cMaChungTu, tPhieuNhapHangHoaChiTiet.cMaHang," +
                "tPhieuNhapHangHoaChiTiet.nSoLuong, tPhieuNhapHangHoaChiTiet.nDonGia,tPhieuNhapHangHoaChiTiet.cDonViTinh,tPhieuNhapHangHoaChiTiet.nThanhTien, " +
                "tDanhMucHangHoa.cTenHang,  tDanhMucHangHoa.cMaHang, sum(tPhieuNhapHangHoaChiTiet.nThanhTien)/10 + sum (tPhieuNhapHangHoaChiTiet.nThanhTien) as TongThanhTien " +
                "from tPhieuNhapHangHoa, tPhieuNhapHangHoaChiTiet, tDanhMucHangHoa where tPhieuNhapHangHoa.cMaChungTu=tPhieuNhapHangHoaChiTiet.cMaChungTu " +
               " AND tPhieuNhapHangHoaChiTiet.cMaHang = tDanhMucHangHoa.cMaHang AND tPhieuNhapHangHoa.cMaChungTu = @mapn " +
              "group by tPhieuNhapHangHoa.cMaChungTu,tPhieuNhapHangHoa.cSoChungTu," +

               " tPhieuNhapHangHoa.dNgayChungTu,tPhieuNhapHangHoa.nThueGTGT," +
              "  tPhieuNhapHangHoa.cTenNguoiBan,tPhieuNhapHangHoa.cMaSoThueNguoiBan,"+
             "   tPhieuNhapHangHoa.cDienGiai, tPhieuNhapHangHoaChiTiet.cMaChungTu," +
              "  tPhieuNhapHangHoaChiTiet.cMaHang,tPhieuNhapHangHoaChiTiet.nSoLuong, "+
              "   tPhieuNhapHangHoaChiTiet.nDonGia, tPhieuNhapHangHoaChiTiet.cDonViTinh,"+
              "   tPhieuNhapHangHoaChiTiet.nThanhTien, tDanhMucHangHoa.cTenHang,  tDanhMucHangHoa.cMaHang ";

            // cmd.Parameters.Clear();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@mapn", txtMact.Text);

            DataTable dt = new DataTable("BangPhieuNhap");
            dt.Load(cmd.ExecuteReader());
            RptPhieuNhap baocao = new RptPhieuNhap();

            TextObject tchuso = (TextObject)baocao.Section4.ReportObjects["txtTienChu"];
            int x = dt.Rows.Count; //x = 1
            double tt;
            int y = 0;
            while (y < x)
            {
                 tongtien += Convert.ToDouble(dt.Rows[y]["TongThanhTien"]);
                 y++;
            }
            {
                tt = tongtien;
            }
          

            tchuso.Text = ClassApp.DocTienBangChu(tt, " đồng");
            baocao.SetDataSource(dt);
            FrmBaoCaoNhap PN = new FrmBaoCaoNhap();
            PN.ctrPN.ReportSource = baocao;
            PN.Show();
            tongtien = 0;
            tt = 0;
        }

        private void frmPhieuNhapHangHoa_Activated(object sender, EventArgs e)
        {
            ConnectSql.bs.Dispose();
            Clearbinding();
            Binding();
            ConnectSql.bs.Position = 0;
            txtP.EditValue = (ConnectSql.bs.Position + 1) + "/" + ConnectSql.bs.Count;
        }
    }

}