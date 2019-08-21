using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncogStuffControl.UtilClass.ResourceEnum
{
    public class LocalizedEnumConverter : ResourceEnumConverter
    {
        public LocalizedEnumConverter(Type type)
            : base(type, Properties.EnumResources.ResourceManager)
        {
        }
    }
}
