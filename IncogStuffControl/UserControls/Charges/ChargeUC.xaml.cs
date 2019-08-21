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

namespace IncogStuffControl.UserControls.Charges
{
    /// <summary>
    /// Interaction logic for ChargeUC.xaml
    /// </summary>
    public partial class ChargeUC : UserControl
    {
        public List<RosterCViewModel> lstRoster;
        public ChargeUC()
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

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            ViewWindow_Modal.CloseModal();
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(lblPath.Text))
            {
                MessageBoxModal.Show("Please Select the Load File: ", "Information", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);

            }
            else
            {
                lstRoster = new List<RosterCViewModel>();
                lstRoster = ReadFile(lblPath.Text);
            }


            MessageResponseViewModel responseObj = await ServiceCharges.ChageRoster(lstRoster);

        }

        private List<RosterCViewModel> ReadFile(string Path)
        {
            string connString = string.Empty;
            connString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Path + "; Extended Properties='Excel 8.0;IMEX=1;HDR=No'";

            OleDbConnection connExcel = new OleDbConnection(connString);
            OleDbCommand cmdExcel = new OleDbCommand();
            try
            {
                cmdExcel.Connection = connExcel;

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
                int Day = 0;
                int Date = 0;
                int StartTime = 0;
                int EndTime = 0;
                int Break = 0;
                int Precint = 0;
                int Zone = 0;
                int Area = 0;
                int Shift = 0;
                int Labour = 0;
                int Employee = 0;
                int Payroll = 0;
                int LockedIn = 0;

                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    if (ds.Tables[0].Rows[0][i].ToString() == "Day")
                    {
                        Day = i;
                    }
                    else if (ds.Tables[0].Rows[0][i].ToString() == "Date")
                    {
                        Date = i;
                    }
                    else if (ds.Tables[0].Rows[0][i].ToString() == "Start Time")
                    {
                        StartTime = i;
                    }
                    else if (ds.Tables[0].Rows[0][i].ToString() == "End Time")
                    {
                        EndTime = i;
                    }
                    else if (ds.Tables[0].Rows[0][i].ToString() == "Break (Mins)")
                    {
                        Break = i;
                    }
                    else if (ds.Tables[0].Rows[0][i].ToString() == "Precinct")
                    {
                        Precint = i;
                    }
                    else if (ds.Tables[0].Rows[0][i].ToString() == "Zone")
                    {
                        Zone = i;
                    }
                    else if (ds.Tables[0].Rows[0][i].ToString() == "Area")
                    {
                        Area = i;
                    }
                    else if (ds.Tables[0].Rows[0][i].ToString() == "Shift No.")
                    {
                        Shift = i;
                    }
                    else if (ds.Tables[0].Rows[0][i].ToString() == "Labour Type")
                    {
                        Labour = i;
                    }
                    else if (ds.Tables[0].Rows[0][i].ToString() == "Employee")
                    {
                        Employee = i;
                    }
                    else if (ds.Tables[0].Rows[0][i].ToString() == "Payroll No.")
                    {
                        Payroll = i;
                    }
                    else if (ds.Tables[0].Rows[0][i].ToString() == "Locked In")
                    {
                        LockedIn = i;
                    }
                }

                for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                {
                    RosterCViewModel Roster = new RosterCViewModel();
                    Roster.Day = Convert.ToInt32(ds.Tables[0].Rows[i][Day].ToString());
                    Roster.Area = ds.Tables[0].Rows[i][Area].ToString();
                    Roster.Break = Convert.ToInt32(ds.Tables[0].Rows[i][Break].ToString());
                    Roster.Date = Convert.ToDateTime(ds.Tables[0].Rows[i][Date].ToString());
                    Roster.Employee = ds.Tables[0].Rows[i][Employee].ToString();
                    Roster.EndTime = ds.Tables[0].Rows[i][EndTime].ToString();
                    Roster.LabourType = ds.Tables[0].Rows[i][Labour].ToString();
                    Roster.LookedIn = Convert.ToBoolean(ds.Tables[0].Rows[i][LockedIn].ToString());
                    Roster.Payroll = ds.Tables[0].Rows[i][Payroll].ToString();
                    Roster.Precint = ds.Tables[0].Rows[i][Precint].ToString();
                    Roster.ShiftNum = Convert.ToInt32(ds.Tables[0].Rows[i][Shift].ToString());
                    Roster.StartTime = ds.Tables[0].Rows[i][StartTime].ToString();
                    Roster.Zone = ds.Tables[0].Rows[i][Zone].ToString();
                    lstRoster.Add(Roster);
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
            return lstRoster;
        }

    }
}
