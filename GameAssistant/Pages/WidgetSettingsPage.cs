using System.Windows;

namespace GameAssistant.Pages
{
    public abstract class WidgetSettingsPage : SettingsPage
    {
        /// <summary>
        /// Set properties active state.
        /// </summary>
        /// <param name="newState">True = enabled, false = disabled.</param>
        protected abstract void ActiveChanged(bool newState);

        /// <summary>
        /// Active property changed.
        /// </summary>
        protected abstract void ActiveProperty_PropertyValueChanged(object sender, bool? e);

        /// <summary>
        /// On default button click.
        /// </summary>
        protected abstract void DefaultSettingsButton_Click(object sender, RoutedEventArgs e);

        /// <summary>
        /// On open save dire button click.
        /// </summary>
        protected abstract void OpenSaveConfigurationDireButton_Click(object sender, RoutedEventArgs e);

        /// <summary>
        /// On loaded save configuration button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected abstract void LoadSavedConfigurationButton_Click(object sender, RoutedEventArgs e);

        /// <summary>
        /// Remove page's method from ChangeWidgetEvent.
        /// </summary>
        public abstract void RemovePageMethodsFromWidgetEvents();

    }
}
