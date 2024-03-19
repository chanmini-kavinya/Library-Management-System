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
using static Library_System.Database;

namespace Library_System
{
    /// <summary>
    /// Interaction logic for Book.xaml
    /// </summary>
    public partial class Book : Page
    {
        public Book()
        {
            InitializeComponent();
        }

        Database db = new Database();

        private void btn_save_a_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_accno_a.Text.Length == 0)
                {
                    error_msg_a.Text = "* Accession number cannot be blank";
                    txt_accno_a.Focus();
                }
                if (txt_isbn_a.Text.Length == 0)
                {
                    error_msg_a.Text = "* ISBN cannot be blank";
                    txt_isbn_a.Focus();
                }
                else if (txt_title_a.Text.Length == 0)
                {
                    error_msg_a.Text = "* Title cannot be blank";
                    txt_title_a.Focus();
                }
                else if (txt_classno_a.Text.Length == 0)
                {
                    error_msg_a.Text = "Class cannot be blank";
                    txt_classno_a.Focus();
                }
            
                else if (txt_author_a.Text.Length == 0)
                {
                    error_msg_a.Text = "* Name cannot be blank";
                    txt_author_a.Focus();
                }

                else
                {
                    int id;
                    int count = Convert.ToInt32(db.getValue("Select Count(*) from Author where Name='" + txt_author_a.Text + "' "));
                    if (count > 0)
                    {
                        id = Convert.ToInt32(db.getValue("Select Author_ID from Author where Name='" + txt_author_a.Text + "'"));
                    }
                    else
                    {
                        int s = db.save_update_delete("Insert into Author (Name) values('" + txt_author_a.Text + "')");
                        id = Convert.ToInt32(db.getValue("Select Author_ID from Author where Name='" + txt_author_a.Text + "'"));
                    }

                    
                    int count1 = Convert.ToInt32(db.getValue("Select Count(*) from Book where ISBN='" + txt_isbn_a.Text + "' "));
                    if (count1 == 0)
                    {
                        int i = db.save_update_delete("Insert into Book values ('" + txt_isbn_a.Text + "','" + txt_title_a.Text + "','" + id + "','" + txt_classno_a.Text + "','" + cmb_Letter_a.Text + "')");
                    }
                                        
                    int j = db.save_update_delete("Insert into Book_Copy values('" + txt_accno_a.Text + "','" + txt_isbn_a.Text + "','" + cmb_reference_a.Text + "','Y')");

                    //if ((i == 1)||(j==1)||(k==1)||(l==1))
                    if (j == 1)
                    {
                        MessageBox.Show("Book saved successfully", "Information", MessageBox.Buttons.OK, MessageBox.Icon.Info, MessageBox.AnimateStyle.FadeIn);
                        txt_accno_a.Clear(); txt_isbn_a.Clear(); txt_title_a.Clear(); txt_classno_a.Clear(); cmb_Letter_a.SelectedIndex=-1; txt_author_a.Clear(); cmb_reference_a.SelectedIndex=-1;
                    }
                    else
                        MessageBox.Show("Book cannot be saved", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
                }
            
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }
        }

        private void btn_find_u_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (txt_isbn_u.Text.Length == 0)
                {
                    error_msg_u.Text = "* Please enter ISBN ";
                    txt_isbn_u.Focus();
                }

                else
                {
                    error_msg_u.Text = "";

                
                        int count = Convert.ToInt32(db.getValue("Select Count(*) from Book where ISBN='" + txt_isbn_u.Text + "' "));

                        if (count > 0)
                        {
                            book y = db.GetRecord1("Select * from Book where ISBN=('" + txt_isbn_u.Text + "') ", txt_isbn_u.Text);
                            //txt_isbn_e.Text = y.ISBN;
                            txt_title_u.Text = y.Title;
                            txt_classno_u.Text = y.Class_NO;
                            cmb_Letter_u.Text = y.Letter;
                            txt_author_u.Text = db.getValue("Select Name from Author where Author_ID='" + y.Author_id + "' ");
                        }
                        else
                        {
                            MessageBox.Show("Record not found", "", MessageBox.Buttons.OK, MessageBox.Icon.Exclamation, MessageBox.AnimateStyle.FadeIn);
                            cmb_Letter_u.SelectedIndex = -1; txt_isbn_u.Clear(); txt_title_u.Clear(); txt_classno_u.Clear(); txt_author_u.Clear();
                        }
                
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }
           
        }

        private void btn_save_u_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (txt_isbn_u.Text.Length == 0)
                {
                    error_msg_u.Text = "* Please enter ISBN";
                    txt_isbn_u.Focus();
                }

                else
                {
                
                        int count = Convert.ToInt32(db.getValue("Select Count(*) from Book where ISBN='" + txt_isbn_u.Text + "' "));
                        if (count > 0)
                        {
                            if (txt_title_u.Text.Length == 0)
                            {
                                error_msg_u.Text = "* NIC No cannot be blank";
                                txt_title_u.Focus();
                            }
                            else if (txt_classno_u.Text.Length == 0)
                            {
                                error_msg_u.Text = "* Name cannot include numbers";
                                txt_classno_u.Focus();
                            }
                            else if (cmb_Letter_u.Text.Length == 0)
                                error_msg_u.Text = "* Member Type cannot be blank";
                        
                            else if (txt_author_u.Text.Length == 0)
                            {
                                error_msg_u.Text = "* Name cannot include numbers";
                                txt_author_u.Focus();
                            }

                            else
                            {
                                error_msg_u.Text = "";

                                int id;
                                int a_count = Convert.ToInt32(db.getValue("Select Count(*) from Author where Name='" + txt_author_u.Text + "' "));
                                if (a_count > 0)
                                {
                                    id = Convert.ToInt32(db.getValue("Select Author_ID from Author where Name='" + txt_author_u.Text + "'"));
                                }
                                else
                                {
                                    int s = db.save_update_delete("Insert into Author (Name) values('" + txt_author_u.Text + "')");
                                    id = Convert.ToInt32(db.getValue("Select Author_ID from Author where Name='" + txt_author_u.Text + "'"));
                                }
                                int i = db.save_update_delete("Update Book set Title = '" + txt_title_u.Text + "', Author_ID = '" + id + "', Class_No = '" + txt_classno_u.Text + "', Letter = '" + cmb_Letter_u.Text + "' where ISBN = '" + txt_isbn_u.Text + "'");
                                if (i == 1)
                                {
                                    MessageBox.Show("Data updated successfully", "Information", MessageBox.Buttons.OK, MessageBox.Icon.Info, MessageBox.AnimateStyle.FadeIn);
                                    txt_isbn_u.Clear(); txt_title_u.Clear(); txt_classno_u.Clear(); cmb_Letter_u.SelectedIndex = -1; txt_author_u.Clear();
                                }

                                else
                                    MessageBox.Show("Data cannot Be updated", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);

                            }
                        }

                        else
                        {
                            MessageBox.Show("Record not found", "", MessageBox.Buttons.OK, MessageBox.Icon.Exclamation, MessageBox.AnimateStyle.FadeIn);
                            txt_title_u.Clear(); txt_classno_u.Clear(); cmb_Letter_u.SelectedIndex = -1; txt_author_u.Clear();
                        }
               

                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }
        }

    }
}
