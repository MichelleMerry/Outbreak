 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{

    TriggerPatrol triggerPatrol;

    [HideInInspector]
    public GameObject target;


    public bool showDebug;


    [Header("Nav Mesh")]
    NavMeshAgent myAgent;
    public Transform[] points;
    public int destPoints = 0;
    public float speed = 7f;

    [Header("Ranges")]
    public float agroRadius;
    public float attackDistance;
    public float attackCoolDown;
    float startTimer;
    bool attacking = false;



    void OnEnable()
    {
        triggerPatrol = GetComponentInChildren<TriggerPatrol>();
        triggerPatrol.agroRad = agroRadius;
    }

    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.speed = speed;
        myAgent.autoBraking = false;


        for (int i = 0; i < points.Length; i++)
        {
            points[i].GetComponent <MeshRenderer>().enabled = false;
        }



    }


    private void OnDrawGizmos()
    {
        if(showDebug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDistance);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, agroRadius);
            Gizmos.color = Color.blue;
         
            for (int i = 0; i <  points.Length; i++)
            {
                Gizmos.DrawWireSphere(points[i].position, 0.5f);
            }
        }
  
    }




    void Update()
    {

        if (attacking == true)
        {
            startTimer += Time.deltaTime;
            if (startTimer >= attackCoolDown)
                startTimer = 0f;
            {
                Attack();
            }


        }




        if (target != null)
        {
            CheckDist();

        }


        if (!myAgent.pathPending && myAgent.remainingDistance <= 0.5f)
        {
            NextPoint();
        }
    }
    void NextPoint()
    {
        if (points.Length == 0)
        {
            return;
        }

        myAgent.destination = points[destPoints].position;
        destPoints = (destPoints + 1) % points.Length;

    }

    void CheckDist()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);

        if (dist <= attackDistance)
        {
            attacking = true;
            myAgent.isStopped = true;
        }
        else
        {
            myAgent.isStopped = false;
            myAgent.destination = target.transform.position;
            attacking = false;
        }

    }
    void Attack()
    {
        print("attacting the player");
    }
    void TakeDamage()
    {

    }

}

