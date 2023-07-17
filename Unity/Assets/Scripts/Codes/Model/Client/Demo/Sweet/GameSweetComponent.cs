using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class GameSweetComponent: Entity, IAwake<string>, IDestroy
    {
        public GameSweet[,] Sweets;

        public Dictionary<int,int> AddedMatchIndex = new();

        public List<HashSet<GameSweet>> FinalMatchList = new();
    }
}