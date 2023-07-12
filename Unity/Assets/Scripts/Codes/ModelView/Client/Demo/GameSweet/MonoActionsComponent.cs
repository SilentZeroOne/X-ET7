using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET
{
    [ComponentOf()]
    public class MonoActionsComponent: Entity, IAwake, IDestroy
    {
        public GameObject GameObject { get; set; }
    }
}