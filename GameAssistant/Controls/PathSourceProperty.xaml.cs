using System;
using System.Windows;
using System.Windows.Media;

namespace GameAssistant.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy PathSourceProperty.xaml
    /// </summary>
    public partial class PathSourceProperty : BindableControl, ISettingProperty
    {
        public PathSourceProperty()
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
                PathSettingProperty.BackgorundColor = value;
            }
        }

        public string PropertyName { get; set; }

        public string PropertyValue
        {
            get => PathSettingProperty.ValueTextBox.Text;
            set => PathSettingProperty.ValueTextBox.Text = value;
        }

        public Brush ForegroundColor
        {
            set
            {
                PathSettingProperty.ForegroundColor = value;
            }
        }

        /// <summary>
        /// On button click.
        /// </summary>
        public event EventHandler ButtonClick;

        /// <summary>
        /// On PropertyValue changed.
        /// </summary>
        public event EventHandler<string> PropertyValueChanged;

        private void SelectPathButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick?.Invoke(sender, e);
        }

        private void PathSettingProperty_PropertyValueChanged(object sender, string e)
        {
            PropertyValueChanged?.Invoke(sender, e);
        }
    }
}
