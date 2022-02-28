using GameAssistant.Core;
using GameAssistant.Models;
using GameAssistant.Widgets;
using GameAssistant.WidgetViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAssistant.PageViewModels
{
    internal class ClockSettingsViewModel : BindableObject
    {
        private WidgetContainer<ClockWidget> _clockWidgetContainer;

        public WidgetContainer<ClockWidget> ClockWidgetContainer
        {
            get => _clockWidgetContainer;
            set => SetProperty(ref _clockWidgetContainer, value);
        }

        private ClockModel _clockModel;

        public ClockModel ClockModel
        {
            get => _clockModel;
            set => SetProperty(ref _clockModel, value);
        }

        public ClockSettingsViewModel(ref WidgetContainer<ClockWidget> clockWidget)
        {
            _clockWidgetContainer = clockWidget;

            if (_clockWidgetContainer != null)
                _clockModel = (_clockWidgetContainer.Widget.DataContext as IWidgetViewModel<ClockModel>).WidgetModel;
        }

    }
}
