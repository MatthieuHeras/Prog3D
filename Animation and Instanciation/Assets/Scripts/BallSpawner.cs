using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private float spawnCD = 1f;
    [SerializeField] private float speed = 20f;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            InvokeRepeating("SpawnBall", 0f, spawnCD);
        if (Input.GetButtonUp("Fire1"))
            CancelInvoke();
    }

    private void SpawnBall()
    {
        GameObject tmp = Instantiate(ball, transform.position, transform.rotation);
        tmp.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);
        Destroy(tmp, 5f);
    }
}
