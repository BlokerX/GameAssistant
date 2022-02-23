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
    /// Logika interakcji dla klasy TextBoxStringSettingProperty.xaml
    /// </summary>
    public partial class TextBoxStringSettingProperty : UserControl, ISettingProperty
    {
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

        public Brush ValueTextBoxBackgroundColor
        {
            get => ValueTextBox.Background;
            set => ValueTextBox.Background = value;
        }

        public string PropertyValue
        {
            get => ValueTextBox.Text;
            set => ValueTextBox.Text = value;
        }
    }
}
