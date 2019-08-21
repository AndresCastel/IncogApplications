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
        public DateTime Sign_In { get; set; }
        public DateTime Sign_Off { get; set; }
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
            TimeSpan diff = Sign_Off - Sign_In;
            double hours = diff.TotalHours - (double)(Break*100/60)/100;
            return hours;
        }
    }
}
