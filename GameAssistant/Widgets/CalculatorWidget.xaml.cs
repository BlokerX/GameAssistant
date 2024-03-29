﻿using GameAssistant.Models;
using GameAssistant.Services;

namespace GameAssistant.Widgets
{
    /// <summary>
    /// Logika interakcji dla klasy CalculatorWidget.xaml
    /// </summary>
    public partial class CalculatorWidget : WidgetBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public CalculatorWidget()
        {
            InitializeComponent();
            WindowSizeOrPositionChangedEvent += () => WidgetManager.SaveWidgetConfigurationInFile<CalculatorWidget, CalculatorModel>(this);
        }

        public static WidgetEvents Events = new WidgetEvents();

    }
}
