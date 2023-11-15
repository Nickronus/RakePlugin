namespace RakePlugin.Wrapper
{
    using Kompas6API5;
    using Kompas6Constants3D;
    using Microsoft.SqlServer.Server;
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Xml.Linq;

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
        private ksCutExtrusionDefinition CutDef { get; set; }
        private ksExtrusionParam ExtrProp { get; set; }
        private ksRectangleParam WorkingSurfaceParam { get; set; }
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
        }

        public void InitializationSketchDefinitionXOY()
        {
            Sketch = Part.NewEntity((short)Obj3dType.o3d_sketch);
            DefinitionSketch = Sketch.GetDefinition();
            DefinitionSketch.SetPlane(Part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY));
            Sketch.Create();
        }

        public void InitializationSketchDefinitionXOZ()
        {

            Sketch = Part.NewEntity((short)Obj3dType.o3d_sketch);
            DefinitionSketch = Sketch.GetDefinition();
            DefinitionSketch.SetPlane(Part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ));
            Sketch.Create();
        }

        public void CreateDocument2DForOneRectangleParam(ksRectangleParam ksRectangleParam)
        {
            Document2D = DefinitionSketch.BeginEdit();
            Document2D.ksRectangle(ksRectangleParam, 0);
            DefinitionSketch.EndEdit();
        }

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