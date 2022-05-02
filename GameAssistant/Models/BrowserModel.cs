namespace GameAssistant.Models
{
    /// <summary>
    /// Browser properties that able to serialize.
    /// </summary>
    internal class BrowserModel : WidgetModelBase
    {
        #region Serialize properties

        private string _address = "https://www.google.com/";
        /// <summary>
        /// The address of page in the browser.
        /// </summary>
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
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
