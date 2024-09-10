using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maui_planets.Models
{
    public class Planet
    {
        public string Name { get; set; }
        public string Subtitle { get; set; }
        public string HeroImage { get; set; }
        public string Description { get; set; }
        public Color AccentColorStart { get; set; }
        public Color AccentColorEnd { get; set; }
        public List<string> Images { get; set; }

        public Brush Background { 
            get
            {
                var gradientStops = new GradientStopCollection();
                gradientStops.Add(new GradientStop(AccentColorStart, 0.00f));
                gradientStops.Add(new GradientStop(AccentColorEnd, 0.00f));

                var bgBrush = new LinearGradientBrush(
                    gradientStops, 
                    new Point(0.0,0.0), 
                    new Point(1.0, 1.0));

                return bgBrush;
            }
        }

    }
}
