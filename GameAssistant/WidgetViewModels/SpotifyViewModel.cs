using GameAssistant.Core;
using GameAssistant.Models;
using System.Windows.Media;

namespace GameAssistant.WidgetViewModels
{
    /// <summary>
    /// View model that contains bindings for SpotifyWidget.
    /// </summary>
    internal class SpotifyViewModel : BindableObject, IWidgetViewModel<SpotifyModel>
    {
        private SpotifyModel _widgetModel = new SpotifyModel();
        /// <summary>
        /// Spotify widget properties.
        /// </summary>
        public SpotifyModel WidgetModel
        {
            get => _widgetModel;
            set => SetProperty(ref _widgetModel, value);
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SpotifyViewModel()
        {
            LoadModel();
        }

        public void LoadModel()
        {
            // Set title:
            WidgetModel.Title = "Spotify widget";

            // Set widget size:
            WidgetModel.Width = 360;
            WidgetModel.Height = 420;

            // Set widget position:
            WidgetModel.ScreenPositionX += 820;

            // Set widget background color:
            WidgetModel.BackgroundAnimatedBrush.BrushContainer.Variable = new SolidColorBrush(Colors.Green);
            WidgetModel.BackgroundOpacity = 0.5;
        }
    }
}
