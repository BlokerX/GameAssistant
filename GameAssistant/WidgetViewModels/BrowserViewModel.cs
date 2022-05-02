using GameAssistant.Core;
using GameAssistant.Models;
using System;
using System.Windows;
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
            // Set title:
            WidgetModel.Title = "Browser widget";

            // Set widget size:
            WidgetModel.Width = 450;
            WidgetModel.Height = 320;

            // Set widget position:
            WidgetModel.ScreenPositionY += 70;

            // Set widget background color:
            WidgetModel.BackgroundAnimatedBrush.BrushContainer.Variable = new SolidColorBrush(Colors.DarkBlue);
            WidgetModel.BackgroundOpacity = 0.25;
        }

    }
}
