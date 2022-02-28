using GameAssistant.Models;
using GameAssistant.Pages;
using GameAssistant.Widgets;
using GameAssistant.WidgetViewModels;
using GameAssistant.WindowViewModels;
using System.Windows;
using System.Windows.Media;

namespace GameAssistant.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SettingsWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with widgets.
        /// </summary>
        public SettingsWindow(ref WidgetContainer<ClockWidget> clockWidgetContainer, ref WidgetContainer<PictureWidget> pictureWidgetContainer, ref WidgetContainer<NoteWidget> noteWidgetContainer)
        {
            InitializeComponent();

            ClockWidgetFrame.Content = new ClockSettingsPage(ref clockWidgetContainer); 
        }

        private void ClockWidgetButton_Click(object sender, RoutedEventArgs e)
        {
            //PictureWidgetPage.Visibility = Visibility.Hidden;
            //ClockWidgetPage.Visibility = Visibility.Visible;
        }

        private void PictureWidgetButton_Click(object sender, RoutedEventArgs e)
        {
            //ClockWidgetPage.Visibility = Visibility.Hidden;
            //PictureWidgetPage.Visibility = Visibility.Visible;
        }

        private void NoteWidgetButton_Click(object sender, RoutedEventArgs e)
        {
            //ClockWidgetPage.Visibility = Visibility.Hidden;
            //PictureWidgetPage.Visibility = Visibility.Hidden;
        }
    }
}
