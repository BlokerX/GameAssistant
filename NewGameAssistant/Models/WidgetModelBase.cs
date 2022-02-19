using NewGameAssistant.Core;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Media;

namespace NewGameAssistant.Models
{
    /// <summary>
    /// Base of widget model that contains bindings for Widget.
    /// </summary>
    internal abstract class WidgetModelBase : BindableObject
    {
        // Window const elements:
        private string _title = "Widget";
        [JsonIgnore]
        /// <summary>
        /// The title of widget window.
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private double _width;
        /// <summary>
        /// The width of widget window.
        /// </summary>
        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        private double _height;
        /// <summary>
        /// The height of the widget window.
        /// </summary>
        public double Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        private ResizeMode _resizeMode = ResizeMode.CanResizeWithGrip;
        /// <summary>
        /// Resize mode of window.
        /// </summary>
        public ResizeMode ResizeMode
        {
            get => _resizeMode;
            set => SetProperty(ref _resizeMode, value);
        }

        private bool _isDragActive = true /* todo false */;
        /// <summary>
        /// If true drag move is enable, if false drag move is disable.
        /// </summary>
        public bool IsDragActive
        {
            get => _isDragActive;
            set => SetProperty(ref _isDragActive, value);
        }

        // Variables:
        private double _screenPositionX = 70;
        /// <summary>
        /// Widget's horizontal position.
        /// </summary>
        public double ScreenPositionX
        {
            get => _screenPositionX;
            set => SetProperty(ref _screenPositionX, value);
        }

        private double _screenPositionY = 50;
        /// <summary>
        /// Widget's vertical position.
        /// </summary>
        public double ScreenPositionY
        {
            get => _screenPositionY;
            set => SetProperty(ref _screenPositionY, value);
        }

        // Visual elements:
        private Brush _backgroundColor = new SolidColorBrush(Color.FromRgb(249, 255, 129));
        /// <summary>
        /// Widget's background brush (color).
        /// </summary>
        public Brush BackgroundColor
        {
            get => _backgroundColor;
            set => SetProperty(ref _backgroundColor, value);
        }

        private double _backgroundOpacity = 0.5;
        /// <summary>
        /// Widget background's opacity.
        /// </summary>
        public double BackgroundOpacity
        {
            get => _backgroundOpacity;
            set => SetProperty(ref _backgroundOpacity, value);
        }

        // TODO add color's animations
    }
}
