using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncogStuffControl.Services.ViewModel
{
    public class FilterParametersRoster
    {
        public int Day { get; set; }        
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Employee { get; set; }
        public string Payroll { get; set; }
        public string filter { get; set; }
    }
}
