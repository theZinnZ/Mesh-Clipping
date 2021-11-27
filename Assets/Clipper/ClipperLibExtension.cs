using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace theZinnZ.NClipper
{
    using Polygon = List<ClipperLib.IntPoint>;
    using Polygons = List<List<ClipperLib.IntPoint>>;

    public static class ClipperLibExtension
    {
        public static Polygon ToPolygon(this List<Vector3> source)
        {
            return source.ConvertAll((x) => x.ToIntPoint());
        }
        public static Polygon ToPolygon(this List<Vector2> source)
        {
            return source.ConvertAll((x) => x.ToIntPoint());
        }

        public static void MergePaths(Polygons args)
        {
            int count = args.Count - 1;
            for (int i = 0; i < count; i++)
            {
                args[0].AddRange(args[i + 1]);
            }

            args.RemoveAll((x) => args.IndexOf(x) > 0);
        }

        public static Mesh ToMesh(this Polygons paths)
        {
            MergePaths(paths);
            return paths[0].ToMesh();
        }

        public static Mesh ToMesh(this Polygons paths, Transform transform)
        {
            Mesh mesh = new Mesh();

            #region MESH COMBINE
            CombineInstance[] instances = new CombineInstance[paths.Count];
            for (int i = 0; i < instances.Length; i++)
            {
                instances[i].mesh = paths[i].ToMesh();
                instances[i].transform = transform.localToWorldMatrix;
            }
            mesh.CombineMeshes(instances);

            #endregion

            return mesh;
        }
        public static Mesh ToMesh(this Polygon path)
        {
            var vertices2D = path.ConvertAll((x) => x.ToVector2()).ToArray();
            var triangulator = new Triangulator(vertices2D).Triangulate();

            var delaunayTriangulationBuilder = new Unity3d.PlaneTriangulator.DelaunayTriangulationBuilder();
            var delaunayTriangles = delaunayTriangulationBuilder.Build(vertices2D).ToArray();

            //var triangleNetBuilder = new TriangleNETBuilder(vertices2D);
            //var netTriangles = triangleNetBuilder.Build();

            Mesh mesh = new Mesh()
            {
                vertices = path.ConvertAll((x) => x.ToVector3()).ToArray(),
                triangles = triangulator,
            };

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            mesh.RecalculateTangents();
            return mesh;
        }
    }

}
