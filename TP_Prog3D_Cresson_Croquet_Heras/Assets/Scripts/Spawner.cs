using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab = default;
    [SerializeField] private float xSize = 10f;
    [SerializeField] private float ySize = 10f;
    [SerializeField] private float averageSpawnCD = 3f;

    private void Start()
    {
        if (prefab == null)
            Destroy(gameObject);
    }

    private void Update()
    {
        
    }
}
