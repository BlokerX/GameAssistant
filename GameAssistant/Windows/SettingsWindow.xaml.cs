using GameAssistant.Pages;
using GameAssistant.Widgets;
using System.Windows;
using System.Windows.Controls;

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

        /// <summary>
        /// Set visibility to hidden for all pages.
        /// </summary>
        private void HideAllPages()
        {
            ClockWidgetFrame.Visibility = Visibility.Collapsed;
            PictureWidgetFrame.Visibility = Visibility.Collapsed;
            NoteWidgetFrame.Visibility = Visibility.Collapsed;
            AboutFrame.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Light up selected button and turn off it for other.
        /// </summary>
        /// <param name="senderButton">Selected button (to light up).</param>
        private void CheckMenuButton(Button senderButton)
        {
            ClockWidgetButton.BorderThickness = new Thickness(0);
            PictureWidgetButton.BorderThickness = new Thickness(0);
            NoteWidgetButton.BorderThickness = new Thickness(0);
            AboutButton.BorderThickness = new Thickness(0);

            senderButton.BorderThickness = new Thickness(1);
        }

        /// <summary>
        /// On clock widget button click.
        /// </summary>
        private void ClockWidgetButton_Click(object sender, RoutedEventArgs e)
        {
            HideAllPages();
            ClockWidgetFrame.Visibility = Visibility.Visible;
            CheckMenuButton(sender as Button);
        }

        /// <summary>
        /// On picture widget button click.
        /// </summary>
        private void PictureWidgetButton_Click(object sender, RoutedEventArgs e)
        {
            HideAllPages();
            PictureWidgetFrame.Visibility = Visibility.Visible;
            CheckMenuButton(sender as Button);
        }

        /// <summary>
        /// On widget button click.
        /// </summary>
        private void NoteWidgetButton_Click(object sender, RoutedEventArgs e)
        {
            HideAllPages();
            NoteWidgetFrame.Visibility = Visibility.Visible;
            CheckMenuButton(sender as Button);
        }

        /// <summary>
        /// On about button click.
        /// </summary>
        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            HideAllPages();
            AboutFrame.Visibility = Visibility.Visible;
            CheckMenuButton(sender as Button);
        }
    }
}
