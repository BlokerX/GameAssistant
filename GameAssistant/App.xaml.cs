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
        #region WidgetsContainer
        /// <summary>
        /// The widgets container.
        /// </summary>
        private AllWidgetsContainer widgetsContainer = new AllWidgetsContainer()
        {
            clockWidgetContainer = new WidgetContainer<ClockWidget>(),
            pictureWidgetContainer = new WidgetContainer<PictureWidget>(),
            noteWidgetContainer = new WidgetContainer<NoteWidget>(),
            calculatorWidgetContainer = new WidgetContainer<CalculatorWidget>(),
            browserWidgetContainer = new WidgetContainer<BrowserWidget>(),
            keyDetectorWidgetContainer = new WidgetContainer<KeyDetectorWidget>()
        };
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
            BrowserWidget,
            KeyDetectorWidget,
            // - //
            SettingsWindow = 7,
            // - //
            CloseApp = 9
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
        /// Invoke when browser widget button clicked.
        /// </summary>
        private void NotifyIcon_BrowserWidget_Settings_Click(object sender, System.EventArgs e)
        {
            BrowserWidget.Events.WidgetActiveChanged_Invoke(!NotifyIcon.ContextMenu.MenuItems[(int)NotifyIconMenuItem.BrowserWidget].Checked);
        }
        
        /// <summary>
        /// Invoke when key detector widget button clicked.
        /// </summary>
        private void NotifyIcon_KeyDetectorWidget_Settings_Click(object sender, System.EventArgs e)
        {
            KeyDetectorWidget.Events.WidgetActiveChanged_Invoke(!NotifyIcon.ContextMenu.MenuItems[(int)NotifyIconMenuItem.KeyDetectorWidget].Checked);
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
                settingsWindow = new SettingsWindow(ref widgetsContainer);
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
                nameof(CalculatorWidget),
                nameof(BrowserWidget),
                nameof(KeyDetectorWidget)
            );

            // Register widget events:
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
                        new System.Windows.Forms.MenuItem("Browser widget", NotifyIcon_BrowserWidget_Settings_Click),
                        new System.Windows.Forms.MenuItem("KeyDetector widget", NotifyIcon_KeyDetectorWidget_Settings_Click),
                        new System.Windows.Forms.MenuItem("-"),
                        new System.Windows.Forms.MenuItem("Settings", NotifyIcon_MenuItem_Settings_Click),
                        new System.Windows.Forms.MenuItem("-"),
                        new System.Windows.Forms.MenuItem("Close app", NotifyIcon_MenuItem_CloseApp_Click)
                    }
                )
            };

            // Notify icon double click event add:
            NotifyIcon.DoubleClick += (s, a) => OpenSettingsWindow();

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
            WidgetManager.ChangeWidgetState<ClockWidget, ClockViewModel, ClockModel>(ref widgetsContainer.clockWidgetContainer.Widget, b);
            PictureWidget.Events.WidgetActiveChanged += (b) =>
            WidgetManager.ChangeWidgetState<PictureWidget, PictureViewModel, PictureModel>(ref widgetsContainer.pictureWidgetContainer.Widget, b);
            NoteWidget.Events.WidgetActiveChanged += (b) =>
            WidgetManager.ChangeWidgetState<NoteWidget, NoteViewModel, NoteModel>(ref widgetsContainer.noteWidgetContainer.Widget, b);
            CalculatorWidget.Events.WidgetActiveChanged += (b) =>
            WidgetManager.ChangeWidgetState<CalculatorWidget, CalculatorViewModel, CalculatorModel>(ref widgetsContainer.calculatorWidgetContainer.Widget, b);
            BrowserWidget.Events.WidgetActiveChanged += (b) =>
            WidgetManager.ChangeWidgetState<BrowserWidget, BrowserViewModel, BrowserModel>(ref widgetsContainer.browserWidgetContainer.Widget, b);
            KeyDetectorWidget.Events.WidgetActiveChanged += (b) =>
            WidgetManager.ChangeWidgetState<KeyDetectorWidget, KeyDetectorViewModel, KeyDetectorModel>(ref widgetsContainer.keyDetectorWidgetContainer.Widget, b);

        }

        private void RegisterWidgetEventsForNotifyIcon()
        {
            ClockWidget.Events.WidgetActiveChanged += (b) => NotifyIcon.ContextMenu.MenuItems[(int)NotifyIconMenuItem.ClockWidget].Checked = b;
            PictureWidget.Events.WidgetActiveChanged += (b) => NotifyIcon.ContextMenu.MenuItems[(int)NotifyIconMenuItem.PictureWidget].Checked = b;
            NoteWidget.Events.WidgetActiveChanged += (b) => NotifyIcon.ContextMenu.MenuItems[(int)NotifyIconMenuItem.NoteWidget].Checked = b;
            CalculatorWidget.Events.WidgetActiveChanged += (b) => NotifyIcon.ContextMenu.MenuItems[(int)NotifyIconMenuItem.CalculatorWidget].Checked = b;
            BrowserWidget.Events.WidgetActiveChanged += (b) => NotifyIcon.ContextMenu.MenuItems[(int)NotifyIconMenuItem.BrowserWidget].Checked = b;
            KeyDetectorWidget.Events.WidgetActiveChanged += (b) => NotifyIcon.ContextMenu.MenuItems[(int)NotifyIconMenuItem.KeyDetectorWidget].Checked = b;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            widgetsContainer.keyDetectorWidgetContainer?.Widget?.Close();
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
            WidgetManager.LoadWidget<ClockWidget, ClockViewModel, ClockModel>(ref widgetsContainer.clockWidgetContainer.Widget);
            if (widgetsContainer.clockWidgetContainer.Widget != null)
                NotifyIcon.ContextMenu.MenuItems[(int)NotifyIconMenuItem.ClockWidget].Checked = true;

            WidgetManager.LoadWidget<PictureWidget, PictureViewModel, PictureModel>(ref widgetsContainer.pictureWidgetContainer.Widget);
            if (widgetsContainer.pictureWidgetContainer.Widget != null)
                NotifyIcon.ContextMenu.MenuItems[(int)NotifyIconMenuItem.PictureWidget].Checked = true;

            WidgetManager.LoadWidget<NoteWidget, NoteViewModel, NoteModel>(ref widgetsContainer.noteWidgetContainer.Widget);
            if (widgetsContainer.noteWidgetContainer.Widget != null)
                NotifyIcon.ContextMenu.MenuItems[(int)NotifyIconMenuItem.NoteWidget].Checked = true;

            WidgetManager.LoadWidget<CalculatorWidget, CalculatorViewModel, CalculatorModel>(ref widgetsContainer.calculatorWidgetContainer.Widget);
            if (widgetsContainer.calculatorWidgetContainer.Widget != null)
                NotifyIcon.ContextMenu.MenuItems[(int)NotifyIconMenuItem.CalculatorWidget].Checked = true;

            WidgetManager.LoadWidget<BrowserWidget, BrowserViewModel, BrowserModel>(ref widgetsContainer.browserWidgetContainer.Widget);
            if (widgetsContainer.browserWidgetContainer.Widget != null)
                NotifyIcon.ContextMenu.MenuItems[(int)NotifyIconMenuItem.BrowserWidget].Checked = true;
            
            WidgetManager.LoadWidget<KeyDetectorWidget, KeyDetectorViewModel, KeyDetectorModel>(ref widgetsContainer.keyDetectorWidgetContainer.Widget);
            if (widgetsContainer.keyDetectorWidgetContainer.Widget != null)
                NotifyIcon.ContextMenu.MenuItems[(int)NotifyIconMenuItem.KeyDetectorWidget].Checked = true;
        }

        /// <summary>
        /// Save configuration for all widgets.
        /// </summary>
        private void SaveWidgets()
        {
            WidgetManager.SaveWidgetConfigurationInFile<ClockWidget, ClockModel>(widgetsContainer.clockWidgetContainer.Widget);
            WidgetManager.SaveWidgetConfigurationInFile<PictureWidget, PictureModel>(widgetsContainer.pictureWidgetContainer.Widget);
            WidgetManager.SaveWidgetConfigurationInFile<NoteWidget, NoteModel>(widgetsContainer.noteWidgetContainer.Widget);
            WidgetManager.SaveWidgetConfigurationInFile<CalculatorWidget, CalculatorModel>(widgetsContainer.calculatorWidgetContainer.Widget);
            WidgetManager.SaveWidgetConfigurationInFile<BrowserWidget, BrowserModel>(widgetsContainer.browserWidgetContainer.Widget);
            WidgetManager.SaveWidgetConfigurationInFile<KeyDetectorWidget, KeyDetectorModel>(widgetsContainer.keyDetectorWidgetContainer.Widget);
        }

        /// <summary>
        /// Close all widgets.
        /// </summary>
        private void CloseWidgets()
        {
            WidgetManager.CloseWidget<ClockWidget, ClockModel>(ref widgetsContainer.clockWidgetContainer.Widget);
            WidgetManager.CloseWidget<PictureWidget, PictureModel>(ref widgetsContainer.pictureWidgetContainer.Widget);
            WidgetManager.CloseWidget<NoteWidget, NoteModel>(ref widgetsContainer.noteWidgetContainer.Widget);
            WidgetManager.CloseWidget<CalculatorWidget, CalculatorModel>(ref widgetsContainer.calculatorWidgetContainer.Widget);
            WidgetManager.CloseWidget<BrowserWidget, BrowserModel>(ref widgetsContainer.browserWidgetContainer.Widget);
            WidgetManager.CloseWidget<KeyDetectorWidget, KeyDetectorModel>(ref widgetsContainer.keyDetectorWidgetContainer.Widget);
        }

        /// <summary>
        /// Close all widgets.
        /// </summary>
        private void CloseAndSaveWidgets()
        {
            WidgetManager.CloseAndSaveWidget<ClockWidget, ClockModel>(ref widgetsContainer.clockWidgetContainer.Widget);
            WidgetManager.CloseAndSaveWidget<PictureWidget, PictureModel>(ref widgetsContainer.pictureWidgetContainer.Widget);
            WidgetManager.CloseAndSaveWidget<NoteWidget, NoteModel>(ref widgetsContainer.noteWidgetContainer.Widget);
            WidgetManager.CloseAndSaveWidget<CalculatorWidget, CalculatorModel>(ref widgetsContainer.calculatorWidgetContainer.Widget);
            WidgetManager.CloseAndSaveWidget<BrowserWidget, BrowserModel>(ref widgetsContainer.browserWidgetContainer.Widget);
            WidgetManager.CloseAndSaveWidget<KeyDetectorWidget, KeyDetectorModel>(ref widgetsContainer.keyDetectorWidgetContainer.Widget);
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
