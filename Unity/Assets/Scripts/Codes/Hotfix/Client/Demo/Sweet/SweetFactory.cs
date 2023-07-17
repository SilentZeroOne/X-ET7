using ET.EventType;
using Unity.Mathematics;

namespace ET.Client
{
    public static class SweetFactory
    {
        public static GameSweet Create(Scene currentScene, int sweetId, float2 pos)
        {
            GameSweetComponent sweetComponent = currentScene.GetComponent<GameSweetComponent>();
            GameSweet sweet = sweetComponent.Add(sweetId, pos);
            sweet.PosInGrid = pos;

            sweet.AddComponent<ObjectWait>();
            
            EventSystem.Instance.Publish(sweet.DomainScene(), new AfterSweetCreate() { Sweet = sweet });
            return sweet;
        }
        
        public static async ETTask<GameSweet> CreateAsync(Scene currentScene, int sweetId, float2 pos)
        {
            GameSweetComponent sweetComponent = currentScene.GetComponent<GameSweetComponent>();
            GameSweet sweet = sweetComponent.Add(sweetId, pos);
            sweet.PosInGrid = pos;

            sweet.AddComponent<ObjectWait>();
            
            await EventSystem.Instance.PublishAsync(sweet.DomainScene(), new AfterSweetCreate() { Sweet = sweet });
            return sweet;
        }
        
        public static async ETTask CreateAsyncNoReturn(Scene currentScene, int sweetId, float2 pos)
        {
            GameSweetComponent sweetComponent = currentScene.GetComponent<GameSweetComponent>();
            GameSweet sweet = sweetComponent.Add(sweetId, pos);
            sweet.PosInGrid = pos;

            sweet.AddComponent<ObjectWait>();
            
            await EventSystem.Instance.PublishAsync(sweet.DomainScene(), new AfterSweetCreate() { Sweet = sweet });
        }
    }
}