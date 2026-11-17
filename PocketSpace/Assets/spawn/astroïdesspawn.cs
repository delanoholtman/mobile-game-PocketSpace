using UnityEngine;

public class AsteroidsSpawn : MonoBehaviour
{
    public GameObject[] meteorPrefabs;

    public float spawnRate = 1f;
    private float minX = -2.5f;
    private float maxX = 2.5f;
    private float spawnY = 6f;

    private float meteorSpeed = 3.2f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnMeteor();
            timer = 0f;
        }
    }

    public void SpawnMeteor()
    {
        GameObject prefab = meteorPrefabs[Random.Range(0, meteorPrefabs.Length)];

        int side = Random.Range(0, 4);
        Vector3 spawnPos = Vector3.zero;

        switch (side)
        {
            case 0:
                spawnPos = new Vector3(Random.Range(minX, maxX), spawnY, 0f);
                break;

            case 1:
                spawnPos = new Vector3(Random.Range(minX, maxX), -spawnY, 0f);
                break;

            case 2:
                spawnPos = new Vector3(-spawnY, Random.Range(minX, maxX), 0f);
                break;

            case 3:
                spawnPos = new Vector3(spawnY, Random.Range(minX, maxX), 0f);
                break;
        }

        GameObject meteor = Instantiate(prefab, spawnPos, Quaternion.identity);

        meteor.transform.localScale = Vector3.one * Random.Range(1.5f, 3f);

        meteor.AddComponent<DestroyOffscreen>();

        Rigidbody2D rb = meteor.GetComponent<Rigidbody2D>();

        Vector2 direction = GetDiagonalDirection(side);

        rb.linearVelocity = direction * meteorSpeed;

        rb.angularVelocity = Random.Range(-180f, 180f);
    }

    Vector2 GetDiagonalDirection(int side)
    {
        switch (side)
        {
            case 0:
                return new Vector2(Random.Range(-0.7f, 0.7f), -1f).normalized;

            case 1:
                return new Vector2(Random.Range(-0.7f, 0.7f), 1f).normalized;

            case 2:
                return new Vector2(1f, Random.Range(-0.7f, 0.7f)).normalized;

            case 3:
                return new Vector2(-1f, Random.Range(-0.7f, 0.7f)).normalized;
        }

        return Vector2.down;
    }
}
