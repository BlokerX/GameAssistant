using System.Windows;

namespace GameAssistant.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void ClockWidgetButton_Click(object sender, RoutedEventArgs e)
        {
            ClockWidgetGrid.Visibility = Visibility.Visible;
        }

        private void PictureWidgetButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NoteWidgetButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
