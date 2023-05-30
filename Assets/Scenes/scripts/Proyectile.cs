using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Proyectile : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    private int dir = 1;
    /*private bool projectile;*/
    private float destroyTime;


    

    private void Start()
    {


        Destroy(gameObject, 3f);
        /*projectile = GetComponent<Rigidbody>();*/


    } 

    private void Update()
    {
        transform.position = new Vector3(0, 0, transform.position.z + (speed * Time.deltaTime * dir));
    }


}
