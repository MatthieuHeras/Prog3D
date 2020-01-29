using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform spawnTransform = default;
    [SerializeField] private List<GameObject> prefabs = new List<GameObject>();
    [SerializeField] private float xSize = 10f;
    [SerializeField] private float ySize = 10f;
    [SerializeField] private float averageSpawnCD = 3f;
    [SerializeField] LayerMask groundLayer = 0;

    private float timer = 0f;
    private float spawnCD;

    private void Start()
    {
        if (prefabs.Count <= 0 || spawnTransform == null)
            Destroy(gameObject);
        UpdateSpawnCD();
    }

    private void Update() // Way of doing it with Update
    {
        timer += Time.deltaTime;
        
        if (timer >= spawnCD)
        {
            UpdateSpawnCD();
            timer = 0f;
            SpawnPrefab();
        }
    }

    private void UpdateSpawnCD()
    {
        spawnCD = Random.Range(averageSpawnCD * 0.5f, averageSpawnCD * 1.5f);
    }

    private void SpawnPrefab()
    {
        Vector3 pos = new Vector3(Random.Range(-xSize / 2f, xSize / 2f), 0f, Random.Range(-ySize / 2f, ySize / 2f));

        if (Physics.Raycast(new Vector3(pos.x, spawnTransform.position.y, pos.z), Vector3.down, out RaycastHit info, 1000f, groundLayer))
            pos.y = info.point.y + 3f;

        Instantiate(prefabs[Random.Range(0, prefabs.Count)], pos, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(spawnTransform.position, new Vector3(xSize, 0.1f, ySize));
    }
}
