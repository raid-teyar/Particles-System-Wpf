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

        #region Dpendency Propreties


        public int sizeRangeOffset
        {
            get { return (int)GetValue(sizeRangeOffsetProperty); }
            set { SetValue(sizeRangeOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for sizeRangeOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty sizeRangeOffsetProperty =
            DependencyProperty.Register("sizeRangeOffset", typeof(int), typeof(MainWindow), new PropertyMetadata(10));



        public int coloreRangeOffset
        {
            get { return (int)GetValue(coloreRangeOffsetProperty); }
            set { SetValue(coloreRangeOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for coloreRangeOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty coloreRangeOffsetProperty =
            DependencyProperty.Register("coloreRangeOffset", typeof(int), typeof(MainWindow), new PropertyMetadata(100));



        public int lifeRangeOffset
        {
            get { return (int)GetValue(lifeRangeOffsetProperty); }
            set { SetValue(lifeRangeOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for lifeRangeOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty lifeRangeOffsetProperty =
            DependencyProperty.Register("lifeRangeOffset", typeof(int), typeof(MainWindow), new PropertyMetadata(1000));



        public int xPositionRangeOffset
        {
            get { return (int)GetValue(xPositionRangeOffsetProperty); }
            set { SetValue(xPositionRangeOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for xPositionRangeOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xPositionRangeOffsetProperty =
            DependencyProperty.Register("xPositionRangeOffset", typeof(int), typeof(MainWindow), new PropertyMetadata(400));



        public int yPositionRangeOffset
        {
            get { return (int)GetValue(yPositionRangeOffsetProperty); }
            set { SetValue(yPositionRangeOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for yPositionRangeOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty yPositionRangeOffsetProperty =
            DependencyProperty.Register("yPositionRangeOffset", typeof(int), typeof(MainWindow), new PropertyMetadata(270));




        public int speedRangeOffset
        {
            get { return (int)GetValue(speedRangeOffsetProperty); }
            set { SetValue(speedRangeOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for speedRangeOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty speedRangeOffsetProperty =
            DependencyProperty.Register("speedRangeOffset", typeof(int), typeof(MainWindow), new PropertyMetadata(5));




        public int opacityRangeOffset
        {
            get { return (int)GetValue(opacityRangeOffsetProperty); }
            set { SetValue(opacityRangeOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for opacityRangeOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty opacityRangeOffsetProperty =
            DependencyProperty.Register("opacityRangeOffset", typeof(int), typeof(MainWindow), new PropertyMetadata(0.2));



        #endregion

        Point screenCenter;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromMilliseconds(5);
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
                StrokeThickness = 0,
                Opacity = particle.opacity

            };
            Canvas.SetBottom(ellipse, particle.position.Y);
            Canvas.SetRight(ellipse, particle.position.X);
            myCanvas.Children.Add(ellipse);
        }

        private Particle CreateParticle(int sizeRng, TimeSpan lifeRng, Point positionRng, int speedRng)
        {
            Particle particle = new Particle()
            {
                size = rnd.Next(sizeRng - sizeRangeOffset, sizeRng + sizeRangeOffset),
                life = TimeSpan.FromMilliseconds(rnd.Next(Convert.ToInt32(lifeRng.TotalMilliseconds - TimeSpan.FromMilliseconds(lifeRangeOffset).TotalMilliseconds), Convert.ToInt32(lifeRng.TotalMilliseconds + TimeSpan.FromMilliseconds(lifeRangeOffset).TotalMilliseconds))),
                colore = new SolidColorBrush(Color.FromArgb(Convert.ToByte(rnd.Next(0, 255)), Convert.ToByte(rnd.Next(0, 255)), Convert.ToByte(rnd.Next(0, 255)), Convert.ToByte(rnd.Next(0, 255)))),
                position = new Point(rnd.Next(Convert.ToInt32(positionRng.X - xPositionRangeOffset), Convert.ToInt32(positionRng.X + xPositionRangeOffset)), rnd.Next(Convert.ToInt32(positionRng.Y - yPositionRangeOffset), Convert.ToInt32(positionRng.Y + yPositionRangeOffset))),
                speed = rnd.Next(speedRng - speedRangeOffset, speedRng + speedRangeOffset),
                opacity = rnd.NextDouble()
            };
           
            return particle;
        }

       
    }
}
