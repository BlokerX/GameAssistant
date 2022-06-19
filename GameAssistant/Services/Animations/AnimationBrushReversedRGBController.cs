using System.Windows.Media;

namespace GameAssistant.Services.Animations
{
    /// <summary>
    /// Reversed RGB animation controller.
    /// </summary>
    internal class AnimationBrushReversedRGBController : AnimationControllerBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="animationInterval">Refresh time.</param>
        public AnimationBrushReversedRGBController(double animationInterval = 5) : base(animationInterval) { }

        /// <summary>
        /// Important for Animate() method.
        /// </summary>
        private int animationInformation = 0;

        /// <summary>
        /// Animate as Reversed RGB style.
        /// </summary>
        protected override void Animate()
        {
            var tmpColor = default(Color);
            if (animationTimer.Enabled)
                System.Windows.Application.Current?.Dispatcher.Invoke(() => tmpColor = ((SolidColorBrush)(brush.Variable)).Color);
            const byte jump = 1;

            switch (animationInformation)
            {
                case 0:
                    tmpColor = Color.FromRgb(255, 0, 0);
                    animationInformation = 1;
                    break;

                case 1:
                    if (tmpColor.B < 255)
                    {
                        tmpColor.B += jump;
                    }
                    else animationInformation = 2;
                    break;

                case 2:
                    if (tmpColor.R > 0)
                    {
                        tmpColor.R -= jump;
                    }
                    else animationInformation = 3;
                    break;

                case 3:
                    if (tmpColor.G < 255)
                    {
                        tmpColor.G += jump;
                    }
                    else animationInformation = 4;
                    break;

                case 4:
                    if (tmpColor.B > 0)
                    {
                        tmpColor.B -= jump;
                    }

                    else animationInformation = 5;
                    break;

                case 5:
                    if (tmpColor.R < 255)
                    {
                        tmpColor.R += jump;
                    }
                    else animationInformation = 6;
                    break;

                case 6:
                    if (tmpColor.G > 0)
                    {
                        tmpColor.G -= jump;
                    }
                    else animationInformation = 1;
                    break;

            }

            if (animationTimer.Enabled)
                System.Windows.Application.Current?.Dispatcher.Invoke(() => brush.Variable = new SolidColorBrush(tmpColor));
        }
    }
}
