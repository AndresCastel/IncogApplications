using Axede.WPF.FilterPopup;
using System.Data;
using Incog.Utils;
using Incog.Utils.Enums;
using Incog.wpf.Messages;
using IncogStuffControl.Services;
using IncogStuffControl.Services.ViewModel;
using IncogStuffControl.UtilControls.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using IncogStuffControl.UtilControls.ModalMessageBox;
using IncogStuffControl.UserControls.Selectors;
using IncogStuffControl.UtilControls.ViewModal;

namespace IncogStuffControl.UserControls.Roster
{
    /// <summary>
    /// Interaction logic for RosterAdminUC.xaml
    /// </summary>
    public partial class RosterAdminUC : UserControl, INotifyPropertyChanged
    {
        private string _OrdenCampo = "Asc";
        private RosterWM lstRoster;
        public string CampoOrden
        {
            get { return _CampoOrden; }
            set { _CampoOrden = value; }
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
        private string _CampoOrden = "Nom_Tercero, Nom_EstadoGestion";
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

        private FilterParametersRoster _Filter;
        public FilterParametersRoster Filter
        {
            get { return _Filter; }
            set
            {
                _Filter = value;

            }
        }

        public string OrdenCampo
        {
            get { return _OrdenCampo; }
            set { _OrdenCampo = value; }
        }

        private DateTime _dateFilter;
        public DateTime dateFilter
        {
            get { return dtpDate.SelectedDate.Value; }
            set
            {
                _dateFilter = value;

            }
        }

        private DateRange _dateRange;
        public DateRange dateRange
        {
            get { return _dateRange; }
            set
            {
                _dateRange = value;

            }
        }

        public RosterAdminUC(bool oControlExpandido = false)
        {
            InitializeComponent();
            dtpDate.SelectedDate = DateTime.Now;
           
            if (oControlExpandido == true)
            {
                parentstack.Width = double.NaN;
                parentstack.Height = double.NaN;
            }

        }

        private async void FillGrid()
        {
            MessageResponseViewModel<RosterWM> responseObj = await ServiceRoster.GetRoster(Filter);
            if (responseObj.Succesfull)
            { 
            grvRoster.ItemsSource = responseObj.Data.lstRoster;
                lblReg.Content = responseObj.Data.lstRoster.Count();
            }
            else
            {
                MessageBoxModal.Show(General.ResolveOwnerWindow(), responseObj.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Uc_RosterAdmin_Loaded(object sender, RoutedEventArgs e)
        {
            Inicio();
        }

        private void grvRosterAdmin_Loaded(object sender, RoutedEventArgs e)
        {
            if (oWPFFilterPopupManager == null)
            {
                oWPFFilterPopupManager = new WPFFilterPopupManager();
                oWPFFilterPopupManager.ColumnFilterAdding += new ColumnFilterEventHandler(oWPFFilterPopupManager_ColumnFilterAdding);
                oWPFFilterPopupManager.ColumnFilterExec += new ColumnFilterEventHandler(oWPFFilterPopupManager_ColumnFilterExec);
                oWPFFilterPopupManager.DataGrid = grvRoster;
            }
        }
   

        #region Definiciones
       // private AdminEstadoGestion_Presenter _AdminEstadoGestion_Presenter;


       
        private WPFFilterPopupManager oWPFFilterPopupManager = null;

      

        #endregion

        #region Eventos Formulario

        public RosterAdminUC()
        {
            InitializeComponent();
            //
          
            parentstack.Width = double.NaN;
            parentstack.Height = double.NaN;


        }
        

       

        #endregion

        #region Loading Grid

      

       
        #endregion

        #region General

        public void Inicio()
        {
           
            CargaRecursos();

        }

        private void ObtenerListaEstadoGestion()
        {

        }

        private void CargaRecursos()
        {

            grvRoster.Columns[3].Header = "Day";
            grvRoster.Columns[4].Header = "Date";
            grvRoster.Columns[5].Header = "Start Time";
            grvRoster.Columns[6].Header = "End Time";
            grvRoster.Columns[7].Header = "Break";
            grvRoster.Columns[8].Header = "Precint";
            grvRoster.Columns[9].Header = "Zone";
            grvRoster.Columns[10].Header = "Area";
            grvRoster.Columns[11].Header = "Labour Type";
            grvRoster.Columns[12].Header = "Employee";
            grvRoster.Columns[13].Header = "Payroll";
            grvRoster.Columns[14].Header = "Event Name";

        }

     
        #endregion

        #region Eventos de Notificacion Hijos

        /// <summary>
        /// Notifica que se agrego o actualizo un registro desde el hijo para actualizar la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void crearEstado_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //if ((((UC_CrudEstadoGestion)sender).TipoOperacion) == OperacionCRUD.Nuevo)
            //{
            //    _TipoPagina = TipoPaginaPaginado.PrimeraPagina;
            //    _CurrentPageIndex = 1;
            //    _AdminEstadoGestion_Presenter.ObtenerListaEstadoGestion();
            //}
            //if ((((UC_CrudEstadoGestion)sender).TipoOperacion) == OperacionCRUD.Modificar)
            //{
            //    _TipoPagina = TipoPaginaPaginado.PaginaActual;
            //    _AdminEstadoGestion_Presenter.ObtenerListaEstadoGestion();
            //    ViewWindow_Modal.Cerrar = ViewWindow_Modal.WinBehavior.Close;
            //    ViewWindow_Modal.CloseModal();

            //}
        }

        
        #endregion

        #region Botones Principales

        private async void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            MessageResponseViewModel<string> responseObj;
            TestObjectVM test = new TestObjectVM();
            //var usCulture = new System.Globalization.CultureInfo("en-AU");
           // test.Date = DateTime.Now.ToString("yyyy-MM-dd");
            test.Datestring = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime oDate = DateTime.ParseExact(test.Datestring, "yyyy-MM-dd", null);
            responseObj = await ServiceRoster.GetDate(test);
            // MessageBoxModal.Show(General.ResolveOwnerWindow, DateTime.Now.Date.ToString(), true);
            MessageBoxModal.Show(General.ResolveOwnerWindow(), DateTime.Now.Date.ToString(), "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async void btnExportar_Click(object sender, RoutedEventArgs e)
        {
            dateRange = new DateRange();
            dateRange.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(DateRange_PropertyChanged);
            MessageBoxResult Response = ViewWindow_Modal.Show(dateRange, "Export", dateRange.btnCancelar);
            if (Response == MessageBoxResult.Cancel)
            {
                return;
            }
            ViewWindow_Modal.CloseModal();

        }

        private async void DateRange_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            FilterParametersRoster filter = new FilterParametersRoster();
            filter.filter = "Export";
            filter.DateFrom = dateRange.dateInitial.ToString("yyyy-MM-dd");
            filter.DateTo = dateRange.dateEnd.ToString("yyyy-MM-dd");



            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".xlsx";
            dlg.Filter = "Excel Files (*.xlsx)|*.xlsx|Excel Files (*.xls)|*.xls";

            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
            }

            var res = await ServiceRoster.GetRoster(filter);

            ExcelUtlity obj = new ExcelUtlity();
            DataTable dt = General.ConvertToDataTableRoster(res.Data.lstRoster);
            obj.WriteDataTableToExcel(dt, "Roster", dlg.FileName, "Details");


            //obj.WriteDataTableToExcel(dt, "Person Details", "D:\\testPersonExceldata.xlsx", "Details");
            MessageBoxModal.Show(General.ResolveOwnerWindow(), "Data Exported: " + dlg.FileName, "Information", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void btnAyuda_Click(object sender, RoutedEventArgs e)
        {
          //  HelpProvider.SetTopicId(btnAyuda, HELPTOPICS_GESTIONCOBRO.EstadoGestionTopic);
        }

        #endregion

        #region Eventos Grilla

        private void EstableceAtributosGrilla()
        {

            for (int iRow = 0; iRow < grvRoster.Items.Count; iRow++)
            {
                for (int iCol = 0; iCol < grvRoster.Columns.Count; iCol++)
                {
                    DataGridCell oCell = grvRoster.GetCell(iRow, iCol);
                    if (oCell != null)
                    {

                        if (oCell.Column != null)
                        {
                            DataGridColumn oColumnaBase = oCell.Column;
                            string sortPropertyName = WPFDataGridHelper.GetSortMemberPath(oColumnaBase);
                            if (!string.IsNullOrEmpty(sortPropertyName))
                            {
                                if (sortPropertyName == "Estado")
                                {
                                    sortPropertyName = "Estado_EstadoGestion";
                                }

                                if (CampoOrden == sortPropertyName)
                                {
                                    if (OrdenCampo == "Asc")
                                    {
                                        oColumnaBase.SortDirection = ListSortDirection.Descending;
                                    }
                                    else
                                    {
                                        oColumnaBase.SortDirection = ListSortDirection.Ascending;
                                    }
                                    break;
                                }
                            }
                        }

                        Button oButton = WPFDataGridHelper.GetVisualChild<Button>(oCell);
                        if (oButton != null)
                        {
                            string sMensajeToolTip = string.Empty;
                            switch (oButton.Name)
                            {
                                case "btnConsultarGrilla":
                                    sMensajeToolTip = AdministradorMensaje.Instance.GetMensajePorCodigo(Incog.wpf.Messages.CodeMessagesGeneral.General_ToolTip_BotonConsultarGrilla);
                                    break;

                                case "btnEditarGrilla":
                                    sMensajeToolTip = AdministradorMensaje.Instance.GetMensajePorCodigo(Incog.wpf.Messages.CodeMessagesGeneral.General_ToolTip_BotonEditarGrilla);
                                    break;

                                case "btnEliminarGrilla":
                                    sMensajeToolTip = AdministradorMensaje.Instance.GetMensajePorCodigo(Incog.wpf.Messages.CodeMessagesGeneral.General_ToolTip_BotonEliminaGrilla);
                                    break;

                                default:
                                    break;
                            }

                            oButton.ToolTip = sMensajeToolTip;
                        }

                    }
                }
            }

            EstableceSeguridad();
        }

       

        private void grvRosterAdmin_Sorting(object sender, DataGridSortingEventArgs e)
        {
           

           
        }

        void oWPFFilterPopupManager_ColumnFilterAdding(object sender, ColumnFilterEventArgs e)
        {
            switch (e.FieldName)
            {
                case "DateShort":
                    e.ColumnFilter = new RangeDateColumnFilter();
                    break;             
                   
                case "Employee":
                    e.ColumnFilter = new TextBoxColumnFilter();
                    break;

                default:
                    break;
            }
        }

      async  void  oWPFFilterPopupManager_ColumnFilterExec(object sender, ColumnFilterEventArgs e)
        {
            FilterParametersRoster filter = new FilterParametersRoster();
            string sFilter = string.Empty;
            foreach (ColumnFilters CF in e.ColumnFilters)
            {
                if (CF == null) continue;
                if (CF.FilterExpression != string.Empty)
                {
                    if (CF.FieldName == "Employee")
                    {
                        filter.Employee = CF.FilterExpression;
                        filter.filter = "Employee";
                    }
                }

            }
            filter.DateGridFilter = dateFilter.ToString("yyyy-MM-dd");
            MessageResponseViewModel<RosterWM> responseObj = await ServiceRoster.GetRoster(filter);
            grvRoster.ItemsSource = responseObj.Data.lstRoster;
            
        }

        #endregion

        #region Botones Grilla

        private void Consultar_Click(object sender, RoutedEventArgs e)
        {
            //DtoEstadoGestion Seleccionado = grvEstadoGestion.SelectedItem as DtoEstadoGestion;
            //EstadoGestion oEstadoGestion = new EstadoGestion();
            //oEstadoGestion.Ide_EstadoGestion = Seleccionado.Ide_EstadoGestion;
            //UC_CrudEstadoGestion crearEstadoGestion = new UC_CrudEstadoGestion(OperacionCRUD.Consultar, oEstadoGestion);
            //string sModulo = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.EstadoGestion_TextoGrillaColumnaEstadoGestion);
            //string sTitulo = string.Format(AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.General_TituloConsultar), sModulo);
            //MessageBoxResult Response = ViewWindow_Modal.Show(crearEstadoGestion, sTitulo, crearEstadoGestion.btnCancelar);
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            //DtoEstadoGestion Seleccionado = grvEstadoGestion.SelectedItem as DtoEstadoGestion;
            //EstadoGestion oEstadoGestion = new EstadoGestion();
            //oEstadoGestion.Ide_EstadoGestion = Seleccionado.Ide_EstadoGestion;
            //UC_CrudEstadoGestion crearEstadoGestion = new UC_CrudEstadoGestion(OperacionCRUD.Modificar, oEstadoGestion);
            //crearEstadoGestion.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(crearEstado_PropertyChanged);
            //string sModulo = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.EstadoGestion_TextoGrillaColumnaEstadoGestion);
            //string sTitulo = string.Format(AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.General_TituloEditar), sModulo);
            //MessageBoxResult Response = ViewWindow_Modal.Show(crearEstadoGestion, sTitulo, crearEstadoGestion.btnCancelar);
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            //string sTituloModal = AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.TituloModalMensajes);
            //MessageBoxResult Response = MessageBoxModal.Show(General.ResolveOwnerWindow(), AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.General_AlertaEliminarRegistro), sTituloModal, MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel, true);

            //if (Response == MessageBoxResult.OK)
            //{
            //    DtoEstadoGestion oDtoElim = grvEstadoGestion.SelectedItem as DtoEstadoGestion;
            //    EstadoGestion oEstadoGestion = new EstadoGestion();
            //    oEstadoGestion.Ide_EstadoGestion = oDtoElim.Ide_EstadoGestion;
            //    _AdminEstadoGestion_Presenter.EliminarEstadoGestion();
            //}
        }

        #endregion

        #region IView

        //private ObservableCollection<DtoEstadoGestion> _ObserGrilla;
        //public ObservableCollection<DtoEstadoGestion> ObserGrilla
        //{
        //    get { return _ObserGrilla; }
        //    set { _ObserGrilla = value; }
        //}

        //private List<DtoEstadoGestion> _CargaGrilla;
        //public List<DtoEstadoGestion> CargaGrilla
        //{
        //    get
        //    {
        //        return _CargaGrilla;
        //    }
        //    set
        //    {
        //        _CargaGrilla = value;
        //        General.ToObservableCollection<DtoEstadoGestion>(_CargaGrilla, ObserGrilla);
        //        if (value.Count > 0)
        //        {
        //            _TotalRegistros = Convert.ToInt32(value[0].TotalRegistros);
        //            btnExportar.IsEnabled = true;

        //        }
        //        else
        //        {
        //            _TotalRegistros = 0;
        //            btnExportar.IsEnabled = false;
        //        }

        //        ActualizarTotalPaginas();

        //    }
        //}

        public string ActualizacionSatisfactoria
        {
            set
            {
                //string sTituloModal = AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.TituloModalMensajes);
                //MessageBoxModal.Show(General.ResolveOwnerWindow(), value, sTituloModal, MessageBoxButton.OK, MessageBoxImage.Information, true);
                //_TipoPagina = TipoPaginaPaginado.PrimeraPagina;
                //_CurrentPageIndex = 1;
                //_AdminEstadoGestion_Presenter.ObtenerListaEstadoGestion();
            }
        }

        public string MuestraMensaje
        {
            set
            {
                //string sTituloModal = AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.TituloModalMensajes);
                //MessageBoxModal.Show(General.ResolveOwnerWindow(), value, sTituloModal, MessageBoxButton.OK, MessageBoxImage.Error, true);

            }
        }


        public string MuestraMensajeAdvertencia
        {
            set
            {
                //string sTituloModal = AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.TituloModalMensajes);
                //MessageBoxModal.Show(General.ResolveOwnerWindow(), value, sTituloModal, MessageBoxButton.OK, MessageBoxImage.Warning, true);
            }
        }

        #endregion

        #region Seguridad

        public void EstableceSeguridad()
        {

            if (System.Windows.Application.Current.MainWindow != null)
            {
                //frmMain oMain = (frmMain)System.Windows.Application.Current.MainWindow;
                //if (oMain != null)
                //{
                //    oMain.EstableceSeguridad(this);
                //}
            }
        }

        #endregion

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            save = true;
        }

        private void DtpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtpDate.SelectedDate != null)
            {
                Filter = new FilterParametersRoster();
                Filter.filter = "All";
                Filter.DateGridFilter = dateFilter.Date.ToString("yyyy-MM-dd");
                FillGrid();
            }
        }
    }
}

