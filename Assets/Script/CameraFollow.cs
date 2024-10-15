using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distacneToTarger;

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, target.position.y + distacneToTarger, transform.position.z);
    }
}
