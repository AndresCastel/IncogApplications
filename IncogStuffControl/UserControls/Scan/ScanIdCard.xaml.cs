﻿using IncogStuffControl.Services;
using IncogStuffControl.Services.ViewModel;
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
        private EmployeeRegisterViewModel _employee;
        public EmployeeRegisterViewModel employee
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

            //uncomment when scan is working
            if (txtScan.Text.Length >= 12)
            {
                EmployeeRegisterViewModel responseObj = await ServiceEmployee.GetEmployee(txtScan.Text);
                if (responseObj != null)
                {
                    employee = responseObj;
                }
                else
                {
                    MessageBox.Show("This employee does not exist on our system");
                }

                txtScan.Text = string.Empty;
            }
        }

        //private async void btnTest_Click(object sender, RoutedEventArgs e)
        //{
        //    if (txtScan.Text.Length >= 12)
        //    {
        //        //EmployeeViewModel responseObj = await  ServiceEmployee.GetEmployee(txtScan.Text);
        //        //if (responseObj != null)
        //        //{
        //        //    employee = responseObj;
        //        //}
        //        //else
        //        //{
        //        //    MessageBox.Show("This employee does not exist on our system");
        //        //}

        //        //txtScan.Text = string.Empty;
        //    }
        //}
    }
}
