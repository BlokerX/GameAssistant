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
                ShowHelp = false
            };

            //fontDialog.ShowDialog();
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

        //todo
        public FontFamily PropertyFontFamily
        {
            get;
            set;
        }

        public FontStyle PropertyFontStyle
        {
            get;
            set;
        }
        
        public double PropertyFontSize
        {
            get;
            set;
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

    }
}
