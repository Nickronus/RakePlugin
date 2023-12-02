namespace RakePlugin.Wrapper
{
    using Kompas6API5;
    using Kompas6Constants3D;
    using System;

    /// <summary>
    /// Класс взаимодействия с API Компаса.
    /// </summary>
    public class Kompas3DWrapper : I3DWrapper
    {
        /// <summary>
        /// Объект.
        /// </summary>
        private KompasObject KompasObject { get; set; }

        /// <summary>
        /// 3D документ.
        /// </summary>
        private ksDocument3D Document3D { get; set; }

        /// <summary>
        /// Часть.
        /// </summary>
        private ksPart Part { get; set; }

        /// <summary>
        /// Эскиз .
        /// </summary>
        private ksEntity Sketch { get; set; }

        /// <summary>
        /// Описание эскиза.
        /// </summary>
        private ksSketchDefinition DefinitionSketch { get; set; }

        /// <summary>
        /// 2D документ.
        /// </summary>
        private ksDocument2D Document2D { get; set; }

        /// <summary>
        /// Сущность.
        /// </summary>
        private ksEntity EntityExtr { get; set; }

        /// <summary>
        /// Выдавливание.
        /// </summary>
        private ksBossExtrusionDefinition ExtrusionDef { get; set; }

        /// <summary>
        /// Вырезание выдавливанием.
        /// </summary>
        private ksCutExtrusionDefinition CutDef { get; set; }

        /// <summary>
        /// Параметр выдавливания.
        /// </summary>
        private ksExtrusionParam ExtrProp { get; set; }

        /// <summary>
        /// Параметр рабочей поверхности.
        /// </summary>
        private ksRectangleParam WorkingSurfaceParam { get; set; }

        /// <summary>
        /// Метод запуска САПР.
        /// </summary>
        public void OpenSAPR()
        {
            Type type = Type.GetTypeFromProgID("KOMPAS.Application.5");
            KompasObject = (KompasObject)Activator.CreateInstance(type);
            KompasObject.Visible = true;
            KompasObject.ActivateControllerAPI();
        }

        /// <summary>
        /// Метод создания документа 3D.
        /// </summary>
        public void CreateDocument3D()
        {
            Document3D = (ksDocument3D)KompasObject.Document3D();
            Document3D.Create(false /*видимый*/, true /*деталь*/);
        }

        /// <summary>
        /// Методж создания части.
        /// </summary>
        public void CreatePart()
        {
            Part = Document3D.GetPart((short)Part_Type.pTop_Part);
        }

        /// <summary>
        /// Метод вырезания выдавливанием.
        /// </summary>
        /// <param name="normal">Нормальное направление.</param>
        /// <param name="value">Значение.</param>
        public void Cut(bool normal, float value)
        {
            EntityExtr = (ksEntity)Part.NewEntity((short)Obj3dType.o3d_cutExtrusion);
            CutDef = (ksCutExtrusionDefinition)EntityExtr.GetDefinition();
            ExtrProp = (ksExtrusionParam)CutDef.ExtrusionParam();
            CutDef.SetSketch(Sketch);
            CutDef.cut = true;

            if (normal)
            {
                ExtrProp.direction = (short)Direction_Type.dtNormal;
                ExtrProp.typeNormal = (short)End_Type.etBlind;
                ExtrProp.depthNormal = value;
            }
            else
            {
                ExtrProp.direction = (short)Direction_Type.dtReverse;
                ExtrProp.typeReverse = (short)End_Type.etBlind;
                ExtrProp.depthReverse = value;
            }

            EntityExtr.Create();
        }

        /// <summary>
        /// Метод создания параметра выдавливания.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="normal">Нормальное направление.</param>
        public void CreateExtrusionParam(float value, bool normal)
        {
            EntityExtr = (ksEntity)Part.NewEntity((short)Obj3dType.o3d_bossExtrusion);
            ExtrusionDef = (ksBossExtrusionDefinition)EntityExtr.GetDefinition();
            ExtrProp = (ksExtrusionParam)ExtrusionDef.ExtrusionParam();
            ExtrusionDef.SetSketch(Sketch);
            if (normal)
            {
                ExtrProp.direction = (short)Direction_Type.dtNormal;
                ExtrProp.typeNormal = (short)End_Type.etBlind;
                ExtrProp.depthNormal = value;
            }
            else
            {
                ExtrProp.direction = (short)Direction_Type.dtReverse;
                ExtrProp.typeReverse = (short)End_Type.etBlind;
                ExtrProp.depthReverse = value;
            }

            EntityExtr.Create();
        }

        /// <summary>
        /// Создание описания плоскости эскиза.
        /// </summary>
        /// <param name="plane">Плоскость.</param>
        public void InitializationSketchDefinition(Plane plane)
        {
            Sketch = Part.NewEntity((short)Obj3dType.o3d_sketch);
            DefinitionSketch = Sketch.GetDefinition();
            if (plane == Plane.XOY)
            {
                DefinitionSketch.SetPlane(Part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY));
            }

            if (plane == Plane.XOZ)
            {
                DefinitionSketch.SetPlane(Part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ));
            }

            if (plane == Plane.ZOY)
            {
                DefinitionSketch.SetPlane(Part.GetDefaultEntity((short)Obj3dType.o3d_planeYOZ));
            }

            Sketch.Create();
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
            CreatePart();
            InitializationSketchDefinition(plane);

            Document2D = DefinitionSketch.BeginEdit();
            Document2D.ksLineSeg(xCenter + (width / 2), yCenter + (height / 2), xCenter + (width / 2), yCenter - (height / 2), 1);
            Document2D.ksLineSeg(xCenter + (width / 2), yCenter + (height / 2), xCenter - (width / 2), yCenter + (height / 2), 1);
            Document2D.ksLineSeg(xCenter - (width / 2), yCenter - (height / 2), xCenter - (width / 2), yCenter + (height / 2), 1);
            Document2D.ksLineSeg(xCenter - (width / 2), yCenter - (height / 2), xCenter + (width / 2), yCenter - (height / 2), 1);

            Document2D.ksLineSeg(xCenter + (width / 2), yCenter + (height / 2), xCenter - (width / 2), yCenter - (height / 2), 2);
            Document2D.ksLineSeg(xCenter + (width / 2), yCenter - (height / 2), xCenter - (width / 2), yCenter + (height / 2), 2);

            Document2D.ksPoint(0, 0, 0);
            DefinitionSketch.EndEdit();
            DefinitionSketch.angle = 180;
            Sketch.Update();
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
            CreatePart();
            InitializationSketchDefinition(plane);

            Document2D = DefinitionSketch.BeginEdit();
            Document2D.ksCircle(xCenter, yCenter, radius, 1);
            DefinitionSketch.angle = 180;
            DefinitionSketch.EndEdit();
            Sketch.Update();
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
            CreatePart();
            CreateExtrusionParam(value, normal);
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
            CreatePart();
            Cut(normal, value);
        }
    }
}