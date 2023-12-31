﻿namespace RakePlugin.View
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
        /// Параметры грабель.
        /// </summary>
        private readonly RakeParameters _parameters = new RakeParameters();

        /// <summary>
        /// Построитель грабель.
        /// </summary>
        private readonly RakeBuilder _builder = new RakeBuilder();

        /// <summary>
        /// Расстояне между зубцами.
        /// </summary>
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
        /// Длина ручки.
        /// </summary>
        private Parameter _handleLength = new Parameter
        {
            MaxValue = 2000,
            MinValue = 1000,
            Value = 1000
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
        /// Цвет корректного значения.
        /// </summary>
        private Color _correctСolor = Color.White;

        /// <summary>
        /// Цвет ошибочного значения.
        /// </summary>
        private Color _errorColor = Color.LightPink;

        /// <summary>
        /// Словарь ошибок.
        /// </summary>
        private Dictionary<string, bool> _dictionaryErrors = new Dictionary<string, bool>()
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
        /// Длина зубца.
        /// </summary>
        private Parameter _lengthOfTeeth = new Parameter
        {
            MaxValue = 200,
            MinValue = 50,
            Value = 100
        };

        /// <summary>
        /// Инициализация главного окна.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Экзкепляр Kompas3DWrapper.
        /// </summary>
        private Kompas3DWrapper _Kompas3DWrapper { get; set; }

        /// <summary>
        /// Экземпляр SolidWorksWrapper.
        /// </summary>
        private SolidWorksWrapper _SolidWorksWrapper { get; set; }

        /// <summary>
        /// Всплывающее окно.
        /// </summary>
        private ToolTip _toolTip { get; set; } = new ToolTip();

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
                if (_Kompas3DWrapper == null)
                {
                    _Kompas3DWrapper = new Kompas3DWrapper();
                }

                return _Kompas3DWrapper;
            }

            if (saprComboBox.Text == "SolidWorks")
            {
                if (_SolidWorksWrapper == null)
                {
                    _SolidWorksWrapper = new SolidWorksWrapper();
                }

                return _SolidWorksWrapper;
            }

            return new Kompas3DWrapper();
        }

        /// <summary>
        /// Метод, определяющий САПР.
        /// </summary>
        /// <returns>САПР.</returns>
        private string WhatCAD()
        {
            if (saprComboBox.Text == "Компас 3D")
            {
                return CAD.Kompas;
            }

            if (saprComboBox.Text == "SolidWorks")
            {
                return CAD.SolidWorks;
            }

            return "";
        }

        /// <summary>
        /// Валидировать значение.
        /// </summary>
        /// <param name="parameter">Параметр.</param>
        /// <param name="textBox">TextBox.</param>
        /// <param name="errorColor">Цвет ошибки.</param>
        /// <param name="correctColor">Цвет корректности.</param>
        /// <param name="toolTip">ToolTip.</param>
        /// <param name="dictionaryErrors">Словарь ошибок.</param>
        private static void ValidateValue(
            string message,
            ref Parameter parameter,
            ref TextBox textBox,
            ref Color errorColor,
            ref Color correctColor,
            ToolTip toolTip,
            ref Dictionary<string, bool> dictionaryErrors)
        {
            parameter.Value = System.Convert.ToSingle(textBox.Text);
            if (!Validator.Validate(parameter))
            {
                textBox.BackColor = errorColor;
                toolTip.SetToolTip(textBox, message);
                dictionaryErrors[nameof(textBox)] = false;
            }
            else
            {
                textBox.BackColor = correctColor;
                toolTip.SetToolTip(textBox, "");
                dictionaryErrors[nameof(textBox)] = true;
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

            _builder.BuildRake(_parameters, WrapperFactory(), WhatCAD());
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
        /// Создать разброс ширины рабочей поверхности.
        /// </summary>
        private void MakeWorkingSurfaceSpread()
        {
            _workingSurfaceWidth.MaxValue = (((_numberOfTeeth.Value - 1) * 11) + 1) * 10;
            _workingSurfaceWidth.MinValue = (((_numberOfTeeth.Value - 1) * 2) + 1) * 10;

            if (_workingSurfaceWidth.MaxValue > 1010)
            {
                _workingSurfaceWidth.MaxValue = 1010;
            }

            if (_workingSurfaceWidth.MinValue < 120)
            {
                _workingSurfaceWidth.MinValue = 120;
            }
        }

        /// <summary>
        /// Метод создания расстояния между зубцами.
        /// </summary>
        private void MakeDistanceBetweenTeeth()
        {
            _distanceBetweenTeeth.Value = (((_workingSurfaceWidth.Value / 10) - _numberOfTeeth.Value) / (_numberOfTeeth.Value - 1) * 10) + 10;
        }

        private void WorkingSurfaceWidthTextBox_TextChanged(object sender, EventArgs e)
        {
            if (WorkingSurfaceWidthTextBox.Text != "")
            {
                WorkingSurfaceWidthTextBox.BackColor = _correctСolor;
                _numberOfTeeth.MinValue = 2;
                _numberOfTeeth.MaxValue = 51;

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
                _workingSurfaceWidth.MaxValue = 1010;
                _workingSurfaceWidth.MinValue = 120;

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
                ValidateValue(
                    "Длина зубьев должна быть в диапазоне от 50 до 200 мм",
                    ref _lengthOfTeeth,
                    ref LengthOfTeethTextBox,
                    ref _errorColor,
                    ref _correctСolor,
                    _toolTip,
                    ref _dictionaryErrors);
                CheckFormOnErrors();
            }
        }

        private void HandleDiameterTextBox_TextChanged(object sender, EventArgs e)
        {
            if (HandleDiameterTextBox.Text != "")
            {
                ValidateValue(
                    "Диаметр ручки должен быть в диапазоне от 20 до 30 мм",
                    ref _handleDiameter,
                    ref HandleDiameterTextBox,
                    ref _errorColor,
                    ref _correctСolor,
                    _toolTip,
                    ref _dictionaryErrors);
                CheckFormOnErrors();
            }
        }

        private void HandleLengthTextBox_TextChanged(object sender, EventArgs e)
        {
            if (HandleLengthTextBox.Text != "")
            {
                ValidateValue(
                    "Длина ручки должна быть в диапазоне от 1000 до 2000 мм",
                    ref _handleLength,
                    ref HandleLengthTextBox,
                    ref _errorColor,
                    ref _correctСolor,
                    _toolTip,
                    ref _dictionaryErrors);
                CheckFormOnErrors();
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