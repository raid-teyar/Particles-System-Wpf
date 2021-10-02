using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Windows.Threading;

namespace Particules_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        Random rnd = new Random();

        const int sizeRangeOffset = 10;
        const int lifeRangeOffset = 1000;
        const int coloreRangeOffset = 100;
        const int positionRangeOffset = 200;
        const int speedRangeOffset = 5;
        //const double opacityRangeOffset = 0.2;

        Point screenCenter;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromMilliseconds(33.33);
            timer.Tick += Timer_Tick;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            myCanvas.Children.Clear();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            screenCenter = new Point(myCanvas.ActualWidth / 2, myCanvas.ActualHeight / 2);
            Particle particle = CreateParticle(20, TimeSpan.FromMilliseconds(2000), screenCenter, 10);
            Ellipse ellipse = new Ellipse()
            {
                Height = particle.size,
                Width = particle.size,
                Fill = particle.colore,
                Stroke = particle.colore,
                Opacity = particle.opacity

            };
            Canvas.SetBottom(myCanvas, particle.position.Y);
            Canvas.SetRight(myCanvas, particle.position.X);
            myCanvas.Children.Add(ellipse);
        }

        private Particle CreateParticle(int sizeRng, TimeSpan lifeRng, Point positionRng, int speedRng)
        {
            Particle particle = new Particle()
            {
                size = rnd.Next(sizeRng - sizeRangeOffset, sizeRng + sizeRangeOffset),
                life = TimeSpan.FromMilliseconds(rnd.Next(Convert.ToInt32(lifeRng.TotalMilliseconds - TimeSpan.FromMilliseconds(lifeRangeOffset).TotalMilliseconds), Convert.ToInt32(lifeRng.TotalMilliseconds + TimeSpan.FromMilliseconds(lifeRangeOffset).TotalMilliseconds))),
                colore = new SolidColorBrush(Color.FromArgb(Convert.ToByte(rnd.Next(0, 255)), Convert.ToByte(rnd.Next(0, 255)), Convert.ToByte(rnd.Next(0, 255)), Convert.ToByte(rnd.Next(0, 255)))),
                position = new Point(rnd.Next(Convert.ToInt32(positionRng.X - positionRangeOffset), Convert.ToInt32(positionRng.X + positionRangeOffset)), rnd.Next(Convert.ToInt32(positionRng.Y - positionRangeOffset), Convert.ToInt32(positionRng.Y + positionRangeOffset))),
                speed = rnd.Next(speedRng - speedRangeOffset, speedRng + speedRangeOffset),
                opacity = rnd.NextDouble()
            };
           
            return particle;
        }

       
    }
}
