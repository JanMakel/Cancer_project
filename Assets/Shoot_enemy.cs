using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_enemy : MonoBehaviour
{
    public GameObject Projectil;
    public float timeToShoot;
    public float shootCooldown;
    void Start()
    {
        shootCooldown = timeToShoot;
    }
    void Update()
    {
        shootCooldown -= Time.deltaTime;
        if (shootCooldown < 0)
        {
            Instantiate(Projectil, transform.position + Vector3.forward, Quaternion.identity);

            shootCooldown = timeToShoot;
        }
    }
}
