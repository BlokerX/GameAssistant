using GameAssistant.Core;
using GameAssistant.Models;
using System.Threading;
using System.Windows.Input;
using System.Windows.Media;

namespace GameAssistant.WidgetViewModels
{
    /// <summary>
    /// View model that contains bindings for KeyDetectorWidget.
    /// </summary>
    internal class KeyDetectorViewModel : BindableObject, IWidgetViewModel<KeyDetectorModel>
    {
        private KeyDetectorModel _widgetModel = new KeyDetectorModel();
        /// <summary>
        /// KeyDetector widget properties.
        /// </summary>
        public KeyDetectorModel WidgetModel
        {
            get => _widgetModel;
            set => SetProperty(ref _widgetModel, value);
        }

        private Thread _keyDetectorThread;
        /// <summary>
        /// Key detector thread.
        /// </summary>
        public Thread KeyDetectorThread
        {
            get => _keyDetectorThread;
            set => SetProperty(ref _keyDetectorThread, value);
        }

        private bool _keyDetector_IsRunning;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public KeyDetectorViewModel()
        {
            LoadModel();

            // Key detect method:
            KeyDetectorThread = new Thread(KeyboardKeyStateDetect);
            KeyDetectorThread.SetApartmentState(ApartmentState.STA);
            KeyDetectorThread.Start();
            _keyDetector_IsRunning = true;
        }

        public void LoadModel()
        {
            // Set title:
            WidgetModel.Title = "KeyDetector widget";

            // Set widget size:
            WidgetModel.Width = 270;
            WidgetModel.Height = 170;

            // Set widget position:
            WidgetModel.ScreenPositionX += 410;
            WidgetModel.ScreenPositionY += 335;

            // Set widget background color:
            WidgetModel.BackgroundAnimatedBrush.BrushContainer.Variable = new SolidColorBrush(Colors.Orange);
            WidgetModel.BackgroundOpacity = 0.6;
        }

        public void Closing()
        {
            KeyDetectorThread.Abort();
        }

        private void KeyboardKeyStateDetect()
        {
            while (_keyDetector_IsRunning)
            {
                //todo dodać mouse buttons to
                #region Keyboard buttons

                if ((Keyboard.GetKeyStates(Key.Z) & KeyStates.Down) > 0)
                {
                    WidgetModel.DetectPanelOpacityZ = 1;
                }
                else
                {
                    WidgetModel.DetectPanelOpacityZ = WidgetModel.DetectPanelOpacity;
                }

                if ((Keyboard.GetKeyStates(Key.X) & KeyStates.Down) > 0)
                {
                    WidgetModel.DetectPanelOpacityX = 1;
                }
                else
                {
                    WidgetModel.DetectPanelOpacityX = WidgetModel.DetectPanelOpacity;
                }

                // --- //

                if ((Keyboard.GetKeyStates(Key.W) & KeyStates.Down) > 0)
                {
                    WidgetModel.DetectPanelOpacityW = 1;
                }
                else
                {
                    WidgetModel.DetectPanelOpacityW = WidgetModel.DetectPanelOpacity;
                }

                if ((Keyboard.GetKeyStates(Key.A) & KeyStates.Down) > 0)
                {
                    WidgetModel.DetectPanelOpacityA = 1;
                }
                else
                {
                    WidgetModel.DetectPanelOpacityA = WidgetModel.DetectPanelOpacity;
                }

                if ((Keyboard.GetKeyStates(Key.S) & KeyStates.Down) > 0)
                {
                    WidgetModel.DetectPanelOpacityS = 1;
                }
                else
                {
                    WidgetModel.DetectPanelOpacityS = WidgetModel.DetectPanelOpacity;
                }

                if ((Keyboard.GetKeyStates(Key.D) & KeyStates.Down) > 0)
                {
                    WidgetModel.DetectPanelOpacityD = 1;
                }
                else
                {
                    WidgetModel.DetectPanelOpacityD = WidgetModel.DetectPanelOpacity;
                }

                if ((Keyboard.GetKeyStates(Key.Space) & KeyStates.Down) > 0)
                {
                    WidgetModel.DetectPanelOpacitySpace = 1;
                }
                else
                {
                    WidgetModel.DetectPanelOpacitySpace = WidgetModel.DetectPanelOpacity;
                }

                #endregion
            }
        }
    }
}
