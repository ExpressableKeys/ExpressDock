using ExpressableToolbar.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ExpressableToolbar
{
    public abstract class ExpressElement
    {
        public String ImageSource { get; set; }

        public ExpressElement()
        {
        }
    }
}