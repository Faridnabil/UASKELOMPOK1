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
    public partial class MasterUser: Form
    {

        private void Form1_Load(object sender, EventArgs e)
        {
            tampilData();
        }

        //Data null
        void dataNull()
        {
            txtId.Text = null;
            txtNama.Text = null;
            txtEmail.Text = null;
            txtPassword.Text = null;
            DropdownLevel.Text = null;
            txtSearch.Text = null;

            tampilData();
        }

        //Menampilkan data
        public void tampilData()
        {
            try
            {
                Koneksi.Conn.Open();
                SqlDataAdapter sqlDisplay = new SqlDataAdapter("EXEC viewUser", Koneksi.Conn);
                sqlDisplay.SelectCommand.ExecuteNonQuery();
                DataTable data = new DataTable();
                sqlDisplay.Fill(data);
                dtGrid.DataSource = data;

                dtGrid.AllowUserToAddRows = false;
                dtGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dtGrid.Columns[0].HeaderText = "#";
                dtGrid.Columns[1].HeaderText = "Nama";
                dtGrid.Columns[2].HeaderText = "Email";
                dtGrid.Columns[3].HeaderText = "Password";
                dtGrid.Columns[4].HeaderText = "Level";
                dtGrid.Refresh();
                Koneksi.Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }

        }

        //Data Grid
        private void dtGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dtGrid.Rows[e.RowIndex];
                txtId.Text = row.Cells["id"].Value.ToString();
                txtNama.Text = row.Cells["name"].Value.ToString();
                txtEmail.Text = row.Cells["email"].Value.ToString();
                txtPassword.Text = row.Cells["password"].Value.ToString();
                DropdownLevel.Text = row.Cells["levelU"].Value.ToString();
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
                SqlDataAdapter sqlDisplay = new SqlDataAdapter("EXEC cariUser @name", Koneksi.Conn);
                sqlDisplay.SelectCommand.Parameters.AddWithValue("name", txtSearch.Text.Trim());
                sqlDisplay.SelectCommand.ExecuteNonQuery();
                DataTable data = new DataTable();
                sqlDisplay.Fill(data);
                dtGrid.DataSource = data;

                txtId.DataBindings.Add("Text", data, "id");
                txtNama.DataBindings.Add("Text", data, "name");
                txtEmail.DataBindings.Add("Text", data, "email");
                txtPassword.DataBindings.Add("Text", data, "password");
                DropdownLevel.DataBindings.Add("Text", data, "levelU");

                txtId.DataBindings.Clear();
                txtNama.DataBindings.Clear();
                txtEmail.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                DropdownLevel.DataBindings.Clear();

                Koneksi.Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        //Input data
        private void btnTambah_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNama.Text.Trim() == "" || txtEmail.Text.Trim() == "" || txtPassword.Text.Trim() == "" || DropdownLevel.Text.Trim() == "")
                {
                    MessageBox.Show("Data Belum Lengkap!");
                }
                else
                {
                    Koneksi.Conn.Open();
                    SqlCommand cmd = new SqlCommand("EXEC inputUser @name, @email, @password, @level ", Koneksi.Conn);
                    cmd.Parameters.AddWithValue("@name", txtNama.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());
                    cmd.Parameters.AddWithValue("@level", DropdownLevel.Text.Trim());

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

        //Edit Data
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNama.Text.Trim() == "" || txtEmail.Text.Trim() == "" || txtPassword.Text.Trim() == "" || DropdownLevel.Text.Trim() == "")
                {
                    MessageBox.Show("Data Belum Lengkap!");
                }
                else
                {
                    Koneksi.Conn.Open();
                    SqlCommand cmd = new SqlCommand("EXEC updateUser @id, @name, @email, @password, @level", Koneksi.Conn);
                    cmd.Parameters.AddWithValue("@id", txtId.Text.Trim());
                    cmd.Parameters.AddWithValue("@name", txtNama.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());
                    cmd.Parameters.AddWithValue("@level", DropdownLevel.Text.Trim());

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
                if (txtNama.Text.Trim() == "" || txtEmail.Text.Trim() == "" || txtPassword.Text.Trim() == "" || DropdownLevel.Text.Trim() == "")
                {
                    MessageBox.Show("Data Belum Lengkap!");
                }
                else
                {
                    if (MessageBox.Show("Yakin akan menghapus : " + txtNama.Text + " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Koneksi.Conn.Open();
                        SqlCommand cmd = new SqlCommand("EXEC hapusUser @id", Koneksi.Conn);
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

        public MasterUser()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNav.Height = btnUser.Height;
            pnlNav.Top = btnUser.Top;
            btnUser.BackColor = Color.FromArgb(46, 51, 73);
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
        //Data user
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
