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
    [SerializeField] private float playerLive;
    private bool gameOver;
    private int spores;
    private Grounded groundedScript;

    //Movimiento
    public float velocity = 5f;
    public float turnspeed = 10f;

    Vector2 input;
    float angle;
    Quaternion targetrotation;
    Transform cam;

    //Dash
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 100f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    
    [SerializeField] private TrailRenderer tr;


    //Bala
    public Rigidbody bullet;
    public float bulletSpeed = 1f;
    public Vector3 bulletOffset;
    private bool canFire;

    //Player en los límites del mapa

    private float rangeZ = 15f;


    void Start()
    {
        cam = Camera.main.transform;
        groundedScript = GetComponent<Grounded>();
        Debug.Log(gameObject.name);
        _rigidbody = GetComponent<Rigidbody>();
        playerLive = 3;
        gameOver = false;
        canFire = true;
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

     
    private void Move()
    {
        transform.position += transform.forward * velocity * Time.deltaTime;
    }


    void Update()
    {
        GetInput();
        if (isDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F) && canFire)
        {
            StartCoroutine(Fire());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
        

        if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1) return;

        CalculateRotation();
        rotate();
        Move();

        


        PlayerInBounds();

       
        
        
    }
       
        
        
    private IEnumerator Fire()
    {
        canFire = false;
        var clone = Instantiate(bullet, transform.position + bulletOffset, Quaternion.identity);
        clone.velocity = transform.forward * bulletSpeed;
        yield return new WaitForSeconds(2f);
        canFire = true;
    }
    

    private void jump()
    {
        if (groundedScript.GetIsGrounded())
        {
            Debug.Log("Salto");
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


    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        Vector3 originalGravity = Physics.gravity;
        Physics.gravity = Vector3.zero;
        _rigidbody.velocity = transform.forward * dashingPower;
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        _rigidbody.velocity = Vector3.zero;
        Physics.gravity = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void PlayerInBounds()
    {
        Vector3 pos = transform.position;
        if (pos.z < -rangeZ)
        {
            transform.position = new Vector3(pos.x, pos.y, -rangeZ);
        }

        if (pos.z > rangeZ)
        {
            transform.position = new Vector3(pos.x, pos.y, rangeZ);
        }
    }
}