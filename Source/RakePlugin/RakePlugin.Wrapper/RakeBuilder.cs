namespace RakePlugin.Wrapper
{
    using RakePlugin.Model;

    /// <summary>
    /// Построитель грабель.
    /// </summary>
    public class RakeBuilder
    {
        /// <summary>
        /// Метод создания грабель.
        /// </summary>
        /// <param name="rakeParameters">Параметры грабель.</param>
        public void BuildRake(RakeParameters rakeParameters, I3DWrapper wrapper, bool isSAPRopened)
        {
            if (isSAPRopened == true)
            {
                try
                {
                    wrapper.CreateDocument3D();
                    CreateRake(rakeParameters, wrapper);

                }
                catch
                {
                    wrapper.OpenSAPR();
                    wrapper.CreateDocument3D();
                    CreateRake(rakeParameters, wrapper);
                }
            }
            else
            {
                wrapper.OpenSAPR();
                wrapper.CreateDocument3D();
                CreateRake(rakeParameters, wrapper);
            }  
        }

        /// <summary>
        /// Вспомогательный метод создания грабель.
        /// </summary>
        /// <param name="rakeParameters">Параметры грабель.</param>
        private void CreateRake(RakeParameters rakeParameters, I3DWrapper wrapper)
        {
            wrapper.CreateWorkingSurface(
                rakeParameters.Parameters[ParameterType.WorkingSurfaceWidth].Value,
                    rakeParameters.Parameters[ParameterType.WorkingSurfaceLength].Value);
            wrapper.CreateHandle(
                rakeParameters.Parameters[ParameterType.HandleDiameter].Value,
                rakeParameters.Parameters[ParameterType.HandleLength].Value);
            wrapper.CreateTeeth(
                rakeParameters.Parameters[ParameterType.WorkingSurfaceWidth].Value,
                rakeParameters.Parameters[ParameterType.LengthOfTeeth].Value,
                rakeParameters.Parameters[ParameterType.NumberOfTeeth].Value,
                rakeParameters.Parameters[ParameterType.WorkingSurfaceLength].Value,
                rakeParameters.Parameters[ParameterType.ToothShape].Value,
                rakeParameters.Parameters[ParameterType.DistanceBetweenTeeth].Value);
            if (rakeParameters.Parameters[ParameterType.LightweightWorkSurface].Value == 1)
            {
                wrapper.CreateHole(
                    rakeParameters.Parameters[ParameterType.WorkingSurfaceWidth].Value,
                    rakeParameters.Parameters[ParameterType.WorkingSurfaceLength].Value);
            }
        }
    }
}
