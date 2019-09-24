using IncogStuffControl.UtilControls.ViewModal;
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

namespace IncogStuffControl.UserControls
{
    /// <summary>
    /// Interaction logic for BreakUC.xaml
    /// </summary>
    public partial class BreakUC : UserControl
    {

        public int Break = 0;
        public BreakUC()
        {
            InitializeComponent();
        }

        public BreakUC(int breakdef)
        {
            InitializeComponent();
            Break = breakdef;
            setDefaultBreak(breakdef);
        }

        private void setDefaultBreak(int breakdef)
        {
            switch (breakdef)
            {
                case 0:
                    chkNobreak.IsChecked = true;
                    break;
                case 15:
                    chk15Min.IsChecked = true;
                    break;
                case 30:
                    chk30Min.IsChecked = true;
                    break;
                case 45:
                    chk45Min.IsChecked = true;
                    break;
                case 60:
                    chk60Min.IsChecked = true;
                    break;
                default:
                    break;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ViewWindow_Modal.Cerrar = ViewWindow_Modal.WinBehavior.OK;
            ViewWindow_Modal.CloseModal();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ViewWindow_Modal.CloseModal();
        }

      

        private void Chkrbt_Click(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton)
            {
                RadioButton item = sender as RadioButton;
                if (item.Name == "chkNobreak")
                {
                    Break = 0;
                }
                if (item.Name == "chk15Min")
                {
                    Break = 15;
                }
                if (item.Name == "chk30Min")
                {
                    Break = 30;
                }
                if (item.Name == "chk45Min")
                {
                    Break = 45;
                }
                if (item.Name == "chk60Min")
                {
                    Break = 60;
                }
            }
        }
    }
}
