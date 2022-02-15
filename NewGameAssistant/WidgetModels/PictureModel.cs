using System.Windows.Media;

namespace NewGameAssistant.WidgetModels
{
    /// <summary>
    /// View model that contains bindings for PictureWidget.
    /// </summary>
    internal class PictureModel : WidgetModelBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PictureModel()
        {
            // Set title:
            Title = "Picture widget";

            // Set widget size:
            Width = 400;
            Height = 250;

            // Set widget position:
            ScreenPositionY += 70;

            // Set widget background color:
            BackgroundColor = new SolidColorBrush(Colors.Transparent);
        }

        private string _imageSource;
        /// <summary>
        /// The source of widget's image.
        /// </summary>
        public string ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                SetProperty(ref _imageSource, value);
            }
        }

        private double _imageOpacity = 1;
        /// <summary>
        /// Image's opacity.
        /// </summary>
        public double ImageOpacity
        {
            get { return _imageOpacity; }
            set { _imageOpacity = value; }
        }

    }
}
