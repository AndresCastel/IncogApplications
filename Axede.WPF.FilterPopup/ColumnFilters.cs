using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axede.WPF.FilterPopup
{
    public class ColumnFilters
    {
        private int _IndexColumn = 0;
        public int IndexColumn
        {
            get { return _IndexColumn; }
            set { _IndexColumn = value; }
        }

        private string _ColumnName = string.Empty;
        public string ColumnName
        {
            get { return _ColumnName; }
            set { _ColumnName = value; }
        }

        private string _FieldName = string.Empty;
        public string FieldName
        {
            get { return _FieldName; }
            set { _FieldName = value; }
        }


        private string _FilterExpression;
        public string FilterExpression
        {
            get { return _FilterExpression; }
            set 
            {
                if (value == null) value = string.Empty;
                _FilterExpression = value; 
            }
        }
    }
}
