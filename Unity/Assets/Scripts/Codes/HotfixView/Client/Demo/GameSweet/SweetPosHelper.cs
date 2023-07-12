using Unity.Mathematics;

namespace ET.Client
{
    public static class SweetPosHelper
    {
        public static float2 CorrectPosition(Scene currentScene, float2 pos)
        {
            var stage = currentScene.GetComponent<SweetStageComponent>();

            return new float2(-stage.Config.Size.X / 2 + pos.x + 0.5f, stage.Config.Size.Y / 2 - pos.y - 0.5f);
        }
    }
}