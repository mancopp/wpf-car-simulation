/*
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
        
        public static void Start(MainWindow mw)
        {
            mainWindow = mw;
            Thread Car1 = new Thread(() => CarThread(0, 10, true));
            Thread Train1 = new Thread(() => TrainThread(2, 5, false));
            Car1.Start();
            Train1.Start();
        }

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
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

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

        Rectangle[] recArray = new Rectangle[100];

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

        private Rectangle _current;
        private Point _initialPoint;

        public static void car_spaw(Canvas MyCanvas)
        {
            int width = 10;
            int height = 10;
            int top = 130;
            int left = -570;

            Rectangle rec = new Rectangle()
            {
                Width = width,
                Height = height,
                Fill = Brushes.Green,
                Stroke = Brushes.Red,
                StrokeThickness = 2,
            };

            MyCanvas.Children.Add(rec);
            Canvas.SetTop(rec, top);
            Canvas.SetLeft(rec, left);
        }

        /*
        public static void s(Canvas MyCanvas)
        {
            List<Rectangle> recList = new List<Rectangle>();


            tGenerator(MyCanvas, recList);

            new Thread(() => tMove(MyCanvas, recList)).Start();
        }

        public static void tGenerator(Canvas MyCanvas, List<Rectangle> recList)
        {

            recList.Add(new Rectangle { Width = 10, Height = 10, Fill = Brushes.Red });
            MyCanvas.Children.Add(recList[0]);
            Canvas.SetTop(recList[0], 130);
            Canvas.SetLeft(recList[0], -570);
        }


        private static void tMove(Canvas MyCanvas, List<Rectangle> recList)
        {
            for (int i = 0; i < 100; i++)
            {
                Dispatcher.Invoke(new Action(() => { Canvas.SetLeft(recList[0], Canvas.GetLeft(recList[0]) + 1); ; }));
                {
                    this.Invoke(new IntDelegate(SetTextboxTextSafe), new object[] { result });
                }
                Thread.Sleep(100);
            }
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

            //After car reaches it's destination, it removes itself and thread stops
        }

        public static void TrainThread(int id, int length, bool toBot)
        {
            //In: direction (left to right, right to left)

            //After train reaches it's destination, it removes itself and thread stops
        }
    }
}
