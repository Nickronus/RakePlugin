namespace RakePlugin.Model
{
    /// <summary>
    /// Валидатор
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Метод валидации
        /// </summary>
        /// <param name="parameter">Параметр валидации</param>
        /// <returns>Возвращает true, если валидация успешна</returns>
        public static bool Validate(Parameter parameter)
        {
            if (parameter.Value > parameter.MaxValue || parameter.Value < parameter.MinValue)
            {
                return false;
            }
            return true;
        }
    }
}
