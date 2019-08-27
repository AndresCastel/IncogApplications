using IncogStuffControl.UtilControls.ViewModal;
using IncogStuffControl.UserControls;
using IncogStuffControl.UserControls.MainBoard;
using IncogStuffControl.UserControls.Scan;
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
using IncogStuffControl.Services.ViewModel;
using IncogStuffControl.Services;
using IncogStuffControl.UserControls.Charges;

namespace IncogStuffControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        public ScanIdCard scanus;
        public MainWindow()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            scanus = new ScanIdCard();
            contentUserControl.Content = scanus;
            scanus.PropertyChanged += Scanus_PropertyChanged;
            FocusManager.SetFocusedElement(this, scanus.txtScan);
            Keyboard.Focus(scanus.txtScan);
            IncImage.Visibility = Visibility.Visible;
            CreateMenuBase();
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
            
        }

        //Create Menu
        private void CreateMenuBase()
        {

          

            MenuItem oMenuItemEmployee = new MenuItem();
            oMenuItemEmployee.Header = "Employee";
            MenuBase.Items.Add(oMenuItemEmployee);

            MenuItem oMenuItemNewEmployee = new MenuItem();
            oMenuItemNewEmployee.InputGestureText = "Ctrl+N";
            oMenuItemNewEmployee.Header = "New";
            oMenuItemNewEmployee.Click += new RoutedEventHandler(oMenuItemNewEmployee_Click);
            oMenuItemEmployee.Items.Add(oMenuItemNewEmployee);

            MenuItem oMenuItemEditEmployee = new MenuItem();
            oMenuItemEditEmployee.InputGestureText = "Ctrl+E";
            oMenuItemEditEmployee.Header = "Edit";
            oMenuItemEditEmployee.Click += new RoutedEventHandler(oMenuItemEditEmployee_Click);
            oMenuItemEmployee.Items.Add(oMenuItemEditEmployee);

            MenuItem oMenuItemLoadData = new MenuItem();
            oMenuItemLoadData.Header = "Load Data";
            MenuBase.Items.Add(oMenuItemLoadData);

            MenuItem oMenuItemAddEmployee = new MenuItem();
            oMenuItemAddEmployee.InputGestureText = "Ctrl+L";
            oMenuItemAddEmployee.Header = "Add Employees";
            oMenuItemAddEmployee.Click += new RoutedEventHandler(oMenuItemAddEmployee_Click);
            oMenuItemLoadData.Items.Add(oMenuItemAddEmployee);

            MenuItem oMenuItemAddRoster = new MenuItem();
            oMenuItemAddRoster.InputGestureText = "Ctrl+L";
            oMenuItemAddRoster.Header = "Add Roster";
            oMenuItemAddRoster.Click += new RoutedEventHandler(oMenuItemAddRoster_Click);
            oMenuItemLoadData.Items.Add(oMenuItemAddRoster);


            MenuItem oMenuItemReport = new MenuItem();
            oMenuItemReport.Header = "Reports";
            MenuBase.Items.Add(oMenuItemReport);

            MenuItem oMenuItemLoadReports = new MenuItem();
            oMenuItemLoadReports.InputGestureText = "Ctrl+R";
            oMenuItemLoadReports.Header = "Timesheets";
            oMenuItemLoadReports.Click += new RoutedEventHandler(oMenuItemLoadReports_Click);
            oMenuItemReport.Items.Add(oMenuItemLoadReports);
        }

        private void oMenuItemEditEmployee_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void  oMenuItemLoadReports_Click(object sender, RoutedEventArgs e)
        {
            List<TimesheetsReportViewModel> lst = await ServiceEmployee.GetTimesheetReport();
            ReportViewUC oReportTimesheet = new ReportViewUC(lst);
            oReportTimesheet.PropertyChanged += OReportTimesheet_PropertyChanged;
            contentUserControl.Content = null;

            contentUserControl.Content = oReportTimesheet;
        }

        private void OReportTimesheet_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is ReportViewUC)
            {
                contentUserControl.Content = null;
                FocusManager.SetFocusedElement(this, scanus.txtScan);
                Keyboard.Focus(scanus.txtScan);
                contentUserControl.Content = scanus;
                MenuBase.Visibility = Visibility.Hidden;
                IncImage.Visibility = Visibility.Visible;
            }
        }

        private void oMenuItemAddRoster_Click(object sender, RoutedEventArgs e)
        {
            ChargeUC oUC_Charge = new ChargeUC();
            MessageBoxResult Response = ViewWindow_Modal.Show(oUC_Charge, "Load Roster", oUC_Charge.BtnCancel);
        }

        private void oMenuItemAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            ChargeEmployees oUC_Charge = new ChargeEmployees();
             MessageBoxResult Response = ViewWindow_Modal.Show(oUC_Charge, "Load Employees", oUC_Charge.BtnCancel);
        }

        private void oMenuItemNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void Scanus_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is ScanIdCard)
            {
                MainBoardUC MainUC;
                contentUserControl.Content = null;
                MessageResponseViewModel<AllStuffVM> responseObj = await ServiceEmployee.GetAllStuff();
                    if (responseObj.Succesfull)
                    {
                    MainUC = new MainBoardUC(((ScanIdCard)sender).employee, responseObj.Data.Stuff);
                   
                    }
                    else
                    {
                    MainUC = new MainBoardUC(((ScanIdCard)sender).employee,  new List<StuffAssignViewModel>());
                }
                
                MainUC.PropertyChanged += MainUC_PropertyChanged;
                contentUserControl.Content = MainUC;
                if (((ScanIdCard)sender).employee != null)
                {
                    if (((ScanIdCard)sender).employee.employregister.Employee.RolId == 1)
                    {
                        MenuBase.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        MenuBase.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    //MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "You already did Sign In: " + employeepriv.SignIn, "Information", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);
                }
                IncImage.Visibility = Visibility.Hidden;
            }
        }

        private void MainUC_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is MainBoardUC)
            {
                contentUserControl.Content = null;
                FocusManager.SetFocusedElement(this, scanus.txtScan);
                Keyboard.Focus(scanus.txtScan);
                contentUserControl.Content = scanus;
                MenuBase.Visibility = Visibility.Hidden;
                IncImage.Visibility = Visibility.Visible;
            }
        }

        public void Dispose()
        {
            this.Dispose();
        }

        

       
    }
}
