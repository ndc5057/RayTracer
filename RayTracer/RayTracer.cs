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
                    Color color = TraceRay(new Ray{ Origin = scene.Camera.Pos, Direction = GetRayDirection(x, y, scene)}, scene, 0);
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

        private Color TraceRay(Ray ray, Scene scene, int recurseDepth)
        {
            //TODO
            var intersections = scene.Objects.Select(obj => obj.Intersect(ray)).ToList();

            var firstIntersect =
                intersections.Where(isect => !double.IsPositiveInfinity(isect.Distance))
                    .OrderBy(isect => isect.Distance)
                    .FirstOrDefault();
            if (firstIntersect == null)
            {
                return scene.BackgroundColor;
            }

            return GetShade(firstIntersect, scene, recurseDepth);
        }

        private Color GetShade(Intersection firstInstersect, Scene scene, int depth)
        {
            Vector3 position = firstInstersect.Distance*firstInstersect.DirectionRay.Direction + firstInstersect.DirectionRay.Origin;
            Vector3 normal = firstInstersect.Object.GetNormal(position);
            return GetNaturalColor(scene, position, normal, firstInstersect.Object).Add(firstInstersect.Object.Surface.Color);
        }

        private Color GetNaturalColor(Scene scene, Vector3 position, Vector3 norm, SceneObject firstIntersectObject)
        {
            Color output = Color.Black;
            foreach (var light in scene.Lights)
            {
                Vector3 lightDistance = light.Pos - position;
                Vector3 lightVector = Vector3.Normalize(lightDistance);
                var distToObjTowardsLight = TestRay(new Ray {Origin = position, Direction = lightVector}, scene);
                bool isInShadow = !((distToObjTowardsLight > lightDistance.Length) || (Math.Abs(distToObjTowardsLight) < Double.TOLERANCE));
                if (!isInShadow)
                {
                    var illumination = Vector3.DotProduct(lightVector, norm);
                    Color lcolor = illumination > 0 ? light.Color.Times(illumination) : scene.BackgroundColor;
                    output = output.Add(lcolor);
                }
            }
            return output;
        }

        private static double TestRay(Ray ray, Scene scene)
        {
            var intersections = scene.Objects.Select(obj => obj.Intersect(ray)).ToList();

            var firstIntersect =
                intersections.Where(isect => !double.IsPositiveInfinity(isect.Distance))
                    .OrderBy(isect => isect.Distance)
                    .FirstOrDefault();

            if (firstIntersect == null)
            {
                return 0;
            }

            return firstIntersect.Distance;
        }

        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }

        internal static readonly Scene DefaultScene = new Scene
        {
            Camera = Camera.Create(new Vector3(1, 0, -20), new Vector3(0, 0, 0), new Vector3(0, 1, 0), 45),
            Lights = new List<Light>
            {
                new Light {Color = Color.White, Pos = new Vector3(0, 0, 0)}
            },
            Objects = new List<SceneObject>
            {
                new Sphere
                {
                    CenterPos = new Vector3(0, -0.2, 3),
                    Radius = 0.25,
                    Surface = new Surface {Color = Color.Orange}
                },
                new Sphere
                {
                    CenterPos = new Vector3(0, 0.25, 3),
                    Radius = 0.25,
                    Surface = new Surface {Color = Color.Firebrick}
                },
                new Plane {Distance = 4, Normal = new Vector3(0, 0, -1)}
            },
            BackgroundColor = Color.Blue
        };
    }
}


