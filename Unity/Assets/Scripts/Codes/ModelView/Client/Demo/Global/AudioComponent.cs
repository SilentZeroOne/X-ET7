using UnityEngine;
using UnityEngine.Audio;

namespace ET
{
    [ComponentOf()]
    public class AudioComponent: Entity, IAwake, IDestroy
    {
        [StaticField]
        public static AudioComponent Instance;

        public AudioMixer Mixer { get; set; }
        
        public AudioSource BackgroundSource { get; set; }
        public AudioSource SFXSource { get; set; }
    }
}