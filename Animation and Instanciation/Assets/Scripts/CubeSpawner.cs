using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 center;
    [SerializeField] private float size;
    [SerializeField] private int nbShapes;
    [SerializeField] private List<GameObject> shapes = new List<GameObject>();
    [SerializeField] private int shapeIndex;

    private Vector3 formerCenter; // We keep a copy of the value of the editor variables to see if there is any change,
    private float formerRadius; // it avoids instanciating every cube each frame
    private int formerNbCubes;
    private int formerShapeIndex;
    private List<GameObject> createdObjects = new List<GameObject>();

    private void Start()
    {
        ResetCenter();
        ResetShapes();
        ResetRadius();
    }

    private void Update()
    {
        if (!formerCenter.Equals(center))
            ResetCenter();
        if (!formerNbCubes.Equals(nbShapes) || !formerShapeIndex.Equals(shapeIndex))
            ResetShapes();
        if (!formerRadius.Equals(size))
            ResetRadius();
    }

    private void ResetCenter()
    {
        // Set position and refresh memory
        transform.position = center;
        formerCenter = center;
    }

    private void ResetShapes()
    {
        // Refresh memory and safety
        if (shapeIndex >= shapes.Count)
            shapeIndex = shapes.Count - 1;
        if (shapeIndex < 0)
            shapeIndex = 0;
        formerShapeIndex = shapeIndex;
        if (nbShapes < 0)
            nbShapes = 0;
        formerNbCubes = nbShapes;

        // Destroy current shapes
        while (createdObjects.Count > 0)
        {
            Destroy(createdObjects[0]);
            createdObjects.RemoveAt(0);
        }

        if (nbShapes == 0 || shapes.Count == 0)
            return;
        // Create new shapes
        for(float teta = 0; teta < 360f; teta += (360f / nbShapes))
        {
            float alpha = teta - 90f;
            Vector3 offset = new Vector3(Mathf.Cos(alpha * Mathf.Deg2Rad) * size, Mathf.Cos(alpha * Mathf.Deg2Rad) * Mathf.Sin(alpha * Mathf.Deg2Rad) * size);

            createdObjects.Add(Instantiate(
                shapes[shapeIndex], 
                center + offset, 
                Quaternion.Euler(0f, 0f, Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg),
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

        if (nbShapes <= 0 || shapes.Count == 0)
            return;
        // Move current shapes
        for (int i = 0; i * 360f / nbShapes < 360f; i++)
        {
            float alpha = i * 360f / nbShapes - 90f;

            createdObjects[i].transform.position = center + new Vector3(Mathf.Cos(alpha * Mathf.Deg2Rad) * size, Mathf.Cos(alpha * Mathf.Deg2Rad) * Mathf.Sin(alpha * Mathf.Deg2Rad) * size);
        }
    }
}
