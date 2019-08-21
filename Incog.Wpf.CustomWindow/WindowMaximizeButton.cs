﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Incog.Wpf.CustomWindow
{
    public class WindowMaximizeButton : WindowButton
    {
        public WindowMaximizeButton()
        {
            // open resource where in XAML are defined icons and colors
            Stream resourceStream = Application.GetResourceStream(new Uri("pack://application:,,,/Incog.Wpf.CustomWindow;component/ButtonIcons.xaml")).Stream;
            ResourceDictionary resourceDictionary = (ResourceDictionary)XamlReader.Load(resourceStream);

            this.Content = resourceDictionary["WindowButtonMaximizeIcon"];
            this.ContentDisabled = resourceDictionary["WindowButtonMaximizeIconDisabled"];
        }
    }
}
