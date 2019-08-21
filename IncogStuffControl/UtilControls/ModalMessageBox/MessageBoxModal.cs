﻿/************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2010-2012 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license           

   This program can be provided to you by Xceed Software Inc. under a
   proprietary commercial license agreement for use in non-Open Source
   projects. The commercial version of Extended WPF Toolkit also includes
   priority technical support, commercial updates, and many additional 
   useful WPF controls if you license Xceed Business Suite for WPF.

   Visit http://xceed.com and follow @datagrid on Twitter.

  **********************************************************************/
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Input;
using System.Text;
using System.Security.Permissions;
using System.Security;
using Xceed.Wpf.Toolkit.Primitives;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Interop;
using Incog.wpf.Messages;
using System.Windows.Shapes;

namespace IncogStuffControl.UtilControls.ModalMessageBox
{
    public class MessageBoxModal : WindowControl
    {
        #region Private Members

        /// <summary>
        /// Tracks the MessageBoxButon value passed into the InitializeContainer method
        /// </summary>
        private MessageBoxButton _button = MessageBoxButton.OK;

        /// <summary>
        /// Tracks the MessageBoxResult to set as the default and focused button
        /// </summary>
        private MessageBoxResult _defaultResult = MessageBoxResult.None;

        /// <summary>
        /// Tracks the owner of the MessageBox
        /// </summary>
        private Window _owner;

        #endregion //Private Members

        #region Constructors

        static MessageBoxModal()
        {
            DefaultStyleKeyProperty.OverrideMetadata( typeof( MessageBoxModal), new FrameworkPropertyMetadata( typeof( MessageBoxModal )));
        }

        internal MessageBoxModal()
        {
            /*user cannot create instance */
            AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(Button_Click));
        }

        #endregion //Constructors

        #region Properties

        #region Protected Properties

        /// <summary>
        /// A System.Windows.MessageBoxResult value that specifies which message box button was clicked by the user.
        /// </summary>
        protected MessageBoxResult MessageBoxResult = MessageBoxResult.None;

        protected Window Container
        {
            get;
            private set;
        }

        protected Window WindowBackGround
        {
            get;
            private set;
        }

        protected Thumb DragWidget
        {
            get;
            private set;
        }

        #endregion //Protected Properties

        #region Dependency Properties

        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("Caption", typeof(string), typeof(MessageBoxModal), new UIPropertyMetadata(String.Empty));
        public string Caption
        {
            get
            {
                return (string)GetValue(CaptionProperty);
            }
            set
            {
                SetValue(CaptionProperty, value);
            }
        }

        public static readonly DependencyProperty CaptionForegroundProperty = DependencyProperty.Register("CaptionForeground", typeof(Brush), typeof(MessageBoxModal), new UIPropertyMetadata(null));
        public Brush CaptionForeground
        {
            get
            {
                return (Brush)GetValue(CaptionForegroundProperty);
            }
            set
            {
                SetValue(CaptionForegroundProperty, value);
            }
        }

        public static readonly DependencyProperty CancelButtonContentProperty = DependencyProperty.Register("CancelButtonContent", typeof(object), typeof(MessageBoxModal), new UIPropertyMetadata(AdministradorMensaje.Instance.GetMensajePorCodigo(CodeMessagesGeneral.General_TextoBotonCANCEL_Msg)));
        public object CancelButtonContent
        {
            get
            {
                return (object)GetValue(CancelButtonContentProperty);
            }
            set
            {
                SetValue(CancelButtonContentProperty, value);
            }
        }

        public static readonly DependencyProperty CloseButtonStyleProperty = DependencyProperty.Register( "CloseButtonStyle", typeof(Style), typeof(MessageBoxModal), new PropertyMetadata(null));
        public Style CloseButtonStyle
        {
            get
            {
                return (Style)GetValue(CloseButtonStyleProperty);
            }
            set
            {
                SetValue(CloseButtonStyleProperty, value);
            }
        }

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(MessageBoxModal), new UIPropertyMetadata(default(ImageSource)));
        public ImageSource ImageSource
        {
            get
            {
                return (ImageSource)GetValue(ImageSourceProperty);
            }
            set
            {
                SetValue(ImageSourceProperty, value);
            }
        }

        public static readonly DependencyProperty NoButtonContentProperty = DependencyProperty.Register("NoButtonContent", typeof(object), typeof(MessageBoxModal), new UIPropertyMetadata(AdministradorMensaje.Instance.GetMensajePorCodigo(CodeMessagesGeneral.General_TextoBotonNO_Msg)));
        public object NoButtonContent
        {
            get
            {
                return (object)GetValue(NoButtonContentProperty);
            }
            set
            {
                SetValue(NoButtonContentProperty, value);
            }
        }

        public static readonly DependencyProperty OkButtonContentProperty = DependencyProperty.Register("OkButtonContent", typeof(object), typeof(MessageBoxModal), new UIPropertyMetadata(AdministradorMensaje.Instance.GetMensajePorCodigo(CodeMessagesGeneral.General_TextoBotonOK_Msg)));
        public object OkButtonContent
        {
            get
            {
                return (object)GetValue(OkButtonContentProperty);
            }
            set
            {
                SetValue(OkButtonContentProperty, value);
            }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MessageBoxModal), new UIPropertyMetadata(String.Empty));
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public static readonly DependencyProperty WindowBackgroundProperty = DependencyProperty.Register("WindowBackground", typeof(Brush), typeof(MessageBoxModal), new PropertyMetadata(null));
        public Brush WindowBackground
        {
            get
            {
                return (Brush)GetValue(WindowBackgroundProperty);
            }
            set
            {
                SetValue(WindowBackgroundProperty, value);
            }
        }

        public static readonly DependencyProperty WindowBorderBrushProperty = DependencyProperty.Register("WindowBorderBrush", typeof(Brush), typeof(MessageBoxModal), new PropertyMetadata(null));
        public Brush WindowBorderBrush
        {
            get
            {
                return (Brush)GetValue(WindowBorderBrushProperty);
            }
            set
            {
                SetValue(WindowBorderBrushProperty, value);
            }
        }

        public static readonly DependencyProperty WindowOpacityProperty = DependencyProperty.Register("WindowOpacity", typeof(double), typeof(MessageBoxModal), new PropertyMetadata(null));
        public double WindowOpacity
        {
            get
            {
                return (double)GetValue(WindowOpacityProperty);
            }
            set
            {
                SetValue(WindowOpacityProperty, value);
            }
        }

        public static readonly DependencyProperty YesButtonContentProperty = DependencyProperty.Register("YesButtonContent", typeof(object), typeof(MessageBoxModal), new UIPropertyMetadata(AdministradorMensaje.Instance.GetMensajePorCodigo(CodeMessagesGeneral.General_TextoBotonYES_Msg)));
        public object YesButtonContent
        {
            get
            {
                return (object)GetValue(YesButtonContentProperty);
            }
            set
            {
                SetValue(YesButtonContentProperty, value);
            }
        }

        #endregion //Dependency Properties

        #endregion //Properties

        #region Base Class Overrides

        /// <summary>
        /// Overrides the OnApplyTemplate method.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (DragWidget != null)
                DragWidget.DragDelta -= (o, e) => ProcessMove(e);

            DragWidget = GetTemplateChild("PART_DragWidget") as Thumb;

            if (DragWidget != null)
                DragWidget.DragDelta += (o, e) => ProcessMove(e);

            ChangeVisualState(_button.ToString(), true);

            SetDefaultResult();
        }

        #endregion //Base Class Overrides

        #region Methods

        #region Public Static

        /// <summary>
        /// Displays a message box that has a message and that returns a result.
        /// </summary>
        /// <param name="messageText">A System.String that specifies the text to display.</param>
        /// <returns>A System.Windows.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(string messageText, bool bShowBackGround = false)
        {
            return Show(messageText, string.Empty, MessageBoxButton.OK, bShowBackGround);
        }

        /// <summary>
        /// Displays a message box that has a message and that returns a result.
        /// </summary>
        /// <param name="owner">A System.Windows.Window that represents the owner of the MessageBox</param>
        /// <param name="messageText">A System.String that specifies the text to display.</param>
        /// <returns>A System.Windows.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(Window owner, string messageText, bool bShowBackGround = false)
        {
            return Show(owner, messageText, string.Empty, MessageBoxButton.OK, bShowBackGround);
        }

        /// <summary>
        /// Displays a message box that has a message and title bar caption; and that returns a result.
        /// </summary>
        /// <param name="messageText">A System.String that specifies the text to display.</param>
        /// <param name="caption">A System.String that specifies the title bar caption to display.</param>
        /// <returns>A System.Windows.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(string messageText, string caption, bool bShowBackGround = false)
        {
            return Show(messageText, caption, MessageBoxButton.OK, bShowBackGround);
        }


        public static MessageBoxResult Show(Window owner, string messageText, string caption, bool bShowBackGround = false)
        {
            return Show(owner, messageText, caption, MessageBoxButton.OK, bShowBackGround);
        }

        /// <summary>
        /// Displays a message box that has a message and that returns a result.
        /// </summary>
        /// <param name="messageText">A System.String that specifies the text to display.</param>
        /// <param name="caption">A System.String that specifies the title bar caption to display.</param>
        /// <param name="button">A System.Windows.MessageBoxButton value that specifies which button or buttons to display.</param>
        /// <returns>A System.Windows.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(string messageText, string caption, MessageBoxButton button, bool bShowBackGround = false)
        {
            return ShowCore(null, messageText, caption, button, MessageBoxImage.None, MessageBoxResult.None, bShowBackGround);
        }


        public static MessageBoxResult Show(Window owner, string messageText, string caption, MessageBoxButton button, bool bShowBackGround = false)
        {
            return ShowCore(owner, messageText, caption, button, MessageBoxImage.None, MessageBoxResult.None, bShowBackGround);
        }

        /// <summary>
        /// Displays a message box that has a message and that returns a result.
        /// </summary>
        /// <param name="messageText">A System.String that specifies the text to display.</param>
        /// <param name="caption">A System.String that specifies the title bar caption to display.</param>
        /// <param name="button">A System.Windows.MessageBoxButton value that specifies which button or buttons to display.</param>
        /// <param name="image"> A System.Windows.MessageBoxImage value that specifies the icon to display.</param>
        /// <returns>A System.Windows.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(string messageText, string caption, MessageBoxButton button, MessageBoxImage icon, bool bShowBackGround = false)
        {
            return ShowCore(null, messageText, caption, button, icon, MessageBoxResult.None, bShowBackGround);
        }


        public static MessageBoxResult Show(Window owner, string messageText, string caption, MessageBoxButton button, MessageBoxImage icon, bool bShowBackGround = false)
        {
            return ShowCore(owner, messageText, caption, button, icon, MessageBoxResult.None, bShowBackGround);
        }

        /// <summary>
        /// Displays a message box that has a message and that returns a result.
        /// </summary>
        /// <param name="messageText">A System.String that specifies the text to display.</param>
        /// <param name="caption">A System.String that specifies the title bar caption to display.</param>
        /// <param name="button">A System.Windows.MessageBoxButton value that specifies which button or buttons to display.</param>
        /// <param name="image"> A System.Windows.MessageBoxImage value that specifies the icon to display.</param>
        /// <param name="defaultResult">A System.Windows.MessageBoxResult value that specifies the default result of the MessageBox.</param>
        /// <returns>A System.Windows.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(string messageText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, bool bShowBackGround = false)
        {
            return ShowCore(null, messageText, caption, button, icon, defaultResult, bShowBackGround);
        }


        public static MessageBoxResult Show(Window owner, string messageText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, bool bShowBackGround = false)
        {
            return ShowCore(owner, messageText, caption, button, icon, defaultResult, bShowBackGround);
        }

        #endregion //Public Static

        #region Protected

        /// <summary>
        /// Shows the container which contains the MessageBox.
        /// </summary>
        protected void Show()
        {
            Container.ShowDialog();
        }

        /// <summary>
        /// Initializes the MessageBox.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="button">The button.</param>
        /// <param name="image">The image.</param>
        protected void InitializeMessageBox(Window owner, string text, string caption, MessageBoxButton button, MessageBoxImage image, MessageBoxResult defaultResult, bool bShowBackGround)
        {
            Text = text;
            Caption = caption;
            _button = button;
            _defaultResult = defaultResult;
            _owner = owner;

            if (_owner == null)
            {
                _owner = System.Windows.Application.Current.MainWindow;
            }

            SetImageSource(image);
            Container = CreateContainer(bShowBackGround);
            WindowBackGround = Container.Owner;
        }

        /// <summary>
        /// Changes the control's visual state(s).
        /// </summary>
        /// <param name="name">name of the state</param>
        /// <param name="useTransitions">True if state transitions should be used.</param>
        protected void ChangeVisualState(string name, bool useTransitions)
        {
            VisualStateManager.GoToState(this, name, useTransitions);
        }

        #endregion //Protected

        #region Private

        /// <summary>
        /// Sets the button that represents the _defaultResult to the default button and gives it focus.
        /// </summary>
        private void SetDefaultResult()
        {
            var defaultButton = GetDefaultButtonFromDefaultResult();
            if (defaultButton != null)
            {
                defaultButton.IsDefault = true;
                defaultButton.Focus();
            }
        }

        /// <summary>
        /// Gets the default button from the _defaultResult.
        /// </summary>
        /// <returns>The default button that represents the defaultResult</returns>
        private Button GetDefaultButtonFromDefaultResult()
        {
            Button defaultButton = null;
            switch (_defaultResult)
            {
                case MessageBoxResult.Cancel:
                    defaultButton = GetMessageBoxButton("PART_CancelButton");
                    break;
                case MessageBoxResult.No:
                    defaultButton = GetMessageBoxButton("PART_NoButton");
                    break;
                case MessageBoxResult.OK:
                    defaultButton = GetMessageBoxButton("PART_OkButton");
                    break;
                case MessageBoxResult.Yes:
                    defaultButton = GetMessageBoxButton("PART_YesButton");
                    break;
                case MessageBoxResult.None:
                    defaultButton = GetDefaultButton();
                    break;
            }
            return defaultButton;
        }

        /// <summary>
        /// Gets the default button.
        /// </summary>
        /// <remarks>Used when the _defaultResult is set to None</remarks>
        /// <returns>The button to use as the default</returns>
        private Button GetDefaultButton()
        {
            Button defaultButton = null;
            switch (_button)
            {
                case MessageBoxButton.OK:
                case MessageBoxButton.OKCancel:
                    defaultButton = GetMessageBoxButton("PART_OkButton");
                    break;
                case MessageBoxButton.YesNo:
                case MessageBoxButton.YesNoCancel:
                    defaultButton = GetMessageBoxButton("PART_YesButton");
                    break;
            }
            return defaultButton;
        }

        /// <summary>
        /// Gets a message box button.
        /// </summary>
        /// <param name="name">The name of the button to get.</param>
        /// <returns>The button</returns>
        private Button GetMessageBoxButton(string name)
        {
            Button button = GetTemplateChild(name) as Button;
            return button;
        }

        /// <summary>
        /// Shows the MessageBox.
        /// </summary>
        /// <param name="messageText">The message text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="button">The button.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="defaultResult">The default result.</param>
        /// <returns></returns>
        private static MessageBoxResult ShowCore(Window owner, string messageText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, bool bShowBackGround)
        {
            MessageBoxModal msgBox = new MessageBoxModal();
            msgBox.InitializeMessageBox(owner, messageText, caption, button, icon, defaultResult, bShowBackGround);
            msgBox.Show();
            if (bShowBackGround == true)
            {
                if (msgBox.WindowBackGround != null)
                {
                    msgBox.WindowBackGround.Close();
                }
            }
            if (msgBox.WindowBackGround != null)
            {
                if (msgBox.WindowBackGround.Owner != null)
                {
                    msgBox.WindowBackGround.Owner.Activate();
                }
            }
            return msgBox.MessageBoxResult;
        }

        /// <summary>
        /// Resolves the owner Window of the MessageBox.
        /// </summary>
        /// <returns>the owner Window</returns>
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

        /// <summary>
        /// Sets the message image source.
        /// </summary>
        /// <param name="image">The image to show.</param>
        private void SetImageSource(MessageBoxImage image)
        {
            String iconName = String.Empty;

            switch (image)
            {
                case MessageBoxImage.Error:
                    {
                        iconName = "MsgError48.png";
                        break;
                    }
                case MessageBoxImage.Information:
                    {
                        iconName = "MsgInformation48.png";
                        break;
                    }
                case MessageBoxImage.Question:
                    {
                        iconName = "MsgQuestion48.png";
                        break;
                    }
                case MessageBoxImage.Warning:
                    {
                        iconName = "MsgWarning48.png";
                        break;
                    }
                case MessageBoxImage.None:
                default:
                    {
                        return;
                    }
            }

            ImageSource = (ImageSource)new ImageSourceConverter().ConvertFromString(String.Format("pack://application:,,,/IncogStuffControl;component/Themes/Images/{0}", iconName));
        }

        /// <summary>
        /// Creates the container which will host the MessageBox control.
        /// </summary>
        /// <returns></returns>
        private Window CreateContainer(bool bShowBackGround)
        {
            var newWindow = new Window();
            newWindow.AllowsTransparency = true;
            newWindow.Background = Brushes.Transparent;
            newWindow.Content = this;
            newWindow.Owner = _owner ?? ResolveOwnerWindow();

            if (newWindow.Owner != null)
                newWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            else
                newWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            newWindow.ShowInTaskbar = false;
            newWindow.SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
            newWindow.ResizeMode = System.Windows.ResizeMode.NoResize;
            newWindow.WindowStyle = System.Windows.WindowStyle.None;

            if (bShowBackGround == true)
            {
                Window backgroundWindow = new Window();
                if (newWindow.Owner != null)
                {
                    if (newWindow.Owner.WindowState == WindowState.Maximized)
                    {
                        backgroundWindow.Top = 0;
                        backgroundWindow.Left = 0;
                    }
                    else
                    {
                        backgroundWindow.Top = newWindow.Owner.Top;
                        backgroundWindow.Left = newWindow.Owner.Left;
                    }
                    backgroundWindow.Height = newWindow.Owner.ActualHeight;
                    backgroundWindow.Width = newWindow.Owner.ActualWidth;
                    backgroundWindow.AllowsTransparency = true;
                    backgroundWindow.Background = Brushes.Transparent;
                    backgroundWindow.WindowStyle = WindowStyle.None;
                    backgroundWindow.ShowInTaskbar = false;
                    Rectangle rect = new Rectangle() { Fill = Brushes.Silver, Opacity = 0.5 };
                    backgroundWindow.Content = rect;
                    backgroundWindow.Show();

                    if (newWindow.Owner == null)
                    {
                        newWindow.Owner = backgroundWindow;
                    }
                    else
                    {
                        newWindow.Owner = backgroundWindow;
                        backgroundWindow.Owner = _owner;

                    }
                }

            }

            return newWindow;
        }

        #endregion //Private

        #endregion //Methods

        #region Event Handlers

        /// <summary>
        /// Processes the move of a drag operation on the header.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Controls.Primitives.DragDeltaEventArgs"/> instance containing the event data.</param>
        private void ProcessMove(DragDeltaEventArgs e)
        {
            double left = 0.0;

            if (FlowDirection == System.Windows.FlowDirection.RightToLeft)
                left = Container.Left - e.HorizontalChange;
            else
                left = Container.Left + e.HorizontalChange;

            Container.Left = left;
            Container.Top = Container.Top + e.VerticalChange;
        }

        /// <summary>
        /// Sets the MessageBoxResult according to the button pressed and then closes the MessageBox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.OriginalSource as Button;

            if (button == null)
                return;

            switch (button.Name)
            {
                case "PART_NoButton":
                    MessageBoxResult = MessageBoxResult.No;
                    break;
                case "PART_YesButton":
                    MessageBoxResult = MessageBoxResult.Yes;
                    break;
                case "PART_CloseButton":
                case "PART_CancelButton":
                    MessageBoxResult = MessageBoxResult.Cancel;
                    break;
                case "PART_OkButton":
                    MessageBoxResult = MessageBoxResult.OK;
                    break;
            }

            Close();
        }

        /// <summary>
        /// Closes the MessageBox.
        /// </summary>
        private void Close()
        {
            Container.Close();
        }

        #endregion //Event Handlers
    }
}
