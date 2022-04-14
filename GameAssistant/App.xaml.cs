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
        /// NotifyIcon menu items.
        /// </summary>
        private enum NotifyIconMenuItem
        {
            ClockWidget,
            PictureWidget,
            NoteWidget,
            CalculatorWidget,
            // - //
            SettingsWindow = 6,
            // - //
            CloseApp = 8
        }

        /// <summary>
        /// Invoke when clock widget button clicked.
        /// </summary>
        private void NotifyIcon_ClockWidget_Settings_Click(object sender, System.EventArgs e)
        {
            ClockWidget.Events.WidgetActiveChanged_Invoke(!NotifyIcon.ContextMenu.MenuItems[(int)NotifyIconMenuItem.ClockWidget].Checked);
        }

        /// <summary>
        /// Invoke when picture widget button clicked.
        /// </summary>
        private void NotifyIcon_PictureWidget_Settings_Click(object sender, System.EventArgs e)
        {
            PictureWidget.Events.WidgetActiveChanged_Invoke(!NotifyIcon.ContextMenu.MenuItems[(int)NotifyIconMenuItem.PictureWidget].Checked);
        }

        /// <summary>
        /// Invoke when note widget button clicked.
        /// </summary>
        private void NotifyIcon_NoteWidget_Settings_Click(object sender, System.EventArgs e)
        {
            NoteWidget.Events.WidgetActiveChanged_Invoke(!NotifyIcon.ContextMenu.MenuItems[(int)NotifyIconMenuItem.NoteWidget].Checked);
        }

        /// <summary>
        /// Invoke when calculator widget button clicked.
        /// </summary>
        private void NotifyIcon_CalculatorWidget_Settings_Click(object sender, System.EventArgs e)
        {
            CalculatorWidget.Events.WidgetActiveChanged_Invoke(!NotifyIcon.ContextMenu.MenuItems[(int)NotifyIconMenuItem.CalculatorWidget].Checked);
        }

        /// <summary>
        /// Invoke when settings button clicked.
        /// </summary>
        private void NotifyIcon_MenuItem_Settings_Click(object sender, System.EventArgs e)
        {
            OpenSettingsWindow();
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
        }

        #endregion

        #region Ovverides methods

        /// <summary>
        /// On application startup.
        /// </summary>
        /// <param name="e">Startup args.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SelectDisks();
            AppFileSystem.RegisterFileSystem
            (
                nameof(ClockWidget),
                nameof(PictureWidget),
                nameof(NoteWidget),
                nameof(CalculatorWidget)
            );

            // Register widget events
            RegisterWidgetEvents();

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

        private void RegisterWidgetEvents()
        {
            RegisterWidgetEventsMethods();
            RegisterWidgetEventsForNotifyIcon();
        }

        private void RegisterWidgetEventsMethods()
        {
            ClockWidget.Events.WidgetActiveChanged += (b) =>
            WidgetManager.ChangeWidgetState<ClockWidget, ClockViewModel, ClockModel>(ref clockWidgetContainer.Widget, b);
            PictureWidget.Events.WidgetActiveChanged += (b) =>
            WidgetManager.ChangeWidgetState<PictureWidget, PictureViewModel, PictureModel>(ref pictureWidgetContainer.Widget, b);
            NoteWidget.Events.WidgetActiveChanged += (b) =>
            WidgetManager.ChangeWidgetState<NoteWidget, NoteViewModel, NoteModel>(ref noteWidgetContainer.Widget, b);
            CalculatorWidget.Events.WidgetActiveChanged += (b) =>
            WidgetManager.ChangeWidgetState<CalculatorWidget, CalculatorViewModel, CalculatorModel>(ref calculatorWidgetContainer.Widget, b);

        }

        private void RegisterWidgetEventsForNotifyIcon()
        {
            ClockWidget.Events.WidgetActiveChanged += (b) => NotifyIcon.ContextMenu.MenuItems[(int)NotifyIconMenuItem.ClockWidget].Checked = b;
            PictureWidget.Events.WidgetActiveChanged += (b) => NotifyIcon.ContextMenu.MenuItems[(int)NotifyIconMenuItem.PictureWidget].Checked = b;
            NoteWidget.Events.WidgetActiveChanged += (b) => NotifyIcon.ContextMenu.MenuItems[(int)NotifyIconMenuItem.NoteWidget].Checked = b;
            CalculatorWidget.Events.WidgetActiveChanged += (b) => NotifyIcon.ContextMenu.MenuItems[(int)NotifyIconMenuItem.CalculatorWidget].Checked = b;
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
            if (clockWidgetContainer.Widget != null)
                NotifyIcon.ContextMenu.MenuItems[0].Checked = true;
            WidgetManager.LoadWidget<PictureWidget, PictureViewModel, PictureModel>(ref pictureWidgetContainer.Widget);
            if (pictureWidgetContainer.Widget != null)
                NotifyIcon.ContextMenu.MenuItems[1].Checked = true;
            WidgetManager.LoadWidget<NoteWidget, NoteViewModel, NoteModel>(ref noteWidgetContainer.Widget);
            if (noteWidgetContainer.Widget != null)
                NotifyIcon.ContextMenu.MenuItems[2].Checked = true;
            WidgetManager.LoadWidget<CalculatorWidget, CalculatorViewModel, CalculatorModel>(ref calculatorWidgetContainer.Widget);
            if (calculatorWidgetContainer.Widget != null)
                NotifyIcon.ContextMenu.MenuItems[3].Checked = true;
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
