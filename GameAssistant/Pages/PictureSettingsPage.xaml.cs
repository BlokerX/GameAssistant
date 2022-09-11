using GameAssistant.Core;
using GameAssistant.Models;
using GameAssistant.Services;
using GameAssistant.Widgets;
using GameAssistant.WidgetViewModels;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace GameAssistant.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy PictureSettingsPage.xaml
    /// </summary>
    public partial class PictureSettingsPage : WidgetSettingsPage
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="pictureWidget">Widget container with picture widget to use.</param>
        public PictureSettingsPage(ref WidgetContainer<PictureWidget> pictureWidget)
        {
            // Register widget change active state event:
            PictureWidget.Events.WidgetActiveChanged += WidgetChangeActiveStateMethodForSettingsPage;

            InitializeComponent();

            PictureWidgetContainer = pictureWidget;
            LoadWidget(ref _pictureWidgetContainer);
        }

        public PictureSettingsPage(ref WidgetContainer<PictureWidget> pictureWidget, ref bool? pictureWidgetState) : this(ref pictureWidget)
        {
            ActiveProperty.PropertyValue = pictureWidgetState;
        }

        public override void RemovePageMethodsFromWidgetEvents() => PictureWidget.Events.WidgetActiveChanged -= WidgetChangeActiveStateMethodForSettingsPage;

        private void WidgetChangeActiveStateMethodForSettingsPage(bool state)
        {
            if (ActiveProperty.PropertyValue != state)
                ActiveProperty.PropertyValue = state;
        }

        #endregion

        /// <summary>
        /// Load widget in settings page.
        /// </summary>
        /// <param name="pictureWidgetContainer">Picture widget to load.</param>
        public void LoadWidget(ref WidgetContainer<PictureWidget> pictureWidgetContainer)
        {
            if (PictureWidgetContainer.Widget == null)
            {
                this.ActiveProperty.PropertyValue = false;
                ActiveChanged(false);
            }
            else
            {
                this.ActiveProperty.PropertyValue = true;
                LoadWidgetSettings(ref pictureWidgetContainer);
                ActiveChanged(true);
            }
        }

        private void LoadWidgetSettings(ref WidgetContainer<PictureWidget> pictureWidgetContainer)
        {
            var model = (pictureWidgetContainer.Widget.DataContext as IWidgetViewModel<PictureModel>).WidgetModel;

            this.BackgroundAnimationBrushProperty.ColorProperty.PropertyColor = model.BackgroundAnimatedBrush.BrushContainer.Variable;

            this.BackgroundAnimationBrushProperty.AnimationProperty.SelectedElementIndex = (int)model.BackgroundAnimatedBrush.BrushAnimationManager.Animation;

            this.BackgroundOpacityProperty.PropertyValue = model.BackgroundOpacity;
            this.ImageOpacityProperty.PropertyValue = model.ImageOpacity;

            this.CanResizeProperty.PropertyValue = TypeConverter.ResizeModToBool(model.ResizeMode);
            this.DragActiveProperty.PropertyValue = model.IsDragActive;

            this.ImageSourceProperty.PropertyValue = model.ImageSource;
        }

        protected override void ActiveChanged(bool newState)
        {
            this.BackgroundAnimationBrushProperty.IsEnabled = newState;

            this.BackgroundOpacityProperty.IsEnabled = newState;
            this.ImageOpacityProperty.IsEnabled = newState;

            this.CanResizeProperty.IsEnabled = newState;
            this.DragActiveProperty.IsEnabled = newState;

            this.ImageSourceProperty.IsEnabled = newState;
        }

        #region Widget

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

        #endregion

        #region PropertyChangedMethods

        private void BackgroundColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (PictureWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<PictureWidget, PictureModel>(ref PictureWidgetContainer.Widget);
                model.BackgroundAnimatedBrush.BrushContainer.Variable = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
        }
        private void BackgroundAnimationProperty_PropertyValueChanged(object sender, int e)
        {
            if (PictureWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<PictureWidget, PictureModel>(ref PictureWidgetContainer.Widget);
                model.BackgroundAnimatedBrush.BrushAnimationManager.Animation = (AnimationManager.AnimationType)e;
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
            if (PictureWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<PictureWidget, PictureModel>(ref PictureWidgetContainer.Widget);
                model.ImageSource = e;
                WidgetManager.SaveWidgetConfigurationInFile(model);
            }
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

        protected override void ActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            PictureWidget.Events.WidgetActiveChanged_Invoke((bool)e);

            if (_pictureWidgetContainer.Widget != null)
            {
                LoadWidgetSettings(ref _pictureWidgetContainer);
            }
            ActiveChanged((bool)e);
        }

        #endregion

        protected override void DefaultSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            _pictureWidgetContainer.ResetWidgetConfigurationToDefaultWithMessageBox<PictureWidget, PictureViewModel, PictureModel>(() => LoadWidget(ref _pictureWidgetContainer));
        }

        protected override void OpenSaveConfigurationDireButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("Explorer", AppFileSystem.GetWidgetConfigurationDirePath(typeof(PictureWidget).Name));
        }

        protected override void LoadSavedConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            _pictureWidgetContainer.LoadConfigurationFromFile<PictureWidget, PictureViewModel, PictureModel>(() => LoadWidget(ref _pictureWidgetContainer));
        }
    }
}
