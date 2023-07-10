using ET.EventType;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class AfterSweetCreate_CreateView : AEvent<Scene,AfterSweetCreate>
    {
        protected override async ETTask Run(Scene scene, AfterSweetCreate a)
        {
            GameSweet sweet = a.Sweet;

            GameObject prefab = await ResComponent.Instance.LoadAssetAsync<GameObject>(sweet.Config.PrefabName);

            GameObject sweetObj = UnityEngine.Object.Instantiate(prefab, GlobalComponent.Instance.Sweets);
            sweetObj.GetComponentInChildren<SpriteRenderer>().sprite = await ResComponent.Instance.LoadAssetAsync<Sprite>(sweet.Config.SpriteName);

            sweetObj.transform.position = sweet.Position.ToVector2();

            sweetObj.AddComponent<EntityBridge>().Entity = sweet;
            
            await ETTask.CompletedTask;
        }
    }
}