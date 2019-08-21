using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Incog.wpf.Messages
{

  

    /// <summary>
    /// Coleccion (array) de Clases Message para permitir la configuracipón de los mensajes 
    /// usado en el control
    /// </summary>
    [XmlRoot(ElementName = "ConfigurationMessages", Namespace = "")]
    public class ConfigurationMessages : List<Message>
    {
        public ConfigurationMessages()
        {

        }
    }
}
