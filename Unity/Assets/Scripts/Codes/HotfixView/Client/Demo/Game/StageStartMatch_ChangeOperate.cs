using ET.EventType;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class StageStartMatch_ChangeOperate : AEvent<Scene,StageStartMatch>
    {
        protected override async ETTask Run(Scene scene, StageStartMatch a)
        {
            scene.GetComponent<SweetStageComponent>().CanOperate = !a.StartMatch;
            await ETTask.CompletedTask;
        }
    }
}