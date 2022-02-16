using NewGameAssistant.Services;
using NewGameAssistant.WidgetModels;
using NewGameAssistant.Widgets;
using System.Windows;

namespace NewGameAssistant
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Widgets:
        ClockWidget someClockWidget;
        PictureWidget somePictureWidget;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            WidgetMenager<ClockWidget, ClockModel>.SaveWidgetConfigurationInFile(someClockWidget);
            WidgetMenager<PictureWidget, PictureModel>.SaveWidgetConfigurationInFile(somePictureWidget);
        }

        private void DownloadConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            if (someClockWidget != null && WidgetMenager<ClockWidget, ClockModel>.DownloadWidgetConfigurationFromFile(out ClockModel cm))
                someClockWidget.DataContext = cm;
            else
                MessageBox.Show("Not found clock widget's configuration file or widget has been null.", "Failed download configuration");

            if (somePictureWidget != null && WidgetMenager<PictureWidget, PictureModel>.DownloadWidgetConfigurationFromFile(out PictureModel pm))
                somePictureWidget.DataContext = pm;
            else
                MessageBox.Show("Not found picture widget's configuration file or widget has been null.", "Failed download configuration");

        }

        private void RebuildWidgetsButton_Click(object sender, RoutedEventArgs e)
        {
            CloseWidgetsButton_Click(sender, e);

            someClockWidget = new ClockWidget();
            somePictureWidget = new PictureWidget();

            someClockWidget.Show();
            somePictureWidget.Show();
        }

        private void CloseWidgetsButton_Click(object sender, RoutedEventArgs e)
        {
            someClockWidget?.Close();
            someClockWidget = null;
            somePictureWidget?.Close();
            somePictureWidget = null;
        }
    }
}
