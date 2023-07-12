using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET
{
    public class EntityBridge: MonoBehaviour, IDragHandler,IPointerClickHandler,IPointerDownHandler,IPointerUpHandler
    {
        public Entity Entity;
        public Action<PointerEventData> OnDragAction;
        public Action<PointerEventData> OnClickAction;
        public Action<PointerEventData> OnPointerDownAction;
        public Action<PointerEventData> OnPointerUpAction;
        
        public void OnDrag(PointerEventData eventData)
        {
            this.OnDragAction?.Invoke(eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            this.OnClickAction?.Invoke(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            this.OnPointerDownAction?.Invoke(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            this.OnPointerUpAction?.Invoke(eventData);
        }
    }
}

