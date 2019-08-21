﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncogStuffControl.Services.ViewModel
{
    public class EmployeeViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Barcode { get; set; }
        public string Email { get; set; }
        public int RolId { get; set; }
        public bool Active { get; set; }
    }
}
