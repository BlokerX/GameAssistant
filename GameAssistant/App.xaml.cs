using GameAssistant.Models;
using GameAssistant.Services;
using GameAssistant.Widgets;
using GameAssistant.WidgetViewModels;
using GameAssistant.Windows;
using System.IO;
using System.Windows;

namespace GameAssistant
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region WidgetsContainers

        #region ClockWidgetContainer
        /// <summary>
        /// The clock widget's container.
        /// </summary>
        private WidgetContainer<ClockWidget> clockWidgetContainer = new WidgetContainer<ClockWidget>();
        #endregion

        #region PictureWidgetContainer
        /// <summary>
        /// The picture widget's container.
        /// </summary>
        private WidgetContainer<PictureWidget> pictureWidgetContainer = new WidgetContainer<PictureWidget>();
        #endregion

        #region NoteWidgetContainer
        /// <summary>
        /// The note widget's container.
        /// </summary>
        private WidgetContainer<NoteWidget> noteWidgetContainer = new WidgetContainer<NoteWidget>();
        #endregion

        #region CalculatorWidgetContainer
        /// <summary>
        /// The calculator widget's container.
        /// </summary>
        private WidgetContainer<CalculatorWidget> calculatorWidgetContainer = new WidgetContainer<CalculatorWidget>();
        #endregion

        #endregion

        #region NotifyIcon
        /// <summary>
        /// The notify icon.
        /// </summary>
        private System.Windows.Forms.NotifyIcon NotifyIcon;

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
            WidgetManager.Widget_ChangeStateAndSave<ClockWidget, ClockViewModel, ClockModel>(ref clockWidgetContainer.Widget);
        }

        /// <summary>
        /// Invoke when picture widget button clicked.
        /// </summary>
        private void NotifyIcon_PictureWidget_Settings_Click(object sender, System.EventArgs e)
        {
            WidgetManager.Widget_ChangeStateAndSave<PictureWidget, PictureViewModel, PictureModel>(ref pictureWidgetContainer.Widget);
        }

        /// <summary>
        /// Invoke when note widget button clicked.
        /// </summary>
        private void NotifyIcon_NoteWidget_Settings_Click(object sender, System.EventArgs e)
        {
            WidgetManager.Widget_ChangeStateAndSave<NoteWidget, NoteViewModel, NoteModel>(ref noteWidgetContainer.Widget);
        }

        /// <summary>
        /// Invoke when calculator widget button clicked.
        /// </summary>
        private void NotifyIcon_CalculatorWidget_Settings_Click(object sender, System.EventArgs e)
        {
            WidgetManager.Widget_ChangeStateAndSave<CalculatorWidget, CalculatorViewModel, CalculatorModel>(ref calculatorWidgetContainer.Widget);
        }

        /// <summary>
        /// Invoke when close app button clicked.
        /// </summary>
        private void NotifyIcon_MenuItem_CloseApp_Click(object sender, System.EventArgs e)
        {
            CloseApp();
        }

        #endregion

        #region Settings window

        /// <summary>
        /// Setting window slot.
        /// </summary>
        private SettingsWindow settingsWindow;

        /// <summary>
        /// Open setting window.
        /// </summary>
        private void OpenSettingsWindow()
        {
            if (settingsWindow == null)
            {
                // todo . DODAĆ CALCULATOR widget
                settingsWindow = new SettingsWindow(
                    ref clockWidgetContainer,
                    ref pictureWidgetContainer,
                    ref noteWidgetContainer,
                    ref calculatorWidgetContainer);
                settingsWindow.Closed += (s, o) => settingsWindow = null;
                settingsWindow.AppClose += CloseApp;
                settingsWindow.Show();
            }
            settingsWindow?.Focus();
            //todo zorobić synchronizację między wyborem widgetów
        }

        #endregion

        #region Ovverides methods

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AppFileSystem.CreateStartupKey();

            SelectDisks();
            AppFileSystem.RegisterFileSystem
            (
                nameof(ClockWidget),
                nameof(PictureWidget),
                nameof(NoteWidget),
                nameof(CalculatorWidget)
            );

            // Notify icon register:
            NotifyIcon = new System.Windows.Forms.NotifyIcon()
            {
                Visible = true,
                Icon = GameAssistant.Properties.Resources.NotifyIcon,
                ContextMenu = new System.Windows.Forms.ContextMenu(
                    new System.Windows.Forms.MenuItem[]
                    {
                        new System.Windows.Forms.MenuItem("Clock widget", NotifyIcon_ClockWidget_Settings_Click),
                        new System.Windows.Forms.MenuItem("Picture widget", NotifyIcon_PictureWidget_Settings_Click),
                        new System.Windows.Forms.MenuItem("Note widget", NotifyIcon_NoteWidget_Settings_Click),
                        new System.Windows.Forms.MenuItem("Calculator widget", NotifyIcon_CalculatorWidget_Settings_Click),
                        new System.Windows.Forms.MenuItem("-"),
                        new System.Windows.Forms.MenuItem("Settings", NotifyIcon_MenuItem_Settings_Click),
                        new System.Windows.Forms.MenuItem("-"),
                        new System.Windows.Forms.MenuItem("Close app", NotifyIcon_MenuItem_CloseApp_Click)
                    }
                )
            };

            LoadWidgets();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            CloseAndSaveWidgets();
            NotifyIcon.Dispose();
            base.OnExit(e);
        }

        #endregion

        #region AppMethods

        private void CloseApp()
        {
            CloseWidgets();
            App.Current.Shutdown();
        }

        #endregion

        #region Widgets methods

        /// <summary>
        /// Load all widgets.
        /// </summary>
        private void LoadWidgets()
        {
            WidgetManager.LoadWidget<ClockWidget, ClockViewModel, ClockModel>(ref clockWidgetContainer.Widget);
            WidgetManager.LoadWidget<PictureWidget, PictureViewModel, PictureModel>(ref pictureWidgetContainer.Widget);
            WidgetManager.LoadWidget<NoteWidget, NoteViewModel, NoteModel>(ref noteWidgetContainer.Widget);
            WidgetManager.LoadWidget<CalculatorWidget, CalculatorViewModel, CalculatorModel>(ref calculatorWidgetContainer.Widget);
        }

        /// <summary>
        /// Save configuration for all widgets.
        /// </summary>
        private void SaveWidgets()
        {
            WidgetManager.SaveWidgetConfigurationInFile<ClockWidget, ClockModel>(clockWidgetContainer.Widget);
            WidgetManager.SaveWidgetConfigurationInFile<PictureWidget, PictureModel>(pictureWidgetContainer.Widget);
            WidgetManager.SaveWidgetConfigurationInFile<NoteWidget, NoteModel>(noteWidgetContainer.Widget);
            WidgetManager.SaveWidgetConfigurationInFile<CalculatorWidget, CalculatorModel>(calculatorWidgetContainer.Widget);
        }

        /// <summary>
        /// Close all widgets.
        /// </summary>
        private void CloseWidgets()
        {
            WidgetManager.CloseWidget<ClockWidget, ClockModel>(ref clockWidgetContainer.Widget);
            WidgetManager.CloseWidget<PictureWidget, PictureModel>(ref pictureWidgetContainer.Widget);
            WidgetManager.CloseWidget<NoteWidget, NoteModel>(ref noteWidgetContainer.Widget);
            WidgetManager.CloseWidget<CalculatorWidget, CalculatorModel>(ref calculatorWidgetContainer.Widget);
        }

        /// <summary>
        /// Close all widgets.
        /// </summary>
        private void CloseAndSaveWidgets()
        {
            WidgetManager.CloseAndSaveWidget<ClockWidget, ClockModel>(ref clockWidgetContainer.Widget);
            WidgetManager.CloseAndSaveWidget<PictureWidget, PictureModel>(ref pictureWidgetContainer.Widget);
            WidgetManager.CloseAndSaveWidget<NoteWidget, NoteModel>(ref noteWidgetContainer.Widget);
            WidgetManager.CloseAndSaveWidget<CalculatorWidget, CalculatorModel>(ref calculatorWidgetContainer.Widget);
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
