using Unity.Mathematics;

namespace ET
{
    [ComponentOf()]
    public class SweetMoveComponent: Entity, IAwake, IDestroy
    {
        public long StartTime;
        public long BeginTime;
        public long NeedTime;
        public long MoveTimer;
        public float Speed; //m/s

        public float2 StartPos { get; set; }
        public float2 TargetPos { get; set; }

        public ETTask<bool> tcs;
    }
}