using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IncogStuffControl.UtilControls.ButtonImage
{
    public class ButtonImage : Button
    {

        static ButtonImage()
        {
            //set DefaultStyleKeyProperty
        }

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public object Tag
        {
            get { return (object)GetValue(TagProperty); }
            set { SetValue(TagProperty, value); }
        }


        public static readonly DependencyProperty ImageSourceProperty =
        DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ButtonImage), new UIPropertyMetadata(null));

        public static readonly DependencyProperty TagProperty =
        DependencyProperty.Register("Tag", typeof(object), typeof(ButtonImage), new UIPropertyMetadata(null));


    }
}
