using UnityEngine;

public class FlashlightDetect : MonoBehaviour
{
    [SerializeField] float timeToBusted = 0.8f;
    float t;
    bool bustedSent;

    void OnTriggerStay2D(Collider2D other)
    {
        if (bustedSent) return;
        if (!other.CompareTag("Player")) return;

        t += Time.unscaledDeltaTime; // IMPORTANT
        if (t >= timeToBusted)
        {
            bustedSent = true;
            other.GetComponent<PlayerBusted>()?.Busted();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        t = 0f;
        bustedSent = false;
    }
}
