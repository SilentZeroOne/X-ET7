using ET.EventType;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class CurrentTurnChange_ShowUI : AEvent<Scene,CurrentTurnChange>
    {
        protected override async ETTask Run(Scene scene, CurrentTurnChange a)
        {
            scene.Parent.DomainScene().GetComponent<FUIComponent>().GetPanelLogic<StagePanel>()?.SetTurn(a.CurrentTurn);
            await ETTask.CompletedTask;
        }
    }
}