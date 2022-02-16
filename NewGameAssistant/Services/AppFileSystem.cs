using NewGameAssistant.Widgets;
using System;
using System.IO;
using System.Windows;

namespace NewGameAssistant.Services
{
    internal class AppFileSystem
    {
        #region DiresArchitecture

        /// <summary>
        /// Path to main dire of app informations and save configurations.
        /// </summary>
        public static string WidgetsConfigurationsMainDire;

        public static string GetSaveDireConfigurationPath(string widgetType)
        {
            return Path.Combine(WidgetsConfigurationsMainDire, $"{widgetType}Settings");
        }

        public static string GetSaveFileConfigurationPath(string widgetType)
        {
            return Path.Combine(GetSaveDireConfigurationPath(widgetType), $"{widgetType}_configuration.json");
        }

        /// <summary>
        /// Register paths.
        /// Use this method one time at the start. 
        /// </summary>
        /// <param name="widgetTypesNames">Widgets names</param>
        public static void RegisterFileSystem(params string[] widgetTypesNames)
        {
            // Set main dire path:
            WidgetsConfigurationsMainDire = Path.Combine(App.DiskName, "Users", Environment.UserName, Application.ResourceAssembly.GetName().Name);

            // Set widgets dires paths:
            Directory.CreateDirectory(WidgetsConfigurationsMainDire);
            
            foreach(var widgetTypeName in widgetTypesNames)
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

        #endregion
    }
}
