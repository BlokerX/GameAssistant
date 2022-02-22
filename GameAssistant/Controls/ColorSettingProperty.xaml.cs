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
    /// Logika interakcji dla klasy ColorSettingProperty.xaml
    /// </summary>
    public partial class ColorSettingProperty : UserControl, ISettingProperty
    {
        public ColorSettingProperty()
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

        public Brush PropertyColor
        {
            get => ColorRectangle.Fill;
            set => ColorRectangle.Fill = value;
        }

        public Brush ForegroundColor
        {
            set
            {
                ColorRectangle.Stroke = value;
                PropertyNameLabel.Foreground = value;
            }
        }

    }
}
