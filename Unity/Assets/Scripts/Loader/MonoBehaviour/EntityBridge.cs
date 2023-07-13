using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET
{
    public class EntityBridge: MonoBehaviour, IDragHandler,IPointerClickHandler,IPointerDownHandler,IPointerUpHandler,
            IBeginDragHandler,IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
    {
        public Entity Entity;
        public Action<PointerEventData> OnDragAction;
        public Action<PointerEventData> OnBeginDragAction;
        public Action<PointerEventData> OnEndDragAction;
        
        public Action<PointerEventData> OnClickAction;
        public Action<PointerEventData> OnPointerDownAction;
        public Action<PointerEventData> OnPointerUpAction;
        public Action<PointerEventData> OnPointerEnterAction;
        public Action<PointerEventData> OnPointerExitAction;
        
        
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

        public void OnBeginDrag(PointerEventData eventData)
        {
            this.OnBeginDragAction?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            this.OnEndDragAction?.Invoke(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            this.OnPointerEnterAction?.Invoke(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            this.OnPointerExitAction?.Invoke(eventData);
        }
    }
}

