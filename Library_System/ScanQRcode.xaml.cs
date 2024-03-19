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
using AForge.Video.DirectShow;
using AForge.Video; //set of libraries for video processing
using ZXing; //barcode image processing library(Zebra crossing)
using System.Windows.Threading; //Namespace for DispatcherTimer class
using System.Drawing; //Namespace for Bitmap class(used to work with images defined by pixel data)


namespace Library_System
{
    /// <summary>
    /// Interaction logic for ScanQRcode.xaml
    /// </summary>

    public partial class ScanQRcode : Page
    {
        public ScanQRcode()
        {
            InitializeComponent();
        }

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;
        DispatcherTimer timer = new DispatcherTimer();
        public string scan_accno; //store decoded qr code
        private void ScanQRcode_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                // enumerate video devices
                filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                foreach (FilterInfo Device in filterInfoCollection)    // list devices
                    cmb_camera.Items.Add(Device.Name);
                cmb_camera.SelectedIndex = 0;
                
                videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cmb_camera.SelectedIndex].MonikerString);    //captures video data from local video capture device
                videoCaptureDevice.NewFrame += FinalFrame_NewFrame;   //event handler to display the captured image from the camera, into the PictureBox control
                videoCaptureDevice.Start();     // start the video source

                timer.Tick += timer_Tick;
                timer.Interval = new TimeSpan(100);
                timer.Start();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }

        }

       
        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                img_scan.Image = (Bitmap)eventArgs.Frame.Clone();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }
            
        }

        //Occurs when the specified timer interval has elapsed
        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                //Read & Recognize QR code
                BarcodeReader Reader = new BarcodeReader();
               Result result = Reader.Decode((Bitmap)img_scan.Image);
                if (result != null)
                {
                    scan_accno = result.ToString();
                    if (videoCaptureDevice.IsRunning == true)
                        videoCaptureDevice.Stop();
                    ((MainWindow)Application.Current.MainWindow).frame1.GoBack();
                    
                }
                 
            }

            catch(ArgumentNullException)
            {

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }

        }
       
        private void ScanQRcode_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (videoCaptureDevice.IsRunning == true)
                    videoCaptureDevice.Stop();
                timer.Stop();
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }
           
            
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (videoCaptureDevice.IsRunning == true)
                    videoCaptureDevice.Stop();
                ((MainWindow)Application.Current.MainWindow).frame1.GoBack();
                timer.Stop();
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }
        }

    }
}
