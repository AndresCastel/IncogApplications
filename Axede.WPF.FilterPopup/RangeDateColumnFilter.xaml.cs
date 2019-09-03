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
    public partial class RangeDateColumnFilter : BaseColumnFilter
    {

        #region Defniciones

          
        #endregion

        #region EventosControl

           public RangeDateColumnFilter()
            {
                InitializeComponent();
            }

            private void BaseColumnFilter_Loaded(object sender, RoutedEventArgs e)
            {
                dtpInicio.Focus();
            
            }

            private void dtpInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
            {
                CrearFiltro();
               
            }

            private void dtpInicio_CalendarOpened(object sender, RoutedEventArgs e)
            {
                dtpFin.IsDropDownOpen = false;
            }

            private void dtpInicio_TodayClick(object sender, RoutedEventArgs e)
            {
                dtpInicio.IsDropDownOpen = false;
            }

            private void dtpFin_TodayClick(object sender, RoutedEventArgs e)
            {
                dtpFin.IsDropDownOpen = false;
            }
            private void dtpFin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
            {
                CrearFiltro();
               
            }

            private void dtpFin_CalendarOpened(object sender, RoutedEventArgs e)
            {
                dtpInicio.IsDropDownOpen = false;
            }

            private void CrearFiltro() 
            {
                DateTime dFechaInicial = Convert.ToDateTime(dtpInicio.SelectedDate);
                DateTime dFechaFinal = Convert.ToDateTime(dtpFin.SelectedDate);

                if (dFechaInicial.Year != 1 && dFechaFinal.Year != 1)
                {

                    DateTime dFechaInicialBase = new DateTime(dFechaInicial.Year, dFechaInicial.Month, dFechaInicial.Day);
                    DateTime dFechaFinalBase = new DateTime(dFechaFinal.Year, dFechaFinal.Month, dFechaFinal.Day);
                    if (DateTime.Compare(dFechaFinalBase, dFechaInicialBase) < 0)
                    {
                        dtpFin.Text = string.Empty;
                    }
                    else
                    {
                        Filtro = dFechaInicial.ToString("dd/MM/yyyy") + "|" + dFechaFinal.ToString("dd/MM/yyyy");
                        dtpInicio.IsDropDownOpen = false;
                        dtpFin.IsDropDownOpen = false;
                        ActivarFiltro = true;
                    }
                }
                
            }

        #endregion

           



    }
}
