using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using Incog.wpf.Messages;

namespace Axede.WPF.FilterPopup
{
    /// <summary>
    /// Lógica de interacción para FilterHost.xaml
    /// </summary>
    public partial class FilterHost : UserControl, INotifyPropertyChanged
    {

        #region Definiciones

            private int _IndexColumn = 0;
            public int IndexColumn
            {
                get { return _IndexColumn; }
                set { _IndexColumn = value; }
            }

            private string _ColumnName = string.Empty;
            public string ColumnName
            {
                get { return _ColumnName; }
                set { _ColumnName = value; }
            }

            private string _FieldName = string.Empty;
            public string FieldName
            {
                get { return _FieldName; }
                set { _FieldName = value; }
            }

            private string _FilterExpression;
            public string FilterExpression
            {
                get { return _FilterExpression; }
                set { _FilterExpression = value; }
            }

            private bool _CancelarOperacion = false;
            public bool CancelarOperacion
            {
                get { return _CancelarOperacion; }
                set
                {
                    _CancelarOperacion = value;
                    NotifyPropertyChanged("CancelarOperacion");
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

            private bool _RemoverFiltro = false;
            public bool RemoverFiltro
            {
                get { return _RemoverFiltro; }
                set
                {
                    _RemoverFiltro = value;
                    NotifyPropertyChanged("RemoverFiltro");
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


        #endregion

        #region EventosControls

            public FilterHost()
            {
                InitializeComponent();
                Inicio();
            }

            private void UserControl_Loaded(object sender, RoutedEventArgs e)
            {
                BoderFooter.Width = this.ActualWidth;
                btnCancelar.SetValue(Canvas.LeftProperty, ActualWidth - btnCancelar.ActualWidth);
                btnAceptar.SetValue(Canvas.LeftProperty, ActualWidth - btnCancelar.ActualWidth - btnAceptar.ActualWidth);
            }

        #endregion

        #region Botones Principales

            private void btnAceptar_Click(object sender, RoutedEventArgs e)
            {
                ActivarFiltro = true;
            }

            private void btnCancelar_Click(object sender, RoutedEventArgs e)
            {
                CancelarOperacion = true;
            }

            private void btnRemoverFiltro_Click(object sender, RoutedEventArgs e)
            {
                RemoverFiltro = true;
            }
        
        #endregion

        #region Generales

            private void Inicio() 
            {
                CargaRecursos();
            }

            private void CargaRecursos()
            {
                btnAceptar.ToolTip = AdministradorMensaje.Instance.GetMensajePorCodigo(Incog.wpf.Messages.CodeMessagesGeneral.General_FiltroToolTipBotonAceptar);
                btnCancelar.ToolTip = AdministradorMensaje.Instance.GetMensajePorCodigo(Incog.wpf.Messages.CodeMessagesGeneral.General_FiltroToolTipBotonCancelar);
                btnRemoverFiltro.ToolTip = AdministradorMensaje.Instance.GetMensajePorCodigo(Incog.wpf.Messages.CodeMessagesGeneral.General_FiltroToolTipBotonRemover);
            }

        #endregion

          

    }
}
