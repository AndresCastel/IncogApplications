using IncogStuffControl.BusinessClass.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IncogStuffControl.UserControls.Selectors
{
    /// <summary>
    /// Interaction logic for BreakSelector.xaml
    /// </summary>
    public partial class BreakSelector : UserControl
    {
        public BreakSelector()
        {
            InitializeComponent();
            cmbBreaks.ItemsSource = Enum.GetValues(typeof(BreakEnum));
            cmbBreaks.SelectedValue = 0;
        }
    }
}
