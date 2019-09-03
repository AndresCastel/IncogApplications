using Axede.WPF.FilterPopup;
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

namespace IncogStuffControl.UserControls.Roster
{
    /// <summary>
    /// Interaction logic for RosterAdminUC.xaml
    /// </summary>
    public partial class RosterAdminUC : UserControl
    {

       

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


        //Paginado
        private int _CurrentPageIndex = 1;
        private int _TotalPage = 0;
        private int _TotalRegistros = 0;
        private TipoPaginaPaginado _TipoPagina = TipoPaginaPaginado.PrimeraPagina;
        private string _CampoOrden = "Nom_Tercero, Nom_EstadoGestion";
        private string _OrdenCampo = "Asc";
        private string _Filtro = string.Empty;

        public string Filtro
        {
            get { return _Filtro; }
            set { _Filtro = value; }
        }
        public int CurrentPageIndex
        {
            get { return _CurrentPageIndex; }
            set { _CurrentPageIndex = value; }
        }
        public int TotalPage
        {
            get { return _TotalPage; }
            set { _TotalPage = value; }
        }
        public TipoPaginaPaginado TipoPagina
        {
            get { return _TipoPagina; }
            set { _TipoPagina = value; }
        }
        public string CampoOrden
        {
            get { return _CampoOrden; }
            set { _CampoOrden = value; }
        }
        public string OrdenCampo
        {
            get { return _OrdenCampo; }
            set { _OrdenCampo = value; }
        }
        private WPFFilterPopupManager oWPFFilterPopupManager = null;

      

        #endregion

        #region Eventos Formulario

        public RosterAdminUC()
        {
            InitializeComponent();
            //
           // _AdminEstadoGestion_Presenter = new AdminEstadoGestion_Presenter(this);
            Paginador.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Paginador_PropertyChanged);
            parentstack.Width = double.NaN;
            parentstack.Height = double.NaN;


        }
        public RosterAdminUC(bool oControlExpandido = false)
        {
            InitializeComponent();
            FillGrid();
            Paginador.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Paginador_PropertyChanged);
            if (oControlExpandido == true)
            {
                parentstack.Width = double.NaN;
                parentstack.Height = double.NaN;
            }

        }

        private async void FillGrid()
        {
            MessageResponseViewModel<RosterWM> responseObj = await ServiceRoster.GetRoster( new FilterParametersRoster() { filter = "All" });
            grvRoster.ItemsSource = responseObj.Data.lstRoster;
        }

        #endregion

        #region Loading Grid

      

       
        #endregion

        #region General

        public void Inicio()
        {
            //if (ObserGrilla == null)
            //{
            CargaRecursos();
            //}

        }

        private void ObtenerListaEstadoGestion()
        {
            //Task tsk = Task.Factory.StartNew(() =>
            //{
            //    EstadoProcesoCargueGrilla = EstadoProceso.Ocupado;

            //    Thread.Sleep(100);

            //    this.Dispatcher.BeginInvoke(
            //  (Action)(() => _AdminEstadoGestion_Presenter.ObtenerListaEstadoGestion()));

            //});
            //tsk.ContinueWith(t =>
            //{
            //    EstableceSeguridad();

            //    EstadoProcesoCargueGrilla = EstadoProceso.Disponible;

            //}, TaskScheduler.FromCurrentSynchronizationContext());

        }

        private void CargaRecursos()
        {
            //btnNuevo.Content = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.General_TextoBotonNuevo);
            //btnExportar.Content = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.General_TextoBotonExportar);
            //btnAyuda.Content = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.General_TextoBotonAyuda);

            //btnNuevo.ToolTip = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.EstadoGestion_ToolTipBotonNuevo);
            //btnExportar.ToolTip = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.EstadoGestion_ToolTipBotonExportar);
            //btnAyuda.ToolTip = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.General_ToolTipBotonAyuda);

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

        private void ActualizarTotalPaginas()
        {

            if (_TotalRegistros % Globals.iRegistrosPagina > 0)
            {
                _TotalPage = (int)(_TotalRegistros / Globals.iRegistrosPagina) + 1;
            }
            else
            {
                _TotalPage = (int)(_TotalRegistros / Globals.iRegistrosPagina);
            }
            if (_CurrentPageIndex > _TotalPage)
            {
                _CurrentPageIndex = _CurrentPageIndex - 1;
            }
            else if (_CurrentPageIndex == 0 && _TotalPage > 0)
            {
                _CurrentPageIndex = 1;
            }
            Paginador.LblPaginas.Content = AdministradorMensaje.Instance.GetMensajePorCodigo(Incog.wpf.Messages.CodeMessagesGeneral.General_TextoPagina) + _CurrentPageIndex.ToString() + "/" + _TotalPage.ToString() + " - " + AdministradorMensaje.Instance.GetMensajePorCodigo(Incog.wpf.Messages.CodeMessagesGeneral.General_TextoRegistro) + _TotalRegistros.ToString();
            EstableceAtributosGrilla();
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

        /// <summary>
        /// Notifica la accion del Control Paginador
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Paginador_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            switch (((PageGridGestionUC)sender).NotificarAccionPag)
            {
                case TipoPaginaPaginado.PaginaAnterior:
                    if (_CurrentPageIndex > 1)
                    {
                        _TipoPagina = TipoPaginaPaginado.PaginaAnterior;
                       // _AdminEstadoGestion_Presenter.ObtenerListaEstadoGestion();
                        _CurrentPageIndex--;
                    }
                    else
                    {
                        _CurrentPageIndex = 2;
                        _TipoPagina = TipoPaginaPaginado.PrimeraPagina;
                        //_AdminEstadoGestion_Presenter.ObtenerListaEstadoGestion();
                        _CurrentPageIndex = 1;
                    }

                    Paginador.LblPaginas.Content = AdministradorMensaje.Instance.GetMensajePorCodigo(Incog.wpf.Messages.CodeMessagesGeneral.General_TextoPagina) + _CurrentPageIndex.ToString() + "/" + _TotalPage.ToString() + " - " + AdministradorMensaje.Instance.GetMensajePorCodigo(Incog.wpf.Messages.CodeMessagesGeneral.General_TextoRegistro) + _TotalRegistros.ToString();
                    break;

                case TipoPaginaPaginado.PaginaSiguiente:
                    if (_CurrentPageIndex < _TotalPage)
                    {
                        if (_CurrentPageIndex > 1)
                        {
                            _TipoPagina = TipoPaginaPaginado.PaginaSiguiente;
                           // _AdminEstadoGestion_Presenter.ObtenerListaEstadoGestion();
                            _CurrentPageIndex++;
                        }
                        else
                        {
                            _TipoPagina = TipoPaginaPaginado.PaginaSiguiente;
                           // _AdminEstadoGestion_Presenter.ObtenerListaEstadoGestion();
                            _CurrentPageIndex = 2;
                        }
                    }
                    else
                    {
                        _TipoPagina = TipoPaginaPaginado.UltimaPagina;
                        _CurrentPageIndex = _TotalPage;
                    }
                    Paginador.LblPaginas.Content = AdministradorMensaje.Instance.GetMensajePorCodigo(Incog.wpf.Messages.CodeMessagesGeneral.General_TextoPagina) + _CurrentPageIndex.ToString() + "/" + _TotalPage.ToString() + " - " + AdministradorMensaje.Instance.GetMensajePorCodigo(Incog.wpf.Messages.CodeMessagesGeneral.General_TextoRegistro) + _TotalRegistros.ToString();
                    break;

                case TipoPaginaPaginado.PrimeraPagina:
                    _CurrentPageIndex = 1;
                    _TipoPagina = TipoPaginaPaginado.PrimeraPagina;
                   // _AdminEstadoGestion_Presenter.ObtenerListaEstadoGestion();
                    Paginador.LblPaginas.Content = AdministradorMensaje.Instance.GetMensajePorCodigo(Incog.wpf.Messages.CodeMessagesGeneral.General_TextoPagina) + _CurrentPageIndex.ToString() + "/" + _TotalPage.ToString() + " - " + AdministradorMensaje.Instance.GetMensajePorCodigo(Incog.wpf.Messages.CodeMessagesGeneral.General_TextoRegistro) + _TotalRegistros.ToString();
                    break;

                case TipoPaginaPaginado.UltimaPagina:
                    _CurrentPageIndex = _TotalPage - 1;
                    _TipoPagina = TipoPaginaPaginado.UltimaPagina;
                    //_AdminEstadoGestion_Presenter.ObtenerListaEstadoGestion();
                    _CurrentPageIndex = _TotalPage;
                    Paginador.LblPaginas.Content = AdministradorMensaje.Instance.GetMensajePorCodigo(Incog.wpf.Messages.CodeMessagesGeneral.General_TextoPagina) + _CurrentPageIndex.ToString() + "/" + _TotalPage.ToString() + " - " + AdministradorMensaje.Instance.GetMensajePorCodigo(Incog.wpf.Messages.CodeMessagesGeneral.General_TextoRegistro) + _TotalRegistros.ToString();
                    break;

                default:
                    break;
            }
            ActualizarTotalPaginas();
        }

        #endregion

        #region Botones Principales

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            //EstadoGestion oEstadoGestion = new EstadoGestion();
            //UC_CrudEstadoGestion crearEstado = new UC_CrudEstadoGestion(OperacionCRUD.Nuevo, oEstadoGestion);
            //crearEstado.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(crearEstado_PropertyChanged);
            //string sModulo = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.EstadoGestion_TextoGrillaColumnaEstadoGestion);
            //string sTitulo = string.Format(AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.General_TituloCrear), sModulo);
            //MessageBoxResult Response = ViewWindow_Modal.Show(crearEstado, sTitulo, crearEstado.btnCancelar);
        }

        private void btnExportar_Click(object sender, RoutedEventArgs e)
        {
            //List<DtoExportEstadoGestion> lstExportEstadoGestion = _AdminEstadoGestion_Presenter.ObtenerListaEstadoGestionExportacion();
            //UC_Exportacion oUC_Exportacion = new UC_Exportacion();
            //oUC_Exportacion.Inicializar(lstExportEstadoGestion, ";");
            //string sTituloExportar = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.EstadoGestion_TextoTituloExportar);

            //MessageBoxResult Response = ViewWindow_Modal.Show(oUC_Exportacion, sTituloExportar, oUC_Exportacion.btnCancelar);
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
            DataGrid dataGrid = sender as DataGrid;
            string sortPropertyName = WPFDataGridHelper.GetSortMemberPath(e.Column);
            if (!string.IsNullOrEmpty(sortPropertyName))
            {
                //Cancela el ordenamiento por defecto
                e.Handled = true;

                //establece el nuevo ordenamiento
                _CampoOrden = sortPropertyName;
                if (_CampoOrden == "Estado")
                {
                    _CampoOrden = "Estado_EstadoGestion";
                }

                if (e.Column.SortDirection.HasValue && e.Column.SortDirection.Value == ListSortDirection.Descending)
                {
                    _OrdenCampo = "Desc";
                }
                else if (e.Column.SortDirection.HasValue && e.Column.SortDirection.Value == ListSortDirection.Ascending)
                {
                    _OrdenCampo = "Asc";
                }

                _TipoPagina = TipoPaginaPaginado.PaginaActual;
                //_AdminEstadoGestion_Presenter.ObtenerListaEstadoGestion();

                if (_OrdenCampo == "Asc")
                {
                    e.Column.SortDirection = ListSortDirection.Descending;
                }
                else if (_OrdenCampo == "Desc")
                {
                    e.Column.SortDirection = ListSortDirection.Ascending;
                }

            }
        }

        void oWPFFilterPopupManager_ColumnFilterAdding(object sender, ColumnFilterEventArgs e)
        {
            switch (e.FieldName)
            {
                case "Day":
                    //if (ListaTipoCliente != null)
                    //{
                    //    if (ListaTipoCliente.Count > 0)
                    //    {
                    //        List<FilterPopupList> oListaTipoCliente = new List<FilterPopupList>();
                    //        foreach (DtoTipoCliente oDtoTipCli in ListaTipoCliente)
                    //        {
                    //            oListaTipoCliente.Add(new FilterPopupList() { IdValor = oDtoTipCli.Ide_TipoCliente, Valor = oDtoTipCli.Nom_TipoCliente });
                    //        }

                    //        e.ColumnFilter = new ComboBoxColumnFilter(oListaTipoCliente);
                    //    }
                    //}
                    break;
                case "DateShort":
                    e.ColumnFilter = new RangeDateColumnFilter();
                    break;             
                   
                case "Employee":
                    e.ColumnFilter = new TextBoxColumnFilter();
                    break;
                case "Payroll":
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
                    if (CF.FieldName == "Payroll")
                    {
                        filter.Payroll = CF.FilterExpression;
                        filter.filter = "Payroll";
                    }
                    else if (CF.FieldName == "DateShort")
                    {
                        DateTime dFInicio = Convert.ToDateTime(CF.FilterExpression.Split('|')[0]);
                        DateTime dFFin = Convert.ToDateTime(CF.FilterExpression.Split('|')[1]);
                        filter.DateStart = dFInicio;
                        filter.DateEnd = dFFin;
                        filter.filter = "Date";
                    }
                    if (CF.FieldName == "Employee")
                    {
                        filter.Employee = CF.FilterExpression;
                    }
                }
            }

            Filtro = sFilter;
            _TipoPagina = TipoPaginaPaginado.PrimeraPagina;
            _CurrentPageIndex = 1;
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
    }
}

