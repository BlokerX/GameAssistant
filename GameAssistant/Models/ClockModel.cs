using GameAssistant.Core;
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
            AnimationToken_True += () => _foregroundAnimationManager?.StartAnimate();
            AnimationToken_False += () => _foregroundAnimationManager?.StopAnimate();
            _foregroundAnimationManager = new AnimationManager(ref _foregroundColor);
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

        private VariableContainer<Brush> _foregroundColor = new VariableContainer<Brush>(new SolidColorBrush((Colors.Navy)));
        /// <summary>
        /// Container with clock label's brush (Container).
        /// </summary>
        public VariableContainer<Brush> ForegroundColorContainer
        {
            get => _foregroundColor;
            set
            {
                SetProperty(ref _foregroundColor, value);
            }
        }
        /// <summary>
        /// Clock label's foreground brush (color).
        /// </summary>
        public Brush ForegroundColor
        {
            get => _foregroundColor.Variable;
            set
            {
                _foregroundColor.Variable = value;
            }
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

        private AnimationManager _foregroundAnimationManager;
        /// <summary>
        /// Clock label's color animation.
        /// </summary>
        public AnimationManager ForegroundAnimationManager
        {
            get { return _foregroundAnimationManager; }
            set { _foregroundAnimationManager = value; }
        }

        #endregion
    }
}
