using GameAssistant.Core;
using GameAssistant.Models;
using GameAssistant.Services;
using GameAssistant.Widgets;
using GameAssistant.WidgetViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameAssistant.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy PictureSettingsPage.xaml
    /// </summary>
    public partial class PictureSettingsPage : SettingsPageBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="pictureWidget">Widget container with picture widget to use.</param>
        public PictureSettingsPage(ref WidgetContainer<PictureWidget> pictureWidget)
        {
            InitializeComponent();

            PictureWidgetContainer = pictureWidget;
            LoadWidget(PictureWidgetContainer);
        }

        public PictureSettingsPage(ref WidgetContainer<PictureWidget> pictureWidget, ref bool? pictureWidgetState)
        {
            InitializeComponent();

            PictureWidgetContainer = pictureWidget;
            LoadWidget(PictureWidgetContainer);
            ActiveProperty.PropertyValue = pictureWidgetState;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pictureWidgetContainer"></param>
        private void LoadWidget(WidgetContainer<PictureWidget> pictureWidgetContainer)
        {
            if (PictureWidgetContainer.Widget == null)
            {
                this.ActiveProperty.PropertyValue = false;
                ActiveChanged(false);
            }
            else
            {
                this.ActiveProperty.PropertyValue = true;

                //this.DataContext = new PictureSettingsViewModel(ref pictureWidgetContainer);
                var model = (pictureWidgetContainer.Widget.DataContext as IWidgetViewModel<PictureModel>).WidgetModel;

                //todo problem's solution
                this.BackgroundColorProperty.PropertyColor = model.BackgroundColor;

                this.BackgroundOpacityProperty.PropertyValue = model.BackgroundOpacity;
                this.ImageOpacityProperty.PropertyValue = model.ImageOpacity;

                this.CanResizeProperty.PropertyValue = TypeConverter.ResizeModToBool(model.ResizeMode);
                this.DragActiveProperty.PropertyValue = model.IsDragActive;

                this.ImageSourceProperty.PropertyValue = model.ImageSource;
            }
        }

        /// <summary>
        /// Set properties active state.
        /// </summary>
        /// <param name="newState">True = enabled, false = disabled.</param>
        private void ActiveChanged(bool newState)
        {
            this.BackgroundColorProperty.IsEnabled = newState;

            this.BackgroundOpacityProperty.IsEnabled = newState;
            this.ImageOpacityProperty.IsEnabled = newState;

            this.CanResizeProperty.IsEnabled = newState;
            this.DragActiveProperty.IsEnabled = newState;

            this.ImageSourceProperty.IsEnabled = newState;
        }

        public static readonly DependencyProperty PropertyColorProperty = DependencyProperty.Register(
        "PictureWidgetContainer", typeof(WidgetContainer<PictureWidget>),
        typeof(PictureSettingsPage)
        );
        private WidgetContainer<PictureWidget> _pictureWidgetContainer;
        /// <summary>
        /// The picture container with picture widget.
        /// </summary>
        public WidgetContainer<PictureWidget> PictureWidgetContainer
        {
            get => _pictureWidgetContainer;
            set => SetProperty(ref _pictureWidgetContainer, value);
        }

        public void BackgroundColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (PictureWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<PictureWidget, PictureModel>(ref PictureWidgetContainer.Widget);
                model.BackgroundColor = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void BackgroundOpacityProperty_PropertyValueChanged(object sender, double e)
        {
            if (PictureWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<PictureWidget, PictureModel>(ref PictureWidgetContainer.Widget);
                model.BackgroundOpacity = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void ImageOpacityProperty_PropertyValueChanged(object sender, double e)
        {
            if (PictureWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<PictureWidget, PictureModel>(ref PictureWidgetContainer.Widget);
                model.ImageOpacity = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void CanResizeProperty_PropertyValueChanged(object sender, bool? e)
        {
            if (PictureWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<PictureWidget, PictureModel>(ref PictureWidgetContainer.Widget);
                model.ResizeMode = TypeConverter.BoolToResizeMod(e);
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void DragActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            if (PictureWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<PictureWidget, PictureModel>(ref PictureWidgetContainer.Widget);
                model.IsDragActive = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }

        private void ImageSourceProperty_PropertyValueChanged(object sender, string e)
        {
            // todo zdjęcie ustawienia
            //if (PictureWidgetContainer.Widget?.DataContext != null)
            //{
            //    var model = WidgetManager.GetModelFromWidget<PictureWidget, PictureModel>(ref PictureWidgetContainer.Widget);
            //    model.ImageSource = e;
            //    WidgetManager.SaveWidgetConfigurationInFile(model);
            //}
        }

        private void ImageSourceProperty_ButtonClick(object sender, EventArgs e)
        {
            if (PictureWidgetContainer.Widget?.DataContext != null)
            {
                var fileDialog = new System.Windows.Forms.OpenFileDialog()
                {
                    Filter =
                    "png files (*.png)|*.png" +
                    "|jpg files (*.jpg)|*.jpg" +
                    "|jpeg files (*.jpeg)|*.jpeg" +
                    "|bmp files (*.bmp)|*.bmp" +
                    "|svg files (*.svg)|*.svg" +
                    "|webp files (*.webp)|*.webp" +
                    "|gif files (*.*)|*.gif" +
                    "|All files (*.*)|*.*",

                    Multiselect = false,

                    Title = "Select image to picture widget:"
                };

                if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var model = WidgetManager.GetModelFromWidget<PictureWidget, PictureModel>(ref PictureWidgetContainer.Widget);
                    model.ImageSource = ImageSourceProperty.PropertyValue = fileDialog.FileName;
                    WidgetManager.SaveWidgetConfigurationInFile(model);
                }
            }
        }

        private void ActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            //todo tu skończyłem
            WidgetManager.SaveWidgetConfigurationInFile<PictureWidget, PictureModel>(_pictureWidgetContainer.Widget);

            var downloadedConfigurationResult = WidgetManager.DownloadWidgetConfigurationFromFile(out PictureModel model);
            switch (e)
            {
                case true:
                    if (_pictureWidgetContainer.Widget == null)
                        WidgetManager.BuildWidget<PictureWidget, PictureViewModel, PictureModel>(ref _pictureWidgetContainer.Widget, ref model, downloadedConfigurationResult);
                    break;

                case false:
                    if (_pictureWidgetContainer.Widget != null)
                        WidgetManager.CloseWidget(ref _pictureWidgetContainer.Widget, ref model);
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
                if (_pictureWidgetContainer.Widget != null)
                {
                    WidgetManager.CloseWidget<PictureWidget, PictureModel>(ref _pictureWidgetContainer.Widget);
                }
                _pictureWidgetContainer.Widget = new PictureWidget();
                _pictureWidgetContainer.Widget.Show();
                LoadWidget(_pictureWidgetContainer);
                WidgetManager.SaveWidgetConfigurationInFile<PictureWidget, PictureModel>(_pictureWidgetContainer.Widget);
            }
        }

        private void OpenSaveConfigurationDireButton_Click(object sender, RoutedEventArgs e)
        {
            // todo zabezpieczyć
            Process.Start("Explorer", AppFileSystem.GetSaveDireConfigurationPath(typeof(PictureWidget).Name));
        }

        private void LoadSavedConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            // todo zrobić kiedyś konfiguracje z plików
        }
    }
}
