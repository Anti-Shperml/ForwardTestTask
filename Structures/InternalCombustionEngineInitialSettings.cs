namespace ForwardTestTask.Structures
{
    public struct InternalCombustionEngineInitialSettings
    {
        /// <summary>
        /// Момент инерции двигателя.
        /// </summary>
        public double inertiaMoment;
        /// <summary>
        /// Крутящий момент двигателя.
        /// </summary>
        public double[] torque;
        /// <summary>
        /// Скорость вращения двигателя.
        /// </summary>
        public double[] crankshaftRotationSpeed;
        /// <summary>
        /// Температура перегрева.
        /// </summary>
        public double overheatTemperature;
        /// <summary>
        /// Коэффициент зависимости скорости нагрева от крутящего момента.
        /// </summary>
        public double heatingSpeedTorqueCoefficient;
        /// <summary>
        /// Коэффициент зависимости скорости нагрева от скорости вращения коленвала.
        /// </summary>
        public double heatingSpeedCrankshaftSpeedCoefficient;
        /// <summary>
        /// Коэффициент зависимости скорости охлаждения от температуры двигателя и окружающей среды.
        /// </summary>
        public double coolingSpeedDependencyCoefficient;
    }
}
