using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace Dashboard
{
    public partial class Dashboard : Form
    {

        private void Form1_Load(object sender, EventArgs e)
        {
            totalSuratMasuk();
            totalSuratKeluar();
            totalUser();
        }

        void totalUser()
        {
            if(Koneksi.Conn.State == ConnectionState.Closed)
            {
                Koneksi.Conn.Open();
            }
            
            SqlCommand cmd = new SqlCommand("select * from tbl_user", Koneksi.Conn);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable data = new DataTable();
            da.SelectCommand = cmd;
            data.Clear();
            da.Fill(data);
            labelUser.Text = data.Rows.Count.ToString();
            Koneksi.Conn.Close();
        }
        void totalSuratMasuk()
        {
            if (Koneksi.Conn.State == ConnectionState.Closed)
            {
                Koneksi.Conn.Open();
            }
            SqlCommand cmd = new SqlCommand("select * from tbl_suratMasuk", Koneksi.Conn);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable data = new DataTable();
            da.SelectCommand = cmd;
            data.Clear();
            da.Fill(data);
            labelMasuk.Text = data.Rows.Count.ToString();
            Koneksi.Conn.Close();
        }
        void totalSuratKeluar()
        {
            if (Koneksi.Conn.State == ConnectionState.Closed)
            {
                Koneksi.Conn.Open();
            }
            SqlCommand cmd = new SqlCommand("select * from tbl_suratKeluar", Koneksi.Conn);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable data = new DataTable();
            da.SelectCommand = cmd;
            data.Clear();
            da.Fill(data);
            labelKeluar.Text = data.Rows.Count.ToString();
            Koneksi.Conn.Close();
        }


        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
         (
               int nLeftRect,
               int nTopRect,
               int nRightRect,
               int nBottomRect,
               int nWidthEllipse,
               int nHeightEllipse
         );

        public Dashboard()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNav.Height = btnDashbord.Height;
            pnlNav.Top = btnDashbord.Top;
            pnlNav.Left = btnDashbord.Left;
            btnDashbord.BackColor = Color.FromArgb(46, 51, 73);
        }



        //Background menu active
        //Dasboard
        private void btnDashbord_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnDashbord.Height;
            pnlNav.Top = btnDashbord.Top;
            pnlNav.Left = btnDashbord.Left;
            btnDashbord.BackColor = Color.FromArgb(46, 51, 73);

            Dashboard view = new Dashboard();
            view.Show();
            this.Hide();
        }
        //Master user
        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnUser.Height;
            pnlNav.Top = btnUser.Top;
            btnUser.BackColor = Color.FromArgb(46, 51, 73);

            MasterUser view = new MasterUser();
            view.Show();
            this.Hide();
        }
        //Surat masuk
        private void btnCalender_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnMasuk.Height;
            pnlNav.Top = btnMasuk.Top;
            btnMasuk.BackColor = Color.FromArgb(46, 51, 73);

            SMasuk view = new SMasuk();
            view.Show();
            this.Hide();

        }
        //Surat keluar
        private void btnContactUs_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnKeluar.Height;
            pnlNav.Top = btnKeluar.Top;
            btnKeluar.BackColor = Color.FromArgb(46, 51, 73);

            SKeluar view = new SKeluar();
            view.Show();
            this.Hide();
        }
        //Jenis surat
        private void btnJenis_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnJenis.Height;
            pnlNav.Top = btnJenis.Top;
            btnJenis.BackColor = Color.FromArgb(46, 51, 73);

            JenisSurat view = new JenisSurat();
            view.Show();
            this.Hide();
        }


        //Meni kiri active
        private void btnDashbord_Leave(object sender, EventArgs e)
        {
            btnDashbord.BackColor = Color.FromArgb(24, 30, 54);
        }
        private void btnAnalytics_Leave(object sender, EventArgs e)
        {
            btnUser.BackColor = Color.FromArgb(24, 30, 54);
        }
        private void btnCalender_Leave(object sender, EventArgs e)
        {
            btnMasuk.BackColor = Color.FromArgb(24, 30, 54);
        }
        private void btnContactUs_Leave(object sender, EventArgs e)
        {
            btnKeluar.BackColor = Color.FromArgb(24, 30, 54);
        }
        private void btnJenis_Leave(object sender, EventArgs e)
        {
            btnJenis.BackColor = Color.FromArgb(24, 30, 54);
        }

        //Exit
        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
