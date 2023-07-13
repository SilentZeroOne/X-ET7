﻿using System;
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
            var sweet = self.DomainScene().GetComponent<SweetStageComponent>().CurrentDragSweet;
            if (sweet != null && sweet != self.Sweet)
            {
                SweetPosHelper.SwitchPos(self.DomainScene(), sweet, self.Sweet);
            }
        }

        private static void OnEndDragAction(this MonoActionsComponent self, PointerEventData obj)
        {
            var stageComponent = self.DomainScene().GetComponent<SweetStageComponent>();
            if (stageComponent.CurrentDragSweet != null)
                stageComponent.CurrentDragSweet = null;
            
            self.Sweet.GetComponent<SpriteRendererComponent>().SpriteRenderer.color = Color.white;
            if (self.TempDragObject != null)
            {
                self.TempDragObject.SetActive(false);
            }
        }

        private static void OnBeginDragAction(this MonoActionsComponent self, PointerEventData obj)
        {
            var stageComponent = self.DomainScene().GetComponent<SweetStageComponent>();
            stageComponent.CurrentDragSweet = self.Sweet;
            
            self.Sweet.GetComponent<SpriteRendererComponent>().SpriteRenderer.color = new Color(1, 1, 1, 0.4f);
            var tempObj = ResComponent.Instance.LoadAsset<GameObject>("TempSweetPrefab");

            if (self.TempDragObject == null)
                self.TempDragObject = UnityEngine.Object.Instantiate(tempObj, Vector3.zero, Quaternion.identity);

            self.TempDragObject.SetActive(true);
            
            var spriteRender = self.TempDragObject.GetComponentInChildren<SpriteRenderer>();
            if (spriteRender)
            {
                spriteRender.sprite = ResComponent.Instance.LoadAsset<Sprite>(self.Sweet.Config.SpriteName);
                spriteRender.sortingOrder = 1;
                spriteRender.color = Color.white;
            }
        }

        private static void OnClick(this MonoActionsComponent self, PointerEventData obj)
        {
            Log.Debug("Click" + self.Sweet.PosInGrid);
        }

        private static void OnDrag(this MonoActionsComponent self, PointerEventData obj)
        {
            //Log.Debug($"On Drag {self.Sweet.PosInGrid}");
            if (self.TempDragObject != null)
            {
                var pointerPos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
                self.TempDragObject.transform.position = new Vector3(pointerPos.x, pointerPos.y, 0);
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