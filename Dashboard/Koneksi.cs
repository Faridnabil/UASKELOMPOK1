using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Dashboard
{
    class Koneksi
    {
        public static SqlConnection Conn = new SqlConnection("Data Source= DESKTOP-GQUT03P\\SQLEXPRESS; initial catalog=DB_UASSURAT; integrated security=true; MultipleActiveResultSets=true");
        ///public static SqlConnection Conn = new SqlConnection("Data Source= LAPTOP-7019HN7R\\SQLEXPRESS; initial catalog=DB_UASSURAT; integrated security=true; MultipleActiveResultSets=true");
    }
}   
