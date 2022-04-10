using GameAssistant.Core;

namespace GameAssistant.ControlViewModels
{
    internal class DoubleSettingPropertyViewModel : BindableObject
    {
        private double _value;
        /// <summary>
        /// Value as double type.
        /// </summary>
        public double Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        private double _maximum = 1;
        /// <summary>
        /// Maximum double.
        /// </summary>
        public double Maximum
        {
            get => _maximum;
            set => SetProperty(ref _maximum, value);
        }

        private double _minimum = 0;
        /// <summary>
        /// Minimum double.
        /// </summary>
        public double Minimum
        {
            get => _minimum;
            set => SetProperty(ref _minimum, value);
        }

    }
}
