using Microsoft.Reporting.WinForms;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
    /// Interaction logic for QRcode.xaml
    /// </summary>
    public partial class QRcode : Page
    {
        public QRcode()
        {
            InitializeComponent();
        }

        //private bool _isReportViewerLoaded;
        private void btn_print_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                QRCoder.QRCodeGenerator qRCodeGenerator = new QRCoder.QRCodeGenerator();
                QRCoder.QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(txt_accno.Text, QRCoder.QRCodeGenerator.ECCLevel.Q);
                QRCoder.QRCode qRCode = new QRCoder.QRCode(qRCodeData);
                Bitmap bmp = qRCode.GetGraphic(20);
                var img = BitmapToImageSource(bmp);

                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Bmp);
                    ReportData reportData = new ReportData();
                    ReportData.QRCodeRow qRCodeRow = reportData.QRCode.NewQRCodeRow();
                    qRCodeRow.Image = ms.ToArray();
                    reportData.QRCode.AddQRCodeRow(qRCodeRow);

                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "ReportData";
                    reportDataSource.Value = reportData.QRCode;
                    rv_QRcode.LocalReport.DataSources.Clear();
                    rv_QRcode.LocalReport.DataSources.Add(reportDataSource);
                    rv_QRcode.LocalReport.ReportEmbeddedResource = "Library_System.ReportQR.rdlc";

                    rv_QRcode.RefreshReport();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }


        }
        private ImageSource BitmapToImageSource(Bitmap bitmap)
        {
            
            using (MemoryStream memory = new MemoryStream())
            {
                try
                {
                    bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                    memory.Position = 0;
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = memory;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();

                    return bitmapImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
                    return null;
                }
            }
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.rv_QRcode.RefreshReport();
                this.rv_QRcode.LocalReport.EnableExternalImages = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }

        }

    }
}
