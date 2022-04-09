using GameAssistant.Core;
using GameAssistant.Models;
using System.Windows.Input;
using System.Windows.Media;

namespace GameAssistant.WidgetViewModels
{
    /// <summary>
    /// View model that contains bindings for CalculatorWidget.
    /// </summary>
    internal class CalculatorViewModel : BindableObject, IWidgetViewModel<CalculatorModel>
    {
        private CalculatorModel _widgetModel = new CalculatorModel();
        /// <summary>
        /// Calculator widget properties.
        /// </summary>
        public CalculatorModel WidgetModel
        {
            get => _widgetModel;
            set => SetProperty(ref _widgetModel, value);
        }

        private string _calculatorTextBox = "0";
        /// <summary>
        /// The textbox with numbers.
        /// </summary>
        public string CalculatorTextBox
        {
            get => _calculatorTextBox;
            set => SetProperty(ref _calculatorTextBox, value);
        }

        /// <summary>
        /// First number in calculator's memory.
        /// </summary>
        private double firstNumber = 0;

        /// <summary>
        /// Second number in calculator's memory.
        /// </summary>
        private double secondNumber = 0;

        /// <summary>
        /// Type of calculate operation.
        /// </summary>
        private char operationType = ' ';

        /// <summary>
        /// If first number is selected return true.
        /// </summary>
        private bool firstNumberSelected = false;

        /// <summary>
        /// If first number is from equals previous calculate.
        /// </summary>
        private bool firstNumberFromEquals = false;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CalculatorViewModel()
        {
            // Set title:
            WidgetModel.Title = "Calculator widget";

            // Set widget size:
            WidgetModel.Width = 240;
            WidgetModel.Height = 260;

            // Set widget position:
            WidgetModel.ScreenPositionY += 620;

            // Set widget background color:
            WidgetModel.BackgroundAnimatedBrush.BrushContainer.Variable = new SolidColorBrush(Colors.GhostWhite);

            // Command initialize:
            Button0_Command = new RelayCommand((o) => ClickNumberButton(0));
            Button1_Command = new RelayCommand((o) => ClickNumberButton(1));
            Button2_Command = new RelayCommand((o) => ClickNumberButton(2));
            Button3_Command = new RelayCommand((o) => ClickNumberButton(3));
            Button4_Command = new RelayCommand((o) => ClickNumberButton(4));
            Button5_Command = new RelayCommand((o) => ClickNumberButton(5));
            Button6_Command = new RelayCommand((o) => ClickNumberButton(6));
            Button7_Command = new RelayCommand((o) => ClickNumberButton(7));
            Button8_Command = new RelayCommand((o) => ClickNumberButton(8));
            Button9_Command = new RelayCommand((o) => ClickNumberButton(9));

            AddictionButton_Command = new RelayCommand((o) => ClickOperationButton('+'));
            SubstractButton_Command = new RelayCommand((o) => ClickOperationButton('-'));
            MultiplyButton_Command = new RelayCommand((o) => ClickOperationButton('*'));
            DivideButton_Command = new RelayCommand((o) => ClickOperationButton('/'));

            PercentButton_Command = new RelayCommand((o) =>
            {
                if (double.TryParse(CalculatorTextBox, out var dResult))
                {
                    CalculatorTextBox = (dResult / 100).ToString();
                }
            });

            DecimalButton_Command = new RelayCommand((o) =>
            {
                if (firstNumberFromEquals)
                {
                    firstNumberSelected = false;
                    firstNumberFromEquals = false;
                    CalculatorTextBox = "0";
                }

                CalculatorTextBox += ',';
            });

            EqualsButton_Command = new RelayCommand((o) => ClickEqualsButton());

            BackspaceButton_Command = new RelayCommand((o) =>
            {
                if (CalculatorTextBox.Length > 0)
                    CalculatorTextBox = CalculatorTextBox.Remove(CalculatorTextBox.Length - 1);

                if (CalculatorTextBox.Length == 0)
                    CalculatorTextBox = "0";
            });

            ClearButton_Command = new RelayCommand((o) => ClickClearButton());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        private void ClickNumberButton(int number)
        {
            if (firstNumberFromEquals)
            {
                firstNumberSelected = false;
                firstNumberFromEquals = false;
                CalculatorTextBox = "0";
            }

            CalculatorTextBox += number.ToString();

            if (decimal.TryParse(CalculatorTextBox, out var dResult))
                CalculatorTextBox = dResult.ToString();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        private void ClickOperationButton(char operation)
        {
            if (double.TryParse(CalculatorTextBox, out var dResult))
                firstNumber = dResult;
            else return;

            operationType = operation;
            CalculatorTextBox = "0";
            firstNumberFromEquals = false;
            firstNumberSelected = true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClickEqualsButton()
        {
            if (double.TryParse(CalculatorTextBox, out var dResult))
                secondNumber = dResult;
            else return;

            double result;
            if (!firstNumberSelected && !firstNumberFromEquals)
            {
                result = firstNumber;
                return;
            }

            switch (operationType)
            {
                case '+':
                    result = (firstNumber + secondNumber);
                    break;

                case '-':
                    result = (firstNumber - secondNumber);
                    break;

                case '*':
                    result = (firstNumber * secondNumber);
                    break;

                case '/':
                    result = (firstNumber / secondNumber);
                    break;

                default:
                    return;
            }

            firstNumber = result;
            firstNumberFromEquals = true;
            CalculatorTextBox = (result).ToString();
            operationType = ' ';
            firstNumberSelected = false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClickClearButton()
        {
            firstNumber = 0;
            secondNumber = 0;
            operationType = ' ';
            firstNumberSelected = false;
            firstNumberFromEquals = false;
            CalculatorTextBox = "0";
        }

        #region Buttons Commands

        #region Numeric Buttons

        private ICommand _button1_Command = new RelayCommand((o) => { });
        /// <summary>
        /// Command that button 1 clicked.
        /// </summary>
        public ICommand Button1_Command
        {
            get => _button1_Command;
            set => SetProperty(ref _button1_Command, value);
        }

        private ICommand _button2_Command = new RelayCommand((o) => { });
        /// <summary>
        /// Command that button 2 clicked.
        /// </summary>
        public ICommand Button2_Command
        {
            get => _button2_Command;
            set => SetProperty(ref _button2_Command, value);
        }

        private ICommand _button3_Command = new RelayCommand((o) => { });
        /// <summary>
        /// Command that button 3 clicked.
        /// </summary>
        public ICommand Button3_Command
        {
            get => _button3_Command;
            set => SetProperty(ref _button3_Command, value);
        }

        private ICommand _button4_Command = new RelayCommand((o) => { });
        /// <summary>
        /// Command that button 4 clicked.
        /// </summary>
        public ICommand Button4_Command
        {
            get => _button4_Command;
            set => SetProperty(ref _button4_Command, value);
        }

        private ICommand _button5_Command = new RelayCommand((o) => { });
        /// <summary>
        /// Command that button 5 clicked.
        /// </summary>
        public ICommand Button5_Command
        {
            get => _button5_Command;
            set => SetProperty(ref _button5_Command, value);
        }

        private ICommand _button6_Command = new RelayCommand((o) => { });
        /// <summary>
        /// Command that button 6 clicked.
        /// </summary>
        public ICommand Button6_Command
        {
            get => _button6_Command;
            set => SetProperty(ref _button6_Command, value);
        }

        private ICommand _button7_Command = new RelayCommand((o) => { });
        /// <summary>
        /// Command that button 7 clicked.
        /// </summary>
        public ICommand Button7_Command
        {
            get => _button7_Command;
            set => SetProperty(ref _button7_Command, value);
        }

        private ICommand _button8_Command = new RelayCommand((o) => { });
        /// <summary>
        /// Command that button 8 clicked.
        /// </summary>
        public ICommand Button8_Command
        {
            get => _button8_Command;
            set => SetProperty(ref _button8_Command, value);
        }

        private ICommand _button9_Command = new RelayCommand((o) => { });
        /// <summary>
        /// Command that button 9 clicked.
        /// </summary>
        public ICommand Button9_Command
        {
            get => _button9_Command;
            set => SetProperty(ref _button9_Command, value);
        }

        private ICommand _button0_Command = new RelayCommand((o) => { });
        /// <summary>
        /// Command that button 0 clicked.
        /// </summary>
        public ICommand Button0_Command
        {
            get => _button0_Command;
            set => SetProperty(ref _button0_Command, value);
        }

        #endregion

        #region Operation Buttons

        private ICommand _addictionButton_Command = new RelayCommand((o) => { });
        /// <summary>
        /// Command that addiction button clicked.
        /// </summary>
        public ICommand AddictionButton_Command
        {
            get => _addictionButton_Command;
            set => SetProperty(ref _addictionButton_Command, value);
        }

        private ICommand _substractButton_Command = new RelayCommand((o) => { });
        /// <summary>
        /// Command that subtract button clicked.
        /// </summary>
        public ICommand SubstractButton_Command
        {
            get => _substractButton_Command;
            set => SetProperty(ref _substractButton_Command, value);
        }

        private ICommand _multiplyButton_Command = new RelayCommand((o) => { });
        /// <summary>
        /// Command that multiply button clicked.
        /// </summary>
        public ICommand MultiplyButton_Command
        {
            get => _multiplyButton_Command;
            set => SetProperty(ref _multiplyButton_Command, value);
        }

        private ICommand _divideButton_Command = new RelayCommand((o) => { });
        /// <summary>
        /// Command that divide button clicked.
        /// </summary>
        public ICommand DivideButton_Command
        {
            get => _divideButton_Command;
            set => SetProperty(ref _divideButton_Command, value);
        }

        #endregion

        #region Other Buttons

        private ICommand _percentButton_Command = new RelayCommand((o) => { });
        /// <summary>
        /// Command that percent button clicked.
        /// </summary>
        public ICommand PercentButton_Command
        {
            get => _percentButton_Command;
            set => SetProperty(ref _percentButton_Command, value);
        }

        private ICommand _equalsButton_Command = new RelayCommand((o) => { });
        /// <summary>
        /// Command that equals button clicked.
        /// </summary>
        public ICommand EqualsButton_Command
        {
            get => _equalsButton_Command;
            set => SetProperty(ref _equalsButton_Command, value);
        }

        private ICommand _decimalButton_Command = new RelayCommand((o) => { });
        /// <summary>
        /// Command that decimal button clicked.
        /// </summary>
        public ICommand DecimalButton_Command
        {
            get => _decimalButton_Command;
            set => SetProperty(ref _decimalButton_Command, value);
        }

        private ICommand _backspaceButton_Command = new RelayCommand((o) => { });
        /// <summary>
        /// Command that backspace button clicked.
        /// </summary>
        public ICommand BackspaceButton_Command
        {
            get => _backspaceButton_Command;
            set => SetProperty(ref _backspaceButton_Command, value);
        }

        private ICommand _clearButton_Command = new RelayCommand((o) => { });
        /// <summary>
        /// Command that clear button clicked.
        /// </summary>
        public ICommand ClearButton_Command
        {
            get => _clearButton_Command;
            set => SetProperty(ref _clearButton_Command, value);
        }

        private ICommand _moreOptionsButton_Command = new RelayCommand((o) => { });
        /// <summary>
        /// Command that more options button clicked.
        /// </summary>
        public ICommand MoreOptionsButton_Command
        {
            get => _moreOptionsButton_Command;
            set => SetProperty(ref _moreOptionsButton_Command, value);
        }

        #endregion

        #endregion
    }
}
