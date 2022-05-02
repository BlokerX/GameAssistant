using Newtonsoft.Json;
using System;
using System.Windows;

namespace GameAssistant.Models
{
    /// <summary>
    /// Browser properties that able to serialize.
    /// </summary>
    internal class BrowserModel : WidgetModelBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public BrowserModel()
        {
            DragActiveChanged += (b) =>
            {
                try
                {
                    switch (IsDragActive)
                    {
                        case true:
                            DragAndDropBarVisibility = Visibility.Visible;
                            break;

                        case false:
                        default:
                            DragAndDropBarVisibility = Visibility.Hidden;
                            break;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            };
        }

        private Visibility _dragAndDropBarVisibility = Visibility.Hidden;
        [JsonIgnore]
        /// <summary>
        /// Drag and drop bar visibility state.
        /// </summary>
        public Visibility DragAndDropBarVisibility
        {
            get => _dragAndDropBarVisibility;
            set => SetProperty(ref _dragAndDropBarVisibility, value);
        }

        #region Serialize properties

        private string _address = "https://www.google.com/";
        /// <summary>
        /// The address of page in the browser.
        /// </summary>
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        private double _browserOpacity = 0.5;
        /// <summary>
        /// Browser's opacity.
        /// </summary>
        public double BrowserOpacity
        {
            get => _browserOpacity;
            set => SetProperty(ref _browserOpacity, value);
        }

        #endregion
    }
}
