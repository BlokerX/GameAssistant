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
    /// Logika interakcji dla klasy BrowserSettingsPage.xaml
    /// </summary>
    public partial class BrowserSettingsPage : WidgetSettingsPage
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="browserWidget">Widget container with browser widget to use.</param>
        public BrowserSettingsPage(ref WidgetContainer<BrowserWidget> browserWidget)
        {
            // Register widget change active state event:
            BrowserWidget.Events.WidgetActiveChanged += WidgetChangeActiveStateMethodForSettingsPage;

            InitializeComponent();

            BrowserWidgetContainer = browserWidget;
            LoadWidget(ref _browserWidgetContainer);
        }

        public BrowserSettingsPage(ref WidgetContainer<BrowserWidget> browserWidget, ref bool? browserWidgetState) : this(ref browserWidget)
        {
            ActiveProperty.PropertyValue = browserWidgetState;
        }

        public override void RemovePageMethodsFromWidgetEvents() => BrowserWidget.Events.WidgetActiveChanged -= WidgetChangeActiveStateMethodForSettingsPage;

        private void WidgetChangeActiveStateMethodForSettingsPage(bool state)
        {
            if (ActiveProperty.PropertyValue != state)
                ActiveProperty.PropertyValue = state;
        }

        #endregion

        /// <summary>
        /// Load widget in settings page.
        /// </summary>
        /// <param name="browserWidgetContainer">Browser widget to load.</param>
        private void LoadWidget(ref WidgetContainer<BrowserWidget> browserWidgetContainer)
        {
            if (BrowserWidgetContainer.Widget == null)
            {
                this.ActiveProperty.PropertyValue = false;
                ActiveChanged(false);
            }
            else
            {
                this.ActiveProperty.PropertyValue = true;
                LoadWidgetSettings(ref browserWidgetContainer);
                ActiveChanged(true);
            }
        }

        private void LoadWidgetSettings(ref WidgetContainer<BrowserWidget> browserWidgetContainer)
        {
            var model = (browserWidgetContainer.Widget.DataContext as IWidgetViewModel<BrowserModel>).WidgetModel;

            this.BackgroundColorProperty.PropertyColor = model.BackgroundAnimatedBrush.BrushContainer.Variable;

            this.BackgroundAnimationProperty.SelectedElementIndex = (int)model.BackgroundAnimatedBrush.BrushAnimationManager.Animation;

            this.BackgroundOpacityProperty.PropertyValue = model.BackgroundOpacity;
            this.BrowserOpacityProperty.PropertyValue = model.BrowserOpacity;

            this.CanResizeProperty.PropertyValue = TypeConverter.ResizeModToBool(model.ResizeMode);
            this.DragActiveProperty.PropertyValue = model.IsDragActive;

            this.AddressProperty.PropertyValue = model.Address;
        }

        protected override void ActiveChanged(bool newState)
        {
            this.BackgroundColorProperty.IsEnabled = newState;

            this.BackgroundOpacityProperty.IsEnabled = newState;
            this.BrowserOpacityProperty.IsEnabled = newState;

            this.BackgroundAnimationProperty.IsEnabled = newState;

            this.CanResizeProperty.IsEnabled = newState;
            this.DragActiveProperty.IsEnabled = newState;

            this.AddressProperty.IsEnabled = newState;
        }

        #region Widget

        public static readonly DependencyProperty PropertyColorProperty = DependencyProperty.Register(
        "BrowserWidgetContainer", typeof(WidgetContainer<BrowserWidget>),
        typeof(BrowserSettingsPage)
        );

        private WidgetContainer<BrowserWidget> _browserWidgetContainer;
        /// <summary>
        /// The browser container with browser widget.
        /// </summary>
        public WidgetContainer<BrowserWidget> BrowserWidgetContainer
        {
            get => _browserWidgetContainer;
            set => SetProperty(ref _browserWidgetContainer, value);
        }

        #endregion

        #region PropertyChangedMethods

        private void BackgroundColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (BrowserWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<BrowserWidget, BrowserModel>(ref BrowserWidgetContainer.Widget);
                model.BackgroundAnimatedBrush.BrushContainer.Variable = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }
        private void BackgroundAnimationProperty_PropertyValueChanged(object sender, int e)
        {
            if (BrowserWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<BrowserWidget, BrowserModel>(ref BrowserWidgetContainer.Widget);
                model.BackgroundAnimatedBrush.BrushAnimationManager.Animation = (AnimationManager.AnimationType)e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void BackgroundOpacityProperty_PropertyValueChanged(object sender, double e)
        {
            if (BrowserWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<BrowserWidget, BrowserModel>(ref BrowserWidgetContainer.Widget);
                model.BackgroundOpacity = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void BrowserOpacityProperty_PropertyValueChanged(object sender, double e)
        {
            if (BrowserWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<BrowserWidget, BrowserModel>(ref BrowserWidgetContainer.Widget);
                model.BrowserOpacity = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void CanResizeProperty_PropertyValueChanged(object sender, bool? e)
        {
            if (BrowserWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<BrowserWidget, BrowserModel>(ref BrowserWidgetContainer.Widget);
                model.ResizeMode = TypeConverter.BoolToResizeMod(e);
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void DragActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            if (BrowserWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<BrowserWidget, BrowserModel>(ref BrowserWidgetContainer.Widget);
                model.IsDragActive = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void AddressProperty_PropertyValueChanged(object sender, string e)
        {
            if (BrowserWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<BrowserWidget, BrowserModel>(ref BrowserWidgetContainer.Widget);
                model.Address = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        protected override void ActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            BrowserWidget.Events.WidgetActiveChanged_Invoke((bool)e);

            if (_browserWidgetContainer.Widget != null)
            {
                LoadWidgetSettings(ref _browserWidgetContainer);
            }
            ActiveChanged((bool)e);
        }

        #endregion

        protected override void DefaultSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Should you set widget configuration to default?\n(Warning, if you restore the default settings you will not be able to restore the current data.)", "Setting configuration to default:", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                if (_browserWidgetContainer.Widget != null)
                {
                    WidgetManager.CloseWidget<BrowserWidget, BrowserModel>(ref _browserWidgetContainer.Widget);
                }
                _browserWidgetContainer.Widget = WidgetManager.CreateWidget<BrowserWidget, BrowserViewModel, BrowserModel>(new BrowserModel());
                _browserWidgetContainer.Widget.Show();
                LoadWidget(ref _browserWidgetContainer);
                WidgetManager.SaveWidgetConfigurationInFile<BrowserWidget, BrowserModel>(_browserWidgetContainer.Widget);
            }
        }

        protected override void OpenSaveConfigurationDireButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("Explorer", AppFileSystem.GetSaveDireConfigurationPath(typeof(BrowserWidget).Name));
        }

        protected override void LoadSavedConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            // todo ujednolicić sposób wybierania plików json dla wszystkich widgetów (load saved config)
            var fileDialog = new System.Windows.Forms.OpenFileDialog()
            {
                Filter =
                "JSON files (*.json)|*.json" +
                "|All files (*.*)|*.*",

                Multiselect = false,

                Title = "Select save with widget configuration:"
            };

            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (MessageBox.Show("Should you change widget configuration?\n(Warning, if you change configuration settings without backup you will not be able to restore the current data.)", "Change setting configuration:", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                {
                    var model = new BrowserModel();
                    using (var sr = File.OpenText(fileDialog.FileName))
                    {
                        model = JsonConvert.DeserializeObject<BrowserModel>(sr.ReadToEnd());
                        WidgetManager.SaveWidgetConfigurationInFile(model);
                    }

                    if (_browserWidgetContainer.Widget != null)
                    {
                        WidgetManager.CloseWidget<BrowserWidget, BrowserModel>(ref _browserWidgetContainer.Widget);
                    }
                    WidgetManager.LoadWidget<BrowserWidget, BrowserViewModel, BrowserModel>(ref _browserWidgetContainer.Widget);
                    _browserWidgetContainer.Widget?.Show();
                    LoadWidget(ref _browserWidgetContainer);
                }
            }
        }
    }
}
