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
    public partial class ReportViewer : Form
    {
        public ReportViewer()
        {
            InitializeComponent();
        }

        public void display()
        {
            Koneksi.Conn.Open();
            SuratMasuk report = new SuratMasuk();

            var ds = new dsPengguna();
            var table = ds.tbUser;

            sqlConnection.Open();
            SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM tbUser where username = '" + txtCari.Text + "'", sqlConnection);
            sqlda.SelectCommand.ExecuteNonQuery();
            sqlda.Fill(table);

            report.SetDataSource(ds);
            crystalReportViewer1.ReportSource = report;
            crystalReportViewer1.RefreshReport();
        }
    }
}
