using GameAssistant.Core;
using GameAssistant.Models;
using GameAssistant.Services;
using GameAssistant.Widgets;
using GameAssistant.WidgetViewModels;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace GameAssistant.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy ClockSettingsPage.xaml
    /// </summary>
    public partial class ClockSettingsPage : WidgetSettingsPage
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="clockWidget">Widget container with clock widget to use.</param>
        public ClockSettingsPage(ref WidgetContainer<ClockWidget> clockWidget)
        {
            // Register widget change active state event:
            ClockWidget.Events.WidgetActiveChanged += WidgetChangeActiveStateMethodForSettingsPage;

            InitializeComponent();

            ClockWidgetContainer = clockWidget;
            LoadWidget(ref _clockWidgetContainer);

        }

        public override void RemovePageMethodsFromWidgetEvents() => ClockWidget.Events.WidgetActiveChanged -= WidgetChangeActiveStateMethodForSettingsPage;

        public ClockSettingsPage(ref WidgetContainer<ClockWidget> clockWidget, ref bool? clockWidgetState) : this(ref clockWidget)
        {
            ActiveProperty.PropertyValue = clockWidgetState;
        }

        private void WidgetChangeActiveStateMethodForSettingsPage(bool state)
        {
            if (ActiveProperty.PropertyValue != state)
                ActiveProperty.PropertyValue = state;
        }

        #endregion

        /// <summary>
        /// Load widget in settings page.
        /// </summary>
        /// <param name="clockWidgetContainer">Clock widget to load.</param>
        public void LoadWidget(ref WidgetContainer<ClockWidget> clockWidgetContainer)
        {
            if (clockWidgetContainer.Widget == null)
            {
                this.ActiveProperty.PropertyValue = false;
                ActiveChanged(false);
            }
            else
            {
                this.ActiveProperty.PropertyValue = true;
                LoadWidgetSettings(ref clockWidgetContainer);
                ActiveChanged(true);
            }
        }

        private void LoadWidgetSettings(ref WidgetContainer<ClockWidget> clockWidgetContainer)
        {
            var model = (clockWidgetContainer.Widget.DataContext as IWidgetViewModel<ClockModel>).WidgetModel;

            this.BackgroundAnimationBrushProperty.ColorProperty.PropertyColor = model.BackgroundAnimatedBrush.BrushContainer.Variable;
            this.ForegroundAnimationBrushProperty.ColorProperty.PropertyColor = model.ForegroundAnimatedBrush.BrushContainer.Variable;

            this.BackgroundOpacityProperty.PropertyValue = model.BackgroundOpacity;
            this.ForegroundOpacityProperty.PropertyValue = model.ClockLabelOpacity;

            this.BackgroundAnimationBrushProperty.AnimationProperty.SelectedElementIndex = (int)model.BackgroundAnimatedBrush.BrushAnimationManager.Animation;
            this.ForegroundAnimationBrushProperty.AnimationProperty.SelectedElementIndex = (int)model.ForegroundAnimatedBrush.BrushAnimationManager.Animation;

            this.FontSettingsPropertyPanel.PropertyFontFamily = new FontFamily(model.FontFamily);
            this.FontSettingsPropertyPanel.PropertyFontSize = model.FontSize;

            this.IsTopmostProperty.PropertyValue = model.IsTopmost;
            this.CanResizeProperty.PropertyValue = TypeConverter.ResizeModToBool(model.ResizeMode);
            this.DragActiveProperty.PropertyValue = model.IsDragActive;

        }

        //todo uprościć dla wszystkich widgetów
        protected override void ActiveChanged(bool newState)
        {
            this.BackgroundAnimationBrushProperty.IsEnabled = newState;
            this.ForegroundAnimationBrushProperty.IsEnabled = newState;

            this.BackgroundOpacityProperty.IsEnabled = newState;
            this.ForegroundOpacityProperty.IsEnabled = newState;

            this.FontSettingsPropertyPanel.IsEnabled = newState;

            this.IsTopmostProperty.IsEnabled = newState;
            this.CanResizeProperty.IsEnabled = newState;
            this.DragActiveProperty.IsEnabled = newState;
        }

        #region Widget

        public static readonly DependencyProperty PropertyClockWidgetContainer = DependencyProperty.Register(
        "ClockWidgetContainer", typeof(WidgetContainer<ClockWidget>),
        typeof(ClockSettingsPage)
        );

        private WidgetContainer<ClockWidget> _clockWidgetContainer;

        /// <summary>
        /// The clock container with clock widget.
        /// </summary>
        public WidgetContainer<ClockWidget> ClockWidgetContainer
        {
            get => _clockWidgetContainer;
            set => SetProperty(ref _clockWidgetContainer, value);
        }

        #endregion

        #region PropertyChangedMethods

        private void BackgroundColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (ClockWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<ClockWidget, ClockModel>(ref ClockWidgetContainer.Widget);
                model.BackgroundAnimatedBrush.BrushContainer.Variable = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void BackgroundAnimationProperty_PropertyValueChanged(object sender, int e)
        {
            if (ClockWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<ClockWidget, ClockModel>(ref ClockWidgetContainer.Widget);
                model.BackgroundAnimatedBrush.BrushAnimationManager.Animation = (AnimationManager.AnimationType)e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void ForegroundColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (ClockWidgetContainer.Widget.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<ClockWidget, ClockModel>(ref ClockWidgetContainer.Widget);
                model.ForegroundAnimatedBrush.BrushContainer.Variable = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void ForegroundAnimationProperty_PropertyValueChanged(object sender, int e)
        {
            if (ClockWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<ClockWidget, ClockModel>(ref ClockWidgetContainer.Widget);
                model.ForegroundAnimatedBrush.BrushAnimationManager.Animation = (AnimationManager.AnimationType)e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void BackgroundOpacityProperty_PropertyValueChanged(object sender, double e)
        {
            if (ClockWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<ClockWidget, ClockModel>(ref ClockWidgetContainer.Widget);
                model.BackgroundOpacity = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void ForegroundOpacityProperty_PropertyValueChanged(object sender, double e)
        {
            if (ClockWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<ClockWidget, ClockModel>(ref ClockWidgetContainer.Widget);
                model.ClockLabelOpacity = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void IsTopmostProperty_PropertyValueChanged(object sender, bool? e)
        {
            if (ClockWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<ClockWidget, ClockModel>(ref ClockWidgetContainer.Widget);
                model.IsTopmost = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }
        
        private void CanResizeProperty_PropertyValueChanged(object sender, bool? e)
        {
            if (ClockWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<ClockWidget, ClockModel>(ref ClockWidgetContainer.Widget);
                model.ResizeMode = TypeConverter.BoolToResizeMod(e);
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void DragActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            if (ClockWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<ClockWidget, ClockModel>(ref ClockWidgetContainer.Widget);
                model.IsDragActive = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void FontSettingsPropertyPanel_PropertyValueChanged(object sender, (FontFamily, double) e)
        {
            if (ClockWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<ClockWidget, ClockModel>(ref ClockWidgetContainer.Widget);
                model.FontFamily = e.Item1.ToString();
                model.FontSize = e.Item2;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        protected override void ActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            ClockWidget.Events.WidgetActiveChanged_Invoke((bool)e);

            if (_clockWidgetContainer.Widget != null)
            {
                LoadWidgetSettings(ref _clockWidgetContainer);
            }
            ActiveChanged((bool)e);
        }

        #endregion

        protected override void DefaultSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            _clockWidgetContainer.ResetWidgetConfigurationToDefaultWithMessageBox<ClockWidget, ClockViewModel, ClockModel>(() => LoadWidget(ref _clockWidgetContainer));
        }

        protected override void OpenSaveConfigurationDireButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("Explorer", AppFileSystem.GetWidgetConfigurationDirePath(typeof(ClockWidget).Name));
        }

        protected override void LoadSavedConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            _clockWidgetContainer.LoadConfigurationFromFile<ClockWidget, ClockViewModel, ClockModel>(() => LoadWidget(ref _clockWidgetContainer));
        }
    }
}
