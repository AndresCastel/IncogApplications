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
using System.ComponentModel;

namespace Axede.WPF.FilterPopup
{
    /// <summary>
    /// Lógica de interacción para TextBoxColumnFilter.xaml
    /// </summary>
    public partial class TextBoxColumnFilter : BaseColumnFilter
    {

        #region Defniciones

          
        #endregion

        #region EventosControl

            public TextBoxColumnFilter()
            {
                InitializeComponent();
            }

            private void BaseColumnFilter_Loaded(object sender, RoutedEventArgs e)
            {
                txtFilter.Focus();
            
            }

            private void txtFilter_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Return) 
                {
                    if (txtFilter.Text.Trim() != string.Empty)
                    {
                        Filtro = txtFilter.Text.Trim();
                        ActivarFiltro = true;
                    }
                   
                }
            }

            private void BaseColumnFilter_LostFocus(object sender, RoutedEventArgs e)
            {
                if (txtFilter.Text.Trim() != string.Empty)
                {
                    Filtro = txtFilter.Text.Trim();
                }
            }

        #endregion

        

           

          
       

    }
}
