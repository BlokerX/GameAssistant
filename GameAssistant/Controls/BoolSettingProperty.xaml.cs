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
    /// Logika interakcji dla klasy BoolSettingProperty.xaml
    /// </summary>
    public partial class BoolSettingProperty : UserControl, ISettingProperty
    {
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

        public bool? PropertyValue
        {
            get => ValueCheckBox.IsChecked;
            set => ValueCheckBox.IsChecked = value;
        }

        public Brush CheckBoxForegroundColor
        {
            get => ValueCheckBox.Foreground;
            set => ValueCheckBox.Foreground = value;
        }
        
        public Brush CheckBoxBackgroundColor
        {
            get => ValueCheckBox.Background;
            set => ValueCheckBox.Background = value;
        }
        
        public Brush CheckBoxBorderColor
        {
            get => ValueCheckBox.BorderBrush;
            set => ValueCheckBox.BorderBrush = value;
        }

        public Brush ForegroundColor
        {
            set
            {
                ValueCheckBox.Foreground = value;
                PropertyNameLabel.Foreground = value;
            }
        }

    }
}
