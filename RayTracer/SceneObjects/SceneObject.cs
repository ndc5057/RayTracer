namespace RayTracer.SceneObjects
{
    public abstract class SceneObject
    {
        private Surface _surface = new Surface();

        public Surface Surface
        {
            get { return _surface; }
            set { _surface = value; }
        }

        public abstract double Intersect(Ray ray);
    }
}