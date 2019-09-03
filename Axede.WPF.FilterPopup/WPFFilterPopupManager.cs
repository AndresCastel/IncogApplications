using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace Axede.WPF.FilterPopup
{
    public delegate void ColumnFilterEventHandler(object sender, ColumnFilterEventArgs e);
   
    public class WPFFilterPopupManager : IDisposable
    {

        private bool disposed = false;
        private DataGrid mDataGridView;

        private Dictionary<string, BaseColumnFilter> mColumnFilterList;

        private ContextMenu mPopup;
        public event ColumnFilterEventHandler ColumnFilterAdding;
        public event ColumnFilterEventHandler ColumnFilterExec;

        public DataGrid DataGrid
        {
            get
            {
                return mDataGridView;
            }
            set
            {
                mDataGridView = value;
                mColumnFilterList = new Dictionary<string, BaseColumnFilter>(mDataGridView.Columns.Count);

                if (mDataGridView == null)
                {
                    return;
                }
                else
                {
                    mDataGridView.MouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(mDataGridView_MouseRightButtonDown);
                }

                //Crea el item en la lista por cada columna
                foreach (DataGridColumn c in mDataGridView.Columns)
                {
                    string sClave = (c.SortMemberPath == string.Empty) ? c.DisplayIndex.ToString() : c.SortMemberPath;

                    if (!mColumnFilterList.ContainsKey(sClave))
                             mColumnFilterList.Add(sClave, null);
                }

                foreach (DataGridColumn c in mDataGridView.Columns)
                {
                    CreateColumnFilter(c);
                }

                mDataGridView.SelectedIndex = -1;
            }
        
        }

           

        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            if (parent != null)
            {
                int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < numVisuals; i++)
                {
                    Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                    child = v as T;
                    if (child == null)
                    {
                        child = GetVisualChild<T>(v);
                    }
                    if (child != null)
                    {
                        break;
                    }
                }
            }
            return child;
        }

        void mDataGridView_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (e.ClickCount == 1 && e.ButtonState== System.Windows.Input.MouseButtonState.Pressed)
                {
                    DependencyObject depObj = (DependencyObject)e.OriginalSource;

                    while (
                        (depObj != null) &&
                        !(depObj is DataGridCell) &&
                        !(depObj is DataGridColumnHeader))
                    {
                        depObj = VisualTreeHelper.GetParent(depObj);
                    }

                    if (depObj == null)
                    {
                        return;
                    }

                    if (depObj is DataGridColumnHeader)
                    {
                        while ((depObj != null) && !(depObj is DataGridColumnHeader))
                        {
                            depObj = VisualTreeHelper.GetParent(depObj);
                        }

                        DataGridColumnHeader dgColumnHeader = depObj as DataGridColumnHeader;

                        if (dgColumnHeader.ContextMenu != null)
                        {
                            if (dgColumnHeader.IsVisible == false)
                            {
                                dgColumnHeader = null;
                            }
                            else
                            {
                                mPopup = dgColumnHeader.ContextMenu;
                            }
                        }

                        if (mPopup != null)
                        {
                            mPopup.Items.Clear();
                            mPopup = null;
                        }


                        ShowPopup(dgColumnHeader);
                        return;

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        

        }


        #region Private 

            private void ShowPopup(DataGridColumnHeader oColumnHeader)
            {
                int ColumnIndex = oColumnHeader.DisplayIndex;


                //definiciones
                mPopup = new ContextMenu();
                mPopup.Style = (Style)Application.Current.FindResource("FilterContexMenu");
                mPopup.Closed += new RoutedEventHandler(mPopup_Closed);

                FilterHost oFilterHost_Base = new FilterHost();
                
                
                string sClave = (oColumnHeader.Column.SortMemberPath == string.Empty) ? oColumnHeader.DisplayIndex.ToString() : oColumnHeader.Column.SortMemberPath;
                if (mColumnFilterList.ContainsKey(sClave))
                {
                    BaseColumnFilter _TMPBaseColumnFilter = mColumnFilterList[sClave];
                    if (_TMPBaseColumnFilter != null)
                    {
                        oFilterHost_Base = new FilterHost();
                        oFilterHost_Base = _TMPBaseColumnFilter.FilterHost;
                        if (oFilterHost_Base == null) 
                        {
                            oFilterHost_Base = new FilterHost();
                            oFilterHost_Base.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(oFilterHost_PropertyChanged);
                            oFilterHost_Base.lblColumna.Content = oColumnHeader.Content;
                            oFilterHost_Base.FieldName = oColumnHeader.Column.SortMemberPath;
                            oFilterHost_Base.IndexColumn = ColumnIndex;

                            _TMPBaseColumnFilter.GridColumnHeader = oColumnHeader;
                            _TMPBaseColumnFilter.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(WPFFilterPopupManager_PropertyChanged);

                            oFilterHost_Base.UC.Content = _TMPBaseColumnFilter;
                            mColumnFilterList[sClave].FilterHost = oFilterHost_Base;
                        }
                    }

                    FormattedText sFT = new FormattedText(oFilterHost_Base.lblColumna.Content.ToString(), CultureInfo.GetCultureInfo("en-us"),
                                                          FlowDirection.LeftToRight,
                                                          new Typeface(oFilterHost_Base.lblColumna.FontFamily.ToString()),
                                                          oFilterHost_Base.lblColumna.FontSize,
                                                          Brushes.Black);


                    if (_TMPBaseColumnFilter != null)
                    {
                        //carga los valores actuales de filtro
                        if (_TMPBaseColumnFilter.GetType().ToString().Contains("TextBoxColumnFilter"))
                        {
                            if (sFT.Width + 30 > ((TextBoxColumnFilter)_TMPBaseColumnFilter).txtFilter.Width)
                            {
                                ((TextBoxColumnFilter)_TMPBaseColumnFilter).txtFilter.Width = sFT.Width + 30;
                            }
                            ((TextBoxColumnFilter)_TMPBaseColumnFilter).txtFilter.Text = _TMPBaseColumnFilter.Filtro;

                        }
                        else if (_TMPBaseColumnFilter.GetType().ToString().Contains("ComboBoxColumnFilter"))
                        {
                            if (sFT.Width + 30 > ((ComboBoxColumnFilter)_TMPBaseColumnFilter).cmbFilter.Width)
                            {
                                ((ComboBoxColumnFilter)_TMPBaseColumnFilter).cmbFilter.Width = sFT.Width + 30;
                            }

                            if (((ComboBoxColumnFilter)_TMPBaseColumnFilter).cmbFilter.Items.Count > 0)
                            {
                                if (_TMPBaseColumnFilter.Filtro.Trim() != string.Empty)
                                {
                                    ((ComboBoxColumnFilter)_TMPBaseColumnFilter).cmbFilter.SelectedValue = Convert.ToInt32(_TMPBaseColumnFilter.Filtro);
                                }
                            }
                        }
                        else if (_TMPBaseColumnFilter.GetType().ToString().Contains("RangeDateColumnFilter"))
                        {
                            if (sFT.Width + 30 > ((RangeDateColumnFilter)_TMPBaseColumnFilter).dtpInicio.Width)
                            {
                                ((RangeDateColumnFilter)_TMPBaseColumnFilter).dtpInicio.Width = sFT.Width + 30;
                            }
                            if (sFT.Width + 30 > ((RangeDateColumnFilter)_TMPBaseColumnFilter).dtpFin.Width)
                            {
                                ((RangeDateColumnFilter)_TMPBaseColumnFilter).dtpFin.Width = sFT.Width + 30;
                            }

                            if (_TMPBaseColumnFilter.Filtro.Trim() != string.Empty)
                            {
                                ((RangeDateColumnFilter)_TMPBaseColumnFilter).dtpInicio.Text = _TMPBaseColumnFilter.Filtro.Split('|')[0];
                                ((RangeDateColumnFilter)_TMPBaseColumnFilter).dtpFin.Text = _TMPBaseColumnFilter.Filtro.Split('|')[1];
                            }
                            else 
                            {
                                ((RangeDateColumnFilter)_TMPBaseColumnFilter).dtpInicio.Text = string.Empty;
                                ((RangeDateColumnFilter)_TMPBaseColumnFilter).dtpFin.Text = string.Empty;
                            }

                        }
                        else if (_TMPBaseColumnFilter.GetType().ToString().Contains("RangeValueColumnFilter"))
                        {
                            if (sFT.Width + 30 > ((RangeValueColumnFilter)_TMPBaseColumnFilter).txtInicial.Width)
                            {
                                ((RangeValueColumnFilter)_TMPBaseColumnFilter).txtInicial.Width = sFT.Width + 30;
                            }
                            if (sFT.Width + 30 > ((RangeValueColumnFilter)_TMPBaseColumnFilter).txtFinal.Width)
                            {
                                ((RangeValueColumnFilter)_TMPBaseColumnFilter).txtFinal.Width = sFT.Width + 30;
                            }


                            if (_TMPBaseColumnFilter.Filtro.Trim() != string.Empty)
                            {
                                ((RangeValueColumnFilter)_TMPBaseColumnFilter).txtInicial.Text = _TMPBaseColumnFilter.Filtro.Split('|')[0];
                                ((RangeValueColumnFilter)_TMPBaseColumnFilter).txtFinal.Text = _TMPBaseColumnFilter.Filtro.Split('|')[1];
                            }
                            else
                            {
                                ((RangeValueColumnFilter)_TMPBaseColumnFilter).txtInicial.Text = string.Empty;
                                ((RangeValueColumnFilter)_TMPBaseColumnFilter).txtFinal.Text = string.Empty;
                            }


                        }
                        

                        mPopup.Items.Add(oFilterHost_Base);
                        oColumnHeader.ContextMenu = mPopup;
                    }
                }


              
           
            }

           
            void oFilterHost_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                FilterHost oFilterHost = (FilterHost)sender;

                int index = ((FilterHost)sender).IndexColumn;

                switch (e.PropertyName)
                {
                    case "ActivarFiltro":

                          ColumnFilterEventArgs cmF = new ColumnFilterEventArgs(CrearFiltros());
                          mPopup.Visibility = Visibility.Hidden;

                          if (((FilterHost)sender).FilterExpression != null)
                          {
                              if (((FilterHost)sender).FilterExpression != string.Empty) 
                              {
                                  //Visualiza imagen filtro en el header de la columna
                                  OcultarIconoFiltroHeader(oFilterHost.FieldName, false);
                              }
                             
                          }
                          if (ColumnFilterExec != null) ColumnFilterExec(null, cmF);
                
                        break;

                    case "CancelarOperacion":

                        LimpiarControles(oFilterHost.FieldName);
                        mPopup.Visibility = Visibility.Hidden;

                        break;

                    case "RemoverFiltro":

                       ((FilterHost)sender).FilterExpression=string.Empty;
                       RemoverFiltro(oFilterHost.FieldName);


                       //oculta imagen filtro en el header de la columna
                       OcultarIconoFiltroHeader(oFilterHost.FieldName, true);
                        
                       mPopup.Visibility = Visibility.Hidden;
                       ColumnFilterEventArgs cmR = new ColumnFilterEventArgs(CrearFiltros());
                       mPopup.Visibility = Visibility.Hidden;
                       if (ColumnFilterExec != null) ColumnFilterExec(null, cmR);

                       break;

                    default:
                        break;
                }

              
            }

            private void OcultarIconoFiltroHeader(string sColumna,bool bOcultar) 
            {
                if (mColumnFilterList[sColumna] != null)
                {
                    DataGridColumnHeader dch = mColumnFilterList[sColumna].GridColumnHeader;
                    Image oimager = GetVisualChild<Image>(dch);
                    if (oimager != null)
                    {
                        if (bOcultar == true)
                        {
                            //oimager.Visibility = Visibility.Collapsed;
                            dch.Tag = "false";
                        }
                        else
                        {
                            //oimager.Visibility = Visibility.Visible;
                            dch.Tag = "true";
                        }
                    }
                }
            }

            private void RemoverFiltro(string sColumna) 
            {
                if (mColumnFilterList.ContainsKey(sColumna))
                {
                    mColumnFilterList[sColumna].Filtro = string.Empty;
                    if (mColumnFilterList[sColumna].GetType().ToString().Contains("TextBoxColumnFilter"))
                    {
                        ((TextBoxColumnFilter)mColumnFilterList[sColumna]).txtFilter.Text = string.Empty;
                    }
                    else if (mColumnFilterList[sColumna].GetType().ToString().Contains("ComboBoxColumnFilter"))
                    {
                        ((ComboBoxColumnFilter)mColumnFilterList[sColumna]).cmbFilter.SelectedIndex = -1;
                    }
                    else if (mColumnFilterList[sColumna].GetType().ToString().Contains("RangeDateColumnFilter")) 
                    {
                        ((RangeDateColumnFilter)mColumnFilterList[sColumna]).dtpInicio.Text = string.Empty;
                        ((RangeDateColumnFilter)mColumnFilterList[sColumna]).dtpFin.Text = string.Empty;
                    }
                    else if (mColumnFilterList[sColumna].GetType().ToString().Contains("RangeValueColumnFilter"))
                    {
                        ((RangeValueColumnFilter)mColumnFilterList[sColumna]).txtInicial.Text = string.Empty;
                        ((RangeValueColumnFilter)mColumnFilterList[sColumna]).txtFinal.Text = string.Empty;
                    }
                }
            }

            private void LimpiarControles(string sColumna)
            {
                if (mColumnFilterList.ContainsKey(sColumna))
                {
                    if (mColumnFilterList[sColumna].Filtro.Trim() == string.Empty)
                    {
                        if (mColumnFilterList[sColumna].GetType().ToString().Contains("TextBoxColumnFilter"))
                        {
                            ((TextBoxColumnFilter)mColumnFilterList[sColumna]).txtFilter.Text = string.Empty;
                        }
                        else if (mColumnFilterList[sColumna].GetType().ToString().Contains("ComboBoxColumnFilter"))
                        {
                            ((ComboBoxColumnFilter)mColumnFilterList[sColumna]).cmbFilter.SelectedIndex = -1;
                        }
                        else if (mColumnFilterList[sColumna].GetType().ToString().Contains("RangeDateColumnFilter"))
                        {
                            ((RangeDateColumnFilter)mColumnFilterList[sColumna]).dtpInicio.Text = string.Empty;
                            ((RangeDateColumnFilter)mColumnFilterList[sColumna]).dtpFin.Text = string.Empty;
                        }
                        else if (mColumnFilterList[sColumna].GetType().ToString().Contains("RangeValueColumnFilter"))
                        {
                            ((RangeValueColumnFilter)mColumnFilterList[sColumna]).txtInicial.Text = string.Empty;
                            ((RangeValueColumnFilter)mColumnFilterList[sColumna]).txtFinal.Text = string.Empty;
                        }
                    }
                }
            }


            public void LimpiarFiltro()
            {
                foreach (KeyValuePair<string, BaseColumnFilter> iFiltro in mColumnFilterList)
                {
                  
                    if (iFiltro.Key != "0")
                    {
                        if (mColumnFilterList[iFiltro.Key] != null)
                        {
                            mColumnFilterList[iFiltro.Key].Filtro = string.Empty;
                            if (mColumnFilterList[iFiltro.Key].GetType().ToString().Contains("TextBoxColumnFilter"))
                            {
                                ((TextBoxColumnFilter)mColumnFilterList[iFiltro.Key]).txtFilter.Text = string.Empty;
                            }
                            else if (mColumnFilterList[iFiltro.Key].GetType().ToString().Contains("ComboBoxColumnFilter"))
                            {
                                ((ComboBoxColumnFilter)mColumnFilterList[iFiltro.Key]).cmbFilter.SelectedIndex = -1;
                            }
                            else if (mColumnFilterList[iFiltro.Key].GetType().ToString().Contains("RangeDateColumnFilter"))
                            {
                                ((RangeDateColumnFilter)mColumnFilterList[iFiltro.Key]).dtpInicio.Text = string.Empty;
                                ((RangeDateColumnFilter)mColumnFilterList[iFiltro.Key]).dtpFin.Text = string.Empty;
                            }
                            else if (mColumnFilterList[iFiltro.Key].GetType().ToString().Contains("RangeValueColumnFilter"))
                            {
                                ((RangeValueColumnFilter)mColumnFilterList[iFiltro.Key]).txtInicial.Text = string.Empty;
                                ((RangeValueColumnFilter)mColumnFilterList[iFiltro.Key]).txtFinal.Text = string.Empty;
                            }
                        }
                        OcultarIconoFiltroHeader(iFiltro.Key, true);
                    }
                    
                }

                //mColumnFilterList = new Dictionary<string, BaseColumnFilter>();
                if (mPopup != null)
                {
                    mPopup.Visibility = Visibility.Hidden;
                    ColumnFilterEventArgs cmR = new ColumnFilterEventArgs(CrearFiltros());
                    mPopup.Visibility = Visibility.Hidden;
                    if (ColumnFilterExec != null) ColumnFilterExec(null, cmR);
                }
            }

            private List<ColumnFilters> CrearFiltros() 
            {
                List<ColumnFilters> lstColumnFilters = new List<ColumnFilters>();

                foreach (KeyValuePair<string, BaseColumnFilter> iFiltro in mColumnFilterList)
                {
                    if (iFiltro.Value != null) 
                    {
                        if (iFiltro.Value.FilterHost != null)
                        {

                            ColumnFilters oColumnFilters = new ColumnFilters();
                            oColumnFilters.FilterExpression = iFiltro.Value.FilterHost.FilterExpression;
                            oColumnFilters.IndexColumn = iFiltro.Value.FilterHost.IndexColumn;
                            oColumnFilters.ColumnName = iFiltro.Value.FilterHost.ColumnName;
                            oColumnFilters.FieldName = iFiltro.Value.FilterHost.FieldName;
                            lstColumnFilters.Add(oColumnFilters);

                        }
                    }
                }

              
                return lstColumnFilters;
            }
        

            void WPFFilterPopupManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                BaseColumnFilter iBaseColumnFilter = ((BaseColumnFilter)sender);
                string sClave = (iBaseColumnFilter.FilterHost.FieldName == string.Empty) ? iBaseColumnFilter.FilterHost.IndexColumn.ToString() : iBaseColumnFilter.FilterHost.FieldName;

                switch (e.PropertyName)
                {
                    case "Filtro":

                        if (mColumnFilterList.ContainsKey(sClave))
                        {
                            string sNombreColumna=((BaseColumnFilter)sender).GridColumnHeader.Content.ToString();
                            string sColumnFilter = ((BaseColumnFilter)sender).Filtro;
                            mColumnFilterList[sClave].FilterHost.FilterExpression = sColumnFilter;
                            mColumnFilterList[sClave].FilterHost.ColumnName = sNombreColumna;
                        }
                        
                      

                        break;

                    case "ActivarFiltro":

                          ColumnFilterEventArgs cme = new ColumnFilterEventArgs(CrearFiltros());
                          mPopup.Visibility = Visibility.Hidden;

                          if (mColumnFilterList[sClave].FilterHost.FilterExpression != null)
                          {
                              if (mColumnFilterList[sClave].FilterHost.FilterExpression != string.Empty) 
                              {
                                  //Visualiza imagen filtro en el header de la columna
                                  OcultarIconoFiltroHeader(sClave, false);
                              }

                             
                          }

                          if (ColumnFilterExec != null) ColumnFilterExec(null, cme);
                          
                          break;
                    default:
                        break;
                }

           
            }

            void mPopup_Closed(object sender, RoutedEventArgs e)
            {
               
            }

            private void CreateColumnFilter(DataGridColumn c)
            {
                //if (c.DisplayIndex != -1)
                //{
                   
                    //Raise the event about column filter creation
                    ColumnFilterEventArgs e = new ColumnFilterEventArgs(c, null);
                    if (ColumnFilterAdding != null) ColumnFilterAdding(this, e);
                    if (c.DisplayIndex <= mColumnFilterList.Count)
                    {
                        string sClave = (c.SortMemberPath == string.Empty) ? c.DisplayIndex.ToString() : c.SortMemberPath;
                        if (e.ColumnFilter != null)
                        {
                            mColumnFilterList[sClave]=e.ColumnFilter;
                        }
                    }
                //}

            }

            private DataGridColumnHeadersPresenter GetColumnHeadersPresenter(DataGrid dataGrid)
            {
                DataGridColumnHeadersPresenter chp = null;
                Control sv = dataGrid.Template.FindName("DG_ScrollViewer", dataGrid) as Control;
                if (sv != null)
                {
                    chp = sv.Template.FindName("PART_ColumnHeadersPresenter", sv) as DataGridColumnHeadersPresenter;
                }
                return chp;
            }

            private DataGridColumnHeader GetColumnHeader(int index, DataGrid dataGrid)
            {
                DataGridColumnHeadersPresenter presenter = GetColumnHeadersPresenter(dataGrid);


                if (presenter != null)
                {
                    return (DataGridColumnHeader)presenter.ItemContainerGenerator.ContainerFromIndex(index);
                }
                return null;
            }

        #endregion

        #region Dispose

            public void Dispose()
            {
                Dispose(true);

                GC.SuppressFinalize(this);
            }

            private void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                 
                    }
                    disposed = true;
                }
            }

        #endregion

    }
}
