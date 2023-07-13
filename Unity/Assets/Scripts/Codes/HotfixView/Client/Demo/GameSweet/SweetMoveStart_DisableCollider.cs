using ET;
using ET.EventType;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class SweetMoveStart_DisableCollider : AEvent<Scene,SweetMoveStart>
    {
        protected override async ETTask Run(Scene scene, SweetMoveStart a)
        {
            a.Sweet.GetComponent<GameObjectComponent>().GameObject.GetComponent<BoxCollider2D>().enabled = false;
            await ETTask.CompletedTask;
        }
    }
}