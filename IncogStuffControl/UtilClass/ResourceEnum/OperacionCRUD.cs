using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncogStuffControl.UtilClass.ResourceEnum
{
    public enum OperacionCRUD
    {
        /// <summary>
        /// Representa Crear.
        /// </summary>
        Nuevo = 0,

        /// <summary>
        /// Representa Modificar.
        /// </summary>
        Modificar = 1,

        /// <summary>
        /// Representa Eliminar.
        /// </summary>
        Eliminar = 2,

        /// <summary>
        /// Representa Eliminar.
        /// </summary>
        Consultar = 3,

        /// <summary>
        /// Representa Crear.
        /// </summary>
        Listar = 4,

        /// <summary>
        /// Representa Cancelar cualquier operacion CRUD
        /// </summary>
        Cancelar = 5

    }
}
