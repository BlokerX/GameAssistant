using NewGameAssistant.Models;
using NewGameAssistant.Services;
using NewGameAssistant.Widgets;
using NewGameAssistant.WidgetViewModels;
using NewGameAssistant.Windows;
using System.IO;
using System.Windows;

namespace NewGameAssistant
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Widgets

        #region ClockWidget
        private ClockWidget _clockWidget;
        /// <summary>
        /// The clock widget.
        /// </summary>
        public ClockWidget ClockWidget
        {
            get => _clockWidget;
            set => _clockWidget = value;
        }
        #endregion

        #region PictureWidget
        private PictureWidget _pictureWidget;
        /// <summary>
        /// The picture widget.
        /// </summary>
        public PictureWidget PictureWidget
        {
            get => _pictureWidget;
            set => _pictureWidget = value;
        }
        #endregion

        #region NoteWidget
        private NoteWidget _noteWidget;
        /// <summary>
        /// The note widget.
        /// </summary>
        public NoteWidget NoteWidget
        {
            get => _noteWidget;
            set => _noteWidget = value;
        }
        #endregion

        #endregion

        #region NotifyIcon
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        /// <summary>
        /// The notify icon.
        /// </summary>
        public System.Windows.Forms.NotifyIcon NotifyIcon
        {
            get => _notifyIcon;
            set => _notifyIcon = value;
        }

        /// <summary>
        /// Invoke when notify icon clicked.
        /// </summary>
        private void NotifyIcon_Click(object sender, System.EventArgs e)
        {
            OpenSettingsWindow();
        }

        /// <summary>
        /// Invoke when close app button clicked.
        /// </summary>
        private void NotifyIcon_MenuItem_CloseApp_Click(object sender, System.EventArgs e)
        {
            App.Current.Shutdown();
        }

        /// <summary>
        /// Invoke when settings button clicked.
        /// </summary>
        private void NotifyIcon_MenuItem_Settings_Click(object sender, System.EventArgs e)
        {
            OpenSettingsWindow();
        }

        /// <summary>
        /// Invoke when clock widget button clicked.
        /// </summary>
        private void NotifyIcon_ClockWidget_Settings_Click(object sender, System.EventArgs e)
        {
            var dwcff = WidgetMenager.DownloadWidgetConfigurationFromFile(out ClockModel cw);

            if (ClockWidget != null)
            {
                ClockWidget.Close();
                ClockWidget = null;
                cw.IsActive = false;
                goto END;
            }

            if (dwcff)
            {
                cw.IsActive = true;
                ClockWidget = WidgetMenager.CreateWidget<ClockWidget, ClockViewModel, ClockModel>(cw);
            }
            else
                ClockWidget = new ClockWidget();

            ClockWidget.Show();

        END:
            WidgetMenager.SaveWidgetConfigurationInFile(cw);
        }

        #endregion

        #region Settings window
        /// <summary>
        /// Open setting window.
        /// </summary>
        private static void OpenSettingsWindow()
        {
            var sw = new SettingsWindow();
            sw.Show();
            //todo
        }

        #endregion

        #region Ovverides methods

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SelectDisks();
            AppFileSystem.RegisterFileSystem(
                nameof(ClockWidget),
                nameof(PictureWidget),
                nameof(NoteWidget)
                );

            // Notify icon register:
            NotifyIcon = new System.Windows.Forms.NotifyIcon()
            {
                Visible = true,
                Icon = new System.Drawing.Icon("C:\\Users\\Lenovo\\Desktop\\favicon.ico"),
                ContextMenu = new System.Windows.Forms.ContextMenu(
                    new System.Windows.Forms.MenuItem[]
                    {
                        new System.Windows.Forms.MenuItem("Clock widget", NotifyIcon_ClockWidget_Settings_Click),
                        new System.Windows.Forms.MenuItem("Picture widget"),
                        new System.Windows.Forms.MenuItem("Note widget"),
                        new System.Windows.Forms.MenuItem("-"),
                        new System.Windows.Forms.MenuItem("Settings", NotifyIcon_MenuItem_Settings_Click),
                        new System.Windows.Forms.MenuItem("-"),
                        new System.Windows.Forms.MenuItem("Close app", NotifyIcon_MenuItem_CloseApp_Click)
                    }
                ),
                //todo notify icon picture's Icon = ,
            };
            NotifyIcon.DoubleClick += NotifyIcon_Click;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            NotifyIcon.Dispose();
            base.OnExit(e);
        }

        #endregion

        #region Select global disk

        /// <summary>
        /// Set DiskName to default disk.
        /// </summary>
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

        /// <summary>
        /// Name of disc that has users dire.
        /// </summary>
        public static string DiskName { get; private set; }

        #endregion

    }
}
