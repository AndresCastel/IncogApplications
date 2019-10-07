using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncogStuffControl.Services.ViewModel
{
    public class TimesheetsReportViewModel
    {
        public string Day { get; set; }
        //public string DateShort
        //{
        //    get
        //    {
        //        return Day.ToShortDateString();
        //    }
        //}
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int Break { get; set; }
        public double Hours
        {
            get
            {
                return CalculateDiff();
            }
        }
        public string FullName
        {
            get
            {
                string Full;
                if(string.IsNullOrEmpty(MiddleName))
                {
                    Full= Name + " " + LastName;
                }
                else
                {
                    Full = Name + " " + MiddleName + " " + LastName;
                }
                return Full;
            }
        }
        public string LabourType { get; set; }
        public string Employee { get; set; }
        public string Payroll { get; set; }
        public string Precint { get; set; }
        public string Zone { get; set; }
        public string Area { get; set; }
        public bool LookedIn { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public bool Active { get; set; }
        public int Id { get; set; }
        public string EventName { get; set; }
        public bool Confirm { get; set; }
        

        private double CalculateDiff()
        {
            TimeSpan SignIn = TimeSpan.ParseExact(StartTime, "hhmm", null);
            TimeSpan diff;
            if (!string.IsNullOrEmpty(EndTime))
            {
                TimeSpan SignOff = TimeSpan.ParseExact(EndTime, "hhmm", null);
                if (SignOff >= SignIn)
                {
                    diff = SignOff - SignIn;
                }
                else
                {
                    diff = SignOff - SignIn;
                    diff = diff.Add(new TimeSpan(24,0,0));
                }
            }
            else
            {
                diff = new TimeSpan();
            }

            double hours = diff.TotalHours - (double)(Break * 100 / 60) / 100;
            return hours;
        }
    }
}
