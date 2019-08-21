using IncogStuffControl.Services.ViewModel;
using Microsoft.Reporting.WinForms;
using System;
using System.IO;
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

namespace IncogStuffControl.UserControls
{
    /// <summary>
    /// Interaction logic for ReportViewUC.xaml
    /// </summary>
    public partial class ReportViewUC : UserControl
    {
        public ReportViewUC()
        {
            InitializeComponent();
           
        }
        private static string _path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())));
        public static string ContentStart = _path + @"\IncogStuffControl\Reports\TimesheetsRpt.rdlc";
        public ReportViewUC(List<TimesheetsReportViewModel> lstTimesheet)
        {
            InitializeComponent();

            _reportviewer.LocalReport.DataSources.Clear();
            var rpds_model = new ReportDataSource() { Name = "DS_TimeSheet", Value = lstTimesheet };
            _reportviewer.LocalReport.DataSources.Add(rpds_model);

            ReportParameter[] param = new ReportParameter[]
            {

            };
            _reportviewer.LocalReport.EnableExternalImages = true;

            _reportviewer.LocalReport.ReportPath = ContentStart;
            _reportviewer.SetDisplayMode(DisplayMode.PrintLayout);
            _reportviewer.Refresh();
            _reportviewer.RefreshReport();
        }
    }
}
