using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace GameAssistant.Pages
{
    public abstract class SettingsPage : Page, INotifyPropertyChanged
    {
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
    }
}