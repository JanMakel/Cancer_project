using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;


    public Vector3 offset;

    public float cameraSpeed = 10f;
  

    
    void LateUpdate()
    {

        Vector3 desiredPostion = player.position + offset;

        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPostion, cameraSpeed * Time.deltaTime);

        transform.position = smoothPosition;

    }
}
