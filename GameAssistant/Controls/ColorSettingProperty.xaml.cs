using GameAssistant.Core;
using System;
using System.Windows.Input;
using System.Windows.Media;
using Forms = System.Windows.Forms;

namespace GameAssistant.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy ColorSettingProperty.xaml
    /// </summary>
    public partial class ColorSettingProperty : BindableControl, ISettingProperty
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
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

        public Brush ForegroundColor
        {
            set
            {
                ColorRectangle.Stroke = value;
                PropertyNameLabel.Foreground = value;
            }
        }

        /// <summary>
        /// Property value (Color).
        /// </summary>
        public Brush PropertyColor
        {
            get => ColorRectangle.Fill;
            set
            {
                ColorRectangle.Fill = value;
                PropertyColorChanged?.Invoke(this, value);
            }
        }

        /// <summary>
        /// On PropertyColor changed.
        /// </summary>
        public event EventHandler<Brush> PropertyColorChanged;

        /// <summary>
        /// On mouse left button click Color Rectangle.
        /// </summary>
        private void ColorRectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var drawingColor = TypeConverter.ConvertColorMediaToDrawing((PropertyColor as SolidColorBrush).Color);

            var colorDialog = new Forms.ColorDialog()
            {
                Color = drawingColor,
                FullOpen = true,
                ShowHelp = false
            };

            if (colorDialog.ShowDialog() == Forms.DialogResult.OK)
            {
                PropertyColor = new SolidColorBrush(TypeConverter.ConvertColorDrawingToMedia(colorDialog.Color));
            }
        }

    }
}
