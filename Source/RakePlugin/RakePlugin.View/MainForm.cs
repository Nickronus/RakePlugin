namespace RakePlugin.View
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using RakePlugin.Model;
    using RakePlugin.Wrapper;

    /// <summary>
    /// Класс главной формы.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Цвет корректного значения.
        /// </summary>
        private readonly Color _correctСolor = Color.White;

        /// <summary>
        /// Цвет ошибочного значения.
        /// </summary>
        private readonly Color _errorColor = Color.LightPink;

        /// <summary>
        /// Словарь ошибок.
        /// </summary>
        private Dictionary<string, bool> _dictionaryErrors = new Dictionary<string, bool>()
        {
            { nameof(WorkingSurfaceWidthComboBox), true },
            { nameof(NumberOfTeethComboBox), true },
            { nameof(LengthOfTeethTextBox), true },
            { nameof(HandleDiameterTextBox), true },
            { nameof(HandleLengthTextBox), true },
            { nameof(WorkingSurfaceLengthComboBox), true },
            { nameof(ToothShapeComboBox), true },
            { nameof(LightweightWorkSurfaceComboBox), true }
        };

        /// <summary>
        /// Параметры грабель.
        /// </summary>
        private RakeParameters _parameters = new RakeParameters();

        /// <summary>
        /// Построитель грабель.
        /// </summary>
        private RakeBuilder _builder = new RakeBuilder();

        /// <summary>
        /// Длина рабочей поверхности.
        /// </summary>
        private Parameter _workingSurfaceLength = new Parameter
        {
            MaxValue = 150,
            MinValue = 30,
            Value = 100
        };

        /// <summary>
        /// Тип зубца.
        /// </summary>
        private Parameter _toothShape = new Parameter
        {
            MaxValue = (float)ToothShareType.circle,
            MinValue = (float)ToothShareType.square,
            Value = (float)ToothShareType.square
        };

        /// <summary>
        /// Вид рабочей поверхности.
        /// </summary>
        private Parameter _lightweightWorkSurface = new Parameter
        {
            MaxValue = 1,
            MinValue = 0,
            Value = 0
        };

        /// <summary>
        /// Ширина рабочей поверхности.
        /// </summary>
        private Parameter _workingSurfaceWidth = new Parameter
        {
            MaxValue = 1290,
            MinValue = 90,
            Value = 330
        };

        /// <summary>
        /// Количество зубцов.
        /// </summary>
        private Parameter _numberOfTeeth = new Parameter
        {
            MaxValue = 17,
            MinValue = 5,
            Value = 5
        };

        /// <summary>
        /// Длина зубца.
        /// </summary>
        private Parameter _lengthOfTeeth = new Parameter
        {
            MaxValue = 200,
            MinValue = 50,
            Value = 100
        };

        /// <summary>
        /// Диаметр ручки.
        /// </summary>
        private Parameter _handleDiameter = new Parameter
        {
            MaxValue = 30,
            MinValue = 20,
            Value = 30
        };

        /// <summary>
        /// Длина ручки.
        /// </summary>
        private Parameter _handleLength = new Parameter
        {
            MaxValue = 2000,
            MinValue = 1000,
            Value = 1000
        };

        /// <summary>
        /// Всплывающее окно.
        /// </summary>
        private ToolTip _toolTip = new ToolTip();

        /// <summary>
        /// Инициализация главного окна.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Проверка формы на ошибки.
        /// </summary>
        private void CheckFormOnErrors()
        {
            foreach (var error in _dictionaryErrors)
            {
                if (error.Value == false)
                {
                    BuildFigure.Enabled = false;
                    return;
                }
            }

            BuildFigure.Enabled = true;
        }

        /// <summary>
        /// Построение фигуры.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuildFigureClick(object sender, EventArgs e)
        {
            _parameters.Parameters = new Dictionary<ParameterType, Parameter>
            {
                { ParameterType.WorkingSurfaceWidth, _workingSurfaceWidth},
                { ParameterType.NumberOfTeeth, _numberOfTeeth},
                { ParameterType.LengthOfTeeth, _lengthOfTeeth},
                { ParameterType.HandleDiameter, _handleDiameter},
                { ParameterType.HandleLength, _handleLength},
                { ParameterType.WorkingSurfaceLength, _workingSurfaceLength},
                { ParameterType.ToothShape, _toothShape},
                { ParameterType.LightweightWorkSurface, _lightweightWorkSurface},
            };

            _builder.BuildRake(_parameters);
        }

        /// <summary>
        /// Создать разброс количества зубцов.
        /// </summary>
        private void MakeTeethSpread()
        {
            _numberOfTeeth.MaxValue = (((_workingSurfaceWidth.Value / 10) - 1) / 2) + 1;
            _numberOfTeeth.MinValue = (int)(((_workingSurfaceWidth.Value / 1) - 1) / 7) + 1;
        }

        /// <summary>
        /// Создать разброс ширины рабочей поверхности.
        /// </summary>
        private void MakeWorkingSurfaceSpread()
        {
            _workingSurfaceWidth.MaxValue = (((_numberOfTeeth.Value - 1) * 8) + 1) * 10;
            _workingSurfaceWidth.MinValue = (((_numberOfTeeth.Value - 1) * 2) + 1) * 10;
        }

        private void WorkingSurfaceWidthComboBox_TextChanged(object sender, EventArgs e)
        {
            if (WorkingSurfaceWidthComboBox.Text != "")
            {
                WorkingSurfaceWidthComboBox.BackColor = _correctСolor;
                _toolTip.SetToolTip(WorkingSurfaceWidthComboBox, "");
                _dictionaryErrors[nameof(WorkingSurfaceWidthComboBox)] = true;
                _workingSurfaceWidth.Value = System.Convert.ToSingle(WorkingSurfaceWidthComboBox.Text);
                MakeTeethSpread();
                if (!Validator.Validate(_numberOfTeeth))
                {
                    NumberOfTeethComboBox.BackColor = _errorColor;
                    _toolTip.SetToolTip(NumberOfTeethComboBox, "Неподходящее количество зубьев для выбранной ширины рабочей поверхности.");
                    _dictionaryErrors[nameof(NumberOfTeethComboBox)] = false;
                    CheckFormOnErrors();
                }
                else
                {
                    NumberOfTeethComboBox.BackColor = _correctСolor;
                    _toolTip.SetToolTip(NumberOfTeethComboBox, "");
                    _dictionaryErrors[nameof(NumberOfTeethComboBox)] = true;
                    CheckFormOnErrors();
                }
            }
        }

        private void NumberOfTeethComboBox_TextChanged(object sender, EventArgs e)
        {
            if (NumberOfTeethComboBox.Text != "")
            {
                NumberOfTeethComboBox.BackColor = _correctСolor;
                _toolTip.SetToolTip(NumberOfTeethComboBox, "");
                _dictionaryErrors[nameof(NumberOfTeethComboBox)] = true;
                _numberOfTeeth.Value = System.Convert.ToSingle(NumberOfTeethComboBox.Text);
                MakeWorkingSurfaceSpread();
                if (!Validator.Validate(_workingSurfaceWidth))
                {
                    WorkingSurfaceWidthComboBox.BackColor = _errorColor;
                    _toolTip.SetToolTip(WorkingSurfaceWidthComboBox, "Неподходящая ширина рабочей поверхности для выбранного количества зубьев");
                    _dictionaryErrors[nameof(WorkingSurfaceWidthComboBox)] = false;
                    CheckFormOnErrors();
                }
                else
                {
                    WorkingSurfaceWidthComboBox.BackColor = _correctСolor;
                    _toolTip.SetToolTip(WorkingSurfaceWidthComboBox, "");
                    _dictionaryErrors[nameof(WorkingSurfaceWidthComboBox)] = true;
                    CheckFormOnErrors();
                }
            }
        }

        private void LengthOfTeethTextBox_TextChanged(object sender, EventArgs e)
        {
            if (LengthOfTeethTextBox.Text != "")
            {
                _lengthOfTeeth.Value = System.Convert.ToSingle(LengthOfTeethTextBox.Text);
                if (!Validator.Validate(_lengthOfTeeth))
                {
                    LengthOfTeethTextBox.BackColor = _errorColor;
                    _toolTip.SetToolTip(LengthOfTeethTextBox, "Длина зубьев должна быть в диапазоне от 50 до 200 мм");
                    _dictionaryErrors[nameof(LengthOfTeethTextBox)] = false;
                    CheckFormOnErrors();
                }
                else
                {
                    LengthOfTeethTextBox.BackColor = _correctСolor;
                    _toolTip.SetToolTip(LengthOfTeethTextBox, "");
                    _dictionaryErrors[nameof(LengthOfTeethTextBox)] = true;
                    CheckFormOnErrors();
                }
            }
        }

        private void HandleDiametertTextBox_TextChanged(object sender, EventArgs e)
        {
            if (HandleDiameterTextBox.Text != "")
            {
                _handleDiameter.Value = System.Convert.ToSingle(HandleDiameterTextBox.Text);
                if (!Validator.Validate(_handleDiameter))
                {
                    HandleDiameterTextBox.BackColor = _errorColor;
                    _toolTip.SetToolTip(HandleDiameterTextBox, "Диаметр ручки должен быть в диапазоне от 20 до 30 мм");
                    _dictionaryErrors[nameof(HandleDiameterTextBox)] = false;
                    CheckFormOnErrors();
                }
                else
                {
                    HandleDiameterTextBox.BackColor = _correctСolor;
                    _toolTip.SetToolTip(HandleDiameterTextBox, "");
                    _dictionaryErrors[nameof(HandleDiameterTextBox)] = true;
                    CheckFormOnErrors();
                }
            }
        }

        private void HandleLengthTextBox_TextChanged(object sender, EventArgs e)
        {
            if (HandleLengthTextBox.Text != "")
            {
                _handleLength.Value = System.Convert.ToSingle(HandleLengthTextBox.Text);
                if (!Validator.Validate(_handleLength))
                {
                    HandleLengthTextBox.BackColor = _errorColor;
                    _toolTip.SetToolTip(HandleLengthTextBox, "Длина ручки должна быть в диапазоне от 1000 до 2000 мм");
                    _dictionaryErrors[nameof(HandleLengthTextBox)] = false;
                    CheckFormOnErrors();
                }
                else
                {
                    HandleLengthTextBox.BackColor = _correctСolor;
                    _toolTip.SetToolTip(HandleLengthTextBox, "");
                    _dictionaryErrors[nameof(HandleLengthTextBox)] = true;
                    CheckFormOnErrors();
                }
            }
        }

        private void KeyPress(KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8 && number != 44) // цифры, клавиша BackSpace и запятая
            {
                e.Handled = true;
            }
        }

        private void LengthOfTeethTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPress(e);
        }

        private void HandleDiameterTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPress(e);
        }

        private void HandleLengthTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPress(e);
        }

        private void ToothShapeComboBox_TextChanged(object sender, EventArgs e)
        {
            if (ToothShapeComboBox.Text != "")
            {
                _toothShape.Value = -1;
                if (ToothShapeComboBox.Text == "Квадрат")
                {
                    _toothShape.Value = 0;
                }

                if (ToothShapeComboBox.Text == "Круг")
                {
                    _toothShape.Value = 1;
                }

                if (!Validator.Validate(_toothShape))
                {
                    ToothShapeComboBox.BackColor = _errorColor;
                    _toolTip.SetToolTip(ToothShapeComboBox, "Неверное значение поля формы зуба");
                    _dictionaryErrors[nameof(ToothShapeComboBox)] = false;
                    CheckFormOnErrors();
                }
                else
                {
                    ToothShapeComboBox.BackColor = _correctСolor;
                    _toolTip.SetToolTip(ToothShapeComboBox, "");
                    _dictionaryErrors[nameof(ToothShapeComboBox)] = true;
                    CheckFormOnErrors();
                }
            }
        }

        private void WorkingSurfaceLengthComboBox_TextChanged(object sender, EventArgs e)
        {
            if (WorkingSurfaceLengthComboBox.Text != "")
            {
                _workingSurfaceLength.Value = System.Convert.ToSingle(WorkingSurfaceLengthComboBox.Text);
                if (!Validator.Validate(_workingSurfaceLength))
                {
                    WorkingSurfaceLengthComboBox.BackColor = _errorColor;
                    _toolTip.SetToolTip(WorkingSurfaceLengthComboBox, "Неверное значение поля длины рабочей поверхности");
                    _dictionaryErrors[nameof(WorkingSurfaceLengthComboBox)] = false;
                    CheckFormOnErrors();
                }
                else
                {
                    WorkingSurfaceLengthComboBox.BackColor = _correctСolor;
                    _toolTip.SetToolTip(WorkingSurfaceLengthComboBox, "");
                    _dictionaryErrors[nameof(WorkingSurfaceLengthComboBox)] = true;
                    CheckFormOnErrors();
                }
            }
        }

        private void LightweightWorkSurfaceComboBox_TextChanged(object sender, EventArgs e)
        {
            if (LightweightWorkSurfaceComboBox.Text != "")
            {
                _lightweightWorkSurface.Value = -1;
                if (LightweightWorkSurfaceComboBox.Text == "Нет")
                {
                    _lightweightWorkSurface.Value = 0;
                }

                if (LightweightWorkSurfaceComboBox.Text == "Да")
                {
                    _lightweightWorkSurface.Value = 1;
                }

                if (!Validator.Validate(_lightweightWorkSurface))
                {
                    LightweightWorkSurfaceComboBox.BackColor = _errorColor;
                    _toolTip.SetToolTip(LightweightWorkSurfaceComboBox, "Неверное значение поля облегчённости рабочей поверхности");
                    _dictionaryErrors[nameof(LightweightWorkSurfaceComboBox)] = false;
                    CheckFormOnErrors();
                }
                else
                {
                    LightweightWorkSurfaceComboBox.BackColor = _correctСolor;
                    _toolTip.SetToolTip(LightweightWorkSurfaceComboBox, "");
                    _dictionaryErrors[nameof(LightweightWorkSurfaceComboBox)] = true;
                    CheckFormOnErrors();
                }
            }
        }
    }
}