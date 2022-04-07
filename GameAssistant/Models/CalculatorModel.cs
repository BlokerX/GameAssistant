using GameAssistant.Core;
using GameAssistant.Services;
using System.Windows.Media;

namespace GameAssistant.Models
{
    /// <summary>
    /// Calculator properties set that is able to serialize.
    /// </summary>
    internal class CalculatorModel : WidgetModelBase
    {
        // Constructors:
        public CalculatorModel()
        {
            AnimationToken_True += () => _buttonsBackgroundAnimationManager?.StartAnimate();
            AnimationToken_False += () => _buttonsBackgroundAnimationManager?.StopAnimate();
            _buttonsBackgroundAnimationManager = new AnimationManager(ref _buttonsBackground);
            
            AnimationToken_True += () => _buttonsForegroundAnimationManager?.StartAnimate();
            AnimationToken_False += () => _buttonsForegroundAnimationManager?.StopAnimate();
            _buttonsForegroundAnimationManager = new AnimationManager(ref _buttonsForeground);
            
            AnimationToken_True += () => _textBoxBackgroundAnimationManager?.StartAnimate();
            AnimationToken_False += () => _textBoxBackgroundAnimationManager?.StopAnimate();
            _textBoxBackgroundAnimationManager = new AnimationManager(ref _textBoxBackground);
            
            AnimationToken_True += () => _textBoxForegroundAnimationManager?.StartAnimate();
            AnimationToken_False += () => _textBoxForegroundAnimationManager?.StopAnimate();
            _textBoxForegroundAnimationManager = new AnimationManager(ref _textBoxForeground);
        }

        #region Serialize properties

        // Buttons foreground:

        private string _buttonsFontFamily = "Century Gothic";
        /// <summary>
        /// Buttons font family.
        /// </summary>
        public string ButtonsFontFamily
        {
            get => _buttonsFontFamily;
            set => SetProperty(ref _buttonsFontFamily, value);
        }

        private double _buttonsFontSize = 20;
        /// <summary>
        /// Buttons font size.
        /// </summary>
        public double ButtonsFontSize
        {
            get => _buttonsFontSize;
            set => SetProperty(ref _buttonsFontSize, value);
        }

        private VariableContainer<Brush> _buttonsForeground = new VariableContainer<Brush>(new SolidColorBrush((Colors.Black)));
        /// <summary>
        /// Container with buttons foreground brush (Container).
        /// </summary>
        public VariableContainer<Brush> ButtonsForegroundContainer
        {
            get => _buttonsForeground;
            set => SetProperty(ref _buttonsForeground, value);
        }

        private AnimationManager _buttonsForegroundAnimationManager;
        /// <summary>
        /// Buttons foreground color animation.
        /// </summary>
        public AnimationManager ButtonsForegroundAnimationManager
        {
            get { return _buttonsForegroundAnimationManager; }
            set { _buttonsForegroundAnimationManager = value; }
        }

        // Buttons background:

        private VariableContainer<Brush> _buttonsBackground = new VariableContainer<Brush>(new SolidColorBrush((Colors.DarkGray)));
        /// <summary>
        /// Container with buttons background brush (Container).
        /// </summary>
        public VariableContainer<Brush> ButtonsBackgroundContainer
        {
            get => _buttonsBackground;
            set => SetProperty(ref _buttonsBackground, value);
        }

        private AnimationManager _buttonsBackgroundAnimationManager;
        /// <summary>
        /// Buttons background color animation.
        /// </summary>
        public AnimationManager ButtonsBackgroundAnimationManager
        {
            get { return _buttonsBackgroundAnimationManager; }
            set { _buttonsBackgroundAnimationManager = value; }
        }

        private double _buttonsOpacity = 0.75;
        /// <summary>
        /// Buttons opacity.
        /// </summary>
        public double ButtonsOpacity
        {
            get => _buttonsOpacity;
            set => SetProperty(ref _buttonsOpacity, value);
        }

        // TextBox foreground:

        private string _textBoxFontFamily = "Century Gothic";
        /// <summary>
        /// TextBox font family.
        /// </summary>
        public string TextBoxFontFamily
        {
            get => _textBoxFontFamily;
            set => SetProperty(ref _textBoxFontFamily, value);
        }

        private double _textBoxFontSize = 28;
        /// <summary>
        /// TextBox font size.
        /// </summary>
        public double TextBoxFontSize
        {
            get => _textBoxFontSize;
            set => SetProperty(ref _textBoxFontSize, value);
        }

        private VariableContainer<Brush> _textBoxForeground = new VariableContainer<Brush>(new SolidColorBrush((Colors.Black)));
        /// <summary>
        /// Container with TextBox foreground brush (Container).
        /// </summary>
        public VariableContainer<Brush> TextBoxForegroundContainer
        {
            get => _textBoxForeground;
            set => SetProperty(ref _textBoxForeground, value);
        }

        private AnimationManager _textBoxForegroundAnimationManager;
        /// <summary>
        /// TextBox foreground color animation.
        /// </summary>
        public AnimationManager TextBoxForegroundAnimationManager
        {
            get { return _textBoxForegroundAnimationManager; }
            set { _textBoxForegroundAnimationManager = value; }
        }

        // TextBox background:

        private VariableContainer<Brush> _textBoxBackground = new VariableContainer<Brush>(new SolidColorBrush((Colors.White)));
        /// <summary>
        /// Container with TextBox background brush (Container).
        /// </summary>
        public VariableContainer<Brush> TextBoxBackgroundContainer
        {
            get => _textBoxBackground;
            set => SetProperty(ref _textBoxBackground, value);
        }

        private AnimationManager _textBoxBackgroundAnimationManager;
        /// <summary>
        /// TextBox background color animation.
        /// </summary>
        public AnimationManager TextBoxBackgroundAnimationManager
        {
            get { return _textBoxBackgroundAnimationManager; }
            set { _textBoxBackgroundAnimationManager = value; }
        }

        private double _textBoxOpacity = 0.75;
        /// <summary>
        /// TextBox opacity.
        /// </summary>
        public double TextBoxOpacity
        {
            get => _textBoxOpacity;
            set => SetProperty(ref _textBoxOpacity, value);
        }

        #endregion
    }
}
