using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncogStuffControl.Services.ViewModel
{
    public class EmployeeRegisterViewModel : ViewModelBase
    {
        public int EmployeeId { get; set; }
        public EmployeeViewModel Employee { get; set; }
        public int Type_RegisterId { get; set; }
        public bool Active { get; set; }
        public string Day { get; set; }
        public string StartTime { get; set; }
        public string Payroll { get; set; }
        public string EndTime { get; set; }
        public int Break { get; set; }
        public List<StuffAssignViewModel> lstStuffAssig { get; set; }
        public int RosterId { get; set; }
        public RosterCViewModel Roster { get; set; }

        public EmployeeRegisterViewModel()
        {

        }
       
    }
}
