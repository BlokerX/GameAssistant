using GameAssistant.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Forms = System.Windows.Forms;

namespace GameAssistant.ControlViewModels
{
    internal class ColorSettingPropertyViewModel : SettingPropertyViewModelBase
    {
        public ColorSettingPropertyViewModel(ref Brush propertyColor)
        {
            //_propertyColor = propertyColor;
        }


        private Brush _propertyColor;
        /// <summary>
        /// Property value(Color).
        /// </summary>
        public Brush PropertyColor
        {
            get => _propertyColor;
            set => SetProperty(ref _propertyColor, value);
        }

    }
}
