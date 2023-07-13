using ET.EventType;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class SweetMoveEnd_EnableCollider : AEvent<Scene ,SweetMoveStop>
    {
        protected override async ETTask Run(Scene scene, SweetMoveStop a)
        {
            a.Sweet.GetComponent<GameObjectComponent>().GameObject.GetComponent<BoxCollider2D>().enabled = true;
            await ETTask.CompletedTask;
        }
    }
}