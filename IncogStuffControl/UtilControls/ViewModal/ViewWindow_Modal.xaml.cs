using Axede.WPF.CustomWindow;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace IncogStuffControl.UtilControls.ViewModal
{
    /// <summary>
    /// Interaction logic for ViewWindow_Modal.xaml
    /// </summary>
    public partial class ViewWindow_Modal : StandardWindow
    {
        public ViewWindow_Modal()
        {
            InitializeComponent();
            Cerrar = WinBehavior.Close;
            System.Windows.Application.Current.Activated += new EventHandler(Current_Activated);
            this.KeyDown += new KeyEventHandler(wnd_KeyDown);
        }

        void wnd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.System && e.SystemKey == Key.F4)
            {
                e.Handled = true;
            }
        }

        #region PROPIEDADES

        public static double MaxVertical = 0.8;

        public static double MaxHorizontal = 0.8;

        public static Window ParentWindow = null;

        public static Window GrantParentWindow = null;

        private static bool _IsParent = false;

        private static string _caption;

        public static bool IsParent
        {
            get { return ViewWindow_Modal._IsParent; }
            set { ViewWindow_Modal._IsParent = value; }
        }

        public enum MyMessageBoxButton
        {
            // Summary:
            //     The message box displays an OK button.
            OK,
            // 
            // Summary:
            //     The message box displays OK and Cancel buttons.
            OKCancel,
            //
            // Summary:
            //     The message box displays Yes, No, and Cancel buttons.
            YesNoCancel,
            //
            // Summary:
            //     The message box displays Yes and No buttons.
            YesNo,
            //
            // Summary:
            //     Displays a message box without Response buttons or Close button
            None,
            //
            // Summary:
            //     Displays a message box with the X Button only
            NoResponse,
            //
            // Summary:
            //     Displays a message box with a Close Button 
            Close,
        }

        public enum WinBehavior
        {
            /// <summary>
            /// Cierra el modal
            /// </summary>
            Close,
            /// <summary>
            /// No cierra el modal dependiendo de la respuesta 
            /// </summary>
            DontClose,

            /// <summary>
            /// No cierra el modal OK
            /// </summary>
            OK,
        }

        private static MessageBoxResult responseButton;

        public static MessageBoxResult ResponseButton
        {
            get { return ViewWindow_Modal.responseButton; }
            set { ViewWindow_Modal.responseButton = value; }
        }


        private MessageBoxResult response;
        public MessageBoxResult Response
        {
            get { return response; }
            set { response = value; }
        }

        private MyMessageBoxButton buttons;
        private MyMessageBoxButton Buttons
        {
            get { return buttons; }
            set
            {
                switch (value)
                {
                    //case MyMessageBoxButton.OK:
                    //    this.Button_OK.Visibility = Visibility.Visible;
                    //    break;
                    //case MyMessageBoxButton.OKCancel:
                    //    this.Button_OK.Visibility = Visibility.Visible;
                    //    this.Button_CANCEL.Visibility = Visibility.Visible;
                    //    this.Button_OK.HorizontalAlignment = HorizontalAlignment.Center;
                    //    this.Button_CANCEL.HorizontalAlignment = HorizontalAlignment.Right;
                    //    break;
                    //case MyMessageBoxButton.YesNo:
                    //    this.Button_YES.Visibility = Visibility.Visible;
                    //    this.Button_NO.Visibility = Visibility.Visible;
                    //    this.Button_YES.HorizontalAlignment = HorizontalAlignment.Center;
                    //    this.Button_NO.HorizontalAlignment = HorizontalAlignment.Right;
                    //    break;
                    //case MyMessageBoxButton.YesNoCancel:
                    //    this.Button_YES.Visibility = Visibility.Visible;
                    //    this.Button_NO.Visibility = Visibility.Visible;
                    //    this.Button_CANCEL.Visibility = Visibility.Visible;
                    //    this.Button_YES.HorizontalAlignment = HorizontalAlignment.Left;
                    //    this.Button_NO.HorizontalAlignment = HorizontalAlignment.Center;
                    //    this.Button_CANCEL.HorizontalAlignment = HorizontalAlignment.Right;
                    //    break;                
                    default:
                        break;
                }
                this.buttons = value;
            }
        }


        #endregion

        #region PROCEDIMIENTOS DE EVENTO

        private void Grid_MouseLeftButtonDown_drag(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CANCEL_Click(object sender, RoutedEventArgs e)
        {
            this.response = MessageBoxResult.Cancel;
            Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.response = MessageBoxResult.OK;
            Close();
        }

        private void YES_Click(object sender, RoutedEventArgs e)
        {
            this.response = MessageBoxResult.Yes;
            Close();
        }

        private void NO_Click(object sender, RoutedEventArgs e)
        {
            this.response = MessageBoxResult.No;
            Close();
        }

        private void win_Deactivated(object sender, EventArgs e)
        {
            //this.Activate();
        }

        private void win_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //double posx = ((System.Windows.SystemParameters.PrimaryScreenWidth - e.NewSize.Width) / 2);
            //((System.Windows.Window)sender).Left = posx;

            //double posy = ((System.Windows.SystemParameters.PrimaryScreenHeight - e.NewSize.Height) / 2);
            //((System.Windows.Window)sender).Top = posy;
        }

        #endregion

        #region PROCEDIMIENTOS PRIVADOS



        static void click_cerrar1(object sender, RoutedEventArgs e)
        {
            if (ParentWindow != null)
            {
                if (Cerrar != WinBehavior.DontClose)
                    ParentWindow.Close();
            }
            else
            {
                if (Cerrar != WinBehavior.DontClose)
                    GrantParentWindow.Close();
            }
        }

        private static WinBehavior _Cerrar;

        public static WinBehavior Cerrar
        {
            get { return _Cerrar; }
            set { _Cerrar = value; }
        }

        public static void CloseModal()
        {
            click_cerrar1(null, null);

        }
        public static MessageBoxResult Show(object userControl, string caption, Button CloseButton)
        {
            return Show(userControl, caption, MyMessageBoxButton.OK, CloseButton);
        }

        public static MessageBoxResult Show(object userControl, string caption, MyMessageBoxButton myMessageBoxButton)
        {
            return Show(userControl, caption, myMessageBoxButton, ScrollBarVisibility.Hidden, ScrollBarVisibility.Hidden);
        }

        public static MessageBoxResult Show(object userControl, string caption, MyMessageBoxButton myMessageBoxButton, Button CloseButton)
        {
            return Show(userControl, caption, myMessageBoxButton, ScrollBarVisibility.Hidden, ScrollBarVisibility.Hidden, CloseButton);
        }

        public static MessageBoxResult Show(object userControl, string caption, MyMessageBoxButton myMessageBoxButton, ScrollBarVisibility ucSBH, ScrollBarVisibility ucSBV)
        {
            _caption = caption;
            MessageBoxResult Response = MessageBoxResult.Cancel;
            bool s = System.Windows.Application.Current.Dispatcher.CheckAccess();
            if (System.Windows.Application.Current.Dispatcher.CheckAccess())
            {
                Response = ShowShadow(System.Windows.Application.Current.MainWindow, userControl, caption, myMessageBoxButton, ucSBH, ucSBV);
            }
            else
            {
                System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        Response = ShowShadow(System.Windows.Application.Current.MainWindow, null, caption, myMessageBoxButton, ucSBH, ucSBV);
                    }));
            }
            return Response;
        }

        public static MessageBoxResult Show(object userControl, string caption, MyMessageBoxButton myMessageBoxButton, ScrollBarVisibility scrollBarVisibility, ScrollBarVisibility scrollBarVisibility_5, Button CloseButton, Window owner = null)
        {
            _caption = caption;
            MessageBoxResult Response = MessageBoxResult.Cancel;
            if (System.Windows.Application.Current.Dispatcher.CheckAccess())
            {
                Response = ShowShadow(owner, userControl, caption, myMessageBoxButton, scrollBarVisibility, scrollBarVisibility_5, CloseButton);
            }
            else
            {
                System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        Response = ShowShadow(owner, userControl, caption, myMessageBoxButton, scrollBarVisibility, scrollBarVisibility_5, CloseButton);
                    }));
            }
            return Response;
        }

        public static MessageBoxResult ShowShadow(Window owner, object userControl, string caption, MyMessageBoxButton myMessageBoxButton, ScrollBarVisibility ucSBH, ScrollBarVisibility ucSBV)
        {
            ViewWindow_Modal win = new ViewWindow_Modal();
            TypeConverterStringToUIElement ocov = new TypeConverterStringToUIElement();
            win.Caption = (UIElement)ocov.ConvertFromString(_caption);
            win.Title = caption;
            //win.labelTitle.Content = caption;
            win.Buttons = myMessageBoxButton;
            win.UC.Content = userControl;
            win.sb.VerticalScrollBarVisibility = ucSBV;
            win.sb.HorizontalScrollBarVisibility = ucSBH;
            win.sb.MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight * MaxVertical;
            win.sb.MaxWidth = System.Windows.SystemParameters.PrimaryScreenWidth * MaxHorizontal;
            win.ShowDialog();
            win.UC.Content = null;
            return win.Response;
        }

        public static MessageBoxResult ShowShadow(Window owner, object userControl, string caption, MyMessageBoxButton myMessageBoxButton, ScrollBarVisibility scrollBarVisibility, ScrollBarVisibility scrollBarVisibility_5, Button CloseButton)
        {

            ViewWindow_Modal win = new ViewWindow_Modal();
            TypeConverterStringToUIElement ocov = new TypeConverterStringToUIElement();
            win.Caption = (UIElement)ocov.ConvertFromString(_caption);

            if (IsParent)
            {
                ParentWindow = win;
            }
            else
            {
                GrantParentWindow = win;
            }

            win.Title = caption;
            //win.labelTitle.Content = caption;
            win.UC.Content = userControl;
            win.sb.MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight * MaxVertical;
            win.sb.MaxWidth = System.Windows.SystemParameters.PrimaryScreenWidth * MaxHorizontal;
            CloseButton.Click += new RoutedEventHandler(click_cerrar1);

            IsParent = true;

            if (owner == null)
            {
                owner = System.Windows.Application.Current.MainWindow;
                if (owner != null)
                {
                    owner.Activate();
                }
                else
                {
                    owner = ResolveOwnerWindow();
                    if (owner != null)
                    {
                        owner.Activate();
                    }
                }
            }

            Window backgroundWindow = new Window();
            backgroundWindow.Activated += new EventHandler(backgroundWindow_Activated);

            if (owner.WindowState == WindowState.Maximized)
            {
                backgroundWindow.Top = 0;
                backgroundWindow.Left = 0;
            }
            else
            {
                backgroundWindow.Top = owner.Top;
                backgroundWindow.Left = owner.Left;
            }
            backgroundWindow.Height = owner.ActualHeight;
            backgroundWindow.Width = owner.ActualWidth;
            backgroundWindow.AllowsTransparency = true;
            backgroundWindow.Background = Brushes.Transparent;
            backgroundWindow.WindowStyle = WindowStyle.None;
            backgroundWindow.ShowInTaskbar = false;
            Rectangle rect = new Rectangle() { Fill = Brushes.Silver, Opacity = 0.5 };
            backgroundWindow.Content = rect;
            backgroundWindow.Show();
            backgroundWindow.Activate();

            if (owner != null)
            {
                backgroundWindow.Owner = owner;
            }


            win.Owner = backgroundWindow;
            win.ShowDialog();
            if (backgroundWindow != null)
            {
                backgroundWindow.Close();
            }

            if (owner != null)
                owner.Activate();

            IsParent = false;

            win.UC.Content = null;
            CloseButton.Click -= new RoutedEventHandler(click_cerrar1);
            ParentWindow = null;

            if (Cerrar == WinBehavior.OK)
            {
                win.response = MessageBoxResult.OK;
            }
            else
            {
                win.response = MessageBoxResult.Cancel;
            }

            var x = win.response;
            return win.response;


        }

        static void backgroundWindow_Activated(object sender, EventArgs e)
        {
            Window oWindowOwner = (Window)sender;
            if (oWindowOwner.Owner != null)
            {
                oWindowOwner.Owner.Activate();
            }

        }

        void Current_Activated(object sender, EventArgs e)
        {
            this.Activate();
        }

        private static Window ResolveOwnerWindow()
        {
            Window owner = null;
            if (System.Windows.Application.Current != null)
            {
                foreach (Window w in System.Windows.Application.Current.Windows)
                {
                    if (w.IsActive)
                    {
                        owner = w;
                        break;
                    }
                }
            }
            return owner;
        }

        #endregion
    }
}
