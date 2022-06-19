using GameAssistant.Models;
using GameAssistant.Widgets;
using GameAssistant.WidgetViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace GameAssistant.Services
{
    /// <summary>
    /// Widget's helper class.
    /// </summary>
    internal static class WidgetManager
    {
        /// <summary>
        /// List of widget type's unit.
        /// </summary>
        public static List<MVVMUnit> WidgetsList = new List<MVVMUnit>()
        {
            new MVVMUnit()
            {
                WidgetType = typeof(ClockWidget),
                WidgetViewModelType = typeof(ClockViewModel),
                WidgetModelType = typeof(ClockModel)
            },
            new MVVMUnit()
            {
                WidgetType = typeof(PictureWidget),
                WidgetViewModelType = typeof(PictureViewModel),
                WidgetModelType = typeof(PictureModel)
            },
            new MVVMUnit()
            {
                WidgetType = typeof(NoteWidget),
                WidgetViewModelType = typeof(NoteViewModel),
                WidgetModelType = typeof(NoteModel)
            },
            new MVVMUnit()
            {
                WidgetType = typeof(CalculatorWidget),
                WidgetViewModelType = typeof(CalculatorViewModel),
                WidgetModelType = typeof(CalculatorModel)
            },
            new MVVMUnit()
            {
                WidgetType = typeof(BrowserWidget),
                WidgetViewModelType = typeof(BrowserViewModel),
                WidgetModelType = typeof(BrowserModel)
            },
            new MVVMUnit()
            {
                WidgetType = typeof(KeyDetectorWidget),
                WidgetViewModelType = typeof(KeyDetectorViewModel),
                WidgetModelType = typeof(KeyDetectorModel)
            },
        };

        /// <summary>
        /// Convert widget type to view model type. 
        /// </summary>
        /// <param name="widgetType">Widget type.</param>
        /// <returns>View model type.</returns>
        public static Type GetViewModelTypeOfWidgetType(Type widgetType)
        {
            foreach (var wtu in WidgetsList)
            {
                if (wtu.WidgetType == widgetType)
                    return wtu.WidgetViewModelType;
            }
            return null;
        }

        /// <summary>
        /// Convert view model type to widget type.
        /// </summary>
        /// <param name="viewModelType">View model type.</param>
        /// <returns>Widget type.</returns>
        public static Type GetWidgetTypeOfViewModelType(Type viewModelType)
        {
            foreach (var wtu in WidgetsList)
            {
                if (wtu.WidgetType == viewModelType)
                    return wtu.WidgetType;
            }
            return null;
        }

        /// <summary>
        /// Coonvert view model type to model type.
        /// </summary>
        /// <param name="viewModelType">View model type.</param>
        /// <returns>Models type.</returns>
        public static Type GetModelTypeOfViewModelType(Type viewModelType)
        {
            foreach (var wtu in WidgetsList)
            {
                if (wtu.WidgetViewModelType == viewModelType)
                    return wtu.WidgetModelType;
            }
            return null;
        }

        /// <summary>
        /// Convert model type to view model type.
        /// </summary>
        /// <param name="modelType">Model type.</param>
        /// <returns>View model type.</returns>
        public static Type GetViewModelTypeOfModelType(Type modelType)
        {
            foreach (var wtu in WidgetsList)
            {
                if (wtu.WidgetModelType == modelType)
                    return wtu.WidgetViewModelType;
            }
            return null;
        }

        /// <summary>
        /// Convert widget type to model type. 
        /// </summary>
        /// <param name="widgetType">Widget type.</param>
        /// <returns>Model type.</returns>
        public static Type GetModelTypeOfWidgetType(Type widgetType)
        {
            foreach (var wtu in WidgetsList)
            {
                if (wtu.WidgetType == widgetType)
                    return wtu.WidgetModelType;
            }
            return null;
        }

        /// <summary>
        /// Convert model type to widget type.
        /// </summary>
        /// <param name="modelType">Model type.</param>
        /// <returns>Widget type.</returns>
        public static Type GetWidgetTypeOfModelType(Type modelType)
        {
            foreach (var wtu in WidgetsList)
            {
                if (wtu.WidgetModelType == modelType)
                    return wtu.WidgetType;
            }
            return null;
        }

        /// <summary>
        /// Reset widget configuration to default without saving and show it.
        /// </summary>
        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="ViewModelType">Type of widget's view model.</typeparam>
        /// <typeparam name="ModelType">Type of widget model.</typeparam>
        /// <param name="widgetContainer">The box with widget to reset.</param>
        public static void ResetWidgetConfigurationToDefaultWithoutSaving<WidgetType, ViewModelType, ModelType>(ref WidgetContainer<WidgetType> widgetContainer)
            where WidgetType : WidgetBase, new()
            where ViewModelType : class, IWidgetViewModel<ModelType>, new()
            where ModelType : WidgetModelBase, new()
        {
            if (widgetContainer.Widget != null)
            {
                CloseWidget<WidgetType, ModelType>(ref widgetContainer.Widget);
            }
            widgetContainer.Widget = CreateWidget<WidgetType, ViewModelType, ModelType>(new ModelType());
            (widgetContainer.Widget.DataContext as IWidgetViewModel<ModelType>).LoadModel();
            widgetContainer.Widget.Show();
        }

        /// <summary>
        /// Reset widget configuration to default with saving and show it.
        /// </summary>
        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="ViewModelType">Type of widget's view model.</typeparam>
        /// <typeparam name="ModelType">Type of widget model.</typeparam>
        /// <param name="widgetContainer">The box with widget to reset.</param>
        public static void ResetWidgetConfigurationToDefault<WidgetType, ViewModelType, ModelType>(ref WidgetContainer<WidgetType> widgetContainer, Action action = null)
            where WidgetType : WidgetBase, new()
            where ViewModelType : class, IWidgetViewModel<ModelType>, new()
            where ModelType : WidgetModelBase, new()
        {
            ResetWidgetConfigurationToDefaultWithoutSaving<WidgetType, ViewModelType, ModelType>(ref widgetContainer);
            SaveWidgetConfigurationInFile<WidgetType, ModelType>(widgetContainer.Widget);
            action?.Invoke();
        }

        /// <summary>
        /// Reset widget configuration to default with saving, show it and push informing message box.
        /// </summary>
        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="ViewModelType">Type of widget's view model.</typeparam>
        /// <typeparam name="ModelType">Type of widget model.</typeparam>
        /// <param name="widgetContainer">The box with widget to reset.</param>
        /// <param name="action">Method to invoke.</param>
        public static void ResetWidgetConfigurationToDefaultWithMessageBox<WidgetType, ViewModelType, ModelType>(this WidgetContainer<WidgetType> widgetContainer, Action action = null)
            where WidgetType : WidgetBase, new()
            where ViewModelType : class, IWidgetViewModel<ModelType>, new()
            where ModelType : WidgetModelBase, new()
        {
            if (MessageBox.Show("Should you set widget configuration to default?\n(Warning, if you restore the default settings you will not be able to restore the current data.)", "Setting configuration to default:", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                ResetWidgetConfigurationToDefault<WidgetType, ViewModelType, ModelType>(ref widgetContainer, action);
            }
        }

        /// <summary>
        /// Load configuration form file.
        /// </summary>
        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="ViewModelType">Type of widget's view model.</typeparam>
        /// <typeparam name="ModelType">Type of widget model.</typeparam>
        /// <param name="widgetContainer">The box with widget to reset.</param>
        /// <param name="action">Method to invoke.</param>
        public static void LoadConfigurationFromFile<WidgetType, ViewModelType, ModelType>(this WidgetContainer<WidgetType> widgetContainer, Action action = null)
            where WidgetType : WidgetBase, new()
            where ViewModelType : class, IWidgetViewModel<ModelType>, new()
            where ModelType : WidgetModelBase, new()
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
                    var model = new ModelType();
                    using (var sr = File.OpenText(fileDialog.FileName))
                    {
                        model = JsonConvert.DeserializeObject<ModelType>(sr.ReadToEnd());
                        SaveWidgetConfigurationInFile(model);
                    }

                    if (widgetContainer.Widget != null)
                    {
                        CloseWidget<WidgetType, ModelType>(ref widgetContainer.Widget);
                    }
                    LoadWidget<WidgetType, ViewModelType, ModelType>(ref widgetContainer.Widget);
                    widgetContainer.Widget?.Show();

                    // Action invoke:
                    action?.Invoke();
                }
            }
        }

        /// <summary>
        /// Return new widget.
        /// </summary>
        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="ViewModelType">Type of widget's view model.</typeparam>
        /// <typeparam name="ModelType">Type of widget model.</typeparam>
        /// <returns>Widget.</returns>
        public static WidgetType CreateWidget<WidgetType, ViewModelType, ModelType>(ModelType widgetModel = null)
            where WidgetType : WidgetBase, new()
            where ViewModelType : class, IWidgetViewModel<ModelType>, new()
            where ModelType : WidgetModelBase, new()
        {
            AppFileSystem.CheckDiresArchitecture();

            WidgetType widget;

            if (widgetModel == null)
                widget = new WidgetType()
                {
                    DataContext = new ViewModelType()
                };
            else
                widget = new WidgetType()
                {
                    DataContext = new ViewModelType()
                    {
                        WidgetModel = widgetModel
                    }
                };
            return widget;
        }

        /// <summary>
        /// Save widget model to file by JSON way.
        /// </summary>
        /// <param name="widget">Widget with widget configuration (in data context) to save.</param>
        /// <param name="path">Path to file where it save widget configuration.</param>
        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="ModelType">Type of widget model.</typeparam>
        /// <returns>Operation result.</returns>
        public static bool SaveWidgetConfigurationInFile<WidgetType, ModelType>(WidgetType widget, string path)
            where WidgetType : WidgetBase, new()
            where ModelType : WidgetModelBase, new()
        {
            if (widget == null)
            {
                return false;
            }

            var widgetModel = (widget.DataContext as IWidgetViewModel<ModelType>).WidgetModel;

            SaveWidgetConfigurationInFile(widgetModel, path);

            return true;
        }

        /// <summary>
        /// Save widget model to file by JSON way.
        /// </summary>
        /// <param name="widget">Widget with widget configuration (in data context) to save.</param>
        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="ModelType">Type of widget model.</typeparam>
        /// <returns>Operation result.</returns>
        public static bool SaveWidgetConfigurationInFile<WidgetType, ModelType>(WidgetType widget)
            where WidgetType : WidgetBase, new()
            where ModelType : WidgetModelBase, new()
        {
            Directory.CreateDirectory(AppFileSystem.GetSaveDireConfigurationPath(typeof(WidgetType).Name));
            return SaveWidgetConfigurationInFile<WidgetType, ModelType>(widget, AppFileSystem.GetSaveFileConfigurationPath(typeof(WidgetType).Name));
        }

        /// <summary>
        /// Save widget model to file by JSON way.
        /// </summary>
        /// <param name="widget">Widget with widget configuration (in data context) to save.</param>
        /// <param name="path">Path to file where it save widget configuration.</param>
        /// <typeparam name="ModelType">Type of widget model.</typeparam>
        /// <returns>Operation result.</returns>
        public static bool SaveWidgetConfigurationInFile<ModelType>(ModelType model, string path)
            where ModelType : WidgetModelBase, new()
        {
            if (model == null)
            {
                return false;
            }

            var directoryPath = path.TrimStart(Path.DirectorySeparatorChar);
            if (Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(path.TrimStart(Path.DirectorySeparatorChar));
            }

            using (var sw = File.CreateText(path))
            {
                sw.Write(JsonConvert.SerializeObject(model));
            }

            return true;
        }

        /// <summary>
        /// Save widget model to file by JSON way.
        /// </summary>
        /// <param name="widget">Widget with widget configuration (in data context) to save.</param>
        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="ModelType">Type of widget model.</typeparam>
        /// <returns>Operation result.</returns>
        public static bool SaveWidgetConfigurationInFile<ModelType>(ModelType model)
            where ModelType : WidgetModelBase, new()
        {
            Directory.CreateDirectory(AppFileSystem.GetSaveDireConfigurationPath(GetWidgetTypeOfModelType(typeof(ModelType)).Name));
            return SaveWidgetConfigurationInFile(model, AppFileSystem.GetSaveFileConfigurationPath(GetWidgetTypeOfModelType(typeof(ModelType)).Name));
        }

        /// <summary>
        /// Read widget configuration from path, save it in widgetModel and return operation's result.
        /// </summary>
        /// <param name="widgetModel">Widget with downloaded configuration.</param>
        /// <param name="path">Path to json file with save of widget configuration.</param>
        /// <typeparam name="WidgetModelType">Type of widget model.</typeparam>
        /// <returns>Operation result.</returns>
        public static bool DownloadWidgetConfigurationFromFile<WidgetModelType>(out WidgetModelType widgetModel, string path)
            where WidgetModelType : WidgetModelBase, new()
        {
            widgetModel = null;

            if (!File.Exists(path))
            {
                return false;
            }

            using (var sr = new StreamReader(path))
            {
                widgetModel = JsonConvert.DeserializeObject<WidgetModelType>(sr.ReadToEnd());
            }
            return true;
        }

        /// <summary>
        /// Read widget configuration from path, save it in widgetModel and return operation's result.
        /// </summary>
        /// <param name="widgetModel">Widget with downloaded configuration.</param>
        /// <typeparam name="WidgetModelType">Type of widget model.</typeparam>
        /// <returns>Operation result.</returns>
        public static bool DownloadWidgetConfigurationFromFile<WidgetModelType>(out WidgetModelType widgetModel)
            where WidgetModelType : WidgetModelBase, new()
        {
            return DownloadWidgetConfigurationFromFile(out widgetModel, AppFileSystem.GetSaveFileConfigurationPath(GetWidgetTypeOfModelType(typeof(WidgetModelType)).Name));
        }

        //                //
        // load and save: //
        //                //

        /// <summary>
        /// Change widget's state and save configuration. 
        /// </summary>
        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="ViewModelType">Type of view model.</typeparam>
        /// <typeparam name="ModelType">Type of model.</typeparam>
        /// <param name="widget">Widget to change state.</param>
        public static void Widget_ChangeStateAndSave<WidgetType, ViewModelType, ModelType>(ref WidgetType widget)
            where WidgetType : WidgetBase, new()
            where ViewModelType : class, IWidgetViewModel<ModelType>, new()
            where ModelType : WidgetModelBase, new()
        {
            var downloadedConfigurationResult = DownloadWidgetConfigurationFromFile(out ModelType model);

            if (widget != null)
            {
                CloseWidget(ref widget, ref model);
                goto END;
            }

            BuildWidget<WidgetType, ViewModelType, ModelType>(ref widget, ref model, downloadedConfigurationResult);
        END:
            SaveWidgetConfigurationInFile(model);
        }

        /// <summary>
        /// Create or close widget.
        /// </summary>
        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="ViewModelType">Type of view model.</typeparam>
        /// <typeparam name="ModelType">Type of model.</typeparam>
        /// <param name="widget"></param>
        /// <param name="state"></param>
        public static void ChangeWidgetState<WidgetType, ViewModelType, ModelType>(ref WidgetType widget, bool state)
            where WidgetType : WidgetBase, new()
            where ViewModelType : class, IWidgetViewModel<ModelType>, new()
            where ModelType : WidgetModelBase, new()
        {
            var downloadedConfigurationResult = DownloadWidgetConfigurationFromFile(out ModelType model);

            if (state == false && widget != null)
            {
                CloseWidget(ref widget, ref model);
            }

            if (state == true && widget == null)
            {
                BuildWidget<WidgetType, ViewModelType, ModelType>(ref widget, ref model, downloadedConfigurationResult);
            }

            SaveWidgetConfigurationInFile(model);
        }

        /// <summary>
        /// Build widget.
        /// </summary>
        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="ViewModelType">Type of view model.</typeparam>
        /// <typeparam name="ModelType">Type of model.</typeparam>
        /// <param name="widget">Widget to build.</param>
        /// <param name="downloadedModel">Widget's model.</param>
        /// <param name="downloadWidgetConfigurationResult">If true build widget use save configuration,
        /// if false build new default widget</param>
        public static void BuildWidget<WidgetType, ViewModelType, ModelType>(ref WidgetType widget, ref ModelType downloadedModel, bool downloadWidgetConfigurationResult)
            where WidgetType : WidgetBase, new()
            where ViewModelType : class, IWidgetViewModel<ModelType>, new()
            where ModelType : WidgetModelBase, new()
        {
            if (downloadWidgetConfigurationResult)
            {
                widget = CreateWidget<WidgetType, ViewModelType, ModelType>(downloadedModel);
            }
            else
            {
                widget = CreateWidget<WidgetType, ViewModelType, ModelType>();
                downloadedModel = (widget.DataContext as IWidgetViewModel<ModelType>).WidgetModel;
            }
            widget.Show();
            downloadedModel.IsActive = true;
        }

        /// <summary>
        /// Close widget.
        /// </summary>
        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="ModelType">Type of model.</typeparam>
        /// <param name="widget">Widget to close.</param>
        /// <param name="model">Widget's model.</param>
        public static void CloseWidget<WidgetType, ModelType>(ref WidgetType widget, ref ModelType model)
            where WidgetType : WidgetBase, new()
            where ModelType : WidgetModelBase, new()
        {
            (widget?.DataContext as IWidgetViewModel<ModelType>)?.WidgetModel.AnimationMemberDepose_Invoke();

            widget?.Close();
            widget = null;
            model.IsActive = false;
        }

        /// <summary>
        /// Close widget and return model before closing.
        /// </summary>
        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="ModelType">Type of model.</typeparam>
        /// <param name="widget">Widget to close.</param>
        /// <returns>Widget model.</returns>
        public static ModelType CloseWidget<WidgetType, ModelType>(ref WidgetType widget)
            where WidgetType : WidgetBase, new()
            where ModelType : WidgetModelBase, new()
        {
            (widget?.DataContext as IWidgetViewModel<ModelType>)?.WidgetModel.AnimationMemberDepose_Invoke();
            DownloadWidgetConfigurationFromFile(out ModelType model);
            widget?.Close();
            widget = null;
            return model;
        }

        /// <summary>
        /// Close and save widget.
        /// </summary>
        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="ModelType">Type of model.</typeparam>
        /// <param name="widget">Widget to close.</param>
        public static void CloseAndSaveWidget<WidgetType, ModelType>(ref WidgetType widget)
            where WidgetType : WidgetBase, new()
            where ModelType : WidgetModelBase, new()
        {
            SaveWidgetConfigurationInFile(CloseWidget<WidgetType, ModelType>(ref widget));
        }

        /// <summary>
        /// Load widget (build or not).
        /// </summary>
        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="ViewModelType">Type of view model.</typeparam>
        /// <typeparam name="ModelType">Type of model.</typeparam>
        /// <param name="widget">Widget to load.</param>
        public static void LoadWidget<WidgetType, ViewModelType, ModelType>(ref WidgetType widget)
            where WidgetType : WidgetBase, new()
            where ViewModelType : class, IWidgetViewModel<ModelType>, new()
            where ModelType : WidgetModelBase, new()
        {
            var downloadedConfigurationResult = DownloadWidgetConfigurationFromFile(out ModelType model);

            if (!downloadedConfigurationResult || model.IsActive)
            {
                BuildWidget<WidgetType, ViewModelType, ModelType>(ref widget, ref model, downloadedConfigurationResult);
            }

            SaveWidgetConfigurationInFile(model);
        }

        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="ModelType">Type of model.</typeparam>
        /// <param name="widget">Widget to load.</param>
        public static ModelType GetModelFromWidget<WidgetType, ModelType>(ref WidgetType widget)
            where WidgetType : WidgetBase, new()
            where ModelType : WidgetModelBase, new()
        {
            return (widget.DataContext as IWidgetViewModel<ModelType>).WidgetModel;
        }
    }
}
