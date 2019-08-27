using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Incog.Utils
{
    public class General
    {
        //TODO:Andres Method to convert hours to the system needs
        public static string ConvertDatetoMilitaryHour(DateTime date)
        {
            TimeSpan Time = TimeSpan.ParseExact(date.TimeOfDay.ToString(), "c", null);
            string MilitaryHour;
            MilitaryHour = Time.ToString("hhmm");
            return MilitaryHour;
        }

        public static string RoundUp(TimeSpan dt, TimeSpan d)
        {
            return  General.ConvertDatetoMilitaryHour(new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks));
            
        }

        public static string RoundToNearest(TimeSpan dt, TimeSpan d)
        {
            var delta = dt.Ticks % d.Ticks;
            bool roundUp = delta > d.Ticks / 2;
            var offset = roundUp ? d.Ticks : 0;

            return General.ConvertDatetoMilitaryHour(new DateTime(dt.Ticks + offset - delta));
        }

        public static Window ResolveOwnerWindow()
        {
            Window owner = null;
            if (System.Windows.Application.Current != null)
            {
                foreach (Window w in System.Windows.Application.Current.Windows)
                {
                    if (w.IsActive)
                    {
                        owner = w;
                        break;
                    }
                }
            }
            return owner;
        }

        //Validate if an excel is open
        public static bool FileIsOpen(string Path)
        {
            bool retVal = false;
            try
            {

                using (FileStream stream = new FileStream(Path, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    try
                    {
                        stream.ReadByte();
                    }
                    catch (IOException)
                    {
                        retVal = true;
                    }
                    finally
                    {
                        stream.Close();
                        stream.Dispose();
                    }
                }
            }
            catch (IOException e)
            {
                //file is opened at another location
                retVal = true;
            }
            catch (UnauthorizedAccessException e)
            {
                //Bypass this exception since this is due to the file is being set to read-only
            }

            return retVal;
        }
    }
}
