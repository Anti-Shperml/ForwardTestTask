using ForwardTestTask.Classes;
using ForwardTestTask.Structures;
using System;

namespace ForwardTestTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InternalCombustionEngineInitialSettings settings = new InternalCombustionEngineInitialSettings();
            settings.inertiaMoment = 10.0;
            settings.torque = new double[] { 20, 75, 100, 105, 75, 0 };
            settings.crankshaftRotationSpeed = new double[] { 0, 75, 150, 105, 75, 0 };
            settings.overheatTemperature = 110.0;
            settings.heatingSpeedTorqueCoefficient = 0.01;
            settings.heatingSpeedCrankshaftSpeedCoefficient = 0.0001;
            settings.coolingSpeedDependencyCoefficient = 0.1;
            Engine engine = new InternalCombustionEngine(settings);

            double ambientTempreture;
            while (true)
            {
                Console.WriteLine("Введите значение температуры внешней среды:");
                string input = Console.ReadLine();
                
                if (Double.TryParse(input, out ambientTempreture))
                    break;

                Console.WriteLine("Допущена ошибка при вводе температуры внешней среды. Попробуйте еще раз!\n");
            }
            EngineTestStand.TestEngine(engine, ambientTempreture);

        }
    }
}
