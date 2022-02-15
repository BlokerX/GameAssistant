using System.Windows;
using System.Windows.Media;

namespace NewGameAssistant.Widgets
{
    /// <summary>
    /// Base of window for Widget.
    /// </summary>
    public abstract class WidgetBase : Window
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public WidgetBase()
        {
            Background = new SolidColorBrush(Color.FromArgb(0,0,0,0));
            AllowsTransparency = true;
            WindowStyle = WindowStyle.None;
            Topmost = true;

            this.MouseLeftButtonDown += DragWindow;
        }

        #region Properties

        private static readonly DependencyProperty IsDragActiveProperty = 
            DependencyProperty.Register
                (
                "IsDragActive",
                typeof(bool),
                typeof(WidgetBase)
                );

        /// <summary>
        /// If true drag window is enable, if false drag window is disable.
        /// </summary>
        public bool IsDragActive
        {
            get => (bool)GetValue(IsDragActiveProperty);
            set => SetValue(IsDragActiveProperty, value);
        }

        #endregion

        /// <summary>
        /// Drag widget if mouse is pressed over window.
        /// </summary>
        private void DragWindow(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (IsDragActive && e.ButtonState == System.Windows.Input.MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
