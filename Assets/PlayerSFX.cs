using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSFX : MonoBehaviour
{
    [SerializeField] private AudioClip landClip;
    [Range(0f, 1f)][SerializeField] private float landVolume = 0.8f;

    private AudioSource src;

    void Awake() => src = GetComponent<AudioSource>();

    // Called by Animation Event
    public void PlayLand()
    {
        if (landClip == null) return;
        src.PlayOneShot(landClip, landVolume);
    }
}
