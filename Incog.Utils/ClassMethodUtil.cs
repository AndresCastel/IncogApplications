﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Incog.Utils
{
    public class ClassMethodUtil
    {
        public static Window ResolveOwnerWindow()
        {
            Window owner = null;
            if (System.Windows.Application.Current != null)
            {
                foreach (Window w in System.Windows.Application.Current.Windows)
                {
                    if (w.IsActive)
                    {
                        owner = w;
                        break;
                    }
                }
            }
            return owner;
        }
    }
}
