using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PatrolBetweenPoints2D_RB : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    public float speed = 2f;
    public float arriveDistance = 0.05f;
    public bool flipOnTurn = true;

    [Header("Flashlight (triangle)")]
    public Transform flashlight;                 // triangle object
    public bool mirrorFlashlightPosition = true; // flip localPosition.x
    public float flashlightTiltZ = -20.334f;     // right-facing tilt (from your inspector)

    Rigidbody2D _rb;
    Transform _target;
    SpriteRenderer _sr;
    SpriteRenderer _flashSR;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _sr = GetComponentInChildren<SpriteRenderer>();
        if (_sr == null) _sr = GetComponent<SpriteRenderer>();

        if (flashlight) _flashSR = flashlight.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // choose initial target
        _target = pointB;
        if (pointA != null && pointB != null)
        {
            float da = (transform.position - pointA.position).sqrMagnitude;
            float db = (transform.position - pointB.position).sqrMagnitude;
            _target = (da > db) ? pointA : pointB;
        }

        // apply correct facing immediately at level start
        ApplyInitialFacing();
    }

    void FixedUpdate()
    {
        if (pointA == null || pointB == null) return;

        Vector2 pos = _rb.position;
        float targetX = _target.position.x;

        float dir = Mathf.Sign(targetX - pos.x);
        float vx = dir * speed;

        // switch target
        if (Mathf.Abs(targetX - pos.x) <= arriveDistance)
        {
            _target = (_target == pointA) ? pointB : pointA;

            targetX = _target.position.x;
            dir = Mathf.Sign(targetX - pos.x);
            vx = dir * speed;
        }

        _rb.linearVelocity = new Vector2(vx, _rb.linearVelocity.y);

        if (flipOnTurn && Mathf.Abs(vx) > 0.001f)
        {
            bool movingRight = vx > 0f;

            // cop sprite flip
            if (_sr != null)
                _sr.flipX = !movingRight; // if wrong, swap to: _sr.flipX = movingRight;

            // flashlight triangle follow
            ApplyFlashlightFacing(movingRight);
        }
    }

    void ApplyInitialFacing()
    {
        if (_target == null) return;

        float dir = Mathf.Sign(_target.position.x - transform.position.x);
        bool movingRight = dir >= 0f;

        if (_sr != null)
            _sr.flipX = !movingRight; // if wrong, swap

        ApplyFlashlightFacing(movingRight);
    }

    void ApplyFlashlightFacing(bool movingRight)
    {
        if (!flashlight) return;

        // keep flashlight on correct side
        if (mirrorFlashlightPosition)
        {
            Vector3 p = flashlight.localPosition;
            p.x = Mathf.Abs(p.x) * (movingRight ? 1f : -1f);
            flashlight.localPosition = p;
        }

        // keep the SAME tilt look:
        // right = tiltZ, left = mirrored tilt (-tiltZ)
        float z = movingRight ? flashlightTiltZ : -flashlightTiltZ;
        flashlight.localRotation = Quaternion.Euler(0f, 0f, z);

        // flip the triangle sprite so it points left/right
        if (_flashSR != null)
            _flashSR.flipX = !movingRight;
    }
}
