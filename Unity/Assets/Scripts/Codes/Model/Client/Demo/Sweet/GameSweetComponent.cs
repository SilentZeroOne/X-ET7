namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class GameSweetComponent: Entity, IAwake, IDestroy
    {
        public GameSweet[,] Sweets = new GameSweet[6, 6];
    }
}