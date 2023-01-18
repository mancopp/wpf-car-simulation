using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace wpf_car_simulation
{
    public class Train
    {
        private readonly string imgPath = "Resources/Train.png";
        private Canvas mainCanvas;
        public int id = 1;
        public int velocity = 1;
        private Image trainSprite;
        private double _rotateTransformAngle = 0;
        public bool directionBot;
        public double top;
        public double left;

        public Train(Canvas canvas)
        {
            this.mainCanvas = canvas;
        }

        public void spawnStatic()
        {
            left = 745;

            if (directionBot == true)
            {
                top = -100;
            }
            else
            {
                top = 850;
            }

            System.Windows.Controls.Image trainSprite = new System.Windows.Controls.Image();
            trainSprite.Source = new BitmapImage(new Uri(@"/wpf-car-simulation;component/" + imgPath, UriKind.Relative));
            trainSprite.Height = 200;

            this.trainSprite = trainSprite;

            this.mainCanvas.Children.Add(this.trainSprite);
            Canvas.SetTop(this.trainSprite, top);
            Canvas.SetLeft(this.trainSprite, left);

        }

        public void SetPos(int top, int left)
        {
            Canvas.SetTop(trainSprite, top);
            Canvas.SetLeft(trainSprite, left);
        }

        public void OffsetPos(double topOff, double leftOff)
        {
            Canvas.SetTop(trainSprite, Canvas.GetTop(trainSprite) + topOff);
            top = Canvas.GetTop(trainSprite) + topOff;
            Canvas.SetLeft(trainSprite, Canvas.GetLeft(trainSprite) + leftOff);
            left = Canvas.GetTop(trainSprite) + topOff;
        }
    }
}
