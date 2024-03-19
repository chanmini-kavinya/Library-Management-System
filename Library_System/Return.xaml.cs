using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Library_System
{
    /// <summary>
    /// Interaction logic for Return.xaml
    /// </summary>
    public partial class Return : Page
    {
        String isbn;
        bool memAvailable = false;
        String reference;
        String title;
        String available;

        public Return()
        {
            InitializeComponent();
            txt_returned_date.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        Database db = new Database();
        ScanQRcode scn = new ScanQRcode();

        private void btn_scan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).frame1.Content = scn;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }
        }

        private void btn_find_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var mID = new Regex("^[0-9a-zA-Z]+$");
                var aNO = new Regex("^[0-9a-zA-Z]+$");
                if (string.IsNullOrEmpty(txt_mid.Text))
                {
                    MessageBox.Show("Please Enter Member ID", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
                }
                else if (!mID.IsMatch(txt_mid.Text))
                {
                    MessageBox.Show("Invalied Member ID", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
                }
                else if (string.IsNullOrEmpty(txt_accno.Text))
                {
                    MessageBox.Show("Please Enter Accession No", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
                }
                else if (!aNO.IsMatch(txt_accno.Text))
                {
                    MessageBox.Show("Invalied Accession No", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
                }
                else
                {
                    db.openConnection();
                    String sqlSelectQuery2 = "Select * from Book_Copy where Accession_No ='" + txt_accno.Text + "'";
                    SqlCommand cmd2 = new SqlCommand(sqlSelectQuery2, db.con);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (dr2.Read())
                    {
                        reference = (dr2["Reference"].ToString());
                        isbn = (dr2["ISBN"].ToString());
                        title = isbn.ToString();
                    }
                    else
                    {
                        isbn = null;
                    }
                    dr2.Close();
                    db.closeConnection();

                    db.openConnection();
                    String memberQuery = "Select * from Member where Member_ID ='" + txt_mid.Text + "'";
                    SqlCommand cmdMem = new SqlCommand(memberQuery, db.con);
                    SqlDataReader drMem = cmdMem.ExecuteReader();
                    if (drMem.Read())
                    {
                        memAvailable = true;
                    }
                    else
                    {
                        memAvailable = false;
                    }
                    drMem.Close();
                    db.closeConnection();


                    if (isbn != null && memAvailable == true)
                    {

                        db.openConnection();
                        String sqlSelectQuery3 = "Select * from Book where ISBN ='" + isbn.ToString() + "'";
                        SqlCommand cmd3 = new SqlCommand(sqlSelectQuery3, db.con);
                        SqlDataReader dr3 = cmd3.ExecuteReader();
                        if (dr3.Read())
                        {
                            txt_reference.Text = reference;
                            txt_title.Text = (dr3["Title"].ToString());

                        }
                        dr3.Close();
                        db.closeConnection();

                        db.openConnection();
                        String sqlSelectQuery4 = "Select * from Borrow where Accession_No ='" + txt_accno.Text + "'";
                        SqlCommand cmd4 = new SqlCommand(sqlSelectQuery4, db.con);
                        SqlDataReader dr4 = cmd4.ExecuteReader();
                        if (dr4.Read())
                        {


                            DateTime getIssueDate = (DateTime)dr4["Issue_Date"];
                            string formattedIssueDate = getIssueDate.ToString("yyyy-MM-dd");
                            txt_issue_date.Text = formattedIssueDate;

                            DateTime getDueReturnDate = (DateTime)dr4["Due_Return_Date"];
                            string formattedDueReturnDate = getDueReturnDate.ToString("yyyy-MM-dd");
                            txt_due_rtn_date.Text = formattedDueReturnDate;
                            MessageBox.Show("Record founded", "Information", MessageBox.Buttons.OK, MessageBox.Icon.Info, MessageBox.AnimateStyle.FadeIn);

                        }
                        db.closeConnection();


                    }
                    else
                    {
                        if (memAvailable == false)
                        {
                            MessageBox.Show("There is no member found", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
                        }

                        else
                        {
                            MessageBox.Show("There is no record found", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
                        }
                        isbn = null;
                        reference = "";
                        memAvailable = false;
                        txt_title.Text = "";
                        txt_reference.Text = "";
                        txt_due_rtn_date.Text = "";
                        txt_issue_date.Text = "";

                    }

                }
            }

            catch (SqlException)
            {
                MessageBox.Show("Please Check the Fields", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }
            catch (Exception)
            {
                MessageBox.Show("Please Check the Fields", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }


        }

        private void btn_return_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var mID = new Regex("^[0-9a-zA-Z]+$");
                var aNO = new Regex("^[0-9a-zA-Z]+$");
                if (string.IsNullOrEmpty(txt_mid.Text))
                {
                    MessageBox.Show("Please Enter Member ID", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
                }
                else if (!mID.IsMatch(txt_mid.Text))
                {
                    MessageBox.Show("Invalied Member ID", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
                }
                else if (string.IsNullOrEmpty(txt_accno.Text))
                {
                    MessageBox.Show("Please Enter Accession No", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
                }
                else if (!aNO.IsMatch(txt_accno.Text))
                {
                    MessageBox.Show("Invalied Accession No", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
                }
                else
                {

                    db.openConnection();
                    String sqlSelectQuery2 = "Select * from Book_Copy where Accession_No ='" + txt_accno.Text + "'";
                    SqlCommand cmd2 = new SqlCommand(sqlSelectQuery2, db.con);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (dr2.Read())
                    {
                        available = (dr2["Available"].ToString());
                    }
                    dr2.Close();
                    db.closeConnection();
                    if (available == "N")
                    {
                        DateTime returned_date = DateTime.ParseExact(txt_returned_date.Text, "yyyy-MM-dd", null);
                        db.openConnection();
                        SqlCommand cmd1 = new SqlCommand("update Borrow set Returned_Date = @Returned_Date where Accession_No = '" + txt_accno.Text + "' AND Returned_Date is null", db.con);

                        cmd1.Parameters.AddWithValue("@Returned_Date", returned_date);

                        cmd1.ExecuteNonQuery();
                        cmd1.Dispose();
                        db.closeConnection();
                        db.openConnection();


                        SqlCommand cmd3 = new SqlCommand("update Book_Copy set Available = @Available where Accession_No = '" + txt_accno.Text + "'", db.con);
                        cmd3.Parameters.AddWithValue("@Available", "Y");
                        int line = cmd3.ExecuteNonQuery();
                        cmd3.Dispose();
                        db.closeConnection();
                        if (line == 1)
                        {
                            MessageBox.Show("Book Return Succesfully", "Information", MessageBox.Buttons.OK, MessageBox.Icon.Info, MessageBox.AnimateStyle.FadeIn);
                        }
                        else
                        {
                            MessageBox.Show("Data not Inserted", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
                        }
                        txt_accno.Clear();
                        txt_mid.Clear();
                        txt_title.Clear();
                        txt_reference.Clear();
                    }
                    else if (available == "Y")
                    {
                        MessageBox.Show("This Book is Already Return", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);

                    }


                }
            }

            catch (SqlException)
            {
                MessageBox.Show("Please Check the Fields", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }
            catch (Exception)
            {
                MessageBox.Show("Please Check the Fields", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }
        }

        private void Return_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                txt_accno.Text = scn.scan_accno;
                scn.scan_accno = default;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }
            
        }
    }
}
