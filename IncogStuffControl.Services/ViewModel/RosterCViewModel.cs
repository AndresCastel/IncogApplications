using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncogStuffControl.Services.ViewModel
{
    public class RosterCViewModel : ViewModelBase
    {
        public int Day { get; set; }        
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int Break { get; set; }
        public string Precint { get; set; }
        public string Zone { get; set; }
        public string Area { get; set; }
        public int ShiftNum { get; set; }
        public string LabourType { get; set; }
        public string Employee { get; set; }
        public string Payroll { get; set; }
        public string EventName { get; set; }
        public bool LookedIn { get; set; }
        public bool Confirm { get; set; }
        //public string DateShort
        //{
        //    get { return Date.ToShortDateString(); }
        //}
    }
}
