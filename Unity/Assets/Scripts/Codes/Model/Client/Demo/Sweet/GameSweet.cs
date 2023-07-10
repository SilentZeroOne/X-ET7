using Unity.Mathematics;

namespace ET
{
    [ChildOf(typeof(GameSweetComponent))]
    public class GameSweet: Entity, IAwake<int>, IDestroy
    {
        public int ConfigId { get; set; }
        public SweetConfig Config => SweetConfigCategory.Instance.Get(this.ConfigId);
        
        public float2 Position { get; set; }
    }
}

