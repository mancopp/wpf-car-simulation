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

namespace wpf_car_simulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Car car;

        List<Car> CarList = new List<Car>();

        public int counter=-1;

        public MainWindow()
        {
            InitializeComponent();

            
         
            
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
            CarList.Add(new Car(myCanvas));

            counter++;
            new Thread(() => StandardRoute(CarList[counter])).Start();
            CarList[counter].spawnStatic();
           
            //this.car = CarList[0];

            //this.DataContext = CarList[0];
        }

        public void StandardRoute(Car car)
        {
            for (int i = 0; i < 650; i++)
            {
                this.Dispatcher.Invoke(() => car.OffsetPos(0, 1));
                Thread.Sleep(1);
            }

            //Check if redlight
           

            for (int i = 0; i < 120; i++)
            {
                this.Dispatcher.Invoke(() => car.OffsetPos(0, 1));
                Thread.Sleep(1);
            }

            

            //this.Dispatcher.Invoke(() => car.SetRotation(0));
          
            for (int i = 0; i < 85; i++)
            {
                this.Dispatcher.Invoke(() => car.OffsetPos(1, 1));
                Thread.Sleep(1);
            }
            for (int i = 0; i < 85; i++)
            {
                this.Dispatcher.Invoke(() => car.OffsetPos(1, -1));
                Thread.Sleep(1);
            }

            
            for (int i = 0; i < 550; i++)
            {
                this.Dispatcher.Invoke(() => car.OffsetPos(0, -1));
                Thread.Sleep(1);
            }
           

            for (int i = 0; i < 145; i++)
            {
                this.Dispatcher.Invoke(() => car.OffsetPos(1, -1));
                Thread.Sleep(1);
            }
           
            for (int i = 0; i < 145; i++)
            {
                this.Dispatcher.Invoke(() => car.OffsetPos(1, 1));
                Thread.Sleep(1);
            }

            for (int i = 0; i < 400; i++)
            {
                this.Dispatcher.Invoke(() => car.OffsetPos(0, 1));
                Thread.Sleep(1);
            }
          
            for (int i = 0; i < 600; i++)
            {
                this.Dispatcher.Invoke(() => car.OffsetPos(0, 1));
                Thread.Sleep(1);
            }

        }
    }
}
