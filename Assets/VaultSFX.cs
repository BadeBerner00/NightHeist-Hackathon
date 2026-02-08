using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VaultSFX : MonoBehaviour
{
    [SerializeField] private AudioClip vaultOpenClip;
    [Range(0f, 1f)][SerializeField] private float volume = 0.9f;

    private AudioSource src;
    private bool played;

    private void Awake()
    {
        src = GetComponent<AudioSource>();
        src.playOnAwake = false;
        src.spatialBlend = 0f; // 2D
    }

    // Animation Event on the VaultOpen clip
    public void PlayVaultOpen()
    {
        if (played) return;   // prevents double triggers
        played = true;

        if (vaultOpenClip)
            src.PlayOneShot(vaultOpenClip, volume);
    }
}
