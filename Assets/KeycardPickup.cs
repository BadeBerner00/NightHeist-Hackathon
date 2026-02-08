using UnityEngine;

public class KeycardPickup : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSfx;
    [Range(0f, 1f)][SerializeField] private float volume = 0.8f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Add to your count / manager
        KeycardManager.Instance.AddCard(1);

        // Play sound from the PLAYER so it doesn't get cut off when the keycard is destroyed
        var src = other.GetComponent<AudioSource>();
        if (src != null && pickupSfx != null)
            src.PlayOneShot(pickupSfx, volume);

        Destroy(gameObject);
    }
}
