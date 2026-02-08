using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class KeycardReaderSFX : MonoBehaviour
{
    [SerializeField] private AudioClip cardInsertClip;
    [Range(0f, 1f)][SerializeField] private float insertVolume = 0.8f;

    // optional anti-spam if events are very close
    [SerializeField] private float minInterval = 0.08f;
    private float lastTime = -999f;

    private AudioSource src;

    private void Awake()
    {
        src = GetComponent<AudioSource>();
        src.playOnAwake = false;
        src.spatialBlend = 0f; // 2D
    }

    // Animation Event calls this (add it 3 times in the clip)
    public void PlayCardInsert()
    {
        if (!cardInsertClip) return;
        if (Time.time - lastTime < minInterval) return;

        lastTime = Time.time;
        src.PlayOneShot(cardInsertClip, insertVolume);
    }
}
