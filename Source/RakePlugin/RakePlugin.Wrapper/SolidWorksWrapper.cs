namespace RakePlugin.Wrapper
{
    using Kompas6API5;

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
        /// Методж создания части.
        /// </summary>
        public void CreatePart()
        {

        }

        /// <summary>
        /// Метод создания Эскиза по плоскости XOY.
        /// </summary>
        public void InitializationSketchDefinitionXOY()
        {

        }

        /// <summary>
        /// Метод создания эскиза по плосксти XOZ.
        /// </summary>
        public void InitializationSketchDefinitionXOZ()
        {

        }

        /// <summary>
        /// Создание 2D документа по одному параметру прямоугольника.
        /// </summary>
        /// <param name="ksRectangleParam">Параметрт прямоугольника.</param>
        public void CreateDocument2DForOneRectangleParam(ksRectangleParam ksRectangleParam)
        {

        }

        /// <summary>
        /// Метод создания рабочей поверхности.
        /// </summary>
        /// <param name="workingSurfaceWidth">Ширина рабочей поверхности.</param>
        /// <param name="workingSurfaceLength">Длина рабочей поверхности.</param>
        public void CreateWorkingSurface(float workingSurfaceWidth, float workingSurfaceLength)
        {

        }

        /// <summary>
        /// Метод создания ручки.
        /// </summary>
        /// <param name="handleDiameter">Диаметр ручки.</param>
        /// <param name="handleLength">Длина ручки.</param>
        public void CreateHandle(float handleDiameter, float handleLength)
        {

        }

        /// <summary>
        /// Метод создания зубца.
        /// </summary>
        /// <param name="workingSurfaceWidth">Ширина рабочей поверхности.</param>
        /// <param name="lengthOfTeeth">Длина зубца.</param>
        /// <param name="numberOfTeeth">Количество зубцов.</param>
        /// <param name="workingSurfaceLength">Длина рабочей поверхности.</param>
        /// <param name="toothShape">Вид зубца.</param>
        public void CreateTeeth(float workingSurfaceWidth, float lengthOfTeeth, float numberOfTeeth, float workingSurfaceLength, float toothShape, float distanceBetweenTeeth)
        {

        }

        /// <summary>
        /// Метод создания дырки.
        /// </summary>
        /// <param name="workingSurfaceWidth">Ширина рабочей поверхности.</param>
        /// <param name="workingSurfaceLength">Длина рабочей поверхности.</param>
        public void CreateHole(float workingSurfaceWidth, float workingSurfaceLength)
        {

        }

        /// <summary>
        /// Метод вырезания выдавливанием.
        /// </summary>
        /// <param name="normal">Нормальное направление.</param>
        /// <param name="value">Значение.</param>
        public void Cut(bool normal, int value)
        {

        }

        /// <summary>
        /// Метод создания параметра выдавливания.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="normal">Нормальное направление.</param>
        public void CreateExtrusionParam(float value, bool normal)
        {

        }

        public void CreateRectangleSketch(
    float width,
    float height,
    float xCenter,
    float yCenter,
    Plane plane)
        {

        }

        public void CreateCircleSketch(
            float radius,
            float xCenter,
            float yCenter,
            Plane plane)
        {

        }

        public void ExtructionSketch(
            float value,
            bool normal)
        {

        }

        public void CutExtructionSketch(
            float value,
            bool normal)
        {

        }
    }
}
