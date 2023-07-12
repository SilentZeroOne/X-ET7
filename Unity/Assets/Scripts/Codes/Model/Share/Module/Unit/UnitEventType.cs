using Unity.Mathematics;

namespace ET
{
    namespace EventType
    {
        public struct ChangePosition
        {
            public Unit Unit;
            public float3 OldPos;
        }

        public struct ChangeRotation
        {
            public Unit Unit;
        }
        
        public struct SweetChangePosition
        {
            public GameSweet Sweet;
            public float2 OldPos;
        }
    }
}