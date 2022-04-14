using GameAssistant.Models;
using GameAssistant.Widgets;
using GameAssistant.WidgetViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

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
        /// Return new widget.
        /// </summary>
        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="WidgetViewModelType">Type of widget's view model.</typeparam>
        /// <typeparam name="WidgetModelType">Type of widget model.</typeparam>
        /// <returns>Widget.</returns>
        public static WidgetType CreateWidget<WidgetType, WidgetViewModelType, WidgetModelType>(WidgetModelType widgetModel)
            where WidgetType : WidgetBase, new()
            where WidgetViewModelType : class, IWidgetViewModel<WidgetModelType>, new()
            where WidgetModelType : WidgetModelBase, new()
        {
            AppFileSystem.CheckDiresArchitecture();

            return new WidgetType()
            {
                DataContext = new WidgetViewModelType()
                {
                    WidgetModel = widgetModel
                }
            };
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
                widgetModel.AnimationToken = false;
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
            var downloadedConfigurationResult = WidgetManager.DownloadWidgetConfigurationFromFile(out ModelType model);

            if (widget != null)
            {
                CloseWidget(ref widget, ref model);
                goto END;
            }

            BuildWidget<WidgetType, ViewModelType, ModelType>(ref widget, ref model, downloadedConfigurationResult);
        END:
            WidgetManager.SaveWidgetConfigurationInFile(model);
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
            var downloadedConfigurationResult = WidgetManager.DownloadWidgetConfigurationFromFile(out ModelType model);

            if (state == false && widget != null)
            {
                WidgetManager.CloseWidget(ref widget, ref model);
            }

            if (state == true && widget == null)
            {
                WidgetManager.BuildWidget<WidgetType, ViewModelType, ModelType>(ref widget, ref model, downloadedConfigurationResult);
            }

            WidgetManager.SaveWidgetConfigurationInFile(model);
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
            if (downloadedModel != null)
                downloadedModel.AnimationToken = true;
            if (downloadWidgetConfigurationResult)
            {
                widget = WidgetManager.CreateWidget<WidgetType, ViewModelType, ModelType>(downloadedModel);
            }
            else
            {
                widget = new WidgetType();
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
            // todo zamykanie menadżera animacji (przenieść we właściwe miejsce)
            (widget.DataContext as IWidgetViewModel<ModelType>).WidgetModel.AnimationToken = false;

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
            // todo zamykanie menadżera animacji (przenieść we właściwe miejsce)
            if (widget != null)
                (widget.DataContext as IWidgetViewModel<ModelType>).WidgetModel.AnimationToken = false;

            WidgetManager.DownloadWidgetConfigurationFromFile(out ModelType model);
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
            WidgetManager.SaveWidgetConfigurationInFile(CloseWidget<WidgetType, ModelType>(ref widget));
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
            var downloadedConfigurationResult = WidgetManager.DownloadWidgetConfigurationFromFile(out ModelType model);

            if (!downloadedConfigurationResult || model.IsActive)
            {
                BuildWidget<WidgetType, ViewModelType, ModelType>(ref widget, ref model, downloadedConfigurationResult);
            }

            WidgetManager.SaveWidgetConfigurationInFile(model);
        }

        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="ModelType">Type of model.</typeparam>
        /// <param name="widget">Widget to load.</param>
        public static ModelType GetModelFromWidget<WidgetType, ModelType>(ref WidgetType widget)
            where WidgetType : WidgetBase, new()
            where ModelType : WidgetModelBase, new()
        {
            // todo pozyskiwanie modelu z widgetu
            return (widget.DataContext as IWidgetViewModel<ModelType>).WidgetModel;
        }
    }
}
