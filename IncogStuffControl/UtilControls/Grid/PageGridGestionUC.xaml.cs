using Incog.Utils.Enums;
using Incog.wpf.Messages;
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

namespace IncogStuffControl.UtilControls.Grid
{
    /// <summary>
    /// Interaction logic for PageGridGestionUC.xaml
    /// </summary>
    public partial class PageGridGestionUC : UserControl, INotifyPropertyChanged
    {

        #region Definiciones

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

        private TipoPaginaPaginado _NotificarAccionPag;

        public TipoPaginaPaginado NotificarAccionPag
        {
            get { return _NotificarAccionPag; }
            set
            {
                _NotificarAccionPag = value;
                NotifyPropertyChanged("NotificarAccionPag");
            }
        }

        #endregion

        #region Eventos Control

        public PageGridGestionUC()
        {
            InitializeComponent();
            if (!ApplicationIsInDesignMode)
            {
                CargaRecursos();
            }

        }

        #endregion

        #region Generales

        private void CargaRecursos()
        {
            BtnPagInicial.ToolTip = AdministradorMensaje.Instance.GetMensajePorCodigo(Incog.wpf.Messages.CodeMessagesGeneral.General_ToolTipBotonPrimeraPagina);
            BtnPagAnterior.ToolTip = AdministradorMensaje.Instance.GetMensajePorCodigo(Incog.wpf.Messages.CodeMessagesGeneral.General_ToolTipBotonAnteriorPagina);
            BtnPagSiguiente.ToolTip = AdministradorMensaje.Instance.GetMensajePorCodigo(Incog.wpf.Messages.CodeMessagesGeneral.General_ToolTipBotonSiguientePagina);
            BtnPagFinal.ToolTip = AdministradorMensaje.Instance.GetMensajePorCodigo(Incog.wpf.Messages.CodeMessagesGeneral.General_ToolTipBotonUltimaPagina);
        }

        #endregion

        #region Botones Principales

        private void BtnPagInicial_Click(object sender, RoutedEventArgs e)
        {
            NotificarAccionPag = TipoPaginaPaginado.PrimeraPagina;
        }

        private void BtnPagAnterior_Click(object sender, RoutedEventArgs e)
        {
            NotificarAccionPag = TipoPaginaPaginado.PaginaAnterior;
        }

        private void BtnPagSiguiente_Click(object sender, RoutedEventArgs e)
        {
            NotificarAccionPag = TipoPaginaPaginado.PaginaSiguiente;
        }

        private void BtnPagFinal_Click(object sender, RoutedEventArgs e)
        {
            NotificarAccionPag = TipoPaginaPaginado.UltimaPagina;
        }

        #endregion

        #region Privadas

        private static bool ApplicationIsInDesignMode
        {
            get { return (bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue); }
        }

        #endregion
    }
}
