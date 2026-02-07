using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] Transform target;

    [Header("Follow")]
    [SerializeField] Vector2 offset = new Vector2(0f, 1.0f);
    [SerializeField] float smoothTime = 0.15f;
    [SerializeField] bool followX = true;

    [Header("Clamp (optional)")]
    [SerializeField] bool useClamp = false;
    [SerializeField] Vector2 minPos;
    [SerializeField] Vector2 maxPos;

    Vector3 velocity;
    float fixedY;

    void Awake()
    {
        fixedY = transform.position.y; // keep the original game view height
    }

    void LateUpdate()
    {
        if (!target) return;

        Vector3 current = transform.position;
        Vector3 desired = current;

        if (followX) desired.x = target.position.x + offset.x;

        desired.y = fixedY;     // <- locked Y
        desired.z = current.z;  // keep camera z

        Vector3 smoothed = Vector3.SmoothDamp(current, desired, ref velocity, smoothTime);

        if (useClamp)
        {
            smoothed.x = Mathf.Clamp(smoothed.x, minPos.x, maxPos.x);
            // keep Y fixed even if clamping is enabled
            smoothed.y = fixedY;
        }

        transform.position = smoothed;
    }
}
