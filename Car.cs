using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace wpf_car_simulation
{
    public class Car
    {
        private readonly string imgPath = "Resources/car.png";
        private Canvas mainCanvas;
        private int id = 1;
        private int velocity = 1;
        private bool directedToRight = true;
        private Image carSprite;
        private int _rotateTransformAngle = 0;

        public Car(Canvas canvas)
        {
            this.mainCanvas = canvas;
        }

        public void spawnStatic()
        {
            int top = 290;
            int left = 10;

            System.Windows.Controls.Image carSprite = new System.Windows.Controls.Image();
            carSprite.Source = new BitmapImage(new Uri(@"/wpf-car-simulation;component/Resources/car.png", UriKind.Relative));
            carSprite.Height = 50;

            this.carSprite = carSprite;

            SetRotation(-90);

            this.mainCanvas.Children.Add(this.carSprite);
            Canvas.SetTop(this.carSprite, top);
            Canvas.SetLeft(this.carSprite, left);
        }

        public void startThread()
        {

            int top = 180;
            int left = 30;

            System.Windows.Controls.Image carSprite = new System.Windows.Controls.Image();
            carSprite.Source = new BitmapImage(new Uri(@"/wpf-car-simulation;component/Resources/car.png", UriKind.Relative));
            carSprite.Width = 80;
            carSprite.Height = 80;
            RotateTransform rt = new RotateTransform(-90);
            rt.CenterX = 25;
            rt.CenterY = 25;

            carSprite.RenderTransform = rt;

            this.mainCanvas.Children.Add(carSprite);
            Canvas.SetTop(carSprite, top);
            Canvas.SetLeft(carSprite, left);

        }

        public void SetPos(int top, int left)
        {
            Canvas.SetTop(carSprite, top);
            Canvas.SetLeft(carSprite, left);
        }

        public void OffsetPos(int topOff, int leftOff)
        {
            Canvas.SetTop(carSprite, Canvas.GetTop(carSprite) + topOff);
            Canvas.SetLeft(carSprite, Canvas.GetLeft(carSprite) + leftOff);
        }

        public void SetRotation(int rot)
        {
            _rotateTransformAngle = rot;
            RotateTransform rt = new RotateTransform(_rotateTransformAngle);
            rt.CenterX = 25;
            rt.CenterX = 25;
            carSprite.RenderTransform = rt;
        }

        public void Rotate(int rot)
        {
            _rotateTransformAngle += rot;
            RotateTransform rt = new RotateTransform(_rotateTransformAngle);
            rt.CenterX = 25;
            rt.CenterY = 25;
            carSprite.RenderTransform = rt;
        }
    }
}
