using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float speed = 40f;
    void Update()
    {
        transform.Translate(translation: Vector3.forward * speed * Time.deltaTime);
    }
}
