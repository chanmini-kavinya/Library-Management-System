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
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Data.SqlClient;
using static Library_System.Database;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Library_System
{
    /// <summary>
    /// Interaction logic for Member.xaml
    /// </summary>
    public partial class Member : Page
    {
        public Member()
        {
            InitializeComponent();
        }

        Database db = new Database();

        private void Member_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                int count = Convert.ToInt32(db.getValue("Select Count(*) from Member"));
                if (count > 0)
                    txt_mid_a.Text = Convert.ToString(Convert.ToInt32(db.getValue("Select TOP 1 Member_ID From Member order by Member_ID desc")) + 1);
                else
                    txt_mid_a.Text = "1";
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }


        }

        private void btn_save_a_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_nic_a.Text.Length == 0)
                {
                    error_msg_a.Text = "* NIC No cannot be blank";
                    txt_nic_a.Focus();
                }
                else if (txt_nic_a.Text.Length != 10 && txt_nic_a.Text.Length != 12)
                {
                    error_msg_a.Text = "* Please enter a valid NIC No";
                    txt_nic_a.Focus();
                }
                else if (txt_name_a.Text.Length == 0)
                {
                    error_msg_a.Text = "* Name cannot be blank";
                    txt_name_a.Focus();
                }
                else if (txt_name_a.Text.Any(char.IsDigit))
                {
                    error_msg_a.Text = "* Name cannot include numbers";
                    txt_name_a.Focus();
                }
                else if (cmb_type_a.Text.Length == 0)
                    error_msg_a.Text = "* Member Type cannot be blank";
  
                else if (cmb_status_a.Text.Length == 0)
                    error_msg_a.Text = "* Status cannot be blank";

                else if (txt_address_a.Text.Length == 0)
                {
                    error_msg_a.Text = "* Address cannot be blank";
                    txt_address_a.Focus();
                }
                else if (txt_mobile_a.Text.Length == 0)
                {
                    error_msg_a.Text = "* Mobile No cannot be blank";
                    txt_mobile_a.Focus();
                }
                else if (!Regex.IsMatch(txt_mobile_a.Text, @"^(?:7|0|(?:\+94))[0-9]{8,9}$"))
                {
                    error_msg_a.Text = "* Please enter a valid Mobile No";
                    txt_mobile_a.Focus();
                }
                else if (txt_email_a.Text.Length == 0)
                {
                    error_msg_a.Text = "* Email cannot be blank";
                    txt_email_a.Focus();
                }
                else if (!Regex.IsMatch(txt_email_a.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                {
                    error_msg_a.Text = "* Please enter a valid Email Address. Eg:name@gmail.com";
                    txt_email_a.Focus();
                }
                else
                {
                    error_msg_a.Text = "";

                
                        int i = db.save_update_delete("Insert into Member values ('" + txt_mid_a.Text + "', '" + cmb_type_a.Text + "', '" + txt_name_a.Text + "' , '" + cmb_status_a.Text + "', '" + txt_nic_a.Text + "','" + txt_address_a.Text + "', '" + txt_email_a.Text + "', '" + txt_mobile_a.Text + "') ");
                        if (i == 1)
                        {
                            MessageBox.Show("Data saved successfully", "Information", MessageBox.Buttons.OK, MessageBox.Icon.Info, MessageBox.AnimateStyle.FadeIn);
                            txt_mid_a.Text = Convert.ToString(Convert.ToInt32(db.getValue("Select TOP 1 Member_ID From Member order by Member_ID desc")) + 1);
                            cmb_type_a.SelectedIndex = -1; txt_name_a.Clear(); cmb_status_a.SelectedIndex = -1; txt_nic_a.Clear(); txt_address_a.Clear(); txt_email_a.Clear(); txt_mobile_a.Clear();
                        }

                        else
                            MessageBox.Show("Data cannot be saved", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
                
                }

            }

            catch (Exception)
            {
                MessageBox.Show("Please check again", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }
        }


        private void btn_find_u_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_mid_u.Text.Length == 0)
                {
                    error_msg_u.Text = "* Please enter Member ID";
                    txt_mid_u.Focus();
                }
                
                else
                {
                    error_msg_u.Text = "";

                
                        int count = Convert.ToInt32(db.getValue("Select Count(*) from Member where Member_ID='" + txt_mid_u.Text + "' "));
                        if (count > 0)
                        {
                            member x = db.getRecord("Select * from Member where Member_ID=('" + txt_mid_u.Text + "') ", Convert.ToInt32(txt_mid_u.Text));
                            txt_nic_u.Text = x.NIC;
                            txt_name_u.Text = x.Name;
                            cmb_type_u.Text = x.Member_Type;
                            cmb_status_u.Text = x.Status;
                            txt_address_u.Text = x.Address;
                            txt_mobile_u.Text = x.Mobile_No;
                            txt_email_u.Text = x.Email;
                        }

                        else
                            MessageBox.Show("Record not found", "", MessageBox.Buttons.OK, MessageBox.Icon.Exclamation, MessageBox.AnimateStyle.FadeIn);
                
                }

            }

            catch (Exception)
            {
                MessageBox.Show("Please check again", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }

        }

        private void btn_update_u_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_mid_u.Text.Length == 0)
                {
                    error_msg_u.Text = "* Please enter Member ID";
                    txt_mid_u.Focus();
                }
                else if (txt_nic_u.Text.Length == 0)
                {
                    error_msg_u.Text = "* NIC No cannot be blank";
                    txt_nic_u.Focus();
                }
                else if (txt_nic_u.Text.Length != 10 && txt_nic_u.Text.Length != 12)
                {
                    error_msg_u.Text = "* Please enter a valid NIC No";
                    txt_nic_u.Focus();
                }
                else if (txt_name_u.Text.Any(char.IsDigit))
                {
                    error_msg_u.Text = "* Name cannot include numbers";
                    txt_name_u.Focus();
                }
                else if (txt_name_u.Text.Length == 0)
                {
                    error_msg_u.Text = "* Name cannot be blank";
                    txt_name_u.Focus();
                }
                else if (cmb_type_u.Text.Length == 0)
                    error_msg_u.Text = "* Member Type cannot be blank";
                else if (cmb_status_u.Text.Length == 0)
                    error_msg_u.Text = "* Status cannot be blank";
                else if (txt_address_u.Text.Length == 0)
                {
                    error_msg_u.Text = "* Address cannot be blank";
                    txt_address_u.Focus();
                }
                else if (txt_mobile_u.Text.Length == 0)
                {
                    error_msg_u.Text = "* Mobile No cannot be blank";
                    txt_mobile_u.Focus();
                }
                else if (!Regex.IsMatch(txt_mobile_u.Text, @"^(?:7|0|(?:\+94))[0-9]{8,9}$"))
                {
                    error_msg_u.Text = "* Please enter a valid Mobile No";
                    txt_mobile_u.Focus();
                }
                else if (txt_email_u.Text.Length == 0)
                {
                    error_msg_u.Text = "* Email cannot be blank";
                    txt_email_u.Focus();
                }
                else if (!Regex.IsMatch(txt_email_u.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                {
                    error_msg_u.Text = "* Please enter a valid Email Address. Eg:name@gmail.com";
                    txt_email_u.Focus();
                }
                else
                {
                    error_msg_u.Text = "";

                
                        int i = db.save_update_delete("Update Member set  NIC = '" + txt_nic_u.Text + "', Name = '" + txt_name_u.Text + "', Member_Type = '" + cmb_type_u.Text + "', Status = '" + cmb_status_u.Text + "', Address = '" + txt_address_u.Text + "', Mobile_No = '" + txt_mobile_u.Text + "', Email = '" + txt_email_u.Text + "' where Member_ID = '" + txt_mid_u.Text + "' ");
                        if (i == 1)
                        {
                            MessageBox.Show("Data updated successfully", "Information", MessageBox.Buttons.OK, MessageBox.Icon.Info, MessageBox.AnimateStyle.FadeIn);
                            txt_mid_u.Clear(); cmb_type_u.SelectedIndex = -1; txt_name_u.Clear(); cmb_status_u.SelectedIndex = -1; txt_nic_u.Clear(); txt_address_u.Clear(); txt_email_u.Clear(); txt_mobile_u.Clear();
                        }

                        else
                            MessageBox.Show("Data cannot Be updated", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
              
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Please check again", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }
        }

        private void btn_find_d_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_mid_d.Text.Length == 0)
                {
                    error_msg_d.Text = "* Please enter Member ID";
                    txt_mid_d.Focus();
                }
                else
                {
                    error_msg_d.Text = "";
                
                        int count = Convert.ToInt32(db.getValue("Select Count(*) from Member where Member_ID='" + txt_mid_d.Text + "' "));
                        if (count > 0)
                            txt_name_d.Text = db.getValue("Select Name from Member where Member_ID='" + txt_mid_d.Text + "' ");
                        else
                            MessageBox.Show("Record not found", "", MessageBox.Buttons.OK, MessageBox.Icon.Exclamation, MessageBox.AnimateStyle.FadeIn);
                
                }

            }

            catch (Exception)
            {
                MessageBox.Show("Please check again", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }

        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (txt_mid_d.Text.Length == 0)
                {
                    error_msg_d.Text = "* Please enter Member ID";
                    txt_mid_d.Focus();
                }
                else
                {
                    error_msg_d.Text = "";

                
                        int count = Convert.ToInt32(db.getValue("Select Count(*) from Member where Member_ID='" + txt_mid_d.Text + "' "));
                        if (count > 0)
                        {
                            DialogResult result = MessageBox.Show("Are you sure you want to delete this record", "Warning", MessageBox.Buttons.YesNo, MessageBox.Icon.Warning, MessageBox.AnimateStyle.FadeIn);

                            if (result == DialogResult.Yes)
                            {
                                int i = db.save_update_delete("Delete from Member where Member_ID = '" + txt_mid_d.Text + "'");
                                if (i == 1)
                                {
                                    MessageBox.Show("Record deleted successfully", "Information", MessageBox.Buttons.OK, MessageBox.Icon.Info, MessageBox.AnimateStyle.FadeIn);
                                }

                                else
                                    MessageBox.Show("Record cannot be deleted", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
                            }

                            txt_mid_d.Clear(); txt_name_d.Clear();
                        }

                        else
                            MessageBox.Show("Record not found", "", MessageBox.Buttons.OK, MessageBox.Icon.Exclamation, MessageBox.AnimateStyle.FadeIn);
               
                }

            }

            catch (Exception)
            {
                MessageBox.Show("Please check again", "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }

        }
               
    }
}

