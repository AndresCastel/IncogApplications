using Incog.Utils;
using IncogStuffControl.Services;
using IncogStuffControl.Services.ViewModel;
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

namespace IncogStuffControl.UserControls.MainBoard
{
    /// <summary>
    /// Interaction logic for MainBoardUC.xaml
    /// </summary>
    public partial class MainBoardUC : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// Evento de acuerdo a la interfáz.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public int Type_Checked;
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

        private bool _save;
        public bool save
        {
            get { return _save; }
            set
            {
                _save = value;
                if (value)
                {
                    NotifyPropertyChanged("save");
                }
            }
        }

        public EmployeeRegisterViewModel employeepriv;
        public MainBoardUC()
        {
            InitializeComponent();
        }

        public MainBoardUC(EmployeeRegisterViewModel employee)
        {
            InitializeComponent();
            employeepriv = employee;
            SetEmployee(employee);
            if (chkSignIn.IsChecked.Value)
            {
                Type_Checked = 1;
            }
            else if (chkSignOff.IsChecked.Value)
            {
                Type_Checked = 2;
            }
            else if (chkEquipment.IsChecked.Value)
            {
                Type_Checked = 3;
            }
        }

        private void SetEmployee(EmployeeRegisterViewModel employee)
        {
            txtName.Text = employee.Employee.Name + " " + employee.Employee.MiddleName + " " + employee.Employee.LastName;
            //Section stuff
            //if (employee.SignIn != DateTime.MinValue)
            //{
            //    lblSignIn.Text = employee.SignIn.ToString();
            //}
            //if (employee.Signoff != DateTime.MinValue)
            //{
            //    lblSignOff.Text = employee.Signoff.ToString();
            //}
            if (employee.lstStuffAssig != null)
            {
                foreach (var item in employee.lstStuffAssig)
                {
                    SetStuff(item);
                }
            }

        }

        private void SetStuff(StuffAssignViewModel StuffAssig)
        {

            switch (StuffAssig.StuffId)
            {
                case 1:
                    UniformsUC.txtPolo.Text = StuffAssig.Quantity.ToString();
                    break;
                case 2:
                    UniformsUC.txtRainJacket.Text = StuffAssig.Quantity.ToString();
                    break;
                case 8:
                    UniformsUC.txtMandarine.Text = StuffAssig.Quantity.ToString();
                    break;
                case 9:
                    UniformsUC.txtFleece.Text = StuffAssig.Quantity.ToString();
                    break;
                case 3:
                    KeysUC.txtColorado.Text = StuffAssig.Quantity.ToString();
                    break;
                case 4:
                    KeysUC.txtTrayTruck.Text = StuffAssig.Quantity.ToString();
                    break;
                case 10:
                    KeysUC.txtSweeper.Text = StuffAssig.Quantity.ToString();
                    break;
                case 5:
                    RadioUC.rbtRadio1.IsChecked = true;
                    break;
                case 6:
                    RadioUC.rbtRadio2.IsChecked = true;
                    break;
                case 7:
                    RadioUC.rbtRadio3.IsChecked = true;
                    break;
                case 11:
                    RadioUC.rbtRadio4.IsChecked = true;
                    break;
                case 12:
                    RadioUC.rbtRadio5.IsChecked = true;
                    break;
                case 13:
                    RadioUC.rbtRadio6.IsChecked = true;
                    break;
                case 14:
                    RadioUC.rbtRadio7.IsChecked = true;
                    break;
                case 15:
                    RadioUC.rbtRadio8.IsChecked = true;
                    break;
                case 16:
                    RadioUC.rbtRadio9.IsChecked = true;
                    break;
                case 17:
                    RadioUC.rbtRadio10.IsChecked = true;
                    break;
                case 18:
                    RadioUC.rbtRadio11.IsChecked = true;
                    break;
                case 19:
                    RadioUC.rbtRadio12.IsChecked = true;
                    break;
                case 20:
                    RadioUC.rbtRadio13.IsChecked = true;
                    break;
                case 21:
                    RadioUC.rbtRadio14.IsChecked = true;
                    break;
                case 22:
                    RadioUC.rbtRadio15.IsChecked = true;
                    break;

                default:
                    break;
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            EmployeeRegisterViewModel EmployeeRegister = new EmployeeRegisterViewModel();
            switch (Type_Checked)
            {
                //Sign In
                case 1:
                    //if (employeepriv.Type_RegisterId == 1 && employeepriv.SignIn != DateTime.MinValue)
                    //{
                    //    MessageBoxModal.Show(ClassMethodUtil.ResolveOwnerWindow(), "You already did Sign In: " + employeepriv.SignIn, "Information", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.No, true);
                    //    return;
                    //}
                    break;
                //Sign off
                case 2:
                    //if (employeepriv.Type_RegisterId != 1 || employeepriv.SignIn == DateTime.MinValue)
                    //{
                    //    MessageBoxModal.Show(ClassMethodUtil.ResolveOwnerWindow(), "You have not signed In", "Information", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);
                    //    return;
                    //}
                    BreakUC oUC_Break = new BreakUC();
                    MessageBoxResult Response = ViewWindow_Modal.Show(oUC_Break, "Break", oUC_Break.btnCancel);
                    if (Response == MessageBoxResult.Cancel)
                    {
                        return;
                    }
                    if(Response == MessageBoxResult.OK)
                    {
                        EmployeeRegister.Break = oUC_Break.Break;

                    }
                    break;
                case 3://Equipment
                    //if (employeepriv.Type_RegisterId != 1 || employeepriv.SignIn == DateTime.MinValue)
                    //{
                    //    MessageBoxModal.Show(ClassMethodUtil.ResolveOwnerWindow(), "You have not signed In", "Information", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);
                    //    return;
                    //}
                    break;
                default:
                    break;
            }


                
            EmployeeRegister.Active = true;
            //Save Employer Register Info
            #region Employer Register
            switch (Type_Checked)
                {
                    case 1:
                        EmployeeRegister.Day = DateTime.Now.Date;
                        break;
                    case 2:
                        //EmployeeRegister.SignIn = employeepriv.SignIn;
                        //EmployeeRegister.Signoff = DateTime.Now;
                    EmployeeRegister.Active = false;
                    break;
                    default:
                        break;
                }
                EmployeeRegister.Id = employeepriv.Id;
                EmployeeRegister.EmployeeId = employeepriv.Employee.Id;


            //Set Type of Register Sign In, Sign off, Equipment
            if (Type_Checked != 3)
            {
                EmployeeRegister.Type_RegisterId = Type_Checked;
            }
            else
            {
                EmployeeRegister.Type_RegisterId = employeepriv.Type_RegisterId;
            }

                #endregion

                //Save Assign Stuff to employee
                #region Assign Stuff List
                List<StuffAssignViewModel> lstAssignstuff = new List<StuffAssignViewModel>();
                //Polo Section            
                if (UniformsUC.txtPolo.Text != string.Empty)
                {
                    if (Convert.ToInt16(UniformsUC.txtPolo.Text) > 0)
                    {
                        StuffAssignViewModel StuffAssign = new StuffAssignViewModel();
                        StuffAssign.Quantity = Convert.ToInt16(UniformsUC.txtPolo.Text);
                        StuffAssign.StuffId = 1;
                        StuffAssign.Active = true;
                        lstAssignstuff.Add(StuffAssign);
                    }
                }

                //Rain Jacket Section            
                if (UniformsUC.txtRainJacket.Text != string.Empty)
                {
                    if (Convert.ToInt16(UniformsUC.txtRainJacket.Text) > 0)
                    {
                        StuffAssignViewModel StuffAssign = new StuffAssignViewModel();
                        StuffAssign.Quantity = Convert.ToInt16(UniformsUC.txtRainJacket.Text);
                        StuffAssign.StuffId = 2;
                        StuffAssign.Active = true;
                        lstAssignstuff.Add(StuffAssign);
                    }
                }

                //Mandarin Section            
                if (UniformsUC.txtMandarine.Text != string.Empty)
                {
                    if (Convert.ToInt16(UniformsUC.txtMandarine.Text) > 0)
                    {
                        StuffAssignViewModel StuffAssign = new StuffAssignViewModel();
                        StuffAssign.Quantity = Convert.ToInt16(UniformsUC.txtMandarine.Text);
                        StuffAssign.StuffId = 8;
                        StuffAssign.Active = true;
                        lstAssignstuff.Add(StuffAssign);
                    }
                }

                //Fleece Jacket Section            
                if (UniformsUC.txtFleece.Text != string.Empty)
                {
                    if (Convert.ToInt16(UniformsUC.txtFleece.Text) > 0)
                    {
                        StuffAssignViewModel StuffAssign = new StuffAssignViewModel();
                        StuffAssign.Quantity = Convert.ToInt16(UniformsUC.txtFleece.Text);
                        StuffAssign.StuffId = 9;
                        StuffAssign.Active = true;
                        lstAssignstuff.Add(StuffAssign);
                    }
                }

                //Colorado Key Section            
                if (KeysUC.txtColorado.Text != string.Empty)
                {
                    if (Convert.ToInt16(KeysUC.txtColorado.Text) > 0)
                    {
                        StuffAssignViewModel StuffAssign = new StuffAssignViewModel();
                        StuffAssign.Quantity = Convert.ToInt16(KeysUC.txtColorado.Text);
                        StuffAssign.StuffId = 3;
                        StuffAssign.Active = true;
                        lstAssignstuff.Add(StuffAssign);
                    }
                }

                //Tray Truck Key Section            
                if (KeysUC.txtTrayTruck.Text != string.Empty)
                {
                    if (Convert.ToInt16(KeysUC.txtTrayTruck.Text) > 0)
                    {
                        StuffAssignViewModel StuffAssign = new StuffAssignViewModel();
                        StuffAssign.Quantity = Convert.ToInt16(KeysUC.txtTrayTruck.Text);
                        StuffAssign.StuffId = 4;
                        StuffAssign.Active = true;
                        lstAssignstuff.Add(StuffAssign);
                    }
                }


                //Sweeper Key Section            
                if (KeysUC.txtSweeper.Text != string.Empty)
                {
                    if (Convert.ToInt16(KeysUC.txtSweeper.Text) > 0)
                    {
                        StuffAssignViewModel StuffAssign = new StuffAssignViewModel();
                        StuffAssign.Quantity = Convert.ToInt16(KeysUC.txtSweeper.Text);
                        StuffAssign.StuffId = 10;
                        StuffAssign.Active = true;
                        lstAssignstuff.Add(StuffAssign);
                    }
                }

                //Radios Section

                if (RadioUC.rbtRadio1.IsChecked.Value)
                {
                    StuffAssignViewModel StuffAssign = new StuffAssignViewModel();
                    StuffAssign.Quantity = 1;
                    StuffAssign.StuffId = 5;
                    StuffAssign.Active = true;
                    lstAssignstuff.Add(StuffAssign);
                }

                if (RadioUC.rbtRadio2.IsChecked.Value)
                {
                    StuffAssignViewModel StuffAssign = new StuffAssignViewModel();
                    StuffAssign.Quantity = 1;
                    StuffAssign.StuffId = 6;
                    StuffAssign.Active = true;
                    lstAssignstuff.Add(StuffAssign);
                }

                if (RadioUC.rbtRadio3.IsChecked.Value)
                {
                    StuffAssignViewModel StuffAssign = new StuffAssignViewModel();
                    StuffAssign.Quantity = 1;
                    StuffAssign.StuffId = 7;
                    StuffAssign.Active = true;
                    lstAssignstuff.Add(StuffAssign);
                }

                if (RadioUC.rbtRadio4.IsChecked.Value)
                {
                    StuffAssignViewModel StuffAssign = new StuffAssignViewModel();
                    StuffAssign.Quantity = 1;
                    StuffAssign.StuffId = 11;
                    StuffAssign.Active = true;
                    lstAssignstuff.Add(StuffAssign);
                }

                if (RadioUC.rbtRadio5.IsChecked.Value)
                {
                    StuffAssignViewModel StuffAssign = new StuffAssignViewModel();
                    StuffAssign.Quantity = 1;
                    StuffAssign.StuffId = 12;
                    StuffAssign.Active = true;
                    lstAssignstuff.Add(StuffAssign);
                }

                if (RadioUC.rbtRadio6.IsChecked.Value)
                {
                    StuffAssignViewModel StuffAssign = new StuffAssignViewModel();
                    StuffAssign.Quantity = 1;
                    StuffAssign.StuffId = 13;
                    StuffAssign.Active = true;
                    lstAssignstuff.Add(StuffAssign);
                }

                if (RadioUC.rbtRadio7.IsChecked.Value)
                {
                    StuffAssignViewModel StuffAssign = new StuffAssignViewModel();
                    StuffAssign.Quantity = 1;
                    StuffAssign.StuffId = 14;
                    StuffAssign.Active = true;
                    lstAssignstuff.Add(StuffAssign);
                }

                if (RadioUC.rbtRadio8.IsChecked.Value)
                {
                    StuffAssignViewModel StuffAssign = new StuffAssignViewModel();
                    StuffAssign.Quantity = 1;
                    StuffAssign.StuffId = 15;
                    StuffAssign.Active = true;
                    lstAssignstuff.Add(StuffAssign);
                }

                if (RadioUC.rbtRadio9.IsChecked.Value)
                {
                    StuffAssignViewModel StuffAssign = new StuffAssignViewModel();
                    StuffAssign.Quantity = 1;
                    StuffAssign.StuffId = 16;
                    StuffAssign.Active = true;
                    lstAssignstuff.Add(StuffAssign);
                }

                if (RadioUC.rbtRadio10.IsChecked.Value)
                {
                    StuffAssignViewModel StuffAssign = new StuffAssignViewModel();
                    StuffAssign.Quantity = 1;
                    StuffAssign.StuffId = 17;
                    StuffAssign.Active = true;
                    lstAssignstuff.Add(StuffAssign);
                }

                if (RadioUC.rbtRadio11.IsChecked.Value)
                {
                    StuffAssignViewModel StuffAssign = new StuffAssignViewModel();
                    StuffAssign.Quantity = 1;
                    StuffAssign.StuffId = 18;
                    StuffAssign.Active = true;
                    lstAssignstuff.Add(StuffAssign);
                }

                if (RadioUC.rbtRadio12.IsChecked.Value)
                {
                    StuffAssignViewModel StuffAssign = new StuffAssignViewModel();
                    StuffAssign.Quantity = 1;
                    StuffAssign.StuffId = 19;
                    StuffAssign.Active = true;
                    lstAssignstuff.Add(StuffAssign);
                }

                if (RadioUC.rbtRadio13.IsChecked.Value)
                {
                    StuffAssignViewModel StuffAssign = new StuffAssignViewModel();
                    StuffAssign.Quantity = 1;
                    StuffAssign.StuffId = 20;
                    StuffAssign.Active = true;
                    lstAssignstuff.Add(StuffAssign);
                }

                if (RadioUC.rbtRadio14.IsChecked.Value)
                {
                    StuffAssignViewModel StuffAssign = new StuffAssignViewModel();
                    StuffAssign.Quantity = 1;
                    StuffAssign.StuffId = 21;
                    StuffAssign.Active = true;
                    lstAssignstuff.Add(StuffAssign);
                }

                if (RadioUC.rbtRadio15.IsChecked.Value)
                {
                    StuffAssignViewModel StuffAssign = new StuffAssignViewModel();
                    StuffAssign.Quantity = 1;
                    StuffAssign.StuffId = 22;
                    StuffAssign.Active = true;
                    lstAssignstuff.Add(StuffAssign);
                }
                #endregion

                EmployeeRegister.lstStuffAssig = lstAssignstuff;

                MessageResponseViewModel<EmployeeRegisterViewModel> responseObj = await ServiceEmployee.RegisterEmployeeStuff(EmployeeRegister);
                if (!responseObj.Succesfull)
                {
                    MessageBoxModal.Show(ClassMethodUtil.ResolveOwnerWindow(), responseObj.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);
                    return;
                }

           

            save = true;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox)
            {
                if (chkEquipment != null && chkSignOff != null && chkEquipment != null)
                {
                    if (((CheckBox)sender).Name == "chkSignIn")
                    {
                        chkEquipment.IsChecked = false;
                        chkSignOff.IsChecked = false;
                        Type_Checked = 1;
                    }
                    if (((CheckBox)sender).Name == "chkSignOff")
                    {
                        chkEquipment.IsChecked = false;
                        chkSignIn.IsChecked = false;
                        Type_Checked = 2;
                    }
                    if (((CheckBox)sender).Name == "chkEquipment")
                    {
                        chkSignOff.IsChecked = false;
                        chkSignIn.IsChecked = false;
                        Type_Checked = 3;
                    }
                }
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            save = true;
        }
    }
}
