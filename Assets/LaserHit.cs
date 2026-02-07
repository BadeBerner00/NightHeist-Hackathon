using UnityEngine;

public class LaserHit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        other.GetComponent<PlayerBusted>()?.Busted();
    }
}
