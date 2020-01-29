﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 0f;
    [SerializeField] private float rotationSpeed = 0f;

    private Rigidbody rb;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody missing on : " + gameObject.name);
            Destroy(gameObject);
        }
    }


    void Update() // Simple 2D controller
    {
        rb.AddRelativeForce(new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0f, Input.GetAxis("Vertical") * speed * Time.deltaTime));

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 
            transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime, 
            0f);
    }
}
