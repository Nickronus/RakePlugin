namespace RakePlugin.Wrapper
{
    using SolidWorks.Interop.sldworks;
    using SolidWorks.Interop.swconst;

    /// <summary>
    /// Класс оболочка методов Солид-Воркс.
    /// </summary>
    public class SolidWorksWrapper : I3DWrapper
    {
        /// <summary>
        /// Объект SW api.
        /// </summary>
        public SldWorks SolidWorks { get; private set; } = new SldWorks();

        /// <summary>
        /// Объект для работы с созданными документами.
        /// </summary>
        public ModelDoc2 ModelDocument { get; private set; }

        /// <summary>
        /// Метод запуска САПР.
        /// </summary>
        public void OpenSAPR()
        {
            SolidWorks.Visible = true;
            SolidWorks.FrameState = (int)swWindowState_e.swWindowMaximized;
        }

        /// <summary>
        /// Метод создания документа 3D.
        /// </summary>
        public void CreateDocument3D()
        {
            SolidWorks.NewPart();
            ModelDocument = SolidWorks.IActiveDoc2;
        }

        /// <summary>
        /// Создание описания плоскости эскиза.
        /// </summary>
        /// <param name="plane">Плоскость.</param>
        public string InitializationSketchDefinition(Plane plane)
        {
            if (plane == Plane.XOY)
            {
                return "Сверху";
            }

            if (plane == Plane.XOZ)
            {
                return "Спереди";
            }

            if (plane == Plane.ZOY)
            {
                return "Справа";
            }

            return "Сверху";
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
            ModelDocument.Extension.SelectByID2(InitializationSketchDefinition(plane), "PLANE", 0, 0, 0, false, 0, null, 0);
            ModelDocument.SketchManager.InsertSketch(true);
            ModelDocument.ClearSelection2(true);

            // Делим пополам, так как метод САПР строит по половине параметров
            width /= 2;
            height /= 2;

            var widthInMeters = width / 1000;
            var heightInMeters = height / 1000;

            xCenter = xCenter / 1000;
            yCenter = yCenter / 1000;
            var zCenter = 0;

            var x2 = xCenter + widthInMeters;
            var y2 = yCenter + heightInMeters;

            ModelDocument.SketchManager.CreateCenterRectangle(
                xCenter,
                yCenter,
                zCenter,
                x2,
                y2,
                zCenter);

            var feature = ModelDocument.SketchManager.ActiveSketch as Feature;
            feature.Name = "RectangleSketch";

            ModelDocument.ClearSelection2(true);
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
            ModelDocument.Extension.SelectByID2(InitializationSketchDefinition(plane), "PLANE", 0, 0, 0, false, 0, null, 0);
            ModelDocument.SketchManager.InsertSketch(true);
            ModelDocument.ClearSelection2(true);

            xCenter = xCenter / 1000;
            yCenter = yCenter / 1000;
            var zCenter = 0;
            var radiusInMeters = radius / 1000;

            ModelDocument.SketchManager.CreateCircleByRadius(xCenter, yCenter, zCenter, radiusInMeters);

            var currentFeature = (Feature)ModelDocument.SketchManager.ActiveSketch;
            currentFeature.Name = "CircleSketch";

            ModelDocument.ClearSelection2(true);
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
            // Доступ к элементу feature manager к активному эскизу.
            Feature currentFeature = ModelDocument.SketchManager.ActiveSketch as Feature;
            var sketchName = currentFeature.Name;

            var extrusionDepthInMeters = value / 1000;

            ModelDocument.Extension.SelectByID2(
                sketchName,
                "SKETCHSEGMENT",
                0,
                0,
                0,
                false,
                0,
                null,
                0);

            var feature = ModelDocument.FeatureManager.FeatureExtrusion2(
                true,
                false,
                ReversNormalValue(normal),
                0,
                0,
                extrusionDepthInMeters,
                value,
                false,
                false,
                false,
                false,
                0,
                0,
                false,
                false,
                false,
                false,
                true,
                true,
                true,
                0,
                0,
                false);

            feature.Name = "Extruct";
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
            // Доступ к элементу feature manager к активному эскизу.
            Feature currentFeature = ModelDocument.SketchManager.ActiveSketch as Feature;
            var sketchName = currentFeature.Name;

            var valueInMeters = value / 1000;

            ModelDocument.Extension.SelectByID2(
                sketchName, "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);

            var feature = ModelDocument.FeatureManager.FeatureCut3(
                true,
                false,
                ReversNormalValue(normal),
                0,
                0,
                valueInMeters,
                0,
                false,
                false,
                false,
                false,
                0,
                0,
                false,
                false,
                false,
                false,
                false,
                true,
                true,
                true,
                true,
                false,
                0,
                0,
                false);

            feature.Name = "Cut";
        }

        /// <summary>
        /// Изменяет значение нормали на противоположное.
        /// </summary>
        /// <param name="normal">Нормаль.</param>
        /// <returns>Обратную нормаль.</returns>
        private bool ReversNormalValue(bool normal)
        {
            if (normal)
            {
                return false;
            }

            return true;
        }
    }
}
