using ET.EventType;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class ReFillAll_FillView : AEvent<Scene,ReFillAll>
    {
        protected override async ETTask Run(Scene scene, ReFillAll a)
        {
            var stageComponent = scene.GetComponent<SweetStageComponent>();

            await stageComponent.AllFill();
        }
    }
}