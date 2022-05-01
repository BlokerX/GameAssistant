using GameAssistant.Models;
using GameAssistant.Services;
using System;

namespace GameAssistant.Widgets
{
    /// <summary>
    /// Logika interakcji dla klasy CalculatorWidget.xaml
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
            //WebBrowserControl.Source = new Uri("https://www.google.pl/");
        }

        //private string _browserAddress = "http://google.com";
        //public string BrowserAddress
        //{
        //    get => _browserAddress;
        //    set => WebBrowserControl.Source = new Uri(_browserAddress = value);
        //}

        //public static WidgetEvents Events = new WidgetEvents();

        //private void WebBrowserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    WebBrowserControl.UpdateWindowPos();
        //}
    }
}
