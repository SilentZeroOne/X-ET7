using Unity.Mathematics;

namespace ET
{
    [ChildOf(typeof(GameSweetComponent))]
    public class GameSweet: Entity, IAwake<int>, IDestroy
    {
        public int ConfigId { get; set; }
        public SweetConfig Config => SweetConfigCategory.Instance.Get(this.ConfigId);
        public float2 PosInGrid { get; set; }

        private float2 _realPos;
        public float2 RealPos
        {
            get => this._realPos;
            set
            {
                var oldPos = this._realPos;
                this._realPos = value;
                EventSystem.Instance.Publish(this.DomainScene(), new EventType.SweetChangePosition() { Sweet = this, OldPos = oldPos });
            }
        }

        public bool CanMoveDown => (int) this.PosInGrid.y != 5 && this.GetComponent<SweetMoveComponent>() != null; //不是最后一行就能移动

        public bool ViewInited;
    }
}

