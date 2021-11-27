using System.Collections.Generic;
using UnityEngine;

namespace theZinnZ.NClipper
{
    public interface IClip
    {
        List<Vector3> GetPoints();
        Mesh GetMesh();
    }
}
