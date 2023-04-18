using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float jumpForce = 20f;
    private Rigidbody _rigidbody;
    private bool isOnTheGround;
    private float speed = 10f;
    private float rotationSpeed = 30f;



    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        _rigidbody.AddForce(transform.forward * speed * verticalInput);

        
         if (Mathf.Approximately(verticalInput, 0) && isOnTheGround)
        {
            _rigidbody.velocity = Vector3.zero;
        }

        if( Mathf.Approximately(horizontalInput,0) && isOnTheGround)
        {
            _rigidbody.angularVelocity = Vector3.zero;
        }
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space) && isOnTheGround)
        {
            jump();
        }

        
    }

    private void jump()
    {
        isOnTheGround = false;
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }


    private void OnCollisionEnter(Collision othercollision)
    {

        if (othercollision.gameObject.CompareTag("Ground"))
        {
            isOnTheGround = true;
        }
    }
}