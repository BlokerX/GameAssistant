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
using Forms = System.Windows.Forms;

namespace GameAssistant.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy FontSettingProperty.xaml
    /// </summary>
    public partial class FontSettingProperty : UserControl, ISettingProperty
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
        public string PropertyName { get; set; }

        //todo
        public FontFamily PropertyFontFamily
        {
            get => new FontFamily(FontFamilySettingProperty.ValueTextBox.Text);
            set => FontFamilySettingProperty.ValueTextBox.Text = value.ToString();
        }

        //todo konwersja double-string
        public double PropertyFontSize
        {
            get => double.Parse(FontSizeSettingProperty.PropertyValue);
            set => FontSizeSettingProperty.PropertyValue = value.ToString();
        }

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

            if(fontDialog.ShowDialog() == Forms.DialogResult.OK)
            {
                FontFamilySettingProperty.PropertyValue = fontDialog.Font.FontFamily.Name;
                FontSizeSettingProperty.PropertyValue = fontDialog.Font.Size.ToString();
            }
        }
    }
}
