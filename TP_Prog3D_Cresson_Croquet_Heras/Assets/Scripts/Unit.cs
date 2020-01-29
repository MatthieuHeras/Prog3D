using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private Animator anm;
    private ScoreManager scoreManager;
    private bool touched = false;

    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();   
        if (scoreManager == null)
        {
            Debug.LogError("Can't find scoreManager in the scene : destroy : " + gameObject.name);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!touched && collision.collider.CompareTag("Projectile"))
        {
            scoreManager.AddScore(value);
            anm.SetTrigger("Die");
            touched = true;
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
