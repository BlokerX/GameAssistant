using GameAssistant.Models;
using GameAssistant.Services;
using System;

namespace GameAssistant.Widgets
{
    /// <summary>
    /// Logika interakcji dla klasy PictureWidget.xaml
    /// </summary>
    public partial class PictureWidget : WidgetBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PictureWidget()
        {
            InitializeComponent();
            // todo widget model w dziedziczeniu widgetModel = (DataContext as IWidgetViewModel<PictureModel>).WidgetModel;
            DragWindowEvent += () => WidgetManager.SaveWidgetConfigurationInFile<PictureWidget, PictureModel>(this);
        }

        public static WidgetEvents Events = new WidgetEvents();
        
    }
}
