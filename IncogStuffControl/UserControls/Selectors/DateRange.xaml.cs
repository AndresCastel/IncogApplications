using Incog.Utils;
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

namespace IncogStuffControl.UserControls.Selectors
{
    /// <summary>
    /// Interaction logic for DateRange.xaml
    /// </summary>
    public partial class DateRange : UserControl, INotifyPropertyChanged
    {
        public DateRange()
        {
            InitializeComponent();
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


        private DateTime _dateEnd;
        public DateTime dateEnd
        {
            get { return dtpDateTo.SelectedDate.Value; }
            set
            {
                _dateEnd = value;

            }
        }

        private DateTime _dateInitial;
        public DateTime dateInitial
        {
            get { return dtpDateFrom.SelectedDate.Value; }
            set
            {
                _dateInitial = value;

            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dtpDateFrom.SelectedDate = DateTime.Now;
            dtpDateTo.SelectedDate = DateTime.Now;
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            save = true;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DtpDateTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtpDateTo.SelectedDate != null && dtpDateFrom.SelectedDate != null)
            {
                var Datefrom = Convert.ToDateTime(dtpDateFrom.SelectedDate.Value.ToShortDateString()); //this sets time to 00:00:00
                var Dateto = Convert.ToDateTime(dtpDateTo.SelectedDate.Value.ToShortDateString());
                //do a normal compare
                if (Dateto < Datefrom)
                {
                    MessageBoxModal.Show(General.ResolveOwnerWindow(), "End date cannot be less than Initial date", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);
                    dtpDateTo.SelectedDate = dtpDateFrom.SelectedDate.Value;
                }
            }
        }
    }
}
