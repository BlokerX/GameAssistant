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
            if (DataContext is BrowserViewModel vm && vm.WidgetModel != null)
                vm.WidgetModel.Address = "https://google.com";
        }

        private void HideBarButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is BrowserViewModel vm && vm.WidgetModel != null)
                vm.WidgetModel.DragAndDropBarVisibility = System.Windows.Visibility.Hidden;
        }

        //todo BrowserWidget : hide bar button
    }
}
