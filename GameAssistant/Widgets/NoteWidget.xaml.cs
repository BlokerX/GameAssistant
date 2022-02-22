using GameAssistant.WidgetViewModels;
using System.Windows.Controls;

namespace GameAssistant.Widgets
{
    /// <summary>
    /// Logika interakcji dla klasy NoteWidget.xaml
    /// </summary>
    public partial class NoteWidget : WidgetBase
    {
        public NoteWidget()
        {
            InitializeComponent();
        }

        public void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var binding = ((TextBox)sender).GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
        }

        private void WidgetBase_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            (DataContext as NoteViewModel).WidgetModel.IsActive = false;
        }

        private void WidgetBase_Initialized(object sender, System.EventArgs e)
        {
            (DataContext as NoteViewModel).WidgetModel.IsActive = true;
        }
    }
}
