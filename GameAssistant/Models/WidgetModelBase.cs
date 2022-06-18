using GameAssistant.Core;
using GameAssistant.Services;
using Newtonsoft.Json;
using System;
using System.Windows;
using System.Windows.Media;

namespace GameAssistant.Models
{
    /// <summary>
    /// Base of widget model that contains bindings for Widget.
    /// </summary>
    public class WidgetModelBase : BindableObject
    {
        ///// <summary>
        ///// Model properties list.
        ///// </summary>
        //protected virtual List<object> GetProperties()
        //{
        //    return new List<object>()
        //    {
        //        //_title,

        //        _width,
        //        _height,

        //        _screenPositionX,
        //        _screenPositionY,

        //        _isActive,

        //        _isDragActive,
        //        _resizeMode,

        //        _backgroundAnimatedBrush,
        //        _backgroundOpacity
        //    };
        //}

        public WidgetModelBase()
        {
            AnimationMemberDepose += BackgroundAnimatedBrush.BrushAnimationManager.AnimationMemberDepose;
        }

        protected event Action AnimationMemberDepose;

        public void AnimationMemberDepose_Invoke()
        {
            AnimationMemberDepose.Invoke();
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
            }
        }

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
            set
            {
                SetProperty(ref _isDragActive, value);
                DragActiveChanged?.Invoke(_isDragActive);
            }
        }

        /// <summary>
        /// On drag active changed event.
        /// </summary>
        public event Predicate<bool?> DragActiveChanged;

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

        private AnimatedBrush _backgroundAnimatedBrush = new AnimatedBrush(new SolidColorBrush(Color.FromRgb(249, 255, 129)));
        /// <summary>
        /// Widget's background animated brush.
        /// </summary>
        public AnimatedBrush BackgroundAnimatedBrush
        {
            get => _backgroundAnimatedBrush;
            set => SetProperty(ref _backgroundAnimatedBrush, value);
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

        ~WidgetModelBase()
        {
            AnimationMemberDepose_Invoke();
        }

    }
}
