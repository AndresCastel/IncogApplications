using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Axede.WPF.FilterPopup
{
    public class ColumnFilterEventArgs : EventArgs
    {

        private DataGridColumn mColumn;
        private BaseColumnFilter mColumnFilter;
        private List<ColumnFilters> mColumnFilters;


        public ColumnFilterEventArgs(DataGridColumn Column, BaseColumnFilter ColumnFilter)
        {
            this.mColumn = Column;
            this.mColumnFilter = ColumnFilter;
            this.mColumnFilters = null;
        }

        public ColumnFilterEventArgs(List<ColumnFilters> ColumnFilters)
        {
            this.mColumn = null;
            this.mColumnFilter = null;
            this.mColumnFilters = ColumnFilters;
        }

        private string _FieldName;
        public string FieldName
        {
            get { return mColumn.SortMemberPath; }
        }

        public DataGridColumn Column { get { return mColumn; } }
        public BaseColumnFilter ColumnFilter
        {
            get { return mColumnFilter; }
            set { mColumnFilter = value; }
        }
        public List<ColumnFilters> ColumnFilters
        {
            get { return mColumnFilters; }
            set { mColumnFilters = value; }
        }

    }
}
