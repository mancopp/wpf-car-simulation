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
        public int id = 1;
        public int velocity = 1;
        private Image carSprite;
        private double _rotateTransformAngle = 0;
        public double top;
        public double left;
        public int position;
        public int carstop=325;
    


        public Car(Canvas canvas)
        {
            this.mainCanvas = canvas;
        }

        public void spawnStatic(bool direction)
        {


            if (direction == true)
            {
                top = 290;
                left = 10;
                

            }
            else
            {

                top = 685;
                left = 1150;


            }

            System.Windows.Controls.Image carSprite = new System.Windows.Controls.Image();
            carSprite.Source = new BitmapImage(new Uri(@"/wpf-car-simulation;component/" + imgPath, UriKind.Relative));
            carSprite.Height = 50;

            this.carSprite = carSprite;

            //    SetRotation(-90);

            SetRotation(-90);

            if (direction == true)
            {
                SetRotation(-90);

            }
            else
            {

                SetRotation(90);


            }


            this.mainCanvas.Children.Add(this.carSprite);
            Canvas.SetTop(this.carSprite, top);
            Canvas.SetLeft(this.carSprite, left);

        }

        public void startThread(double startRt)
        {

            int top = 180;
            int left = 30;

            System.Windows.Controls.Image carSprite = new System.Windows.Controls.Image();
            carSprite.Source = new BitmapImage(new Uri(@"/wpf-car-simulation;component/Resources/car.png", UriKind.Relative));
            carSprite.Width = 80;
            carSprite.Height = 80;
            RotateTransform rt = new RotateTransform(startRt);
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

        public void OffsetPos(double topOff, double leftOff)
        {
            Canvas.SetTop(carSprite, Canvas.GetTop(carSprite) + topOff);
            top = Canvas.GetTop(carSprite) + topOff;
            Canvas.SetLeft(carSprite, Canvas.GetLeft(carSprite) + leftOff);
           left = Canvas.GetTop(carSprite) + topOff;
        }

        public void SetRotation(int rot)
        {
            _rotateTransformAngle = rot;
            RotateTransform rt = new RotateTransform(_rotateTransformAngle);
            rt.CenterX = 25;
            rt.CenterX = 25;
            carSprite.RenderTransform = rt;
        }

        public void Rotate(double rot)
        {
            _rotateTransformAngle += rot;
            RotateTransform rt = new RotateTransform(_rotateTransformAngle);
            rt.CenterX = 25;
            rt.CenterY = 25;
            carSprite.RenderTransform = rt;
        }
    }
}
