namespace RakePlugin.Wrapper
{
    public class SolidWorksWrapper : I3DWrapper
    {

        /// <summary>
        /// Метод запуска САПР.
        /// </summary>
        public void OpenSAPR()
        {

        }

        /// <summary>
        /// Метод создания документа 3D.
        /// </summary>
        public void CreateDocument3D()
        {

        }

        /// <summary>
        /// Создание прямоугольного эскиза.
        /// </summary>
        /// <param name="width">Ширина.</param>
        /// <param name="height">Длина.</param>
        /// <param name="xCenter">Центр х.</param>
        /// <param name="yCenter">Центр у.</param>
        /// <param name="plane">Плоскость.</param>
        public void CreateRectangleSketch(
            float width,
            float height,
            float xCenter,
            float yCenter,
            Plane plane)
        {

        }

        /// <summary>
        /// Создание кругового эскиза.
        /// </summary>
        /// <param name="radius">Радиус.</param>
        /// <param name="xCenter">Центр х.</param>
        /// <param name="yCenter">Центр у.</param>
        /// <param name="plane">Плоскость.</param>
        public void CreateCircleSketch(
            float radius,
            float xCenter,
            float yCenter,
            Plane plane)
        {

        }

        /// <summary>
        /// Выдавливание эскиза.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="normal">По нормали.</param>
        public void ExtructionSketch(
            float value,
            bool normal)
        {

        }

        /// <summary>
        /// Вырезание выдавливанием.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="normal">По нормали.</param>
        public void CutExtructionSketch(
            float value,
            bool normal)
        {

        }
    }
}
