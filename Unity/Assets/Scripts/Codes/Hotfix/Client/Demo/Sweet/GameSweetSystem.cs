using System;

namespace ET
{
    public class GameSweetAwakeSystem: AwakeSystem<GameSweet,int>
    {
        protected override void Awake(GameSweet self, int a)
        {
            self.ConfigId = a;
        }
    }

    public class GameSweetDestroySystem: DestroySystem<GameSweet>
    {
        protected override void Destroy(GameSweet self)
        {
            self.ConfigId = 0;
            self.ViewInited = false;
        }
    }

    [FriendOf(typeof (GameSweet))]
    public static class GameSweetSystem
    {
        public static bool IsNear(this GameSweet self, GameSweet other)
        {
            return Math.Abs(self.PosInGrid.x - other.PosInGrid.x)- 1 < 0.1f && Math.Abs(self.PosInGrid.y - other.PosInGrid.y) - 1 < 0.1f || self == other;
        }
        
    }
}