using GameAssistant.Services;
using System.Windows.Media;

namespace GameAssistant.Models
{
    /// <summary>
    /// KeyDetector properties that able to serialize.
    /// </summary>
    internal class KeyDetectorModel : WidgetModelBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public KeyDetectorModel()
        {
            AnimationMemberDepose += ForegroundAnimatedBrush.BrushAnimationManager.AnimationMemberDepose;
            AnimationMemberDepose += DetectPanelAnimatedBrush.BrushAnimationManager.AnimationMemberDepose;
        }

        #region Serialize properties

        private string _fontFamily = "Century Gothic";
        /// <summary>
        /// Detect panel's font family.
        /// </summary>
        public string FontFamily
        {
            get => _fontFamily;
            set => SetProperty(ref _fontFamily, value);
        }

        private double _fontSize = 32;
        /// <summary>
        /// Detect panel's font size.
        /// </summary>
        public double FontSize
        {
            get => _fontSize;
            set => SetProperty(ref _fontSize, value);
        }

        //todo dodać enumy
        private double[] _detectPanelOpacity = new double[8] { 0.75, 0.75, 0.75, 0.75, 0.75, 0.75, 0.75, 0.75 };
        /// <summary>
        /// Detect panel's opacity.
        /// </summary>
        public double DetectPanelOpacity
        {
            get => _detectPanelOpacity[0];
            set => SetProperty(ref _detectPanelOpacity[0], value);
        }
        public double DetectPanelOpacityZ
        {
            get => _detectPanelOpacity[1];
            set => SetProperty(ref _detectPanelOpacity[1], value);
        }
        public double DetectPanelOpacityX
        {
            get => _detectPanelOpacity[2];
            set => SetProperty(ref _detectPanelOpacity[2], value);
        }
        public double DetectPanelOpacityW
        {
            get => _detectPanelOpacity[3];
            set => SetProperty(ref _detectPanelOpacity[3], value);
        }
        public double DetectPanelOpacityA
        {
            get => _detectPanelOpacity[4];
            set => SetProperty(ref _detectPanelOpacity[4], value);
        }
        public double DetectPanelOpacityS
        {
            get => _detectPanelOpacity[5];
            set => SetProperty(ref _detectPanelOpacity[5], value);
        }
        public double DetectPanelOpacityD
        {
            get => _detectPanelOpacity[6];
            set => SetProperty(ref _detectPanelOpacity[6], value);
        }
        public double DetectPanelOpacitySpace
        {
            get => _detectPanelOpacity[7];
            set => SetProperty(ref _detectPanelOpacity[7], value);
        }

        private AnimatedBrush _foregroundAnimatedBrush = new AnimatedBrush(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)));
        /// <summary>
        /// Detect panel font's animated brushContainer.
        /// </summary>
        public AnimatedBrush ForegroundAnimatedBrush
        {
            get => _foregroundAnimatedBrush;
            set => SetProperty(ref _foregroundAnimatedBrush, value);
        }

        private AnimatedBrush _detectPanelAnimatedBrush = new AnimatedBrush(new SolidColorBrush(Colors.Cyan));
        /// <summary>
        /// Detect panel's animated brushContainer.
        /// </summary>
        public AnimatedBrush DetectPanelAnimatedBrush
        {
            get => _detectPanelAnimatedBrush;
            set => SetProperty(ref _detectPanelAnimatedBrush, value);
        }

        #endregion
    }
}
