﻿using GameAssistant.Services;
using System.Windows.Media;

namespace GameAssistant.Models
{
    /// <summary>
    /// Calculator properties set that is able to serialize.
    /// </summary>
    internal class CalculatorModel : WidgetModelBase
    {
        // Constructors:
        public CalculatorModel()
        {
            AnimationMemberDepose += ButtonsBackgroundAnimatedBrush.BrushAnimationManager.AnimationMemberDepose;
            AnimationMemberDepose += ButtonsForegroundAnimatedBrush.BrushAnimationManager.AnimationMemberDepose;
            AnimationMemberDepose += TextBoxBackgroundAnimatedBrush.BrushAnimationManager.AnimationMemberDepose;
            AnimationMemberDepose += TextBoxForegroundAnimatedBrush.BrushAnimationManager.AnimationMemberDepose;
        }

        #region Serialize properties

        // Buttons foreground:

        private string _buttonsFontFamily = "Century Gothic";
        /// <summary>
        /// Buttons font family.
        /// </summary>
        public string ButtonsFontFamily
        {
            get => _buttonsFontFamily;
            set => SetProperty(ref _buttonsFontFamily, value);
        }

        private double _buttonsFontSize = 20;
        /// <summary>
        /// Buttons font size.
        /// </summary>
        public double ButtonsFontSize
        {
            get => _buttonsFontSize;
            set => SetProperty(ref _buttonsFontSize, value);
        }

        private AnimatedBrush _buttonsForegroundAnimatedBrush = new AnimatedBrush(new SolidColorBrush(Colors.Black));
        /// <summary>
        /// Buttons foreground animated brushContainer.
        /// </summary>
        public AnimatedBrush ButtonsForegroundAnimatedBrush
        {
            get => _buttonsForegroundAnimatedBrush;
            set => SetProperty(ref _buttonsForegroundAnimatedBrush, value);
        }

        // Buttons background:
        private AnimatedBrush _buttonsBackgroundAnimatedBrush = new AnimatedBrush(new SolidColorBrush(Colors.DarkGray));
        /// <summary>
        /// Buttons background animated brushContainer.
        /// </summary>
        public AnimatedBrush ButtonsBackgroundAnimatedBrush
        {
            get => _buttonsBackgroundAnimatedBrush;
            set => SetProperty(ref _buttonsBackgroundAnimatedBrush, value);
        }

        private double _buttonsOpacity = 0.75;
        /// <summary>
        /// Buttons opacity.
        /// </summary>
        public double ButtonsOpacity
        {
            get => _buttonsOpacity;
            set => SetProperty(ref _buttonsOpacity, value);
        }

        // TextBox foreground:

        private string _textBoxFontFamily = "Century Gothic";
        /// <summary>
        /// TextBox font family.
        /// </summary>
        public string TextBoxFontFamily
        {
            get => _textBoxFontFamily;
            set => SetProperty(ref _textBoxFontFamily, value);
        }

        private double _textBoxFontSize = 28;
        /// <summary>
        /// TextBox font size.
        /// </summary>
        public double TextBoxFontSize
        {
            get => _textBoxFontSize;
            set => SetProperty(ref _textBoxFontSize, value);
        }

        private AnimatedBrush _textBoxForegroundAnimatedBrush = new AnimatedBrush(new SolidColorBrush(Colors.Black));
        /// <summary>
        /// TextBox foreground animated brushContainer.
        /// </summary>
        public AnimatedBrush TextBoxForegroundAnimatedBrush
        {
            get => _textBoxForegroundAnimatedBrush;
            set => SetProperty(ref _textBoxForegroundAnimatedBrush, value);
        }

        // TextBox background:

        private AnimatedBrush _textBoxBackgroundAnimatedBrush = new AnimatedBrush(new SolidColorBrush(Colors.White));
        /// <summary>
        /// TextBox background animated brushContainer.
        /// </summary>
        public AnimatedBrush TextBoxBackgroundAnimatedBrush
        {
            get => _textBoxBackgroundAnimatedBrush;
            set => SetProperty(ref _textBoxBackgroundAnimatedBrush, value);
        }

        private double _textBoxOpacity = 0.75;
        /// <summary>
        /// TextBox opacity.
        /// </summary>
        public double TextBoxOpacity
        {
            get => _textBoxOpacity;
            set => SetProperty(ref _textBoxOpacity, value);
        }

        #endregion
    }
}
