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
    /// Logika interakcji dla klasy ClockSettingsPage.xaml
    /// </summary>
    public partial class ClockSettingsPage : SettingsPageBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="clockWidget">Widget container with clock widget to use.</param>
        public ClockSettingsPage(ref WidgetContainer<ClockWidget> clockWidget)
        {
            InitializeComponent();

            ClockWidgetContainer = clockWidget;
            LoadWidget(ClockWidgetContainer);
        }

        public ClockSettingsPage(ref WidgetContainer<ClockWidget> clockWidget, ref bool? clockWidgetState)
        {
            InitializeComponent();

            ClockWidgetContainer = clockWidget;
            LoadWidget(ClockWidgetContainer);
            ActiveProperty.PropertyValue = clockWidgetState;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clockWidgetContainer"></param>
        private void LoadWidget(WidgetContainer<ClockWidget> clockWidgetContainer)
        {
            if (ClockWidgetContainer.Widget == null)
            {
                this.ActiveProperty.PropertyValue = false;
                ActiveChanged(false);
            }
            else
            {
                this.ActiveProperty.PropertyValue = true;

                //this.DataContext = new ClockSettingsViewModel(ref clockWidgetContainer);
                var model = (clockWidgetContainer.Widget.DataContext as IWidgetViewModel<ClockModel>).WidgetModel;

                //todo problem's solution
                this.BackgroundColorProperty.PropertyColor = model.BackgroundColor;
                this.ForegroundColorProperty.PropertyColor = model.ClockLabelForeground;

                this.BackgroundOpacityProperty.PropertyValue = model.BackgroundOpacity;
                this.ForegroundOpacityProperty.PropertyValue = model.ClockLabelOpacity;

                this.FontSettingsPropertyPanel.PropertyFontFamily = new FontFamily(model.FontFamily);
                this.FontSettingsPropertyPanel.PropertyFontSize = model.FontSize;

                this.CanResizeProperty.PropertyValue = TypeConverter.ResizeModToBool(model.ResizeMode);
                this.DragActiveProperty.PropertyValue = model.IsDragActive;
            }
        }

        /// <summary>
        /// Set properties active state.
        /// </summary>
        /// <param name="newState">True = enabled, false = disabled.</param>
        private void ActiveChanged(bool newState)
        {
            this.BackgroundColorProperty.IsEnabled = newState;
            this.ForegroundColorProperty.IsEnabled = newState;

            this.BackgroundOpacityProperty.IsEnabled = newState;
            this.ForegroundOpacityProperty.IsEnabled = newState;

            this.FontSettingsPropertyPanel.IsEnabled = newState;

            this.CanResizeProperty.IsEnabled = newState;
            this.DragActiveProperty.IsEnabled = newState;
        }

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

        public void BackgroundColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (ClockWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<ClockWidget, ClockModel>(ref ClockWidgetContainer.Widget);
                model.BackgroundColor = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void ForegroundColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (ClockWidgetContainer.Widget.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<ClockWidget, ClockModel>(ref ClockWidgetContainer.Widget);
                model.ClockLabelForeground = e;
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

        private void ActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            //todo tu skończyłem
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

            ActiveChanged((bool)e);
        }

        private void DefaultSettingsButton_Click(object sender, RoutedEventArgs e)
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
                LoadWidget(_clockWidgetContainer);
                WidgetManager.SaveWidgetConfigurationInFile<ClockWidget, ClockModel>(_clockWidgetContainer.Widget);
            }
        }

        private void OpenSaveConfigurationDireButton_Click(object sender, RoutedEventArgs e)
        {
            // todo zabezpieczyć
            Process.Start("Explorer", AppFileSystem.GetSaveDireConfigurationPath(typeof(ClockWidget).Name));
        }

        private void LoadSavedConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            // todo zrobić kiedyś konfiguracje z plików
        }
    }
}
