using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncogStuffControl.Services.ViewModel
{
    public class MessageResponseViewModel<T>
    {
        public bool Succesfull { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
