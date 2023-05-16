using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{


    public float speed = 80f;
    void Update()
    {
        transform.Translate(translation: Vector3.right * speed * Time.deltaTime);
    }
}
