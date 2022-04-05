namespace GameAssistant.Models
{
    /// <summary>
    /// Note properties that able to serialize.
    /// </summary>
    internal class PictureModel : WidgetModelBase
    {

        #region Serialize properties

        private string _imageSource;
        /// <summary>
        /// The source of widget's image.
        /// </summary>
        public string ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        private double _imageOpacity = 1;
        /// <summary>
        /// Image's opacity.
        /// </summary>
        public double ImageOpacity
        {
            get => _imageOpacity;
            set => SetProperty(ref _imageOpacity, value);
        }

        #endregion
    }
}
