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
using IncogStuffControl.UserControls.Selectors;
using IncogStuffControl.UtilClass.ResourceEnum;
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

        private List<TimesheetsReportViewModel> timesheets;

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

        private FilterParametersTimesheet _Filter;
        public FilterParametersTimesheet Filter
        {
            get { return _Filter; }
            set
            {
                _Filter = value;

            }
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

        public int countDateChanged = 0;
        private DateRange _dateRange;
        public DateRange dateRange
        {
            get { return _dateRange; }
            set
            {
                _dateRange = value;

            }
        }

        public TimesheetUC(bool oControlExpandido = false)
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
            timesheets = await ServiceEmployee.GetTimesheetReport(Filter);
            grvTimesheet.ItemsSource = timesheets;
            lblReg.Content = timesheets.Count();
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
            TimesheetsReportViewModel Seleccionado = grvTimesheet.SelectedItem as TimesheetsReportViewModel;
            TimesheetCrudUC Timesheets = new TimesheetCrudUC(OperacionCRUD.Consultar, Seleccionado, timesheets);
            Timesheets.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Timesheet_PropertyChanged);
            string sTitulo = "View";
            MessageBoxResult Response = ViewWindow_Modal.Show(Timesheets, sTitulo, Timesheets.btnCancelar);
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            TimesheetsReportViewModel Seleccionado = grvTimesheet.SelectedItem as TimesheetsReportViewModel;
            TimesheetCrudUC Timesheets = new TimesheetCrudUC(OperacionCRUD.Modificar, Seleccionado, timesheets);
            Timesheets.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Timesheet_PropertyChanged);
            string sTitulo = "Edit";
            MessageBoxResult Response = ViewWindow_Modal.Show(Timesheets, sTitulo, Timesheets.btnCancelar);
        }

        private void Timesheet_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is TimesheetCrudUC)
            {
                FillGrid();
                ViewWindow_Modal.CloseModal();
            }
        }

        private async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            TimesheetsReportViewModel Seleccionado = grvTimesheet.SelectedItem as TimesheetsReportViewModel;
            if(Seleccionado.EndTime==null)
            {
                MessageBoxModal.Show(General.ResolveOwnerWindow(), "You can deleted this register once it sign off", "Information", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Cancel, true);
                return;
            }
            string sTituloModal = "Delete";
            MessageBoxResult Response = MessageBoxModal.Show(General.ResolveOwnerWindow(), "Are you sure you want to delete this register?", sTituloModal, MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel, true);

            if (Response == MessageBoxResult.OK)
            {
               

                MessageResponseViewModel<bool> result = await ServiceEmployee.DeleteTimesheets(Seleccionado).ConfigureAwait(true);
                if (result.Succesfull)
                {
                    MessageBoxModal.Show(General.ResolveOwnerWindow(), "Timesheet sucesfully deleted", "Information", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Cancel, true);
                    FillGrid();
                }
                else
                {
                    MessageBoxModal.Show(General.ResolveOwnerWindow(), result.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Cancel, true);
                }
            }
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

        public List<TimesheetsReportViewModel> Timesheets { get => timesheets; set => timesheets = value; }

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
            FilterParametersTimesheet filter = new FilterParametersTimesheet();
            string sFilter = string.Empty;
            foreach (ColumnFilters CF in e.ColumnFilters)
            {
                if (CF == null) continue;
                if (CF.FilterExpression != string.Empty)
                {
                   if(CF.FieldName == "Employee")
                    {
                        filter.Employee = CF.FilterExpression;
                        filter.filter = "Employee";
                    }
                }                   
                
            }

            filter.DateGridFilter = dateFilter.ToString("yyyy/MM/dd");
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
            dateRange = new DateRange();
            dateRange.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(DateRange_PropertyChanged);
            MessageBoxResult Response = ViewWindow_Modal.Show(dateRange, "Export", dateRange.btnCancelar);
            if(Response == MessageBoxResult.Cancel)
            {
                return;
            }
            ViewWindow_Modal.CloseModal();

        }

        private async void DateRange_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                FilterParametersTimesheet filter = new FilterParametersTimesheet();
                filter.filter = "Export";
                filter.DateFrom = dateRange.dateInitial.ToString("yyyy/MM/dd");
                filter.DateTo = dateRange.dateEnd.ToString("yyyy/MM/dd");



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
                DataTable dt = General.ConvertToDataTableTimesheets(await ServiceEmployee.GetTimesheetReport(filter));
                obj.WriteDataTableToExcel(dt, "Timesheet", dlg.FileName, "Details");


                //obj.WriteDataTableToExcel(dt, "Person Details", "D:\\testPersonExceldata.xlsx", "Details");
                MessageBoxModal.Show(General.ResolveOwnerWindow(), "Data Exported: " + dlg.FileName, "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {

                MessageBoxModal.Show(General.ResolveOwnerWindow(), ex.Message + ex.InnerException, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAyuda_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            TimesheetsReportViewModel Seleccionado = grvTimesheet.SelectedItem as TimesheetsReportViewModel;
            TimesheetCrudUC Timesheets = new TimesheetCrudUC(OperacionCRUD.Nuevo, Seleccionado, timesheets);
            Timesheets.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Timesheet_PropertyChanged);
            string sTitulo = "New";
            MessageBoxResult Response = ViewWindow_Modal.Show(Timesheets, sTitulo, Timesheets.btnCancelar);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            save = true;
        }

        private void DtpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtpDate.SelectedDate != null)
            {
                Filter = new FilterParametersTimesheet();
                Filter.filter = "All";
                Filter.DateGridFilter = dateFilter.ToString("yyyy/MM/dd");
                FillGrid();
                countDateChanged++;
            }
        }
    }
}
