using UnityEngine;

namespace theZinnZ.NClipper
{
    public class ClipperVisualization : MonoBehaviour
    {
        public MeshFilter meshFilter;
        public ClipperBase clipper;

        private void Update()
        {
            meshFilter.mesh = clipper.GetMesh();
        }
    }

}
