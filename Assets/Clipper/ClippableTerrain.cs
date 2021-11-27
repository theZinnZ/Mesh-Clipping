using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace theZinnZ.NClipper
{
    using ClipperLib;
    using Polygon = List<ClipperLib.IntPoint>;
    using Polygons = List<List<ClipperLib.IntPoint>>;

    public class ClippableTerrain : MonoBehaviour
    {
        public Polygons polygons;

        public IntPoint[] points;
        private Mesh mesh;
        private void Awake()
        {
            Initializing();
        }

        private void GenerateMesh()
        {
            if (polygons == null)
                polygons = new Polygons();

            polygons.Add(new Polygon(4));
            polygons[0].Add(new IntPoint(0, 400000));
            polygons[0].Add(new IntPoint(0, 0));
            polygons[0].Add(new IntPoint(400000, 0));
            polygons[0].Add(new IntPoint(400000, 400000));
        }

        public void Initializing()
        {

            // Get Mesh
            mesh = GetComponent<MeshFilter>().mesh;
            mesh.MarkDynamic();

            // Get Polygons
            points = System.Array.ConvertAll(mesh.vertices, x => x.ToIntPoint());

            if (polygons == null)
                polygons = new Polygons();

            polygons.Add(new Polygon(points));

            //PolygonUpdate();
        }

        public void Clip(IClip clip)
        {
            Polygons result = new Polygons();

            Clipper clipper = new Clipper();
            clipper.AddPolygons(polygons, PolyType.ptSubject);
            clipper.AddPolygon(clip.GetPoints().ToPolygon(), PolyType.ptClip);
            clipper.Execute(ClipType.ctDifference, result, PolyFillType.pftNonZero, PolyFillType.pftNonZero);

            polygons = result;
            PolygonUpdate();
        }

        private void MergePaths(Polygons args)
        {
            int count = args.Count - 1;
            for (int i = 0; i < count; i++)
            {
                args[0].AddRange(args[i + 1]);
            }

            args.RemoveAll((x) => args.IndexOf(x) > 0);
        }

        public void PolygonUpdate()
        {
            mesh.Clear();
            GetComponent<MeshFilter>().mesh = polygons.ToMesh(transform);
        }

    }
}
