using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float distance;
    [SerializeField] float height;

    private void Update()
    {
        if (target == null) { return; }
        transform.position = target.transform.position - target.transform.forward * distance;
        transform.position = new Vector3(transform.position.x, transform.position.y * height, transform.position.z);
        transform.LookAt(target.transform.position);
    }
}
