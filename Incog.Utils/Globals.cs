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
        public static string BaseUrl
        {
            get { return "https://localhost:44390"; }
        }
        //public static string BaseUrl
        //{
        //    get { return "http://incognitusbackapi-dev.us-west-2.elasticbeanstalk.com"; }
        //}
    }
}
