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

        public MainWindow()
        {
            DataContext = this;

            InitializeComponent();
            //  



        }

        public Car car;

        List<Car> CarListLeft = new List<Car>();
        List<Car> CarListRight = new List<Car>();

        public int counterLeft = -1;
        public int counterRight = -1;
        public bool direction = true;

        public string Speed="1";





        private string _boundNumber;

        public string BoundNumber
        {
            get { return _boundNumber; }
            set
            {


                if (_boundNumber != value)
                {
                _boundNumber = value;
                    OnPropertyChanged();
                
                }
            }


        }




        public void Output(string str)
        {
            MessageBox.Show(str);
        }

        private void moveCarThr()
        {
            for (int i = 0; i < 360*100; i++)
            {
               

              this.Dispatcher.Invoke(() => car.Rotate(1));
               


                Thread.Sleep(1);
            }
        }

      


            private void Button_Click(object sender, RoutedEventArgs e)
        {
            


            int velocity = Int32.Parse(BoundNumber);

            if (direction == true)
            {
                CarListRight.Add(new Car(myCanvas));


                counterRight++;
                CarListRight[counterRight].velocity = velocity;
                CarListRight[counterRight].id = counterRight;

                CarListRight[counterRight].spawnStatic(direction);
                if(counterRight > 0)
                new Thread(() => StandardRoute(CarListRight[counterRight], CarListRight[counterRight-1])).Start();
                else new Thread(() => StandardRoute(CarListRight[counterRight], CarListRight[counterRight])).Start();
            }

            else {
                CarListLeft.Add(new Car(myCanvas));

                counterLeft++;

                CarListLeft[counterLeft].velocity = velocity;
                CarListLeft[counterLeft].spawnStatic(direction);

                new Thread(() => StandardRoute2(CarListLeft[counterLeft])).Start();

            }
















        }

        public void StandardRoute(Car car, Car car2)
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


        public void StandardRoute2(Car car)
        {
           double x =0, y = 0,r=0;

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
                else r= 0;

                ////1000<i<= y=-1 ; x=0;



                this.Dispatcher.Invoke(() => car.OffsetPos(y, x));
                this.Dispatcher.Invoke(() => car.Rotate(r));
                Thread.Sleep(car.velocity);




            }


            //for (int i = 0; i < 910; i++)
            //{
            //    this.Dispatcher.Invoke(() => car.OffsetPos(0, -1));
            //    Thread.Sleep(car.velocity);
            //       r =0
            //}

            ////910<i<=1000 y=-1 ; x=-1; r=
            ///  //       r =1
            //for (int i = 0; i < 90; i++)
            //{
            //    this.Dispatcher.Invoke(() => car.OffsetPos(-1, -1));
            //    this.Dispatcher.Invoke(() => car.Rotate(1));

            //    Thread.Sleep(car.velocity + 1);

            //       r =0
            //}

            ////1000<i<=1030 y=-1 ; x=0;
            ///  //       r =1
            //for (int i = 0; i < 30; i++)
            //{
            //    this.Dispatcher.Invoke(() => car.OffsetPos(-1, 0));
            //    Thread.Sleep(car.velocity + 1);
            //}
            ////1030<i<=1120 y=-1 ; x=1;
            ///  //       r =1
            //for (int i = 0; i < 90; i++)
            //{
            //    this.Dispatcher.Invoke(() => car.OffsetPos(-1, 1));
            //    this.Dispatcher.Invoke(() => car.Rotate(1));
            //    Thread.Sleep(car.velocity + 1);

            //}
            ////1120<i<=1660 y=0 ; x=1;
            ///  //       r =0
            //for (int i = 0; i < 540; i++)
            //{
            //    this.Dispatcher.Invoke(() => car.OffsetPos(0, 1));
            //    Thread.Sleep(car.velocity);
            //}
            ////1660<i<=1760 y=-0.2; x=1;

            //       r =-0.418
            //for (int i = 0; i < 100; i++)
            //{
            //    this.Dispatcher.Invoke(() => car.OffsetPos(-0.2, 1));
            //    this.Dispatcher.Invoke(() => car.Rotate(-0.418));
            //    Thread.Sleep(car.velocity+1);

            //}
            ////1760<i<=1830 y=-1; x=0.6;
            /// //       r =-0.418
            //for (int i = 0; i < 70; i++)
            //{
            //    this.Dispatcher.Invoke(() => car.OffsetPos(-1, 0.6));
            //    this.Dispatcher.Invoke(() => car.Rotate(-0.418));
            //    Thread.Sleep(car.velocity+1);

            //}
            ////1830<i<=1890 y=-1; x=0;
            /// //       r =-0.418
            //for (int i = 0; i < 60; i++)
            //{
            //    this.Dispatcher.Invoke(() => car.OffsetPos(-1, 0));
            //  this.Dispatcher.Invoke(() => car.Rotate(-0.418));
            //    Thread.Sleep(car.velocity+1);

            //}
            ////1890<i<=1990 y=-1; x=0.6;
            //       r =-0.418
            //for (int i = 0; i < 100; i++)
            //{
            //    this.Dispatcher.Invoke(() => car.OffsetPos(-1, -0.6));
            //    this.Dispatcher.Invoke(() => car.Rotate(-0.418));
            //    Thread.Sleep(car.velocity+1);
            //}
            ////1990<i<=2090 y=-0.2; x=-1;
            /// //       r =-0.418
            //for (int i = 0; i < 100; i++)
            //{
            //    this.Dispatcher.Invoke(() => car.OffsetPos(-0.2, -1));
            //    this.Dispatcher.Invoke(() => car.Rotate(-0.418));
            //    Thread.Sleep(car.velocity+1);

            //}


            ////2090<i<=2990 y=0; x=-1;

            //for (int i = 0; i < 900; i++)
            //{
            //    this.Dispatcher.Invoke(() => car.OffsetPos(0, -1));
            //    Thread.Sleep(car.velocity);

            //       r =0
            //}


        }




        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
          direction = false;
        }

        private void Right_Checked(object sender, RoutedEventArgs e)
        {
            direction = true;
        }
    }
}
