using GameAssistant.Core;
using GameAssistant.Services.Animations;
using System.Windows.Media;

namespace GameAssistant.Services
{
    public class AnimationManager
    {
        /// <summary>
        /// Brush to animate.
        /// </summary>
        private VariableContainer<Brush> brush;

        public bool IsAnimationSelectorOn = true;

        private AnimationType _animation;
        /// <summary>
        /// Type of animation.
        /// </summary>
        public AnimationType Animation
        {
            get => _animation;
            set
            {
                if (!IsAnimationSelectorOn)
                {
                    _animation = value;
                    return;
                }

                if (value != _animation)
                {

                    switch (_animation)
                    {
                        case AnimationType.RGB:
                            RGBAnimation.RemoveMember(ref brush);
                            break;
                        case AnimationType.ReversedRGB:
                            ReservedRGBAnimation.RemoveMember(ref brush);
                            break;
                    }

                    _animation = value;

                    switch (value)
                    {
                        case AnimationType.RGB:
                            RGBAnimation.AddMember(ref brush);
                            break;
                        case AnimationType.ReversedRGB:
                            ReservedRGBAnimation.AddMember(ref brush);
                            break;
                    }
                }
            }
        }

        public void AnimationMemberDepose()
        {
            switch (Animation)
            {
                case AnimationType.RGB:
                    RGBAnimation.RemoveMember(ref brush);
                    break;
                case AnimationType.ReversedRGB:
                    ReservedRGBAnimation.RemoveMember(ref brush);
                    break;
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
        }

        /// <summary>
        /// Constructor with animation parameter.
        /// </summary>
        /// <param name="brush">Brush to animate.</param>
        /// <param name="animation">Type of animation.</param>
        public AnimationManager(ref VariableContainer<Brush> brush, AnimationType animation, double animationInterval = 5) : this(ref brush, animationInterval)
        {
            _animation = animation;
        }

        /// <summary>
        /// Selects and triggers animation.
        /// </summary>
        private void Animate(VariableContainer<Brush> b)
        {
            brush.Variable = b.Variable;
        }

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
            ReversedRGB = 2
        }

        ~AnimationManager()
        {
            RGBAnimation.RemoveMember(ref brush);
        }

        private static AnimationBrushRGB RGBAnimation = new AnimationBrushRGB();

        private static AnimationBrushReversedRGB ReservedRGBAnimation = new AnimationBrushReversedRGB();

        //public static void RegisterAnimations()
        //{

        //}

    }
}
