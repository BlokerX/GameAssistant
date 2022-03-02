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
        /// <param name="clockWidgetContainer"></param>
        /// <param name="pictureWidgetContainer"></param>
        /// <param name="noteWidgetContainer"></param>
        public SettingsWindow(ref WidgetContainer<ClockWidget> clockWidgetContainer, ref WidgetContainer<PictureWidget> pictureWidgetContainer, ref WidgetContainer<NoteWidget> noteWidgetContainer)
        {
            InitializeComponent();

            ClockWidgetFrame.Content = new ClockSettingsPage(ref clockWidgetContainer);
            PictureWidgetFrame.Content = new PictureSettingsPage(ref pictureWidgetContainer);
            NoteWidgetFrame.Content = new NoteSettingsPage(ref noteWidgetContainer);
        }


        public SettingsWindow(ref WidgetContainer<ClockWidget> clockWidgetContainer, ref WidgetContainer<PictureWidget> pictureWidgetContainer, ref WidgetContainer<NoteWidget> noteWidgetContainer,
            ref bool? clockWidgetState, ref bool? pictureWidgetState, ref bool? noteWidgetState)
        {
            ClockWidgetFrame.Content = new ClockSettingsPage(ref clockWidgetContainer, ref clockWidgetState);
            PictureWidgetFrame.Content = new PictureSettingsPage(ref pictureWidgetContainer, ref pictureWidgetState);
            NoteWidgetFrame.Content = new NoteSettingsPage(ref noteWidgetContainer, ref noteWidgetState);
        }

        private void ClockWidgetButton_Click(object sender, RoutedEventArgs e)
        {
            ClockWidgetFrame.Visibility = Visibility.Visible;
            PictureWidgetFrame.Visibility = Visibility.Hidden;
            NoteWidgetFrame.Visibility = Visibility.Hidden;
        }

        private void PictureWidgetButton_Click(object sender, RoutedEventArgs e)
        {
            ClockWidgetFrame.Visibility = Visibility.Hidden;
            PictureWidgetFrame.Visibility = Visibility.Visible;
            NoteWidgetFrame.Visibility = Visibility.Hidden;
        }

        private void NoteWidgetButton_Click(object sender, RoutedEventArgs e)
        {
            ClockWidgetFrame.Visibility = Visibility.Hidden;
            PictureWidgetFrame.Visibility = Visibility.Hidden;
            NoteWidgetFrame.Visibility = Visibility.Visible;
        }

    }
}
