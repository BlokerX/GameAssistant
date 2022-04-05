namespace GameAssistant.Core
{
    /// <summary>
    /// Container for variables.
    /// </summary>
    /// <typeparam name="T">Variable type.</typeparam>
    public class VariableContainer<T> : BindableObject
    {
        public T _variableSource;
        /// <summary>
        /// Variable access point.
        /// </summary>
        public T Variable
        {
            get => _variableSource;
            set => SetProperty(ref _variableSource,  value);
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="value">Value.</param>
        public VariableContainer(T value)
        {
            _variableSource = value;
        }

        
    }
}
