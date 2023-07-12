using Unity.Mathematics;

namespace ET
{
    public class GameSweetComponentAwakeSystem: AwakeSystem<GameSweetComponent>
    {
        protected override void Awake(GameSweetComponent self)
        {

        }
    }

    public class GameSweetComponentDestroySystem: DestroySystem<GameSweetComponent>
    {
        protected override void Destroy(GameSweetComponent self)
        {

        }
    }
    
    [FriendOfAttribute(typeof(ET.GameSweetComponent))]
    public static class GameSweetComponentSystem
    {
        public static GameSweet Add(this GameSweetComponent self, int sweetId, float2 pos)
        {
            var sweet = self.AddChild<GameSweet, int>(sweetId);
            self.Sweets[(int)pos.x, (int)pos.y] = sweet;

            return sweet;
        }
    }
}