using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vidas : MonoBehaviour
{
    public GameObject[] hearts;

    private int lives;

    


    public GameObject player;

    public GameObject explosion;

    public ParticleSystem explosionParticle01;
    public ParticleSystem explosionParticle02;
    public ParticleSystem explosionParticle03;

    private void Start()
    {

        lives = 3;
        
    }

    
    public void LooseLife()
   {
        lives--;
        Instantiate(explosion, hearts[lives].transform.position, Quaternion.identity);
       
        hearts[lives].SetActive(false);
    }
    


    void Update()
    {
        
        if (lives < 1)
        {

            hearts[0].transform.Find("explosion_01");
           
            hearts[0].gameObject.SetActive(false);
            GetComponent<ParticleSystem>().Play();
            explosionParticle01.Play();

        }

        if (lives < 2)
        {

            hearts[1].transform.Find("explosion_02");
            
            hearts[1].gameObject.SetActive(false);
            explosionParticle02.Play();

        }

        if (lives < 3)
        {

            hearts[2].transform.Find("explosion_03");
            
            hearts[2].gameObject.SetActive(false);
            explosionParticle03.Play();

        }
    
        if (Input.GetKeyDown(KeyCode.Space)) {
            LooseLife();
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hearts[0].transform.Find("Explosion1");
        }
        else if (collision.gameObject.CompareTag("Explosion1"))
        {
            explosion.Play();
        }


    }*/

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "enemy")
            lives -= 1;

        if (lives == 0)
        {
            lives = 3;
            
            hearts[1].gameObject.SetActive(true);
            hearts[2].gameObject.SetActive(true);
            hearts[3].gameObject.SetActive(true);

        }


    }


}
