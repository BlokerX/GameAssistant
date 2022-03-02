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
    /// Logika interakcji dla klasy NoteSettingsPage.xaml
    /// </summary>
    public partial class NoteSettingsPage : SettingsPageBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="noteWidget">Widget container with note widget to use.</param>
        public NoteSettingsPage(ref WidgetContainer<NoteWidget> noteWidget)
        {
            InitializeComponent();

            NoteWidgetContainer = noteWidget;
            LoadWidget(NoteWidgetContainer);
        }

        public NoteSettingsPage(ref WidgetContainer<NoteWidget> noteWidget, ref bool? noteWidgetState)
        {
            InitializeComponent();

            NoteWidgetContainer = noteWidget;
            LoadWidget(NoteWidgetContainer);
            ActiveProperty.PropertyValue = noteWidgetState;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noteWidgetContainer"></param>
        private void LoadWidget(WidgetContainer<NoteWidget> noteWidgetContainer)
        {
            if (NoteWidgetContainer.Widget == null)
            {
                this.ActiveProperty.PropertyValue = false;
                ActiveChanged(false);
            }
            else
            {
                this.ActiveProperty.PropertyValue = true;

                //this.DataContext = new NoteSettingsViewModel(ref noteWidgetContainer);
                var model = (noteWidgetContainer.Widget.DataContext as IWidgetViewModel<NoteModel>).WidgetModel;

                //todo problem's solution
                this.BackgroundColorProperty.PropertyColor = model.BackgroundColor;
                this.ForegroundColorProperty.PropertyColor = model.NoteFontColor;

                this.BackgroundOpacityProperty.PropertyValue = model.BackgroundOpacity;
                this.ForegroundOpacityProperty.PropertyValue = model.NoteFontOpacity;

                this.FontSettingsPropertyPanel.PropertyFontFamily = new FontFamily(model.NoteFontFamily);
                this.FontSettingsPropertyPanel.PropertyFontSize = model.NoteFontSize;

                this.CanResizeProperty.PropertyValue = TypeConverter.ResizeModToBool(model.ResizeMode);
                this.DragActiveProperty.PropertyValue = model.IsDragActive;

                this.SettingBarVisibilityProperty.PropertyValue = TypeConverter.VisibilityToBool(model.SettingsBarVisibility);
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

            this.SettingBarVisibilityProperty.IsEnabled = newState;
        }

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

        public void BackgroundColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (NoteWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<NoteWidget, NoteModel>(ref NoteWidgetContainer.Widget);
                model.BackgroundColor = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void ForegroundColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (NoteWidgetContainer.Widget.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<NoteWidget, NoteModel>(ref NoteWidgetContainer.Widget);
                model.NoteFontColor = e;
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
            if (NoteWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<NoteWidget, NoteModel>(ref NoteWidgetContainer.Widget);
                model.SettingsBarVisibility = TypeConverter.BoolToVisibility(e);
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void ActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            //todo tu skończyłem
            WidgetManager.SaveWidgetConfigurationInFile<NoteWidget, NoteModel>(_noteWidgetContainer.Widget);

            var downloadedConfigurationResult = WidgetManager.DownloadWidgetConfigurationFromFile(out NoteModel model);
            switch (e)
            {
                case true:
                    if (_noteWidgetContainer.Widget == null)
                        WidgetManager.BuildWidget<NoteWidget, NoteViewModel, NoteModel>(ref _noteWidgetContainer.Widget, ref model, downloadedConfigurationResult);
                    break;

                case false:
                    if (_noteWidgetContainer.Widget != null)
                        WidgetManager.CloseWidget(ref _noteWidgetContainer.Widget, ref model);
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
                if (_noteWidgetContainer.Widget != null)
                {
                    WidgetManager.CloseWidget<NoteWidget, NoteModel>(ref _noteWidgetContainer.Widget);
                }
                _noteWidgetContainer.Widget = new NoteWidget();
                _noteWidgetContainer.Widget.Show();
                LoadWidget(_noteWidgetContainer);
                WidgetManager.SaveWidgetConfigurationInFile<NoteWidget, NoteModel>(_noteWidgetContainer.Widget);
            }
        }

        private void OpenSaveConfigurationDireButton_Click(object sender, RoutedEventArgs e)
        {
            // todo zabezpieczyć
            Process.Start("Explorer", AppFileSystem.GetSaveDireConfigurationPath(typeof(NoteWidget).Name));
        }

        private void LoadSavedConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            // todo zrobić kiedyś konfiguracje z plików
        }

    }
}
