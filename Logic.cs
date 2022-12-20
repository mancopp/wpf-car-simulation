using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace wpf_car_simulation
{
    public class Logic
    {
        private static MainWindow mainWindow;
        private static List<Thread> threads;

        private static int carThreadsLastId = 0;
        private static int trainThreadsLastId = 0;

        private static Random random = new Random();
        
        public static bool stopSimulation = false;
        /*
        public static void Start(MainWindow mw)
        {
            mainWindow = mw;
            Thread Car1 = new Thread(() => CarThread(0, 10, true));
            Thread Train1 = new Thread(() => TrainThread(2, 5, false));
            Car1.Start();
            Train1.Start();
        }
        */

        public static void StartSimulation(MainWindow mw)
        {
            mainWindow = mw;

            int minDelta = 2000;
            int maxDelta = 6000;

            while (!stopSimulation)
            {
                ThreadGenerator(random.NextDouble() >= 0.25);
                Thread.Sleep(random.Next(minDelta, maxDelta));
            }

            stopSimulation = false;
        }

        private static void ThreadGenerator(bool threadIsCar)
        {
            if (threadIsCar)
            {
                new Thread(() => CarThread(carThreadsLastId++, random.Next(1, 20), random.NextDouble() >= 0.5)).Start();
            }
            else
            {
                new Thread(() => TrainThread(trainThreadsLastId++, random.Next(1, 5), random.NextDouble() >= 0.5)).Start();
            }
        }

        public static void CarThread(int id, int velocity, bool toRight)
        {
            //In: velocity (some int/float value), direction (left to right, right to left)
            mainWindow.Output($"Car spawned(id: {id}, velocity: {velocity}, direction: left to rigth = {toRight})");
            //After car reaches it's destination, it removes itself and thread stops
        }

        public static void TrainThread(int id, int length, bool toBot)
        {
            //In: direction (left to right, right to left)
            mainWindow.Output($"Train spawned(id: {id}, length: {length} direction: top to bottom = {toBot})");
            //After train reaches it's destination, it removes itself and thread stops
        }
    }
}
