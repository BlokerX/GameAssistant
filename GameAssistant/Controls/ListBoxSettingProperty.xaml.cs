using GameAssistant.Core;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using Forms = System.Windows.Forms;

namespace GameAssistant.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy ListBoxSettingProperty.xaml
    /// </summary>
    public partial class ListBoxSettingProperty : BindableControl, ISettingProperty
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ListBoxSettingProperty()
        {
            InitializeComponent();
            if (Elements.Count > 0)
                PropertyValue = Elements[SelectedElementIndex];
        }

        public Brush BorderColor
        {
            get => MainBorder.Background;
            set => MainBorder.Background = value;
        }

        public Brush BackgorundColor
        {
            set
            {
                FirstColumnGrid.Background = value;
                SecondColumnGrid.Background = value;
            }
        }

        public string PropertyName
        {
            get => PropertyNameLabel.Content.ToString();
            set => PropertyNameLabel.Content = value;
        }

        public Brush ForegroundColor
        {
            set
            {
                PropertyNameLabel.Foreground = value;
                BackButton.Foreground = value;
                SelectedElementLabel.Foreground = value;
                NextButton.Foreground = value;
            }
        }

        private List<string> _elements = new List<string>() { string.Empty };
        /// <summary>
        /// Elements.
        /// </summary>
        public List<string> Elements
        {
            get => _elements;
            set => SetProperty(ref _elements, value);
        }

        private int _selectedElementIndex;
        /// <summary>
        /// The selected element's index.
        /// </summary>
        public int SelectedElementIndex
        {
            get => _selectedElementIndex;
            set
            {
                SetProperty(ref _selectedElementIndex, value);
                if (Elements.Count > value)
                    PropertyValue = Elements[value];
            }
        }


        /// <summary>
        /// Property value (string).
        /// </summary>
        public string PropertyValue
        {
            get => SelectedElementLabel.Content.ToString();
            private set
            {
                SelectedElementLabel.Content = value;
                PropertyValueChanged?.Invoke(this, _selectedElementIndex);
            }
        }

        private void BackButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (SelectedElementIndex > 0)
                --SelectedElementIndex;
            else
                SelectedElementIndex = Elements.Count - 1;
        }

        private void NextButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (SelectedElementIndex + 1 < Elements.Count)
                ++SelectedElementIndex;
            else
                SelectedElementIndex = 0;
        }

        /// <summary>
        /// On PropertyValue changed.
        /// </summary>
        public event EventHandler<int> PropertyValueChanged;

    }
}
