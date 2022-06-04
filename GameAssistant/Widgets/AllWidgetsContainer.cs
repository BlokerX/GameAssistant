namespace GameAssistant.Widgets
{
    /// <summary>
    /// It contains all widget's containers.
    /// </summary>
    public class AllWidgetsContainer
    {
        #region WidgetsContainers

        #region ClockWidgetContainer
        /// <summary>
        /// The clock widget's container.
        /// </summary>
        public WidgetContainer<ClockWidget> clockWidgetContainer;
        #endregion

        #region PictureWidgetContainer
        /// <summary>
        /// The picture widget's container.
        /// </summary>
        public WidgetContainer<PictureWidget> pictureWidgetContainer;
        #endregion

        #region NoteWidgetContainer
        /// <summary>
        /// The note widget's container.
        /// </summary>
        public WidgetContainer<NoteWidget> noteWidgetContainer;
        #endregion

        #region CalculatorWidgetContainer
        /// <summary>
        /// The calculator widget's container.
        /// </summary>
        public WidgetContainer<CalculatorWidget> calculatorWidgetContainer;
        #endregion

        #region BrowserWidgetContainer
        /// <summary>
        /// The browser widget's container.
        /// </summary>
        public WidgetContainer<BrowserWidget> browserWidgetContainer;
        #endregion
        
        #region KeyDetectorWidgetContainer
        /// <summary>
        /// The key detector widget's container.
        /// </summary>
        public WidgetContainer<KeyDetectorWidget> keyDetectorWidgetContainer;
        #endregion

        #endregion
    }
}
