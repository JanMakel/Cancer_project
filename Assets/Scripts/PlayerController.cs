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
    [SerializeField]private float playerLive;
    private bool gameOver;

    private int spores;
    private Grounded groundedScript;




    private void Awake()
    {
        groundedScript = GetComponent<Grounded>();
       
    }
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        playerLive = 3;
        gameOver = false;
        spores = 0;
    }

    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }






        /*
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        _rigidbody.AddForce(transform.forward * speed * verticalInput);

        
         if (Mathf.Approximately(verticalInput, 0))
        {
            _rigidbody.velocity = Vector3.zero;
        }

        if( Mathf.Approximately(horizontalInput,0))
        {
            _rigidbody.angularVelocity = Vector3.zero;
        }
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        */
        

    }

    private void jump()
    {
        if (groundedScript.GetIsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
    }


 

    private void OnCollisionEnter(Collision otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("RedMush"))
        {
            playerLive--;
        }
        else if (otherCollider.gameObject.CompareTag("GreenMush"))
        {
            playerLive--;
        }
        else if (otherCollider.gameObject.CompareTag("Bullet"))
        {
            playerLive--;
        }
        else if (otherCollider.gameObject.CompareTag("BlueMush"))
        {
            playerLive = playerLive - 2;
        }
        else if (otherCollider.gameObject.CompareTag("Spores"))
        {
            spores++;
            Destroy(otherCollider.gameObject);
        }
        else if (otherCollider.gameObject.CompareTag("MegaSpore"))
        {
            spores = spores + 5;
            Destroy(otherCollider.gameObject);
        }
    }


    private void GameOver()
    {
        if(playerLive <= 0)
        {
            gameOver = true;
        }
    }




    

}