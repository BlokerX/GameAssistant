using GameAssistant.Core;
using GameAssistant.Models;
using System.Windows.Media;

namespace GameAssistant.WidgetViewModels
{
    /// <summary>
    /// View model that contains bindings for BrowserWidget.
    /// </summary>
    internal class BrowserViewModel : BindableObject, IWidgetViewModel<BrowserModel>
    {
        private BrowserModel _widgetModel = new BrowserModel();
        /// <summary>
        /// Browser widget properties.
        /// </summary>
        public BrowserModel WidgetModel
        {
            get => _widgetModel;
            set => SetProperty(ref _widgetModel, value);
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BrowserViewModel()
        {
            LoadModel();
        }

        public void LoadModel()
        {
            // Set title:
            WidgetModel.Title = "Browser widget";

            // Set widget size:
            WidgetModel.Width = 360;
            WidgetModel.Height = 325;

            // Set widget position:
            WidgetModel.ScreenPositionX += 410;

            // Set widget background color:
            WidgetModel.BackgroundAnimatedBrush.BrushContainer.Variable = new SolidColorBrush(Colors.Navy);
            WidgetModel.BackgroundOpacity = 0.6;
        }
    }
}
