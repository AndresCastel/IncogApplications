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

namespace Axede.WPF.FilterPopup
{
    /// <summary>
    /// Lógica de interacción para ComboBoxColumnFilter.xaml
    /// </summary>
    public partial class ComboBoxColumnFilter : BaseColumnFilter
    {

        #region Defniciones
        
            private List<FilterPopupList> _ListaDatos;

        #endregion

        #region EventosControl

            public ComboBoxColumnFilter(List<FilterPopupList> oListaDatos)
            {
                InitializeComponent();

                _ListaDatos = oListaDatos;
                
                BindingLista();
            }

            private void BaseColumnFilter_Loaded(object sender, RoutedEventArgs e)
            {
                cmbFilter.Focus();
            }

            private void BaseColumnFilter_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Return)
                {
                    if (cmbFilter.SelectedValue != null)
                    {
                        Filtro = cmbFilter.SelectedValue.ToString();
                        ActivarFiltro = true;
                    }
                }
            }

            private void BindingLista()
            {
                cmbFilter.ItemsSource = null;
                cmbFilter.Items.Clear();
                cmbFilter.IsEnabled = false;

                if (_ListaDatos.Count > 0)
                {
                    cmbFilter.DisplayMemberPath = "Valor";
                    cmbFilter.SelectedValuePath = "IdValor";
                    cmbFilter.ItemsSource = _ListaDatos;
                    cmbFilter.IsEnabled = true;
                }     
            }

            private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                if (cmbFilter.SelectedValue != null)
                {
                    if (cmbFilter.SelectedIndex != -1)
                    {
                        Filtro = cmbFilter.SelectedValue.ToString();
                        ActivarFiltro = true;
                    }
                }
            }

        #endregion

            

          

           
    }
}
