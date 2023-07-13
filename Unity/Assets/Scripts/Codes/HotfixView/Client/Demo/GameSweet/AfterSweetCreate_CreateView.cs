using ET.EventType;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    [FriendOfAttribute(typeof(ET.GameSweet))]
    public class AfterSweetCreate_CreateView : AEvent<Scene, AfterSweetCreate>
    {
        protected override async ETTask Run(Scene scene, AfterSweetCreate a)
        {
            GameSweet sweet = a.Sweet;

            GameObject prefab = await ResComponent.Instance.LoadAssetAsync<GameObject>(sweet.Config.PrefabName);
            GameObject sweetObj =
                    UnityEngine.Object.Instantiate(prefab, new Vector3(0, -100, 0), Quaternion.identity, GlobalComponent.Instance.Sweets);
            if (sweet is { IsDisposed: false })
            {
                sweet.AddComponent<GameObjectComponent>().GameObject = sweetObj;

                if (sweet.Config.Type != (int)SweetType.Empty)
                {
                    var spriteRenderer = sweetObj.GetComponentInChildren<SpriteRenderer>();
                    if (spriteRenderer)
                    {
                        spriteRenderer.sprite = await ResComponent.Instance.LoadAssetAsync<Sprite>(sweet.Config.SpriteName);
                        sweet.AddComponent<SpriteRendererComponent>().SpriteRenderer = spriteRenderer;
                    }
                    
                    sweet.AddComponent<SweetMoveComponent>();
                }

                sweet.RealPos = SweetPosHelper.CorrectPosition(scene, sweet.PosInGrid);
                sweetObj.transform.position = sweet.RealPos.ToVector2();

                sweetObj.AddComponent<EntityBridge>().Entity = sweet;
                sweet.AddComponent<MonoActionsComponent>();

                sweet.ViewInited = true;
            }
            else
            {
                UnityEngine.Object.Destroy(sweetObj);
            }
        }
    }
}