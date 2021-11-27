using System.Collections.Generic;
using UnityEngine;

namespace theZinnZ.NClipper
{
    public class CircleClipper : ClipperBase, IClip
    {
        [Range(3, 25)] public int segmentNum;
        public float radius;

        private List<Vector3> points;
        public override Mesh GetMesh()
        {
            if (points == null)
                points = new List<Vector3>();

            points.Clear();

            float _angle = 0f;
            Vector2 _point = Vector2.zero;

            for (int i = 0; i < segmentNum; i++)
            {
                _angle = Mathf.Deg2Rad * (-90f - 360f / segmentNum * i);
                _point = new Vector3(radius * Mathf.Cos(_angle), radius * Mathf.Sin(_angle));

                points.Add(_point);
            }

            return points.ToPolygon().ToMesh();
        }

        public override List<Vector3> GetPoints()
        {
            if (points == null)
                points = new List<Vector3>();

            points.Clear();

            float _angle = 0f;
            Vector2 _point = Vector2.zero;
            Vector3 _center = transform.position;

            for (int i = 0; i < segmentNum; i++){
                _angle = Mathf.Deg2Rad * (-90f - 360f / segmentNum * i);
                _point = new Vector3(_center.x + radius * Mathf.Cos(_angle), _center.y + radius * Mathf.Sin(_angle), _center.z);

                points.Add(_point);
            }

            return points;
        }

    }

    public abstract class ClipperBase : MonoBehaviour, IClip
    {
        public abstract Mesh GetMesh();
        public abstract List<Vector3> GetPoints();
    }

}
