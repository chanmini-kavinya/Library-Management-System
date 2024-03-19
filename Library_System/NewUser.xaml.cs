using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for NewUser.xaml
    /// </summary>
    public partial class NewUser : Page
    {
        public NewUser()
        {
            InitializeComponent();
        }

        Database db = new Database();
        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txt_name.Text))
                {
                    txt_error.Text = "* Name cannot be blank";
                    txt_name.Focus();
                }

                else if (txt_name.Text.Any(char.IsDigit))
                {
                    txt_error.Text = "* Name cannot have numbers";
                    txt_name.Focus();
                }

                else if (string.IsNullOrEmpty(cmb_level.Text))
                {
                    txt_error.Text = "* User level cannot be blank";
                }

                else if (string.IsNullOrEmpty(txt_username.Text))
                {
                    txt_error.Text = "* Username cannot be blank";
                    txt_username.Focus();
                }

                else if (string.IsNullOrEmpty(pb_pwd.Password))
                {
                    txt_error.Text = "* Password cannot be blank";
                    pb_pwd.Focus();
                }

                else if (string.IsNullOrEmpty(pb_confirm_pwd.Password))
                {
                    txt_error.Text = "* Confirm password cannot be blank";
                    pb_confirm_pwd.Focus();
                }

                else if (pb_pwd.Password != pb_confirm_pwd.Password)
                {
                    txt_error.Text = "* The password and confirm password do not match";
                    pb_confirm_pwd.Focus();
                }

                else
                {
                    txt_error.Text = "";

                
                        char l='0';
                        if (cmb_level.SelectedIndex == 0)
                            l = '1';
                        else if(cmb_level.SelectedIndex==1)
                            l = '2';

                    

                        int i = db.save_update_delete("Insert into lib_user values ('" + txt_name.Text + "', '"+l+"' , '" + txt_username.Text + "' , HASHBYTES('md5', '" + pb_pwd.Password + "')) "); 
                        if (i == 1)
                        {
                            MessageBox.Show("Data saved successfully", "Information", MessageBox.Buttons.OK, MessageBox.Icon.Info, MessageBox.AnimateStyle.FadeIn);
                            txt_name.Clear(); cmb_level.SelectedIndex = -1; txt_username.Clear(); pb_pwd.Clear(); pb_confirm_pwd.Clear();
                        }

                        else
                            MessageBox.Show("Data cannot be saved", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
               
                }

            }

            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    MessageBox.Show("That username is already taken. Please try another.", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
                else
                    MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }


            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }

            finally
            {
                db.closeConnection();
            }

        }
    }
}
