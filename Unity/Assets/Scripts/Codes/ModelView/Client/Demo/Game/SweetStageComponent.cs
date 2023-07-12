using UnityEngine;

namespace ET
{
    [ComponentOf()]
    public class SweetStageComponent: Entity, IAwake<string>, IDestroy
    {
        public StageConfig Config { get; set; }
        public float FillSpeed { get; set; }
    }
}

