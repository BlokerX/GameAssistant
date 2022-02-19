using NewGameAssistant.Core;
using NewGameAssistant.Models;
using NewGameAssistant.Services;
using NewGameAssistant.Widgets;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;

namespace NewGameAssistant.WidgetViewModels
{
    /// <summary>
    /// View model that contains bindings for NoteWidget.
    /// </summary>
    internal class NoteViewModel : BindableObject, IWidgetViewModel<NoteModel>
    {
        /// <summary>
        /// Dire of notes.
        /// </summary>
        private static string notesDirePath = Path.Combine(AppFileSystem.GetSaveDireConfigurationPath(nameof(NoteWidget)), "Notes");

        private NoteModel _widgetModel = new NoteModel();
        /// <summary>
        /// Note widget properties.
        /// </summary>
        public NoteModel WidgetModel
        {
            get => _widgetModel;
            set => SetProperty(ref _widgetModel, value);
        }

        /// <summary>
        /// List of notes.
        /// </summary>
        private List<NoteInformations> notes = new List<NoteInformations>();

        private NoteInformations _selectedNote;
        /// <summary>
        /// Selected note.
        /// </summary>
        public NoteInformations SelectedNote
        {
            get => _selectedNote;
            set => SetProperty(ref _selectedNote, value);
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NoteViewModel()
        {
            // Set title:
            WidgetModel.Title = "Note widget";

            // Set widget size:
            WidgetModel.Width = 360;
            WidgetModel.Height = 280;

            // Set widget position:
            WidgetModel.ScreenPositionY += 330;

            // Set widget background color:
            WidgetModel.BackgroundColor = new SolidColorBrush(Colors.OrangeRed);

            // Set selected note:
            SelectedNote = new NoteInformations()
            {
                Text = "Write note here...",
                SaveFilePath = Path.Combine(notesDirePath, "MyNote1")
            };

            // Check save notes:
            LoadNotesList();

            //todo poprawić to
            BackButtonCommand = new RelayCommand((o) =>
            {
                if (notes.Count > 0)
                {
                    if (WidgetModel.SelectedNoteIndex > 0)
                    {
                        WidgetModel.SelectedNoteIndex--;
                    }
                    else WidgetModel.SelectedNoteIndex = notes.Count - 1;
                }
            });

            NextButtonCommand = new RelayCommand((o) =>
            {
                if (notes.Count > 0)
                {
                    if (WidgetModel.SelectedNoteIndex + 1 < notes.Count)
                    {
                        WidgetModel.SelectedNoteIndex++;
                    }
                    else WidgetModel.SelectedNoteIndex = 0;
                }
            });

        }

        private ICommand _backButtonCommand = new RelayCommand((o) => { });
        /// <summary>
        /// Command that back button clicked.
        /// </summary>
        public ICommand BackButtonCommand
        {
            get => _backButtonCommand;
            set => SetProperty(ref _backButtonCommand, value);
        }

        private ICommand _nextButtonCommand = new RelayCommand((o) => { });
        /// <summary>
        /// Command that next button clicked.
        /// </summary>
        public ICommand NextButtonCommand
        {
            get => _nextButtonCommand;
            set => SetProperty(ref _nextButtonCommand, value);
        }

        //todo naprawić to
        /// <summary>
        /// Load notes from notes dire.
        /// </summary>
        private void LoadNotesList()
        {
            Directory.CreateDirectory(notesDirePath);

            notes = new List<NoteInformations>();

            var noteFiles = Directory.GetFiles(notesDirePath);
            if (noteFiles.Length > 0)
                foreach (var noteFile in noteFiles)
                {
                    notes.Add(new NoteInformations()
                    {
                        SaveFilePath = noteFile,
                        Text = File.ReadAllText(noteFile)
                    });
                }

            if (WidgetModel.SelectedNoteIndex < notes.Count)
                SelectedNote = notes[WidgetModel.SelectedNoteIndex];

        }

    }
}
