using GameAssistant.Models;
using GameAssistant.Services;

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
            DragWindowEvent += () => WidgetManager.SaveWidgetConfigurationInFile<BrowserWidget, BrowserModel>(this);
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
    }
}
