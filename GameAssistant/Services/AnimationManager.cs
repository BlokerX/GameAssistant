using GameAssistant.Core;
using GameAssistant.Models;
using GameAssistant.Services.Animations;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Media;

namespace GameAssistant.Services
{
    /// <summary>
    /// Animation manager with brushContainer and animation controler.
    /// </summary>
    public class AnimationManager
    {
        /// <summary>
        /// Brush to animate.
        /// </summary>
        private VariableContainer<Brush> brushContainer;

        private AnimationType _animation;
        /// <summary>
        /// Type of animation.
        /// </summary>
        public AnimationType Animation
        {
            get => _animation;
            set
            {
                if (value != _animation)
                {
                    switch (_animation)
                    {
                        case AnimationType.RGB:
                            RGBAnimation.RemoveMember(ref brushContainer);
                            break;
                        case AnimationType.ReversedRGB:
                            ReservedRGBAnimation.RemoveMember(ref brushContainer);
                            break;
                        case AnimationType.PixelsAverangeOfScreen:
                            AverangePixelsOfScreenAnimation.RemoveMember(ref brushContainer);
                            break;
                    }

                    _animation = value;

                    switch (value)
                    {
                        case AnimationType.RGB:
                            RGBAnimation.AddMember(ref brushContainer);
                            break;
                        case AnimationType.ReversedRGB:
                            ReservedRGBAnimation.AddMember(ref brushContainer);
                            break;
                        case AnimationType.PixelsAverangeOfScreen:
                            AverangePixelsOfScreenAnimation.AddMember(ref brushContainer);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Depose member of animation.
        /// </summary>
        public void AnimationMemberDepose()
        {
            switch (Animation)
            {
                case AnimationType.RGB:
                    RGBAnimation.RemoveMember(ref brushContainer);
                    break;
                case AnimationType.ReversedRGB:
                    ReservedRGBAnimation.RemoveMember(ref brushContainer);
                    break;
                case AnimationType.PixelsAverangeOfScreen:
                    AverangePixelsOfScreenAnimation.RemoveMember(ref brushContainer);
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
            this.brushContainer = brush;
        }

        /// <summary>
        /// Constructor with animation parameter.
        /// </summary>
        /// <param name="brush">Brush container to animate.</param>
        /// <param name="animation">Type of animation.</param>
        public AnimationManager(ref VariableContainer<Brush> brush, AnimationType animation, double animationInterval = 5) : this(ref brush, animationInterval)
        {
            _animation = animation;
        }

        /// <summary>
        /// Selects and triggers animation.
        /// </summary>
        /// <param name="brushContainer">Brush container to animate.</param>
        private void Animate(VariableContainer<Brush> brushContainer)
        {
            this.brushContainer.Variable = brushContainer.Variable;
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
            ReversedRGB = 2,

            /// <summary>
            /// Pixels averange of screen animation.
            /// </summary>
            PixelsAverangeOfScreen = 3
        }

        /// <summary>
        /// Default destructor.
        /// </summary>
        ~AnimationManager()
        {
            RGBAnimation.RemoveMember(ref brushContainer);
        }

        #region Animations

        /// <summary>
        /// RGB animation controler.
        /// </summary>
        private static AnimationBrushRGBController RGBAnimation = new AnimationBrushRGBController();

        /// <summary>
        /// Reversed RGB animation controler.
        /// </summary>
        private static AnimationBrushReversedRGBController ReservedRGBAnimation = new AnimationBrushReversedRGBController();

        /// <summary>
        /// Averange of screen animation controler.
        /// </summary>
        private static AnimationBrushAverangePixelsOfScreenController AverangePixelsOfScreenAnimation = new AnimationBrushAverangePixelsOfScreenController();

        public static bool DownloadAnimationConfiguation()
        {
            if (!Directory.Exists(AppFileSystem.GetAnimationsConfigurationDirePath()))
            {
                Directory.CreateDirectory(AppFileSystem.GetAnimationsConfigurationDirePath()); ;
            }
            //todo przetestować ustawienia animacji w praktyce
            if (File.Exists(AppFileSystem.GetAnimationsConfigurationFilePath()))
            {
                using(var sr = File.OpenText(AppFileSystem.GetAnimationsConfigurationFilePath()))
                {
                    var config = JsonConvert.DeserializeObject<AnimationsConfiguration>(sr.ReadToEnd());

                    RGBAnimation.AnimationInterval = config.RGBAnimationInterval;
                    ReservedRGBAnimation.AnimationInterval = config.ReversedRGBAnimationInterval;
                    AverangePixelsOfScreenAnimation.AnimationInterval = config.AverangePixelsOfScreenAnimationInterval;

                    sr.Close();
                }
                return true;

            }
            return false;
        }

        #endregion

    }
}
