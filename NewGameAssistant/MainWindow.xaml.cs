using NewGameAssistant.Models;
using NewGameAssistant.Services;
using NewGameAssistant.Widgets;
using NewGameAssistant.WidgetViewModels;
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
        NoteWidget someNoteWidget;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            WidgetMenager.SaveWidgetConfigurationInFile<ClockWidget, ClockModel>(someClockWidget);
            WidgetMenager.SaveWidgetConfigurationInFile<PictureWidget, PictureModel>(somePictureWidget);
            WidgetMenager.SaveWidgetConfigurationInFile<NoteWidget, NoteModel>(someNoteWidget);
        }

        private void DownloadConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            if (someClockWidget != null && WidgetMenager.DownloadWidgetConfigurationFromFile(out ClockModel cm))
                (someClockWidget.DataContext as IWidgetViewModel<ClockModel>).WidgetModel = cm;
            else
                MessageBox.Show("Not found clock widget's configuration file or widget has been null.", "Failed download configuration");

            if (somePictureWidget != null && WidgetMenager.DownloadWidgetConfigurationFromFile(out PictureModel pm))
                (somePictureWidget.DataContext as IWidgetViewModel<PictureModel>).WidgetModel = pm;
            else
                MessageBox.Show("Not found picture widget's configuration file or widget has been null.", "Failed download configuration");

            if (someNoteWidget != null && WidgetMenager.DownloadWidgetConfigurationFromFile(out NoteModel nm))
                (someNoteWidget.DataContext as IWidgetViewModel<NoteModel>).WidgetModel = nm;
            else
                MessageBox.Show("Not found note widget's configuration file or widget has been null.", "Failed download configuration");
        }

        private void RebuildWidgetsButton_Click(object sender, RoutedEventArgs e)
        {
            CloseWidgetsButton_Click(sender, e);

            someClockWidget = new ClockWidget();
            somePictureWidget = new PictureWidget();
            someNoteWidget = new NoteWidget();

            someClockWidget.Show();
            somePictureWidget.Show();
            someNoteWidget.Show();
        }

        private void CloseWidgetsButton_Click(object sender, RoutedEventArgs e)
        {
            someClockWidget?.Close();
            someClockWidget = null;

            somePictureWidget?.Close();
            somePictureWidget = null;

            someNoteWidget?.Close();
            someNoteWidget = null;
        }
    }
}
