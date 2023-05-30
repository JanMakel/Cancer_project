using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    public GameObject proyectile;
    private bool projectile;

    


    private void disparar()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(proyectile, transform.position, Quaternion.identity);
        }

       
    }

   




    private void Update()
    {
        disparar();
        
    }

    
}
