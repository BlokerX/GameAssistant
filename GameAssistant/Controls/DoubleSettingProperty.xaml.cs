using GameAssistant.ControlViewModels;
using System;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace GameAssistant.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy DoubleSettingProperty.xaml
    /// </summary>
    public partial class DoubleSettingProperty : SettingPropertyBase, ISettingProperty
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public DoubleSettingProperty()
        {
            InitializeComponent();
        }

        public Brush BorderColor
        {
            get => MainBorder.Background;
            set => MainBorder.Background = value;
        }

        public Brush BackgorundColor
        {
            set
            {
                FirstColumnGrid.Background = value;
                SecondColumnGrid.Background = value;
            }
        }

        public string PropertyName
        {
            get => PropertyNameLabel.Content.ToString();
            set => PropertyNameLabel.Content = value;
        }

        public Brush ForegroundColor
        {
            set
            {
                ValueSlider.BorderBrush = value;
                ValueDoubleUpDown.BorderBrush = value;
                PropertyNameLabel.Foreground = value;
            }
        }

        /// <summary>
        /// Double value of property.
        /// </summary>
        public double PropertyValue
        {
            get => (DataContext as DoubleSettingPropertyViewModel).Value;
            set
            {
                (DataContext as DoubleSettingPropertyViewModel).Value = value;
                PropertyValueChanged?.Invoke(this, value);
            }
        }

        /// <summary>
        /// On PropertyValue changed.
        /// </summary>
        public event EventHandler<double> PropertyValueChanged;

        /// <summary>
        /// Maximum value in slider.
        /// </summary>
        public double MaximumValue
        {
            get => (DataContext as DoubleSettingPropertyViewModel).Maximum;
            set
            {
                (DataContext as DoubleSettingPropertyViewModel).Maximum = value;
            }
        }

        /// <summary>
        /// Minimum value in slider.
        /// </summary>
        public double MinimumValue
        {
            get => (DataContext as DoubleSettingPropertyViewModel).Minimum;
            set
            {
                (DataContext as DoubleSettingPropertyViewModel).Minimum = value;
            }
        }

        /// <summary>
        /// Color of slider.
        /// </summary>
        public Brush SliderColor
        {
            get => ValueSlider.BorderBrush;
            set => ValueSlider.BorderBrush = value;
        }

        private void ValueSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            PropertyValueChanged?.Invoke(this, e.NewValue);
        }

        private void ValueDoubleUpDown_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            if (double.TryParse((sender as DoubleUpDown).Value.ToString(), out var result))
                PropertyValueChanged?.Invoke(this, result);
        }
    }
}
