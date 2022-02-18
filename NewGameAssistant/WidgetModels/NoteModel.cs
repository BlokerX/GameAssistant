using NewGameAssistant.Core;
using NewGameAssistant.Models;
using NewGameAssistant.Services;
using NewGameAssistant.Widgets;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;

namespace NewGameAssistant.WidgetModels
{
    /// <summary>
    /// View model that contains bindings for NoteWidget.
    /// </summary>
    internal class NoteModel : WidgetModelBase
    {
        /// <summary>
        /// Dire of notes.
        /// </summary>
        private static string notesDirePath = Path.Combine(AppFileSystem.GetSaveDireConfigurationPath(nameof(NoteWidget)), "Notes");

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NoteModel()
        {
            // Set title:
            Title = "Note widget";

            // Set widget size:
            Width = 360;
            Height = 280;

            // Set widget position:
            ScreenPositionY += 330;

            // Set widget background color:
            BackgroundColor = new SolidColorBrush(Colors.OrangeRed);

            // Set selected note:
            SelectedNote = new Note()
            {
                Text = "Write note here...",
                SaveFilePath = Path.Combine(notesDirePath, "MyNote1")
            };

            // Check save notes:
            LoadNotesList();

            BackButtonCommand = new RelayCommand((o) =>
            {
                if (notes.Count > 0)
                {
                    if (SelectedNoteIndex > 0)
                    {
                        SelectedNoteIndex--;
                    }
                    else SelectedNoteIndex = notes.Count - 1;
                }
            });

            NextButtonCommand = new RelayCommand((o) =>
            {
                if (notes.Count > 0)
                {
                    if (SelectedNoteIndex + 1 < notes.Count)
                    {
                        SelectedNoteIndex++;
                    }
                    else SelectedNoteIndex = 0;
                }
            });

        }

        private ICommand _backButtonCommand = new RelayCommand((o) => { });
        [JsonIgnore]
        public ICommand BackButtonCommand
        {
            get => _backButtonCommand;
            set => SetProperty(ref _backButtonCommand, value);
        }

        private ICommand _nextButtonCommand = new RelayCommand((o) => { });
        [JsonIgnore]
        public ICommand NextButtonCommand
        {
            get => _nextButtonCommand;
            set => SetProperty(ref _nextButtonCommand, value);
        }

        private void LoadNotesList()
        {
            Directory.CreateDirectory(notesDirePath);

            notes = new List<Note>();

            var noteFiles = Directory.GetFiles(notesDirePath);
            if (noteFiles.Length > 0)
                foreach (var noteFile in noteFiles)
                {
                    notes.Add(new Note()
                    {
                        SaveFilePath = noteFile,
                        Text = File.ReadAllText(noteFile)
                    });
                }

            if (SelectedNoteIndex < notes.Count)
                SelectedNote = notes[SelectedNoteIndex];

        }

        private int _selectedNoteIndex = 0;
        /// <summary>
        /// Index of selected note.
        /// </summary>
        public int SelectedNoteIndex
        {
            get => _selectedNoteIndex;
            set
            {
                SetProperty(ref _selectedNoteIndex, value);
                if (_selectedNoteIndex < notes.Count)
                    SelectedNote = notes[_selectedNoteIndex];
            }
        }

        private Note _selectedNote;
        [JsonIgnore]
        /// <summary>
        /// Selected note.
        /// </summary>
        public Note SelectedNote
        {
            get => _selectedNote;
            set => SetProperty(ref _selectedNote, value);
        }

        /// <summary>
        /// List of notes.
        /// </summary>
        private List<Note> notes = new List<Note>();

        private Brush _noteFontColor = new SolidColorBrush(Colors.Black);
        /// <summary>
        /// The font color of note text.
        /// </summary>
        public Brush NoteFontColor
        {
            get => _noteFontColor;
            set => SetProperty(ref _noteFontColor, value);
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
    }
}
