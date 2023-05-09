using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    private bool isGrounded;



    public bool GetIsGrounded()
    {
        return isGrounded;
    }


   
    private void OnCollisionStay(Collision collider)
    {
        Debug.Log(collider.gameObject.layer); 
        isGrounded = collider != null && (((1 << collider.gameObject.layer) & platformLayerMask) != 0);
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false; 
    }
}
