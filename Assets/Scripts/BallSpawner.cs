using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform spawnLeft;
    [SerializeField] private Transform spawnRight;
    [SerializeField] private float spawnDelay = 5f;
    [SerializeField] private bool spawnOnStart = true;
    [SerializeField] float leftAngleDeg = 330f;
    [SerializeField] float rightAngleDeg = 210f;
    [SerializeField, Range(0f, 1f)] float rightSideProbability = 0.5f;

    private float nextSpawnTime;

    private void Start()
    {
        if (spawnOnStart) Spawn();
        nextSpawnTime = Time.time + spawnDelay;
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            Spawn();
            nextSpawnTime = Time.time + spawnDelay;
        }
    }

    public void Spawn()
    {
        bool rightSide = Random.value < rightSideProbability;
        Transform t = rightSide ? spawnRight : spawnLeft;
        if (!t || !ballPrefab) return;

        var go = Instantiate(ballPrefab, t.position, Quaternion.identity);
        var bp = go.GetComponent<BallPhysics>() ?? go.AddComponent<BallPhysics>();
        bp.SetInitialAngle(rightSide ? rightAngleDeg : leftAngleDeg);
    }
}
