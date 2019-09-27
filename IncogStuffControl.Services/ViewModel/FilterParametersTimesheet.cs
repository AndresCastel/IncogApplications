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
        public string DateGridFilter { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }
}
