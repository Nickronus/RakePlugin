namespace RakePlugin.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Параметры грабель
    /// </summary>
    public class RakeParameters
    {
        /// <summary>
        /// Словарь параметров
        /// </summary>
        public Dictionary<ParameterType, Parameter> Parameters { get; set; }
    }
}
