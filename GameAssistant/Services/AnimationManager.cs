using GameAssistant.Core;
using System.Timers;
using System.Windows.Media;

namespace GameAssistant.Services
{
    public class AnimationManager
    {
        private Timer animationTimer;

        // todo dodać szybkość animacji INTERVAL

        /// <summary>
        /// Brush to animate.
        /// </summary>
        private VariableContainer<Brush> brush;

        private AnimationType _animation;
        /// <summary>
        /// Type of animation.
        /// </summary>
        public AnimationType Animation
        {
            get => _animation;
            set
            {
                _animation = value;
                animationInformation = 0;
            }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="brush">Brush to animate.</param>
        /// <param name="animationInterval">Refresh time.</param>
        public AnimationManager(ref VariableContainer<Brush> brush, double animationInterval = 5)
        {
            this.brush = brush;
            animationTimer = new Timer(animationInterval);
            animationTimer.Elapsed += AnimationTimer_Elapsed;
        }

        private void AnimationTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Animate();
        }

        /// <summary>
        /// Constructor with animation parameter.
        /// </summary>
        /// <param name="brush">Brush to animate.</param>
        /// <param name="animation">Type of animation.</param>
        public AnimationManager(ref VariableContainer<Brush> brush, AnimationType animation, double animationInterval = 5) : this(ref brush, animationInterval)
        {
            Animation = animation;
        }

        /// <summary>
        /// Turn on timer elapsed event.
        /// </summary>
        public void StartAnimate()
        {
            animationTimer?.Start();
        }

        /// <summary>
        /// Turn off timer elapsed event.
        /// </summary>
        public void StopAnimate()
        {
            animationTimer?.Stop();
        }

        /// <summary>
        /// Selects and triggers animation.
        /// </summary>
        private void Animate()
        {
            switch (Animation)
            {
                case AnimationType.RGB:
                    Animate_RGB();
                    break;

                case AnimationType.RGB_Reversed:
                    Animate_RGB_Reversed();
                    break;

                case AnimationType.None:
                default:
                    return;
            }
        }

        private int animationInformation = 0;

        #region Animations methods

        /// <summary>
        /// Animate as RGB style.
        /// </summary>
        private void Animate_RGB()
        {
            var tmpColor = default(Color);
            if (animationTimer.Enabled)
                System.Windows.Application.Current.Dispatcher.Invoke(() => tmpColor = ((SolidColorBrush)(brush.Variable)).Color);
            const byte jump = 1;

            switch (animationInformation)
            {
                case 0:
                    tmpColor = Color.FromRgb(255, 0, 0);
                    animationInformation = 1;
                    break;

                case 1:
                    if (tmpColor.G < 255)
                    {
                        tmpColor.G += jump;
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
                    if (tmpColor.B < 255)
                    {
                        tmpColor.B += jump;
                    }
                    else animationInformation = 4;
                    break;

                case 4:
                    if (tmpColor.G > 0)
                    {
                        tmpColor.G -= jump;
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
                    if (tmpColor.B > 0)
                    {
                        tmpColor.B -= jump;
                    }
                    else animationInformation = 1;
                    break;

            }

            if (animationTimer.Enabled)
                System.Windows.Application.Current.Dispatcher.Invoke(() => brush.Variable = new SolidColorBrush(tmpColor));
        }

        /// <summary>
        /// Animate as RGB_Reversed style.
        /// </summary>
        private void Animate_RGB_Reversed()
        {
            var tmpColor = default(Color);
            if (animationTimer.Enabled)
                System.Windows.Application.Current.Dispatcher.Invoke(() => tmpColor = ((SolidColorBrush)(brush.Variable)).Color);
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
                System.Windows.Application.Current.Dispatcher.Invoke(() => brush.Variable = new SolidColorBrush(tmpColor));
        }

        #endregion

        /// <summary>
        /// Animation type.
        /// </summary>
        public enum AnimationType
        {
            /// <summary>
            /// No animate.
            /// </summary>
            None = 0,

            /// <summary>
            /// Standard RGB animation.
            /// </summary>
            RGB = 1,

            /// <summary>
            /// Reversed RGB animation.
            /// </summary>
            RGB_Reversed = 2
        }

        ~AnimationManager()
        {
            StopAnimate();
        }

    }
}
