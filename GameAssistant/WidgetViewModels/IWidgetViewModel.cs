namespace GameAssistant.WidgetViewModels
{
    /// <summary>
    /// View model's base interface.
    /// </summary>
    /// <typeparam name="TModel">Widget model.</typeparam>
    internal interface IWidgetViewModel<TModel>
    {
        /// <summary>
        /// Widget properties.
        /// </summary>
        TModel WidgetModel { get; set; }
    }
}
