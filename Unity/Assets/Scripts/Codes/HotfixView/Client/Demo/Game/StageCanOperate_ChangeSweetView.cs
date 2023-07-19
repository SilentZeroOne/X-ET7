using ET.EventType;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    [FriendOfAttribute(typeof(ET.GameSweetComponent))]
    [FriendOfAttribute(typeof(ET.Client.AnimatorComponent))]
    public class StageCanOperate_ChangeSweetView : AEvent<Scene, StageCanOperate>
    {
        protected override async ETTask Run(Scene scene, StageCanOperate a)
        {
            // var gameSweet = scene.GetComponent<GameSweetComponent>();
            // if (a.CanOperate)
            // {
            //     foreach (var sweet in gameSweet.Sweets)
            //     {
            //         if (sweet.Config.Type != (int)SweetType.Empty)
            //             sweet.GetComponent<AnimatorComponent>().Animator.enabled = true;
            //     }
            // }
            // else
            // {
            //     foreach (var sweet in gameSweet.Sweets)
            //     {
            //         if (sweet.Config.Type != (int) SweetType.Empty)
            //         {
            //             sweet.GetComponent<AnimatorComponent>().Animator.enabled = false;
            //             sweet.GetComponent<SpriteRendererComponent>().SpriteRenderer.color = new Color(1, 1, 1, 0.4f);
            //         }
            //     }
            // }
            await ETTask.CompletedTask;
        }
    }
}