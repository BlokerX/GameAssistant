using Newtonsoft.Json;
using System;
using System.Windows;

namespace GameAssistant.Models
{
    /// <summary>
    /// Spotify properties that able to serialize.
    /// </summary>
    internal class SpotifyModel : WidgetModelBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SpotifyModel()
        {
            DragActiveChanged += (b) =>
            {
                try
                {
                    switch (IsDragActive)
                    {
                        case true:
                            DragAndDropBarVisibility = Visibility.Visible;
                            break;

                        case false:
                        default:
                            DragAndDropBarVisibility = Visibility.Hidden;
                            break;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            };
        }

        private Visibility _dragAndDropBarVisibility = Visibility.Hidden;
        [JsonIgnore]
        /// <summary>
        /// Drag and drop bar visibility state.
        /// </summary>
        public Visibility DragAndDropBarVisibility
        {
            get => _dragAndDropBarVisibility;
            set => SetProperty(ref _dragAndDropBarVisibility, value);
        }

        #region Serialize properties

        private string _browserAddress = "https://open.spotify.com";
        /// <summary>
        /// The address of page in the browser.
        /// </summary>
        public string BrowserAddress
        {
            get => _browserAddress;
            set => SetProperty(ref _browserAddress, value);
        }
        
        private double _zoomLevel = -4;
        /// <summary>
        /// The zoom level of the browser in the spotify.
        /// </summary>
        public double ZoomLevel
        {
            get => _zoomLevel;
            set => SetProperty(ref _zoomLevel, value);
        }

        private double _spotifyOpacity = 0.5;
        /// <summary>
        /// Spotify's opacity.
        /// </summary>
        public double SpotifyOpacity
        {
            get => _spotifyOpacity;
            set => SetProperty(ref _spotifyOpacity, value);
        }

        #endregion
    }
}
