using ET.EventType;
using Unity.Mathematics;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class Combo_DisplayComboView : AEvent<Scene,Combo>
    {
        protected override async ETTask Run(Scene scene, Combo a)
        {
            Log.Debug($"Final Combo {a.ComboCount} currentSweetCount {a.CurrentClearSweetCount}");
            AudioComponent.Instance.PlayOneShot("Bubble");
            await TimerComponent.Instance.WaitAsync(1100);

            var oldScore = scene.GetComponent<NumericComponent>().GetAsLong(NumericType.CurrentScore);
            long score = oldScore + (long) math.floor((1 + ((a.ComboCount - 1) * 0.1f)) * (a.CurrentClearSweetCount * 100));
            scene.GetComponent<NumericComponent>().Set(NumericType.CurrentScore, score);
            
            scene.Parent.DomainScene().GetComponent<FUIComponent>().GetPanelLogic<StagePanel>().SetScore(score);
            SweetStageComponent sweetStageComponent = scene.GetComponent<SweetStageComponent>();
            if (sweetStageComponent?.Config.TargetScore <= score)
            {
                scene.Parent.DomainScene().GetComponent<FUIComponent>().GetPanelLogic<StagePanel>().SetController(GameStatus.Win);
                sweetStageComponent.Status = GameStatus.Win;
            }
            
            await ETTask.CompletedTask;
        }
    }
}