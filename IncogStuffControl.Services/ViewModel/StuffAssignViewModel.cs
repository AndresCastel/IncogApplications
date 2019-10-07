using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncogStuffControl.Services.ViewModel
{
    public class StuffAssignViewModel : ViewModelBase
    {
        public int StuffId { get; set; }
        public int EmployeeId { get; set; }
        public int Quantity { get; set; }
        public bool Active { get; set; }

        public StuffAssignViewModel()
        {

        }
    }
}
