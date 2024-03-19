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

namespace Library_System
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    /// 
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        Database db = new Database();

        private void login_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ImageBrush myBrush = new ImageBrush();
                Image image = new Image();
                image.Source = new BitmapImage(
                    new Uri(@"library_bg.png", UriKind.Relative));
                myBrush.ImageSource = image.Source;
                ((MainWindow)Application.Current.MainWindow).grid_bg.Background = myBrush;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }

        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_username.Text))
                {
                    txt_error.Text = "* Please enter a username";
                }

                
                else if (string.IsNullOrEmpty(pb_password.Password))
                {
                    txt_error.Text = "* Please enter password";
                }

                else
                {
                    int count = Convert.ToInt32(db.getValue("select count(*) from lib_user where username='" + txt_username.Text + "' and password=HASHBYTES('md5', '" + pb_password.Password + "') "));
                    if (count >0)
                    {
                        ((MainWindow)Application.Current.MainWindow).frame1.Content = new Dashboard();
                        ((MainWindow)Application.Current.MainWindow).nav_pnl.Visibility = Visibility.Visible;
                        ((MainWindow)Application.Current.MainWindow).btn_logout.Visibility = Visibility.Visible;
                        ((MainWindow)Application.Current.MainWindow).level = Convert.ToChar(db.getValue("select level from lib_user where username='" + txt_username.Text + "' "));
                    }
                    else
                        txt_error.Text = "* Incorrect username or password";


                }

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

        private void login_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ((MainWindow)Application.Current.MainWindow).grid_bg.Background = new SolidColorBrush(Colors.Black);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }

        }
    }
}

