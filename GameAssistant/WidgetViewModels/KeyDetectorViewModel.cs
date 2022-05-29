﻿using GameAssistant.Core;
using GameAssistant.Models;
using System;
using System.Threading;
using System.Timers;
using System.Windows;
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

        private bool _keyDetector_IsRunning = true;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public KeyDetectorViewModel()
        {
            // Set title:
            WidgetModel.Title = "KeyDetector widget";

            // Set widget size:
            WidgetModel.Width = 270;
            WidgetModel.Height = 170;

            // Set widget position:
            WidgetModel.ScreenPositionX += 410;

            // Set widget background color:
            WidgetModel.BackgroundAnimatedBrush.BrushContainer.Variable = new SolidColorBrush(Colors.Orange);
            WidgetModel.BackgroundOpacity = 0.6;

            // Key detect method:
            KeyDetectorThread = new Thread(KeyboardKeyStateDetect);
            KeyDetectorThread.SetApartmentState(ApartmentState.STA);
            KeyDetectorThread.Start();
        }

        public void Closing()
        {
            KeyDetectorThread.Abort();
        }

        private void KeyboardKeyStateDetect()
        {
            while (_keyDetector_IsRunning)
            {
                if ((Keyboard.GetKeyStates(Key.W) & KeyStates.Down) > 0)
                {
                    WidgetModel.DetectPanelOpacity1 = 1;
                }
                else
                {
                    WidgetModel.DetectPanelOpacity1 = WidgetModel.DetectPanelOpacity;
                }

                if ((Keyboard.GetKeyStates(Key.A) & KeyStates.Down) > 0)
                {
                    WidgetModel.DetectPanelOpacity2 = 1;
                }
                else
                {
                    WidgetModel.DetectPanelOpacity2 = WidgetModel.DetectPanelOpacity;
                }

                if ((Keyboard.GetKeyStates(Key.S) & KeyStates.Down) > 0)
                {
                    WidgetModel.DetectPanelOpacity3 = 1;
                }
                else
                {
                    WidgetModel.DetectPanelOpacity3 = WidgetModel.DetectPanelOpacity;
                }

                if ((Keyboard.GetKeyStates(Key.D) & KeyStates.Down) > 0)
                {
                    WidgetModel.DetectPanelOpacity4 = 1;
                }
                else
                {
                    WidgetModel.DetectPanelOpacity4 = WidgetModel.DetectPanelOpacity;
                }

                if ((Keyboard.GetKeyStates(Key.Space) & KeyStates.Down) > 0)
                {
                    WidgetModel.DetectPanelOpacity5 = 1;
                }
                else
                {
                    WidgetModel.DetectPanelOpacity5 = WidgetModel.DetectPanelOpacity;
                }
            }
        }

    }
}
