using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameAssistant.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy PathSourceProperty.xaml
    /// </summary>
    public partial class PathSourceProperty : UserControl, ISettingProperty
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

        public event EventHandler ButtonClick;
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
