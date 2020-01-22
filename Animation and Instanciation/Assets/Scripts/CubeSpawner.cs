using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 center;
    [SerializeField] private float size;
    [SerializeField] private int nbPrefabs;
    [SerializeField] private List<GameObject> prefabs = new List<GameObject>();
    [SerializeField] private int prefabIndex;
    [SerializeField] private int shapeIndex;

    private Vector3 formerCenter; // We keep a copy of the value of the editor variables to see if there is any change,
    private float formerRadius; // it avoids instanciating every cube each frame
    private int formerNbPrefabs;
    private int formerPrefabIndex;
    private int formerShapeIndex;

    private List<GameObject> createdObjects = new List<GameObject>();

    private void Start()
    {
        ResetCenter();
        ResetPrefabs();
        ResetRadius();
    }

    private void Update()
    {
        if (!formerCenter.Equals(center))
            ResetCenter();
        if (!formerNbPrefabs.Equals(nbPrefabs) || !formerPrefabIndex.Equals(prefabIndex) || !formerShapeIndex.Equals(shapeIndex))
            ResetPrefabs();
        if (!formerRadius.Equals(size))
            ResetRadius();
    }

    private void ResetCenter()
    {
        // Set position and refresh memory
        transform.position = center;
        formerCenter = center;
    }

    private void ResetPrefabs()
    {
        // Refresh memory and safety
        if (prefabIndex >= prefabs.Count)
            prefabIndex = prefabs.Count - 1;
        if (prefabIndex < 0)
            prefabIndex = 0;
        formerPrefabIndex = prefabIndex;
        if (nbPrefabs < 0)
            nbPrefabs = 0;
        formerNbPrefabs = nbPrefabs;
        formerShapeIndex = shapeIndex;

        // Destroy current shapes
        while (createdObjects.Count > 0)
        {
            Destroy(createdObjects[0]);
            createdObjects.RemoveAt(0);
        }

        if (nbPrefabs == 0 || prefabs.Count == 0)
            return;
        // Create new shapes
        for (float teta = 0; teta < 360f; teta += (360f / nbPrefabs))
        {
            Vector3 offset;
            switch (shapeIndex)
            {
                case 1:
                    float alpha = teta - 90f;
                    offset = new Vector3(Mathf.Cos(alpha * Mathf.Deg2Rad) * size, Mathf.Cos(alpha * Mathf.Deg2Rad) * Mathf.Sin(alpha * Mathf.Deg2Rad) * size);
                    break;
                default:
                    offset = new Vector3(Mathf.Cos(teta * Mathf.Deg2Rad) * size, Mathf.Sin(teta * Mathf.Deg2Rad) * size);
                    break;
            }
            createdObjects.Add(Instantiate(
                        prefabs[prefabIndex],
                        center + offset,
                        Quaternion.Euler(0f, 0f, Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg + 90f),
                        gameObject.transform
                    ));
        }
    }

    private void ResetRadius()
    {
        // Refresh memory and safety
        if (size < 0f)
            size = 0f;
        formerRadius = size;

        if (nbPrefabs <= 0 || prefabs.Count == 0)
            return;
        // Move current shapes
        for (int i = 0; i * 360f / nbPrefabs < 360f; i++)
        {
            Vector3 offset;
            switch (shapeIndex)
            {
                case 1:
                    float alpha = i * 360f / nbPrefabs - 90f;
                    offset = new Vector3(Mathf.Cos(alpha * Mathf.Deg2Rad) * size, Mathf.Cos(alpha * Mathf.Deg2Rad) * Mathf.Sin(alpha * Mathf.Deg2Rad) * size);
                    break;
                default:
                    alpha = i * 360f / nbPrefabs;
                    offset = new Vector3(Mathf.Cos(alpha * Mathf.Deg2Rad) * size, Mathf.Sin(alpha * Mathf.Deg2Rad) * size);
                    break;
            }
            createdObjects[i].transform.position = center + offset;
        }
    }
}
