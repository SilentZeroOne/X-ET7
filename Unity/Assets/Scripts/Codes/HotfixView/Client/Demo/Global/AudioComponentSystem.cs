using UnityEngine;
using UnityEngine.Audio;

namespace ET.Client
{
    public class AudioComponentAwakeSystem: AwakeSystem<AudioComponent>
    {
        protected override void Awake(AudioComponent self)
        {
            AudioComponent.Instance = self;

            self.Mixer = ResComponent.Instance.LoadAsset<AudioMixer>("Main");
            self.SetBackgroundVolume(PlayerPrefs.GetFloat("sweetgame.backgroundVolume", 100) / 100);
            self.SetSFXVolume(PlayerPrefs.GetFloat("sweetgame.sfxVolume", 100) / 100);
            
            self.BackgroundSource = GameObject.Find("/Global/AudioManager/Background").GetComponent<AudioSource>();
            self.BackgroundSource.outputAudioMixerGroup = self.Mixer.FindMatchingGroups("Background")[0];
            self.SFXSource = GameObject.Find("/Global/AudioManager/SFX").GetComponent<AudioSource>();
            self.SFXSource.outputAudioMixerGroup = self.Mixer.FindMatchingGroups("SFX")[0];
        }
    }

    public class AudioComponentDestroySystem: DestroySystem<AudioComponent>
    {
        protected override void Destroy(AudioComponent self)
        {

        }
    }

    public static class AudioComponentSystem
    {
        public static void PlayInBackground(this AudioComponent self, string cilpName)
        {
            self.BackgroundSource.clip = ResComponent.Instance.LoadAsset<AudioClip>(cilpName);
            self.BackgroundSource.Play();
        }

        public static void PlayOneShot(this AudioComponent self, string clipName)
        {
            self.SFXSource.PlayOneShot(ResComponent.Instance.LoadAsset<AudioClip>(clipName));
        }

        public static void SetBackgroundVolume(this AudioComponent self,float volume)
        {
            var value = Mathf.Lerp(-70, 0, volume);
            Log.Debug($"Set back {value}");
            self.Mixer.SetFloat("Background", value);
        }
        
        public static void SetSFXVolume(this AudioComponent self,float volume)
        {
            var value = Mathf.Lerp(-70, 0, volume);
            self.Mixer.SetFloat("SFX", value);
        }
    }
}