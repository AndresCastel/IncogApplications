using Incog.Utils;
using IncogStuffControl.Services;
using IncogStuffControl.Services.ViewModel;
using IncogStuffControl.UtilControls.ModalMessageBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace IncogStuffControl.UserControls.Scan
{
    /// <summary>
    /// Interaction logic for ScanIdCard.xaml
    /// </summary>
    public partial class ScanIdCard : UserControl, INotifyPropertyChanged
    {

        /// <summary>
        /// Evento de acuerdo a la interfáz.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// Método que permite hacer el raise del evento de cambio de propiedad.
        /// </summary>
        /// <param name="info">Propiedad que está cambiando</param>
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        private EmployeeVsRosterVM _employee;
        public EmployeeVsRosterVM employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                NotifyPropertyChanged("employee");
            }
        }


        public ScanIdCard()
        {
            InitializeComponent();


        }
       

        private async void txtScan_TextChanged(object sender, TextChangedEventArgs e)
        {

            
        }

        private async void TxtScan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EmployeeRegisterViewModel employeer = new EmployeeRegisterViewModel();
                employeer.Employee = new EmployeeViewModel();
                employeer.Employee.Barcode = txtScan.Text;
                employeer.Day = DateTime.Now.Date;
                MessageResponseViewModel<EmployeeVsRosterVM> responseObj = await ServiceEmployee.GetEmployee(employeer);
                if (responseObj.Succesfull)
                {
                    if (responseObj.Succesfull != false)
                    {
                        if (responseObj.Data != null)
                        {

                            employee = (EmployeeVsRosterVM)responseObj.Data;
                        }
                        else
                        {
                            MessageBoxModal.Show(General.ResolveOwnerWindow(), responseObj.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBoxModal.Show(General.ResolveOwnerWindow(), responseObj.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    if (responseObj.Message.Contains("shift today"))
                    {
                        MessageBoxResult res = MessageBoxModal.Show(General.ResolveOwnerWindow(), responseObj.Message + " Do you like to add it into the roster today", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Error);
                        if (res == MessageBoxResult.Yes)
                        {

                            MessageResponseViewModel<EmployeeViewModel> responseemploy = await ServiceEmployee.GetEmploy(employeer.Employee);
                            if (responseemploy.Succesfull)
                            {
                                RosterCViewModel roster = new RosterCViewModel();
                                roster.Date = DateTime.Now.Date;
                                roster.EventName = "Shift Added";
                                roster.Employee = responseemploy.Data.FullName;
                                roster.Break = 30;
                                string HourScan = General.ConvertDatetoMilitaryHour(DateTime.Now);
                                TimeSpan tsScan = TimeSpan.ParseExact(HourScan, "hhmm", null);
                                roster.StartTime = General.RoundToNearest(tsScan, TimeSpan.FromMinutes(15));
                                TimeSpan tsScanEnd = tsScan.Add(new TimeSpan(7, 0, 0));
                                roster.EndTime = General.RoundToNearest(tsScanEnd, TimeSpan.FromMinutes(15));
                                roster.LabourType = "General Labour";
                                roster.Payroll = responseemploy.Data.Payroll;
                                roster.ShiftNum = 1;
                                MessageResponseViewModel<bool> responseroster = await ServiceRoster.SetRoster(roster);
                                if (responseroster.Succesfull)
                                {
                                    MessageResponseViewModel<EmployeeVsRosterVM> responseEmploRoster = await ServiceEmployee.GetEmployee(employeer);
                                    if (responseEmploRoster.Succesfull != false)
                                    {
                                        if (responseEmploRoster.Data != null)
                                        {

                                            employee = (EmployeeVsRosterVM)responseEmploRoster.Data;
                                        }
                                        else
                                        {
                                            MessageBoxModal.Show(General.ResolveOwnerWindow(), responseObj.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Error);
                                        }
                                    }
                                    else
                                    {
                                        MessageBoxModal.Show(General.ResolveOwnerWindow(), responseObj.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Error);
                                    }
                                }
                            }
                            //Get employee

                            // roster.Employee = (EmployeeVsRosterVM)responseObj.Data.
                            // MessageResponseViewModel<bool> responseCreate = await  ServiceRoster.SetRoster(roster);
                        }
                    }
                    else
                    {
                        MessageBoxModal.Show(General.ResolveOwnerWindow(), responseObj.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                txtScan.Text = string.Empty;
            }
        }
    }
}
