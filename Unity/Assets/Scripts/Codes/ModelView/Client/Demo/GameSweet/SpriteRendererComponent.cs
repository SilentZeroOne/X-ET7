using UnityEngine;

namespace ET
{
    [ComponentOf()]
    public class SpriteRendererComponent: Entity, IAwake, IDestroy
    {
        public SpriteRenderer SpriteRenderer { get; set; }
    }
}