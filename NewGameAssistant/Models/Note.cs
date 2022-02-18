using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGameAssistant.Models
{
    internal class Note : BindableObject
    {
        private string _text = string.Empty;
        [JsonIgnore]
        /// <summary>
        /// The text of note.
        /// </summary>
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public Note(string saveFilePath)
        {
            SaveFilePath = saveFilePath;
            Text = File.ReadAllText(saveFilePath);
        }

        public Note() { }

        private string _saveFilePath = string.Empty;
        /// <summary>
        /// Note save file path.
        /// </summary>
        public string SaveFilePath
        {
            get => _saveFilePath;
            set => SetProperty(ref _saveFilePath, value);
        }


    }
}
