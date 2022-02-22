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
        /// <summary>
        /// The notify icon.
        /// </summary>
        private System.Windows.Forms.NotifyIcon NotifyIcon;

        /// <summary>
        /// Invoke when notify icon clicked.
        /// </summary>
        private void NotifyIcon_Click(object sender, System.EventArgs e)
        {
            OpenSettingsWindow();
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
        /// Invoke when close app button clicked.
        /// </summary>
        private void NotifyIcon_MenuItem_CloseApp_Click(object sender, System.EventArgs e)
        {
            App.Current.Shutdown();
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
            AppFileSystem.RegisterFileSystem
            (
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

            LoadWidgets();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            CloseAndSaveWidgets();
            NotifyIcon.Dispose();
            base.OnExit(e);
        }

        #endregion

        #region Widgets methods

        /// <summary>
        /// Change widget's state and save configuration. 
        /// </summary>
        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="ViewModelType">Type of view model.</typeparam>
        /// <typeparam name="ModelType">Type of model.</typeparam>
        /// <param name="widget">Widget to change state.</param>
        private void Widget_ChangeStateAndSave<WidgetType, ViewModelType, ModelType>(ref WidgetType widget)
            where WidgetType : WidgetBase, new()
            where ViewModelType : class, IWidgetViewModel<ModelType>, new()
            where ModelType : WidgetModelBase, new()
        {
            var downloadedConfigurationResult = WidgetMenager.DownloadWidgetConfigurationFromFile(out ModelType model);

            if (widget != null)
            {
                CloseWidget(ref widget, ref model);
                goto END;
            }

            BuildWidget<WidgetType, ViewModelType, ModelType>(ref widget, ref model, downloadedConfigurationResult);

        END:
            WidgetMenager.SaveWidgetConfigurationInFile(model);
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
        private static void BuildWidget<WidgetType, ViewModelType, ModelType>(ref WidgetType widget, ref ModelType downloadedModel, bool downloadWidgetConfigurationResult)
            where WidgetType : WidgetBase, new()
            where ViewModelType : class, IWidgetViewModel<ModelType>, new()
            where ModelType : WidgetModelBase, new()
        {
            if (downloadWidgetConfigurationResult)
            {
                widget = WidgetMenager.CreateWidget<WidgetType, ViewModelType, ModelType>(downloadedModel);
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
        private void CloseWidget<WidgetType, ModelType>(ref WidgetType widget, ref ModelType model)
            where WidgetType : WidgetBase, new()
            where ModelType : WidgetModelBase, new()
        {
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
        private ModelType CloseWidget<WidgetType, ModelType>(ref WidgetType widget)
            where WidgetType : WidgetBase, new()
            where ModelType : WidgetModelBase, new()
        {
            WidgetMenager.DownloadWidgetConfigurationFromFile(out ModelType model);
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
        private void CloseAndSaveWidget<WidgetType, ModelType>(ref WidgetType widget)
            where WidgetType : WidgetBase, new()
            where ModelType : WidgetModelBase, new()
        {
            WidgetMenager.SaveWidgetConfigurationInFile(CloseWidget<WidgetType, ModelType>(ref widget));
        }

        /// <summary>
        /// Load widget (build or not).
        /// </summary>
        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="ViewModelType">Type of view model.</typeparam>
        /// <typeparam name="ModelType">Type of model.</typeparam>
        /// <param name="widget">Widget to load.</param>
        private void LoadWidget<WidgetType, ViewModelType, ModelType>(ref WidgetType widget)
            where WidgetType : WidgetBase, new()
            where ViewModelType : class, IWidgetViewModel<ModelType>, new()
            where ModelType : WidgetModelBase, new()
        {
            var downloadedConfigurationResult = WidgetMenager.DownloadWidgetConfigurationFromFile(out ModelType model);

            if (!downloadedConfigurationResult || model.IsActive)
            {
                BuildWidget<WidgetType, ViewModelType, ModelType>(ref widget, ref model, downloadedConfigurationResult);
            }

            WidgetMenager.SaveWidgetConfigurationInFile(model);
        }

        /// <summary>
        /// Load all widgets.
        /// </summary>
        private void LoadWidgets()
        {
            LoadWidget<ClockWidget, ClockViewModel, ClockModel>(ref ClockWidget);
            LoadWidget<PictureWidget, PictureViewModel, PictureModel>(ref PictureWidget);
            LoadWidget<NoteWidget, NoteViewModel, NoteModel>(ref NoteWidget);
        }

        /// <summary>
        /// Save configuration for all widgets.
        /// </summary>
        private void SaveWidgets()
        {
            WidgetMenager.SaveWidgetConfigurationInFile<ClockWidget, ClockModel>(ClockWidget);
            WidgetMenager.SaveWidgetConfigurationInFile<PictureWidget, PictureModel>(PictureWidget);
            WidgetMenager.SaveWidgetConfigurationInFile<PictureWidget, PictureModel>(PictureWidget);
        }

        /// <summary>
        /// Close all widgets.
        /// </summary>
        private void CloseWidgets()
        {
            CloseWidget<ClockWidget, ClockModel>(ref ClockWidget);
            CloseWidget<PictureWidget, PictureModel>(ref PictureWidget);
            CloseWidget<PictureWidget, PictureModel>(ref PictureWidget);
        }

        /// <summary>
        /// Close all widgets.
        /// </summary>
        private void CloseAndSaveWidgets()
        {
            CloseAndSaveWidget<ClockWidget, ClockModel>(ref ClockWidget);
            CloseAndSaveWidget<PictureWidget, PictureModel>(ref PictureWidget);
            CloseAndSaveWidget<NoteWidget, NoteModel>(ref NoteWidget);
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
