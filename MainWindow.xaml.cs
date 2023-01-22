using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace wpf_car_simulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyOnPropertyChanged
    {
        public Car car;
        private Random rnd = new Random();

        List<Car> CarListLeft = new List<Car>();
        List<Car> CarListRight = new List<Car>();

        public int counterLeft = -1;
        public int counterRight = -1;
        public bool directionRight;
        public bool directionBot;

        public int[] stopLeft = new int[]
        {
            310,
            1550,
            2050
        }; 
        
        public int[] stopRight = new int[]
        {
            670,
            950,
            2250
        };

        public int stopPoint = 310;
        public int stopPoint2 = 1550;
        public int stopPoint3 = 2050;

        public bool LeftCarCanBeSpawned = true;
        public bool RightCarCanBeSpawned = true;
        private bool _trainCanBeSpawned = true;

        private string _boundNumberCar;
        private string _boundNumberTrain;

        private bool carStop = false;
        private bool simulationStarted = false;

        public bool TrainCanBeSpawned
        {
            get { return _trainCanBeSpawned; }
            set
            {
                if (_trainCanBeSpawned != value)
                {
                    _trainCanBeSpawned = value;
                    OnPropertyChanged();
                }
            }
        }

        public string BoundNumberCar
        {
            get { return _boundNumberCar; }
            set
            {


                if (_boundNumberCar != value)
                {
                    _boundNumberCar = value;
                    OnPropertyChanged();

                }
            }
        }

        public string BoundNumberTrain
        {
            get { return _boundNumberTrain; }
            set
            {
                if (_boundNumberTrain != value)
                {
                    _boundNumberTrain = value;
                    OnPropertyChanged();

                }
            }
        }

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        public void Output(string str)
        {
            MessageBox.Show(str);
        }

        private int VelocityConverter(int num)
        {
            int multiplier = num - 1;
            return (num + 9 - 2 * multiplier);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Int32.Parse(BoundNumberCar) > 10 || Int32.Parse(BoundNumberCar) < 1)
                {
                    MessageBox.Show("Invalid velocity, should be 1-10");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid velocity, should be 1-10");
                return;
            }

            int velocity = VelocityConverter(Int32.Parse(BoundNumberCar));

            if (directionRight == true)
            {
                StartRightCarThr(velocity);
            }
            else
            {
                StartLeftCarThr(velocity);
            }
        }

        public void RouteRight(Car car, Car nextCar)
        {
            double x = 0, y = 0, r = 0, k1 = 0, k2 = 0;

            for (int i = 0; i < 12000; i++)
            {
                if ((i <= 790) || (960 < i && i <= 1520) || (1830 < i && i <= 2700)) y = 0;
                if ((790 <= i && i <= 960) || (1640 < i && i <= 1710)) y = 1;
                if ((1520 <= i && i <= 1640) || (1710 < i && i <= 1830)) y = 0.95;

                if ((i <= 875) || (1830 < i && i <= 2700)) x = 1;
                else if (875 < i && i <= 1520) x = -1;
                else if (1640 < i && i <= 1710) x = 0;
                else if (1520 < i && i <= 1640)
                {
                    k1 += 0.002;
                    x = -1 - k1;
                }
                else
                {
                    k2 -= 0.002;
                    x = 1 - k2;
                }

                if ((i <= 790) || (960 < i && i <= 1520) || (1640 < i && i <= 1710) || (1830 < i && i <= 2700)) r = 0;
                else if ((1520 < i && i <= 1640) || (1710 < i && i <= 1830)) r = -0.75;
                else r = 1.058;

                car.position = i;

                if (nextCar != null)
                {
                    if (nextCar.position - car.position <= 81)
                    {
                        car.velocity = nextCar.velocity;
                        Thread.Sleep(100);
                    }

                    if (nextCar.position - car.position == 71)
                    {
                        while (true)
                        {
                            if (carStop)
                            {
                                Thread.Sleep(100);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                if (stopRight.Contains(i))
                {
                    while (true)
                    {

                        if (carStop)
                        {
                            Thread.Sleep(100);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                this.Dispatcher.Invoke(() =>
                {
                    car.Rotate(r);
                    car.OffsetPos(y, x);
                });

                Thread.Sleep(car.velocity);
            }
            CarListLeft.Remove(car);
        }


        public void RouteLeft(Car car, Car nextCar)
        {
            double x = 0, y = 0, r = 0;

            for (int i = 0; i < 12990; i++)
            {
                //Top
                if ((i <= 910) || (1120 < i && i <= 1660) || (2090 < i && i <= 2990)) y = 0;
                if ((910 < i && i <= 1120) || (1760 < i && i <= 1830)) y = -1;
                if ((1660 < i && i <= 1760) || (1990 < i && i <= 2090)) y = -0.2;

                // Left
                if ((i <= 1000) || (1990 < i && i <= 2090)) x = -1;
                if ((1000 < i && i <= 1030) || (1830 < i && i <= 1890)) x = 0;
                if (1030 < i && i <= 1760) x = 1;
                if (1760 < i && i <= 1830) x = 0.6;
                if (1890 < i && i <= 1990) x = -0.6;

                // Rotate
                if ((910 < i && i <= 1000) || (1030 < i && i <= 1120)) r = 1;
                else if (1660 < i && i <= 2090) r = -0.418;
                else r = 0;

                car.position = i;

                if (nextCar != null)
                {
                    if (nextCar.position - car.position <= 81)
                    { 
                        car.velocity = nextCar.velocity;
                        Thread.Sleep(100);
                    }

                    if (nextCar.position - car.position == 71)
                    {
                        while (true)
                        {
                            if (carStop)
                            {
                                Thread.Sleep(100);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                if (stopLeft.Contains(i))
                {
                    while (true)
                    {

                        if (carStop)
                        {
                            Thread.Sleep(100);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                this.Dispatcher.Invoke(() =>
                {
                    car.OffsetPos(y, x);
                    car.Rotate(r);
                });

                Thread.Sleep(car.velocity);
            }
            CarListLeft.Remove(car);
        }



        public void RailRoadBot(Train train)
        {
            this.Dispatcher.Invoke(() => btnSpawnTrain.IsEnabled = false);
            TrainCanBeSpawned = false;
            carStop = true;

            Thread.Sleep(500);
            int offset;
            if (train.directionBot)
            {
                offset = 1;
            }
            else
            {
                offset = -1;
            }
            for (int i = 0; i < 1200; i++)
            {
                this.Dispatcher.Invoke(() => train.OffsetPos(offset, 0));
                Thread.Sleep(train.velocity);
            }

            carStop = false;
            this.Dispatcher.Invoke(() => btnSpawnTrain.IsEnabled = true);
            TrainCanBeSpawned = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private void Left_Checked(object sender, RoutedEventArgs e)
        {
            directionRight = false;
        }

        private void Right_Checked(object sender, RoutedEventArgs e)
        {
            directionRight = true;
        }

        private void Top_Checked(object sender, RoutedEventArgs e)
        {
            directionBot = false;
        }

        private void Bot_Checked(object sender, RoutedEventArgs e)
        {
            directionBot = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Int32.Parse(BoundNumberTrain) > 10 || Int32.Parse(BoundNumberTrain) < 1)
                {
                    MessageBox.Show("Invalid velocity, should be 1-10");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid velocity, should be 1-10");
                return;
            }

            int velocity = VelocityConverter(Int32.Parse(BoundNumberTrain));

            Train train = new Train(myCanvas);

            train.velocity = velocity;
            train.directionBot = directionBot;
            train.spawnStatic();
            new Thread(() => RailRoadBot(train)).Start();
        }

        private void BtnStartSimulation(object sender, RoutedEventArgs e)
        {
            simulationStarted = !simulationStarted;
            if (simulationStarted)
            {
                new Thread(() => Simulate()).Start();
                btnSimulation.Content = "Stop Simulation";
            }
            else
            {
                btnSimulation.Content = "Start Simulation";
            }
        }

        private void StartRightCarThr(int vel)
        {
            if (CarListRight.ElementAtOrDefault(CarListRight.Count - 1) != null && Canvas.GetLeft(CarListRight[CarListRight.Count - 1].carSprite) < 150) return;

            Car currCar = new Car(myCanvas);
            CarListRight.Add(currCar);

            currCar.velocity = vel;
            currCar.spawnStatic(true);
            new Thread(() => RouteRight(currCar, CarListRight.ElementAtOrDefault(CarListRight.Count - 2))).Start();
        }  
        
        private void StartLeftCarThr(int vel)
        {
            if (CarListLeft.ElementAtOrDefault(CarListLeft.Count - 1) != null && Canvas.GetLeft(CarListLeft[CarListLeft.Count - 1].carSprite) > 1000) return;

            Car currCar = new Car(myCanvas);
            CarListLeft.Add(currCar);

            currCar.velocity = vel;
            currCar.spawnStatic(false);
            new Thread(() => RouteLeft(currCar, CarListLeft.ElementAtOrDefault(CarListLeft.Count - 2))).Start();
        }

        private void Simulate()
        {
            while (simulationStarted)
            {
                int velocity = rnd.Next(1, 11); // 1-10
                int type = rnd.Next(1, 11); // 1-10
                bool way = Convert.ToBoolean(rnd.Next(2));
                if(type < 8)
                {
                    if (way)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            StartRightCarThr(velocity);
                        });
                    }
                    else
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            StartLeftCarThr(velocity);
                        });
                    }
                }
                else
                {
                    if (TrainCanBeSpawned)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            Train train = new Train(myCanvas);
                            train.velocity = velocity;
                            train.directionBot = way;
                            train.spawnStatic();
                            new Thread(() => RailRoadBot(train)).Start();
                        });
                    }
                }
                Thread.Sleep(rnd.Next(400, 4000));
            }
        }
    }
}
