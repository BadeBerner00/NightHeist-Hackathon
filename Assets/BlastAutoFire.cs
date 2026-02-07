using System.Collections;
using UnityEngine;

public class BlasterAutoFire : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] Animator animator;
    [SerializeField] string triggerName = "Fire";
    [SerializeField] float interval = 5f;

    [Header("Laser Hitbox")]
    [SerializeField] Collider2D laserCollider;
    [SerializeField] float laserActiveTime = 2f; // <-- set this to match beam duration
    [SerializeField] bool startLaserOff = true;

    void Awake()
    {
        if (!animator) animator = GetComponent<Animator>();
        if (!laserCollider) laserCollider = GetComponentInChildren<Collider2D>(true);
        if (laserCollider && startLaserOff) laserCollider.enabled = false;
    }

    void OnEnable() => StartCoroutine(FireLoop());
    void OnDisable() => StopAllCoroutines();

    IEnumerator FireLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            animator.SetTrigger(triggerName);

            if (laserCollider) laserCollider.enabled = true;
            yield return new WaitForSeconds(laserActiveTime);
            if (laserCollider) laserCollider.enabled = false;
        }
    }
}
