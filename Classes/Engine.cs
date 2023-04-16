namespace ForwardTestTask.Classes
{
    public abstract class Engine
    {
        /// <summary>
        /// Текущая температура двигателя
        /// </summary>
        public abstract double EngineTempreture { get; set; }

        public Engine() { }

        /// <summary>
        /// Симмуляция работы двигателя в течении 1 секунды.
        /// </summary>
        /// <param name="ambientTempreture">Температура внешней среды.</param>
        /// <returns>Перегрет ли двигатель по результатам работы.</returns>
        public abstract bool WorkForOneSecond(double ambientTempreture);
    }
}
