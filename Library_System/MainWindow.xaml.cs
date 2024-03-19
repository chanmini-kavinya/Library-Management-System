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
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using Library_System;
using AForge.Video.DirectShow;
using AForge.Video;

namespace Library_System
{

    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        char l;
        public char level //method associated with the l variable
        {
            get { return l; }  //returns the value of the variable
            set { l = value; } //assigns a value to the variable
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                frame1.Content = new Login();
                nav_pnl.Visibility = Visibility.Collapsed;
                btn_logout.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }
        }


        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                frame1.Content = new Login();
                nav_pnl.Visibility = Visibility.Collapsed;
                btn_logout.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }
            
        }

        private void frame1_Navigated(object sender, NavigationEventArgs e)
        {
            
        }

        private void dashboard_click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                frame1.Content = new Dashboard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }
            
        }

        private void newUser_click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (level != '1')
                    MessageBox.Show("Access denied", "", MessageBox.Buttons.OK, MessageBox.Icon.Warning, MessageBox.AnimateStyle.FadeIn);
                else
                    frame1.Content = new NewUser();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }
        }

        private void LV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
                {
                    case "dashboard":
                        frame1.Content = new Dashboard();
                        break;
                    case "member":
                        frame1.Content = new Member();
                        break;
                    case "book":
                        frame1.Content = new Book();
                        break;
                    case "issue":
                        frame1.Content = new Issues();
                        break;
                    case "return":
                        frame1.Content = new Return();
                        break;
                    case "search":
                        frame1.Content = new Search();
                        break;
                    case "reminder":
                        frame1.Content = new Reminder();
                        break;
                    case "report":
                        frame1.Content = new Report();
                        break;
                    case "qrcode":
                        frame1.Content = new QRcode();
                        break;
                    case "newUser":
                        {
                            if (level != '1')
                                MessageBox.Show("Access denied", "", MessageBox.Buttons.OK, MessageBox.Icon.Warning, MessageBox.AnimateStyle.FadeIn);
                            else
                                frame1.Content = new NewUser();
                        }
                        break;
                    default:
                        break;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }

        }

    }

    internal class MetroTabControl1
    {
        public MetroTabControl1()
        {
        }
    }
}
