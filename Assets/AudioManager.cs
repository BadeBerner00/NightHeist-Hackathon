using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager I;

    [SerializeField] private AudioSource sfx;

    [Header("Clips")]
    public AudioClip pickup;
    public AudioClip insert;
    public AudioClip vaultOpen;
    public AudioClip win;

    private void Awake()
    {
        if (I != null && I != this) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);

        if (sfx == null) sfx = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip, float volume = 1f)
    {
        if (clip == null || sfx == null) return;
        sfx.PlayOneShot(clip, volume);
    }
}
