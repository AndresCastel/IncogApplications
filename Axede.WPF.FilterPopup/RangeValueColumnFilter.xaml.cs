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
    /// Lógica de interacción para RangeDateColumnFilter.xaml
    /// </summary>
    public partial class RangeValueColumnFilter : BaseColumnFilter
    {

        #region Defniciones

          
        #endregion

        #region EventosControl

            public RangeValueColumnFilter()
            {
                InitializeComponent();
            }

            private void BaseColumnFilter_Loaded(object sender, RoutedEventArgs e)
            {
               txtInicial.Focus();
            
            }

            private void BaseColumnFilter_LostFocus(object sender, RoutedEventArgs e)
            {
                CrearFiltro();
            }

            private void txtInicial_PreviewTextInput(object sender, TextCompositionEventArgs e)
            {
                if (!char.IsNumber(e.Text, e.Text.Length - 1))
                {
                    e.Handled = true;
                }
            }

            private void txtInicial_TextChanged(object sender, TextChangedEventArgs e)
            {
                CrearFiltro(false);
            }

            private void txtFinal_TextChanged(object sender, TextChangedEventArgs e)
            {
                CrearFiltro(false);
            }


            private void txtInicial_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Return)
                {
                    CrearFiltro();
                }
            }

            private void txtFinal_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Return)
                {
                    CrearFiltro();
                }
            }

            private void textBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
            {
                if (e.Command == ApplicationCommands.Copy ||
                    e.Command == ApplicationCommands.Cut ||
                    e.Command == ApplicationCommands.Paste)
                {
                    e.Handled = true;
                }
            }

            private void CrearFiltro(bool bFiltrar=true)
            {
                if (txtInicial.Text.Trim() != string.Empty && txtFinal.Text.Trim() != string.Empty)
                {

                    double dValInicial = Convert.ToDouble(txtInicial.Text.Trim());
                    double dValFinal = Convert.ToDouble(txtFinal.Text.Trim());

                    if (dValFinal < dValInicial)
                    {
                        ////txtFinal.Text = string.Empty;
                    }
                    else
                    {
                        Filtro = txtInicial.Text.Trim() + "|" + txtFinal.Text.Trim();
                        if (bFiltrar == true)
                        {
                            ActivarFiltro = true;
                        }
                    }
                }
            
            }

        #endregion

          
            

            

    }
}
