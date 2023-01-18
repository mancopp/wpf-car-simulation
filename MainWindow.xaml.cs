﻿using System;
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

        List<Car> CarListLeft = new List<Car>();
        List<Car> CarListRight = new List<Car>();

        public int counterLeft = -1;
        public int counterRight = -1;
        public bool directionRight;
        public bool directionBot;

        public string Speed = "1";

        private string _boundNumberCar;
        private string _boundNumberTrain;

        private bool carStop = false;


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
            int velocity = VelocityConverter(Int32.Parse(BoundNumberCar));

            if (directionRight == true)
            {
                CarListRight.Add(new Car(myCanvas));

                counterRight++;
                CarListRight[counterRight].velocity = velocity;
                CarListRight[counterRight].id = counterRight;
                CarListRight[counterRight].spawnStatic(directionRight);

                CarListRight[counterRight].spawnStatic(directionRight);
                if(counterRight > 0)
                new Thread(() => RouteRight(CarListRight[counterRight], CarListRight[counterRight-1])).Start();
                else new Thread(() => RouteRight(CarListRight[counterRight], CarListRight[counterRight])).Start();
            }

            else {
                CarListLeft.Add(new Car(myCanvas));

                counterLeft++;
                CarListLeft[counterLeft].velocity = velocity;
                CarListLeft[counterLeft].spawnStatic(directionRight);
                new Thread(() => RouteLeft(CarListLeft[counterLeft])).Start();
            }
        }

        public void RouteRight(Car car, Car car2)
        {
            for (int i = 0; i < 650; i++)
            {
                this.Dispatcher.Invoke(() => car.OffsetPos(0, 1));
               


                //if (car.id>1)
                //{

                //    if (/*(car.velocity < car2.velocity)&&*/(car.left==car2.left)&& (car.top == car2.top)) MessageBox.Show() ;

                //}

                Thread.Sleep(car.velocity);

            }

            //Check if redlight


            for (int i = 0; i < 120; i++)
            {
                this.Dispatcher.Invoke(() => car.OffsetPos(0, 1));
                Thread.Sleep(car.velocity);
            }

            

            //this.Dispatcher.Invoke(() => car.SetRotation(0));
          
            for (int i = 0; i < 85; i++)
            {
                this.Dispatcher.Invoke(() => car.OffsetPos(1, 1));
                Thread.Sleep(car.velocity);
            }
            for (int i = 0; i < 85; i++)
            {
                this.Dispatcher.Invoke(() => car.OffsetPos(1, -1));
                Thread.Sleep(car.velocity);
            }

            
            for (int i = 0; i < 550; i++)
            {
                this.Dispatcher.Invoke(() => car.OffsetPos(0, -1));
                Thread.Sleep(car.velocity);
            }
           

            for (int i = 0; i < 145; i++)
            {
                this.Dispatcher.Invoke(() => car.OffsetPos(1, -1));
                Thread.Sleep(car.velocity);
            }
           
            for (int i = 0; i < 145; i++)
            {
                this.Dispatcher.Invoke(() => car.OffsetPos(1, 1));
                Thread.Sleep(car.velocity);
            }

            for (int i = 0; i < 400; i++)
            {
                this.Dispatcher.Invoke(() => car.OffsetPos(0, 1));
                Thread.Sleep(car.velocity);
            }
          
            for (int i = 0; i < 600; i++)
            {
                this.Dispatcher.Invoke(() => car.OffsetPos(0, 1));
                Thread.Sleep(car.velocity);
            }

        }


        public void RouteLeft(Car car)
        {
           double x = 0, y = 0,r = 0;

            for (int i = 0; i < 2990; i++)
            {
                //Top

                if ((i <= 910) || (1120 < i && i <= 1660) || (2090 < i && i <= 2990)) y = 0;
                if ( (910 < i && i <= 1120) || (1760 < i && i <= 1830)) y = -1;
                if ((1660 <i && i <= 1760) || (1990 < i && i <= 2090)) y = -0.2;
                
                // Left
                if (( i <= 1000) || (1990 <i && i <= 2090)) x = -1;

                if ((1000 < i && i <= 1030) || (1830 < i && i <= 1890)) x = 0;

                if (1030 < i &&i <= 1760)  x = 1;

                if (1760 < i && i <= 1830)  x = 0.6;

                if (1890 < i && i <= 1990)  x = -0.6;

                // Rotate
            
                if ((910 < i && i <= 1000) || (1030 < i && i <= 1120)) r = 1;
                else if (1660 < i && i <= 2090) r = -0.418;
                else r = 0;

                car.position = i;
                Car car2 = car;
                if (CarListLeft.ElementAtOrDefault(car.id + 1) != null)
                {
                    car2 = CarListLeft[car.id + 1];
                }

                if (counterLeft > 1)
                {

                    if (car2.position - car.position == 60)
                    {
                        MessageBox.Show("Stop");
                        car.velocity = car2.velocity;
                    }
                }

                if (i == 325 || i == 1550 || i == 2050)
                {
                    while (true)
                    {
                        if (carStop)
                        {
                            Thread.Sleep(500);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                this.Dispatcher.Invoke(() => car.OffsetPos(y, x));
                this.Dispatcher.Invoke(() => car.Rotate(r));
                Thread.Sleep(car.velocity);
            }
            if (counterLeft > 0)
            {
                CarListLeft.RemoveAt(car.id);
                counterLeft--;
            }
        }

        public void RailRoadBot(Train train)
        {
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
            int velocity = VelocityConverter(Int32.Parse(BoundNumberTrain));

            Train train = new Train(myCanvas);

            train.velocity = velocity;
            train.directionBot = directionBot;
            train.spawnStatic();
            new Thread(() => RailRoadBot(train)).Start();
        }
    }
}
