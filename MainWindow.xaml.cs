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
        public MainWindow()
        {
            InitializeComponent();
            canvas.Focus();

            Thread thr = new Thread(move);
            thr.Start();

        }
            //
        private void move()
        {

        for(int i = 0; i < 100; i++)
            {
               
                this.Dispatcher.Invoke((Action)(() =>
                {
                    Canvas.SetLeft(car1, Canvas.GetLeft(car1) + 1);

                }));
                Thread.Sleep(100);

            }

       

        }




    }
}
