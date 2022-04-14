using GameAssistant.Core;
using System;
using System.Windows;
using System.Windows.Media;
using Forms = System.Windows.Forms;

namespace GameAssistant.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy FontSettingProperty.xaml
    /// </summary>
    public partial class FontSettingProperty : SettingPropertyBase, ISettingProperty
    {
        public FontSettingProperty()
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
                FontFamilySettingProperty.BackgorundColor = value;
                FontSizeSettingProperty.BackgorundColor = value;
            }
        }

        //todo property name
        private string _propertyName;
        public string PropertyName
        {
            get => _propertyName;
            set => SetProperty(ref _propertyName, value);
        }

        public FontFamily PropertyFontFamily
        {
            get => new FontFamily(FontFamilySettingProperty.ValueTextBox.Text);
            set => FontFamilySettingProperty.ValueTextBox.Text = value.ToString();
        }

        //todo zmienić textbox size na DoubleUpDown
        public double PropertyFontSize
        {
            get => double.Parse(FontSizeSettingProperty.PropertyValue);
            set => FontSizeSettingProperty.PropertyValue = value.ToString();
        }

        public event EventHandler<(FontFamily, double)> PropertyValueChanged;

        public Brush ForegroundColor
        {
            set
            {
                FontSizeSettingProperty.ForegroundColor = value;
                FontFamilySettingProperty.ForegroundColor = value;
            }
        }

        /// <summary>
        /// On change font button click.
        /// </summary>
        private void ChangeFontButton_Click(object sender, RoutedEventArgs e)
        {
            var fontDialog = new Forms.FontDialog()
            {
                AllowScriptChange = false,
                AllowSimulations = true,
                AllowVectorFonts = true,
                AllowVerticalFonts = true,
                ShowEffects = false,
                FontMustExist = true,
                ScriptsOnly = false,
                ShowColor = false,
                ShowHelp = false,

                Font = new System.Drawing.Font(FontFamilySettingProperty.PropertyValue, float.Parse(FontSizeSettingProperty.PropertyValue))
            };

            if (fontDialog.ShowDialog() == Forms.DialogResult.OK)
            {
                FontFamilySettingProperty.PropertyValue = fontDialog.Font.FontFamily.Name;
                FontSizeSettingProperty.PropertyValue = fontDialog.Font.Size.ToString();
                PropertyValueChanged?.Invoke(this,
                    (
                        TypeConverter.ConvertFontFamilyDrawingToMedia(fontDialog.Font.FontFamily),
                        fontDialog.Font.Size
                    )
                    );
            }

        }
    }
}
