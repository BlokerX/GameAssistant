using GameAssistant.Models;
using GameAssistant.Services;

namespace GameAssistant.Widgets
{
    /// <summary>
    /// Logika interakcji dla klasy ClockWidget.xaml
    /// </summary>
    public partial class ClockWidget : WidgetBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ClockWidget()
        {
            InitializeComponent();
            DragWindowEvent += () => WidgetManager.SaveWidgetConfigurationInFile<ClockWidget, ClockModel>(this);
        }

        public static WidgetEvents Events = new WidgetEvents();

    }
}
