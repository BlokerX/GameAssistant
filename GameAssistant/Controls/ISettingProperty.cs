using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GameAssistant.Controls
{
    internal interface ISettingProperty
    {
        string PropertyName { get; set; }

        Brush ForegroundColor { set; }

        Brush BorderColor { get; set; }

        Brush BackgorundColor { set; }
    }
}
