namespace GameAssistant.Models
{
    /// <summary>
    /// Browser properties that able to serialize.
    /// </summary>
    internal class BrowserModel : WidgetModelBase
    {
        #region Serialize properties

        private string _source = "https://www.google.pl/";
        /// <summary>
        /// The address of page in the browser.
        /// </summary>
        public string Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }

        private double _browserOpacity = 0.75;
        /// <summary>
        /// Browser's opacity.
        /// </summary>
        public double BrowserOpacity
        {
            get => _browserOpacity;
            set => SetProperty(ref _browserOpacity, value);
        }

        #endregion
    }
}
