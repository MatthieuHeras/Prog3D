using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Vector3 offset;
    public GameObject target;
    [SerializeField] float rotationSpeed = 50f;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("target missing : " + gameObject.name);
        }
    }
    private void LateUpdate()
    {
        transform.position = target.transform.position + offset;

        float xRotation = transform.rotation.eulerAngles.x;
        if (xRotation >= 270f)
            xRotation -= 360f;
        transform.rotation = Quaternion.Euler(Mathf.Clamp(xRotation - Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime, -90f, 90f),
        target.transform.rotation.eulerAngles.y,
        target.transform.rotation.eulerAngles.z);
    }
}
