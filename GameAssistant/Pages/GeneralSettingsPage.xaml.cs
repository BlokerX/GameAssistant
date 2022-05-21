using GameAssistant.Services;
using GameAssistant.Widgets;
using System;
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
        public GeneralSettingsPage(/*ref WidgetContainer<ClockWidget> clockWidgetContainer,
                                   ref WidgetContainer<PictureWidget> pictureWidgetContainer,
                                   ref WidgetContainer<NoteWidget> noteWidgetContainer,
                                   ref WidgetContainer<CalculatorWidget> calculatorWidgetContainer,
                                   ref WidgetContainer<BrowserWidget> browserWidgetContainer,*/
                                   ref AllWidgetsContainer allWidgetsContainer,
                                   Action resetAllSettings = null)
        {
            InitializeComponent();

            _allWidgetsContainer = allWidgetsContainer;

            LoadWidgets(AllWidgetsContainer /*clockWidgetContainer, pictureWidgetContainer, noteWidgetContainer, calculatorWidgetContainer, browserWidgetContainer*/);

            SubscriptionWidgetsActiveChangedEvents();

            AutoStart.PropertyValue = AppFileSystem.CheckStartupKeyValue();
            if (resetAllSettings != null)
                ResetAllSettings += resetAllSettings;
        }

        public void RemovePageMethodsFromWidgetsEvents() => DesubscriptionWidgetsActiveChangedEvents();

        #region WidgetsConteners

        public static readonly DependencyProperty PropertyAllWidgetsContainer = DependencyProperty.Register(
        "AllWidgetsContainer", typeof(AllWidgetsContainer),
        typeof(GeneralSettingsPage)
        );

        private AllWidgetsContainer _allWidgetsContainer;

        /// <summary>
        /// The all widgets containers with widgets.
        /// </summary>
        public AllWidgetsContainer AllWidgetsContainer
        {
            get => _allWidgetsContainer;
            set => SetProperty(ref _allWidgetsContainer, value);
        }

        #endregion

        private void LoadWidgets(AllWidgetsContainer allWidgetsContainer)
        {
            if (allWidgetsContainer.clockWidgetContainer.Widget == null)
            {
                ClockWidgetActiveProperty.PropertyValue = false;
            }
            else
            {
                ClockWidgetActiveProperty.PropertyValue = true;
            }

            if (allWidgetsContainer.pictureWidgetContainer.Widget == null)
            {
                PictureWidgetActiveProperty.PropertyValue = false;
            }
            else
            {
                PictureWidgetActiveProperty.PropertyValue = true;
            }

            if (allWidgetsContainer.noteWidgetContainer.Widget == null)
            {
                NoteWidgetActiveProperty.PropertyValue = false;
            }
            else
            {
                NoteWidgetActiveProperty.PropertyValue = true;
            }

            if (allWidgetsContainer.calculatorWidgetContainer.Widget == null)
            {
                CalculatorWidgetActiveProperty.PropertyValue = false;
            }
            else
            {
                CalculatorWidgetActiveProperty.PropertyValue = true;
            }

            if (allWidgetsContainer.browserWidgetContainer.Widget == null)
            {
                BrowserWidgetActiveProperty.PropertyValue = false;
            }
            else
            {
                BrowserWidgetActiveProperty.PropertyValue = true;
            }
        }

        #region Widgets Active Changed Events

        /// <summary>
        /// Add subscription to widgets active changed's events.
        /// </summary>
        private void SubscriptionWidgetsActiveChangedEvents()
        {
            ClockWidget.Events.WidgetActiveChanged += ClockWidgetEvents_WidgetActiveChanged;
            PictureWidget.Events.WidgetActiveChanged += PictureWidgetEvents_WidgetActiveChanged;
            NoteWidget.Events.WidgetActiveChanged += NoteWidgetEvents_WidgetActiveChanged;
            CalculatorWidget.Events.WidgetActiveChanged += CalculatorWidgetEvents_WidgetActiveChanged;
            BrowserWidget.Events.WidgetActiveChanged += BrowserWidgetEvents_WidgetActiveChanged;
        }

        /// <summary>
        /// Remove subscription to widgets active changed's events.
        /// </summary>
        public void DesubscriptionWidgetsActiveChangedEvents()
        {
            ClockWidget.Events.WidgetActiveChanged -= ClockWidgetEvents_WidgetActiveChanged;
            PictureWidget.Events.WidgetActiveChanged -= PictureWidgetEvents_WidgetActiveChanged;
            NoteWidget.Events.WidgetActiveChanged -= NoteWidgetEvents_WidgetActiveChanged;
            CalculatorWidget.Events.WidgetActiveChanged -= CalculatorWidgetEvents_WidgetActiveChanged;
            BrowserWidget.Events.WidgetActiveChanged -= BrowserWidgetEvents_WidgetActiveChanged;
        }

        #region Widgets event's methods

        private void ClockWidgetEvents_WidgetActiveChanged(bool state)
        {
            if (ClockWidgetActiveProperty.PropertyValue != state)
                ClockWidgetActiveProperty.PropertyValue = state;
        }

        private void PictureWidgetEvents_WidgetActiveChanged(bool state)
        {
            if (PictureWidgetActiveProperty.PropertyValue != state)
                PictureWidgetActiveProperty.PropertyValue = state;
        }

        private void NoteWidgetEvents_WidgetActiveChanged(bool state)
        {
            if (NoteWidgetActiveProperty.PropertyValue != state)
                NoteWidgetActiveProperty.PropertyValue = state;
        }

        private void CalculatorWidgetEvents_WidgetActiveChanged(bool state)
        {
            if (CalculatorWidgetActiveProperty.PropertyValue != state)
                CalculatorWidgetActiveProperty.PropertyValue = state;
        }

        private void BrowserWidgetEvents_WidgetActiveChanged(bool state)
        {
            if (BrowserWidgetActiveProperty.PropertyValue != state)
                BrowserWidgetActiveProperty.PropertyValue = state;
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

        /// <summary>
        /// On reset all settings button clicked.
        /// </summary>
        public event Action ResetAllSettings;

        private void ResetAllSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            ResetAllSettings?.Invoke();
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

        #endregion
    }
}
