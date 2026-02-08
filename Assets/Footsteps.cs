using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip stepClip;   // single clip
    [Range(0f, 1f)][SerializeField] private float volume = 0.6f;

    [Header("Anti-spam")]
    [SerializeField] private float minStepInterval = 0.12f; // tweak: 0.10–0.18

    private float lastStepTime = -999f;

    private void Awake()
    {
        if (source == null) source = GetComponent<AudioSource>();
    }

    public void PlayStep()
    {
        if (stepClip == null || source == null) return;

        if (Time.time - lastStepTime < minStepInterval)
            return;

        lastStepTime = Time.time;
        source.PlayOneShot(stepClip, volume);
    }
}
