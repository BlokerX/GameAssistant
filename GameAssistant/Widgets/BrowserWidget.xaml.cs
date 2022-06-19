using GameAssistant.Models;
using GameAssistant.Services;
using GameAssistant.WidgetViewModels;

namespace GameAssistant.Widgets
{
    /// <summary>
    /// Logika interakcji dla klasy BrowserWidget.xaml
    /// </summary>
    public partial class BrowserWidget : WidgetBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public BrowserWidget()
        {
            InitializeComponent();
            WindowSizeOrPositionChangedEvent += () => WidgetManager.SaveWidgetConfigurationInFile<BrowserWidget, BrowserModel>(this);
        }

        /// <summary>
        /// When browser's address changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChromiumWebBrowser_AddressChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            WidgetManager.SaveWidgetConfigurationInFile<BrowserWidget, BrowserModel>(this);
        }

        public static WidgetEvents Events = new WidgetEvents();

        private void HomeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var vm = DataContext as BrowserViewModel;
            if (vm != null && vm.WidgetModel != null)
                vm.WidgetModel.Address = "https://google.com";
        }

        //todo BrowserWidget : hide bar button
    }
}
