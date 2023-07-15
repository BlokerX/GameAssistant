using CefSharp;
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
    /// Logika interakcji dla klasy SpotifySettingsPage.xaml
    /// </summary>
    public partial class SpotifySettingsPage : WidgetSettingsPage
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="spotifyWidget">Widget container with spotify widget to use.</param>
        public SpotifySettingsPage(ref WidgetContainer<SpotifyWidget> spotifyWidget)
        {
            // Register widget change active state event:
            SpotifyWidget.Events.WidgetActiveChanged += WidgetChangeActiveStateMethodForSettingsPage;

            InitializeComponent();

            SpotifyWidgetContainer = spotifyWidget;
            LoadWidget(ref _spotifyWidgetContainer);
        }

        public SpotifySettingsPage(ref WidgetContainer<SpotifyWidget> spotifyWidget, ref bool? spotifyWidgetState) : this(ref spotifyWidget)
        {
            ActiveProperty.PropertyValue = spotifyWidgetState;
        }

        public override void RemovePageMethodsFromWidgetEvents() => SpotifyWidget.Events.WidgetActiveChanged -= WidgetChangeActiveStateMethodForSettingsPage;

        private void WidgetChangeActiveStateMethodForSettingsPage(bool state)
        {
            if (ActiveProperty.PropertyValue != state)
                ActiveProperty.PropertyValue = state;
        }

        #endregion

        /// <summary>
        /// Load widget in settings page.
        /// </summary>
        /// <param name="spotifyWidgetContainer">Spotify widget to load.</param>
        public void LoadWidget(ref WidgetContainer<SpotifyWidget> spotifyWidgetContainer)
        {
            if (SpotifyWidgetContainer.Widget == null)
            {
                this.ActiveProperty.PropertyValue = false;
                ActiveChanged(false);
            }
            else
            {
                this.ActiveProperty.PropertyValue = true;
                LoadWidgetSettings(ref spotifyWidgetContainer);
                ActiveChanged(true);
            }
        }

        private void LoadWidgetSettings(ref WidgetContainer<SpotifyWidget> spotifyWidgetContainer)
        {
            var model = (spotifyWidgetContainer.Widget.DataContext as IWidgetViewModel<SpotifyModel>).WidgetModel;

            this.BackgroundAnimationBrushProperty.ColorProperty.PropertyColor = model.BackgroundAnimatedBrush.BrushContainer.Variable;
            this.BackgroundAnimationBrushProperty.AnimationProperty.SelectedElementIndex = (int)model.BackgroundAnimatedBrush.BrushAnimationManager.Animation;

            this.BackgroundOpacityProperty.PropertyValue = model.BackgroundOpacity;
            this.SpotifyOpacityProperty.PropertyValue = model.SpotifyOpacity;

            this.IsTopmostProperty.PropertyValue = model.IsTopmost;
            this.CanResizeProperty.PropertyValue = TypeConverter.ResizeModToBool(model.ResizeMode);
            this.DragActiveProperty.PropertyValue = model.IsDragActive;

            this.BrowserAddressProperty.PropertyValue = model.BrowserAddress;
            this.ZoomLevelProperty.PropertyValue = model.ZoomLevel;
        }

        protected override void ActiveChanged(bool newState)
        {
            this.BackgroundAnimationBrushProperty.IsEnabled = newState;

            this.BackgroundOpacityProperty.IsEnabled = newState;
            this.SpotifyOpacityProperty.IsEnabled = newState;

            this.IsTopmostProperty.IsEnabled = newState;
            this.CanResizeProperty.IsEnabled = newState;
            this.DragActiveProperty.IsEnabled = newState;

            this.BrowserAddressProperty.IsEnabled = newState;
            this.ZoomLevelProperty.IsEnabled = newState;
        }

        #region Widget

        public static readonly DependencyProperty PropertyColorProperty = DependencyProperty.Register(
        "SpotifyWidgetContainer", typeof(WidgetContainer<SpotifyWidget>),
        typeof(SpotifySettingsPage)
        );

        private WidgetContainer<SpotifyWidget> _spotifyWidgetContainer;
        /// <summary>
        /// The spotify container with spotify widget.
        /// </summary>
        public WidgetContainer<SpotifyWidget> SpotifyWidgetContainer
        {
            get => _spotifyWidgetContainer;
            set => SetProperty(ref _spotifyWidgetContainer, value);
        }

        #endregion

        #region PropertyChangedMethods

        private void BackgroundColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (SpotifyWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<SpotifyWidget, SpotifyModel>(ref SpotifyWidgetContainer.Widget);
                model.BackgroundAnimatedBrush.BrushContainer.Variable = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }
        private void BackgroundAnimationProperty_PropertyValueChanged(object sender, int e)
        {
            if (SpotifyWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<SpotifyWidget, SpotifyModel>(ref SpotifyWidgetContainer.Widget);
                model.BackgroundAnimatedBrush.BrushAnimationManager.Animation = (AnimationManager.AnimationType)e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void BackgroundOpacityProperty_PropertyValueChanged(object sender, double e)
        {
            if (SpotifyWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<SpotifyWidget, SpotifyModel>(ref SpotifyWidgetContainer.Widget);
                model.BackgroundOpacity = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void SpotifyOpacityProperty_PropertyValueChanged(object sender, double e)
        {
            if (SpotifyWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<SpotifyWidget, SpotifyModel>(ref SpotifyWidgetContainer.Widget);
                model.SpotifyOpacity = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void IsTopmostProperty_PropertyValueChanged(object sender, bool? e)
        {
            if (SpotifyWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<SpotifyWidget, SpotifyModel>(ref SpotifyWidgetContainer.Widget);
                model.IsTopmost = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void CanResizeProperty_PropertyValueChanged(object sender, bool? e)
        {
            if (SpotifyWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<SpotifyWidget, SpotifyModel>(ref SpotifyWidgetContainer.Widget);
                model.ResizeMode = TypeConverter.BoolToResizeMod(e);
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void DragActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            if (SpotifyWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<SpotifyWidget, SpotifyModel>(ref SpotifyWidgetContainer.Widget);
                model.IsDragActive = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void BrowserAddressProperty_PropertyValueChanged(object sender, string e)
        {
            if (SpotifyWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<SpotifyWidget, SpotifyModel>(ref SpotifyWidgetContainer.Widget);
                model.BrowserAddress = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void ZoomLevelProperty_PropertyValueChanged(object sender, double e)
        {
            if (SpotifyWidgetContainer.Widget?.DataContext != null)
            {
                SpotifyWidgetContainer.Widget.browserWindow.SetZoomLevel((SpotifyWidgetContainer.Widget.DataContext as SpotifyViewModel).WidgetModel.ZoomLevel = e);
                var model = WidgetManager.GetModelFromWidget<SpotifyWidget, SpotifyModel>(ref SpotifyWidgetContainer.Widget);
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        protected override void ActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            SpotifyWidget.Events.WidgetActiveChanged_Invoke((bool)e);

            if (_spotifyWidgetContainer.Widget != null)
            {
                LoadWidgetSettings(ref _spotifyWidgetContainer);
            }
            ActiveChanged((bool)e);
        }

        #endregion

        protected override void DefaultSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            _spotifyWidgetContainer.ResetWidgetConfigurationToDefaultWithMessageBox<SpotifyWidget, SpotifyViewModel, SpotifyModel>(() => LoadWidget(ref _spotifyWidgetContainer));
        }

        protected override void OpenSaveConfigurationDireButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("Explorer", AppFileSystem.GetWidgetConfigurationDirePath(typeof(SpotifyWidget).Name));
        }

        protected override void LoadSavedConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            _spotifyWidgetContainer.LoadConfigurationFromFile<SpotifyWidget, SpotifyViewModel, SpotifyModel>(() => LoadWidget(ref _spotifyWidgetContainer));
        }

    }
}
