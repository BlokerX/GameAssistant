using GameAssistant.Models;
using GameAssistant.PageViewModels;
using GameAssistant.Services;
using GameAssistant.Widgets;
using GameAssistant.WidgetViewModels;
using System;
using System.Collections.Generic;
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
    /// Logika interakcji dla klasy ClockSettingsPage.xaml
    /// </summary>
    public partial class ClockSettingsPage : SettingsPageBase
    {
        public ClockSettingsPage(ref WidgetContainer<ClockWidget> clockWidget)
        {
            InitializeComponent();

            ClockWidgetContainer = clockWidget;

            if (ClockWidgetContainer == null)
            {
                this.ActiveProperty.PropertyValue = false;
                ActiveChanged(false);
            }
            else
            {
                this.ActiveProperty.PropertyValue = true;

                this.DataContext = new ClockSettingsViewModel(ref clockWidget);
                var model = (clockWidget.Widget.DataContext as IWidgetViewModel<ClockModel>).WidgetModel;


                //todo problem's solution
                this.BackgroundColorProperty.PropertyColor = model.BackgroundColor;
                this.ForegroundColorProperty.PropertyColor = model.ClockLabelForeground;

                this.BackgroundOpacityProperty.PropertyValue = model.BackgroundOpacity;
                this.ForegroundOpacityProperty.PropertyValue = model.ClockLabelOpacity;

                this.FontSettingsPropertyPanel.PropertyFontFamily = new FontFamily(model.FontFamily);
                this.FontSettingsPropertyPanel.PropertyFontSize = model.FontSize;

                this.CanResizeProperty.PropertyValue = ResizeModToBool(model.ResizeMode);
                this.DragActiveProperty.PropertyValue = model.IsDragActive;
            }
        }

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

        public bool? ResizeModToBool(ResizeMode resizeMode)
        {
            switch (resizeMode)
            {
                //case ResizeMode.CanResize:
                case ResizeMode.CanResizeWithGrip:
                    return true;
                default:
                case ResizeMode.NoResize:
                    return false;
            }
        }

        public ResizeMode BoolToResizeMod(bool? resizeMode)
        {
            switch (resizeMode)
            {
                case true:
                    return ResizeMode.CanResizeWithGrip;
                default:
                case false:
                    return ResizeMode.NoResize;
            }
        }

        public static readonly DependencyProperty PropertyColorProperty = DependencyProperty.Register(
        "ClockWidgetContainer", typeof(WidgetContainer<ClockWidget>),
        typeof(ClockSettingsPage)
        );
        private WidgetContainer<ClockWidget> _clockWidgetContainer;
        public WidgetContainer<ClockWidget> ClockWidgetContainer
        {
            get => _clockWidgetContainer;
            set => SetProperty(ref _clockWidgetContainer, value);
        }

        public void BackgroundColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (ClockWidgetContainer.Widget?.DataContext != null)
            {
                var model = (ClockWidgetContainer.Widget.DataContext as IWidgetViewModel<ClockModel>).WidgetModel;
                model.BackgroundColor = e;
            }
        }

        private void ForegroundColorProperty_PropertyColorChanged(object sender, Brush e)
        {
            if (ClockWidgetContainer.Widget.DataContext != null)
            {
                var model = (ClockWidgetContainer.Widget.DataContext as IWidgetViewModel<ClockModel>).WidgetModel;
                model.ClockLabelForeground = e;
            }
        }

        private void BackgroundOpacityProperty_PropertyValueChanged(object sender, double e)
        {
            if (ClockWidgetContainer.Widget?.DataContext != null)
            {
                var model = (ClockWidgetContainer.Widget.DataContext as IWidgetViewModel<ClockModel>).WidgetModel;
                model.BackgroundOpacity = e;
            }
        }

        private void ForegroundOpacityProperty_PropertyValueChanged(object sender, double e)
        {
            if (ClockWidgetContainer.Widget?.DataContext != null)
            {
                var model = (ClockWidgetContainer.Widget.DataContext as IWidgetViewModel<ClockModel>).WidgetModel;
                model.ClockLabelOpacity = e;
            }
        }

        private void CanResizeProperty_PropertyValueChanged(object sender, bool? e)
        {
            if (ClockWidgetContainer.Widget?.DataContext != null)
            {
                var model = (ClockWidgetContainer.Widget.DataContext as IWidgetViewModel<ClockModel>).WidgetModel;
                model.ResizeMode = BoolToResizeMod(e);
            }
        }

        private void DragActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            if (ClockWidgetContainer.Widget?.DataContext != null)
            {
                var model = (ClockWidgetContainer.Widget.DataContext as IWidgetViewModel<ClockModel>).WidgetModel;
                model.IsDragActive = e;
            }
        }

        private void FontSettingsPropertyPanel_PropertyValueChanged(object sender, (FontFamily, double) e)
        {
            if (ClockWidgetContainer.Widget?.DataContext != null)
            {
                var model = (ClockWidgetContainer.Widget.DataContext as IWidgetViewModel<ClockModel>).WidgetModel;
                model.FontFamily = e.Item1.ToString();
                model.FontSize = e.Item2;
            }
        }

        private void ActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            //todo tu skończyłem
            WidgetMenager.SaveWidgetConfigurationInFile<ClockWidget, ClockModel>(ClockWidgetContainer.Widget);
            WidgetMenager.Widget_ChangeStateAndSave<ClockWidget, ClockViewModel, ClockModel>(ref _clockWidgetContainer.Widget);
            ActiveChanged((bool)e);
        }
    }
}
