using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAssistant.Controls
{
    public class AnimationModeSettingProperty : ListBoxSettingProperty
    {
        public AnimationModeSettingProperty()
        {
            Elements = new List<string>()
            {
                "None",
                "RGB",
                "Reversed RGB",
                "Pixels averange of screen"
            };
        }
    }
}
