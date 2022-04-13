using System;

namespace GameAssistant.Services
{
    /// <summary>
    /// Contains events and invoked methods.
    /// </summary>
    public class WidgetEvents
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public WidgetEvents()
        {
            WidgetActiveChanged += (x) => { };
        }

        /// <summary>
        /// On widget active changed.
        /// </summary>
        public event Action<bool> WidgetActiveChanged;

        /// <summary>
        /// Invoke WidgetActiveChanged event.
        /// </summary>
        /// <param name="state"></param>
        public void WidgetActiveChanged_Invoke(bool state)
        {
            WidgetActiveChanged?.Invoke(state);
        }
    }
}
