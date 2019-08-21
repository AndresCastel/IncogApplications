using IncogStuffControl.UtilClass.ResourceEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncogStuffControl.BusinessClass.Enums
{
    [TypeConverter(typeof(LocalizedEnumConverter))]
    enum BreakEnum
    {
        Cero = 0,
        Cuarto = 15,
        Half = 30,
        ThreeQuora =45,
        Hour = 60
    }
}
