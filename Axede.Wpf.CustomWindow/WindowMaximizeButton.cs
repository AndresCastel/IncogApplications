﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Controls;
using System.Globalization;

namespace Axede.WPF.CustomWindow
{
    public class WindowMaximizeButton : WindowButton
    {
        public WindowMaximizeButton()
        {
            // open resource where in XAML are defined icons and colors
            Stream resourceStream = Application.GetResourceStream(new Uri("pack://application:,,,/Axede.WPF.CustomWindow;component/ButtonIcons.xaml")).Stream;
            ResourceDictionary resourceDictionary = (ResourceDictionary)XamlReader.Load(resourceStream);

            this.Content = resourceDictionary["WindowButtonMaximizeIcon"];
            this.ContentDisabled = resourceDictionary["WindowButtonMaximizeIconDisabled"];
        }
    }
}
