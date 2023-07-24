using ET.EventType;
using UnityEngine;

namespace ET
{
    [ComponentOf()]
    public class SweetStageComponent: Entity, IAwake<string>, IDestroy
    {
        public StageConfig Config { get; set; }
        public float FillSpeed { get; set; }

        private int _currentTurn;
        public int CurrentTurn
        {
            get => this._currentTurn;
            set
            {
                if(this.IsDisposed) return;
                
                this._currentTurn = value;
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
            get => this._canOperate && this._currentTurn > 0 && this._currentDragTime > 0 && this.Status == GameStatus.Progressing;
            set
            {
                if(this.IsDisposed) return;
                
                this._canOperate = value;
                EventSystem.Instance.Publish(this.DomainScene(), new StageCanOperate() { CanOperate = value });
            }
        }
        
        private int _currentDragTime;
        public int CurrentDragTime
        {
            get => this._currentDragTime;
            set
            {
                if(this.IsDisposed) return;
                
                this._currentDragTime = value;
                EventSystem.Instance.Publish(this.DomainScene(), new CurrentDragTimeChange() { CurrentDragTime = value });
            }
        }

        public GameStatus Status { get; set; }
    }
}

