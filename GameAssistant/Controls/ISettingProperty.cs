using System.Windows.Media;

namespace GameAssistant.Controls
{
    /// <summary>
    /// Base of setting property control.
    /// </summary>
    internal interface ISettingProperty
    {
        /// <summary>
        /// Name of property.
        /// </summary>
        string PropertyName { get; set; }

        /// <summary>
        /// Color of property name label.
        /// </summary>
        Brush ForegroundColor { set; }

        /// <summary>
        /// Color of grid's border.
        /// </summary>
        Brush BorderColor { get; set; }

        /// <summary>
        /// Color of grid's slots.
        /// </summary>
        Brush BackgorundColor { set; }
    }
}
