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
    public partial class SMasuk: Form
    {


        private void Form1_Load(object sender, EventArgs e)
        {
            tampilData();
            DataJenisSurat();
        }

        //relasi dengan tabel jenis surat
        void DataJenisSurat()
        {
            SqlCommand cmd;
            SqlDataAdapter da;
            SqlDataReader rd;
            Koneksi.Conn.Open();
           
            cmd = new SqlCommand("select * from tbl_jenisSurat", Koneksi.Conn);
            rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                DropdownPerihal.Items.Add(rd[2].ToString());
            }
            Koneksi.Conn.Close();
        }

        //Data null
        void dataNull()
        {
            txtId.Text = null;
            txtNomor.Text = null;
            date.Text = null;
            txtPengirim.Text = null;
            DropdownPerihal.Text = null;

            txtSearch.Text = null;
            tampilData();
        }

        //Menampilkan data
        public void tampilData()
        {
            try
            {
                Koneksi.Conn.Open();
                SqlDataAdapter sqlDisplay = new SqlDataAdapter("EXEC viewMasuk", Koneksi.Conn);
                sqlDisplay.SelectCommand.ExecuteNonQuery();
                DataTable data = new DataTable();
                sqlDisplay.Fill(data);
                dtGrid.DataSource = data;
                dtGrid.AllowUserToAddRows = false;
                dtGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dtGrid.Columns[0].HeaderText = "#";
                dtGrid.Columns[1].HeaderText = "Nomor Surat";
                dtGrid.Columns[2].HeaderText = "Tanggal";
                dtGrid.Columns[3].HeaderText = "Pengirim";
                dtGrid.Columns[4].HeaderText = "Perihal";
                dtGrid.Refresh();
                Koneksi.Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }

        }

        //Data grid
        private void dtGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dtGrid.Rows[e.RowIndex];
                txtId.Text = row.Cells["id"].Value.ToString();
                txtNomor.Text = row.Cells["noSmasuk"].Value.ToString();
                date.Text = row.Cells["tglMasuk"].Value.ToString();
                txtPengirim.Text = row.Cells["pengirim"].Value.ToString();
                DropdownPerihal.Text = row.Cells["perihal"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        //Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dataNull();
        }

        //Search
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Koneksi.Conn.Open();
                SqlDataAdapter sqlDisplay = new SqlDataAdapter("EXEC cariMasuk @nomor", Koneksi.Conn);
                sqlDisplay.SelectCommand.Parameters.AddWithValue("nomor", txtSearch.Text.Trim());
                sqlDisplay.SelectCommand.ExecuteNonQuery();
                DataTable data = new DataTable();
                sqlDisplay.Fill(data);
                dtGrid.DataSource = data;

                txtId.DataBindings.Add("Text", data, "id");
                txtNomor.DataBindings.Add("Text", data, "noSmasuk");
                date.DataBindings.Add("Text", data, "tglMasuk");
                txtPengirim.DataBindings.Add("Text", data, "pengirim");
                DropdownPerihal.DataBindings.Add("Text", data, "perihal");

                txtId.DataBindings.Clear();
                txtNomor.DataBindings.Clear();
                date.DataBindings.Clear();
                txtPengirim.DataBindings.Clear();
                DropdownPerihal.DataBindings.Clear();

                Koneksi.Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        //input data
        private void btnTambah_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNomor.Text.Trim() == "" || date.Text.Trim() == "" || txtPengirim.Text.Trim() == "" || DropdownPerihal.Text.Trim() == "")
                {
                    MessageBox.Show("Data Belum Lengkap!");
                }
                else
                {
                    Koneksi.Conn.Open();
                    SqlCommand cmd = new SqlCommand("EXEC inputMasuk @nomor,@tanggal,@pengirim,@perihal", Koneksi.Conn);
                    cmd.Parameters.AddWithValue("@nomor", txtNomor.Text.Trim());
                    cmd.Parameters.AddWithValue("@tanggal", date.Text.Trim());
                    cmd.Parameters.AddWithValue("@pengirim", txtPengirim.Text.Trim());
                    cmd.Parameters.AddWithValue("@perihal", DropdownPerihal.Text.Trim());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil menambahkan data");
                    Koneksi.Conn.Close();
                    dataNull();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        //Update data
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNomor.Text.Trim() == "" || date.Text.Trim() == "" || txtPengirim.Text.Trim() == "" || DropdownPerihal.Text.Trim() == "")
                {
                    MessageBox.Show("Data Belum Lengkap!");
                }
                else
                {
                    Koneksi.Conn.Open();
                    SqlCommand cmd = new SqlCommand("EXEC updateMasuk @id,@nomor,@tanggal,@pengirim,@perihal", Koneksi.Conn);
                    cmd.Parameters.AddWithValue("@id", txtId.Text.Trim());
                    cmd.Parameters.AddWithValue("@nomor", txtNomor.Text.Trim());
                    cmd.Parameters.AddWithValue("@tanggal", date.Text.Trim());
                    cmd.Parameters.AddWithValue("@pengirim", txtPengirim.Text.Trim());
                    cmd.Parameters.AddWithValue("@perihal", DropdownPerihal.Text.Trim());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil mengedit data");
                    Koneksi.Conn.Close();
                    dataNull();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        //hapus data
        private void btnHapus_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNomor.Text.Trim() == "" || date.Text.Trim() == "" || txtPengirim.Text.Trim() == "" || DropdownPerihal.Text.Trim() == "")
                {
                    MessageBox.Show("Data Belum Lengkap!");
                }
                else
                {
                    if (MessageBox.Show("Yakin akan menghapus : " + txtNomor.Text + " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Koneksi.Conn.Open();
                        SqlCommand cmd = new SqlCommand("EXEC hapusMasuk @id", Koneksi.Conn);
                        cmd.Parameters.AddWithValue("@id", txtId.Text.Trim());
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Berhasil menghapus data");
                        Koneksi.Conn.Close();
                        dataNull();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
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

        public SMasuk()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNav.Height = btnMasuk.Height;
            pnlNav.Top = btnMasuk.Top;
            btnMasuk.BackColor = Color.FromArgb(46, 51, 73);
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
        //Master User
        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnUser.Height;
            pnlNav.Top = btnUser.Top;
            btnUser.BackColor = Color.FromArgb(46, 51, 73);

            MasterUser view = new MasterUser();
            view.Show();
            this.Hide();
        }
        //Surat Masuk
        private void btnCalender_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnMasuk.Height;
            pnlNav.Top = btnMasuk.Top;
            btnMasuk.BackColor = Color.FromArgb(46, 51, 73);

            SMasuk view = new SMasuk();
            view.Show();
            this.Hide();
        }
        //Surat Keluar
        private void btnContactUs_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnKeluar.Height;
            pnlNav.Top = btnKeluar.Top;
            btnKeluar.BackColor = Color.FromArgb(46, 51, 73);


            SKeluar view = new SKeluar();
            view.Show();
            this.Hide();
        }
        //Jenis Surat
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

        private void dtGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCetak_Click(object sender, EventArgs e)
        {
            ReportViewMasuk report = new ReportViewMasuk();
            report.Show();
        }
    }
}
