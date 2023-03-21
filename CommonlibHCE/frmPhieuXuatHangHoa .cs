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
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;

namespace CommonlibHCE
{
    public partial class FrmPXHH : DevExpress.XtraEditors.XtraForm
    {
        public FrmPXHH()
        {
            InitializeComponent();
        }
        static string strFormState;
        static string strMainState;
        public static int x = 0;
        public static string value = null;
        private void FrmPXHH_Load(object sender, EventArgs e)
        {
            Binding();
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
            string query = "select * from dbo.tPhieuXuatHangHoaChiTiet where cMaChungTu = '" + txtMact.Text + "' ";
            ConnectSql.GetDataToTable1(query, "dbo.tPhieuXuatHangHoaChiTiet");
            dgvPN.DataSource = ConnectSql.ds.Tables["dbo.tPhieuXuatHangHoaChiTiet"];
            ChangColumn();
        }

        private void LoadData1()
        {
            string query = "select * from dbo.tPhieuXuatHangHoaChiTiet";
            ConnectSql.GetDataToTable1(query, "dbo.tPhieuXuatHangHoaChiTiet");
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
            txtTKNGV.DataBindings.Clear();
            txtTKCGV.DataBindings.Clear();
            txtTKNGB.DataBindings.Clear();
            txtTKCGB.DataBindings.Clear();
            txtTKCGTGT.DataBindings.Clear();
            txtDienGiai.DataBindings.Clear();
            txtMH.DataBindings.Clear();
            txtThueXuat.DataBindings.Clear();
            txtThueGTGT.DataBindings.Clear();
            txtSosr.DataBindings.Clear();
            txtSoHD.DataBindings.Clear();
        }
        private void Binding()
        {
            string query = "select * from dbo.tPhieuXuatHangHoa where dNgayChungTu BETWEEN '" + ClassApp.tn + "' AND '" + ClassApp.dn + "'";
            ConnectSql.GetDataToTable1(query, "dbo.tPhieuXuatHangHoa");

            ConnectSql.bs = new BindingSource(ConnectSql.ds, "dbo.tPhieuXuatHangHoa");
            txtMact.DataBindings.Add("Text", ConnectSql.bs, "cMaChungTu");
            txtLoaiCT.DataBindings.Add("Text", ConnectSql.bs, "cLoaiChungTu");
            txtNgayCT.DataBindings.Add("Text", ConnectSql.bs, "dNgayChungTu"); 
            txtSCT.DataBindings.Add("Text", ConnectSql.bs, "cSoChungTu");
            txtMaNB.DataBindings.Add("Text", ConnectSql.bs, "cMaKhachHang");
            txtTenNB.DataBindings.Add("Text", ConnectSql.bs, "cTenKhachHang");
            txtMaST.DataBindings.Add("Text", ConnectSql.bs, "cMaSoThue");
            txtTKNGV.DataBindings.Add("Text", ConnectSql.bs, "cTaiKhoanNoGiaVon");
            txtTKCGV.DataBindings.Add("Text", ConnectSql.bs, "cTaiKhoanCoGiaVon");
            txtTKNGB.DataBindings.Add("Text", ConnectSql.bs, "cTaiKhoanNoGiaBan");
            txtTKCGB.DataBindings.Add("Text", ConnectSql.bs, "cTaiKhoanCoGiaBan");
            txtTKCGTGT.DataBindings.Add("Text", ConnectSql.bs, "cTaiKhoanCoGTGT");
            txtDienGiai.DataBindings.Add("Text", ConnectSql.bs, "cDienGiai");
            txtMH.DataBindings.Add("Text", ConnectSql.bs, "cMatHang");
            txtThueXuat.DataBindings.Add("Text", ConnectSql.bs, "nThueSuat");
            txtThueGTGT.DataBindings.Add("Text", ConnectSql.bs, "nThueGTGT");
            txtSosr.DataBindings.Add("Text", ConnectSql.bs, "cSoSeRi");
            txtSoHD.DataBindings.Add("Text", ConnectSql.bs, "cSoHoaDon");
        }

        private void ChangColumn()
        {
            dgvPN.Columns[0].HeaderText = "Mã chứng từ";
            dgvPN.Columns[1].HeaderText = "Mã hàng";
            dgvPN.Columns[2].HeaderText = "Đơn vị tính";
            dgvPN.Columns[3].HeaderText = "Số lượng";
            dgvPN.Columns[4].HeaderText = "Đơn giá vốn";
            dgvPN.Columns[5].HeaderText = "Thành tiền giá vốn";
            dgvPN.Columns[6].HeaderText = "Đơn giá bán";
            dgvPN.Columns[7].HeaderText = "Thành tiền giá bán";
            dgvPN.Columns[8].HeaderText = "Mã chứng từ nhập";
        }

        private bool textEmpty()
        {
            string empty = maskedTextBox1.Text;
            float tx;
            if (txtLoaiCT.Text == "") { errorProvider1.SetError(txtLoaiCT, "Yêu cầu nhập"); return false; }
            else errorProvider1.Clear();
            if (txtNgayCT.Text == empty) { errorProvider1.SetError(txtNgayCT, "Yêu cầu nhập"); return false; }
            else errorProvider1.Clear();
            if (txtSCT.Text == "") { errorProvider1.SetError(txtSCT, "Yêu cầu nhập"); return false; }
            else errorProvider1.Clear();
            if (txtNgayHD.Text == empty) { errorProvider1.SetError(txtNgayHD, "Yêu cầu nhập"); return false; }
            else errorProvider1.Clear();
            if (txtThueXuat.Text == "") { errorProvider1.SetError(txtThueXuat, "Yêu cầu nhập"); return false; }
            else errorProvider1.Clear();
            if (!float.TryParse(txtThueXuat.Text, out tx)) { errorProvider1.SetError(txtThueXuat, "Yêu cầu nhập"); txtThueXuat.Focus(); return false; }
            else errorProvider1.Clear();
            if (txtThueGTGT.Text == "") { errorProvider1.SetError(txtThueGTGT, "Yêu cầu nhập"); return false; }
            else errorProvider1.Clear();
            if (!float.TryParse(txtThueGTGT.Text, out tx)) { errorProvider1.SetError(txtThueGTGT, "Yêu cầu nhập"); txtThueGTGT.Focus(); return false; }
            else errorProvider1.Clear();
            if (txtSL.Text == "") { errorProvider1.SetError(txtSL, "Yêu cầu nhập"); return false; }
            else errorProvider1.Clear();
            if (!float.TryParse(txtSL.Text, out tx)) { errorProvider1.SetError(txtSL, "Yêu cầu nhập"); txtSL.Focus(); return false; }
            else errorProvider1.Clear();
            if (txtDGV.Text == "") { errorProvider1.SetError(txtDGV, "Yêu cầu nhập"); return false; }
            else errorProvider1.Clear();
            if (!float.TryParse(txtDGV.Text, out tx)) { errorProvider1.SetError(txtDGV, "Yêu cầu nhập"); txtDGV.Focus(); return false; }
            else errorProvider1.Clear();
            return true;
        }
        private bool textEmptyMain()
        {
            float tx;
            if (txtThueXuat.Text == "") { errorProvider1.SetError(txtThueXuat, "Yêu cầu nhập"); return false; }
            else errorProvider1.Clear();
            if (!float.TryParse(txtThueXuat.Text, out tx)) { errorProvider1.SetError(txtThueXuat, "Yêu cầu nhập"); txtThueXuat.Focus(); return false; }
            else errorProvider1.Clear();
            if (txtThueGTGT.Text == "") { errorProvider1.SetError(txtThueGTGT, "Yêu cầu nhập"); return false; }
            else errorProvider1.Clear();
            if (!float.TryParse(txtThueGTGT.Text, out tx)) { errorProvider1.SetError(txtThueGTGT, "Yêu cầu nhập"); txtThueGTGT.Focus(); return false; }
            else errorProvider1.Clear();
            if (txtSL.Text == "") { errorProvider1.SetError(txtSL, "Yêu cầu nhập"); return false; }
            else errorProvider1.Clear();
            if (!float.TryParse(txtSL.Text, out tx)) { errorProvider1.SetError(txtSL, "Yêu cầu nhập"); txtSL.Focus(); return false; }
            else errorProvider1.Clear();
            if (txtDGV.Text == "") { errorProvider1.SetError(txtDGV, "Yêu cầu nhập"); return false; }
            else errorProvider1.Clear();
            if (!float.TryParse(txtDGV.Text, out tx)) { errorProvider1.SetError(txtDGV, "Yêu cầu nhập"); txtDGV.Focus(); return false; }
            else errorProvider1.Clear();
            return true;
        }
        private bool textEmptySub()
        {
            float tx;
            if (txtMahh.Text == "") { errorProvider1.SetError(txtMahh, "Yêu cầu nhập"); return false; }
            else errorProvider1.Clear();
            if (txtDonVitinh.Text == "") { errorProvider1.SetError(txtDonVitinh, "Yêu cầu nhập"); return false; }
            else errorProvider1.Clear();
            if (txtSL.Text == "") { errorProvider1.SetError(txtSL, "Yêu cầu nhập"); return false; }
            else errorProvider1.Clear();
            if (!float.TryParse(txtSL.Text, out tx)) { errorProvider1.SetError(txtSL, "Yêu cầu nhập"); txtSL.Focus(); return false; }
            else errorProvider1.Clear();
            if (txtDGV.Text == "") { errorProvider1.SetError(txtDGV, "Yêu cầu nhập"); return false; }
            else errorProvider1.Clear();
            if (!float.TryParse(txtDGV.Text, out tx)) { errorProvider1.SetError(txtDGV, "Yêu cầu nhập"); txtDGV.Focus(); return false; }
            else errorProvider1.Clear();
            if (txtDGB.Text == "") { errorProvider1.SetError(txtDGB, "Yêu cầu nhập"); return false; }
            else errorProvider1.Clear();
            if (!float.TryParse(txtDGB.Text, out tx)) { errorProvider1.SetError(txtDGB, "Yêu cầu nhập"); txtDGB.Focus(); return false; }
            else errorProvider1.Clear();
            return true;
        }
        
       
      
        private void dgvPN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
            try
            {
                ClassApp.vt = e.RowIndex;
                if (ClassApp.vt == -1 || ClassApp.vt > ConnectSql.ds.Tables["dbo.tPhieuXuatHangHoaChiTiet"].Rows.Count) return;
                DataRow row = ConnectSql.ds.Tables["dbo.tPhieuXuatHangHoaChiTiet"].Rows[ClassApp.vt];
                txtMahh.Text = row["cMaHang"].ToString();
                txtDonVitinh.Text = row["cDonViTinh"].ToString();
                txtSL.Text = row["nSoLuong"].ToString();
                txtDGV.Text = row["nDonGiaVon"].ToString();
                txtTTGV.Text = row["nThanhTienGiaVon"].ToString();
                txtDGB.Text = row["nDonGiaBan"].ToString();
                txtTTGB.Text = row["nThanhTienGiaBan"].ToString();
                txtMctn.Text = row["cMaChungTuNhap"].ToString();
            }
            catch (Exception)
            {
                return;
            }
        }

        private void dgvPN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            splitContainer2.Panel1Collapsed = false;
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
            FrmTimKienPX frm = new FrmTimKienPX();
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

        private void txtMaNB_KeyDown(object sender, KeyEventArgs e)
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

        //main
        private void btnThemP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtMact.ReadOnly = false;
            txtSCT.ReadOnly = false;
            txtNgayCT.ReadOnly = false;
            txtLoaiCT.ReadOnly = false;
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
            splitContainer2.Panel1Collapsed = false;
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
                    string MaCT = txtLoaiCT.Text + "-" + txtSCT.Text + "-" + dt3;
                    txtMact.Text = MaCT;

                    float TS = float.Parse(txtThueXuat.Text.Trim().Replace(",", "."));
                    float TGTGT = float.Parse(txtThueGTGT.Text.Trim().Replace(",", "."));
                    string sql = "INSERT INTO tPhieuXuatHangHoa(cMaChungTu,cLoaiChungTu,dNgayChungTu," +
                        "cSoChungTu,cMaKhachHang,cTenKhachHang, cMaSoThue, cTaiKhoanNoGiaVon, cTaiKhoanCoGiaVon, cTaiKhoanNoGiaBan, " +
                        "cTaiKhoanCoGiaBan,cTaiKhoanCoGTGT, cDienGiai, cMatHang, nThueSuat, nThueGTGT, cSoSeri, cSoHoaDon" +
                        ") VALUES (N'" + txtMact.Text.Trim() + "',N'" + txtLoaiCT.Text.Trim() + "',N'" + NCT +
                        "',N'" + txtSCT.Text.Trim() + "',N'" + txtMaNB.Text.Trim() + "',N'" + txtTenNB.Text.Trim() + "',N'" +
                        txtMaST.Text.Trim() + "',N'" + txtTKNGV.Text.Trim() + "',N'" + txtTKCGV.Text.Trim() + "',N'"
                        + txtTKNGB.Text.Trim() + "',N'" + txtTKCGB.Text.Trim() + "',N'"
                        + txtTKCGTGT.Text.Trim() + "',N'" + txtDienGiai.Text.Trim() + "',N'" + txtMH.Text.Trim() + "',N'"
                        + TS.ToString() + "',N'" + TGTGT.ToString() + "',N'"
                        + txtSosr.Text.Trim() + "',N'" + txtSoHD.Text.Trim() + "')";
                    ConnectSql.RunSQL(sql);
                    //ADD SUB
                    if (ConnectSql.RunSql == true)
                    {
                        double SL = double.Parse(txtSL.Text.Trim());
                        double DGV = double.Parse(txtDGV.Text.Trim());
                        double DGB = double.Parse(txtDGB.Text.Trim());
                        LoadData1();
                        DataRow row = ConnectSql.ds.Tables["dbo.tPhieuXuatHangHoaChiTiet"].NewRow();
                        row["cMaChungTu"] = txtMact.Text.Trim();
                        row["cMaHang"] = txtMahh.Text.Trim();
                        row["cDonViTinh"] = txtDonVitinh.Text.Trim();
                        row["nSoLuong"] = txtSL.Text.Trim();
                        row["nDonGiaVon"] = txtDGV.Text.Trim();
                        row["nThanhTienGiaVon"] = SL * DGV;
                        row["nDonGiaBan"] = txtDGB.Text.Trim();
                        row["nThanhTienGiaBan"] = SL * DGB;
                        row["cMaChungTuNhap"] = txtMctn.Text.Trim();


                        try
                        {
                            ConnectSql.ds.Tables["dbo.tPhieuXuatHangHoaChiTiet"].Rows.Add(row);
                            int rs = ConnectSql.adapter.Update(ConnectSql.ds.Tables["dbo.tPhieuXuatHangHoaChiTiet"]);
                            if (rs > 0)
                            {
                                MessageBox.Show("Thêm dữ liệu thành công!");
                                btnDungP.PerformClick();
                                ConnectSql.XoaNoiDung(this.groupControl2);
                                ConnectSql.bs.ResetBindings(true);
                                LoadData();
                                
                            }

                            else
                            {
                                MessageBox.Show("Thêm dữ liệu thất bại!");
                            }
                            ConnectSql.bs.ResetBindings(true);
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
                    string MaCT = txtLoaiCT.Text + "-" + txtSCT.Text + "-" + dt3;
                    txtMact.Text = MaCT;

                    float TS = float.Parse(txtThueXuat.Text.Trim().Replace(",", "."));
                    float TGTGT = float.Parse(txtThueGTGT.Text.Trim().Replace(",", "."));
                    string sql = "UPDATE tPhieuXuatHangHoa SET cLoaiChungTu = N'" + txtLoaiCT.Text.Trim() + "'" +
                      ",dNgayChungTu = N'" + txtNgayCT.Text.Trim() + "'" +
                      ",cSoChungTu = N'" + txtSCT.Text.Trim() + "'" +
                      ",cMaKhachHang = N'" + txtMaNB.Text.Trim() + "'" +
                      ",cTenKhachHang = N'" + txtTenNB.Text.Trim() + "'" +
                      ",cMaSoThue = N'" + txtMaST.Text.Trim() + "'" +
                        ",cTaiKhoanNoGiaVon = N'" + txtTKNGV.Text.Trim() + "'" +
                      ",cTaiKhoanCoGiaVon = N'" + txtTKCGV.Text.Trim() + "'" +
                        ",cTaiKhoanNoGiaBan = N'" + txtTKNGB.Text.Trim() + "'" +
                      ",cTaiKhoanCoGiaBan = N'" + txtTKCGB.Text.Trim() + "'" +
                      ",cTaiKhoanCoGTGT = N'" + txtTKCGTGT.Text.Trim() + "'" +
                      ",cDienGiai = N'" + txtDienGiai.Text.Trim() + "'" +
                        ",cMatHang = N'" + txtMH.Text.Trim() + "'" +
                         ",nThueSuat = N'" + TS.ToString() + "'" +
                      ",nThueGTGT = N'" + TGTGT.ToString() + "'" +
                      ",cSoSeRi = N'" + txtSosr.Text.Trim() + "'" +
                      ",cSoHoaDon = N'" + txtSoHD.Text.Trim() + "'" +
                      " where cMaChungTu = N'" + txtMact.Text.Trim() + "'";
                    ConnectSql.RunSQL(sql);
                    if (ConnectSql.RunSql == true)
                    {
                        MessageBox.Show("Chỉnh sửa phiếu nhập thành công");
                        btnDung.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Chỉnh sửa phiếu nhập Thất bại");
                    }
                }

            }
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

            ConnectSql.bs.ResetCurrentItem();
            errorProvider1.Clear();
            LoadData();
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
        double tongtien;
        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //lấy dữ liệu cho reporrt 
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.Connection = ConnectSql.Con; //Gán kết nối
            string query = "select tPhieuXuatHangHoa.cMaChungTu,cSoChungTu,dNgayChungTu,nThueGTGT,"
                + "cTenKhachHang,cMaSoThue,cDienGiai" +
                ", tPhieuXuatHangHoaChiTiet.cMaChungTu, tPhieuXuatHangHoaChiTiet.cMaHang," +
                "tPhieuXuatHangHoaChiTiet.nSoLuong, tPhieuXuatHangHoaChiTiet.nDonGiaBan,tPhieuXuatHangHoaChiTiet.cDonViTinh,tPhieuXuatHangHoaChiTiet.nThanhTienGiaBan, " +
                "tDanhMucHangHoa.cTenHang,  tDanhMucHangHoa.cMaHang, sum(tPhieuXuatHangHoaChiTiet.nThanhTienGiaBan)/10 + sum (tPhieuXuatHangHoaChiTiet.nThanhTienGiaBan) as TongThanhTien " +
                "from tPhieuXuatHangHoa, tPhieuXuatHangHoaChiTiet, tDanhMucHangHoa where tPhieuXuatHangHoa.cMaChungTu=tPhieuXuatHangHoaChiTiet.cMaChungTu " +
               " AND tPhieuXuatHangHoaChiTiet.cMaHang = tDanhMucHangHoa.cMaHang AND tPhieuXuatHangHoa.cMaChungTu = @mapn " +
              "group by tPhieuXuatHangHoa.cMaChungTu,tPhieuXuatHangHoa.cSoChungTu," +

               " tPhieuXuatHangHoa.dNgayChungTu,tPhieuXuatHangHoa.nThueGTGT," +
              "  tPhieuXuatHangHoa.cTenKhachHang,tPhieuXuatHangHoa.cMaSoThue," +
             "   tPhieuXuatHangHoa.cDienGiai, tPhieuXuatHangHoaChiTiet.cMaChungTu," +
              "  tPhieuXuatHangHoaChiTiet.cMaHang,tPhieuXuatHangHoaChiTiet.nSoLuong, " +
              "   tPhieuXuatHangHoaChiTiet.nDonGiaBan, tPhieuXuatHangHoaChiTiet.cDonViTinh," +
              "   tPhieuXuatHangHoaChiTiet.nThanhTienGiaBan, tDanhMucHangHoa.cTenHang,  tDanhMucHangHoa.cMaHang ";

            // cmd.Parameters.Clear();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@mapn", txtMact.Text);

            DataTable dt = new DataTable("BangPhieuXuat");
            dt.Load(cmd.ExecuteReader());
            RptPhieuXuat baocao = new RptPhieuXuat();

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
            FrmBaoCaoXuatHang PX = new FrmBaoCaoXuatHang();
            PX.ctrPX.ReportSource = baocao;
            PX.Show();
            tongtien = 0;
            tt = 0;
        }

   

        //sub
        private void btnThem_Click_1(object sender, EventArgs e)
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

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            DialogResult warn = MessageBox.Show("Bạn có thực sự muốn xóa dòng này không?", "Cảnh báo!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (warn == DialogResult.Yes)
            {
                try
                {
              
                    DataRow row = ConnectSql.ds.Tables["dbo.tPhieuXuatHangHoaChiTiet"].Rows[ClassApp.vt];
                    row.Delete();
                    int rs = ConnectSql.adapter.Update(ConnectSql.ds.Tables["dbo.tPhieuXuatHangHoaChiTiet"]);
                    if (rs > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!");
                        ConnectSql.XoaNoiDung(this.groupControl2);
                    }
                }
                catch (Exception)
                {
                    throw;
                    MessageBox.Show("Xóa dữ liệu thất bại!");
                }

            }
        }

        private void btnSua_Click_1(object sender, EventArgs e)
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

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            if (strFormState == "ADDING" && strMainState == "Turn off")
            {
                if (textEmptySub() == true)
                {
                    LoadData1();
                    double SL = double.Parse(txtSL.Text.Trim());
                    double DGV = double.Parse(txtDGV.Text.Trim());
                    double DGB = double.Parse(txtDGB.Text.Trim());
                    DataRow row = ConnectSql.ds.Tables["dbo.tPhieuXuatHangHoaChiTiet"].NewRow();
                    row["cMaChungTu"] = txtMact.Text.Trim();
                    row["cMaHang"] = txtMahh.Text.Trim();
                    row["cDonViTinh"] = txtDonVitinh.Text.Trim();
                    row["nSoLuong"] = txtSL.Text.Trim();
                    row["nDonGiaVon"] = txtDGV.Text.Trim();
                    row["nThanhTienGiaVon"] = SL * DGV;
                    row["nDonGiaBan"] = txtDGB.Text.Trim();
                    row["nThanhTienGiaBan"] = SL * DGB;
                    row["cMaChungTuNhap"] = txtMctn.Text.Trim();
                    try
                    {
                        ConnectSql.ds.Tables["dbo.tPhieuXuatHangHoaChiTiet"].Rows.Add(row);
                        int rs = ConnectSql.adapter.Update(ConnectSql.ds.Tables["dbo.tPhieuXuatHangHoaChiTiet"]);
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
                    double DGV = double.Parse(txtDGV.Text.Trim());
                    double DGB = double.Parse(txtDGB.Text.Trim());
                    DataRow row = ConnectSql.ds.Tables["dbo.tPhieuXuatHangHoaChiTiet"].Rows[ClassApp.vt];
                    row.BeginEdit();
                    row["cMaChungTu"] = txtMact.Text.Trim();
                    row["cMaHang"] = txtMahh.Text.Trim();
                    row["cDonViTinh"] = txtDonVitinh.Text.Trim();
                    row["nSoLuong"] = txtSL.Text.Trim();
                    row["nDonGiaVon"] = txtDGV.Text.Trim();
                    row["nThanhTienGiaVon"] = SL * DGV;
                    row["nDonGiaBan"] = txtDGB.Text.Trim();
                    row["nThanhTienGiaBan"] = SL * DGB;
                    row["cMaChungTuNhap"] = txtMctn.Text.Trim();
                    row.EndEdit();
                    try
                    {

                        int rs = ConnectSql.adapter.Update(ConnectSql.ds.Tables["dbo.tPhieuXuatHangHoaChiTiet"]);
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
            ConnectSql.bs.Dispose();
            Clearbinding();
            Binding();
            txtP.EditValue = (ConnectSql.bs.Position + 1) + "/" + ConnectSql.bs.Count;
        }
                
        private void btnDung_Click_1(object sender, EventArgs e)
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



        //Postion
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
        public static string MH;
        public static string DVT;
        public static string DG;
        public static string MCTN;
        private void txtMahh_KeyDown(object sender, KeyEventArgs e)
        {
            if (strFormState == "ADDING")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    FrmTimKiemCTPN frm = new FrmTimKiemCTPN();
                    frm.ShowDialog();
                    txtMahh.Text = MH;
                    txtDonVitinh.Text = DVT;
                    txtDGB.Text = DG;
                    txtMctn.Text = MCTN;
                    if (txtDGB.Text == "NaN")
                    {
                        txtDGB.Text = "0";
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
                    txtDGB.Text = DG;
                    txtMctn.Text = MCTN;
                    if (txtDGB.Text == "NaN")
                    {
                        txtDGB.Text = "0";
                    }
                }
            }
        }

        private void btnNgay_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmNSL fr = new FrmNSL();
            fr.ShowDialog();
            FrmPXHH frm = new FrmPXHH();
            frm.MdiParent = this.MdiParent;
            frm.Show();
            this.Close();
        }

        private void FrmPXHH_Activated(object sender, EventArgs e)
        {
            ConnectSql.bs.Dispose();
            Clearbinding();
            Binding();
            ConnectSql.bs.Position = 0;
            txtP.EditValue = (ConnectSql.bs.Position + 1) + "/" + ConnectSql.bs.Count;
           
        }
    }
}