using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Globalization;
using System.Collections;
using System.ComponentModel;
using System.Threading;
using NsExcel = Microsoft.Office.Interop.Excel;
using System.Data;

namespace Incog.Utils
{
    public class ExportarPlano
    {

        private string _NombreArchivo;
        private string _SeparadorCampos;

        public bool ExportarListaArchivoPlano(IList oListaExportar, string oNombreArchivo, string oSeparadorCampos)
        {
            return ExportarListaArchivoPlano(oListaExportar, oNombreArchivo, oSeparadorCampos, null);
        }

        /// <summary>
        /// Exporta la lista a un archivo plano
        /// </summary>
        /// <param name="sSeparador"></param>
        public bool ExportarListaArchivoPlano(IList oListaExportar, string oNombreArchivo, string oSeparadorCampos, BackgroundWorker oBackgroundWorker)
        {

            bool bArchivoGenerado = false;
          

            ExcelUtlity obj = new ExcelUtlity();
           // DataTable dt = General.ConvertToDataTable(oListaExportar);

            //obj.WriteDataTableToExcel(dt, "Person Details", "D:\\testPersonExceldata.xlsx", "Details");

            //MessageBox.Show("Excel created D:\testPersonExceldata.xlsx");


            return bArchivoGenerado;

        }


        /// <summary>
        /// Remueve caracteres especiales y/o Diacriticas
        /// </summary>
        /// <param name="stIn"></param>
        /// <returns></returns>
        private static string RemoveDiacritics(string stIn)
        {
            string stFormD = stIn.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }


        /// <summary>
        /// Escribe en el Archivo
        /// </summary>
        public void EscribeArchivo(string sMensaje)
        {
            try
            {
                TraceHandler.WriteLine(_NombreArchivo, sMensaje, TipoLog.PLANO);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
