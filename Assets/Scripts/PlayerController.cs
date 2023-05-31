using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float jumpForce = 20f;
    private Rigidbody _rigidbody;
    private bool isOnTheGround;
    private float speed = 10f;
    private float rotationSpeed = 30f;
    
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
    public float dashingPower = 100f;
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

    //Vidas y UI

    public GameObject[] hearts;
    [SerializeField] private float playerLive;
    public TextMeshProUGUI scoreText;

    //Animaciones
    private Animator _animator;



    void Start()
    {
        
        cam = Camera.main.transform;
        groundedScript = GetComponent<Grounded>();
        Debug.Log(gameObject.name);
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        playerLive = 3;
        gameOver = false;
        canFire = true;
        spores = 0;
        UpdateScore();
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
    private void Rotate()
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
            _animator.SetTrigger("Jump");
            jump();
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
        

        if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1) return;

        CalculateRotation();
        Rotate();
        Move();
        PlayerInBounds();

        if (playerLive < 1)
        {
            hearts[0].gameObject.SetActive(false);
        }

        if (playerLive < 2)
        {
            hearts[1].gameObject.SetActive(false);
        }

        if (playerLive < 3)
        {
            hearts[2].gameObject.SetActive(false);
        }

        UpdateScore();

    }
       
        
        
    private IEnumerator Fire()
    {
        canFire = false;
        var clone = Instantiate(bullet, transform.position + bulletOffset, Quaternion.identity);
        clone.velocity = transform.forward * bulletSpeed;
        yield return new WaitForSeconds(0.5f);
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
            TakeDamage(1);
        }
        else if (otherCollider.gameObject.CompareTag("GreenMush"))
        {
            TakeDamage(1);
        }
        else if (otherCollider.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
        else if (otherCollider.gameObject.CompareTag("BlueMush"))
        {
            TakeDamage(2);
        }
        else if (otherCollider.gameObject.CompareTag("SporeB"))
        {
            spores++;
            Destroy(otherCollider.gameObject);
        }
        else if (otherCollider.gameObject.CompareTag("SporeS"))
        {
            spores = spores += 5;
            Destroy(otherCollider.gameObject);
        }
        else if (otherCollider.gameObject.CompareTag("SporeO"))
        {
            spores = spores += 10;
            dashingPower = dashingPower + 10;
            Destroy(otherCollider.gameObject);
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


    private IEnumerator Dead()
    {
        
        yield return new WaitForSeconds(5);      
        SceneManager.LoadScene("Game Over");

    }

    private IEnumerator WoundDelay()
    {
        yield return new WaitForSeconds(0.5f);
    }
    private void TakeDamage(int damage)
    {
        playerLive -= damage;
        _animator.SetTrigger("Wound");
        if (playerLive <= 0)
        {
            _animator.SetBool("Death_a", true);
            velocity = 0;
            turnspeed = 0;
            StartCoroutine(Dead());
        }
    }
    public void UpdateScore()
    {
        
        scoreText.text = $" x{spores}";
    }
}