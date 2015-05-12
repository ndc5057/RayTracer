using System.Drawing;

namespace RayTracer
{
    public static class ColorExtensions
    {
        public static Color Add(this Color color1, Color color2)
        {
            return Color.FromArgb((int) (color1.R*0.5  + color2.R * 0.5), (int) (color1.G * 0.5 + color2.G * 0.5), (int) (color1.B * 0.5 + color2.B * 0.5));
        }

        public static Color Times(this Color color1, double coefficient)
        {
            return Color.FromArgb((int) (color1.R * coefficient), (int) (color1.G * coefficient), (int) (color1.B * coefficient));
        }
    }
}
