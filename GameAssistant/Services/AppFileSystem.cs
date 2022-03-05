using System;
using System.IO;
using System.Windows;

namespace GameAssistant.Services
{
    internal class AppFileSystem
    {
        /// <summary>
        /// Path to main dire of app informations and save configurations.
        /// </summary>
        private static string WidgetsConfigurationsMainDire = Path.Combine("C:\\", "Users", Environment.UserName, Application.ResourceAssembly.GetName().Name);

        /// <summary>
        /// Return widgets save configuration dire path.
        /// </summary>
        /// <param name="widgetName">Name of widget.</param>
        /// <returns>Save dire configuration path.</returns>
        public static string GetSaveDireConfigurationPath(string widgetName)
        {
            return Path.Combine(WidgetsConfigurationsMainDire, $"{widgetName}Settings");
        }

        /// <summary>
        /// Return widgets save configuration file path.
        /// </summary>
        /// <param name="widgetName">Name of widget.</param>
        /// <returns>Save file configuration path.</returns>
        public static string GetSaveFileConfigurationPath(string widgetName)
        {
            return Path.Combine(GetSaveDireConfigurationPath(widgetName), $"{widgetName}_configuration.json");
        }

        /// <summary>
        /// Register paths.
        /// Use this method one time at the start. 
        /// </summary>
        /// <param name="widgetTypesNames">Widgets names.</param>
        public static void RegisterFileSystem(params string[] widgetTypesNames)
        {
            // Set main dire path:
            WidgetsConfigurationsMainDire = Path.Combine(App.DiskName, "Users", Environment.UserName, Application.ResourceAssembly.GetName().Name);

            // Set widgets dires paths:
            Directory.CreateDirectory(WidgetsConfigurationsMainDire);

            foreach (var widgetTypeName in widgetTypesNames)
            {
                CheckDiresArchitectureEvent += () =>
                {
                    Directory.CreateDirectory(GetSaveDireConfigurationPath(widgetTypeName));
                };
            };

            CheckDiresArchitectureEvent.Invoke();
        }

        /// <summary>
        /// Contains check file system metods.
        /// </summary>
        private static event Action CheckDiresArchitectureEvent;

        /// <summary>
        /// Check check file system metods.
        /// </summary>
        public static void CheckDiresArchitecture()
        {
            CheckDiresArchitectureEvent.Invoke();
        }
    }
}
