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
using PaletteMixr;

namespace Particules_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        DispatcherTimer timer = new DispatcherTimer();
        Random rnd = new Random();
        readonly Point screenOffset = new Point(40, 50);
        System.Drawing.Color rndColor;

        List<Particle> particles = new List<Particle>();

        List<Shape> shapes = new List<Shape>();

        #region Dpendency Propreties


        public int sizeRangeOffset
        {
            get { return (int)GetValue(sizeRangeOffsetProperty); }
            set { SetValue(sizeRangeOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for sizeRangeOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty sizeRangeOffsetProperty =
            DependencyProperty.Register("sizeRangeOffset", typeof(int), typeof(MainWindow), new PropertyMetadata(100));



        public int colorRangeOffset
        {
            get { return (int)GetValue(colorRangeOffsetProperty); }
            set { SetValue(colorRangeOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for colorRangeOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty colorRangeOffsetProperty =
            DependencyProperty.Register("colorRangeOffset", typeof(int), typeof(MainWindow), new PropertyMetadata(90));



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
            DependencyProperty.Register("speedRangeOffset", typeof(int), typeof(MainWindow), new PropertyMetadata(10));




        public double opacityRangeOffset
        {
            get { return (double)GetValue(opacityRangeOffsetProperty); }
            set { SetValue(opacityRangeOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for opacityRangeOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty opacityRangeOffsetProperty =
            DependencyProperty.Register("opacityRangeOffset", typeof(double), typeof(MainWindow), new PropertyMetadata(0.2));



        #endregion

        Point screenCenter;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            timer.Interval = TimeSpan.FromMilliseconds(speedRangeOffset);
            timer.Tick += Timer_Tick;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            rndColor = System.Drawing.Color.FromArgb(Convert.ToByte(rnd.Next(0, 255)), Convert.ToByte(rnd.Next(0, 255)), Convert.ToByte(rnd.Next(0, 255)), Convert.ToByte(rnd.Next(0, 255)));
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
            screenCenter = new Point(myCanvas.ActualWidth / 2 - screenOffset.X, myCanvas.ActualHeight / 2 - screenOffset.Y);
            Particle particle = CreateParticle(sizeRangeOffset / 2, TimeSpan.FromMilliseconds(2000), screenCenter, 10, rndColor);
            if (particle.size < 0)
            {
                particle.size = -particle.size;
            }
            Ellipse ellipse = new Ellipse()
            {
                Height = particle.size,
                Width = particle.size,
                Fill = particle.color,
                Opacity = particle.opacity
            };
            shapes.Add(ellipse);
            
            

            Canvas.SetBottom(ellipse, particle.position.Y);
            Canvas.SetRight(ellipse, particle.position.X);
            myCanvas.Children.Add(ellipse);
        }

        public System.Drawing.Color GenerateColorPalette(System.Drawing.Color baseColor)
        {
           PaletteGenerator PaletteGenerator = new PaletteGenerator(baseColor);
           List<System.Drawing.Color> colors =  PaletteGenerator.GenerateSaturationPalette(PaletteSize.Small).ToList();
           return colors[rnd.Next(0, colors.Count)]; 
        }

        private Particle CreateParticle(int sizeRng, TimeSpan lifeRng, Point positionRng, int speedRng, System.Drawing.Color color)
        {
            Color generatedColor;

            if ((bool)isPalette.IsChecked)
            {
                 generatedColor = Color.FromArgb(GenerateColorPalette(color).A, GenerateColorPalette(color).R, GenerateColorPalette(color).G, GenerateColorPalette(color).B);
            }
            else
            {
                generatedColor = Color.FromArgb(Convert.ToByte(rnd.Next(128 - colorRangeOffset, 128 + colorRangeOffset)), Convert.ToByte(rnd.Next(128 - colorRangeOffset, 128 + colorRangeOffset)), Convert.ToByte(rnd.Next(128 - colorRangeOffset, 128 + colorRangeOffset)), Convert.ToByte(rnd.Next(128 - colorRangeOffset, 128 + colorRangeOffset)));
            }

            Particle particle = new Particle()
            {
                size = rnd.Next(sizeRng - sizeRangeOffset, sizeRng + sizeRangeOffset),
                life = TimeSpan.FromMilliseconds(rnd.Next(Convert.ToInt32(lifeRng.TotalMilliseconds - TimeSpan.FromMilliseconds(lifeRangeOffset).TotalMilliseconds), Convert.ToInt32(lifeRng.TotalMilliseconds + TimeSpan.FromMilliseconds(lifeRangeOffset).TotalMilliseconds))),
                color = new SolidColorBrush(generatedColor),
                position = new Point(rnd.Next(Convert.ToInt32(positionRng.X - xPositionRangeOffset), Convert.ToInt32(positionRng.X + xPositionRangeOffset)), rnd.Next(Convert.ToInt32(positionRng.Y - yPositionRangeOffset), Convert.ToInt32(positionRng.Y + yPositionRangeOffset))),
                speed = rnd.Next(speedRng - speedRangeOffset, speedRng + speedRangeOffset),
                opacity = rnd.NextDouble() * (1 - opacityRangeOffset - (1 + opacityRangeOffset)) + (1 - opacityRangeOffset)
            };

            return particle;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            timer.Interval = TimeSpan.FromMilliseconds(speedRangeOffset);
        }
    }
}
