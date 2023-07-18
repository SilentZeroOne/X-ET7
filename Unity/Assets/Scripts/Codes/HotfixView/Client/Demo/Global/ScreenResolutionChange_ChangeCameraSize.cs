using ET.EventType;

namespace ET.Client
{
    [Event(SceneType.Process)]
    public class ScreenResolutionChange_ChangeCameraSize : AEvent<Scene,ScreenResolutionChange>
    {
        protected override async ETTask Run(Scene scene, ScreenResolutionChange a)
        {
            var originalSize = 5.358332f; //1080下的适配大小
            var resolutionRate = 1.777777778f;
            GlobalComponent.Instance.MainCamera.orthographicSize = (float)a.Resolution.y / a.Resolution.x /resolutionRate * originalSize;
            await ETTask.CompletedTask;
        }
    }
}