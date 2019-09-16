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
using System.Collections;
using System.ComponentModel;
using Microsoft.Win32;
using System.IO;
using Incog.wpf.Messages;
using IncogStuffControl.UtilControls.ModalMessageBox;
using Incog.Utils;
using IncogStuffControl.UtilControls.ViewModal;

namespace IncogStuffControl.UtilControls.Exportacion
{
    /// <summary>
    /// Interaction logic for UC_Exportacion.xaml
    /// </summary>
    public partial class UC_Exportacion : UserControl
    {
        #region Definiciones

        protected IList _ListaDatos { get; private set; }
        protected string _SeparadorCampos { get; private set; }
        protected string _NombreArchivo { get; private set; }
        protected BackgroundWorker _BackgroundWorker = null;
        protected SaveFileDialog _Savedialog = null;
        private bool _Error_ArchivoUso = false;

        #endregion


        #region Eventos Control

        public UC_Exportacion()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Iniciar();
        }

        public void Inicializar(IList oListaDatos, string oSeparadorCampos)
        {
            _Savedialog = new SaveFileDialog();
            _Savedialog.CheckPathExists = false;
            _Savedialog.FileOk += new CancelEventHandler(_Savedialog_FileOk);

            _BackgroundWorker = new BackgroundWorker();
            _BackgroundWorker.WorkerReportsProgress = true;
            _BackgroundWorker.WorkerSupportsCancellation = true;
            _BackgroundWorker.DoWork += new DoWorkEventHandler(_BackgroundWorker_DoWork);
            _BackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(_BackgroundWorker_ProgressChanged);
            _BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BackgroundWorker_RunWorkerCompleted);

            _ListaDatos = oListaDatos;
            _SeparadorCampos = oSeparadorCampos;
            if (_ListaDatos != null)
            {
                if (_ListaDatos.Count <= 0)
                {
                    btnExportar.IsEnabled = false;
                }
            }

            string sProgreso = "Procesing...";
            lblProgreso.Content = sProgreso;

            string sTotalRegistros = _ListaDatos.Count.ToString();
           // string sTotalRegistros = string.Format("Total Registros: { 0}", _ListaDatos.Count);
            lblTotal.Content = sTotalRegistros;
        }

        void _Savedialog_FileOk(object sender, CancelEventArgs e)
        {
            if (File.Exists(_Savedialog.FileName) == true)
            {
                try
                {
                    File.Delete(_Savedialog.FileName);
                }
                catch (System.Exception ex)
                {
                    _Error_ArchivoUso = true;
                }

            }
        }


        #endregion

        #region BackgroundWorker

        private void ExportarListaArchivoPlano()
        {
            ExportarPlano oExportar = new ExportarPlano();
            oExportar.ExportarListaArchivoPlano(_ListaDatos, _NombreArchivo, _SeparadorCampos, _BackgroundWorker);
            oExportar = null;


        }

        void _BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string sTituloModal = "Export";

            if (e.Error != null)// Controla que no haya ocurrido algún error.
            {
                string sMensajeExportacionERR = "Error trying to export data";
                MessageBoxModal.Show(General.ResolveOwnerWindow(), sMensajeExportacionERR, sTituloModal, MessageBoxButton.OK, MessageBoxImage.Error);
                CerrarFormulario();
            }
            else if (e.Cancelled)
            {
                string sMensajeExportacionCANCEL = "Error trying to export data";
                MessageBoxModal.Show(General.ResolveOwnerWindow(), sMensajeExportacionCANCEL, sTituloModal, MessageBoxButton.OK, MessageBoxImage.Warning);
                CerrarFormulario();
            }
            else
            {
                //mainProgressBar.Text = "100%";
                mainProgressBar.Value = this.mainProgressBar.Maximum;
                btnExportar.IsEnabled = true;
                btnCancelar.IsEnabled = true;

                string sMensajeExportacionOK = "Data has been exported";
                MessageBoxModal.Show(General.ResolveOwnerWindow(), sMensajeExportacionOK, sTituloModal, MessageBoxButton.OK, MessageBoxImage.Information);

                CerrarFormulario();
            }
        }

        void _BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            double iProgreso = e.ProgressPercentage;
            double iTotal = mainProgressBar.Maximum;

            double dDivision = iProgreso / iTotal;
            double iPorcentaje = (dDivision * 100);

            //mainProgressBar.Text = Math.Round(iPorcentaje, 0).ToString() + "%";

            mainProgressBar.Value = e.ProgressPercentage;
        }

        void _BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ExportarListaArchivoPlano();
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("Archivo en uso"))
                {
                    //string sTituloModal = AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.TituloModalMensajes);
                    //string sMensajeExportacionOK = AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.General_ExportacionEnUso);
                    //MessageBoxModal.Show(General.ResolveOwnerWindow(), sMensajeExportacionOK, sTituloModal, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                e.Cancel = true;
            }

        }



        #endregion

        #region Botones Principales

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            ViewWindow_Modal.Cerrar = ViewWindow_Modal.WinBehavior.Close;
        }

        private void btnExportar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _Error_ArchivoUso = false;
                if (_ListaDatos != null)
                {
                    if (_ListaDatos.Count > 0)
                    {
                        mainProgressBar.Maximum = _ListaDatos.Count;
                        _Savedialog.Filter = "Excel 97- 2003 WorkBook (*.xls)| *.xls | Excel 2007 WorkBook (*.xlsx) | *.xlsx | All files (*.*)|*.*";
                        _Savedialog.CheckFileExists = false;
                        _Savedialog.CheckPathExists = false;
                        _Savedialog.FilterIndex = 2;
                        _Savedialog.RestoreDirectory = true;

                        Nullable<bool> result = _Savedialog.ShowDialog();

                        if (_Error_ArchivoUso == true)
                        {
                            string sTituloModal = "Export";
                            string sMensajeExportacionOK = "File is being used";
                            MessageBoxModal.Show(General.ResolveOwnerWindow(), sMensajeExportacionOK, sTituloModal, MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        if (result == true)
                        {
                            if (!_Savedialog.FileName.Equals(String.Empty))
                            {

                                FileInfo f = new FileInfo(_Savedialog.FileName);
                                if (f.Extension.Equals(".xls") || f.Extension.Equals(".xlsx"))
                                {

                                    _NombreArchivo = _Savedialog.FileName;
                                    IniciarExportacion();
                                }
                                else
                                {
                                    //Pendiente mensaje adecuado

                                }
                            }
                            else
                            {
                                //Pendiente mensaje adecuado
                            }
                        }

                    }
                    else
                    {

                    }
                }
            }
            catch (System.Exception)
            {
                string sTituloModal = "Export";
                string sMensajeExportacionOK = "File is being used";
                MessageBoxModal.Show(General.ResolveOwnerWindow(), sMensajeExportacionOK, sTituloModal, MessageBoxButton.OK, MessageBoxImage.Error);

                CerrarFormulario();
            }
        }

        #endregion

        #region General

        private void Iniciar()
        {
            CargaRecursos();
            btnExportar.Focus();
        }

        private void CerrarFormulario()
        {
            ViewWindow_Modal.CloseModal();
        }

        private void CargaRecursos()
        {
            btnExportar.Content = "Export";
            btnCancelar.Content = "Cancel";
            btnExportar.ToolTip = "Export";
            btnCancelar.ToolTip = "Cancel";

        }

        private void IniciarExportacion()
        {
            btnExportar.IsEnabled = false;
            btnCancelar.IsEnabled = false;

            _BackgroundWorker.RunWorkerAsync();


        }

        #endregion



    }
}
