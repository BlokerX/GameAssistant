using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace GameAssistant.Services
{
    internal class AnimationManager
    {
        private Timer animationTimer = new Timer(100);

        /// <summary>
        /// Brush to animate.
        /// </summary>
        private readonly SolidBrush solidBrush;

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

                animationTimer.Stop();

                if (value != AnimationType.None)
                    animationTimer.Start();
            }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="solidBrush">Brush to animate.</param>
        AnimationManager(ref SolidBrush solidBrush)
        {
            this.solidBrush = solidBrush;
            animationTimer.Elapsed += (s, e) => Animate();
        }

        /// <summary>
        /// Constructor with animation parameter.
        /// </summary>
        /// <param name="solidBrush">Brush to animate.</param>
        /// <param name="animation">Type of animation.</param>
        AnimationManager(ref SolidBrush solidBrush, AnimationType animation) : this(ref solidBrush)
        {
            Animation = animation;
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

        #region Animations methods

        /// <summary>
        /// Animate as RGB style.
        /// </summary>
        private void Animate_RGB()
        {
            // some code
        }

        /// <summary>
        /// Animate as RGB_Reversed style.
        /// </summary>
        private void Animate_RGB_Reversed()
        {
            // some code
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
    }
}
