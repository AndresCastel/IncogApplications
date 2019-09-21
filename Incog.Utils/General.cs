using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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

        /// <summary>
        /// Verifica si el archivo está en uso
        /// </summary>
        /// <param name="sRuta"></param>
        /// <param name="sNombreArchivo"></param>
        /// <returns></returns>
        public static bool VerificaArchivoEnUso(string sRuta, string sNombreArchivo)
        {
            try
            {
                using (var stream = new FileStream(sRuta + "\\" + sNombreArchivo, FileMode.Open, FileAccess.Read)) { }
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Convierte un la Lista em un ObservableCollection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coll"></param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(List<T> coll, ObservableCollection<T> c)
        {
            c.Clear();
            foreach (var e in coll) c.Add(e);
            return c;
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

        public static DataTable ConvertToDataTableTimesheets<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                if (prop.Name == "DateShort" || prop.Name == "StartTime" || prop.Name == "EndTime" || prop.Name == "Break" || prop.Name == "Hours"
                    || prop.Name == "LabourType" || prop.Name == "Employee" || prop.Name == "Payroll" || prop.Name == "Precint"
                    || prop.Name == "Zone" || prop.Name == "Area" || prop.Name == "LookedIn" || prop.Name == "EventName") {
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    if (prop.Name == "DateShort" || prop.Name == "StartTime" || prop.Name == "EndTime" || prop.Name == "Break" || prop.Name == "Hours"
                   || prop.Name == "LabourType" || prop.Name == "Employee" || prop.Name == "Payroll" || prop.Name == "Precint"
                   || prop.Name == "Zone" || prop.Name == "Area" || prop.Name == "LookedIn" || prop.Name == "EventName")
                    {
                            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                       
                    }
                table.Rows.Add(row);
            }
            return table;

        }

        public static DataTable ConvertToDataTableRoster<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
               
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
               
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)                   
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

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
