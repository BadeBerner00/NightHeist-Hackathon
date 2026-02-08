using UnityEngine;

public class JumpSFX : MonoBehaviour
{
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip jumpClip;
    [Range(0f, 1f)][SerializeField] private float volume = 0.7f;

    void Awake()
    {
        if (!sfxSource) sfxSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // If you use Input Manager "Jump"
        if (Input.GetButtonDown("Jump"))  // or Input.GetKeyDown(KeyCode.Space)
        {
            if (jumpClip) sfxSource.PlayOneShot(jumpClip, volume);
        }
    }
}
