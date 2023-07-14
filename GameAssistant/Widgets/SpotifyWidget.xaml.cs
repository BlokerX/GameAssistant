using CefSharp;
using GameAssistant.Models;
using GameAssistant.Services;
using GameAssistant.WidgetViewModels;

namespace GameAssistant.Widgets
{
    /// <summary>
    /// Logika interakcji dla klasy SpotifyWidget.xaml
    /// </summary>
    public partial class SpotifyWidget : WidgetBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SpotifyWidget()
        {
            InitializeComponent();
            WindowSizeOrPositionChangedEvent += () => WidgetManager.SaveWidgetConfigurationInFile<SpotifyWidget, SpotifyModel>(this);
        }

        public static WidgetEvents Events = new WidgetEvents();

        private void browserWindow_AddressChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            browserWindow.SetZoomLevel((DataContext as SpotifyViewModel).WidgetModel.ZoomLevel);
        }

        //todo SpotifyWidget : hide bar button
    }
}
