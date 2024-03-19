using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls.WebParts;

namespace Library_System
{ 
    class Database
    { 
        public SqlConnection con;
        private SqlCommand cmd;
        private SqlDataAdapter da;

        public Database()
        {           
            con = new SqlConnection("Data Source=ICT_GEN_5050;Initial Catalog=Library;Integrated Security=True");
        }
        public void openConnection()
        {
            con.Open();
        }
        public void closeConnection()
        {
            con.Close();
        }

        public int save_update_delete(string a)
        {
            openConnection();
            cmd = new SqlCommand(a, con);
            int i = cmd.ExecuteNonQuery();
            closeConnection();
            return i;
        }
        public DataTable getData(string a)
        {
            openConnection();
            da = new SqlDataAdapter(a, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            closeConnection();
            return dt;
        }

        public string getValue(string a)
        {
            openConnection();
            cmd = new SqlCommand(a, con);
            string x = Convert.ToString(cmd.ExecuteScalar());
            cmd.Dispose();
            closeConnection();
            return x;
        }

        public class member
        {
            public string Member_Type { get; set; }
            public string Name { get; set; }
            public string Status { get; set; }
            public string NIC { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
            public string Mobile_No { get; set; }
        }

        public member getRecord(string a, int mid)
        {
            member x = new member();
            openConnection();
            cmd = new SqlCommand(a, con);
            cmd.Parameters.AddWithValue("@mid", mid);
            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    x.Member_Type = Reader["Member_Type"].ToString();
                    x.Name = Reader["Name"].ToString();
                    x.Status = Reader["Status"].ToString();
                    x.NIC = Reader["NIC"].ToString();
                    x.Address = Reader["Address"].ToString();
                    x.Email = Reader["Email"].ToString();
                    x.Mobile_No = Reader["Mobile_No"].ToString();
                }

                closeConnection();
            }

            return x;
        }

        public class book
        {
            //public string ISBN { get; set; }
            public string Title { get; set; }
            public string Class_NO { get; set; }
            public string Letter { get; set; }
            public string Author_id { get; set; }

        }
        public book GetRecord1(string a, string isbn)
        {
            book y = new book();
            openConnection();
            cmd = new SqlCommand(a, con);
            cmd.Parameters.AddWithValue("@isbn", isbn);
            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    //y.ISBN = Reader["ISBN"].ToString();
                    y.Title = Reader["Title"].ToString();
                    y.Class_NO = Reader["Class_No"].ToString();
                    y.Letter = Reader["Letter"].ToString();
                    y.Author_id = Reader["Author_ID"].ToString();

                }

                closeConnection();
            }

            return y;
        }
    }

}
