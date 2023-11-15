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

            _kompas3DWrapper.CreateWorkingSurface(rakeParameters.Parameters[ParameterType.WorkingSurfaceWidth].Value,
                rakeParameters.Parameters[ParameterType.WorkingSurfaceLength].Value);
            _kompas3DWrapper.CreateHandle(rakeParameters.Parameters[ParameterType.HandleDiameter].Value,
                rakeParameters.Parameters[ParameterType.HandleLength].Value);
            _kompas3DWrapper.CreateTeeth(rakeParameters.Parameters[ParameterType.WorkingSurfaceWidth].Value,
                rakeParameters.Parameters[ParameterType.LengthOfTeeth].Value,
                rakeParameters.Parameters[ParameterType.NumberOfTeeth].Value,
                rakeParameters.Parameters[ParameterType.WorkingSurfaceLength].Value,
                rakeParameters.Parameters[ParameterType.ToothShape].Value);
            if(rakeParameters.Parameters[ParameterType.LightweightWorkSurface].Value == 1)
            {
                _kompas3DWrapper.CreateHole(rakeParameters.Parameters[ParameterType.WorkingSurfaceWidth].Value,
                    rakeParameters.Parameters[ParameterType.WorkingSurfaceLength].Value);
            }
        }
    }
}
