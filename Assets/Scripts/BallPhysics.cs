using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] float startSpeed = 7f;
    [SerializeField] float minSpeed = 5f;
    // threshold below which we re-seed direction
    [SerializeField] float speedEpsilon = 0.01f; 

    [Header("Collision Tweaks")]
    [SerializeField] float maxJitterAngle = 2f; // degrees
    [SerializeField] float positionOffset = 0.001f;
    [SerializeField] string playerTag = "Player";

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Sets initial velocity from angle (deg) at speed max(startSpeed, minSpeed)
    public void SetInitialAngle(float angleDeg)
    {
        Vector2 dir = new Vector2(Mathf.Cos(angleDeg * Mathf.Deg2Rad), Mathf.Sin(angleDeg * Mathf.Deg2Rad));
        rb.linearVelocity = dir.normalized * Mathf.Max(startSpeed, minSpeed);
    }

    void FixedUpdate()
    {
        float targetSpeed = Mathf.Max(startSpeed, minSpeed);
        // If current speed is effectively zero, seed a fresh random direction; otherwise keep constant speed
        if (rb.linearVelocity.magnitude > speedEpsilon)
            rb.linearVelocity = rb.linearVelocity.normalized * targetSpeed;
        else
            rb.linearVelocity = Random.insideUnitCircle.normalized * targetSpeed;
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.collider.CompareTag(playerTag))
        {
            GameEvents.PlayerHit?.Invoke();
            return;
        }

        // Adds a small random angle change on collisions to avoid "perfect loops"
        float jitterDeg = Random.Range(-maxJitterAngle, maxJitterAngle);
        float rad = jitterDeg * Mathf.Deg2Rad;

        var v = rb.linearVelocity;
        var rotated = new Vector2(
            v.x * Mathf.Cos(rad) - v.y * Mathf.Sin(rad),
            v.x * Mathf.Sin(rad) + v.y * Mathf.Cos(rad)
        );

        rb.linearVelocity = rotated.normalized * Mathf.Max(startSpeed, minSpeed);
        rb.position += c.GetContact(0).normal * positionOffset;
    }
}
