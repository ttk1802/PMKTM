using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using CommonlibHCE;

namespace Acounting
{
    public partial class frmBangDieuKhien : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmBangDieuKhien()
        {
            InitializeComponent();
        }
        void openFrm(Type typeForm)
        {
            foreach (var frm in MdiChildren)
            {
                if (frm.GetType()==typeForm)
                {
                    frm.Activate();
                    return;
                }
            }
            Form f = (Form)Activator.CreateInstance(typeForm);
            f.MdiParent = this;
            f.Show();

        }
        
        private void btnDN_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            if (ConnectSql.succceed == false)
            {
                FrmLG frm = new FrmLG();
                frm.ShowDialog();
            }
            else
                MessageBox.Show("Bạn đã đăng nhập");
           
            

        }

        private void MdiFrm_Load(object sender, EventArgs e)
        {

        }

        private void btnKH_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ConnectSql.succceed == false)
            {
                DialogResult result = MessageBox.Show("Vui lòng đăng nhập", "Thông báo", MessageBoxButtons.OKCancel,
                   MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.OK)
                {
                    FrmLG frm = new FrmLG();
                    frm.ShowDialog();
                    
                }
            }
            else
            {
                openFrm(typeof(FrmDMKH));
            }
        }

        private void btnThoat_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát chương trình không", "Thoát", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnHH_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ConnectSql.succceed == false)
            {
                DialogResult result = MessageBox.Show("Vui lòng đăng nhập", "Thông báo", MessageBoxButtons.OKCancel,
                   MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.OK)
                {
                    FrmLG frm = new FrmLG();
                    frm.ShowDialog();

                }
            }
            else
            {
                openFrm(typeof(FrmDMHH));
            }
        }

        private void btnNSL_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (ConnectSql.succceed == false)
            {
                DialogResult result = MessageBox.Show("Vui lòng đăng nhập", "Thông báo", MessageBoxButtons.OKCancel,
                   MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.OK)
                {
                    FrmLG frm = new FrmLG();
                    frm.ShowDialog();

                }
            }
            else
            {
                FrmNSL frm = new FrmNSL();
                frm.ShowDialog();
            }
           
        }

        private void btnDX_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn đăng xuất", "Thông báo", MessageBoxButtons.OKCancel,
                 MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.OK)
            {
                ConnectSql.Disconnect();
                ConnectSql.succceed = false;
            }
        }

        private void btnTK_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ConnectSql.succceed == false)
            {
                DialogResult result = MessageBox.Show("Vui lòng đăng nhập", "Thông báo", MessageBoxButtons.OKCancel,
                   MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.OK)
                {
                    FrmLG frm = new FrmLG();
                    frm.ShowDialog();

                }
            }
            else
            {
                openFrm(typeof(frmDanhMucTaiKhoan));
            }
        }

        private void btnKho_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ConnectSql.succceed == false)
            {
                DialogResult result = MessageBox.Show("Vui lòng đăng nhập", "Thông báo", MessageBoxButtons.OKCancel,
                   MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.OK)
                {
                    FrmLG frm = new FrmLG();
                    frm.ShowDialog();

                }
            }
            else
            {
               // openFrm(typeof(FrmDMKHO));
            }
        }

        private void btnPhieuNhap_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ConnectSql.succceed == false)
            {
                DialogResult result = MessageBox.Show("Vui lòng đăng nhập", "Thông báo", MessageBoxButtons.OKCancel,
                   MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.OK)
                {
                    FrmLG frm = new FrmLG();
                    frm.ShowDialog();

                }
            }
            else
            {
                openFrm(typeof(frmPhieuNhapHangHoa));
            }
        }

        private void btnPhieuXuat_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ConnectSql.succceed == false)
            {
                DialogResult result = MessageBox.Show("Vui lòng đăng nhập", "Thông báo", MessageBoxButtons.OKCancel,
                   MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.OK)
                {
                    FrmLG frm = new FrmLG();
                    frm.ShowDialog();

                }
            }
            else
            {
                openFrm(typeof(FrmPXHH));
            }
        }
    }
}