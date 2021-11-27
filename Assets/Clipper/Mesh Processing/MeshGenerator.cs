using System.Collections.Generic;
using UnityEngine;


namespace theZinnZ.NClipper.Editor
{
    using NaughtyAttributes;
    public class MeshGenerator : MonoBehaviour
    {
        public List<Transform> points = new List<Transform>();

        [Button("Generate")]
        public void Generate()
        {
            Mesh mesh = GenerateMesh();
             
            MeshSaverEditor.SaveMesh(mesh, mesh.GetInstanceID() + "_Mesh", true, false);
        }

        private Mesh GenerateMesh()
        {
            // Sort by clockwise
            //points = points.OrderBy((x) => Mathf.Abs(x.position.x) + Mathf.Abs(x.position.y)).ToList();

            var path = points.ConvertAll((x) => new Vector2(x.position.x, x.position.y).ToIntPoint());
            return path.ToMesh();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawMesh(GenerateMesh());
        }
    }
}

