using System;
using UnityEngine.EventSystems;

namespace ET.Client
{
    public class MonoActionsComponentAwakeSystem: AwakeSystem<MonoActionsComponent>
    {
        protected override void Awake(MonoActionsComponent self)
        {
            self.RegisterActions();
        }
    }

    public class MonoActionsComponentDestroySystem: DestroySystem<MonoActionsComponent> 
    {
        protected override void Destroy(MonoActionsComponent self)
        {

        }
    }

    public static class MonoActionsComponentSystem
    {
        public static void RegisterActions(this MonoActionsComponent self)
        {
            self.GameObject = self.GetParent<GameSweet>().GetComponent<GameObjectComponent>().GameObject;
            var entityBridge = self.GameObject.GetComponent<EntityBridge>();
            if(!entityBridge) return;

            entityBridge.OnDragAction += self.OnDrag;
            entityBridge.OnClickAction += self.OnClick;
            entityBridge.OnPointerDownAction += self.OnPointerDown;
            entityBridge.OnPointerUpAction += self.OnPointerUp;
        }

        private static void OnClick(this MonoActionsComponent self,PointerEventData obj)
        {
            Log.Debug("Click"+ self.Parent.InstanceId.ToString());
        }

        private static void OnDrag(this MonoActionsComponent self,PointerEventData obj)
        {
            Log.Debug("Drag"+ self.Parent.InstanceId.ToString());
        }
        
        private static void OnPointerDown(this MonoActionsComponent self,PointerEventData obj)
        {
            
        }
        
        private static void OnPointerUp(this MonoActionsComponent self,PointerEventData obj)
        {
            
        }
    }
}