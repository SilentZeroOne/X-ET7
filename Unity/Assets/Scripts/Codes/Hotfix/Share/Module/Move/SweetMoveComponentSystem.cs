using System;
using Unity.Mathematics;

namespace ET
{
    public class SweetMoveComponentAwakeSystem: AwakeSystem<SweetMoveComponent>
    {
        protected override void Awake(SweetMoveComponent self)
        {
            self.StartTime = 0;
            self.TargetPos = new float2(-100, -100);
            self.NeedTime = 0;
            self.MoveTimer = 0;
            self.tcs = null;
            self.StartPos = float2.zero;

            self.Speed = 0;
        }
    }

    public class SweetMoveComponentDestroySystem: DestroySystem<SweetMoveComponent>
    {
        protected override void Destroy(SweetMoveComponent self)
        {
            self.Stop(false);
        }
    }

    [FriendOfAttribute(typeof(ET.SweetMoveComponent))]
    [FriendOfAttribute(typeof(ET.GameSweet))]
    public static class SweetMoveComponentSystem
    {
        [FriendOf(typeof(SweetMoveComponent))]
        [Invoke(TimerInvokeType.SweetMoveTimer)]
        public class SweetMoveComponentTimer : ATimer<SweetMoveComponent>
        {
            protected override void Run(SweetMoveComponent self)
            {
                try
                {
                    self.MoveForward(true);
                }
                catch (Exception e)
                {
                    Log.Error(e.ToString());
                }
            }
        }

        public static async ETTask<bool> MoveToAsync(this SweetMoveComponent self, int x, int y, float speed)
        {
            if (self == null || self.IsDisposed) return false;
            
            self.Stop(false);
            var sweet = self.GetParent<GameSweet>();
            
            self.Speed = speed;
            self.TargetPos = new float2(x, y);
            self.StartPos = sweet.RealPos;

            self.tcs = ETTask<bool>.Create(true);

            EventSystem.Instance.Publish(self.DomainScene(), new EventType.SweetMoveStart() { Sweet = self.GetParent<GameSweet>() });

            self.StartMove();

            bool moveRet = await self.tcs;

            if (moveRet)
            {
                EventSystem.Instance.Publish(self.DomainScene(), new EventType.SweetMoveStop() { Sweet = self.GetParent<GameSweet>() });
            }

            return moveRet;
        }

        public static void StartMove(this SweetMoveComponent self)
        {
            self.BeginTime = TimeHelper.ClientNow();
            self.StartTime = self.BeginTime;
            var distance = math.distance(self.StartPos, self.TargetPos);
            self.NeedTime = (long)(distance / self.Speed * 1000);

            self.MoveTimer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.SweetMoveTimer, self);
        }

        public static void MoveForward(this SweetMoveComponent self, bool ret)
        {
            GameSweet sweet = self.GetParent<GameSweet>();

            long timeNow = TimeHelper.ClientNow();
            long moveTime = timeNow - self.StartTime;

            while (true)
            {
                if (moveTime <= 0)
                {
                    return;
                }
                
                // 计算位置插值
                if (moveTime >= self.NeedTime)
                {
                    sweet.RealPos = self.TargetPos;
                }
                else
                {
                    // 计算位置插值
                    float amount = moveTime * 1f / self.NeedTime;
                    if (amount > 0)
                    {
                        var currentPos = math.lerp(self.StartPos, self.TargetPos, amount);
                        sweet.RealPos = currentPos;
                    }
                }

                moveTime -= self.NeedTime;

                // 表示这个点还没走完，等下一帧再来
                if (moveTime < 0)
                {
                    return;
                }

                var finish = math.abs(self.TargetPos - sweet.RealPos) < math.EPSILON;
                if (finish is { x: true, y: true })
                {
                    sweet.RealPos = self.TargetPos;
                    self.MoveFinish(ret);

                    return;
                }
            }
        }

        public static void Stop(this SweetMoveComponent self, bool ret)
        {
            self.MoveFinish(ret);
        }

        private static void MoveFinish(this SweetMoveComponent self, bool ret)
        {
            if (self == null || self.IsDisposed || self.StartTime == 0)
            {
                return;
            }
            
            TimerComponent.Instance?.Remove(ref self.MoveTimer);
            self.StartPos = float2.zero;
            self.TargetPos = new float2(-100, -100);
            self.StartTime = 0;
            self.BeginTime = 0;
            self.NeedTime = 0;
            self.Speed = 0;

            if (self.tcs != null)
            {
                var tcs = self.tcs;
                self.tcs = null;
                tcs.SetResult(ret);
            }
        }
    }
}