using System;

namespace ForwardTestTask.Classes
{
    public static class EngineTestStand
    {
        static int maxTime = 600;

        /// <summary>
        /// Тестирование двигателя на перегрев.
        /// </summary>
        /// <param name="engine">Двигатель</param>
        /// <param name="ambientTempreture">Температура внешней среды</param>
        public static void TestEngine(Engine engine, double ambientTempreture)
        {
            engine.EngineTempreture = ambientTempreture;
            for (int i = 0; i < maxTime; i++)
            {
                bool isOverheated = engine.WorkForOneSecond(ambientTempreture);
                
                if (isOverheated)
                {
                    Console.WriteLine($"Двигатель перегрелся на {i} секунде.");
                    return;
                }
            }
            Console.WriteLine($"Двигатель не перегрелся за {maxTime} секунд.");

        }
    }
}
