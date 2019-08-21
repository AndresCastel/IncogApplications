using Incog.wpf.Messages;
using IncogStuffControl.UtilControls.ModalMessageBox;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace IncogStuffControl
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        bool _InstanciasAplicacionActivas = false;
        IncogStuffControl.Splash.SplashScreen splashScreenLogin;

        protected override void OnStartup(StartupEventArgs e)
        {

           
            ////Carga estilos  adicionales-----------------------------------------------------------
            ResourceDictionary dictMessageBox = new ResourceDictionary();
            Uri uri = new Uri("Themes/MessageBoxModal.xaml", UriKind.RelativeOrAbsolute);
            dictMessageBox.Source = uri;
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(dictMessageBox);
            ////-------------------------------------------------------------------------------------

            // //Despliega el formulario Splash-------------------------------------------------------
            splashScreenLogin = new IncogStuffControl.Splash.SplashScreen();
            this.MainWindow = splashScreenLogin;
            splashScreenLogin.Show();
            // //-------------------------------------------------------------------------------------


            //Obtiene informacion de la aplicacion
            IncogStuffControl.MainApp.About.AboutAssemblyDataProvider oPv = new IncogStuffControl.MainApp.About.AboutAssemblyDataProvider();
            string sProducto = oPv.Product;
            string sCopyright = oPv.Copyright;
            string sCompany = oPv.Company;
            string sVersion = oPv.Version;
            string sLink = oPv.LinkUri;


            splashScreenLogin.AvailablePlugins = new[] { sProducto, sCopyright, sCompany, sVersion, sLink };



            var startupTask = Task.Factory.StartNew(() =>
            {

                //Verifica instancia de la aplicación--------------------------------------------------
                _InstanciasAplicacionActivas = VerificaInstanciaAplicacion();
                if (_InstanciasAplicacionActivas == false)
                {
                    //string sTituloModal = AdministradorMensaje.Instance.GetMensajePorCodigo(CodeMessagesGeneral.TituloModalMensajes);
                    //string sMensaje_InstanciaEnEjecucion = AdministradorMensaje.Instance.GetMensajePorCodigo(CodeMessagesGeneral.Aplicacion_InstanciaEnEjecucion);
                    //MessageBoxModal.Show(sMensaje_InstanciaEnEjecucion, sTituloModal, MessageBoxButton.OK, MessageBoxImage.Error);
                    //this.Shutdown();


                }

            });

            startupTask.ContinueWith(t =>
            {

                if (_InstanciasAplicacionActivas == true)
                {
                    string sTituloModal = AdministradorMensaje.Instance.GetMensajePorCodigo(CodeMessagesGeneral.TituloModalMensajes);
                    string sMensaje_InstanciaEnEjecucion = AdministradorMensaje.Instance.GetMensajePorCodigo(CodeMessagesGeneral.Aplicacion_InstanciaEnEjecucion);
                    MessageBoxModal.Show(sMensaje_InstanciaEnEjecucion, sTituloModal, MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Shutdown();
                }
                else
                {
                    //Launch main screen
                    LaunchMainScreen();
                }

            }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());


        }

        private void LaunchMainScreen()
        {

            MainWindow _frmMain =  new MainWindow();
            _frmMain.Loaded += (sender, args) => { splashScreenLogin.Visibility = Visibility.Hidden; };
            this.MainWindow = _frmMain;
            _frmMain.Activate();
            //  _frmLogin.Focus();
            _frmMain.ShowDialog();

         


        }

        private bool VerificaInstanciaAplicacion()
        {
            string sMensaje = AdministradorMensaje.Instance.GetMensajePorCodigo(CodeMessagesGeneral.Aplicacion_VerificandoInstanciaAplicacion);

            splashScreenLogin.Dispatcher.BeginInvoke(
                      (Action)(() => splashScreenLogin.Message = sMensaje));


            bool bInstanciaEnEjecucion = false;

            Type type = this.GetType();
            Assembly assembly = type.Assembly;

            if (System.Diagnostics.Process.GetProcessesByName(type.Namespace).Length > 1)
            {
                return true;
            }

            Thread.Sleep(5000);

            return bInstanciaEnEjecucion;
        }
    }
}
