using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RayTracer.SceneObjects;

namespace RayTracer
{
    public class RayTracer
    {
        public Action<int, int, Color> SetPixel;

        public RayTracer(int width, int height, Action<int, int, Color> setPixel)
        {
            ScreenWidth = width;
            ScreenHeight = height;

            SetPixel = setPixel;
        }

        public void Render(Scene scene)
        {
            for (int y = 0; y < ScreenHeight; y++)
            {
                for (int x = 0; x < ScreenWidth; x++)
                {
                    Color color = TraceRay(new Ray{ Origin = scene.Camera.Pos, Direction = GetRayDirection(x, y, scene)}, scene);
                    SetPixel(x, y, color);
                }
            }
        }

        /// <summary>
        ///     Find ray direction from Camera for a given pixel location. Calulated by assuming a virtual screen is 1 unit in front of the camera
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="scene"></param>
        /// <returns></returns>
        private Vector3 GetRayDirection(double x, double y, Scene scene)
        {
            //Calculate virtual screen size based on field of view
            //
            //          Tangent = Opposite / Adjacent
            //
            //                /|
            //                 |
            //              /  |        Divide FOV by 2 to get right triangle with Adjacent = 1
            //                 |                Tan(FOV/2)   =  Opposite / 1
            //            /    |                Opposite * 2 =  Hieght
            //  FOV ang-> --1--|                 
            //            \    |                Height = Tan(FOV/2) * 2
            //                 |
            //              \  |
            //                 |
            //                \|
            //
            var viewHeight = Math.Tan(scene.Camera.FieldOfViewY.ToRadians() / 2) * 2;
            var viewWidth = viewHeight * ((double)ScreenWidth / ScreenHeight);

            //Scale camera direction vectors based on coordinates
            var xScale = viewWidth/2 - x/(ScreenWidth - 1)*viewWidth;
            var yScale = viewHeight/2 - y/(ScreenHeight - 1)*viewHeight;

            return Vector3.Normalize(scene.Camera.Forward 
                                    + scene.Camera.Right * xScale 
                                    + scene.Camera.Up * yScale);
        }

        private Color TraceRay(Ray ray, Scene scene)
        {
            //TODO
            var distance = double.PositiveInfinity;

            var firstIntersect =
                scene.Objects.Where(obj => obj.Intersect(ray) < double.PositiveInfinity)
                    .OrderBy(obj => obj.Intersect(ray))
                    .FirstOrDefault();
            if (firstIntersect == null)
            {
                return scene.BackgroundColor;
            }

            distance = firstIntersect.Intersect(ray);
            return firstIntersect.Surface.Color;
        }

        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }

        internal static readonly Scene DefaultScene = new Scene
        {
            Camera = Camera.Create(new Vector3(0, 0, -2), new Vector3(0.25, 0.5, 1), new Vector3(0, 1, 0), 45),
            Objects = new List<SceneObject>
            {
                new Sphere { CenterPos = new Vector3(0, -0.2, 3), Radius = 0.25, Surface = new Surface {Color = Color.Orange}}, 
                new Sphere { CenterPos = new Vector3(0, 0.25, 3), Radius = 0.25, Surface = new Surface {Color = Color.Firebrick}},
                new Plane {Distance = 4, Normal = new Vector3(1,0,0)}
            },
            BackgroundColor = Color.Blue
        };
    }
}
