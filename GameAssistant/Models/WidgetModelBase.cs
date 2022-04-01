using GameAssistant.Core;
using GameAssistant.Services;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Media;
using System;

namespace GameAssistant.Models
{
    /// <summary>
    /// Base of widget model that contains bindings for Widget.
    /// </summary>
    public class WidgetModelBase : BindableObject
    {
        // Constructors:
        public WidgetModelBase()
        {
            AnimationToken_True += () => _backgroundAnimationManager?.StartAnimate();
            AnimationToken_False += () => _backgroundAnimationManager?.StopAnimate();
            _backgroundAnimationManager = new AnimationManager(ref _backgroundColor);
        }

        private bool _animationToken = false;
        [JsonIgnore]
        /// <summary>
        /// Specjal seciurity protocol for animations threads.
        /// </summary>
        public bool AnimationToken
        {
            get => _animationToken;
            set
            {
                SetProperty(ref _animationToken, value);
                if (value == true)
                    AnimationToken_True();
                else if (value == false)
                    AnimationToken_False();

            }
        }

        [JsonIgnore]
        protected Action AnimationToken_True;

        [JsonIgnore]
        protected Action AnimationToken_False;

        // Window const elements:
        private string _title = "Widget";
        [JsonIgnore]
        /// <summary>
        /// The title of widget window.
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private bool _isActive = true;
        /// <summary>
        /// If true widget has saved as enable, if false widget has saved as disabled.
        /// </summary>
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }


        private double _width;
        /// <summary>
        /// The width of widget window.
        /// </summary>
        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        private double _height;
        /// <summary>
        /// The height of the widget window.
        /// </summary>
        public double Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        private ResizeMode _resizeMode = ResizeMode.CanResizeWithGrip;
        /// <summary>
        /// Resize mode of window.
        /// </summary>
        public ResizeMode ResizeMode
        {
            get => _resizeMode;
            set => SetProperty(ref _resizeMode, value);
        }

        private bool? _isDragActive = false;
        /// <summary>
        /// If true drag move is enable, if false drag move is disable.
        /// </summary>
        public bool? IsDragActive
        {
            get => _isDragActive;
            set => SetProperty(ref _isDragActive, value);
        }

        // Variables:
        private double _screenPositionX = 70;
        /// <summary>
        /// Widget's horizontal position.
        /// </summary>
        public double ScreenPositionX
        {
            get => _screenPositionX;
            set => SetProperty(ref _screenPositionX, value);
        }

        private double _screenPositionY = 50;
        /// <summary>
        /// Widget's vertical position.
        /// </summary>
        public double ScreenPositionY
        {
            get => _screenPositionY;
            set => SetProperty(ref _screenPositionY, value);
        }

        // Visual elements:
        private VariableContainer<Brush> _backgroundColor = new VariableContainer<Brush>(new SolidColorBrush(Color.FromRgb(249, 255, 129)));
        /// <summary>
        /// Container with widget's background brush (Container).
        /// </summary>
        public VariableContainer<Brush> BackgroundColorContainer
        {
            get => _backgroundColor;
            set
            {
                SetProperty(ref _backgroundColor, value);
            }
        }
        /// <summary>
        /// Widget's background brush (color).
        /// </summary>
        public Brush BackgroundColor
        {
            get => _backgroundColor.Variable;
            set
            {
                _backgroundColor.Variable = value;
            }
        }

        private double _backgroundOpacity = 0.5;
        /// <summary>
        /// Widget background's opacity.
        /// </summary>
        public double BackgroundOpacity
        {
            get => _backgroundOpacity;
            set => SetProperty(ref _backgroundOpacity, value);
        }

        // TODO Add color's animations!!!
        private AnimationManager _backgroundAnimationManager;
        /// <summary>
        /// Background color animation.
        /// </summary>
        public AnimationManager BackgroundAnimationManager
        {
            get { return _backgroundAnimationManager; }
            set { _backgroundAnimationManager = value; }
        }


        ~WidgetModelBase()
        {
            AnimationToken = false;
        }

    }
}
