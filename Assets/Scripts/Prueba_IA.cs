using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Prueba_IA : MonoBehaviour
{

    [SerializeField] private Transform destination; 
    [SerializeField] private Transform player;

    private NavMeshAgent _agent;
    private float visionRange = 40f;
    private float attackRange = 5f;

    [SerializeField]private bool playerInVisionRange;
    [SerializeField] private bool playerInAttackRange;

    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private Transform[] waypoints;
    private int totalWaypoints;
    private int nextPoint;

    [SerializeField] private GameObject bullet;
    private float timeBetweenAttacks;
    private bool canAttack;
    [SerializeField] private float upAttackForce = 15f;
    [SerializeField] private float forwardAttackForce = 18f;
    [SerializeField] private Transform spwanPoint;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        totalWaypoints = waypoints.Length;
        nextPoint = 1;
        canAttack = true;
        _agent.SetDestination(waypoints[0].position);
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        playerInVisionRange = Physics.CheckSphere(pos, visionRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(pos, attackRange, playerLayer);
        //_agent.SetDestination(destination.position);


        if(!playerInVisionRange && !playerInAttackRange)
        {
            Patrol();
        }

        if(playerInVisionRange && !playerInAttackRange)
        {
            Chase();
        }

        if(playerInAttackRange && playerInVisionRange)
        {
            Attack();
        }
    }


    private void Patrol()
    {
        if (Vector3.Distance(transform.position, waypoints[nextPoint].position) < 10)
        {
            nextPoint++;
            if(nextPoint == totalWaypoints)
            {
                nextPoint = 0;
            }
            transform.LookAt(waypoints[nextPoint].position);
            _agent.SetDestination(waypoints[nextPoint].position);
        }
       
    }

    private void Chase()
    {
        _agent.SetDestination(player.position);
        transform.LookAt(player);
    }

    private void Attack()
    {
        _agent.SetDestination(transform.position);

        /*
        if (canAttack)
        {
            Rigidbody rigidbody = Instantiate(bullet, spwanPoint.position, Quaternion.identity).GetComponent<Rigidbody>();

            rigidbody.AddForce(transform.forward * forwardAttackForce, ForceMode.Impulse);
            rigidbody.AddForce(transform.up * upAttackForce, ForceMode.Impulse);

            canAttack = false;
            StartCoroutine(AttackCoolDown());
        }
        */
    }
    /*
    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        canAttack = true;
    }
      */

        private void OnDrawGizmos()
    {
        //Esfera de vision
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, visionRange);
        
        //Esfera de ataque
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
