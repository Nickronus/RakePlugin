namespace RakePlugin.Wrapper
{
    using Kompas6API5;
    using Kompas6Constants3D;
    using System;

    public class Kompas3DWrapper
    {
        private KompasObject KompasObject { get; set; }
        private ksDocument3D Document3D { get; set; }
        private ksPart Part { get; set; }
        private ksEntity Sketch { get; set; }
        private ksSketchDefinition DefinitionSketch { get; set; }
        private ksDocument2D Document2D { get; set; }
        private ksEntity EntityExtr { get; set; }
        private ksBossExtrusionDefinition ExtrusionDef { get; set; }
        private ksExtrusionParam ExtrProp { get; set; }
        private ksRectangleParam FirstRectangleParam { get; set; }
        private ksRectangleParam SecondRectangleParam { get; set; }
        private ksRectangleParam ThirdRectangleParam { get; set; }



        public void OpenKompas()
        {
            Type type = Type.GetTypeFromProgID("KOMPAS.Application.5");
            KompasObject = (KompasObject) Activator.CreateInstance(type);
            KompasObject.Visible = true;
            KompasObject.ActivateControllerAPI();
        }

        public void CreateDocument3D()
        {
            Document3D = (ksDocument3D)KompasObject.Document3D();
            Document3D.Create(false /*видимый*/, true /*деталь*/);
        }

        public void CreatePart()
        {
            Part = Document3D.GetPart((short)Part_Type.pTop_Part);
            //document3D.fileName = "PhotoFrame";
        }

        public void InitializationSketchDefinition()
        {
            Sketch = Part.NewEntity((short)Obj3dType.o3d_sketch);
            DefinitionSketch = Sketch.GetDefinition();
            DefinitionSketch.SetPlane(Part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY));
            Sketch.Create();
        }

        public void CreateFirstRectangleParam(float firstParameter, float secondParameter)
        {
            float centreInnerX = (firstParameter / 2) * -1;
            float centreInnerY = (secondParameter / 2) * -1;
            FirstRectangleParam = (ksRectangleParam)KompasObject.GetParamStruct(91);
            FirstRectangleParam.x = centreInnerX;
            FirstRectangleParam.y = centreInnerY;
            FirstRectangleParam.width = firstParameter;
            FirstRectangleParam.height = secondParameter;
            FirstRectangleParam.style = 1;
        }

        public void CreateSecondRectangleParam(float firstParameter, float secondParameter)
        {
            float centreInnerX = (firstParameter / 2) * -1;
            float centreInnerY = (secondParameter / 2) * -1;
            SecondRectangleParam = (ksRectangleParam)KompasObject.GetParamStruct(91);
            SecondRectangleParam.x = centreInnerX;
            SecondRectangleParam.y = centreInnerY;
            SecondRectangleParam.width = firstParameter;
            SecondRectangleParam.height = secondParameter;
            SecondRectangleParam.style = 1;
        }

        public void CreateThirdRectangleParam(float firstParameter, float secondParameter)
        {
            float centreInnerX = (firstParameter / 2) * -1;
            float centreInnerY = (secondParameter / 2) * -1;
            ThirdRectangleParam = (ksRectangleParam)KompasObject.GetParamStruct(91);
            ThirdRectangleParam.x = centreInnerX;
            ThirdRectangleParam.y = centreInnerY;
            ThirdRectangleParam.width = firstParameter;
            ThirdRectangleParam.height = secondParameter;
            ThirdRectangleParam.style = 1;
        }

        public void CreateDocument2DForTwoRectangleParam()
        {
            Document2D = DefinitionSketch.BeginEdit();
            Document2D.ksRectangle(FirstRectangleParam, 0);
            Document2D.ksRectangle(SecondRectangleParam, 0);
            DefinitionSketch.EndEdit();
        }

        public void CreateDocument2DForOneRectangleParam()
        {
            Document2D = DefinitionSketch.BeginEdit();
            Document2D.ksRectangle(ThirdRectangleParam, 0);
            DefinitionSketch.EndEdit();
        }

        public void CreateExtrusionParam(float firstParameter)
        {
            EntityExtr = (ksEntity)Part.NewEntity((short)Obj3dType.o3d_bossExtrusion);
            ExtrusionDef = (ksBossExtrusionDefinition)EntityExtr.GetDefinition();
            ExtrProp = (ksExtrusionParam)ExtrusionDef.ExtrusionParam(); // интерфейс структуры параметров выдавливания
            ExtrusionDef.SetSketch(Sketch); // эскиз операции выдавливания
            ExtrProp.direction = (short)Direction_Type.dtNormal;      // направление выдавливания (прямое)
            ExtrProp.typeNormal = (short)End_Type.etBlind;      // тип выдавливания (строго на глубину)
            ExtrProp.depthNormal = firstParameter; // глубина выдавливания
            EntityExtr.Create();                // создадим операцию
        }
    }
}