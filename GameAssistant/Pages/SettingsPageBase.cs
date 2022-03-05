using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace GameAssistant.Pages
{
    public abstract class SettingsPageBase : Page, INotifyPropertyChanged
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

        // ========================================================================================== //

        #region NotifyPropertyChanged (Implemented)

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Set property with invoke PropertyChangedEvent.
        /// </summary>
        /// <typeparam name="T">Type of property.</typeparam>
        /// <param name="storage">Property reference.</param>
        /// <param name="value">New property value.</param>
        /// <param name="propertyname">The name of property.</param>
        protected void SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyname = null)
        {
            storage = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        #endregion
    }
}
