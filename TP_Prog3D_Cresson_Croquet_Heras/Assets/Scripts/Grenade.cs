using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private float force = 10f;
    [SerializeField] private float explosionDelay = 2f;
    [SerializeField] private GameObject explosionEffect = default;
    public LayerMask layer;

    void Start() // Way of doing it with a Coroutine
    {
        StartCoroutine(nameof(Explode));
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(explosionDelay);

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius, layer.value);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(force, explosionPos, radius, 5f);
        }
        if (explosionEffect != default)
            Destroy(Instantiate(explosionEffect, transform.position, Quaternion.identity), 3f);
        Destroy(gameObject);
    }
}
