using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CaughtSFX : MonoBehaviour
{
    [SerializeField] private AudioClip surprisedHuh;
    [SerializeField] private AudioClip failSting;
    [Range(0f, 1f)][SerializeField] private float volume = 0.9f;

    private AudioSource src;
    private bool playedFail;

    void Awake()
    {
        src = GetComponent<AudioSource>();
        src.playOnAwake = false;
        src.spatialBlend = 0f; // 2D
    }

    // Animation Event: put this near the start of the hands-up/caught anim
    public void PlaySurprisedHuh()
    {
        if (surprisedHuh) src.PlayOneShot(surprisedHuh, volume);
    }

    // Animation Event: put this right when "MISSION FAILED" should hit
    public void PlayFailSting()
    {
        if (playedFail) return;
        playedFail = true;

        if (failSting) src.PlayOneShot(failSting, volume);
    }
}
