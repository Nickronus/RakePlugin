namespace RakePlugin.Wrapper
{
    /// <summary>
    /// Интерфейс построителя.
    /// </summary>
    public interface I3DWrapper
    {
        /// <summary>
        /// Открытие САПР.
        /// </summary>
        void OpenSAPR();

        /// <summary>
        /// Создание документа.
        /// </summary>
        void CreateDocument3D();

        /// <summary>
        /// Создание прямоугольного эскиза.
        /// </summary>
        /// <param name="width">Ширина.</param>
        /// <param name="height">Длина.</param>
        /// <param name="xCenter">Центр х.</param>
        /// <param name="yCenter">Центр у.</param>
        /// <param name="plane">Плоскость.</param>
        void CreateRectangleSketch(
            float width,
            float height,
            float xCenter,
            float yCenter,
            Plane plane);

        /// <summary>
        /// Создание кругового эскиза.
        /// </summary>
        /// <param name="radius">Радиус.</param>
        /// <param name="xCenter">Центр х.</param>
        /// <param name="yCenter">Центр у.</param>
        /// <param name="plane">Плоскость.</param>
        void CreateCircleSketch(
            float radius,
            float xCenter,
            float yCenter,
            Plane plane);

        /// <summary>
        /// Выдавливание эскиза.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="normal">По нормали.</param>
        void ExtructionSketch(
            float value,
            bool normal);

        /// <summary>
        /// Вырезание выдавливанием.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="normal">По нормали.</param>
        void CutExtructionSketch(
            float value,
            bool normal);
}
}
