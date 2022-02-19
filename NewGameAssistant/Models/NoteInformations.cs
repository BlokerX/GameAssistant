using NewGameAssistant.Core;
using System.IO;

namespace NewGameAssistant.Models
{
    internal class NoteInformations : BindableObject
    {
        private string _text = string.Empty;
        /// <summary>
        /// The text of note.
        /// </summary>
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private string _saveFilePath = string.Empty;
        /// <summary>
        /// Note save file path.
        /// </summary>
        public string SaveFilePath
        {
            get => _saveFilePath;
            set => SetProperty(ref _saveFilePath, value);
        }

        /// <summary>
        /// Create NoteInformations base on saveFilePath property.
        /// </summary>
        /// <param name="saveFilePath">Note file path.</param>
        public NoteInformations(string saveFilePath)
        {
            SaveFilePath = saveFilePath;
            Text = File.ReadAllText(saveFilePath);
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NoteInformations() { }

    }
}
