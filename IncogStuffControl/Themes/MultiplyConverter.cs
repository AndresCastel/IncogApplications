using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace IncogStuffControl.Themes
{
    public class MultiplyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!ApplicationIsInDesignMode)
            {
                double result = 1.0;
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] is double)
                        result *= (double)values[i];
                }
                return result;
            }
            return Binding.DoNothing;
        }

        private static bool ApplicationIsInDesignMode
        {
            get { return (bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue); }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (!ApplicationIsInDesignMode)
            {
                throw new System.Exception("Not implemented");
            }

            return null;
        }
    }
}
