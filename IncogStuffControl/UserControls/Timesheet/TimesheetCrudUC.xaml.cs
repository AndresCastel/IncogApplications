using Incog.Utils;
using IncogStuffControl.Services;
using IncogStuffControl.Services.ViewModel;
using IncogStuffControl.UtilClass.ResourceEnum;
using IncogStuffControl.UtilControls.ModalMessageBox;
using IncogStuffControl.UtilControls.ViewModal;
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

namespace IncogStuffControl.UserControls.Timesheet
{
    /// <summary>
    /// Interaction logic for TimesheetCrudUC.xaml
    /// </summary>
    public partial class TimesheetCrudUC : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// Evento de acuerdo a la interfáz.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
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

        private bool _NotificarAccion;
        public bool NotificarAccion
        {
            get { return _NotificarAccion; }
            set
            {
                _NotificarAccion = value;
                NotifyPropertyChanged("NotificarAccion");
            }
        }

        List<TimesheetsReportViewModel> timesheetsList;

        public TimesheetCrudUC(OperacionCRUD Oper, TimesheetsReportViewModel Time, List<TimesheetsReportViewModel> timesheets)
        {
            InitializeComponent();
            _TipoOperacion = Oper;
            _Timesheet = Time;
            timesheetsList = timesheets;
            OpenControl(Oper);

        }

        public void OpenControl(OperacionCRUD OperationType)
        {
            switch (OperationType)
            {
                case OperacionCRUD.Nuevo:
                    OpenNewMode();

                    break;
                case OperacionCRUD.Modificar:

                    OpenEditMode();

                    break;

                case OperacionCRUD.Consultar:
                    OpenViewMode();
                    break;

                default:
                    break;
            }
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (_TipoOperacion == OperacionCRUD.Modificar)
            {
                
                    TimesheetsReportViewModel time = new TimesheetsReportViewModel();
                if(string.IsNullOrEmpty(txtStartTime.Text) && !string.IsNullOrEmpty(txtEndTime.Text))
                {
                    MessageBoxModal.Show(General.ResolveOwnerWindow(), "You can not remove the start time if the end time is still set it", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);
                    return;
                }
                if (string.IsNullOrEmpty(txtStartTime.Text) && string.IsNullOrEmpty(txtEndTime.Text))
                {
                    MessageBoxModal.Show(General.ResolveOwnerWindow(), "Delete the register from the grid", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);
                    return;
                }
                time.StartTime = txtStartTime.Text;
                    time.EndTime = txtEndTime.Text;
                    time.Break = (int)cmbBreak.cmbBreaks.SelectedItem;
                    time.Id = _Timesheet.Id;
                    if(!string.IsNullOrEmpty(time.EndTime))
                    {
                    time.Active = false;
                    }
                    else
                    {
                    time.Active = true;
                    time.Break = 0;
                }

                    MessageResponseViewModel<bool> result = await ServiceEmployee.EditTimesheets(time).ConfigureAwait(true);
                    if (result.Succesfull)
                    {
                        MessageBoxModal.Show(General.ResolveOwnerWindow(), "Timesheet sucesfully edited", "Information", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Cancel, true);
                        
                        NotificarAccion = true;
                    }
                    else
                    {
                        MessageBoxModal.Show(General.ResolveOwnerWindow(), result.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Cancel, true);
                    }
            }
        }

    public async void FillInfo()
        {

            MessageResponseViewModel<List<EmployeeViewModel>> responseObj = await ServiceEmployee.GetAllEmployees().ConfigureAwait(true);
            if (responseObj.Succesfull)
            {
                EmployeeViewModel em = new EmployeeViewModel();
                ListEmployee = responseObj.Data.OrderBy(o => o.FullName).ToList();
                em = ListEmployee.Where(o => o.Payroll.TrimStart(new Char[] { '0' }) == _Timesheet.Payroll.TrimStart(new Char[] { '0' })).FirstOrDefault();

                cmbEmployee.ItemsSource = ListEmployee;
                //Fill Values
                DateTime datecast = General.SplitCreateDate(_Timesheet.Day);
                dtpDate.SelectedDate = datecast;
                txtPrecint.Text = _Timesheet.Precint;
                txtZone.Text = _Timesheet.Zone;
                cmbBreak.cmbBreaks.SelectedItem = _Timesheet.Break;
                txtArea.Text = _Timesheet.Area;
                cmbEmployee.SelectedItem = em;
                txtPayroll.Text = _Timesheet.Payroll;
                txtStartTime.Text = _Timesheet.StartTime;
                txtEndTime.Text = _Timesheet.EndTime;
            }
            else
            {
                MessageBoxModal.Show(General.ResolveOwnerWindow(), responseObj.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);
            }
        }

        private async void OpenEditMode()
        {
            if (_Timesheet != null)
            {
                if (_Timesheet.Id > 0)
                {
                    //Enable Fields
                    dtpDate.IsEnabled = false;
                    txtPrecint.IsEnabled = false;
                    txtZone.IsEnabled = false;
                    txtArea.IsEnabled = false;
                    cmbEmployee.IsEnabled = false;
                    txtPayroll.IsEnabled = false;
                    btnGuardar.Content = "Edit";
                    FillInfo();
                }
            }
            else
            {
                MessageBoxModal.Show(General.ResolveOwnerWindow(), "Times are not be able to be Charged ", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Cancel, true);
            }

        }

        private async void OpenNewMode()
        {
            MessageResponseViewModel<List<EmployeeViewModel>> responseObj = await ServiceEmployee.GetAllEmployees();
            if (responseObj.Succesfull)
            {
                EmployeeViewModel em = new EmployeeViewModel();
                ListEmployee = responseObj.Data.OrderBy(o => o.FullName).ToList();
                cmbEmployee.ItemsSource = ListEmployee;

            }
        }

        private  void OpenViewMode()
        {
            if (_Timesheet != null)
            {
                if (_Timesheet.Id > 0)
                {
                    //Enable Fields
                    dtpDate.IsEnabled = false;
                    txtPrecint.IsEnabled = false;
                    txtZone.IsEnabled = false;
                    txtArea.IsEnabled = false;
                    cmbEmployee.IsEnabled = false;
                    txtPayroll.IsEnabled = false;
                    txtStartTime.IsEnabled = false;
                    txtEndTime.IsEnabled = false;
                    cmbBreak.IsEnabled = false;
                    btnGuardar.IsEnabled = false;

                    FillInfo();
                }
            }
            else
            {
                MessageBoxModal.Show(General.ResolveOwnerWindow(), "Times are not be able to be Charged ", "Error", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);
            }

        }

        private List<EmployeeViewModel> _ListEmployee;
        public List<EmployeeViewModel> ListEmployee
        {
            get { return _ListEmployee; }
            set { _ListEmployee = value; }
        }
        private OperacionCRUD _TipoOperacion;
        public OperacionCRUD TipoOperacion
        {
            get { return _TipoOperacion; }
            set { _TipoOperacion = value; }
        }

        private TimesheetsReportViewModel _Timesheet;
        public TimesheetsReportViewModel Timesheet
        {
            get { return _Timesheet; }
            set { _Timesheet = value; }
        }

        public TimesheetCrudUC()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
