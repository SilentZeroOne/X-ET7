using Unity.Mathematics;

namespace ET
{
    public static partial class MathematicsHelper
    {
        public static int2 ToInt2(this float2 pos)
        {
            return new int2((int)pos.x, (int)pos.y);
        }
    }
}