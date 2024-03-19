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
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace Library_System
{
    /// <summary>
    /// Interaction logic for Reminder.xaml
    /// </summary>
    public partial class Reminder : Page
    {
        public Reminder()
        {
            InitializeComponent();
            fillcombobox();
        }

        Database db = new Database();
        public void fillcombobox()
        {
            try
            {
                string sql = "select * from Borrow where Due_Return_Date < CAST( GETDATE() AS Date ) AND Returned_Date is null ";

                db.openConnection();
                SqlCommand cmd = new SqlCommand(sql, db.con);
                SqlDataReader myreader;
            
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    int Member_ID = myreader.GetInt32(1);
                    cmb_mid.Items.Add(Member_ID);
                }

                db.closeConnection();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }

        }


        private void cmb_mid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
  
        }

        private void btn_send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com",587);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("kavishasaumi@gmail.com", "saumikavisha123");
                MailMessage msgobj = new MailMessage();
                msgobj.To.Add(txt_email.Text);
                msgobj.From = new MailAddress("kavishasaumi@gmail.com");
                msgobj.Body = txt_body.Text;
                client.Send(msgobj);
                
                MessageBox.Show("Message has been sent successfully", "", MessageBox.Buttons.OK, MessageBox.Icon.Info, MessageBox.AnimateStyle.FadeIn);



            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }
        }

        private void cmb_mid_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string sql = "select * from Member where Member_ID = '" + cmb_mid.Text + "' order by Member_ID ASC ";

                db.openConnection();
                SqlCommand cmd = new SqlCommand(sql, db.con);
                SqlDataReader myreader;
                           
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {

                    string Name = myreader.GetString(2);
                    string Email = myreader.GetString(6);
                    txt_name.Text = Name;
                    txt_email.Text = Email;



                }
                txt_body.Text = "Dear Mr./Mrs. '" + txt_name.Text + "', " +
                                Environment.NewLine +
                                Environment.NewLine +
                                "Some of the books you have borrowed from the library are overdue.Please return the books as soon as possible" +
                                Environment.NewLine +
                                Environment.NewLine +
                                "Sincerely Yours," +
                                Environment.NewLine +
                                "Library.";



                db.closeConnection();



            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }

        }
    }
}
