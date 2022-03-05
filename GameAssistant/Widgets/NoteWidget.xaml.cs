using System.Windows.Controls;

namespace GameAssistant.Widgets
{
    /// <summary>
    /// Logika interakcji dla klasy NoteWidget.xaml
    /// </summary>
    public partial class NoteWidget : WidgetBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public NoteWidget()
        {
            InitializeComponent();
        }

        public void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var binding = ((TextBox)sender).GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
        }

    }
}
