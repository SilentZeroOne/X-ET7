using ET.EventType;
using Unity.Mathematics;

namespace ET.Client
{
    public static class SweetFactory
    {
        public static GameSweet Create(Scene currentScene, int sweetId, float2 pos)
        {
            GameSweetComponent sweetComponent = currentScene.GetComponent<GameSweetComponent>();
            GameSweet sweet = sweetComponent.AddChild<GameSweet, int>(sweetId);
            sweet.Position = pos;

            EventSystem.Instance.Publish(sweet.DomainScene(), new AfterSweetCreate() { Sweet = sweet });
            return sweet;
        }
    }
}