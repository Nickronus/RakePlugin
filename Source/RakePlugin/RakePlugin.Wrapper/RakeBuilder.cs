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
            /*
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
            */
            //_________________

            MakeWorkingSurface(rakeParameters, wrapper);
            MakeHandle(rakeParameters, wrapper);
            MakeTeeth(rakeParameters, wrapper);
            if (rakeParameters.Parameters[ParameterType.LightweightWorkSurface].Value == 1)
            {
                MakeHole(rakeParameters,wrapper);
            }
        }

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

        private void MakeHole(RakeParameters rakeParameters, I3DWrapper wrapper)
        {
            wrapper.CreateRectangleSketch(
                rakeParameters.Parameters[ParameterType.WorkingSurfaceWidth].Value - 20,
                rakeParameters.Parameters[ParameterType.WorkingSurfaceLength].Value - 20,
                0,
                rakeParameters.Parameters[ParameterType.WorkingSurfaceLength].Value / 2,
                Plane.XOZ);
            wrapper.CutExtructionSketch(15, true);
            wrapper.CutExtructionSketch(15, false);
        }
    }
}
