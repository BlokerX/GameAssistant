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
            AnimationToken_True += () => ForegroundAnimatedBrush.BrushAnimationManager.StartAnimate();
            AnimationToken_False += () => ForegroundAnimatedBrush.BrushAnimationManager.StopAnimate();
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

        private AnimatedBrush _foregroundAnimatedBrush = new AnimatedBrush(new SolidColorBrush(Colors.Black));
        /// <summary>
        /// Note font animated brush.
        /// </summary>
        public AnimatedBrush ForegroundAnimatedBrush
        {
            get => _foregroundAnimatedBrush;
            set => SetProperty(ref _foregroundAnimatedBrush, value);
        }

        #endregion
    }
}
