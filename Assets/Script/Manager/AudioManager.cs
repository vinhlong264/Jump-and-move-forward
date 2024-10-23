using UnityEngine;
using Extension;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundType type,int volume)
    {
        audioSource.PlayOneShot(audioClips[(int)type], volume);
    }
}

public enum SoundType
{
    JUMP,
    HIT,
    YOUWIN,
    YOULOSE
}
