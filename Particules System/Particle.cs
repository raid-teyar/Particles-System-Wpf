using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Particules_System
{
    public class Particle
    {
        public double size { get; set; }
        public TimeSpan life { get; set; }
        public Brush colore { get; set; }
        public Point position { get; set; }
        public double speed { get; set; }
    }
}
