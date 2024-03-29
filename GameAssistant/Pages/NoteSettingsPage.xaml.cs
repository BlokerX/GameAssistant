﻿using GameAssistant.Core;
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
    public partial class NoteSettingsPage : WidgetSettingsPage
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="noteWidget">Widget container with note widget to use.</param>
        public NoteSettingsPage(ref WidgetContainer<NoteWidget> noteWidget)
        {
            // Register widget change active state event:
            NoteWidget.Events.WidgetActiveChanged += WidgetChangeActiveStateMethodForSettingsPage;

            InitializeComponent();

            NoteWidgetContainer = noteWidget;
            LoadWidget(ref _noteWidgetContainer);
        }

        public NoteSettingsPage(ref WidgetContainer<NoteWidget> noteWidget, ref bool? noteWidgetState) : this(ref noteWidget)
        {
            ActiveProperty.PropertyValue = noteWidgetState;
        }

        public override void RemovePageMethodsFromWidgetEvents()
        {
            NoteWidget.Events.WidgetActiveChanged -= WidgetChangeActiveStateMethodForSettingsPage;
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
        /// <param name="noteWidgetContainer">Note widget to load.</param>
        public void LoadWidget(ref WidgetContainer<NoteWidget> noteWidgetContainer)
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

            this.BackgroundAnimationBrushProperty.ColorProperty.PropertyColor = model.BackgroundAnimatedBrush.BrushContainer.Variable;
            this.ForegroundAnimationBrushProperty.ColorProperty.PropertyColor = model.ForegroundAnimatedBrush.BrushContainer.Variable;

            this.BackgroundOpacityProperty.PropertyValue = model.BackgroundOpacity;
            this.ForegroundOpacityProperty.PropertyValue = model.NoteFontOpacity;

            this.BackgroundAnimationBrushProperty.AnimationProperty.SelectedElementIndex = (int)model.BackgroundAnimatedBrush.BrushAnimationManager.Animation;
            this.ForegroundAnimationBrushProperty.AnimationProperty.SelectedElementIndex = (int)model.ForegroundAnimatedBrush.BrushAnimationManager.Animation;

            this.FontSettingsPropertyPanel.PropertyFontFamily = new FontFamily(model.NoteFontFamily);
            this.FontSettingsPropertyPanel.PropertyFontSize = model.NoteFontSize;

            this.IsTopmostProperty.PropertyValue = model.IsTopmost;
            this.CanResizeProperty.PropertyValue = TypeConverter.ResizeModToBool(model.ResizeMode);
            this.DragActiveProperty.PropertyValue = model.IsDragActive;

            this.SettingBarVisibilityProperty.PropertyValue = TypeConverter.VisibilityToBool(model.SettingsBarVisibility);
        }

        protected override void ActiveChanged(bool newState)
        {
            this.BackgroundAnimationBrushProperty.IsEnabled = newState;
            this.ForegroundAnimationBrushProperty.IsEnabled = newState;

            this.BackgroundOpacityProperty.IsEnabled = newState;
            this.ForegroundOpacityProperty.IsEnabled = newState;

            this.FontSettingsPropertyPanel.IsEnabled = newState;

            this.IsTopmostProperty.IsEnabled = newState;
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

        private void IsTopmostProperty_PropertyValueChanged(object sender, bool? e)
        {
            if (NoteWidgetContainer.Widget?.DataContext != null)
            {
                var model = WidgetManager.GetModelFromWidget<NoteWidget, NoteModel>(ref NoteWidgetContainer.Widget);
                model.IsTopmost = e;
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
            _noteWidgetContainer.ResetWidgetConfigurationToDefaultWithMessageBox<NoteWidget, NoteViewModel, NoteModel>(() => LoadWidget(ref _noteWidgetContainer));
        }

        protected override void OpenSaveConfigurationDireButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("Explorer", AppFileSystem.GetWidgetConfigurationDirePath(typeof(NoteWidget).Name));
        }

        protected override void LoadSavedConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            _noteWidgetContainer.LoadConfigurationFromFile<NoteWidget, NoteViewModel, NoteModel>(() => LoadWidget(ref _noteWidgetContainer));
        }

    }
}
