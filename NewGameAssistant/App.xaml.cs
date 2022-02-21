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
        /// <summary>
        /// The clock widget.
        /// </summary>
        private ClockWidget ClockWidget;
        #endregion

        #region PictureWidget
        /// <summary>
        /// The picture widget.
        /// </summary>
        private PictureWidget PictureWidget;
        #endregion

        #region NoteWidget
        /// <summary>
        /// The note widget.
        /// </summary>
        private NoteWidget NoteWidget;
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
            Widget_ChangeStateAndSave<ClockWidget, ClockViewModel, ClockModel>(ref ClockWidget);
        }
        
        /// <summary>
        /// Invoke when picture widget button clicked.
        /// </summary>
        private void NotifyIcon_PictureWidget_Settings_Click(object sender, System.EventArgs e)
        {
            Widget_ChangeStateAndSave<PictureWidget, PictureViewModel, PictureModel>(ref PictureWidget);
        }
        
        /// <summary>
        /// Invoke when note widget button clicked.
        /// </summary>
        private void NotifyIcon_NoteWidget_Settings_Click(object sender, System.EventArgs e)
        {
            Widget_ChangeStateAndSave<NoteWidget, NoteViewModel, NoteModel>(ref NoteWidget);
        }

        /// <summary>
        /// Change widget's state and save configuration. 
        /// </summary>
        /// <typeparam name="WidgetType"></typeparam>
        /// <typeparam name="ViewModelType"></typeparam>
        /// <typeparam name="ModelType"></typeparam>
        /// <param name="widget"></param>
        private void Widget_ChangeStateAndSave<WidgetType, ViewModelType, ModelType>(ref WidgetType widget)
            where WidgetType : WidgetBase, new()
            where ViewModelType : class, IWidgetViewModel<ModelType>, new()
            where ModelType : WidgetModelBase, new()
        {
            var dwcff = WidgetMenager.DownloadWidgetConfigurationFromFile(out ModelType model);

            if (widget != null)
            {
                widget?.Close();
                widget = null;
                model.IsActive = false;
                goto END;
            }

            if (dwcff)
            {
                model.IsActive = true;
                widget = WidgetMenager.CreateWidget<WidgetType, ViewModelType, ModelType>(model);
            }
            else
                widget = new WidgetType();

            widget.Show();

        END:
            WidgetMenager.SaveWidgetConfigurationInFile(model);
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
                        new System.Windows.Forms.MenuItem("Picture widget", NotifyIcon_PictureWidget_Settings_Click),
                        new System.Windows.Forms.MenuItem("Note widget", NotifyIcon_NoteWidget_Settings_Click),
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
