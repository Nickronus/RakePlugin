namespace RakePlugin.Wrapper
{
    using RakePlugin.Model;
    using System.Diagnostics;

    /// <summary>
    /// Построитель грабель.
    /// </summary>
    public class RakeBuilder
    {
        /// <summary>
        /// Метод создания грабель.
        /// </summary>
        /// <param name="rakeParameters">Параметры грабель.</param>
        public void BuildRake(RakeParameters rakeParameters, I3DWrapper wrapper, string nameCAD)
        {
            if (Process.GetProcessesByName(nameCAD).Length > 0 && !string.IsNullOrEmpty(Process.GetProcessesByName(nameCAD)[0].MainWindowTitle))
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
            MakeWorkingSurface(rakeParameters, wrapper);
            MakeHandle(rakeParameters, wrapper);
            MakeTeeth(rakeParameters, wrapper);
            if (rakeParameters.Parameters[ParameterType.LightweightWorkSurface].Value == 1)
            {
                MakeHole(rakeParameters, wrapper);
            }
        }

        /// <summary>
        /// Создание рабочей поверхности.
        /// </summary>
        /// <param name="rakeParameters">Параметры грабель.</param>
        /// <param name="wrapper">Оболочка.</param>
        private void MakeWorkingSurface(RakeParameters rakeParameters, I3DWrapper wrapper)
        {
            wrapper.CreateRectangleSketch(
                rakeParameters.Parameters[ParameterType.WorkingSurfaceWidth].Value,
                30,
                0,
                0,
                Plane.XOY);
            wrapper.ExtructionSketch(
                rakeParameters.Parameters[ParameterType.WorkingSurfaceLength].Value,
                true);
        }

        /// <summary>
        /// Создание ручки.
        /// </summary>
        /// <param name="rakeParameters">Параметры грабель.</param>
        /// <param name="wrapper">Оболочка.</param>
        private void MakeHandle(RakeParameters rakeParameters, I3DWrapper wrapper)
        {
            wrapper.CreateCircleSketch(
                rakeParameters.Parameters[ParameterType.HandleDiameter].Value / 2,
                0,
                0,
                Plane.XOY);
            wrapper.ExtructionSketch(
                rakeParameters.Parameters[ParameterType.HandleLength].Value,
                false);
        }

        /// <summary>
        /// Создание зубцов.
        /// </summary>
        /// <param name="rakeParameters">Параметры грабель.</param>
        /// <param name="wrapper">Оболочка.</param>
        private void MakeTeeth(RakeParameters rakeParameters, I3DWrapper wrapper)
        {
            for (int i = 0; i < rakeParameters.Parameters[ParameterType.NumberOfTeeth].Value; ++i)
            {
                float x = (rakeParameters.Parameters[ParameterType.WorkingSurfaceWidth].Value / 2) - 5 -
                        (i * (0 + rakeParameters.Parameters[ParameterType.DistanceBetweenTeeth].Value));

                if (rakeParameters.Parameters[ParameterType.ToothShape].Value == 0)
                {
                    wrapper.CreateRectangleSketch(
                        10,
                        10,
                        x,
                        rakeParameters.Parameters[ParameterType.WorkingSurfaceLength].Value - 5,
                        Plane.XOZ);
                }

                if (rakeParameters.Parameters[ParameterType.ToothShape].Value == 1)
                {
                    wrapper.CreateCircleSketch(
                        5,
                        x,
                        rakeParameters.Parameters[ParameterType.WorkingSurfaceLength].Value - 5,
                        Plane.XOZ);
                }

                wrapper.ExtructionSketch(rakeParameters.Parameters[ParameterType.LengthOfTeeth].Value, true);
            }
        }

        /// <summary>
        /// Создание дырки.
        /// </summary>
        /// <param name="rakeParameters">Параметры.</param>
        /// <param name="wrapper">Оболочник.</param>
        private void MakeHole(RakeParameters rakeParameters, I3DWrapper wrapper)
        {
            wrapper.CreateRectangleSketch(
                rakeParameters.Parameters[ParameterType.WorkingSurfaceWidth].Value - 20,
                rakeParameters.Parameters[ParameterType.WorkingSurfaceLength].Value - 20,
                0,
                rakeParameters.Parameters[ParameterType.WorkingSurfaceLength].Value / 2,
                Plane.XOZ);
            wrapper.CutExtructionSketch(15, true);
            wrapper.CreateRectangleSketch(
                rakeParameters.Parameters[ParameterType.WorkingSurfaceWidth].Value - 20,
                rakeParameters.Parameters[ParameterType.WorkingSurfaceLength].Value - 20,
                0,
                rakeParameters.Parameters[ParameterType.WorkingSurfaceLength].Value / 2,
                Plane.XOZ);
            wrapper.CutExtructionSketch(15, false);
        }
    }
}
