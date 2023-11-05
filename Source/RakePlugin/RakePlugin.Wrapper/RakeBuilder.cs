namespace RakePlugin.Wrapper
{
    using RakePlugin.Model;

    public class RakeBuilder
    {
        private Kompas3DWrapper _kompas3DWrapper = new Kompas3DWrapper();

        public void BuildRake(RakeParameters rakeParameters)
        {
            _kompas3DWrapper.OpenKompas();
            _kompas3DWrapper.CreateDocument3D();
            _kompas3DWrapper.CreatePart();

            // Создание рамки
            _kompas3DWrapper.InitializationSketchDefinition();
            _kompas3DWrapper.CreateFirstRectangleParam(rakeParameters.Parameters[ParameterType.WidthInsideFrame].Value, rakeParameters.Parameters[ParameterType.HeightInsideFrame].Value);
            _kompas3DWrapper.CreateSecondRectangleParam(rakeParameters.Parameters[ParameterType.FrameWidth].Value, rakeParameters.Parameters[ParameterType.FrameHeight].Value);
            _kompas3DWrapper.CreateDocument2DForTwoRectangleParam();
            _kompas3DWrapper.CreateExtrusionParam(rakeParameters.Parameters[ParameterType.FrameThickness].Value);

            // Создание бэк плейта
            _kompas3DWrapper.InitializationSketchDefinition();
            _kompas3DWrapper.CreateThirdRectangleParam(rakeParameters.Parameters[ParameterType.WidthInsideFrame].Value, rakeParameters.Parameters[ParameterType.HeightInsideFrame].Value);
            _kompas3DWrapper.CreateDocument2DForOneRectangleParam();
            _kompas3DWrapper.CreateExtrusionParam(rakeParameters.Parameters[ParameterType.BackWallThickness].Value);
        }
    }
}
