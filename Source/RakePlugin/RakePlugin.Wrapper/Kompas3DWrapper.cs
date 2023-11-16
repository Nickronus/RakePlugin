namespace RakePlugin.Wrapper
{
    using Kompas6API5;
    using Kompas6Constants3D;
    using Microsoft.SqlServer.Server;
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Xml.Linq;

    /// <summary>
    /// Класс взаимодействия с API Компаса
    /// </summary>
    public class Kompas3DWrapper
    {
        /// <summary>
        /// Объект
        /// </summary>
        private KompasObject KompasObject { get; set; }

        /// <summary>
        /// 3D документ
        /// </summary>
        private ksDocument3D Document3D { get; set; }

        /// <summary>
        /// Часть
        /// </summary>
        private ksPart Part { get; set; }

        /// <summary>
        /// Эскиз 
        /// </summary>
        private ksEntity Sketch { get; set; }

        /// <summary>
        /// Описание эскиза
        /// </summary>
        private ksSketchDefinition DefinitionSketch { get; set; }

        /// <summary>
        /// 2D документ
        /// </summary>
        private ksDocument2D Document2D { get; set; }

        /// <summary>
        /// Сущность
        /// </summary>
        private ksEntity EntityExtr { get; set; }

        /// <summary>
        /// Выдавливание
        /// </summary>
        private ksBossExtrusionDefinition ExtrusionDef { get; set; }

        /// <summary>
        /// Вырезание выдавливанием
        /// </summary>
        private ksCutExtrusionDefinition CutDef { get; set; }

        /// <summary>
        /// Параметр выдавливания
        /// </summary>
        private ksExtrusionParam ExtrProp { get; set; }
        
        /// <summary>
        /// Параметр рабочей поверхности
        /// </summary>
        private ksRectangleParam WorkingSurfaceParam { get; set; }

        /// <summary>
        /// Метод запуска Компаса
        /// </summary>
        public void OpenKompas()
        {
            Type type = Type.GetTypeFromProgID("KOMPAS.Application.5");
            KompasObject = (KompasObject) Activator.CreateInstance(type);
            KompasObject.Visible = true;
            KompasObject.ActivateControllerAPI();
        }

        /// <summary>
        /// Метод создания документа 3D
        /// </summary>
        public void CreateDocument3D()
        {
            Document3D = (ksDocument3D)KompasObject.Document3D();
            Document3D.Create(false /*видимый*/, true /*деталь*/);
        }

        /// <summary>
        /// Методж создания части
        /// </summary>
        public void CreatePart()
        {
            Part = Document3D.GetPart((short)Part_Type.pTop_Part);
        }

        /// <summary>
        /// Метод создания Эскиза по плоскости XOY
        /// </summary>
        public void InitializationSketchDefinitionXOY()
        {
            Sketch = Part.NewEntity((short)Obj3dType.o3d_sketch);
            DefinitionSketch = Sketch.GetDefinition();
            DefinitionSketch.SetPlane(Part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY));
            Sketch.Create();
        }

        /// <summary>
        /// Метод создания эскиза по плосксти XOZ
        /// </summary>
        public void InitializationSketchDefinitionXOZ()
        {

            Sketch = Part.NewEntity((short)Obj3dType.o3d_sketch);
            DefinitionSketch = Sketch.GetDefinition();
            DefinitionSketch.SetPlane(Part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ));
            Sketch.Create();
        }

        /// <summary>
        /// Создание 2D документа по одному параметру прямоугольника
        /// </summary>
        /// <param name="ksRectangleParam">Параметрт прямоугольника</param>
        public void CreateDocument2DForOneRectangleParam(ksRectangleParam ksRectangleParam)
        {
            Document2D = DefinitionSketch.BeginEdit();
            Document2D.ksRectangle(ksRectangleParam, 0);
            DefinitionSketch.EndEdit();
        }

        /// <summary>
        /// Метод создания рабочей поверхности
        /// </summary>
        /// <param name="workingSurfaceWidth">Ширина рабочей поверхности</param>
        /// <param name="workingSurfaceLength">Длина рабочей поверхности</param>
        public void CreateWorkingSurface(float workingSurfaceWidth, float workingSurfaceLength)
        {
            CreatePart();
            InitializationSketchDefinitionXOY();
            Document2D = DefinitionSketch.BeginEdit();
            Document2D.ksLineSeg((-workingSurfaceWidth) / 2, -15, workingSurfaceWidth / 2, -15, 1);
            Document2D.ksLineSeg((workingSurfaceWidth) / 2, -15, workingSurfaceWidth / 2, 15, 1);
            Document2D.ksLineSeg((workingSurfaceWidth) / 2, 15, (-workingSurfaceWidth) / 2, 15, 1);
            Document2D.ksLineSeg((-workingSurfaceWidth) / 2, 15, (-workingSurfaceWidth) / 2, -15, 1);
            Document2D.ksLineSeg((workingSurfaceWidth) / 2, -15, (-workingSurfaceWidth) / 2, 15, 2);
            Document2D.ksLineSeg((-workingSurfaceWidth) / 2, -15, workingSurfaceWidth / 2, 15, 2);
            Document2D.ksPoint(0, 0, 0);
            DefinitionSketch.EndEdit();
            DefinitionSketch.angle = 180;
            Sketch.Update();
            CreatePart();
            CreateExtrusionParam(workingSurfaceLength, true);
        }

        /// <summary>
        /// Метод создания ручки
        /// </summary>
        /// <param name="handleDiameter">Диаметр ручки</param>
        /// <param name="handleLength">Длина ручки</param>
        public void CreateHandle(float handleDiameter, float handleLength)
        {
            InitializationSketchDefinitionXOY();
            Document2D = DefinitionSketch.BeginEdit();
            Document2D.ksCircle(0, 0, handleDiameter / 2, 1);
            DefinitionSketch.angle = 180;
            DefinitionSketch.EndEdit();
            Sketch.Update();
            CreateExtrusionParam(handleLength, false);
        }

        /// <summary>
        /// Метод создания зубца
        /// </summary>
        /// <param name="workingSurfaceWidth">Ширина рабочей поверхности</param>
        /// <param name="lengthOfTeeth">Длина зубца</param>
        /// <param name="numberOfTeeth">Количество зубцов</param>
        /// <param name="workingSurfaceLength">Длина рабочей поверхности</param>
        /// <param name="toothShape">Вид зубца</param>
        public void CreateTeeth(float workingSurfaceWidth, float lengthOfTeeth, float numberOfTeeth, float workingSurfaceLength, float toothShape)
        {    
            float distanceBetweenTeeth = ((workingSurfaceWidth / 10 - numberOfTeeth) / (numberOfTeeth - 1) * 10) + 10;
            for(int i = 0; i < numberOfTeeth; i++)
            {
                CreatePart();
                InitializationSketchDefinitionXOZ();
                Document2D = DefinitionSketch.BeginEdit();

                if(toothShape == 0)
                {
                    Document2D.ksLineSeg(workingSurfaceWidth / 2 - i * distanceBetweenTeeth - 10, workingSurfaceLength - 10, workingSurfaceWidth / 2 - i * distanceBetweenTeeth, workingSurfaceLength - 10, 1);
                    Document2D.ksLineSeg(workingSurfaceWidth / 2 - i * distanceBetweenTeeth, workingSurfaceLength - 10, workingSurfaceWidth / 2 - i * distanceBetweenTeeth, workingSurfaceLength, 1);
                    Document2D.ksLineSeg(workingSurfaceWidth / 2 - i * distanceBetweenTeeth, workingSurfaceLength, workingSurfaceWidth / 2 - i * distanceBetweenTeeth - 10, workingSurfaceLength, 1);
                    Document2D.ksLineSeg(workingSurfaceWidth / 2 - i * distanceBetweenTeeth - 10, workingSurfaceLength, workingSurfaceWidth / 2 - i * distanceBetweenTeeth - 10, workingSurfaceLength - 10, 1);
                    Document2D.ksLineSeg(workingSurfaceWidth / 2 - i * distanceBetweenTeeth, workingSurfaceLength - 10, workingSurfaceWidth / 2 - i * distanceBetweenTeeth - 10, workingSurfaceLength, 2);
                    Document2D.ksLineSeg(workingSurfaceWidth / 2 - i * distanceBetweenTeeth - 10, workingSurfaceLength - 10, workingSurfaceWidth / 2 - i * distanceBetweenTeeth, workingSurfaceLength, 2);
                }
                if (toothShape == 1)
                {
                    Document2D.ksCircle(workingSurfaceWidth / 2 - 5 - i * (distanceBetweenTeeth), workingSurfaceLength - 5, 5, 1);
                }
                
                Document2D.ksPoint(0, 0, 0);
                DefinitionSketch.EndEdit();
                DefinitionSketch.angle = 180;
                Sketch.Update();
                CreatePart();
                CreateExtrusionParam(lengthOfTeeth + 15, true);
            }
        }

        /// <summary>
        /// Метод вырезания выдавливанием
        /// </summary>
        /// <param name="normal">Нормальное направление</param>
        /// <param name="value">Значение</param>
        private void Cut(bool normal, int value)
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
        /// Метод создания дырки
        /// </summary>
        /// <param name="workingSurfaceWidth">Ширина рабочей поверхности</param>
        /// <param name="workingSurfaceLength">Длина рабочей поверхности</param>
        public void CreateHole(float workingSurfaceWidth, float workingSurfaceLength)
        {
            CreatePart();
            InitializationSketchDefinitionXOZ();
            Document2D = DefinitionSketch.BeginEdit();
            Document2D.ksLineSeg((-workingSurfaceWidth) / 2 + 10, 10, workingSurfaceWidth / 2 - 10, 10, 1);
            Document2D.ksLineSeg((workingSurfaceWidth) / 2 - 10, 10, workingSurfaceWidth / 2 - 10, workingSurfaceLength - 10, 1);
            Document2D.ksLineSeg((workingSurfaceWidth) / 2 - 10, workingSurfaceLength - 10, (-workingSurfaceWidth) / 2 + 10, workingSurfaceLength - 10, 1);
            Document2D.ksLineSeg((-workingSurfaceWidth) / 2 + 10, workingSurfaceLength - 10, (-workingSurfaceWidth) / 2 + 10, 10, 1);
            Document2D.ksLineSeg((workingSurfaceWidth) / 2 - 10, 10, (-workingSurfaceWidth) / 2 + 10, workingSurfaceLength - 10, 2);
            Document2D.ksLineSeg((-workingSurfaceWidth) / 2 + 10, 10, workingSurfaceWidth / 2 - 10, workingSurfaceLength - 10, 2);
            Document2D.ksPoint(0, 0, 0);
            DefinitionSketch.EndEdit();
            DefinitionSketch.angle = 180;
            Sketch.Update();
            CreatePart();

            Cut(true, 15);
            Cut(false, 15);
        }

        /// <summary>
        /// Метод создания параметра выдавливания
        /// </summary>
        /// <param name="value">Значение</param>
        /// <param name="normal">Нормальное направление</param>
        public void CreateExtrusionParam(float value, bool normal)
        {
            EntityExtr = (ksEntity)Part.NewEntity((short)Obj3dType.o3d_bossExtrusion);
            ExtrusionDef = (ksBossExtrusionDefinition)EntityExtr.GetDefinition();
            ExtrProp = (ksExtrusionParam)ExtrusionDef.ExtrusionParam();
            ExtrusionDef.SetSketch(Sketch);
            if(normal)
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
    }
}