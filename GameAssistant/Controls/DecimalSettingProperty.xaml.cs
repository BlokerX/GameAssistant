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
    /// Logika interakcji dla klasy DecimalSettingProperty.xaml
    /// </summary>
    public partial class DecimalSettingProperty : UserControl, ISettingProperty
    {
        public DecimalSettingProperty()
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

        public double PropertyValue
        {
            get => ValueSlider.Value;
            set => ValueSlider.Value = value;
        }
        
        public double MaximumValue
        {
            get => ValueSlider.Maximum;
            set => ValueSlider.Maximum = value;
        }
        
        public double MinimumValue
        {
            get => ValueSlider.Minimum;
            set => ValueSlider.Minimum = value;
        }

        public Brush SliderColor
        {
            get => ValueSlider.BorderBrush;
            set => ValueSlider.BorderBrush = value;
        }

        public Brush ForegroundColor
        {
            set
            {
                ValueSlider.BorderBrush = value;
                PropertyNameLabel.Foreground = value;
            }
        }
   }
}
