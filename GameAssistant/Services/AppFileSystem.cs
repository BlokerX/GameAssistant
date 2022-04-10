using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace GameAssistant.Services
{
    /// <summary>
    /// Manage applicatoion's file system. 
    /// </summary>
    internal static class AppFileSystem
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

        #region Autostart Options

        public static void CreateStartupKey()
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            reg.SetValue("Game Assistant", System.Reflection.Assembly.GetExecutingAssembly().Location);
        }
        
        public static void DeleteStartupKey()
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            reg.DeleteValue("Game Assistant", false);
        }
        
        public static bool CheckStartupKeyValue()
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            var value = reg.GetValue("Game Assistant");
            if(value != null)
                return true;
            return false;
        }

        //public static void SwitchOnStartupState()
        //{
            
        //}
        
        //public static void SwitchOffStartupState()
        //{
            
        //}

        #endregion

    }
}
