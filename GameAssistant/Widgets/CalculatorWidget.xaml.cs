using GameAssistant.Models;
using GameAssistant.Services;
using System;

namespace GameAssistant.Widgets
{
    /// <summary>
    /// Logika interakcji dla klasy CalculatorWidget.xaml
    /// </summary>
    public partial class CalculatorWidget : WidgetBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public CalculatorWidget()
        {
            InitializeComponent();
            DragWindowEvent += () => WidgetManager.SaveWidgetConfigurationInFile<CalculatorWidget, CalculatorModel>(this);
        }

        public static WidgetEvents Events = new WidgetEvents();

    }
}
