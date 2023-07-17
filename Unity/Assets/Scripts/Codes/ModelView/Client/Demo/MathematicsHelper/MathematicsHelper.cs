using Unity.Mathematics;
using UnityEngine;

namespace ET
{
    public static partial class MathematicsHelper
    {
        public static Vector2 ToVector2(this float2 pos)
        {
            return new Vector2(pos.x, pos.y);
        }

        public static Vector3 ToVector3(this float3 pos)
        {
            return new Vector3(pos.x, pos.y, pos.z);
        }
    }
}