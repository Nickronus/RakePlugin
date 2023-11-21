namespace RakePlugin.Wrapper
{
    using RakePlugin.Model;

    /// <summary>
    /// Построитель грабель.
    /// </summary>
    public class RakeBuilder
    {
        /// <summary>
        /// Класс взаимодействия с API Компаса.
        /// </summary>
        public Kompas3DWrapper kompas3DWrapper = new Kompas3DWrapper();

        /// <summary>
        /// Метод создания грабель.
        /// </summary>
        /// <param name="rakeParameters">Параметры грабель.</param>
        public void BuildRake(RakeParameters rakeParameters)
        {
            try
            {
                kompas3DWrapper.CreateDocument3D();
                CreateRake(rakeParameters);

            }
            catch
            {
                kompas3DWrapper.OpenKompas();
                kompas3DWrapper.CreateDocument3D();
                CreateRake(rakeParameters);
            }
        }

        /// <summary>
        /// Вспомогательный метод создания грабель.
        /// </summary>
        /// <param name="rakeParameters">Параметры грабель.</param>
        private void CreateRake(RakeParameters rakeParameters)
        {
            kompas3DWrapper.CreateWorkingSurface(
                rakeParameters.Parameters[ParameterType.WorkingSurfaceWidth].Value,
                    rakeParameters.Parameters[ParameterType.WorkingSurfaceLength].Value);
            kompas3DWrapper.CreateHandle(
                rakeParameters.Parameters[ParameterType.HandleDiameter].Value,
                rakeParameters.Parameters[ParameterType.HandleLength].Value);
            kompas3DWrapper.CreateTeeth(
                rakeParameters.Parameters[ParameterType.WorkingSurfaceWidth].Value,
                rakeParameters.Parameters[ParameterType.LengthOfTeeth].Value,
                rakeParameters.Parameters[ParameterType.NumberOfTeeth].Value,
                rakeParameters.Parameters[ParameterType.WorkingSurfaceLength].Value,
                rakeParameters.Parameters[ParameterType.ToothShape].Value);
            if (rakeParameters.Parameters[ParameterType.LightweightWorkSurface].Value == 1)
            {
                kompas3DWrapper.CreateHole(
                    rakeParameters.Parameters[ParameterType.WorkingSurfaceWidth].Value,
                    rakeParameters.Parameters[ParameterType.WorkingSurfaceLength].Value);
            }
        }
    }
}
