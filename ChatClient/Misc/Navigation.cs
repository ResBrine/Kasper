using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChatClient.Misc
{
    internal class Navigation
    {
        public static Frame frame;

        public static void GoBack()
        {
            frame.GoBack();
        }
        public static void Navigate(object content)
        {
            frame.Navigate(content);
        }
    }
}
