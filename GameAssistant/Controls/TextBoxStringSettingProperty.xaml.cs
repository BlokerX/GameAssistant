using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameAssistant.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy TextBoxStringSettingProperty.xaml
    /// </summary>
    public partial class TextBoxStringSettingProperty : UserControl, ISettingProperty
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public TextBoxStringSettingProperty()
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
                ValueTextBox.Foreground = value;
                PropertyNameLabel.Foreground = value;
                ValueTextBox.BorderBrush = value;
            }
        }

        /// <summary>
        /// Color of ValueTextBox's background.
        /// </summary>
        public Brush ValueTextBoxBackgroundColor
        {
            get => ValueTextBox.Background;
            set => ValueTextBox.Background = value;
        }

        /// <summary>
        /// Property value string.
        /// </summary>
        public string PropertyValue
        {
            get => ValueTextBox.Text;
            set => ValueTextBox.Text = value;
        }

        public event EventHandler<string> PropertyValueChanged;

        private void ValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PropertyValueChanged?.Invoke(sender, (sender as TextBox).Text);
        }
    }
}
