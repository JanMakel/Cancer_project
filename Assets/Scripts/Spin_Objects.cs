using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin_Objects : MonoBehaviour
{
    private float spinSpeed = 25f;

    void Update()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}
