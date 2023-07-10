using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET
{
    public class EntityBridge: MonoBehaviour, IDragHandler
    {
        public Entity Entity;
        public Action<PointerEventData> OnDragAction;
        
        public void OnDrag(PointerEventData eventData)
        {
            this.OnDragAction?.Invoke(eventData);
        }
    }
}

