using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class BallPhysics : MonoBehaviour
{
    [SerializeField] float startSpeed = 7f;
    [SerializeField] float minSpeed = 5f;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.gravityScale = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.freezeRotation = true;
    }

    public void SetInitialAngle(float angleDeg)
    {
        Vector2 dir = new Vector2(Mathf.Cos(angleDeg * Mathf.Deg2Rad), Mathf.Sin(angleDeg * Mathf.Deg2Rad));
        rb.linearVelocity = dir.normalized * Mathf.Max(startSpeed, minSpeed);
    }

    void FixedUpdate()
    {
        float s = Mathf.Max(startSpeed, minSpeed);
        if (rb.linearVelocity.sqrMagnitude > 0.0001f)
            rb.linearVelocity = rb.linearVelocity.normalized * s;
        else
            rb.linearVelocity = Random.insideUnitCircle.normalized * s;
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        float jitterDeg = Random.Range(-2f, 2f);
        float rad = jitterDeg * Mathf.Deg2Rad;
        var v = rb.linearVelocity;
        var rotated = new Vector2(
            v.x * Mathf.Cos(rad) - v.y * Mathf.Sin(rad),
            v.x * Mathf.Sin(rad) + v.y * Mathf.Cos(rad)
        );
        rb.linearVelocity = rotated.normalized * Mathf.Max(startSpeed, minSpeed);
        rb.position += c.GetContact(0).normal * 0.001f;
    }
}
