using System.Windows.Controls;
using System.Windows.Media;

namespace GameAssistant.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy DecimalSettingProperty.xaml
    /// </summary>
    public partial class DecimalSettingProperty : UserControl, ISettingProperty
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
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

        public Brush ForegroundColor
        {
            set
            {
                ValueSlider.BorderBrush = value;
                PropertyNameLabel.Foreground = value;
            }
        }

        /// <summary>
        /// Double value of property.
        /// </summary>
        public double PropertyValue
        {
            get => ValueSlider.Value;
            set => ValueSlider.Value = value;
        }
        
        /// <summary>
        /// Maximum value in slider.
        /// </summary>
        public double MaximumValue
        {
            get => ValueSlider.Maximum;
            set => ValueSlider.Maximum = value;
        }
        
        /// <summary>
        /// Minimum value in slider.
        /// </summary>
        public double MinimumValue
        {
            get => ValueSlider.Minimum;
            set => ValueSlider.Minimum = value;
        }

        /// <summary>
        /// Color of slider.
        /// </summary>
        public Brush SliderColor
        {
            get => ValueSlider.BorderBrush;
            set => ValueSlider.BorderBrush = value;
        }
   }
}
