using GameAssistant.Core;
using GameAssistant.Models;
using System.Windows.Media;

namespace GameAssistant.WidgetViewModels
{
    /// <summary>
    /// View model that contains bindings for CalculatorWidget.
    /// </summary>
    internal class CalculatorViewModel : BindableObject, IWidgetViewModel<CalculatorModel>
    {
        private CalculatorModel _widgetModel = new CalculatorModel();
        /// <summary>
        /// Calculator widget properties.
        /// </summary>
        public CalculatorModel WidgetModel
        {
            get => _widgetModel;
            set => SetProperty(ref _widgetModel, value);
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CalculatorViewModel()
        {
            // Set title:
            WidgetModel.Title = "Calculator widget";

            // Set widget size:
            WidgetModel.Width = 240;
            WidgetModel.Height = 260;

            // Set widget background color:
            WidgetModel.BackgroundColor = new SolidColorBrush(Colors.GhostWhite);
        }
    }
}
