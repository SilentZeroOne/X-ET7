using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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
    [FriendOfAttribute(typeof(ET.Client.AnimatorComponent))]
    public static class MonoActionsComponentSystem
    {
        public static void RegisterActions(this MonoActionsComponent self)
        {
            self.Sweet = self.GetParent<GameSweet>();
            self.GameObject = self.Sweet.GetComponent<GameObjectComponent>().GameObject;
            var entityBridge = self.GameObject.GetComponent<EntityBridge>();
            if (!entityBridge) return;

            entityBridge.OnDragAction += self.OnDrag;
            entityBridge.OnBeginDragAction += self.OnBeginDragAction;
            entityBridge.OnEndDragAction += self.OnEndDragAction;

            entityBridge.OnClickAction += self.OnClick;
            entityBridge.OnPointerDownAction += self.OnPointerDown;
            entityBridge.OnPointerUpAction += self.OnPointerUp;
            entityBridge.OnPointerEnterAction += self.OnPointerEnterAction;
            entityBridge.OnPointerExitAction += self.OnPointerExitAction;
        }

        private static void OnPointerExitAction(this MonoActionsComponent self, PointerEventData obj)
        {

        }

        private static void OnPointerEnterAction(this MonoActionsComponent self, PointerEventData obj)
        {
            var stageComponent = self.DomainScene().GetComponent<SweetStageComponent>();
            if (!stageComponent.CanOperate) return;

            var sweet = stageComponent.CurrentDragSweet;
            if (sweet != null && sweet != self.Sweet)
            {
                SweetPosHelper.SwitchPos(self.DomainScene(), sweet, self.Sweet);
            }
        }

        private static void OnEndDragAction(this MonoActionsComponent self, PointerEventData obj)
        {
            var stageComponent = self.DomainScene().GetComponent<SweetStageComponent>();
            if (!stageComponent.CanOperate) return;

            if (stageComponent.CurrentDragSweet != null)
                stageComponent.CurrentDragSweet = null;

            self.Sweet.GetComponent<AnimatorComponent>().Animator.enabled = true;
            //self.Sweet.GetComponent<SpriteRendererComponent>().SpriteRenderer.color = Color.white;
            Log.Debug($"End {self.Sweet.GetComponent<SpriteRendererComponent>().SpriteRenderer.color}");
            if (stageComponent.TempDragObject != null)
            {
                stageComponent.TempDragObject.SetActive(false);
            }

            var gameSweet = self.DomainScene().GetComponent<GameSweetComponent>();
            gameSweet?.Match();

            stageComponent.CurrentTurn--;
        }

        private static void OnBeginDragAction(this MonoActionsComponent self, PointerEventData obj)
        {
            var stageComponent = self.DomainScene().GetComponent<SweetStageComponent>();
            if (!stageComponent.CanOperate) return;

            stageComponent.CurrentDragSweet = self.Sweet;
            self.Sweet.GetComponent<AnimatorComponent>().Animator.enabled = false;
            self.Sweet.GetComponent<SpriteRendererComponent>().SpriteRenderer.color = new Color(1, 1, 1, 0.4f);

            var tempObj = ResComponent.Instance.LoadAsset<GameObject>("TempSweetPrefab");

            if (stageComponent.TempDragObject == null)
                stageComponent.TempDragObject = UnityEngine.Object.Instantiate(tempObj, Vector3.zero, Quaternion.identity);

            stageComponent.TempDragObject.SetActive(true);

            var spriteRender = stageComponent.TempDragObject.GetComponentInChildren<SpriteRenderer>();
            if (spriteRender)
            {
                spriteRender.sprite = ResComponent.Instance.LoadAsset<Sprite>(self.Sweet.Config.SpriteName);
                spriteRender.sortingOrder = 1;
            }
        }

        private static void OnClick(this MonoActionsComponent self, PointerEventData obj)
        {
            Log.Debug("Click" + self.Sweet.PosInGrid);
        }

        private static void OnDrag(this MonoActionsComponent self, PointerEventData obj)
        {
            var stageComponent = self.DomainScene().GetComponent<SweetStageComponent>();
            if (!stageComponent.CanOperate) return;

            //Log.Debug($"On Drag {self.Sweet.PosInGrid}");
            if (stageComponent.TempDragObject != null)
            {
                var pointerPos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
                stageComponent.TempDragObject.transform.position = new Vector3(pointerPos.x, pointerPos.y, 0);
            }
        }

        private static void OnPointerDown(this MonoActionsComponent self, PointerEventData obj)
        {
        }

        private static void OnPointerUp(this MonoActionsComponent self, PointerEventData obj)
        {
        }
    }
}