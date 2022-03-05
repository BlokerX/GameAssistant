using GameAssistant.Core;
using System.IO;

namespace GameAssistant.Models
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
            set
            {
                SetProperty(ref _text, value);
                OnChangeTextMethod();
            }
        }

        /// <summary>
        /// Invoke when text changed.
        /// </summary>
        private void OnChangeTextMethod()
        {
            if (File.Exists(SaveFilePath))
                File.WriteAllText(SaveFilePath, Text);
        }

        private string _saveFilePath = string.Empty;
        /// <summary>
        /// Note save file path.
        /// </summary>
        public string SaveFilePath
        {
            get => _saveFilePath;
            set
            {
                SetProperty(ref _saveFilePath, value);
                var x = "";
                for (int i = value.LastIndexOf(Path.DirectorySeparatorChar) + 1; i < value.Length; i++)
                {
                    x += value[i];
                }
                SaveFileName = x;
            }
        }

        private string _saveFileName = string.Empty;
        /// <summary>
        /// Note save file name.
        /// </summary>
        public string SaveFileName
        {
            get => _saveFileName;
            set => SetProperty(ref _saveFileName, value);
        }

        /// <summary>
        /// Create NoteInformations base on saveFilePath property.
        /// </summary>
        /// <param name="saveFilePath">Note file path.</param>
        public NoteInformations(string saveDirePath, string fileName)
        {
            var fullPath = Path.Combine(saveDirePath, fileName);
            SaveFilePath = fullPath;
            Text = File.ReadAllText(fullPath);
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NoteInformations() { }

    }
}
