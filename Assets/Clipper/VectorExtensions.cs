using System.Collections.Generic;
using UnityEngine;

namespace theZinnZ.NClipper
{
    using ClipperLib;

    public static class VectorExtensions
    {
        private static float float2int64 = 100000.0f;

        public static IntPoint ToIntPoint(this Vector2 source)
        {
            return new IntPoint()
            {
                x = (long)(source.x * float2int64),
                y = (long)(source.y * float2int64),
            };
        }
        public static IntPoint ToIntPoint(this Vector3 source)
        {
            return new IntPoint()
            {
                x = (long)(source.x * float2int64),
                y = (long)(source.y * float2int64),
            };
        }
        public static IntPoint ToIntPointByZ(this Vector3 source)
        {
            return new IntPoint()
            {
                x = (long)(source.x * float2int64),
                y = (long)(source.z * float2int64),
            };
        }
        public static Vector2 ToVector2(this IntPoint source)
        {
            return new Vector2()
            {
                x = source.x / float2int64,
                y = source.y / float2int64,
            };
        }
        public static Vector3 ToVector3(this IntPoint source)
        {
            return new Vector3()
            {
                x = source.x / float2int64,
                y = source.y / float2int64,
            };
        }
    }
}
