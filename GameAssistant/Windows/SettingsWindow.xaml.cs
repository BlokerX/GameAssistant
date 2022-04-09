using GameAssistant.Pages;
using GameAssistant.Widgets;
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
        }

        /// <summary>
        /// Constructor with widgets.
        /// </summary>
        /// <param name="clockWidgetContainer"></param>
        /// <param name="pictureWidgetContainer"></param>
        /// <param name="noteWidgetContainer"></param>
        /// <param name="calculatorWidgetContainer"></param>
        public SettingsWindow(ref WidgetContainer<ClockWidget> clockWidgetContainer, 
            ref WidgetContainer<PictureWidget> pictureWidgetContainer, 
            ref WidgetContainer<NoteWidget> noteWidgetContainer, 
            ref WidgetContainer<CalculatorWidget> calculatorWidgetContainer)
        {
            InitializeComponent();

            ClockWidgetFrame.Content = new ClockSettingsPage(ref clockWidgetContainer);
            PictureWidgetFrame.Content = new PictureSettingsPage(ref pictureWidgetContainer);
            NoteWidgetFrame.Content = new NoteSettingsPage(ref noteWidgetContainer);
            CalculatorWidgetFrame.Content = new CalculatorSettingsPage(ref calculatorWidgetContainer);
        }

        /// <summary>
        /// Set visibility to hidden for all pages.
        /// </summary>
        private void HideAllPages()
        {
            ClockWidgetFrame.Visibility = Visibility.Collapsed;
            PictureWidgetFrame.Visibility = Visibility.Collapsed;
            NoteWidgetFrame.Visibility = Visibility.Collapsed;
            CalculatorWidgetFrame.Visibility = Visibility.Collapsed;
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
            CalculatorWidgetButton.BorderThickness = new Thickness(0);
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
