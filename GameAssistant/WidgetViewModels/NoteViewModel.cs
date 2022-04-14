using GameAssistant.Core;
using GameAssistant.Models;
using GameAssistant.Services;
using GameAssistant.Widgets;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GameAssistant.WidgetViewModels
{
    /// <summary>
    /// View model that contains bindings for NoteWidget.
    /// </summary>
    internal class NoteViewModel : BindableObject, IWidgetViewModel<NoteModel>
    {
        /// <summary>
        /// Dire of notes.
        /// </summary>
        private static readonly string notesDirePath = Path.Combine(AppFileSystem.GetSaveDireConfigurationPath(nameof(NoteWidget)), "Notes");

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
        private readonly List<NoteInformations> notes = new List<NoteInformations>();

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
            WidgetModel.BackgroundAnimatedBrush.BrushContainer.Variable = new SolidColorBrush(Colors.OrangeRed);

            // Set selected note:
            SelectedNote = new NoteInformations()
            {
                Text = "Write note here...",
                SaveFilePath = Path.Combine(notesDirePath, "MyNote1")
            };

            BackButtonCommand = new RelayCommand((o) =>
            {
                if (notes.Count > 0)
                {
                    if (WidgetModel.SelectedNoteIndex > 0)
                    {
                        SelectedNote = notes[--WidgetModel.SelectedNoteIndex];
                    }
                    else SelectedNote = notes[WidgetModel.SelectedNoteIndex = notes.Count - 1];
                }
            });

            NextButtonCommand = new RelayCommand((o) =>
            {
                if (notes.Count > 0)
                {
                    if (WidgetModel.SelectedNoteIndex + 1 < notes.Count)
                    {
                        SelectedNote = notes[++WidgetModel.SelectedNoteIndex];
                    }
                    else SelectedNote = notes[WidgetModel.SelectedNoteIndex = 0];
                }
            });

            AddButtonCommand = new RelayCommand((o) =>
            {
                var newFileName = "Note";
                bool doMake;
                for (int i = 0; true; i++)
                {
                    doMake = true;

                    if (i == 1)
                        newFileName += "_";

                    foreach (var file in Directory.GetFiles(notesDirePath))
                    {
                        if (i == 0 && file == Path.Combine(notesDirePath, newFileName + ".txt") ||
                        file == Path.Combine(notesDirePath, newFileName + i.ToString() + ".txt"))
                            doMake = false;
                    }

                    if (doMake)
                    {
                        if (i > 0)
                            newFileName += i.ToString();

                        newFileName += ".txt";

                        newFileName = Path.Combine(notesDirePath, newFileName);

                        using (var file = File.CreateText(newFileName))
                        {
                            file.Write("Write something...");
                        };

                        LoadNotesList();
                        SelectedNote = notes[WidgetModel.SelectedNoteIndex = notes.Count - 1];

                        break;
                    }
                }
            });

            DeleteButtonCommand = new RelayCommand((o) =>
            {
                File.Delete(SelectedNote.SaveFilePath);
                LoadNotesList();
                WidgetModel.SelectedNoteIndex = 0;
                if (notes.Count > 0)
                    SelectedNote = notes[WidgetModel.SelectedNoteIndex];
            });

            SettingsVisibleButtonCommand = new RelayCommand((o) =>
            {
                switch (WidgetModel.SettingsBarVisibility)
                {
                    case System.Windows.Visibility.Visible:
                        WidgetModel.SettingsBarVisibility = System.Windows.Visibility.Hidden;
                        TextBoxMargin = new Thickness(10, 10, 10, 10);
                        break;
                    case System.Windows.Visibility.Hidden:
                    default:
                        WidgetModel.SettingsBarVisibility = System.Windows.Visibility.Visible;
                        TextBoxMargin = new Thickness(10, 30, 10, 10);
                        break;
                }

            });

            // Check save notes:
            LoadNotesList();

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

        private ICommand _settingsVisibleButtonCommand = new RelayCommand((o) => { });
        /// <summary>
        /// Command that settings visible button clicked.
        /// </summary>
        public ICommand SettingsVisibleButtonCommand
        {
            get => _settingsVisibleButtonCommand;
            set => SetProperty(ref _settingsVisibleButtonCommand, value);
        }

        private ICommand _addButtonCommand = new RelayCommand((o) => { });
        /// <summary>
        /// Command that add button clicked.
        /// </summary>
        public ICommand AddButtonCommand
        {
            get => _addButtonCommand;
            set => SetProperty(ref _addButtonCommand, value);
        }

        private ICommand _deleteButtonCommand = new RelayCommand((o) => { });
        /// <summary>
        /// Command that delete button clicked.
        /// </summary>
        public ICommand DeleteButtonCommand
        {
            get => _deleteButtonCommand;
            set => SetProperty(ref _deleteButtonCommand, value);
        }

        private Thickness _textBoxMargin = new Thickness(10, 30, 10, 10);
        /// <summary>
        /// Margin of text box margin.
        /// </summary>
        public Thickness TextBoxMargin
        {
            get => _textBoxMargin;
            set => SetProperty(ref _textBoxMargin, value);
        }

        /// <summary>
        /// Load notes from notes dire.
        /// </summary>
        private void LoadNotesList()
        {
            Directory.CreateDirectory(notesDirePath);

            notes.Clear();

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
            else
                AddButtonCommand.Execute(this);

            if (WidgetModel.SelectedNoteIndex < notes.Count)
                SelectedNote = notes[WidgetModel.SelectedNoteIndex];

        }


    }
}
