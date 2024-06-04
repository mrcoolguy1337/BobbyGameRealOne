using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // the object the camera should follow
    public float smoothing = 5f; // the smoothing factor for the camera's movement

    private Vector3 offset; // the offset between the camera's position and the target's position

    void Start()
    {
        // calculate the initial offset
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        // create a target position for the camera, which is the target's position plus the offset
        Vector3 targetCamPos = target.position + offset;

        // smooth the camera's movement towards the target position
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
