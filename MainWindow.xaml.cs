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

        public MainWindow()
        {
            InitializeComponent();
            Car car = new Car(myCanvas);
            car.spawnStatic();
            this.car = car;

            this.DataContext = car;
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
            new Thread(() => moveCarThr()).Start();
        }

        public void StandardRoute()
        {
            for (int i = 0; i < 650; i++)
            {
                this.Dispatcher.Invoke(() => car.OffsetPos(0, 1));
                Thread.Sleep(1);
            }

            //Check if redlight
            Thread.Sleep(500);

            for (int i = 0; i < 120; i++)
            {
                this.Dispatcher.Invoke(() => car.OffsetPos(0, 1));
                Thread.Sleep(1);
            }

            Thread.Sleep(500);

            this.Dispatcher.Invoke(() => car.SetRotation(0));
            Thread.Sleep(1000);
            this.Dispatcher.Invoke(() => car.OffsetPos(70, 70));
        }
    }
}
