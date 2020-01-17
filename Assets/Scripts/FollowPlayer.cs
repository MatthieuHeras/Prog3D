using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Vector3 offset;
    public GameObject target;

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
        transform.rotation = target.transform.rotation;
    }
}
