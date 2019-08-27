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
            string connString = string.Empty;
            connString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Path + "; Extended Properties='Excel 8.0;IMEX=1;HDR=No'";

            OleDbConnection connExcel = new OleDbConnection(connString);
            OleDbCommand cmdExcel = new OleDbCommand();
            try
            {
                cmdExcel.Connection = connExcel;
                //Check if the file is open
                bool open = General.FileIsOpen(lblPath.Text);
                if (open)
                {
                    MessageBoxModal.Show(General.ResolveOwnerWindow(), "The file is Open, Could you close it?", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return null;
                }
                //Check if the Sheet Exists
                connExcel.Open();
                DataTable dtExcelSchema;
                //Get the Schema of the WorkBook
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                connExcel.Close();

                //Read Data from Sheet1
                connExcel.Open();
                OleDbDataAdapter da = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                //Range Query
                //cmdExcel.CommandText = "SELECT * From [" + SheetName + "A3:B5]";

                da.SelectCommand = cmdExcel;
                da.Fill(ds);
                int Name = 0;
                int MiddleName = 0;
                int LastName = 0;
                int Barcode = 0;
                int Email = 0;
                int Payroll = 0;
                int RolId = 0;
                int Active = 0;

                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    if (ds.Tables[0].Rows[0][i].ToString() == "Name")
                    {
                        Name = i;
                    }
                    else if (ds.Tables[0].Rows[0][i].ToString() == "MiddleName")
                    {
                        MiddleName = i;
                    }
                    else if (ds.Tables[0].Rows[0][i].ToString() == "LastName")
                    {
                        LastName = i;
                    }
                    else if (ds.Tables[0].Rows[0][i].ToString() == "Barcode")
                    {
                        Barcode = i;
                    }
                    else if (ds.Tables[0].Rows[0][i].ToString() == "Email")
                    {
                        Email = i;
                    }
                    else if (ds.Tables[0].Rows[0][i].ToString() == "Payroll")
                    {
                        Payroll = i;
                    }
                    else if (ds.Tables[0].Rows[0][i].ToString() == "RolId")
                    {
                        RolId = i;
                    }
                    else if (ds.Tables[0].Rows[0][i].ToString() == "Active")
                    {
                        Active = i;
                    }
                }

                for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                {
                    EmployeeViewModel employ = new EmployeeViewModel();
                    employ.Name = ds.Tables[0].Rows[i][Name].ToString();
                    employ.MiddleName = ds.Tables[0].Rows[i][MiddleName].ToString();
                    employ.LastName = ds.Tables[0].Rows[i][LastName].ToString();
                    employ.Barcode = ds.Tables[0].Rows[i][Barcode].ToString();
                    employ.Email = ds.Tables[0].Rows[i][Email].ToString();
                    employ.Payroll = ds.Tables[0].Rows[i][Payroll].ToString();
                    employ.RolId = Convert.ToInt32(ds.Tables[0].Rows[i][RolId].ToString());
                    employ.Active = true;
                    
                    lstEmployees.Add(employ);
                }

                connExcel.Close();
            }
            catch
            {
                return null;
            }
            finally
            {
                cmdExcel.Dispose();
                connExcel.Dispose();
            }
            return lstEmployees;
        }


        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            ViewWindow_Modal.CloseModal();
        }
    }
}
