using GameAssistant.Core;
using System.Windows.Media;

namespace GameAssistant.Services
{
    /// <summary>
    /// Object contains Brush and AnimationManager.
    /// </summary>
    internal class AnimatedBrush : BindableObject
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="brush">Brush to set.</param>
        public AnimatedBrush(Brush brush)
        {
            _brushContainer = new VariableContainer<Brush>(brush);
            _brushAnimationManager = new AnimationManager(ref _brushContainer);
        }

        #region Properties

        private VariableContainer<Brush> _brushContainer;
        /// <summary>
        /// Container with brush (Container).
        /// </summary>
        public VariableContainer<Brush> BrushBackgroundContainer
        {
            get => _brushContainer;
            set => SetProperty(ref _brushContainer, value);
        }

        private AnimationManager _brushAnimationManager;
        /// <summary>
        /// Brush color animation.
        /// </summary>
        public AnimationManager BrushAnimationManager
        {
            get => _brushAnimationManager;
            set => SetProperty(ref _brushAnimationManager, value);
        }

        #endregion
    }
}
