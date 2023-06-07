using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private bool isGrounded;



    public bool GetIsGrounded()
    {
        return isGrounded;
    }


   
    private void OnCollisionStay(Collision collider)
    {
        
        isGrounded = collider != null && (((1 << collider.gameObject.layer) & platformLayerMask) != 0);
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false; 
    }
}
