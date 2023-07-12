using ET.EventType;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class SweetChangePos_SyncObjPos: AEvent<Scene,SweetChangePosition>
    {
        protected override async ETTask Run(Scene scene, SweetChangePosition a)
        {
            GameSweet sweet = a.Sweet;
            GameObjectComponent gameObjectComponent = sweet.GetComponent<GameObjectComponent>();
            if (gameObjectComponent == null)
            {
                Log.Error($"GameObjectComponent is null Can't Move ID {sweet.InstanceId}");
                return;
            }

            Transform transform = gameObjectComponent.GameObject.transform;
            transform.position = SweetPosHelper.CorrectPosition(scene, sweet.RealPos).ToVector2();

            await ETTask.CompletedTask;
        }
    }
}