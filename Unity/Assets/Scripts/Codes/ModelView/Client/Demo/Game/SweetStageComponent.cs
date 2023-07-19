using ET.EventType;
using UnityEngine;

namespace ET
{
    [ComponentOf()]
    public class SweetStageComponent: Entity, IAwake<string>, IDestroy
    {
        public StageConfig Config { get; set; }
        public float FillSpeed { get; set; }

        private int _curremtTurn;
        public int CurrentTurn
        {
            get => this._curremtTurn;
            set
            {
                this._curremtTurn = value;
                EventSystem.Instance.Publish(this.DomainScene(), new CurrentTurnChange() { CurrentTurn = value });
            } 
        }

        private EntityRef<GameSweet> _currentDragSweet;

        public GameSweet CurrentDragSweet
        {
            get => this._currentDragSweet;
            set => this._currentDragSweet = value;
        }

        public GameObject TempDragObject { get; set; }

        private bool _canOperate;

        public bool CanOperate
        {
            get => this._canOperate;
            set
            {
                this._canOperate = value;
                EventSystem.Instance.Publish(this.DomainScene(), new StageCanOperate() { CanOperate = value });
            }
        }
    }
}

