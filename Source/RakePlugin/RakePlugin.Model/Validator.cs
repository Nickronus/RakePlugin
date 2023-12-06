namespace RakePlugin.Model
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Валидатор.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Метод валидации.
        /// </summary>
        /// <param name="parameter">Параметр валидации.</param>
        /// <returns>Возвращает true, если валидация успешна.</returns>
        public static bool Validate(Parameter parameter)
        {
            if (parameter.Value > parameter.MaxValue || parameter.Value < parameter.MinValue)
            {
                return false;
            }

            return true;
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
        public static void ValidateValue(
            string message,
            ref Parameter parameter,
            ref TextBox textBox,
            ref Color errorColor,
            ref Color correctColor,
            ToolTip toolTip,
            ref Dictionary<string, bool> dictionaryErrors)
        {
            parameter.Value = System.Convert.ToSingle(textBox.Text);
            if (!Validate(parameter))
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
    }
}
