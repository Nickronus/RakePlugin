namespace RakePlugin.Model
{
    /// <summary>
    /// Тип параметра.
    /// </summary>
    public enum ParameterType
    {
        /// <summary>
        /// Ширина рабочей поверхности.
        /// </summary>
        WorkingSurfaceWidth,

        /// <summary>
        /// Ширина рабочей поверхности.
        /// </summary>
        NumberOfTeeth,

        /// <summary>
        /// Длина зубцов.
        /// </summary>
        LengthOfTeeth,

        /// <summary>
        /// Диаметр ручки.
        /// </summary>
        HandleDiameter,

        /// <summary>
        /// Длина ручки.
        /// </summary>
        HandleLength,

        /// <summary>
        /// Длина рабочей поверхности.
        /// </summary>
        WorkingSurfaceLength,

        /// <summary>
        /// Разброс зубцов.
        /// </summary>
        ToothShape,

        /// <summary>
        /// Облегченная рабочая поверхность
        /// </summary>
        LightweightWorkSurface
    }

    /// <summary>
    /// Тип зубца.
    /// </summary>
    public enum ToothShareType
    {
        /// <summary>
        /// Квадрат.
        /// </summary>
        square = 0,

        /// <summary>
        /// Круг.
        /// </summary>
        circle = 1
    }
}
