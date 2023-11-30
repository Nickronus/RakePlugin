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
        private readonly Dictionary<string, bool> _dictionaryErrors = new Dictionary<string, bool>()
        {
            { nameof(WorkingSurfaceWidthTextBox), true },
            { nameof(NumberOfTeethTextBox), true },
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
        private readonly RakeParameters _parameters = new RakeParameters();

        /// <summary>
        /// Построитель грабель.
        /// </summary>
        private readonly RakeBuilder _builder = new RakeBuilder();

        private readonly Parameter _distanceBetweenTeeth = new Parameter
        {
            MaxValue = 100,
            MinValue = 10,
            Value = 10
        };

        /// <summary>
        /// Длина рабочей поверхности.
        /// </summary>
        private readonly Parameter _workingSurfaceLength = new Parameter
        {
            MaxValue = 150,
            MinValue = 30,
            Value = 100
        };

        /// <summary>
        /// Тип зубца.
        /// </summary>
        private readonly Parameter _toothShape = new Parameter
        {
            MaxValue = (float)ToothShareType.circle,
            MinValue = (float)ToothShareType.square,
            Value = (float)ToothShareType.square
        };

        /// <summary>
        /// Вид рабочей поверхности.
        /// </summary>
        private readonly Parameter _lightweightWorkSurface = new Parameter
        {
            MaxValue = 1,
            MinValue = 0,
            Value = 0
        };

        /// <summary>
        /// Ширина рабочей поверхности.
        /// </summary>
        private readonly Parameter _workingSurfaceWidth = new Parameter
        {
            MaxValue = 1010,
            MinValue = 120,
            Value = 330
        };

        /// <summary>
        /// Количество зубцов.
        /// </summary>
        private readonly Parameter _numberOfTeeth = new Parameter
        {
            MaxValue = 51,
            MinValue = 2,
            Value = 9
        };

        /// <summary>
        /// Длина зубца.
        /// </summary>
        private readonly Parameter _lengthOfTeeth = new Parameter
        {
            MaxValue = 200,
            MinValue = 50,
            Value = 100
        };

        /// <summary>
        /// Диаметр ручки.
        /// </summary>
        private readonly Parameter _handleDiameter = new Parameter
        {
            MaxValue = 30,
            MinValue = 20,
            Value = 30
        };

        /// <summary>
        /// Длина ручки.
        /// </summary>
        private readonly Parameter _handleLength = new Parameter
        {
            MaxValue = 2000,
            MinValue = 1000,
            Value = 1000
        };

        /// <summary>
        /// Счётчик запуска построений в Компасе.
        /// </summary>
        private readonly Parameter _KompasOpenedCounter = new Parameter
        {
            Value = 0
        };

        /// <summary>
        /// Счётчик запуска построений в Solidworks.
        /// </summary>
        private readonly Parameter _SolidWorksOpenedCounter = new Parameter
        {
            Value = 0
        };

        /// <summary>
        /// Экзкмпляр Kompas3DWrapper.
        /// </summary>
        private Kompas3DWrapper _Kompas3DWrapper { get; set; } = new Kompas3DWrapper();

        /// <summary>
        /// Экземпляр SolidWorksWrapper.
        /// </summary>
        private SolidWorksWrapper _SolidWorksWrapper { get; set; } = new SolidWorksWrapper();

        /// <summary>
        /// Всплывающее окно.
        /// </summary>
        private readonly ToolTip _toolTip = new ToolTip();

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
        /// Фабрика построителей.
        /// </summary>
        /// <returns>Возвращает построитель.</returns>
        private I3DWrapper WrapperFactory()
        {
            if (saprComboBox.Text == "Компас 3D")
            {
                return _Kompas3DWrapper;
            }

            if (saprComboBox.Text == "SolidWorks")
            {
                return _SolidWorksWrapper;
            }

            return new Kompas3DWrapper();
        }

        /// <summary>
        /// Проверка открытости САПР.
        /// </summary>
        /// <returns>Возвращает правду о сапре.</returns>
        private bool IsSaprOpened()
        {
            if (saprComboBox.Text == "Компас 3D")
            {
                if (_KompasOpenedCounter.Value == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            if (saprComboBox.Text == "SolidWorks")
            {
                if (_SolidWorksOpenedCounter.Value == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Делает значение открытости сапры валидными.
        /// </summary>
        private void SaprOpenedCounterValidator()
        {
            if (saprComboBox.Text == "Компас 3D")
            {
                _KompasOpenedCounter.Value++;
            }

            if (saprComboBox.Text == "SolidWorks")
            {
                _SolidWorksOpenedCounter.Value++;
            }
        }

        /// <summary>
        /// Построение фигуры.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuildFigureClick(object sender, EventArgs e)
        {
            MakeDistanceBetweenTeeth();
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
                { ParameterType.DistanceBetweenTeeth, _distanceBetweenTeeth}
            };

            _builder.BuildRake(_parameters, WrapperFactory(), IsSaprOpened());
            SaprOpenedCounterValidator();
    }

        /// <summary>
        /// Создать разброс количества зубцов.
        /// </summary>
        private void MakeTeethSpread()
        {
            _numberOfTeeth.MaxValue = (int)(((_workingSurfaceWidth.Value / 10) - 1) / 2) + 1;
            _numberOfTeeth.MinValue = (int)(((_workingSurfaceWidth.Value / 10) - 1) / 10) + 1;
        }

        /// <summary>
        /// Создать разброс ширины рабочей поверхности
        /// </summary>
        private void MakeWorkingSurfaceSpread()
        {
            _workingSurfaceWidth.MaxValue = (((_numberOfTeeth.Value - 1) * 11) + 1) * 10;
            _workingSurfaceWidth.MinValue = (((_numberOfTeeth.Value - 1) * 2) + 1) * 10;
        }

        private void MakeDistanceBetweenTeeth()
        {
            _distanceBetweenTeeth.Value = (((_workingSurfaceWidth.Value / 10) - _numberOfTeeth.Value) / (_numberOfTeeth.Value - 1) * 10) + 10;
        }

        private void WorkingSurfaceWidthTextBox_TextChanged(object sender, EventArgs e)
        {
            if (WorkingSurfaceWidthTextBox.Text != "")
            {
                WorkingSurfaceWidthTextBox.BackColor = _correctСolor;
                _workingSurfaceWidth.MaxValue = 1010;
                _workingSurfaceWidth.MinValue = 120;
                _toolTip.SetToolTip(WorkingSurfaceWidthTextBox, "");
                _dictionaryErrors[nameof(WorkingSurfaceWidthTextBox)] = true;
                _workingSurfaceWidth.Value = System.Convert.ToSingle(WorkingSurfaceWidthTextBox.Text);
                MakeTeethSpread();
                if (!Validator.Validate(_workingSurfaceWidth))
                {
                    WorkingSurfaceWidthTextBox.BackColor = _errorColor;
                    _toolTip.SetToolTip(WorkingSurfaceWidthTextBox, "Неподходящая ширина рабочей поверхности.");
                    _dictionaryErrors[nameof(WorkingSurfaceWidthTextBox)] = false;
                    CheckFormOnErrors();
                }
                else
                {
                    NumberOfTeethlabel.Text = _numberOfTeeth.MinValue + " - " + _numberOfTeeth.MaxValue + " мм";    
                    if (!Validator.Validate(_numberOfTeeth))
                    {
                        NumberOfTeethTextBox.BackColor = _errorColor;
                        _toolTip.SetToolTip(NumberOfTeethTextBox, "Неподходящее количество зубьев для выбранной ширины рабочей поверхности.");
                        _dictionaryErrors[nameof(NumberOfTeethTextBox)] = false;
                        CheckFormOnErrors();
                    }
                    else
                    {
                        WorkingSurfaceWidthTextBox.BackColor = _correctСolor;
                        _dictionaryErrors[nameof(WorkingSurfaceWidthTextBox)] = true;

                        NumberOfTeethTextBox.BackColor = _correctСolor;
                        _toolTip.SetToolTip(NumberOfTeethTextBox, "");
                        _dictionaryErrors[nameof(NumberOfTeethTextBox)] = true;
                        CheckFormOnErrors();
                    }
                }
            }
        }

        private void NumberOfTeethTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NumberOfTeethTextBox.Text != "")
            {
                _numberOfTeeth.MinValue = 2;
                _numberOfTeeth.MaxValue = 51;
                NumberOfTeethTextBox.BackColor = _correctСolor;
                _toolTip.SetToolTip(NumberOfTeethTextBox, "");
                _dictionaryErrors[nameof(NumberOfTeethTextBox)] = true;
                _numberOfTeeth.Value = System.Convert.ToSingle(NumberOfTeethTextBox.Text);
                MakeWorkingSurfaceSpread();
                if (!Validator.Validate(_numberOfTeeth))
                {
                    NumberOfTeethTextBox.BackColor = _errorColor;
                    _toolTip.SetToolTip(NumberOfTeethTextBox, "Неподходящее количество зубьев для выбранной ширины рабочей поверхности.");
                    _dictionaryErrors[nameof(NumberOfTeethTextBox)] = false;
                    CheckFormOnErrors();
                }
                else
                {
                    WorkingSurfacewidthLabel.Text = _workingSurfaceWidth.MinValue + " - " + _workingSurfaceWidth.MaxValue + " мм";
                    if (!Validator.Validate(_workingSurfaceWidth))
                    {
                        WorkingSurfaceWidthTextBox.BackColor = _errorColor;
                        _toolTip.SetToolTip(WorkingSurfaceWidthTextBox, "Неподходящая ширина рабочей поверхности для выбранного количества зубьев");
                        _dictionaryErrors[nameof(WorkingSurfaceWidthTextBox)] = false;
                        CheckFormOnErrors();
                    }
                    else
                    {
                        NumberOfTeethTextBox.BackColor = _correctСolor;
                        _dictionaryErrors[nameof(NumberOfTeethTextBox)] = true;

                        WorkingSurfaceWidthTextBox.BackColor = _correctСolor;
                        _toolTip.SetToolTip(WorkingSurfaceWidthTextBox, "");
                        _dictionaryErrors[nameof(WorkingSurfaceWidthTextBox)] = true;
                        CheckFormOnErrors();
                    }
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

        private void WorkingSurfaceWidthTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPress(e);
        }

        private void NumberOfTeethTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPress(e);
        }
    }
}