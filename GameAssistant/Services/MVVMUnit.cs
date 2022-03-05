using System;

namespace GameAssistant.Services
{
    /// <summary>
    /// Class with types base on Model-View-ViewModel strategy.
    /// </summary>
    internal class MVVMUnit
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
