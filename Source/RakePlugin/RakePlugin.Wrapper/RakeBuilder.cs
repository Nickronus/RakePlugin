namespace RakePlugin.Wrapper
{
    using RakePlugin.Model;
    using System;

    /// <summary>
    /// Построитель грабель
    /// </summary>
    public class RakeBuilder
    {
        /// <summary>
        /// Класс взаимодействия с API Компаса
        /// </summary>
        private Kompas3DWrapper _kompas3DWrapper = new Kompas3DWrapper();

        /// <summary>
        /// Вспомогательный метод создания грабель
        /// </summary>
        /// <param name="rakeParameters">Параметры грабель</param>
        private void CreateRake(RakeParameters rakeParameters)
        {
            _kompas3DWrapper.CreateWorkingSurface(rakeParameters.Parameters[ParameterType.WorkingSurfaceWidth].Value,
                    rakeParameters.Parameters[ParameterType.WorkingSurfaceLength].Value);
            _kompas3DWrapper.CreateHandle(rakeParameters.Parameters[ParameterType.HandleDiameter].Value,
                rakeParameters.Parameters[ParameterType.HandleLength].Value);
            _kompas3DWrapper.CreateTeeth(rakeParameters.Parameters[ParameterType.WorkingSurfaceWidth].Value,
                rakeParameters.Parameters[ParameterType.LengthOfTeeth].Value,
                rakeParameters.Parameters[ParameterType.NumberOfTeeth].Value,
                rakeParameters.Parameters[ParameterType.WorkingSurfaceLength].Value,
                rakeParameters.Parameters[ParameterType.ToothShape].Value);
            if (rakeParameters.Parameters[ParameterType.LightweightWorkSurface].Value == 1)
            {
                _kompas3DWrapper.CreateHole(rakeParameters.Parameters[ParameterType.WorkingSurfaceWidth].Value,
                    rakeParameters.Parameters[ParameterType.WorkingSurfaceLength].Value);
            }
        }
    

        /// <summary>
        /// Метод создания грабель
        /// </summary>
        /// <param name="rakeParameters">Параметры грабель</param>
        public void BuildRake(RakeParameters rakeParameters)
        {
            try
            {
                _kompas3DWrapper.CreateDocument3D();
                CreateRake(rakeParameters);

            }
            catch
            {
                _kompas3DWrapper.OpenKompas();
                _kompas3DWrapper.CreateDocument3D();
                CreateRake(rakeParameters);
            }
        }
    }
}
