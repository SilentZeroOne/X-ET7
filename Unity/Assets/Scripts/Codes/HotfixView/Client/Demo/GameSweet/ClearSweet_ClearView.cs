using ET.EventType;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class ClearSweet_ClearView : AEvent<Scene,ClearSweet>
    {
        protected override async ETTask Run(Scene scene, ClearSweet args)
        {
            var sweet = args.Sweet;
            if (!sweet.IsDisposed)
            {
                sweet.GetComponent<AnimatorComponent>().Play(MotionType.Destroy);
                await TimerComponent.Instance.WaitAsync(1000);
                sweet.Dispose();
            }
        }
    }
}