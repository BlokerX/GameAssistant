using GameAssistant.Models;
using GameAssistant.Services;
using GameAssistant.Widgets;
using GameAssistant.WidgetViewModels;
using System.Windows;

namespace GameAssistant
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

            if (WidgetManager.DownloadWidgetConfigurationFromFile(out ClockModel cm))
            {
                if (cm.IsActive)
                {
                    someClockWidget = new ClockWidget();
                    (someClockWidget.DataContext as IWidgetViewModel<ClockModel>).WidgetModel = cm;
                }
            }
            else someClockWidget = new ClockWidget();
            someClockWidget?.Show();

            if (WidgetManager.DownloadWidgetConfigurationFromFile(out PictureModel pm))
            {
                if (pm.IsActive)
                {
                    somePictureWidget = new PictureWidget();
                    (somePictureWidget.DataContext as IWidgetViewModel<PictureModel>).WidgetModel = pm;
                }
            }
            else somePictureWidget = new PictureWidget();
            somePictureWidget?.Show();

            if (WidgetManager.DownloadWidgetConfigurationFromFile(out NoteModel nm))
            {
                if (nm.IsActive)
                {
                    someNoteWidget = new NoteWidget();
                    (someNoteWidget.DataContext as IWidgetViewModel<NoteModel>).WidgetModel = nm;
                }
            }
            else someNoteWidget = new NoteWidget();
            someNoteWidget?.Show();
        }

        private void SaveConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            WidgetManager.SaveWidgetConfigurationInFile<ClockWidget, ClockModel>(someClockWidget);
            WidgetManager.SaveWidgetConfigurationInFile<PictureWidget, PictureModel>(somePictureWidget);
            WidgetManager.SaveWidgetConfigurationInFile<NoteWidget, NoteModel>(someNoteWidget);
        }

        private void DownloadConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            if (someClockWidget != null && WidgetManager.DownloadWidgetConfigurationFromFile(out ClockModel cm))
                (someClockWidget.DataContext as IWidgetViewModel<ClockModel>).WidgetModel = cm;
            else
                MessageBox.Show("Not found clock widget's configuration file or widget has been null.", "Failed download configuration");

            if (somePictureWidget != null && WidgetManager.DownloadWidgetConfigurationFromFile(out PictureModel pm))
                (somePictureWidget.DataContext as IWidgetViewModel<PictureModel>).WidgetModel = pm;
            else
                MessageBox.Show("Not found picture widget's configuration file or widget has been null.", "Failed download configuration");

            if (someNoteWidget != null && WidgetManager.DownloadWidgetConfigurationFromFile(out NoteModel nm))
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
