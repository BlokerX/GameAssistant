using GameAssistant.Core;
using GameAssistant.Models;
using GameAssistant.Widgets;
using GameAssistant.WidgetViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAssistant.WindowViewModels
{
    internal class SettingsViewModel : BindableObject
    {
        private ClockWidget _clockWidget;
        public ClockWidget ClockWidget 
        {
            get => _clockWidget;
            set => SetProperty(ref _clockWidget, value);
        }

        public ClockModel clockModel;

        private PictureWidget pictureWidget;
        private PictureModel pictureModel;

        private NoteWidget noteWidget;
        private NoteModel noteModel;

        public SettingsViewModel(ref ClockWidget clockWidget, ref PictureWidget pictureWidget, ref NoteWidget noteWidget)
        {
            // Load clock widget:
            this._clockWidget = clockWidget;
            if (clockWidget != null)
                this.clockModel = (clockWidget.DataContext as IWidgetViewModel<ClockModel>).WidgetModel;

            // Load picture widget:
            this.pictureWidget = pictureWidget;
            if (pictureWidget != null)
                this.pictureModel = (pictureWidget.DataContext as IWidgetViewModel<PictureModel>).WidgetModel;

            // Load note widget:
            this.noteWidget = noteWidget;
            if (noteWidget != null)
                this.noteModel = (noteWidget.DataContext as IWidgetViewModel<NoteModel>).WidgetModel;

            // When clock widget is.
            //if (clockModel != null)
            //{
            //    ClockWidgetPage.ActiveProperty.PropertyValue = true;

            //    ClockWidgetPage.BackgroundColorProperty.PropertyColor = clockModel.BackgroundColor;
            //    ClockWidgetPage.ForegroundColorProperty.PropertyColor = clockModel.ClockLabelForeground;

            //    ClockWidgetPage.BackgroundOpacityProperty.PropertyValue = clockModel.BackgroundOpacity;
            //    ClockWidgetPage.ForegroundOpacityProperty.PropertyValue = clockModel.ClockLabelOpacity;

            //    ClockWidgetPage.FontSettingsPropertyPanel.PropertyFontFamily = new FontFamily(clockModel.FontFamily);
            //    ClockWidgetPage.FontSettingsPropertyPanel.PropertyFontSize = clockModel.FontSize;

            //    switch (clockModel.ResizeMode)
            //    {
            //        case ResizeMode.CanResizeWithGrip:
            //            ClockWidgetPage.CanResizeProperty.PropertyValue = true;
            //            break;

            //        case ResizeMode.NoResize:
            //            ClockWidgetPage.CanResizeProperty.PropertyValue = false;
            //            break;
            //    }

            //    ClockWidgetPage.DragActiveProperty.PropertyValue = clockModel.IsDragActive;
            //    // todo tu skończyłem
            //}
            //// When clock widget isn't.
            //else
            //{
            //    // todo
            //}

        }
    }
}
