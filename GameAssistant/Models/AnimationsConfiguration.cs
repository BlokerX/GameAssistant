using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAssistant.Models
{
    public class AnimationsConfiguration
    {
        public double RGBAnimationInterval;
        public double ReversedRGBAnimationInterval;
        public double AverangePixelsOfScreenAnimationInterval;

        public AnimationsConfiguration(double rGBAnimationInterval, double reversedRGBAnimationInterval, double averangePixelsOfScreenAnimationInterval)
        {
            RGBAnimationInterval = rGBAnimationInterval;
            ReversedRGBAnimationInterval = reversedRGBAnimationInterval;
            AverangePixelsOfScreenAnimationInterval = averangePixelsOfScreenAnimationInterval;
        }
    }
}
