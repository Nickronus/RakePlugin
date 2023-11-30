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
        /// Создание рабочей поверхности.
        /// </summary>
        /// <param name="workingSurfaceWidth">Ширина рабочей поверхности.</param>
        /// <param name="workingSurfaceLength">Длина рабочей поверхности.</param>
        void CreateWorkingSurface(float workingSurfaceWidth, float workingSurfaceLength);

        /// <summary>
        /// Создание ручки.
        /// </summary>
        /// <param name="handleDiameter">Диаметр ручки.</param>
        /// <param name="handleLength">Длина ручки.</param>
        void CreateHandle(float handleDiameter, float handleLength);

        /// <summary>
        /// Создание зубца.
        /// </summary>
        /// <param name="workingSurfaceWidth">Ширина рабочей поверхности.</param>
        /// <param name="lengthOfTeeth">Длина зубца.</param>
        /// <param name="numberOfTeeth">Количество зубцов.</param>
        /// <param name="workingSurfaceLength">Длина рабочей поверхности.</param>
        /// <param name="toothShape">Вид зубца.</param>
        /// <param name="distanceBetweenTeeth">Расстояние между зубцами.</param>
        void CreateTeeth(
                float workingSurfaceWidth,
                float lengthOfTeeth,
                float numberOfTeeth,
                float workingSurfaceLength,
                float toothShape,
                float distanceBetweenTeeth);

        /// <summary>
        /// Создание дырки.
        /// </summary>
        /// <param name="workingSurfaceWidth">Ширина рабочей поверхности.</param>
        /// <param name="workingSurfaceLength">Длина рабочей поверхности.</param>
        void CreateHole(float workingSurfaceWidth, float workingSurfaceLength);
}
}
