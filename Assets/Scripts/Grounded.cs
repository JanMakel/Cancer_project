using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    public bool isGrounded;


    private void OnTriggerStay(Collider collider)
    {
        isGrounded = collider != null && (((1 << collider.gameObject.layer) & platformLayerMask) != 0);
    }

    private void OnTriggerExit(Collider collision)
    {
        isGrounded = false; 
    }
}
