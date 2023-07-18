using ET.EventType;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    public class ScreenDetectComponentAwakeSystem: AwakeSystem<ScreenDetectComponent>
    {
        protected override void Awake(ScreenDetectComponent self)
        {

        }
    }

    public class ScreenDetectComponentDestroySystem: DestroySystem<ScreenDetectComponent>
    {
        protected override void Destroy(ScreenDetectComponent self)
        {

        }
    }
    
    public class ScreenDetectComponentUpdateSystem: UpdateSystem<ScreenDetectComponent>
    {
        protected override void Update(ScreenDetectComponent self)
        {
            self.Update();
        }
    }

    public static class ScreenDetectComponentSystem
    {
        public static void Update(this ScreenDetectComponent self)
        {
            if (Screen.height != self.Height || Screen.width != self.Width)
            {
                self.Height = Screen.height;
                self.Width = Screen.width;
                
                //更新屏幕信息
                Log.Debug($"Screen Resolution {self.Width}:{self.Height}");
                EventSystem.Instance.Publish(self.DomainScene(), new ScreenResolutionChange() { Resolution = new int2(self.Width, self.Height) });
            }
        }
    }
}