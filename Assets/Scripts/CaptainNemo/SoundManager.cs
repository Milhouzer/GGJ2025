using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public enum E_Sound
{
    GoodBubble,
    WrongBubble,
    WrongBubble2,
    Death,
    StarfishLegPop,
    Valve,
    BubbleDivide,
    BubbleMerge,
    PlayButton,
}

public class SoundManager : MonoBehaviour
{
    [Serializable]
    private struct EnumToSound
    {
        public E_Sound sound;
        public AudioResource audioResource;
    }

    [Header("References")]
    [SerializeField] private AudioSource ambientLoop2 = default;
    [SerializeField] private AudioSource ambientSudden = default;
    [Space(5)]
    [SerializeField] private AudioSource featureEffects = default;
    [SerializeField] private AudioSource characterEffects = default;
    [SerializeField] private AudioSource bubbleEffects = default;
    [SerializeField] private AudioSource uiEffects = default;
    [Space(5)]
    [SerializeField] private List<EnumToSound> sounds = default;

    [Header("Values")]
    [SerializeField] private List<AudioResource> submarinePoufs = default;
    [SerializeField] private Vector2 rangeNSecondsBetweenSubmarinePouf = new Vector2(8f, 10f);
    [SerializeField] private List<AudioResource> ambientsSudden = default;
    [SerializeField] private Vector2 rangeNSecondsBetweenAmbientsSudden = new Vector2(30f, 40f);
    
    private static SoundManager Instance;

    private void Awake()
    {
        Instance = this;

        StartAmbient();
    }

    #region Ambient
    public static void StartAmbient()
    {
        Instance.StartCoroutine(Instance.SubmarinePouf());
        Instance.StartCoroutine(Instance.AmbientsSudden());
    }

    public static void StopAmbient()
    {
        Instance.StopCoroutine(Instance.SubmarinePouf());
        Instance.StopCoroutine(Instance.AmbientsSudden());
    }

    private IEnumerator SubmarinePouf()
    {
        float time = UnityEngine.Random.Range(rangeNSecondsBetweenSubmarinePouf.x, rangeNSecondsBetweenSubmarinePouf.y);

        yield return new WaitForSeconds(time);

        int randomIndex = UnityEngine.Random.Range(0, submarinePoufs.Count - 1);

        ambientLoop2.resource = submarinePoufs[randomIndex];
        ambientLoop2.Play();

        StartCoroutine(SubmarinePouf());
    }

    private IEnumerator AmbientsSudden()
    {
        float time = UnityEngine.Random.Range(rangeNSecondsBetweenAmbientsSudden.x, rangeNSecondsBetweenAmbientsSudden.y);

        yield return new WaitForSeconds(time);

        int randomIndex = UnityEngine.Random.Range(0, ambientsSudden.Count - 1);

        ambientSudden.resource = ambientsSudden[randomIndex];
        ambientSudden.Play();

        StartCoroutine(AmbientsSudden());
    }
    #endregion

    public static void PlayRandomSound(List<E_Sound> sounds)
    {
        int randomIndex = UnityEngine.Random.Range(0, sounds.Count - 1);

        PlaySound(sounds[randomIndex]);
    }

    public static void PlaySoundRandomPitch(E_Sound soundToPlay, Vector2 range)
    {
        AudioSource audioSource = GetAudioSourceFromEnum(soundToPlay);
        audioSource.resource = GetSoundFromEnum(soundToPlay);
        audioSource.pitch = UnityEngine.Random.Range(range.x, range.y);
        audioSource.Play();
    }

    public static void PlaySound(E_Sound soundToPlay, bool loop = false)
    {
        AudioSource audioSource = GetAudioSourceFromEnum(soundToPlay);
        audioSource.resource = GetSoundFromEnum(soundToPlay);
        audioSource.loop = loop;
        audioSource.Play();
    }

    public static void StopSound(E_Sound soundToPlay)
    {
        AudioSource audioSource = GetAudioSourceFromEnum(soundToPlay);
        audioSource.Stop();
    }

    private static AudioSource GetAudioSourceFromEnum(E_Sound soundToPlay)
    {
        AudioSource audioSource = null;

        switch (soundToPlay)
        {
            case E_Sound.GoodBubble:
            case E_Sound.WrongBubble:
            case E_Sound.WrongBubble2:
            case E_Sound.Death:
                audioSource = Instance.characterEffects;
                break;
            case E_Sound.StarfishLegPop:
            case E_Sound.Valve:
                audioSource = Instance.featureEffects;
                break;
            case E_Sound.BubbleDivide:
            case E_Sound.BubbleMerge:
                audioSource = Instance.bubbleEffects;
                break;
            case E_Sound.PlayButton:
                audioSource = Instance.uiEffects;
                break;
        }

        return audioSource;
    }

    private static AudioResource GetSoundFromEnum(E_Sound soundToPlay)
    {
        foreach (var sound in Instance.sounds)
        {
            if (sound.sound == soundToPlay)
                return sound.audioResource;
        }

        return null;
    }
}
