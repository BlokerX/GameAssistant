using GameAssistant.Models;
using GameAssistant.Services;
using GameAssistant.WidgetViewModels;

namespace GameAssistant.Widgets
{
    /// <summary>
    /// Logika interakcji dla klasy KeyDetectorWidget.xaml
    /// </summary>
    public partial class KeyDetectorWidget : WidgetBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public KeyDetectorWidget()
        {
            InitializeComponent();
            WindowSizeOrPositionChangedEvent += () => WidgetManager.SaveWidgetConfigurationInFile<KeyDetectorWidget, KeyDetectorModel>(this);
        }

        public static WidgetEvents Events = new WidgetEvents();

        private void WidgetBase_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            (this.DataContext as KeyDetectorViewModel)?.Closing();
        }
    }
}
