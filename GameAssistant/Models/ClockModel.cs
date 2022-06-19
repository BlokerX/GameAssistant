using GameAssistant.Services;
using System.Windows.Media;

namespace GameAssistant.Models
{
    /// <summary>
    /// Clock properties set that is able to serialize.
    /// </summary>
    internal class ClockModel : WidgetModelBase
    {
        // Constructors:
        public ClockModel()
        {
            AnimationMemberDepose += ForegroundAnimatedBrush.BrushAnimationManager.AnimationMemberDepose;
        }

        #region Serialize properties

        private string _fontFamily = "Century Gothic";
        /// <summary>
        /// Clock label's font family.
        /// </summary>
        public string FontFamily
        {
            get => _fontFamily;
            set => SetProperty(ref _fontFamily, value);
        }

        private double _fontSize = 48;
        /// <summary>
        /// Clock label's font size.
        /// </summary>
        public double FontSize
        {
            get => _fontSize;
            set => SetProperty(ref _fontSize, value);
        }

        private double _clockLabelOpacity = 0.75;
        /// <summary>
        /// Clock label's opacity.
        /// </summary>
        public double ClockLabelOpacity
        {
            get => _clockLabelOpacity;
            set => SetProperty(ref _clockLabelOpacity, value);
        }

        private AnimatedBrush _foregroundAnimatedBrush = new AnimatedBrush(new SolidColorBrush(Colors.Navy));
        /// <summary>
        /// Clock label's animated brushContainer.
        /// </summary>
        public AnimatedBrush ForegroundAnimatedBrush
        {
            get => _foregroundAnimatedBrush;
            set => SetProperty(ref _foregroundAnimatedBrush, value);
        }

        #endregion
    }
}
