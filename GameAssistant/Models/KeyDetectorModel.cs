﻿using GameAssistant.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace GameAssistant.Models
{
    /// <summary>
    /// KeyDetector properties that able to serialize.
    /// </summary>
    internal class KeyDetectorModel : WidgetModelBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public KeyDetectorModel()
        {
            AnimationToken_True += () => DetectPanelAnimatedBrush.BrushAnimationManager.StartAnimate();
            AnimationToken_False += () => DetectPanelAnimatedBrush.BrushAnimationManager.StopAnimate();

            AnimationToken_True += () => ForegroundAnimatedBrush.BrushAnimationManager.StartAnimate();
            AnimationToken_False += () => ForegroundAnimatedBrush.BrushAnimationManager.StopAnimate();
        }


        #region Serialize properties

        private string _fontFamily = "Century Gothic";
        /// <summary>
        /// Detect panel's font family.
        /// </summary>
        public string FontFamily
        {
            get => _fontFamily;
            set => SetProperty(ref _fontFamily, value);
        }

        private double _fontSize = 32;
        /// <summary>
        /// Detect panel's font size.
        /// </summary>
        public double FontSize
        {
            get => _fontSize;
            set => SetProperty(ref _fontSize, value);
        }

        //todo dodać enumy
        private double[] _detectPanelOpacity = new double[6] { 0.75, 0.75, 0.75, 0.75, 0.75, 0.75 };
        /// <summary>
        /// Detect panel's opacity.
        /// </summary>
        public double DetectPanelOpacity
        {
            get => _detectPanelOpacity[0];
            set => SetProperty(ref _detectPanelOpacity[0], value);
        }
        public double DetectPanelOpacity1
        {
            get => _detectPanelOpacity[1];
            set => SetProperty(ref _detectPanelOpacity[1], value);
        }
        public double DetectPanelOpacity2
        {
            get => _detectPanelOpacity[2];
            set => SetProperty(ref _detectPanelOpacity[2], value);
        }
        public double DetectPanelOpacity3
        {
            get => _detectPanelOpacity[3];
            set => SetProperty(ref _detectPanelOpacity[3], value);
        }
        public double DetectPanelOpacity4
        {
            get => _detectPanelOpacity[4];
            set => SetProperty(ref _detectPanelOpacity[4], value);
        }
        public double DetectPanelOpacity5
        {
            get => _detectPanelOpacity[5];
            set => SetProperty(ref _detectPanelOpacity[5], value);
        }

        private AnimatedBrush _detectPanelAnimatedBrush = new AnimatedBrush(new SolidColorBrush(Colors.Cyan));
        /// <summary>
        /// Detect panel's animated brush.
        /// </summary>
        public AnimatedBrush DetectPanelAnimatedBrush
        {
            get => _detectPanelAnimatedBrush;
            set => SetProperty(ref _detectPanelAnimatedBrush, value);
        }

        private AnimatedBrush _foregroundAnimatedBrush = new AnimatedBrush(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)));
        /// <summary>
        /// Detect panel font's animated brush.
        /// </summary>
        public AnimatedBrush ForegroundAnimatedBrush
        {
            get => _foregroundAnimatedBrush;
            set => SetProperty(ref _foregroundAnimatedBrush, value);
        }

        #endregion
    }
}