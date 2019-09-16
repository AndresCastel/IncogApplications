using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using Axede.WPF.FilterPopup;
using Incog.Utils;
using Incog.Utils.Enums;
using Incog.wpf.Messages;
using IncogStuffControl.Services;
using IncogStuffControl.Services.ViewModel;
using IncogStuffControl.UtilControls.Exportacion;
using IncogStuffControl.UtilControls.Grid;
using IncogStuffControl.UtilControls.ModalMessageBox;
using IncogStuffControl.UtilControls.ViewModal;

namespace IncogStuffControl.UserControls.Timesheet
{
    /// <summary>
    /// Interaction logic for TimesheetUC.xaml
    /// </summary>
    public partial class TimesheetUC : UserControl, INotifyPropertyChanged
    {

        #region Definiciones

       
        #endregion

        public TimesheetUC()
        {
            InitializeComponent();
            //
            // _AdminEstadoGestion_Presenter = new AdminEstadoGestion_Presenter(this);
            //Paginador.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Paginador_PropertyChanged);
            parentstack.Width = double.NaN;
            parentstack.Height = double.NaN;
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

        public TimesheetUC(bool oControlExpandido = false)
        {
            InitializeComponent();
            FillGrid();
           // Paginador.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Paginador_PropertyChanged);
            if (oControlExpandido == true)
            {
                parentstack.Width = double.NaN;
                parentstack.Height = double.NaN;
            }

        }

        private async void FillGrid()
        {
            List<TimesheetsReportViewModel> timesheets = await ServiceEmployee.GetTimesheetReport(new FilterParametersRoster() { filter = "All" });
            grvTimesheet.ItemsSource = timesheets;
        }

        public void Inicio()
        {
            //if (ObserGrilla == null)
            //{
            CargaRecursos();
            //}

        }

        private void CargaRecursos()
        {
            //btnNuevo.Content = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.General_TextoBotonNuevo);
            //btnExportar.Content = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.General_TextoBotonExportar);
            //btnAyuda.Content = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.General_TextoBotonAyuda);

            //btnNuevo.ToolTip = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.EstadoGestion_ToolTipBotonNuevo);
            //btnExportar.ToolTip = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.EstadoGestion_ToolTipBotonExportar);
            //btnAyuda.ToolTip = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.General_ToolTipBotonAyuda);
            grvTimesheet.Columns[3].Header = "Day";
            grvTimesheet.Columns[4].Header = "StartTime";
            grvTimesheet.Columns[5].Header = "EndTime";
            grvTimesheet.Columns[6].Header = "Break";
            grvTimesheet.Columns[7].Header = "Hours";
            grvTimesheet.Columns[8].Header = "Labour Type";
            grvTimesheet.Columns[9].Header = "Employee";
            grvTimesheet.Columns[10].Header = "Payroll";
            //grvTimesheet.Columns[11].Header = "Precint";
            grvTimesheet.Columns[11].Header = "Zone";
            //grvTimesheet.Columns[13].Header = "Area";






        }

        private void EstableceAtributosGrilla()
        {

            for (int iRow = 0; iRow < grvTimesheet.Items.Count; iRow++)
            {
                for (int iCol = 0; iCol < grvTimesheet.Columns.Count; iCol++)
                {
                    DataGridCell oCell = grvTimesheet.GetCell(iRow, iCol);
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

           // EstableceSeguridad();
        }

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

        private void grvTimesheet_Loaded(object sender, RoutedEventArgs e)
        {
            if (oWPFFilterPopupManager == null)
            {
                oWPFFilterPopupManager = new WPFFilterPopupManager();
                oWPFFilterPopupManager.ColumnFilterAdding += new ColumnFilterEventHandler(oWPFFilterPopupManager_ColumnFilterAdding);
                oWPFFilterPopupManager.ColumnFilterExec += new ColumnFilterEventHandler(oWPFFilterPopupManager_ColumnFilterExec);
                oWPFFilterPopupManager.DataGrid = grvTimesheet;
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

        void oWPFFilterPopupManager_ColumnFilterAdding(object sender, ColumnFilterEventArgs e)
        {
            switch (e.FieldName)
            {
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

        async void oWPFFilterPopupManager_ColumnFilterExec(object sender, ColumnFilterEventArgs e)
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
                    else if(CF.FieldName == "Employee")
                    {
                        filter.Employee = CF.FilterExpression;
                        filter.filter = "Employee";
                    }
                }                   
                
            }
        
           // Filtro = sFilter;
            //_TipoPagina = TipoPaginaPaginado.PrimeraPagina;
            //_CurrentPageIndex = 1;
            List<TimesheetsReportViewModel> lst = await ServiceEmployee.GetTimesheetReport(filter);
            grvTimesheet.ItemsSource = lst;

        }


        private void grvTimesheet_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }

        private void Uc_Timesheet_Loaded(object sender, RoutedEventArgs e)
        {
            Inicio();
        }

        private async void btnExportar_Click(object sender, RoutedEventArgs e)
        {
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



            ExcelUtlity obj = new ExcelUtlity();
             DataTable dt = General.ConvertToDataTableTimesheets(await ServiceEmployee.GetTimesheetReport(new FilterParametersRoster()));
            obj.WriteDataTableToExcel(dt, "Timesheet", dlg.FileName, "Details");


            //obj.WriteDataTableToExcel(dt, "Person Details", "D:\\testPersonExceldata.xlsx", "Details");
            MessageBoxModal.Show(General.ResolveOwnerWindow(), "Data Exported: " + dlg.FileName, "Information", MessageBoxButton.OK, MessageBoxImage.Information);









            //List<TimesheetsReportViewModel> lstExportCasos = await ServiceEmployee.GetTimesheetReport(new FilterParametersRoster());
            //UC_Exportacion oUC_Exportacion = new UC_Exportacion();
            //oUC_Exportacion.Inicializar(lstExportCasos, ";");
            //List<TimesheetsReportViewModel> tim = new List<TimesheetsReportViewModel>();

            //DataTable dt = General.ConvertToDataTable(tim);

            //string sTituloExportar = "Export";

            // MessageBoxResult Response = ViewWindow_Modal.Show(oUC_Exportacion, sTituloExportar, oUC_Exportacion.btnCancelar);
        }

        private void btnAyuda_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            save = true;
        }
    }
}
