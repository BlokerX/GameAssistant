using System;

namespace GameAssistant.Services
{
    //todo MVVMUnit name
    internal class WidgetTypeUnit
    {
        /// <summary>
        /// Type of widget (window instance).
        /// </summary>
        public Type WidgetType { get; set; }

        /// <summary>
        /// Type of widget's view model.
        /// </summary>
        public Type WidgetViewModelType { get; set; }

        /// <summary>
        /// Type of widget view model's model.
        /// </summary>
        public Type WidgetModelType { get; set; }
    }
}
