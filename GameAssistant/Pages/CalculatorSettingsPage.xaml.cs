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
    /// Logika interakcji dla klasy CalculatorSettingsPage.xaml
    /// </summary>
    public partial class CalculatorSettingsPage : SettingsPageBase
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="calculatorWidget">Widget container with calculator widget to use.</param>
        public CalculatorSettingsPage(ref WidgetContainer<CalculatorWidget> calculatorWidget)
        {
            // Register widget change active state event:
            CalculatorWidget.Events.WidgetActiveChanged += (b) => WidgetChangeActiveStateMethodForSettingsPage(b);

            InitializeComponent();

            CalculatorWidgetContainer = calculatorWidget;
            LoadWidget(ref _calculatorWidgetContainer);
        }

        public CalculatorSettingsPage(ref WidgetContainer<CalculatorWidget> calculatorWidget, ref bool? calculatorWidgetState) : this(ref calculatorWidget)
        {
            ActiveProperty.PropertyValue = calculatorWidgetState;
        }

        public override void RemoveChangeWidgetActive() => CalculatorWidget.Events.WidgetActiveChanged -= (b) => WidgetChangeActiveStateMethodForSettingsPage(b);

        private void WidgetChangeActiveStateMethodForSettingsPage(bool state)
        {
            if (ActiveProperty.PropertyValue != state)
                ActiveProperty.PropertyValue = state;
        }

        #endregion

        /// <summary>
        /// Load widget in settings page.
        /// </summary>
        /// <param name="calculatorWidgetContainer">Calculator widget to load.</param>
        private void LoadWidget(ref WidgetContainer<CalculatorWidget> calculatorWidgetContainer)
        {
            if (calculatorWidgetContainer.Widget == null)
            {
                this.ActiveProperty.PropertyValue = false;
                ActiveChanged(false);
            }
            else
            {
                this.ActiveProperty.PropertyValue = true;
                LoadWidgetSettings(ref calculatorWidgetContainer);
                ActiveChanged(true);
            }
        }

        private void LoadWidgetSettings(ref WidgetContainer<CalculatorWidget> calculatorWidgetContainer)
        {
            var model = (calculatorWidgetContainer.Widget.DataContext as IWidgetViewModel<CalculatorModel>).WidgetModel;

            this.BackgroundColorProperty.PropertyColor = model.BackgroundAnimatedBrush.BrushContainer.Variable;
            this.TextBoxBackgroundColorProperty.PropertyColor = model.TextBoxBackgroundAnimatedBrush.BrushContainer.Variable;
            this.TextBoxForegroundColorProperty.PropertyColor = model.TextBoxForegroundAnimatedBrush.BrushContainer.Variable;
            this.ButtonsBackgroundColorProperty.PropertyColor = model.ButtonsBackgroundAnimatedBrush.BrushContainer.Variable;
            this.ButtonsForegroundColorProperty.PropertyColor = model.ButtonsForegroundAnimatedBrush.BrushContainer.Variable;

            this.BackgroundOpacityProperty.PropertyValue = model.BackgroundOpacity;
            this.TextBoxOpacityProperty.PropertyValue = model.TextBoxOpacity;
            this.ButtonsOpacityProperty.PropertyValue = model.ButtonsOpacity;

            this.BackgroundAnimationProperty.SelectedElementIndex = (int)model.BackgroundAnimatedBrush.BrushAnimationManager.Animation;
            this.TextBoxBackgroundAnimationProperty.SelectedElementIndex = (int)model.TextBoxBackgroundAnimatedBrush.BrushAnimationManager.Animation;
            this.TextBoxForegroundAnimationProperty.SelectedElementIndex = (int)model.TextBoxForegroundAnimatedBrush.BrushAnimationManager.Animation;
            this.ButtonsBackgroundAnimationProperty.SelectedElementIndex = (int)model.ButtonsBackgroundAnimatedBrush.BrushAnimationManager.Animation;
            this.ButtonsForegroundAnimationProperty.SelectedElementIndex = (int)model.ButtonsForegroundAnimatedBrush.BrushAnimationManager.Animation;

            this.TextBoxFontSettingsPropertyPanel.PropertyFontFamily = new FontFamily(model.TextBoxFontFamily);
            this.TextBoxFontSettingsPropertyPanel.PropertyFontSize = model.TextBoxFontSize;

            this.ButtonsFontSettingsPropertyPanel.PropertyFontFamily = new FontFamily(model.ButtonsFontFamily);
            this.ButtonsFontSettingsPropertyPanel.PropertyFontSize = model.ButtonsFontSize;

            this.CanResizeProperty.PropertyValue = TypeConverter.ResizeModToBool(model.ResizeMode);
            this.DragActiveProperty.PropertyValue = model.IsDragActive;

        }

        protected override void ActiveChanged(bool newState)
        {
            this.BackgroundColorProperty.IsEnabled = newState;
            this.TextBoxBackgroundColorProperty.IsEnabled = newState;
            this.TextBoxForegroundColorProperty.IsEnabled = newState;
            this.ButtonsBackgroundColorProperty.IsEnabled = newState;
            this.ButtonsForegroundColorProperty.IsEnabled = newState;

            this.BackgroundOpacityProperty.IsEnabled = newState;
            this.TextBoxOpacityProperty.IsEnabled = newState;
            this.ButtonsOpacityProperty.IsEnabled = newState;

            this.BackgroundAnimationProperty.IsEnabled = newState;
            this.TextBoxBackgroundAnimationProperty.IsEnabled = newState;
            this.TextBoxForegroundAnimationProperty.IsEnabled = newState;
            this.ButtonsBackgroundAnimationProperty.IsEnabled = newState;
            this.ButtonsForegroundAnimationProperty.IsEnabled = newState;

            this.TextBoxFontSettingsPropertyPanel.IsEnabled = newState;
            this.TextBoxFontSettingsPropertyPanel.IsEnabled = newState;

            this.ButtonsFontSettingsPropertyPanel.IsEnabled = newState;
            this.ButtonsFontSettingsPropertyPanel.IsEnabled = newState;

            this.CanResizeProperty.IsEnabled = newState;
            this.DragActiveProperty.IsEnabled = newState;
        }

        #region Widget

        public static readonly DependencyProperty PropertyColorProperty = DependencyProperty.Register(
        "CalculatorWidgetContainer", typeof(WidgetContainer<CalculatorWidget>),
        typeof(CalculatorSettingsPage)
        );

        private WidgetContainer<CalculatorWidget> _calculatorWidgetContainer;
        /// <summary>
        /// The calculator container with calculator widget.
        /// </summary>
        public WidgetContainer<CalculatorWidget> CalculatorWidgetContainer
        {
            get => _calculatorWidgetContainer;
            set => SetProperty(ref _calculatorWidgetContainer, value);
        }

        #endregion

        #region PropertyChangedMethods

        private void BackgroundColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (CalculatorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<CalculatorWidget, CalculatorModel>(ref CalculatorWidgetContainer.Widget);
                model.BackgroundAnimatedBrush.BrushContainer.Variable = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void BackgroundAnimationProperty_PropertyValueChanged(object sender, int e)
        {
            if (CalculatorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<CalculatorWidget, CalculatorModel>(ref CalculatorWidgetContainer.Widget);
                model.BackgroundAnimatedBrush.BrushAnimationManager.Animation = (AnimationManager.AnimationType)e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void TextBoxBackgroundColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (CalculatorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<CalculatorWidget, CalculatorModel>(ref CalculatorWidgetContainer.Widget);
                model.TextBoxBackgroundAnimatedBrush.BrushContainer.Variable = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void TextBoxBackgroundAnimationProperty_PropertyValueChanged(object sender, int e)
        {
            if (CalculatorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<CalculatorWidget, CalculatorModel>(ref CalculatorWidgetContainer.Widget);
                model.TextBoxBackgroundAnimatedBrush.BrushAnimationManager.Animation = (AnimationManager.AnimationType)e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void TextBoxForegroundColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (CalculatorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<CalculatorWidget, CalculatorModel>(ref CalculatorWidgetContainer.Widget);
                model.TextBoxForegroundAnimatedBrush.BrushContainer.Variable = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void TextBoxForegroundAnimationProperty_PropertyValueChanged(object sender, int e)
        {
            if (CalculatorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<CalculatorWidget, CalculatorModel>(ref CalculatorWidgetContainer.Widget);
                model.TextBoxForegroundAnimatedBrush.BrushAnimationManager.Animation = (AnimationManager.AnimationType)e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void ButtonsBackgroundColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (CalculatorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<CalculatorWidget, CalculatorModel>(ref CalculatorWidgetContainer.Widget);
                model.ButtonsBackgroundAnimatedBrush.BrushContainer.Variable = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void ButtonsBackgroundAnimationProperty_PropertyValueChanged(object sender, int e)
        {
            if (CalculatorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<CalculatorWidget, CalculatorModel>(ref CalculatorWidgetContainer.Widget);
                model.ButtonsBackgroundAnimatedBrush.BrushAnimationManager.Animation = (AnimationManager.AnimationType)e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void ButtonsForegroundColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (CalculatorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<CalculatorWidget, CalculatorModel>(ref CalculatorWidgetContainer.Widget);
                model.ButtonsForegroundAnimatedBrush.BrushContainer.Variable = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void ButtonsForegroundAnimationProperty_PropertyValueChanged(object sender, int e)
        {
            if (CalculatorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<CalculatorWidget, CalculatorModel>(ref CalculatorWidgetContainer.Widget);
                model.ButtonsForegroundAnimatedBrush.BrushAnimationManager.Animation = (AnimationManager.AnimationType)e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void BackgroundOpacityProperty_PropertyValueChanged(object sender, double e)
        {
            if (CalculatorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<CalculatorWidget, CalculatorModel>(ref CalculatorWidgetContainer.Widget);
                model.BackgroundOpacity = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void TextBoxOpacityProperty_PropertyValueChanged(object sender, double e)
        {
            if (CalculatorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<CalculatorWidget, CalculatorModel>(ref CalculatorWidgetContainer.Widget);
                model.TextBoxOpacity = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void ButtonsOpacityProperty_PropertyValueChanged(object sender, double e)
        {
            if (CalculatorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<CalculatorWidget, CalculatorModel>(ref CalculatorWidgetContainer.Widget);
                model.ButtonsOpacity = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void CanResizeProperty_PropertyValueChanged(object sender, bool? e)
        {
            if (CalculatorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<CalculatorWidget, CalculatorModel>(ref CalculatorWidgetContainer.Widget);
                model.ResizeMode = TypeConverter.BoolToResizeMod(e);
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void DragActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            if (CalculatorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<CalculatorWidget, CalculatorModel>(ref CalculatorWidgetContainer.Widget);
                model.IsDragActive = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void TextBoxFontSettingsPropertyPanel_PropertyValueChanged(object sender, (FontFamily, double) e)
        {
            if (CalculatorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<CalculatorWidget, CalculatorModel>(ref CalculatorWidgetContainer.Widget);
                model.TextBoxFontFamily = e.Item1.ToString();
                model.TextBoxFontSize = e.Item2;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void ButtonsFontSettingsPropertyPanel_PropertyValueChanged(object sender, (FontFamily, double) e)
        {
            if (CalculatorWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<CalculatorWidget, CalculatorModel>(ref CalculatorWidgetContainer.Widget);
                model.ButtonsFontFamily = e.Item1.ToString();
                model.ButtonsFontSize = e.Item2;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        protected override void ActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            CalculatorWidget.Events.WidgetActiveChanged_Invoke((bool)e);

            if (_calculatorWidgetContainer.Widget != null)
            {
                LoadWidgetSettings(ref _calculatorWidgetContainer);
            }
            ActiveChanged((bool)e);
        }

        #endregion

        protected override void DefaultSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Should you set widget configuration to default?\n(Warning, if you restore the default settings you will not be able to restore the current data.)", "Setting configuration to default:", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                if (_calculatorWidgetContainer.Widget != null)
                {
                    WidgetManager.CloseWidget<CalculatorWidget, CalculatorModel>(ref _calculatorWidgetContainer.Widget);
                }
                _calculatorWidgetContainer.Widget = new CalculatorWidget();
                _calculatorWidgetContainer.Widget.Show();
                LoadWidget(ref _calculatorWidgetContainer);
                WidgetManager.SaveWidgetConfigurationInFile<CalculatorWidget, CalculatorModel>(_calculatorWidgetContainer.Widget);
            }
        }

        protected override void OpenSaveConfigurationDireButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("Explorer", AppFileSystem.GetSaveDireConfigurationPath(typeof(CalculatorWidget).Name));
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
                    var model = new CalculatorModel();
                    using (var sr = File.OpenText(fileDialog.FileName))
                    {
                        model = JsonConvert.DeserializeObject<CalculatorModel>(sr.ReadToEnd());
                        WidgetManager.SaveWidgetConfigurationInFile(model);
                    }

                    if (_calculatorWidgetContainer.Widget != null)
                    {
                        WidgetManager.CloseWidget<CalculatorWidget, CalculatorModel>(ref _calculatorWidgetContainer.Widget);
                    }
                    WidgetManager.LoadWidget<CalculatorWidget, CalculatorViewModel, CalculatorModel>(ref _calculatorWidgetContainer.Widget);
                    _calculatorWidgetContainer.Widget?.Show();
                    LoadWidget(ref _calculatorWidgetContainer);
                }
            }
        }
    }
}
