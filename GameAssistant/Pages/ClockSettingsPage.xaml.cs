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
    public partial class ClockSettingsPage : SettingsPageBase
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="clockWidget">Widget container with clock widget to use.</param>
        public ClockSettingsPage(ref WidgetContainer<ClockWidget> clockWidget)
        {
            InitializeComponent();

            ClockWidgetContainer = clockWidget;
            LoadWidget(ref _clockWidgetContainer);
        }

        public ClockSettingsPage(ref WidgetContainer<ClockWidget> clockWidget, ref bool? clockWidgetState)
        {
            InitializeComponent();

            ClockWidgetContainer = clockWidget;
            LoadWidget(ref _clockWidgetContainer);
            ActiveProperty.PropertyValue = clockWidgetState;
        }

        #endregion

        /// <summary>
        /// Load widget in settings page.
        /// </summary>
        /// <param name="clockWidgetContainer">Clock widget to load.</param>
        private void LoadWidget(ref WidgetContainer<ClockWidget> clockWidgetContainer)
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

            this.BackgroundColorProperty.PropertyColor = model.BackgroundAnimatedBrush.BrushBackgroundContainer.Variable;
            this.ForegroundColorProperty.PropertyColor = model.ForegroundAnimatedBrush.BrushBackgroundContainer.Variable;

            this.BackgroundOpacityProperty.PropertyValue = model.BackgroundOpacity;
            this.ForegroundOpacityProperty.PropertyValue = model.ClockLabelOpacity;

            this.FontSettingsPropertyPanel.PropertyFontFamily = new FontFamily(model.FontFamily);
            this.FontSettingsPropertyPanel.PropertyFontSize = model.FontSize;

            this.CanResizeProperty.PropertyValue = TypeConverter.ResizeModToBool(model.ResizeMode);
            this.DragActiveProperty.PropertyValue = model.IsDragActive;
        }

        protected override void ActiveChanged(bool newState)
        {
            this.BackgroundColorProperty.IsEnabled = newState;
            this.ForegroundColorProperty.IsEnabled = newState;

            this.BackgroundOpacityProperty.IsEnabled = newState;
            this.ForegroundOpacityProperty.IsEnabled = newState;

            this.FontSettingsPropertyPanel.IsEnabled = newState;

            this.CanResizeProperty.IsEnabled = newState;
            this.DragActiveProperty.IsEnabled = newState;
        }

        #region Widget

        public static readonly DependencyProperty PropertyColorProperty = DependencyProperty.Register(
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
                model.BackgroundAnimatedBrush.BrushBackgroundContainer.Variable = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void ForegroundColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (ClockWidgetContainer.Widget.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<ClockWidget, ClockModel>(ref ClockWidgetContainer.Widget);
                model.ForegroundAnimatedBrush.BrushBackgroundContainer.Variable = e;
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
            WidgetManager.SaveWidgetConfigurationInFile<ClockWidget, ClockModel>(_clockWidgetContainer.Widget);

            var downloadedConfigurationResult = WidgetManager.DownloadWidgetConfigurationFromFile(out ClockModel model);
            switch (e)
            {
                case true:
                    if (_clockWidgetContainer.Widget == null)
                        WidgetManager.BuildWidget<ClockWidget, ClockViewModel, ClockModel>(ref _clockWidgetContainer.Widget, ref model, downloadedConfigurationResult);
                    break;

                case false:
                    if (_clockWidgetContainer.Widget != null)
                        WidgetManager.CloseWidget(ref _clockWidgetContainer.Widget, ref model);
                    break;
            }
            WidgetManager.SaveWidgetConfigurationInFile(model);

            if (_clockWidgetContainer.Widget != null)
            {
                LoadWidgetSettings(ref _clockWidgetContainer);
            }
            ActiveChanged((bool)e);
        }

        #endregion

        protected override void DefaultSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Should you set widget configuration to default?\n(Warning, if you restore the default settings you will not be able to restore the current data.)", "Setting configuration to default:", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                // todo przywracanie ustawień domyślnych i zapisywanie ich
                if (_clockWidgetContainer.Widget != null)
                {
                    WidgetManager.CloseWidget<ClockWidget, ClockModel>(ref _clockWidgetContainer.Widget);
                }
                _clockWidgetContainer.Widget = new ClockWidget();
                _clockWidgetContainer.Widget.Show();
                LoadWidget(ref _clockWidgetContainer);
                WidgetManager.SaveWidgetConfigurationInFile<ClockWidget, ClockModel>(_clockWidgetContainer.Widget);
            }
        }

        protected override void OpenSaveConfigurationDireButton_Click(object sender, RoutedEventArgs e)
        {
            // todo zabezpieczyć
            Process.Start("Explorer", AppFileSystem.GetSaveDireConfigurationPath(typeof(ClockWidget).Name));
        }

        protected override void LoadSavedConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
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
                    var model = new ClockModel();
                    using (var sr = File.OpenText(fileDialog.FileName))
                    {
                        model = JsonConvert.DeserializeObject<ClockModel>(sr.ReadToEnd());
                        WidgetManager.SaveWidgetConfigurationInFile(model);
                    }

                    if (_clockWidgetContainer.Widget != null)
                    {
                        WidgetManager.CloseWidget<ClockWidget, ClockModel>(ref _clockWidgetContainer.Widget);
                    }
                    WidgetManager.LoadWidget<ClockWidget, ClockViewModel, ClockModel>(ref _clockWidgetContainer.Widget);
                    _clockWidgetContainer.Widget?.Show();
                    LoadWidget(ref _clockWidgetContainer);
                }
            }
        }
    }
}
