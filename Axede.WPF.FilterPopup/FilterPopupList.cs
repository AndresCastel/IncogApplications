using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axede.WPF.FilterPopup
{
    public class FilterPopupList
    {
        private int _IdValor;
        private string _Valor;

        public int IdValor
        {
            get { return _IdValor; }
            set { _IdValor = value; }
        }
        public string Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }
    }
}
