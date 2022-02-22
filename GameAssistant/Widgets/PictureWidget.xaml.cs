using GameAssistant.WidgetViewModels;

namespace GameAssistant.Widgets
{
    /// <summary>
    /// Logika interakcji dla klasy PictureWidget.xaml
    /// </summary>
    public partial class PictureWidget : WidgetBase
    {
        public PictureWidget()
        {
            InitializeComponent();
        }

        private void WidgetBase_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            (DataContext as PictureViewModel).WidgetModel.IsActive = false;
        }

        private void WidgetBase_Initialized(object sender, System.EventArgs e)
        {
            (DataContext as PictureViewModel).WidgetModel.IsActive = true;
        }
    }
}
