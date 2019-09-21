﻿using IncogStuffControl.BusinessClass.Enums;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IncogStuffControl.UserControls.Selectors
{
    /// <summary>
    /// Interaction logic for BreakSelector.xaml
    /// </summary>
    public partial class BreakSelector : UserControl
    {
        private int[] ValidGainValues = new[] { 0, 15, 30, 45, 60 };
        public BreakSelector()
        {
            InitializeComponent();
            cmbBreaks.ItemsSource = ValidGainValues;
            cmbBreaks.SelectedItem = 0;
        }
    }
}
