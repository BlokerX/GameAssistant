using NewGameAssistant.Services;
using NewGameAssistant.Widgets;
using System.IO;
using System.Windows;

namespace NewGameAssistant
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SelectDisks();
            AppFileSystem.RegisterFileSystem(
                nameof(ClockWidget),
                nameof(PictureWidget)
                );
        }

        private static void SelectDisks()
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                foreach (var directory in Directory.GetDirectories(drive.Name))
                {
                    if (directory == Path.Combine(drive.Name, "Users"))
                    {
                        DiskName = drive.Name;
                        goto END;
                    }
                }
            }
        END:;
        }

        private static string _diskName;
        /// <summary>
        /// Name of disc with users dire.
        /// </summary>
        public static string DiskName
        {
            get => _diskName;
            set => _diskName = value;
        }
    }
}
