using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET.Client
{
    [FriendOfAttribute(typeof (ET.GameSweetComponent))]
    public static class SweetPosHelper
    {
        public static float2 CorrectPosition(Scene currentScene, float2 pos)
        {
            var stage = currentScene.GetComponent<SweetStageComponent>();

            return new float2(-stage.Config.Size.X / 2 + pos.x + 0.5f, stage.Config.Size.Y / 2 - pos.y - 0.5f);
        }

        public static GameSweet FindGameSweetFromPointerEvent(PointerEventData data)
        {
            GameObject sweetObj = null;
            foreach (var gameObj in data.hovered)
            {
                if (gameObj.GetComponent<EntityBridge>())
                {
                    sweetObj = gameObj;
                    break;
                }
            }

            if (sweetObj != null)
            {
                var bridge = sweetObj.GetComponent<EntityBridge>();
                if (Root.Instance.Get(bridge.Entity.InstanceId) is GameSweet sweet)
                {
                    return sweet;
                }
            }

            return null;
        }

        public static void SwitchPos(Scene currentScene, GameSweet sweet1, GameSweet sweet2)
        {
            var sweets = currentScene.GetComponent<GameSweetComponent>();
            var pos1 = sweet1.PosInGrid.ToInt2();
            var pos2 = sweet2.PosInGrid.ToInt2();
            
            Log.Debug($"Switch {pos1} to {pos2}");

            sweet1.GetComponent<SweetMoveComponent>().MoveToAsync(pos2.x, pos2.y, 8).Coroutine();
            sweet2.GetComponent<SweetMoveComponent>().MoveToAsync(pos1.x, pos1.y, 8).Coroutine();

            sweets.Sweets[pos1.x, pos1.y] = sweet2;
            sweets.Sweets[pos2.x, pos2.y] = sweet1;

            sweet1.PosInGrid = pos2;
            sweet2.PosInGrid = pos1;
        }
    }
}