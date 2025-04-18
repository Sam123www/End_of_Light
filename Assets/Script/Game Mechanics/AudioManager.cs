using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioMixer audioMixer;
    public AudioMixerGroup SFXGroup, BGMGroup;
    //[Header("Priority")]
    //AudioSource prioritySource;
    [Header("BGM")]
    AudioSource BGMSource;
    public AudioClip BGMClip;
    [Header("Player")]
    AudioSource playerMoveSource, playerAttackSource, playerLightSource;
    public AudioClip jumpClip, fallToGroundClip, attackClip, hurtEnemyClip, hurtClip, turnOnLightClip, turnOffLightClip;
    public AudioClip[] runClip;
    [Header("UI")]
    AudioSource UISource;
    public AudioClip selectClip, clickClip, enterGameClip;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //prioritySource = gameObject.AddComponent<AudioSource>();
        BGMSource = gameObject.AddComponent<AudioSource>();
        playerMoveSource = gameObject.AddComponent<AudioSource>();
        playerAttackSource = gameObject.AddComponent<AudioSource>();
        playerLightSource = gameObject.AddComponent<AudioSource>();
        UISource = gameObject.AddComponent<AudioSource>();

        BGMSource.outputAudioMixerGroup = BGMGroup;
        playerMoveSource.outputAudioMixerGroup = SFXGroup;
        playerAttackSource.outputAudioMixerGroup = SFXGroup;
        playerLightSource.outputAudioMixerGroup = SFXGroup;
        UISource.outputAudioMixerGroup = SFXGroup;
    }
    private void Start()
    {
        if (BGMClip != null)
        {
            PlayBGM();
        }
    }
    /*public static void MuteAll()
    {
        AudioSource[] allSource = instance.GetComponents<AudioSource>();
        foreach (AudioSource source in allSource)
        {
            source.mute = true;
        }
        instance.prioritySource.mute = false;
    }*/
    public static void PlayBGM()
    {
        instance.BGMSource.clip = instance.BGMClip;
        instance.BGMSource.loop = true;
        instance.BGMSource.Play();
    }
    public static void PlayRunAudio()
    {
        if (instance.playerMoveSource.isPlaying) return;
        int index = Random.Range(0, instance.runClip.Length);
        instance.playerMoveSource.clip = instance.runClip[index];
        instance.playerMoveSource.Play();
    }
    public static void PlayJumpAudio()
    {
        instance.playerMoveSource.clip = instance.jumpClip;
        instance.playerMoveSource.Play();
    }
    public static void PlayFallToGroundAudio()
    {
        instance.playerMoveSource.clip = instance.fallToGroundClip;
        instance.playerMoveSource.Play();
    }
    public static void PlayAttackAudio()
    {
        instance.playerAttackSource.clip = instance.attackClip;
        instance.playerAttackSource.Play();
    }
    public static void PlayHurtEnemyAudio()
    {
        instance.playerAttackSource.clip = instance.hurtEnemyClip;
        instance.playerAttackSource.Play();
    }
    public static void PlayHurtAudio()
    {
        instance.playerAttackSource.clip = instance.hurtClip;
        instance.playerAttackSource.Play();
    }
    public static void PlayTurnOnLightAudio()
    {
        instance.playerLightSource.clip = instance.turnOnLightClip;
        instance.playerLightSource.Play();
    }
    public static void PlayTurnOffLightAudio()
    {
        instance.playerLightSource.clip = instance.turnOffLightClip;
        instance.playerLightSource.Play();
    }
    /*public static void PlayDieAudio()
    {
        instance.prioritySource.clip = instance.hurtClip;
        instance.prioritySource.Play();
    }*/
    public static void PlayButtonSelect()
    {
        instance.UISource.clip = instance.selectClip;
        instance.UISource.Play();
    }
    public static void PlayButtonClick()
    {
        instance.UISource.clip = instance.clickClip;
        instance.UISource.Play();
    }
    public static void PlayEnterGame()
    {
        instance.UISource.clip = instance.enterGameClip;
        instance.UISource.Play();
    }
}
