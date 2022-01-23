using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dashboard
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmail.Text.Length == 0 || txtPassword.Text.Length == 0)
                {
                    MessageBox.Show("Email atau Password anda salah!");
                }
                else
                {
                    Koneksi.Conn.Open();
                    SqlCommand sqlLogin = new SqlCommand("Select Email, Password from TBL_USER where Email = '" + txtEmail.Text + "' and password = @password", Koneksi.Conn);
                    sqlLogin.Parameters.AddWithValue("@password", txtPassword.Text);
                    SqlDataReader dr;
                    dr = sqlLogin.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("Selamat datang Di Suratku!!!");
                        Dashboard db = new Dashboard();
                        db.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Username atau Password anda salah!!!");
                    }
                    Koneksi.Conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }
    }
}
