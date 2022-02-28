using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Forms = System.Windows.Forms;
using Drawing = System.Drawing;
using GameAssistant.Core;
using System.Windows;
using GameAssistant.ControlViewModels;
using System;

namespace GameAssistant.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy ColorSettingProperty.xaml
    /// </summary>
    public partial class ColorSettingProperty : SettingPropertyBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ColorSettingProperty()
        {//todo tutaj jest problem z przekazywaniem argumentu
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
            get
            {
                return this.ColorRectangle.Fill;
            }
            set
            {
                ColorRectangle.Fill = value;
                PropertyColorChanged?.Invoke(this, value);
            }
        }

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
