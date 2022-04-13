using GameAssistant.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace GameAssistant.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy GeneralSettingsPage.xaml
    /// </summary>
    public partial class GeneralSettingsPage : Page
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public GeneralSettingsPage(Action resetAllSettings = null)
        {
            InitializeComponent();
            AutoStart.PropertyValue = AppFileSystem.CheckStartupKeyValue();
            if (resetAllSettings != null)
                ResetAllSettings += resetAllSettings;
        }

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
    }
}
