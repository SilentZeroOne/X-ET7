using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET
{
    [ComponentOf(typeof(GameSweet))]
    public class MonoActionsComponent: Entity, IAwake, IDestroy
    {
        public GameObject GameObject { get; set; }

        private EntityRef<GameSweet> _sweet;

        public GameSweet Sweet
        {
            get => this._sweet;
            set
            {
                this._sweet = value;
            }
        }
    }
}