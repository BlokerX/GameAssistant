using GameAssistant.Core;
using GameAssistant.Models;
using GameAssistant.Services;
using GameAssistant.Widgets;
using GameAssistant.WidgetViewModels;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace GameAssistant.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy KeyDetectorSettingsPage.xaml
    /// </summary>
    public partial class KeyDetectorSettingsPage : WidgetSettingsPage
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="keyDetectorWidget">Widget container with keyDetector widget to use.</param>
        public KeyDetectorSettingsPage(ref WidgetContainer<KeyDetectorWidget> keyDetectorWidget)
        {
            // Register widget change active state event:
            KeyDetectorWidget.Events.WidgetActiveChanged += WidgetChangeActiveStateMethodForSettingsPage;

            InitializeComponent();

            KeyDetectorWidgetContainer = keyDetectorWidget;
            LoadWidget(ref _keyDetectorWidgetContainer);

        }

        public override void RemovePageMethodsFromWidgetEvents() => KeyDetectorWidget.Events.WidgetActiveChanged -= WidgetChangeActiveStateMethodForSettingsPage;

        public KeyDetectorSettingsPage(ref WidgetContainer<KeyDetectorWidget> keyDetectorWidget, ref bool? keyDetectorWidgetState) : this(ref keyDetectorWidget)
        {
            ActiveProperty.PropertyValue = keyDetectorWidgetState;
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
        /// <param name="keyDetectorWidgetContainer">KeyDetector widget to load.</param>
        public void LoadWidget(ref WidgetContainer<KeyDetectorWidget> keyDetectorWidgetContainer)
        {
            if (keyDetectorWidgetContainer.Widget == null)
            {
                this.ActiveProperty.PropertyValue = false;
                ActiveChanged(false);
            }
            else
            {
                this.ActiveProperty.PropertyValue = true;
                LoadWidgetSettings(ref keyDetectorWidgetContainer);
                ActiveChanged(true);
            }
        }

        private void LoadWidgetSettings(ref WidgetContainer<KeyDetectorWidget> keyDetectorWidgetContainer)
        {
            var model = (keyDetectorWidgetContainer.Widget.DataContext as IWidgetViewModel<KeyDetectorModel>).WidgetModel;

            this.BackgroundAnimationBrushProperty.ColorProperty.PropertyColor = model.BackgroundAnimatedBrush.BrushContainer.Variable;
            this.ForegroundAnimationBrushProperty.ColorProperty.PropertyColor = model.ForegroundAnimatedBrush.BrushContainer.Variable;
            this.DetectPanelAnimationBrushProperty.ColorProperty.PropertyColor = model.DetectPanelAnimatedBrush.BrushContainer.Variable;

            this.BackgroundOpacityProperty.PropertyValue = model.BackgroundOpacity;
            this.ForegroundOpacityProperty.PropertyValue = model.DetectPanelOpacity;

            this.BackgroundAnimationBrushProperty.AnimationProperty.SelectedElementIndex = (int)model.BackgroundAnimatedBrush.BrushAnimationManager.Animation;
            this.ForegroundAnimationBrushProperty.AnimationProperty.SelectedElementIndex = (int)model.ForegroundAnimatedBrush.BrushAnimationManager.Animation;
            this.DetectPanelAnimationBrushProperty.AnimationProperty.SelectedElementIndex = (int)model.DetectPanelAnimatedBrush.BrushAnimationManager.Animation;

            this.FontSettingsPropertyPanel.PropertyFontFamily = new FontFamily(model.FontFamily);
            this.FontSettingsPropertyPanel.PropertyFontSize = model.FontSize;

            this.IsTopmostProperty.PropertyValue = model.IsTopmost;
            this.CanResizeProperty.PropertyValue = TypeConverter.ResizeModToBool(model.ResizeMode);
            this.DragActiveProperty.PropertyValue = model.IsDragActive;

        }

        protected override void ActiveChanged(bool newState)
        {
            this.BackgroundAnimationBrushProperty.IsEnabled = newState;
            this.ForegroundAnimationBrushProperty.IsEnabled = newState;
            this.DetectPanelAnimationBrushProperty.IsEnabled = newState;

            this.BackgroundOpacityProperty.IsEnabled = newState;
            this.ForegroundOpacityProperty.IsEnabled = newState;

            this.FontSettingsPropertyPanel.IsEnabled = newState;

            this.IsTopmostProperty.IsEnabled = newState;
            this.CanResizeProperty.IsEnabled = newState;
            this.DragActiveProperty.IsEnabled = newState;
        }

        #region Widget

        public static readonly DependencyProperty PropertyKeyDetectorWidgetContainer = DependencyProperty.Register(
        "KeyDetectorWidgetContainer", typeof(WidgetContainer<KeyDetectorWidget>),
        typeof(KeyDetectorSettingsPage)
        );

        private WidgetContainer<KeyDetectorWidget> _keyDetectorWidgetContainer;

        /// <summary>
        /// The keyDetector container with keyDetector widget.
        /// </summary>
        public WidgetContainer<KeyDetectorWidget> KeyDetectorWidgetContainer
        {
            get => _keyDetectorWidgetContainer;
            set => SetProperty(ref _keyDetectorWidgetContainer, value);
        }

        #endregion

        #region PropertyChangedMethods

        private void BackgroundColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (KeyDetectorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<KeyDetectorWidget, KeyDetectorModel>(ref KeyDetectorWidgetContainer.Widget);
                model.BackgroundAnimatedBrush.BrushContainer.Variable = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void BackgroundAnimationProperty_PropertyValueChanged(object sender, int e)
        {
            if (KeyDetectorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<KeyDetectorWidget, KeyDetectorModel>(ref KeyDetectorWidgetContainer.Widget);
                model.BackgroundAnimatedBrush.BrushAnimationManager.Animation = (AnimationManager.AnimationType)e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void ForegroundColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (KeyDetectorWidgetContainer.Widget.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<KeyDetectorWidget, KeyDetectorModel>(ref KeyDetectorWidgetContainer.Widget);
                model.ForegroundAnimatedBrush.BrushContainer.Variable = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void ForegroundAnimationProperty_PropertyValueChanged(object sender, int e)
        {
            if (KeyDetectorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<KeyDetectorWidget, KeyDetectorModel>(ref KeyDetectorWidgetContainer.Widget);
                model.ForegroundAnimatedBrush.BrushAnimationManager.Animation = (AnimationManager.AnimationType)e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void DetectPanelColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (KeyDetectorWidgetContainer.Widget.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<KeyDetectorWidget, KeyDetectorModel>(ref KeyDetectorWidgetContainer.Widget);
                model.DetectPanelAnimatedBrush.BrushContainer.Variable = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void DetectPanelAnimationProperty_PropertyValueChanged(object sender, int e)
        {
            if (KeyDetectorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<KeyDetectorWidget, KeyDetectorModel>(ref KeyDetectorWidgetContainer.Widget);
                model.DetectPanelAnimatedBrush.BrushAnimationManager.Animation = (AnimationManager.AnimationType)e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void BackgroundOpacityProperty_PropertyValueChanged(object sender, double e)
        {
            if (KeyDetectorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<KeyDetectorWidget, KeyDetectorModel>(ref KeyDetectorWidgetContainer.Widget);
                model.BackgroundOpacity = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void ForegroundOpacityProperty_PropertyValueChanged(object sender, double e)
        {
            if (KeyDetectorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<KeyDetectorWidget, KeyDetectorModel>(ref KeyDetectorWidgetContainer.Widget);
                model.DetectPanelOpacity = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void IsTopmostProperty_PropertyValueChanged(object sender, bool? e)
        {
            if (KeyDetectorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<KeyDetectorWidget, KeyDetectorModel>(ref KeyDetectorWidgetContainer.Widget);
                model.IsTopmost = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void CanResizeProperty_PropertyValueChanged(object sender, bool? e)
        {
            if (KeyDetectorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<KeyDetectorWidget, KeyDetectorModel>(ref KeyDetectorWidgetContainer.Widget);
                model.ResizeMode = TypeConverter.BoolToResizeMod(e);
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void DragActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            if (KeyDetectorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<KeyDetectorWidget, KeyDetectorModel>(ref KeyDetectorWidgetContainer.Widget);
                model.IsDragActive = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void FontSettingsPropertyPanel_PropertyValueChanged(object sender, (FontFamily, double) e)
        {
            if (KeyDetectorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<KeyDetectorWidget, KeyDetectorModel>(ref KeyDetectorWidgetContainer.Widget);
                model.FontFamily = e.Item1.ToString();
                model.FontSize = e.Item2;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        protected override void ActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            KeyDetectorWidget.Events.WidgetActiveChanged_Invoke((bool)e);

            if (_keyDetectorWidgetContainer.Widget != null)
            {
                LoadWidgetSettings(ref _keyDetectorWidgetContainer);
            }
            ActiveChanged((bool)e);
        }

        #endregion

        protected override void DefaultSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            _keyDetectorWidgetContainer.ResetWidgetConfigurationToDefaultWithMessageBox<KeyDetectorWidget, KeyDetectorViewModel, KeyDetectorModel>(() => LoadWidget(ref _keyDetectorWidgetContainer));
        }

        protected override void OpenSaveConfigurationDireButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("Explorer", AppFileSystem.GetWidgetConfigurationDirePath(typeof(KeyDetectorWidget).Name));
        }

        protected override void LoadSavedConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            _keyDetectorWidgetContainer.LoadConfigurationFromFile<KeyDetectorWidget, KeyDetectorViewModel, KeyDetectorModel>(() => LoadWidget(ref _keyDetectorWidgetContainer));
        }
    }
}
