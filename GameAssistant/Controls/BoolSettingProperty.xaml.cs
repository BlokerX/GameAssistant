using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameAssistant.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy BoolSettingProperty.xaml
    /// </summary>
    public partial class BoolSettingProperty : UserControl, ISettingProperty
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public BoolSettingProperty()
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
                ValueCheckBox.Foreground = value;
                PropertyNameLabel.Foreground = value;
            }
        }

        /// <summary>
        /// Bool property value.
        /// </summary>
        public bool? PropertyValue
        {
            get => ValueCheckBox.IsChecked;
            set
            {
                ValueCheckBox.IsChecked = value;
                PropertyValueChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<bool?> PropertyValueChanged;

        /// <summary>
        /// Color of CheckBox char.
        /// </summary>
        public Brush CheckBoxForegroundColor
        {
            get => ValueCheckBox.Foreground;
            set => ValueCheckBox.Foreground = value;
        }
        
        /// <summary>
        /// Color of CheckBox's background.
        /// </summary>
        public Brush CheckBoxBackgroundColor
        {
            get => ValueCheckBox.Background;
            set => ValueCheckBox.Background = value;
        }
        
        /// <summary>
        /// Color of CheckBox's border.
        /// </summary>
        public Brush CheckBoxBorderColor
        {
            get => ValueCheckBox.BorderBrush;
            set => ValueCheckBox.BorderBrush = value;
        }

        private void ValueCheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            PropertyValueChanged?.Invoke(sender, true);
        }

        private void ValueCheckBox_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            PropertyValueChanged?.Invoke(sender, false);
        }
    }
}
