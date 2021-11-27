using System.Collections.Generic;
using TriangleNet.Geometry;
using TriangleNet.Meshing;
using TriangleNet.Topology;
using UnityEngine;

public class TriangleNETBuilder : MonoBehaviour
{
    private Vector2[] points;
    public TriangleNETBuilder(Vector2[] vertices)
    {
        points = vertices;
    }
    public int[] Build()
    {
        GenericMesher gm = new GenericMesher();
        List<Vertex> vertices = new List<Vertex>();
        for (int i = 0; i < points.Length; i++)
        {
            Vertex v = new Vertex(points[i].x, points[i].y);
            vertices.Add(v);
        }

        IMesh imesh = gm.Triangulate(vertices);
        ICollection<Triangle> tri = imesh.Triangles;
        int ntri = tri.Count;
        var triangles = new int[3 * ntri];
        int ctri = 0;
        foreach (Triangle triangle in tri)
        {
            triangles[ctri] = triangle.GetVertexID(0);
            triangles[ctri + 1] = triangle.GetVertexID(1);
            triangles[ctri + 2] = triangle.GetVertexID(2);
            ctri += 3;
        }
        return triangles;
    }
}
