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
    /// Logika interakcji dla klasy NoteSettingsPage.xaml
    /// </summary>
    public partial class NoteSettingsPage : SettingsPageBase
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="noteWidget">Widget container with note widget to use.</param>
        public NoteSettingsPage(ref WidgetContainer<NoteWidget> noteWidget)
        {
            // Register widget change active state event:
            NoteWidget.Events.WidgetActiveChanged += (b) => WidgetChangeActiveStateMethodForSettingsPage(b);

            InitializeComponent();

            NoteWidgetContainer = noteWidget;
            LoadWidget(ref _noteWidgetContainer);
        }

        public NoteSettingsPage(ref WidgetContainer<NoteWidget> noteWidget, ref bool? noteWidgetState) : this(ref noteWidget)
        {
            ActiveProperty.PropertyValue = noteWidgetState;
        }

        public override void RemoveChangeWidgetActive() => NoteWidget.Events.WidgetActiveChanged -= (b) => WidgetChangeActiveStateMethodForSettingsPage(b);

        private void WidgetChangeActiveStateMethodForSettingsPage(bool state)
        {
            if (ActiveProperty.PropertyValue != state)
                ActiveProperty.PropertyValue = state;
        }

        #endregion

        /// <summary>
        /// Load widget in settings page.
        /// </summary>
        /// <param name="noteWidgetContainer">Note widget to load.</param>
        private void LoadWidget(ref WidgetContainer<NoteWidget> noteWidgetContainer)
        {
            if (noteWidgetContainer.Widget == null)
            {
                this.ActiveProperty.PropertyValue = false;
                ActiveChanged(false);
            }
            else
            {
                this.ActiveProperty.PropertyValue = true;
                LoadWidgetSettings(ref noteWidgetContainer);
                ActiveChanged(true);
            }
        }

        private void LoadWidgetSettings(ref WidgetContainer<NoteWidget> noteWidgetContainer)
        {
            var model = (noteWidgetContainer.Widget.DataContext as IWidgetViewModel<NoteModel>).WidgetModel;

            this.BackgroundColorProperty.PropertyColor = model.BackgroundAnimatedBrush.BrushContainer.Variable;
            this.ForegroundColorProperty.PropertyColor = model.ForegroundAnimatedBrush.BrushContainer.Variable;

            this.BackgroundOpacityProperty.PropertyValue = model.BackgroundOpacity;
            this.ForegroundOpacityProperty.PropertyValue = model.NoteFontOpacity;

            this.BackgroundAnimationProperty.SelectedElementIndex = (int)model.BackgroundAnimatedBrush.BrushAnimationManager.Animation;
            this.ForegroundAnimationProperty.SelectedElementIndex = (int)model.ForegroundAnimatedBrush.BrushAnimationManager.Animation;

            this.FontSettingsPropertyPanel.PropertyFontFamily = new FontFamily(model.NoteFontFamily);
            this.FontSettingsPropertyPanel.PropertyFontSize = model.NoteFontSize;

            this.CanResizeProperty.PropertyValue = TypeConverter.ResizeModToBool(model.ResizeMode);
            this.DragActiveProperty.PropertyValue = model.IsDragActive;

            this.SettingBarVisibilityProperty.PropertyValue = TypeConverter.VisibilityToBool(model.SettingsBarVisibility);
        }

        protected override void ActiveChanged(bool newState)
        {
            this.BackgroundColorProperty.IsEnabled = newState;
            this.ForegroundColorProperty.IsEnabled = newState;

            this.BackgroundOpacityProperty.IsEnabled = newState;
            this.ForegroundOpacityProperty.IsEnabled = newState;

            this.BackgroundAnimationProperty.IsEnabled = newState;
            this.ForegroundAnimationProperty.IsEnabled = newState;

            this.FontSettingsPropertyPanel.IsEnabled = newState;

            this.CanResizeProperty.IsEnabled = newState;
            this.DragActiveProperty.IsEnabled = newState;

            this.SettingBarVisibilityProperty.IsEnabled = newState;
        }

        #region Widget

        public static readonly DependencyProperty PropertyColorProperty = DependencyProperty.Register(
        "NoteWidgetContainer", typeof(WidgetContainer<NoteWidget>),
        typeof(NoteSettingsPage)
        );

        private WidgetContainer<NoteWidget> _noteWidgetContainer;
        /// <summary>
        /// The note container with note widget.
        /// </summary>
        public WidgetContainer<NoteWidget> NoteWidgetContainer
        {
            get => _noteWidgetContainer;
            set => SetProperty(ref _noteWidgetContainer, value);
        }

        #endregion

        #region PropertyChangedMethods

        private void BackgroundColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (NoteWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<NoteWidget, NoteModel>(ref NoteWidgetContainer.Widget);
                model.BackgroundAnimatedBrush.BrushContainer.Variable = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }
        private void BackgroundAnimationProperty_PropertyValueChanged(object sender, int e)
        {
            if (NoteWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<NoteWidget, NoteModel>(ref NoteWidgetContainer.Widget);
                model.BackgroundAnimatedBrush.BrushAnimationManager.Animation = (AnimationManager.AnimationType)e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void ForegroundColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (NoteWidgetContainer.Widget.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<NoteWidget, NoteModel>(ref NoteWidgetContainer.Widget);
                model.ForegroundAnimatedBrush.BrushContainer.Variable = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void ForegroundAnimationProperty_PropertyValueChanged(object sender, int e)
        {
            if (NoteWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<NoteWidget, NoteModel>(ref NoteWidgetContainer.Widget);
                model.ForegroundAnimatedBrush.BrushAnimationManager.Animation = (AnimationManager.AnimationType)e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void BackgroundOpacityProperty_PropertyValueChanged(object sender, double e)
        {
            if (NoteWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<NoteWidget, NoteModel>(ref NoteWidgetContainer.Widget);
                model.BackgroundOpacity = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void ForegroundOpacityProperty_PropertyValueChanged(object sender, double e)
        {
            if (NoteWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<NoteWidget, NoteModel>(ref NoteWidgetContainer.Widget);
                model.NoteFontOpacity = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void CanResizeProperty_PropertyValueChanged(object sender, bool? e)
        {
            if (NoteWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<NoteWidget, NoteModel>(ref NoteWidgetContainer.Widget);
                model.ResizeMode = TypeConverter.BoolToResizeMod(e);
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void DragActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            if (NoteWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<NoteWidget, NoteModel>(ref NoteWidgetContainer.Widget);
                model.IsDragActive = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void FontSettingsPropertyPanel_PropertyValueChanged(object sender, (FontFamily, double) e)
        {
            if (NoteWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<NoteWidget, NoteModel>(ref NoteWidgetContainer.Widget);
                model.NoteFontFamily = e.Item1.ToString();
                model.NoteFontSize = e.Item2;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void SettingBarVisibilityProperty_PropertyValueChanged(object sender, bool? e)
        {
            //todo setting bar visibility to synch
            if (NoteWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<NoteWidget, NoteModel>(ref NoteWidgetContainer.Widget);
                ;
                switch (model.SettingsBarVisibility = TypeConverter.BoolToVisibility(e))
                {
                    case System.Windows.Visibility.Visible:
                        (NoteWidgetContainer.Widget.DataContext as NoteViewModel).TextBoxMargin = new Thickness(10, 30, 10, 10);
                        break;
                    case System.Windows.Visibility.Hidden:
                    default:
                        (NoteWidgetContainer.Widget.DataContext as NoteViewModel).TextBoxMargin = new Thickness(10, 10, 10, 10);
                        break;
                }
                WidgetManager.SaveWidgetConfigurationInFile(model);

            }
        }


        protected override void ActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            NoteWidget.Events.WidgetActiveChanged_Invoke((bool)e);

            if (_noteWidgetContainer.Widget != null)
            {
                LoadWidgetSettings(ref _noteWidgetContainer);
            }
            ActiveChanged((bool)e);
        }

        #endregion

        protected override void DefaultSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Should you set widget configuration to default?\n(Warning, if you restore the default settings you will not be able to restore the current data.)", "Setting configuration to default:", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                if (_noteWidgetContainer.Widget != null)
                {
                    WidgetManager.CloseWidget<NoteWidget, NoteModel>(ref _noteWidgetContainer.Widget);
                }
                _noteWidgetContainer.Widget = new NoteWidget();
                _noteWidgetContainer.Widget.Show();
                LoadWidget(ref _noteWidgetContainer);
                WidgetManager.SaveWidgetConfigurationInFile<NoteWidget, NoteModel>(_noteWidgetContainer.Widget);
            }
        }

        protected override void OpenSaveConfigurationDireButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("Explorer", AppFileSystem.GetSaveDireConfigurationPath(typeof(NoteWidget).Name));
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
                    var model = new NoteModel();
                    using (var sr = File.OpenText(fileDialog.FileName))
                    {
                        model = JsonConvert.DeserializeObject<NoteModel>(sr.ReadToEnd());
                        WidgetManager.SaveWidgetConfigurationInFile(model);
                    }

                    if (_noteWidgetContainer.Widget != null)
                    {
                        WidgetManager.CloseWidget<NoteWidget, NoteModel>(ref _noteWidgetContainer.Widget);
                    }
                    WidgetManager.LoadWidget<NoteWidget, NoteViewModel, NoteModel>(ref _noteWidgetContainer.Widget);
                    _noteWidgetContainer.Widget?.Show();
                    LoadWidget(ref _noteWidgetContainer);
                }
            }
        }

    }
}
