using IncogStuffControl.UtilControls.ModalMessageBox;
using IncogStuffControl.UtilControls.ViewModal;
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
using IncogStuffControl.Services.ViewModel;
using System.Data.OleDb;
using System.Data;
using IncogStuffControl.Services.Services;
using Incog.Utils;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;


namespace IncogStuffControl.UserControls.Charges
{
    /// <summary>
    /// Interaction logic for ChargeEmployees.xaml
    /// </summary>
    public partial class ChargeEmployees : UserControl
    {
        public List<EmployeeViewModel> lstEmployees;
        public ChargeEmployees()
        {
            InitializeComponent();
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".xlsx";
            dlg.Filter = "Excel Files (*.xlsx)|*.xlsx|Excel Files (*.xls)|*.xls";

            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                lblPath.Text = filename;
            }

           
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(lblPath.Text))
            {
                MessageBoxModal.Show(General.ResolveOwnerWindow(), "Please Select the Load File: ", "Information", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);

            }
            else
            {
                lstEmployees = new List<EmployeeViewModel>();
                lstEmployees = ReadFile(lblPath.Text);
            }
            MessageResponseViewModel<EmployeesChargeMW> responseObj;
            if (lstEmployees != null)
            {
                responseObj = await ServiceCharges.ChageEmployees(lstEmployees);
            }
            else
            {
                return;
            }
            if (responseObj.Succesfull == true)
            {
                MessageBoxModal.Show(General.ResolveOwnerWindow(), "The process got Successful", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBoxModal.Show(General.ResolveOwnerWindow(), responseObj.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private List<EmployeeViewModel> ReadFile(string Path)
        {
            
            try
            {
                Excel.Application xlapp = new Excel.Application();
                _Workbook xlWoork = xlapp.Workbooks.Open(Path);
                _Worksheet xlWorksheet = xlWoork.Sheets[1];
                Range xlRange = xlWorksheet.UsedRange;
                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;

                int Name = 0;
                int MiddleName = 0;
                int LastName = 0;
                int Barcode = 0;
                int Email = 0;
                int Payroll = 0;
                int RolId = 0;
                int Active = 0;

                for (int i = 1; i <= colCount; i++)
                {
                    if ((String)(xlWorksheet.Cells[1, i] as Excel.Range).Value == "Name")
                    {
                        Name = i;
                    }
                    else if ((String)(xlWorksheet.Cells[1, i] as Excel.Range).Value == "MiddleName")
                    {
                        MiddleName = i;
                    }
                    else if ((String)(xlWorksheet.Cells[1, i] as Excel.Range).Value == "LastName")
                    {
                        LastName = i;
                    }
                    else if ((String)(xlWorksheet.Cells[1, i] as Excel.Range).Value == "Barcode")
                    {
                        Barcode = i;
                    }
                    else if ((String)(xlWorksheet.Cells[1, i] as Excel.Range).Value == "Payroll")
                    {
                        Payroll = i;
                    }
                    else if ((String)(xlWorksheet.Cells[1, i] as Excel.Range).Value == "Email")
                    {
                        Email = i;
                    }
                    else if ((String)(xlWorksheet.Cells[1, i] as Excel.Range).Value == "RolId")
                    {
                        RolId = i;
                    }
                    else if ((String)(xlWorksheet.Cells[1, i] as Excel.Range).Value == "Active")
                    {
                        Active = i;
                    }
                }

                for (int i = 2; i < rowCount; i++)
                {
                    EmployeeViewModel employ = new EmployeeViewModel();
                    employ.Name = (xlWorksheet.Cells[i, Name] as Excel.Range).Value;
                    employ.MiddleName = (xlWorksheet.Cells[i, MiddleName] as Excel.Range).Value;
                    employ.LastName = (xlWorksheet.Cells[i, LastName] as Excel.Range).Value;
                    employ.Barcode = (xlWorksheet.Cells[i, Barcode] as Excel.Range).Value;
                    employ.Email = (xlWorksheet.Cells[i, Email] as Excel.Range).Value;
                    employ.Payroll = (xlWorksheet.Cells[i, Payroll] as Excel.Range).Value;
                    employ.RolId = Convert.ToInt32((xlWorksheet.Cells[i, RolId] as Excel.Range).Value);
                    employ.Active = true;
                    
                    lstEmployees.Add(employ);
                }

                xlapp.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBoxModal.Show(General.ResolveOwnerWindow(), ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            GC.Collect();
            return lstEmployees;
        }


        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            ViewWindow_Modal.CloseModal();
        }
    }
}
