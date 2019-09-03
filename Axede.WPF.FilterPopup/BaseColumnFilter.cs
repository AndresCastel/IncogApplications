using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Controls.Primitives;

namespace Axede.WPF.FilterPopup
{

    public class BaseColumnFilter : UserControl, INotifyPropertyChanged
    {
        private bool _Visible=false;
        private FilterHost _FilterHost = null;
        private DataGridColumnHeader _gridColumnHeader = null;
        private string _ColumnName = string.Empty;

        public string ColumnName
        {
            get { return _ColumnName; }
            set { _ColumnName = value; }
        }


        private string _Filtro = string.Empty;
        public string Filtro
        {
            get { return _Filtro; }
            set
            {
                _Filtro = value;
                NotifyPropertyChanged("Filtro");
            }
        }

        private bool _ActivarFiltro = false;
        public bool ActivarFiltro
        {
            get { return _ActivarFiltro; }
            set
            {
                _ActivarFiltro = value;
                NotifyPropertyChanged("ActivarFiltro");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public DataGridColumnHeader GridColumnHeader
        {
            get { return _gridColumnHeader; }
            set { _gridColumnHeader = value; }
        }

        public FilterHost FilterHost
        {
            get { return _FilterHost; }
            set { _FilterHost = value; }
        }

        public bool Visible
        {
            get { return _Visible; }
            set { _Visible = value; }
        }

        
    }
}
