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
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

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
                MessageBoxModal.Show(General.ResolveOwnerWindow(), "Please Select the Load File: ", "Information", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);

            }
            else
            {
                lstRoster = new List<RosterCViewModel>();
                lstRoster = ReadFile(lblPath.Text);
            }
            MessageResponseViewModel<RosterWM> responseObj;
            if (lstRoster != null)
            {
                responseObj = await ServiceCharges.ChageRoster(lstRoster);
                
            }
            else
            {
                return;
            }

            if(responseObj.Succesfull==true)
            {
                MessageBoxModal.Show(General.ResolveOwnerWindow(), "The process got Successful", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBoxModal.Show(General.ResolveOwnerWindow(), responseObj.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private List<RosterCViewModel> ReadFile(string Path)
        {
            try
            {
                    //Interop
                   // System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application");
                Excel.Application xlapp = new Excel.Application();
            _Workbook xlWoork = xlapp.Workbooks.Open(Path);
            _Worksheet xlWorksheet = xlWoork.Sheets[1];
            Range xlRange = xlWorksheet.UsedRange;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
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
            int EventName = 0;
                string val = (String)(xlWorksheet.Cells[1, 1] as Excel.Range).Value;
                for (int i = 1; i <= colCount; i++)
                {
                    if ((String)(xlWorksheet.Cells[1, i] as Excel.Range).Value == "Day")
                    {
                        Day = i;
                    }
                    else if ((String)(xlWorksheet.Cells[1, i] as Excel.Range).Value == "Date")
                    {
                        Date = i;
                    }
                    else if ((String)(xlWorksheet.Cells[1, i] as Excel.Range).Value == "Start Time")
                    {
                        StartTime = i;
                    }
                    else if ((String)(xlWorksheet.Cells[1, i] as Excel.Range).Value == "End Time")
                    {
                        EndTime = i;
                    }
                    else if ((String)(xlWorksheet.Cells[1, i] as Excel.Range).Value == "Break (Mins)")
                    {
                        Break = i;
                    }
                    else if ((String)(xlWorksheet.Cells[1, i] as Excel.Range).Value == "Precinct")
                    {
                        Precint = i;
                    }
                    else if ((String)(xlWorksheet.Cells[1, i] as Excel.Range).Value == "Zone")
                    {
                        Zone = i;
                    }
                    else if ((String)(xlWorksheet.Cells[1, i] as Excel.Range).Value == "Area")
                    {
                        Area = i;
                    }
                    else if ((String)(xlWorksheet.Cells[1, i] as Excel.Range).Value == "Shift No.")
                    {
                        Shift = i;
                    }
                    else if ((String)(xlWorksheet.Cells[1, i] as Excel.Range).Value == "Labour Type")
                    {
                        Labour = i;
                    }
                    else if ((String)(xlWorksheet.Cells[1, i] as Excel.Range).Value == "Employee")
                    {
                        Employee = i;
                    }
                    else if ((String)(xlWorksheet.Cells[1, i] as Excel.Range).Value == "Payroll No.")
                    {
                        Payroll = i;
                    }
                    else if ((String)(xlWorksheet.Cells[1, i] as Excel.Range).Value == "Locked In")
                    {
                        LockedIn = i;
                    }
                    else if ((String)(xlWorksheet.Cells[1, i] as Excel.Range).Value == "EventName")
                    {
                        EventName = i;
                    }

                }
           

                var val2 = (xlWorksheet.Cells[2, Payroll] as Excel.Range).Value2;
                var val3 = (xlWorksheet.Cells[3, Precint] as Excel.Range).Value2;
                for (int i = 2; i < rowCount; i++)
                {
                    RosterCViewModel Roster = new RosterCViewModel();
                    Roster.Day = Convert.ToInt32((xlWorksheet.Cells[i, Day] as Excel.Range).Value);
                    Roster.Area = (xlWorksheet.Cells[i, Area] as Excel.Range).Value;
                    Roster.Break = Convert.ToInt32((xlWorksheet.Cells[i, Break] as Excel.Range).Value);
                    Roster.Date = Convert.ToDateTime((xlWorksheet.Cells[i, Date] as Excel.Range).Value);
                    Roster.Employee = (xlWorksheet.Cells[i, Employee] as Excel.Range).Value;
                    Roster.EndTime = Convert.ToString((xlWorksheet.Cells[i, EndTime] as Excel.Range).Value);
                    Roster.LabourType = (xlWorksheet.Cells[i, Labour] as Excel.Range).Value;
                    Roster.LookedIn = Convert.ToBoolean((xlWorksheet.Cells[i, LockedIn] as Excel.Range).Value);
                    Roster.Payroll = Convert.ToString((xlWorksheet.Cells[i, Payroll] as Excel.Range).Value);
                    Roster.Precint = (xlWorksheet.Cells[i, Precint] as Excel.Range).Value;
                    Roster.ShiftNum = Convert.ToInt32((xlWorksheet.Cells[i, Shift] as Excel.Range).Value);
                    Roster.StartTime = Convert.ToString((xlWorksheet.Cells[i, StartTime] as Excel.Range).Value);
                    Roster.Zone = (xlWorksheet.Cells[i, Zone] as Excel.Range).Value;
                    Roster.EventName = (xlWorksheet.Cells[i, EventName] as Excel.Range).Value;
                    lstRoster.Add(Roster);
                }

                xlapp.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBoxModal.Show(General.ResolveOwnerWindow(), ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            GC.Collect();

            return lstRoster;
        }

       

    }
}
