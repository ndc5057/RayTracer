using System.Drawing;

namespace RayTracer.SceneObjects
{
    public class Surface
    {
        private Color _color = Color.Black;

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
    }
}