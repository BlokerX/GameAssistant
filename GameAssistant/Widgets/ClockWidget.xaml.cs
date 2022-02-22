using GameAssistant.WidgetViewModels;

namespace GameAssistant.Widgets
{
    /// <summary>
    /// Logika interakcji dla klasy ClockWidget.xaml
    /// </summary>
    public partial class ClockWidget : WidgetBase
    {
        public ClockWidget()
        {
            InitializeComponent();
        }

        //private void WidgetBase_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    (DataContext as ClockViewModel).WidgetModel.IsActive = false;
        //}

        //private void WidgetBase_Initialized(object sender, System.EventArgs e)
        //{
        //    (DataContext as ClockViewModel).WidgetModel.IsActive = true;
        //}
    }
}
