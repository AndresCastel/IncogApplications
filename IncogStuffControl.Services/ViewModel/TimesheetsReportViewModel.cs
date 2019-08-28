using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncogStuffControl.Services.ViewModel
{
    public class TimesheetsReportViewModel
    {
        public int Id { get; set; }
        public string Payroll { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int Break { get; set; }
        public double TotalHours
        {
            get
            {
                return CalculateDiff();
            }
        }

        private double CalculateDiff()
        {
            TimeSpan SignIn = TimeSpan.ParseExact(StartTime, "hhmm", null);
            TimeSpan SignOff = TimeSpan.ParseExact(StartTime, "hhmm", null);
            TimeSpan diff = SignOff - SignIn;
            double hours = diff.TotalHours - (double)(Break*100/60)/100;
            return hours;
        }
    }
}
