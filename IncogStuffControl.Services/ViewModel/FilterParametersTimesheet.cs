using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncogStuffControl.Services.ViewModel
{
    public class FilterParametersTimesheet
    {
        public string Employee { get; set; }
        public string Payroll { get; set; }
        public string filter { get; set; }
        public DateTime DateGridFilter { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
