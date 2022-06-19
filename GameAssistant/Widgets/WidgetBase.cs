using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shell;

namespace GameAssistant.Widgets
{
    /// <summary>
    /// Base of window for Widget.
    /// </summary>
    public abstract class WidgetBase : Window
    {
        //protected WidgetModelBase widgetModel;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public WidgetBase()
        {
            WindowChrome.SetWindowChrome(this, new WindowChrome() { CaptionHeight = 0 });

            Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            AllowsTransparency = true;
            WindowStyle = WindowStyle.None;
            Topmost = true;
            ShowInTaskbar = false;

            this.MouseLeftButtonDown += DragWindow;
            this.SizeChanged += WidgetSizeChanged;
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
                WindowSizeOrPositionChangedEvent?.Invoke();
            }
        }

        /// <summary>
        /// Widget size changed method.
        /// </summary>  
        private void WidgetSizeChanged(object sender, SizeChangedEventArgs e)
        {
            WindowSizeOrPositionChangedEvent?.Invoke();
        }

        /// <summary>
        /// Event invokes when drag window or window is resizing.
        /// </summary>
        protected event Action WindowSizeOrPositionChangedEvent;

    }
}
