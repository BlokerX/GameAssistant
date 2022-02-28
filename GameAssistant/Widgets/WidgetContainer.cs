namespace GameAssistant.Widgets
{
    /// <typeparam name="WidgetType">Type of widget.</typeparam>
    public class WidgetContainer<WidgetType>
        where WidgetType : WidgetBase, new()
    {
        public WidgetContainer() { }

        public WidgetContainer(ref WidgetType widget)
        {
            Widget = widget;
        }

        public WidgetType Widget;
    }
}
