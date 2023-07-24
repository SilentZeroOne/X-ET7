using ET.EventType;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class StageStartMatch_ChangeOperate : AEvent<Scene,StageStartMatch>
    {
        protected override async ETTask Run(Scene scene, StageStartMatch a)
        {
            var stageComponent = scene.GetComponent<SweetStageComponent>();
            stageComponent.CanOperate = !a.StartMatch;
            var currentScore = scene.GetComponent<NumericComponent>().GetAsLong(NumericType.CurrentScore);
            if (!a.StartMatch) //所有掉落结束
            {
                if (stageComponent.CurrentTurn == 0 && currentScore< stageComponent.Config.TargetScore)
                {
                    stageComponent.Parent.Parent.DomainScene().GetComponent<FUIComponent>().GetPanelLogic<StagePanel>().SetController(GameStatus.Over);
                    stageComponent.Status = GameStatus.Over;
                }
            }
            await ETTask.CompletedTask;
        }
    }
}