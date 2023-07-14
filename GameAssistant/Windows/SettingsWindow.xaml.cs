using GameAssistant.Models;
using GameAssistant.Pages;
using GameAssistant.Services;
using GameAssistant.Widgets;
using GameAssistant.WidgetViewModels;
using System;
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
        /// Event what's close whole app.
        /// </summary>
        public event Action AppClose;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SettingsWindow()
        {
            InitializeComponent();

            Closing += (o, e) =>
            {
                // General:
                (GeneralSettingsFrame.Content as GeneralSettingsPage).RemovePageMethodsFromWidgetsEvents();
                // Widgets:
                (ClockWidgetFrame.Content as WidgetSettingsPage).RemovePageMethodsFromWidgetEvents();
                (PictureWidgetFrame.Content as WidgetSettingsPage).RemovePageMethodsFromWidgetEvents();
                (NoteWidgetFrame.Content as WidgetSettingsPage).RemovePageMethodsFromWidgetEvents();
                (CalculatorWidgetFrame.Content as WidgetSettingsPage).RemovePageMethodsFromWidgetEvents();
                (BrowserWidgetFrame.Content as WidgetSettingsPage).RemovePageMethodsFromWidgetEvents();
                (KeyDetectorWidgetFrame.Content as WidgetSettingsPage).RemovePageMethodsFromWidgetEvents();
                (SpotifyWidgetFrame.Content as WidgetSettingsPage).RemovePageMethodsFromWidgetEvents();
            };
        }

        private readonly AllWidgetsContainer widgetsContainer;

        /// <summary>
        /// Constructor with widgets.
        /// </summary>
        /// <param name="widgetsContainer"></param>
        public SettingsWindow(ref AllWidgetsContainer widgetsContainer) : this()
        {
            this.widgetsContainer = widgetsContainer;

            ClockWidgetFrame.Content = new ClockSettingsPage(ref widgetsContainer.ClockWidgetContainer);
            PictureWidgetFrame.Content = new PictureSettingsPage(ref widgetsContainer.PictureWidgetContainer);
            NoteWidgetFrame.Content = new NoteSettingsPage(ref widgetsContainer.NoteWidgetContainer);
            CalculatorWidgetFrame.Content = new CalculatorSettingsPage(ref widgetsContainer.CalculatorWidgetContainer);
            BrowserWidgetFrame.Content = new BrowserSettingsPage(ref widgetsContainer.BrowserWidgetContainer);
            KeyDetectorWidgetFrame.Content = new KeyDetectorSettingsPage(ref widgetsContainer.KeyDetectorWidgetContainer);
            SpotifyWidgetFrame.Content = new SpotifySettingsPage(ref widgetsContainer.SpotifyWidgetContainer);

            GeneralSettingsFrame.Content = new GeneralSettingsPage(ref widgetsContainer, () =>
            {
                if (MessageBox.Show("Should you set widgets configuration to default?\n(Warning, if you restore the default settings you will not be able to restore the current data.)", "Setting configuration to default:", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                {
                    WidgetManager.ResetWidgetConfigurationToDefault<ClockWidget, ClockViewModel, ClockModel>(ref this.widgetsContainer.ClockWidgetContainer, () => (ClockWidgetFrame.Content as ClockSettingsPage).LoadWidget(ref this.widgetsContainer.ClockWidgetContainer));
                    WidgetManager.ResetWidgetConfigurationToDefault<PictureWidget, PictureViewModel, PictureModel>(ref this.widgetsContainer.PictureWidgetContainer, () => (PictureWidgetFrame.Content as PictureSettingsPage).LoadWidget(ref this.widgetsContainer.PictureWidgetContainer));
                    WidgetManager.ResetWidgetConfigurationToDefault<NoteWidget, NoteViewModel, NoteModel>(ref this.widgetsContainer.NoteWidgetContainer, () => (NoteWidgetFrame.Content as NoteSettingsPage).LoadWidget(ref this.widgetsContainer.NoteWidgetContainer));
                    WidgetManager.ResetWidgetConfigurationToDefault<CalculatorWidget, CalculatorViewModel, CalculatorModel>(ref this.widgetsContainer.CalculatorWidgetContainer, () => (CalculatorWidgetFrame.Content as CalculatorSettingsPage).LoadWidget(ref this.widgetsContainer.CalculatorWidgetContainer));
                    WidgetManager.ResetWidgetConfigurationToDefault<BrowserWidget, BrowserViewModel, BrowserModel>(ref this.widgetsContainer.BrowserWidgetContainer, () => (BrowserWidgetFrame.Content as BrowserSettingsPage).LoadWidget(ref this.widgetsContainer.BrowserWidgetContainer));
                    WidgetManager.ResetWidgetConfigurationToDefault<KeyDetectorWidget, KeyDetectorViewModel, KeyDetectorModel>(ref this.widgetsContainer.KeyDetectorWidgetContainer, () => (KeyDetectorWidgetFrame.Content as KeyDetectorSettingsPage).LoadWidget(ref this.widgetsContainer.KeyDetectorWidgetContainer));
                    WidgetManager.ResetWidgetConfigurationToDefault<SpotifyWidget, SpotifyViewModel, SpotifyModel>(ref this.widgetsContainer.SpotifyWidgetContainer, () => (SpotifyWidgetFrame.Content as SpotifySettingsPage).LoadWidget(ref this.widgetsContainer.SpotifyWidgetContainer));
                }
            });
        }

        /// <summary>
        /// Set visibility to hidden for all pages.
        /// </summary>
        private void HideAllPages()
        {
            GeneralSettingsFrame.Visibility = Visibility.Collapsed;
            ClockWidgetFrame.Visibility = Visibility.Collapsed;
            PictureWidgetFrame.Visibility = Visibility.Collapsed;
            NoteWidgetFrame.Visibility = Visibility.Collapsed;
            CalculatorWidgetFrame.Visibility = Visibility.Collapsed;
            BrowserWidgetFrame.Visibility = Visibility.Collapsed;
            KeyDetectorWidgetFrame.Visibility = Visibility.Collapsed;
            SpotifyWidgetFrame.Visibility = Visibility.Collapsed;
            AboutFrame.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Light up selected button and turn off it for other.
        /// </summary>
        /// <param name="senderButton">Selected button (to light up).</param>
        private void CheckMenuButton(Button senderButton)
        {
            GeneralSettingsButton.BorderThickness = new Thickness(0);
            ClockWidgetButton.BorderThickness = new Thickness(0);
            PictureWidgetButton.BorderThickness = new Thickness(0);
            NoteWidgetButton.BorderThickness = new Thickness(0);
            CalculatorWidgetButton.BorderThickness = new Thickness(0);
            BrowserWidgetButton.BorderThickness = new Thickness(0);
            KeyDetectorWidgetButton.BorderThickness = new Thickness(0);
            SpotifyWidgetButton.BorderThickness = new Thickness(0);
            AboutButton.BorderThickness = new Thickness(0);

            senderButton.BorderThickness = new Thickness(1);
        }

        private void GeneralSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            HideAllPages();
            GeneralSettingsFrame.Visibility = Visibility.Visible;
            CheckMenuButton(sender as Button);
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
        /// On note widget button click.
        /// </summary>
        private void NoteWidgetButton_Click(object sender, RoutedEventArgs e)
        {
            HideAllPages();
            NoteWidgetFrame.Visibility = Visibility.Visible;
            CheckMenuButton(sender as Button);
        }

        /// <summary>
        /// On calculator widget button click.
        /// </summary>
        private void CalculatorWidgetButton_Click(object sender, RoutedEventArgs e)
        {
            HideAllPages();
            CalculatorWidgetFrame.Visibility = Visibility.Visible;
            CheckMenuButton(sender as Button);
        }

        /// <summary>
        /// On browser widget button click.
        /// </summary>
        private void BrowserWidgetButton_Click(object sender, RoutedEventArgs e)
        {
            HideAllPages();
            BrowserWidgetFrame.Visibility = Visibility.Visible;
            CheckMenuButton(sender as Button);
        }

        /// <summary>
        /// On key detector widget button click.
        /// </summary>
        private void KeyDetectorWidgetButton_Click(object sender, RoutedEventArgs e)
        {
            HideAllPages();
            KeyDetectorWidgetFrame.Visibility = Visibility.Visible;
            CheckMenuButton(sender as Button);
        }
        
        /// <summary>
        /// On spotify widget button click.
        /// </summary>
        private void SpotifyWidgetButton_Click(object sender, RoutedEventArgs e)
        {
            HideAllPages();
            SpotifyWidgetFrame.Visibility = Visibility.Visible;
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

        /// <summary>
        /// On close app button clicked.
        /// </summary>
        private void CloseAppButton_Click(object sender, RoutedEventArgs e)
        {
            AppClose.Invoke();
        }

        private void StackPanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ButtonState == System.Windows.Input.MouseButtonState.Pressed)
                DragMove();
        }

        private void WindowActionButton_CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WindowActionButton_WindowStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
            {
                var preOperationWindowStyle = WindowStyle;
                WindowStyle = WindowStyle.SingleBorderWindow;
                WindowState = WindowState.Maximized;
                WindowStyle = preOperationWindowStyle;
            }
        }

        private void WindowActionButton_MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                WindowState = WindowState.Normal;
            else WindowState = WindowState.Minimized;
        }
    }
}
