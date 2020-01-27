using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ball = default;
    [SerializeField] private float spawnCD = 1f;
    [SerializeField] private float speed = 20f;
    [SerializeField] private string button = "Fire1";
    [SerializeField] private float destroyDelay = 5f;
    [SerializeField] private GameObject soundPrefab = default;

    private bool isShooting = false;

    private void Start()
    {
        StartCoroutine(nameof(SpawnBall));
    }
    private void Update()
    {
        if (Input.GetButtonDown(button))
            isShooting = true;
        if (Input.GetButtonUp(button))
            isShooting = false;
    }

    private IEnumerator SpawnBall()
    {
        while (true)
        {
            if (isShooting)
            {
                GameObject tmp = Instantiate(ball, transform.position, transform.rotation);
                tmp.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);
                GameObject sound = Instantiate(soundPrefab, transform.position, Quaternion.identity, transform);
                sound.GetComponent<AudioSource>().volume += Random.Range(-0.1f, 0.1f);
                sound.GetComponent<AudioSource>().pitch += Random.Range(-0.1f, 0.1f);
                sound.GetComponent<AudioSource>().Play();
                Destroy(tmp, destroyDelay);
                Destroy(sound, 5f);
                yield return new WaitForSeconds(spawnCD);
            }
            yield return null;
        }
    }
}
