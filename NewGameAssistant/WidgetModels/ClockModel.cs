using System;
using System.Timers;
using System.Windows.Media;

namespace NewGameAssistant.WidgetModels
{
    /// <summary>
    /// View model that contains bindings for ClockWidget.
    /// </summary>
    internal class ClockModel : WidgetModelBase
    {
        /// <summary>
        /// Clock change system.
        /// </summary>
        private readonly Timer ClockTimer = new Timer(0.01);

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ClockModel()
        {
            // Set title:
            Title = "Clock widget";

            // Set widget size:
            Width = 200;
            Height = 60;

            // Set widget background color:
            BackgroundColor = new SolidColorBrush(Colors.Yellow);

            // Clock timer configuration:
            ClockTimer.Elapsed += (sender, e) => ClockTime = DateTime.Now.ToString("HH:mm:ss");

            // Run clock:
            ClockTimer.Start();
        }

        private string _clockTime = DateTime.Now.ToString("HH:mm:ss");
        /// <summary>
        /// Time that is shown in the clock widget.
        /// </summary>
        public string ClockTime
        {
            get => _clockTime;
            set
            {
                SetProperty(ref _clockTime, value);
            }
        }

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

        private Brush _clockLabelForeground = new SolidColorBrush(Colors.Navy);
        /// <summary>
        /// Clock label's foreground brush (color).
        /// </summary>
        public Brush ClockLabelForeground
        {
            get => _clockLabelForeground;
            set => SetProperty(ref _clockLabelForeground, value);
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

    }
}
