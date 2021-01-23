using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMenager : MonoBehaviour
{
    private float sfxVolume = 0.6f;
    private float musicVolume = 0.6f;
    [SerializeField]
    private Slider sfxSlider;
    [SerializeField]
    private Slider musicSlider;
    private GameObject oneShotGameObject;
    private AudioSource oneShotAudioSource;
    private Dictionary<SoundTypesCategory, float> soundTimerDictionary;

    private void Start()
    {
        soundTimerDictionary = new Dictionary<SoundTypesCategory, float>();

        soundTimerDictionary[SoundTypesCategory.Pop] = 0f;
        soundTimerDictionary[SoundTypesCategory.MoabHit] = 0f;
        soundTimerDictionary[SoundTypesCategory.MetalHit] = 0f;
        soundTimerDictionary[SoundTypesCategory.CeramicHit] = 0f;

        musicVolume = PlayerPrefs.GetFloat("MusicVolume", musicVolume);
        sfxVolume = PlayerPrefs.GetFloat("SfxVolume", sfxVolume);

        sfxSlider.value = sfxVolume;
        musicSlider.value = musicVolume;
        AdjustMusicVolume(musicVolume);
    }

    public void PlaySound(SoundTypes sound)
    {
        if (CanPlaySound(sound))
        {
            if(oneShotGameObject == null)
            {
                oneShotGameObject= new GameObject("One Shot Sounds");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }

            oneShotAudioSource.volume = sfxVolume;
            oneShotAudioSource.PlayOneShot(GiveAudioClip(sound));
        }
    }

    public void AdjustSfxVolume(float _volume)
    {
        sfxVolume = _volume;
    }
    public void AdjustMusicVolume(float _volume)
    {
        musicVolume = _volume;
        Music.instance.AdjustVolume(musicVolume);
    }

    private bool CanPlaySound(SoundTypes sound)
    {
        switch (sound)
        {
            case SoundTypes.Pop1:
            case SoundTypes.Pop2:
            case SoundTypes.Pop3:
            case SoundTypes.Pop4:
            case SoundTypes.MoabHit1:
            case SoundTypes.MoabHit2:
            case SoundTypes.MoabHit3:
            case SoundTypes.MetalHit:
            case SoundTypes.CeramicHit:
                if (soundTimerDictionary.ContainsKey(GiveSoundTypesCategory(sound)))
                {
                    float lastTimePlayed = soundTimerDictionary[GiveSoundTypesCategory(sound)];
                    float PopSoundCoolDown = 0.05f; //jak często może pojawić się PoP sound
                    if (lastTimePlayed + PopSoundCoolDown < Time.time)
                    {
                        soundTimerDictionary[GiveSoundTypesCategory(sound)] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            default:
                return true;
        }
    }

    private AudioClip GiveAudioClip(SoundTypes sound)
    {
        foreach (var SoundAudioClip in SoundAudioClips)
        {
            if(SoundAudioClip.sound == sound)
            {
                return SoundAudioClip.audioClip;
            }
        }
        return null;
    }

    public float GiveSfxVolume()
    {
        return sfxVolume;
    }

    public float GiveMusicVolume()
    {
        return musicVolume;
    }

    private SoundTypesCategory GiveSoundTypesCategory(SoundTypes sound)
    {
        switch (sound)
        {
            case SoundTypes.Pop1:
            case SoundTypes.Pop2:
            case SoundTypes.Pop3:
            case SoundTypes.Pop4:
                return SoundTypesCategory.Pop;
            case SoundTypes.MoabHit1:
            case SoundTypes.MoabHit2:
            case SoundTypes.MoabHit3:
                return SoundTypesCategory.MoabHit;
            case SoundTypes.MetalHit:
                return SoundTypesCategory.MetalHit;
            case SoundTypes.CeramicHit:
                return SoundTypesCategory.CeramicHit;
            default:
                return SoundTypesCategory.NONE;
        }
    }

    public SoundAudioClip[] SoundAudioClips;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundTypes sound;
        public AudioClip audioClip;
    }
}
