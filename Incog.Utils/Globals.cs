using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incog.Utils
{
    public class Globals
    {
        /// <summary>
        /// Número de registros por página
        /// </summary>
        public static int iRegistrosPagina
        {
            get { return 10; }
        }

        /// <summary>
        /// Establece si está habilitado el logging de la aplicación
        /// </summary>
        public static bool bEnabledTracking
        {
            get
            {
                string sEnabledTracking = "";
                if (!string.IsNullOrEmpty(sEnabledTracking))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Número de registros por páginaPop
        /// </summary>
        public static int iRegistrosPaginaPopUp
        {
            get { return 5; }
        }

        /// <summary>
        /// Número de registros por página
        /// </summary>
        public static int iRegistrosPaginaWeb
        {
            get { return 10; }
        }

        /// <summary>
        /// Número de registros por página
        /// </summary>
        public static int iRegistrosPaginaSmall
        {
            get { return 5; }
        }
        /// <summary>
        /// Número de registros por páginaPop
        /// </summary>
        //public static string BaseUrl
        //{
        //    get { return "https://localhost:44390"; }
        //}

        public static string BaseUrl
        {
            get { return "http://incognitusbackapi-test.ap-southeast-2.elasticbeanstalk.com/"; }
        }
        public static string BaseUrlReports
        {
            get { return "https://incognitusappscan.s3.us-east-2.amazonaws.com"; }
        }
    }
}
