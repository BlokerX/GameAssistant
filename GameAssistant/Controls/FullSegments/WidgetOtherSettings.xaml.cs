using System;
using System.Windows.Controls;

namespace GameAssistant.Controls.FullSegments
{
    /// <summary>
    /// Logika interakcji dla klasy WidgetOtherSettings.xaml
    /// </summary>
    public partial class WidgetOtherSettings : UserControl
    {
        public WidgetOtherSettings()
        {
            InitializeComponent();

            IsTopmostPropertyValueChanged += (s, b) => { };
            CanResizePropertyValueChanged += (s, b) => { };
            DragActivePropertyValueChanged += (s, b) => { };
        }

        /// <summary>
        ///  On is topmost property value changed.
        /// </summary>
        public event EventHandler<bool?> IsTopmostPropertyValueChanged;
        private void IsTopmostProperty_PropertyValueChanged(object sender, bool? e)
        {
            IsTopmostPropertyValueChanged.Invoke(sender, e);
        }
    
        /// <summary>
        ///  On is can resize property value changed.
        /// </summary>
        public event EventHandler<bool?> CanResizePropertyValueChanged;
        private void CanResizeProperty_PropertyValueChanged(object sender, bool? e)
        {
            CanResizePropertyValueChanged.Invoke(sender, e);
        }

        /// <summary>
        ///  On is drag active property value changed.
        /// </summary>
        public event EventHandler<bool?> DragActivePropertyValueChanged;
        private void DragActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            DragActivePropertyValueChanged.Invoke(sender, e);
        }
    }
}
