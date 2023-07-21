using ET.EventType;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class CurrentDragTimeChange_ShowUI : AEvent<Scene,CurrentDragTimeChange>
    {
        protected override async ETTask Run(Scene scene, CurrentDragTimeChange a)
        {
            scene.Parent.DomainScene().GetComponent<FUIComponent>().GetPanelLogic<StagePanel>().SetTimeSlider(a.CurrentDragTime);
        }
    }
}