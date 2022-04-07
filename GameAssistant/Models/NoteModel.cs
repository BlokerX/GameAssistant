using GameAssistant.Core;
using GameAssistant.Services;
using System.Windows;
using System.Windows.Media;

namespace GameAssistant.Models
{
    /// <summary>
    /// Note properties that able to serialize.
    /// </summary>
    internal class NoteModel : WidgetModelBase
    {
        // Constructors:
        public NoteModel()
        {
            AnimationToken_True += () => _foregroundAnimationManager?.StartAnimate();
            AnimationToken_False += () => _foregroundAnimationManager?.StopAnimate();
            _foregroundAnimationManager = new AnimationManager(ref _foregroundColor);
        }

        #region Serialize properties

        private int _selectedNoteIndex = 0;
        /// <summary>
        /// Index of selected note.
        /// </summary>
        public int SelectedNoteIndex
        {
            get => _selectedNoteIndex;
            set => SetProperty(ref _selectedNoteIndex, value);
        }

        private VariableContainer<Brush> _foregroundColor = new VariableContainer<Brush>(new SolidColorBrush((Colors.Black)));
        /// <summary>
        /// Container with note text brush (Container).
        /// </summary>
        public VariableContainer<Brush> ForegroundColorContainer
        {
            get => _foregroundColor;
            set
            {
                SetProperty(ref _foregroundColor, value);
            }
        }
        /// <summary>
        /// The font color of note text.
        /// </summary>
        public Brush ForegroundColor
        {
            get => _foregroundColor.Variable;
            set
            {
                _foregroundColor.Variable = value;
            }
        }

        private double _noteFontOpacity = 1;
        /// <summary>
        /// The font opacity of note text.
        /// </summary>
        public double NoteFontOpacity
        {
            get => _noteFontOpacity;
            set => SetProperty(ref _noteFontOpacity, value);
        }


        private string _noteFontFamily = "Century Gothic";
        /// <summary>
        /// The font family of note text.
        /// </summary>
        public string NoteFontFamily
        {
            get => _noteFontFamily;
            set => SetProperty(ref _noteFontFamily, value);
        }

        private double _noteFontSize = 20;
        /// <summary>
        /// The font size of note text.
        /// </summary>
        public double NoteFontSize
        {
            get => _noteFontSize;
            set => SetProperty(ref _noteFontSize, value);
        }

        private Visibility _settingsBarVisibility = Visibility.Visible;
        /// <summary>
        /// The visibiility of settings bar.
        /// </summary>
        public Visibility SettingsBarVisibility
        {
            get => _settingsBarVisibility;
            set => SetProperty(ref _settingsBarVisibility, value);
        }

        private AnimationManager _foregroundAnimationManager;
        /// <summary>
        /// Note font's color animation.
        /// </summary>
        public AnimationManager ForegroundAnimationManager
        {
            get { return _foregroundAnimationManager; }
            set { _foregroundAnimationManager = value; }
        }

        #endregion
    }
}
