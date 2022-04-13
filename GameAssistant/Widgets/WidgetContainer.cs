using System;

namespace GameAssistant.Widgets
{
    /// <summary>
    /// Container for widgets (It is very helpfull with use references).
    /// </summary>
    /// <typeparam name="WidgetType">Type of widget.</typeparam>
    public class WidgetContainer<WidgetType>
        where WidgetType : WidgetBase, new()
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public WidgetContainer() { }

        /// <summary>
        /// Constructor with widget parameter.
        /// </summary>
        /// <param name="widget">Widget to aplicate.</param>
        public WidgetContainer(ref WidgetType widget)
        {
            Widget = widget;
        }

        /// <summary>
        /// Widget as container item.
        /// </summary>
        public WidgetType Widget;

    }
}
