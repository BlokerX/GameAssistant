namespace GameAssistant.Widgets
{
    /// <summary>
    /// It contains all widget's containers.
    /// </summary>
    public class AllWidgetsContainer
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public AllWidgetsContainer()
        {
            ClockWidgetContainer = new WidgetContainer<ClockWidget>();
            PictureWidgetContainer = new WidgetContainer<PictureWidget>();
            NoteWidgetContainer = new WidgetContainer<NoteWidget>();
            CalculatorWidgetContainer = new WidgetContainer<CalculatorWidget>();
            BrowserWidgetContainer = new WidgetContainer<BrowserWidget>();
            KeyDetectorWidgetContainer = new WidgetContainer<KeyDetectorWidget>();
        }

        /// <summary>
        /// Constructor with arguments.
        /// </summary>
        /// <param name="clockWidgetContainer">Clock widget container.</param>
        /// <param name="pictureWidgetContainer">Picture widget container.</param>
        /// <param name="noteWidgetContainer">Note widget container.</param>
        /// <param name="calculatorWidgetContainer">Calculator widget container.</param>
        /// <param name="browserWidgetContainer">Browser widget container.</param>
        /// <param name="keyDetectorWidgetContainer">Key detector widget container.</param>
        public AllWidgetsContainer
            (ref WidgetContainer<ClockWidget> clockWidgetContainer,
            ref WidgetContainer<PictureWidget> pictureWidgetContainer,
            ref WidgetContainer<NoteWidget> noteWidgetContainer,
            ref WidgetContainer<CalculatorWidget> calculatorWidgetContainer,
            ref WidgetContainer<BrowserWidget> browserWidgetContainer,
            ref WidgetContainer<KeyDetectorWidget> keyDetectorWidgetContainer)
        {
            ClockWidgetContainer = clockWidgetContainer;
            PictureWidgetContainer = pictureWidgetContainer;
            NoteWidgetContainer = noteWidgetContainer;
            CalculatorWidgetContainer = calculatorWidgetContainer;
            BrowserWidgetContainer = browserWidgetContainer;
            KeyDetectorWidgetContainer = keyDetectorWidgetContainer;
        }

        #region WidgetsContainers

        #region ClockWidgetContainer
        /// <summary>
        /// The clock widget's container.
        /// </summary>
        public WidgetContainer<ClockWidget> ClockWidgetContainer;
        #endregion

        #region PictureWidgetContainer
        /// <summary>
        /// The picture widget's container.
        /// </summary>
        public WidgetContainer<PictureWidget> PictureWidgetContainer;
        #endregion

        #region NoteWidgetContainer
        /// <summary>
        /// The note widget's container.
        /// </summary>
        public WidgetContainer<NoteWidget> NoteWidgetContainer;
        #endregion

        #region CalculatorWidgetContainer
        /// <summary>
        /// The calculator widget's container.
        /// </summary>
        public WidgetContainer<CalculatorWidget> CalculatorWidgetContainer;
        #endregion

        #region BrowserWidgetContainer
        /// <summary>
        /// The browser widget's container.
        /// </summary>
        public WidgetContainer<BrowserWidget> BrowserWidgetContainer;
        #endregion

        #region KeyDetectorWidgetContainer
        /// <summary>
        /// The key detector widget's container.
        /// </summary>
        public WidgetContainer<KeyDetectorWidget> KeyDetectorWidgetContainer;
        #endregion

        #endregion
    }
}
