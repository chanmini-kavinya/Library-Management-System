using Library_System;
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
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Page
    {
        public Report()
        {
            InitializeComponent();

        }

        private void rv_bookList_Load(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                LibraryDataSet dataset = new LibraryDataSet();

                dataset.BeginInit();

                reportDataSource1.Name = "DataSet1"; 
                reportDataSource1.Value = dataset.Book3;
                this.rv_bookList.LocalReport.DataSources.Add(reportDataSource1);
                this.rv_bookList.LocalReport.ReportEmbeddedResource = "Report_Book_List.rdlc";
                this.rv_bookList.LocalReport.ReportPath = "Report_Book_List.rdlc";

                dataset.EndInit();

                Library_System.LibraryDataSetTableAdapters.Book3TableAdapter Book3TableAdapter = new Library_System.LibraryDataSetTableAdapters.Book3TableAdapter();
                Book3TableAdapter.ClearBeforeFill = true;
                Book3TableAdapter.Fill(dataset.Book3);

                rv_bookList.RefreshReport();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }
        }

        private void rv_stock_Load(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                LibraryDataSet dataset = new LibraryDataSet();

                dataset.BeginInit();

                reportDataSource1.Name = "DataSet1"; 
                reportDataSource1.Value = dataset.Book1;
                this.rv_stock.LocalReport.DataSources.Add(reportDataSource1);
                this.rv_stock.LocalReport.ReportEmbeddedResource = "Report_Stock_of_Book.rdlc";
                this.rv_stock.LocalReport.ReportPath = "Report_Stock_of_Book.rdlc";

                dataset.EndInit();

                Library_System.LibraryDataSetTableAdapters.Book1TableAdapter Book1TableAdapter = new Library_System.LibraryDataSetTableAdapters.Book1TableAdapter();
                Book1TableAdapter.ClearBeforeFill = true;
                Book1TableAdapter.Fill(dataset.Book1);

                rv_stock.RefreshReport();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }
        }

        private void rv_notReturned_Load(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                LibraryDataSet dataset = new LibraryDataSet();

                dataset.BeginInit();

                reportDataSource1.Name = "DataSet1"; 
                reportDataSource1.Value = dataset.Book2;
                this.rv_notReturned.LocalReport.DataSources.Add(reportDataSource1);
                this.rv_notReturned.LocalReport.ReportEmbeddedResource = "Report_Books_Not_Return.rdlc";
                this.rv_notReturned.LocalReport.ReportPath = "Report_Books_Not_Return.rdlc";

                dataset.EndInit();

                Library_System.LibraryDataSetTableAdapters.Book2TableAdapter Book2TableAdapter = new Library_System.LibraryDataSetTableAdapters.Book2TableAdapter();
                Book2TableAdapter.ClearBeforeFill = true;
                Book2TableAdapter.Fill(dataset.Book2);

                rv_notReturned.RefreshReport();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBox.Buttons.OK, MessageBox.Icon.Error, MessageBox.AnimateStyle.FadeIn);
            }
        }

        private void TabablzControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
