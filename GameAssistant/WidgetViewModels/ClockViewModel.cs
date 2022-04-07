using GameAssistant.Core;
using GameAssistant.Models;
using System;
using System.Timers;
using System.Windows.Media;

namespace GameAssistant.WidgetViewModels
{
    /// <summary>
    /// View model that contains bindings for ClockWidget.
    /// </summary>
    internal class ClockViewModel : BindableObject, IWidgetViewModel<ClockModel>
    {
        private ClockModel _widgetModel = new ClockModel();
        /// <summary>
        /// Clock widget properties.
        /// </summary>
        public ClockModel WidgetModel
        {
            get => _widgetModel;
            set => SetProperty(ref _widgetModel, value);
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ClockViewModel()
        {
            // Set title:
            WidgetModel.Title = "Clock widget";

            // Set widget size:
            WidgetModel.Width = 200;
            WidgetModel.Height = 60;

            // Set widget background color:
            WidgetModel.BackgroundAnimatedBrush.BrushBackgroundContainer.Variable = new SolidColorBrush(Colors.Yellow);

            // Clock timer configuration:
            ClockTimer.Elapsed += (sender, e) => ClockTime = DateTime.Now.ToString("HH:mm:ss");

            // Run clock:
            ClockTimer.Start();
        }

        private string _clockTime = DateTime.Now.ToString("HH:mm:ss");
        /// <summary>
        /// Time that is shown in the clock widget.
        /// </summary>
        public string ClockTime
        {
            get => _clockTime;
            private set => SetProperty(ref _clockTime, value);
        }

        /// <summary>
        /// Clock change system.
        /// </summary>
        private readonly Timer ClockTimer = new Timer(0.01);

    }
}
