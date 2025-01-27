using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float offSet_y;
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 currentPos = transform.position;
        if(target.position.y > currentPos.y)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(currentPos.x, currentPos.y + offSet_y, currentPos.z), 0.25f);
        }
    }
}
