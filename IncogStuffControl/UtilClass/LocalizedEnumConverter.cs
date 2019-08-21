using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncogStuffControl.UtilClass
{
    class LocalizedEnumConverter : IncogStuffControl.UtilClass.ResourceEnumConverter
    {
        public LocalizedEnumConverter(Type type)
            : base(type, Properties.Resources.ResourceManager)
        {
        }
    }
}
