using UnityEngine;

[DisallowMultipleComponent]
public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform spawnLeft;
    [SerializeField] private Transform spawnRight;
    [SerializeField] private float spawnDelay = 5f;
    [SerializeField] private bool spawnOnStart = true;

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
        bool rightSide = Random.value < 0.5f;
        Transform t = rightSide ? spawnRight : spawnLeft;
        if (!t || !ballPrefab) return;

        var go = Instantiate(ballPrefab, t.position, Quaternion.identity);
        var bp = go.GetComponent<BallPhysics>();
        if (!bp) bp = go.AddComponent<BallPhysics>();
        bp.SetInitialAngle(rightSide ? 210f : 330f);
    }
}
