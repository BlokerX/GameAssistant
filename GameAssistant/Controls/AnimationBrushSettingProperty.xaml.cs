using System;
using System.Windows.Media;

namespace GameAssistant.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy AnimationBrushSettingProperty.xaml
    /// </summary>
    public partial class AnimationBrushSettingProperty : BindableControl, ISettingProperty
    {
        public AnimationBrushSettingProperty()
        {
            InitializeComponent();
            PropertyName = "Property";
            ChangeColorPropertyEnabled();
            AnimationProperty.PropertyValueChanged += (s, e) => ChangeColorPropertyEnabled();
        }

        private void ChangeColorPropertyEnabled()
        {
            if (AnimationProperty.SelectedElementIndex != 0)
                ColorProperty.IsEnabled = false;
            else
                ColorProperty.IsEnabled = true;
        }

        private string _propertyName;

        public string PropertyName
        {
            get => _propertyName;
            set
            {
                SetProperty(ref _propertyName, value);
                ColorProperty.PropertyName = value + " " + "color:";
                AnimationProperty.PropertyName = value + " " + "animation:";
            }
        }

        public Brush ForegroundColor
        {
            set
            {
                ColorProperty.ForegroundColor = value;
                AnimationProperty.ForegroundColor = value;
            }
        }

        public Brush BorderColor
        {
            get => ColorProperty.BorderColor;
            set
            {
                ColorProperty.BorderColor = value;
                AnimationProperty.BorderColor = value;
            }
        }

        public Brush BackgorundColor
        {
            set
            {
                ColorProperty.BackgorundColor = value;
                AnimationProperty.BackgorundColor = value;
            }
        }

        /// <summary>
        /// Property value for color.
        /// </summary>
        public Brush PropertyColor
        {
            get => ColorProperty.PropertyColor;
            set => ColorProperty.PropertyColor = value;
        }

        /// <summary>
        /// On ColorProperty changed.
        /// </summary>
        public event EventHandler<Brush> PropertyColorChanged
        {
            add => ColorProperty.PropertyColorChanged += value;
            remove => ColorProperty.PropertyColorChanged -= value;
        }

        /// <summary>
        /// Property value animation mode.
        /// </summary>
        public int PropertyAnimation
        {
            get => AnimationProperty.SelectedElementIndex;
            set => AnimationProperty.SelectedElementIndex = value;
        }

        /// <summary>
        /// On AnimationProperty changed.
        /// </summary>
        public event EventHandler<int> PropertyAnimationChanged
        {
            add => AnimationProperty.PropertyValueChanged += value;
            remove => AnimationProperty.PropertyValueChanged -= value;
        }

    }
}
