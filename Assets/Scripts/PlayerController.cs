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

    //Movimiento
    public float velocity = 5f;
    public float turnspeed = 10f;

    Vector2 input;
    float angle;
    Quaternion targetrotation;
    Transform cam; 




    void Start()
    {
        cam = Camera.main.transform;
        _rigidbody = GetComponent<Rigidbody>();
        playerLive = 3;
        gameOver = false;
        spores = 0;
    }

    //Funcion que coge el eje horizontal con a,d,< o >, y el eje vertical w, s, ^, v
    private void GetInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }

    //Direccion relativa a la rotacion de la camara
    private void CalculateRotation()
    {
        angle = Mathf.Atan2(input.x, input.y);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y;
            
    }


    //Rotacion conforme al angulo calculado
    private void rotate()
    {
        targetrotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, turnspeed * Time.deltaTime); 
    }

    //El jugador se movera siempre hacia su adelante
    private void Move()
    {
        transform.position += transform.forward * velocity * Time.deltaTime;
    }


    void Update()
    {
        GetInput();

        if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1) return;

        CalculateRotation();
        rotate();
        Move();


        
      
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
        
        
    }

    private void jump()
    {
        isOnTheGround = false;
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }


  private bool isGrounded()
    {
      return transform.Find("Grounded").GetComponent<Grounded>();
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