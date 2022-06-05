using GameAssistant.Models;
using GameAssistant.Services;

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
            DragWindowEvent += () => WidgetManager.SaveWidgetConfigurationInFile<PictureWidget, PictureModel>(this);
        }

        public static WidgetEvents Events = new WidgetEvents();

    }
}
