using System.Windows.Media;

namespace GameAssistant.Models
{
    /// <summary>
    /// Calculator properties set that is able to serialize.
    /// </summary>
    internal class CalculatorModel : WidgetModelBase
    {
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

        private Brush _buttonsForeground = new SolidColorBrush(Colors.Black);
        /// <summary>
        /// Buttons foreground brush (color).
        /// </summary>
        public Brush ButtonsForeground
        {
            get => _buttonsForeground;
            set => SetProperty(ref _buttonsForeground, value);
        }

        // Buttons background:

        private Brush _buttonsBackground = new SolidColorBrush(Colors.DarkGray);
        /// <summary>
        /// Buttons background brush (color).
        /// </summary>
        public Brush ButtonsBackground
        {
            get => _buttonsBackground;
            set => SetProperty(ref _buttonsBackground, value);
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

        private Brush _textBoxForeground = new SolidColorBrush(Colors.Black);
        /// <summary>
        /// TextBox foreground brush (color).
        /// </summary>
        public Brush TextBoxForeground
        {
            get => _textBoxForeground;
            set => SetProperty(ref _textBoxForeground, value);
        }

        // TextBox background:

        private Brush _textBoxBackground = new SolidColorBrush(Colors.White);
        /// <summary>
        /// TextBox background brush (color).
        /// </summary>
        public Brush TextBoxBackground
        {
            get => _textBoxBackground;
            set => SetProperty(ref _textBoxBackground, value);
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
