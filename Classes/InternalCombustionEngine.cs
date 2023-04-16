using ForwardTestTask.Structures;
using System;

namespace ForwardTestTask.Classes
{
    internal class InternalCombustionEngine : Engine
    {
        private double _engineTempreture;
        public override double EngineTempreture
        {
            get => _engineTempreture;
            set => _engineTempreture = value;
        }

        private double _acceleration = 0;
        private double _currentCrankshaftRotationSpeed = 0;
        private double _currentTorque;
        private int _index = 0;

        private InternalCombustionEngineInitialSettings _initialSettings;

        public InternalCombustionEngine(InternalCombustionEngineInitialSettings initialSettings)
        {
            _initialSettings = initialSettings;
            _currentTorque = _initialSettings.torque[_index];
        }

        /// <summary>
        /// Расчет текущего ускорения изменения скорости вращения коленвала.
        /// </summary>
        private void CalculateAcceleration()
        {
            _acceleration = _currentTorque / _initialSettings.inertiaMoment;
        }

        /// <summary>
        /// Расчет текущего крутящего момента.
        /// </summary>
        private void CalculateTorque()
        {
            if (_index == _initialSettings.crankshaftRotationSpeed.Length - 1)
                _currentTorque = 0;
            if (_currentTorque == 0)
                return;

            double ratioOfScales = (_initialSettings.torque[_index + 1] - _initialSettings.torque[_index]) / (_initialSettings.crankshaftRotationSpeed[_index + 1] - _initialSettings.crankshaftRotationSpeed[_index]);
            // Отношение шкал Крутящего момента и Скорости вращения коленвала
            double deltaTorque = (_initialSettings.crankshaftRotationSpeed[_index + 1] - _currentCrankshaftRotationSpeed) * ratioOfScales;
            // Отклонение от правого (i+1) значения момента
            _currentTorque = _initialSettings.torque[_index + 1] - deltaTorque;
        }

        /// <summary>
        /// Расчет текущей скорости вращения коленвала.
        /// </summary>
        private void CalculateCrankshaftRotationSpeed()
        {
            _currentCrankshaftRotationSpeed += _acceleration;
        }

        /// <summary>
        /// Метод, отвечащющий за увеличение индекса, в зависимости от скорости вращения коленвала.
        /// </summary>
        private void Increment()
        {
            if ((_index < _initialSettings.crankshaftRotationSpeed.Length) && (_currentCrankshaftRotationSpeed > _initialSettings.crankshaftRotationSpeed[_index]))
                _index++;
        }

        /// <summary>
        /// Расчет скорости нагрева двигателя.
        /// </summary>
        /// <returns>Скорость нагрева.</returns>
        private double CalculateEngineHeat()
        {
            double torqueHeat = _currentTorque * _initialSettings.heatingSpeedTorqueCoefficient;
            double rotationSpeedHeat = Math.Pow(_currentCrankshaftRotationSpeed, 2) * _initialSettings.heatingSpeedCrankshaftSpeedCoefficient;
            return torqueHeat + rotationSpeedHeat;
        }

        /// <summary>
        /// Расчет скорости охлаждения двигателя.
        /// </summary>
        /// <param name="ambientTemperature">Температура внешней среды.</param>
        /// <returns>Скорость охлаждения</returns>
        private double CalculateEngineCooling(double ambientTemperature)
            => _initialSettings.coolingSpeedDependencyCoefficient * (ambientTemperature - _engineTempreture);

        /// <summary>
        /// Проверка двигателя на перегрев.
        /// </summary>
        /// <returns>Наличие перегрева.</returns>
        private bool CheckEngineOverheating()
            => (_engineTempreture >= _initialSettings.overheatTemperature);

        /// <summary>
        /// Расчет текущей температуры двигателя.
        /// </summary>
        /// <param name="ambientTempreture">Температура внешней среды.</param>
        /// <param name="time">Время в секундах.</param>
        private void CalculateEngineTempreture(double ambientTempreture, int time)
        {
            _engineTempreture += time * (CalculateEngineHeat() - CalculateEngineCooling(ambientTempreture));
        }

        public override bool WorkForOneSecond(double ambientTempreture)
        {
            int time = 1;
            Increment();
            CalculateAcceleration();
            CalculateCrankshaftRotationSpeed();
            CalculateTorque();
            CalculateEngineTempreture(ambientTempreture, time);
            return CheckEngineOverheating();
        }
    }
}
