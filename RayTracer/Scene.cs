using System.Collections.Generic;
using System.Drawing;
using RayTracer.SceneObjects;

namespace RayTracer
{
    public class Scene
    {
        public List<SceneObject> Objects;
        public List<Light> Lights;
        public Camera Camera;
        public Color BackgroundColor;
    }
}
