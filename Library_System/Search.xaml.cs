using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;


namespace Library_System
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Page
    {
   
        public Search()
        {
            InitializeComponent();
            fillcombobox();
            
            
        }

        Database db = new Database();

        public void fillcombobox()
        {
            try
            {
                string sql = "select Book.*,Author.Name from Book inner join Author on Book.Author_ID = Author.Author_ID;";
                SqlCommand cmd = new SqlCommand(sql, db.con);
                SqlDataReader myreader;
            
                
                db.openConnection();
                myreader = cmd.ExecuteReader();
                while(myreader.Read())
                {
                string Title = myreader.GetString(1);
                    cmb_title.Items.Add(Title);
                }


                db.closeConnection();
            }
               
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }
            
        }
        
        
        
        private void cmb_title_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
              
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmb_title_DropDownClosed(object sender, EventArgs e)
        {
            
            try
            {

                db.openConnection();
                string sql = "select Book.*,Author.Name from Book inner join Author on Book.Author_ID = Author.Author_ID where Title = '" + cmb_title.Text + "' order by Title ASC ; ";
                SqlCommand cmd = new SqlCommand(sql, db.con);
                SqlDataReader myreader = cmd.ExecuteReader();

            

                while (myreader.Read())

                {

                    string Class_NO = myreader.GetString(3);
                    string Letter = myreader.GetString(4);
                    string Name = myreader.GetString(5);

                    txt_classno.Text = Class_NO;
                    txt_letter.Text = Letter;
                    txt_author.Text = Name;

                }
            
                db.closeConnection();
                db.openConnection();
                cmd.CommandText = "select Book_Copy.*from Book_Copy inner join Book on Book.ISBN = Book_Copy.ISBN where Title = '" + cmb_title.Text + "' ;";
                cmd.Connection = db.con;
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable("Book_Copy");
                da.SelectCommand = cmd;
                da.Fill(dt);
                db.closeConnection();
                datagridveiw1.ItemsSource = dt.DefaultView;
            }



            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }


        }
    }

    
    
}
