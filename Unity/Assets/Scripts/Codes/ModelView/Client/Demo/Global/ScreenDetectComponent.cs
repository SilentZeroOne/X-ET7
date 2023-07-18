namespace ET
{
    [ComponentOf()]
    public class ScreenDetectComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}