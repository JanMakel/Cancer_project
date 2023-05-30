using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transform : MonoBehaviour
{
    public float moveSpeed = 10f;

    private void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.DownArrow))
            transform.Translate(-Vector3.right * moveSpeed * Time.deltaTime);

    }



}
