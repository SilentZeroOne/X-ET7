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
        public static void RegisterMonoActions(this GameSweet self)
        {
            
        }
        
        public static void OnClick(this GameSweet self)
        {
        }
        
    }
}