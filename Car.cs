using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace wpf_car_simulation
{
    public class Car
    {
        private readonly string imgPath = "Resources/car.png";
        private int id = 1;
        private int velocity = 1;
        private bool directedToRight = true;

        public Car(Canvas myCanvas)
        {
            int top = 245;
            int left = 30;

            System.Windows.Controls.Image carSprite = new System.Windows.Controls.Image();
            carSprite.Source = new BitmapImage(new Uri(@"/wpf-car-simulation;component/Resources/car.png", UriKind.Relative));
            carSprite.Width = 80;
            carSprite.Height = 80;
            RotateTransform rt = new RotateTransform(-90);
            rt.CenterX = 40;
            rt.CenterY = 40;

            carSprite.RenderTransform = rt;

            myCanvas.Children.Add(carSprite);
            Canvas.SetTop(carSprite, top);
            Canvas.SetLeft(carSprite, left);
        }
    }
}
