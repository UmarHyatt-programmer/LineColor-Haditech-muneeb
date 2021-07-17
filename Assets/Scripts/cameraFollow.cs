using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target;
    public float speed = 0.125f;
    public Vector3 offSets;
    private void LateUpdate()
    {
        Vector3 desiredPositions = target.position + offSets;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPositions, speed);
        transform.position = smoothPosition;
        //transform.LookAt(target);
    }
}
