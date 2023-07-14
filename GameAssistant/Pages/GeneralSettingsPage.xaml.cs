using GameAssistant.Services;
using GameAssistant.Widgets;
using System;
using System.Diagnostics;
using System.Windows;

namespace GameAssistant.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy GeneralSettingsPage.xaml
    /// </summary>
    public partial class GeneralSettingsPage : SettingsPage
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public GeneralSettingsPage(ref AllWidgetsContainer allWidgetsContainer, Action resetConfigAction)
        {
            InitializeComponent();

            _allWidgetsContainer = allWidgetsContainer;

            LoadWidgets(AllWidgetsContainer);

            SubscriptionWidgetsActiveChangedEvents();

            AutoStart.PropertyValue = AppFileSystem.CheckStartupKeyValue();

            CheckAllWidgetsActiveProperty();

            ResetAllSettingsButton.Click += (sender, e) => resetConfigAction.Invoke();
        }

        public void RemovePageMethodsFromWidgetsEvents() => DesubscriptionWidgetsActiveChangedEvents();

        #region WidgetsConteners

        public static readonly DependencyProperty PropertyAllWidgetsContainer = DependencyProperty.Register(
        "AllWidgetsContainer", typeof(AllWidgetsContainer),
        typeof(GeneralSettingsPage)
        );

        private AllWidgetsContainer _allWidgetsContainer;

        /// <summary>
        /// The all widgetsContainer containers with widgetsContainer.
        /// </summary>
        public AllWidgetsContainer AllWidgetsContainer
        {
            get => _allWidgetsContainer;
            set => SetProperty(ref _allWidgetsContainer, value);
        }

        #endregion

        private void LoadWidgets(AllWidgetsContainer allWidgetsContainer)
        {
            if (allWidgetsContainer.ClockWidgetContainer.Widget == null)
            {
                ClockWidgetActiveProperty.PropertyValue = false;
            }
            else
            {
                ClockWidgetActiveProperty.PropertyValue = true;
            }

            if (allWidgetsContainer.PictureWidgetContainer.Widget == null)
            {
                PictureWidgetActiveProperty.PropertyValue = false;
            }
            else
            {
                PictureWidgetActiveProperty.PropertyValue = true;
            }

            if (allWidgetsContainer.NoteWidgetContainer.Widget == null)
            {
                NoteWidgetActiveProperty.PropertyValue = false;
            }
            else
            {
                NoteWidgetActiveProperty.PropertyValue = true;
            }

            if (allWidgetsContainer.CalculatorWidgetContainer.Widget == null)
            {
                CalculatorWidgetActiveProperty.PropertyValue = false;
            }
            else
            {
                CalculatorWidgetActiveProperty.PropertyValue = true;
            }

            if (allWidgetsContainer.BrowserWidgetContainer.Widget == null)
            {
                BrowserWidgetActiveProperty.PropertyValue = false;
            }
            else
            {
                BrowserWidgetActiveProperty.PropertyValue = true;
            }

            if (allWidgetsContainer.KeyDetectorWidgetContainer.Widget == null)
            {
                KeyDetectorWidgetActiveProperty.PropertyValue = false;
            }
            else
            {
                KeyDetectorWidgetActiveProperty.PropertyValue = true;
            }

            if (allWidgetsContainer.SpotifyWidgetContainer.Widget == null)
            {
                SpotifyWidgetActiveProperty.PropertyValue = false;
            }
            else
            {
                SpotifyWidgetActiveProperty.PropertyValue = true;
            }
        }

        #region Widgets Active Changed Events

        /// <summary>
        /// Add subscription to widgetsContainer active changed's events.
        /// </summary>
        private void SubscriptionWidgetsActiveChangedEvents()
        {
            ClockWidget.Events.WidgetActiveChanged += ClockWidgetEvents_WidgetActiveChanged;
            PictureWidget.Events.WidgetActiveChanged += PictureWidgetEvents_WidgetActiveChanged;
            NoteWidget.Events.WidgetActiveChanged += NoteWidgetEvents_WidgetActiveChanged;
            CalculatorWidget.Events.WidgetActiveChanged += CalculatorWidgetEvents_WidgetActiveChanged;
            BrowserWidget.Events.WidgetActiveChanged += BrowserWidgetEvents_WidgetActiveChanged;
            KeyDetectorWidget.Events.WidgetActiveChanged += KeyDetectorWidgetEvents_WidgetActiveChanged;
            SpotifyWidget.Events.WidgetActiveChanged += SpotifyWidgetEvents_WidgetActiveChanged;
        }

        /// <summary>
        /// Remove subscription to widgetsContainer active changed's events.
        /// </summary>
        public void DesubscriptionWidgetsActiveChangedEvents()
        {
            ClockWidget.Events.WidgetActiveChanged -= ClockWidgetEvents_WidgetActiveChanged;
            PictureWidget.Events.WidgetActiveChanged -= PictureWidgetEvents_WidgetActiveChanged;
            NoteWidget.Events.WidgetActiveChanged -= NoteWidgetEvents_WidgetActiveChanged;
            CalculatorWidget.Events.WidgetActiveChanged -= CalculatorWidgetEvents_WidgetActiveChanged;
            BrowserWidget.Events.WidgetActiveChanged -= BrowserWidgetEvents_WidgetActiveChanged;
            KeyDetectorWidget.Events.WidgetActiveChanged -= KeyDetectorWidgetEvents_WidgetActiveChanged;
            SpotifyWidget.Events.WidgetActiveChanged -= SpotifyWidgetEvents_WidgetActiveChanged;
        }

        #region Widgets event's methods

        private void ClockWidgetEvents_WidgetActiveChanged(bool state)
        {
            if (ClockWidgetActiveProperty.PropertyValue != state)
                ClockWidgetActiveProperty.PropertyValue = state;
            CheckAllWidgetsActiveProperty();
        }

        private void PictureWidgetEvents_WidgetActiveChanged(bool state)
        {
            if (PictureWidgetActiveProperty.PropertyValue != state)
                PictureWidgetActiveProperty.PropertyValue = state;
            CheckAllWidgetsActiveProperty();
        }

        private void NoteWidgetEvents_WidgetActiveChanged(bool state)
        {
            if (NoteWidgetActiveProperty.PropertyValue != state)
                NoteWidgetActiveProperty.PropertyValue = state;
            CheckAllWidgetsActiveProperty();
        }

        private void CalculatorWidgetEvents_WidgetActiveChanged(bool state)
        {
            if (CalculatorWidgetActiveProperty.PropertyValue != state)
                CalculatorWidgetActiveProperty.PropertyValue = state;
            CheckAllWidgetsActiveProperty();
        }

        private void BrowserWidgetEvents_WidgetActiveChanged(bool state)
        {
            if (BrowserWidgetActiveProperty.PropertyValue != state)
                BrowserWidgetActiveProperty.PropertyValue = state;

            CheckAllWidgetsActiveProperty();
        }

        private void KeyDetectorWidgetEvents_WidgetActiveChanged(bool state)
        {
            if (KeyDetectorWidgetActiveProperty.PropertyValue != state)
                KeyDetectorWidgetActiveProperty.PropertyValue = state;

            CheckAllWidgetsActiveProperty();
        }

        private void SpotifyWidgetEvents_WidgetActiveChanged(bool state)
        {
            if (SpotifyWidgetActiveProperty.PropertyValue != state)
                SpotifyWidgetActiveProperty.PropertyValue = state;

            CheckAllWidgetsActiveProperty();
        }

        #endregion

        #endregion

        /// <summary>
        /// Set or remove startup process.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">True to create autostart, false to delete autostart.</param>
        private void AutoStart_PropertyValueChanged(object sender, bool? e)
        {
            if (e == true)
                AppFileSystem.CreateStartupKey();
            else
                AppFileSystem.DeleteStartupKey();
        }

        #region Active changed checkboxes

        private void ClockWidgetActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            ClockWidget.Events.WidgetActiveChanged_Invoke((bool)e);
        }

        private void PictureWidgetActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            PictureWidget.Events.WidgetActiveChanged_Invoke((bool)e);
        }

        private void NoteWidgetActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            NoteWidget.Events.WidgetActiveChanged_Invoke((bool)e);
        }

        private void CalculatorWidgetActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            CalculatorWidget.Events.WidgetActiveChanged_Invoke((bool)e);
        }

        private void BrowserWidgetActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            BrowserWidget.Events.WidgetActiveChanged_Invoke((bool)e);
        }

        private void KeyDetectorWidgetActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            KeyDetectorWidget.Events.WidgetActiveChanged_Invoke((bool)e);
        }

        private void SpotifyWidgetActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            SpotifyWidget.Events.WidgetActiveChanged_Invoke((bool)e);
        }

        #endregion

        // todo poprawić wydajność programu pod względem wykonywania eventów
        private void AllWidgetsActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            if (!AllWidgetsActiveProperty.IsEnabled)
                return;

            if (ClockWidgetActiveProperty.PropertyValue != e)
                ClockWidgetActiveProperty.PropertyValue = e;

            if (PictureWidgetActiveProperty.PropertyValue != e)
                PictureWidgetActiveProperty.PropertyValue = e;

            if (NoteWidgetActiveProperty.PropertyValue != e)
                NoteWidgetActiveProperty.PropertyValue = e;

            if (CalculatorWidgetActiveProperty.PropertyValue != e)
                CalculatorWidgetActiveProperty.PropertyValue = e;

            if (BrowserWidgetActiveProperty.PropertyValue != e)
                BrowserWidgetActiveProperty.PropertyValue = e;

            if (KeyDetectorWidgetActiveProperty.PropertyValue != e)
                KeyDetectorWidgetActiveProperty.PropertyValue = e;

            if (SpotifyWidgetActiveProperty.PropertyValue != e)
                SpotifyWidgetActiveProperty.PropertyValue = e;
        }

        // a - czy sprawdzać wartość false
        private void CheckAllWidgetsActiveProperty()
        {
            if (!AllWidgetsActiveProperty.IsEnabled)
                return;

            if (true == ClockWidgetActiveProperty.PropertyValue &&
                ClockWidgetActiveProperty.PropertyValue == PictureWidgetActiveProperty.PropertyValue &&
                PictureWidgetActiveProperty.PropertyValue == NoteWidgetActiveProperty.PropertyValue &&
                NoteWidgetActiveProperty.PropertyValue == CalculatorWidgetActiveProperty.PropertyValue &&
                CalculatorWidgetActiveProperty.PropertyValue == BrowserWidgetActiveProperty.PropertyValue &&
                BrowserWidgetActiveProperty.PropertyValue == KeyDetectorWidgetActiveProperty.PropertyValue &&
                KeyDetectorWidgetActiveProperty.PropertyValue == SpotifyWidgetActiveProperty.PropertyValue)
            {
                AllWidgetsActiveProperty.PropertyValue = true;
            }
            else if (AllWidgetsActiveProperty.ValueCheckBox.IsChecked != false)
            {
                AllWidgetsActiveProperty.IsEnabled = false;
                AllWidgetsActiveProperty.ValueCheckBox.IsChecked = false;
                AllWidgetsActiveProperty.IsEnabled = true;
            }
        }

        private void OpenConfigurationsDireButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("Explorer", AppFileSystem.WidgetsConfigurationsMainDire);
        }

    }
}
