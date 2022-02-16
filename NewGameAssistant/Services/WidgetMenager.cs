using NewGameAssistant.WidgetModels;
using NewGameAssistant.Widgets;
using Newtonsoft.Json;
using System.IO;

namespace NewGameAssistant.Services
{
    /// <summary>
    /// Widget's helper class.
    /// </summary>
    /// <typeparam name="WidgetType">Type of widget.</typeparam>
    /// <typeparam name="WidgetModelType">Type of widget model.</typeparam>
    internal static class WidgetMenager<WidgetType, WidgetModelType>
    where WidgetType : WidgetBase, new() where WidgetModelType : WidgetModelBase, new()
    {

        /// <summary>
        /// Return new widget.
        /// </summary>
        /// <returns>Widget.</returns>
        public static WidgetType CreateWidget()
        {
            AppFileSystem.CheckDiresArchitecture();
            return new WidgetType();
        }
            
        /// <summary>
        /// Save widget model to file by JSON way.
        /// </summary>
        /// <param name="widget">Widget with widget configuration (in data context) to save.</param>
        /// <param name="path">Path to file where it save widget configuration.</param>
        /// <returns>Operation result.</returns>
        public static bool SaveWidgetConfigurationInFile(WidgetType widget, string path)
        {
            if (widget == null)
            {
                return false;
            }

            var widgetModel = widget.DataContext as WidgetModelType;

            var directoryPath = path.TrimStart(Path.DirectorySeparatorChar);
            if (Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(path.TrimStart(Path.DirectorySeparatorChar));
            }

            using (var sw = File.CreateText(path))
            {
                sw.Write(JsonConvert.SerializeObject(widgetModel));
            }

            return true;
        }

        /// <summary>
        /// Save widget model to file by JSON way.
        /// </summary>
        /// <param name="widget">Widget with widget configuration (in data context) to save.</param>
        /// <returns>Operation result.</returns>
        public static bool SaveWidgetConfigurationInFile(WidgetType widget)
        {
            Directory.CreateDirectory(AppFileSystem.GetSaveDireConfigurationPath(typeof(WidgetType).Name));
            return SaveWidgetConfigurationInFile(widget, AppFileSystem.GetSaveFileConfigurationPath(typeof(WidgetType).Name));
        }

        /// <summary>
        /// Read widget configuration from path, save it in widgetModel and return operation's result.
        /// </summary>
        /// <param name="widgetModel">Widget with downloaded configuration.</param>
        /// <param name="path">Path to json file with save of widget configuration.</param>
        /// <returns>Operation result.</returns>
        public static bool DownloadWidgetConfigurationFromFile(out WidgetModelType widgetModel, string path)
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
        /// <returns>Operation result.</returns>
        public static bool DownloadWidgetConfigurationFromFile(out WidgetModelType widgetModel)
        {
            return DownloadWidgetConfigurationFromFile(out widgetModel, AppFileSystem.GetSaveFileConfigurationPath(typeof(WidgetType).Name));
        }

    }
}
