using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPatrol : MonoBehaviour
{
    EnemyPatrol _enemypatrol;
    SphereCollider myCollider;
    public float agroRad;


    private void Start()
    {
        _enemypatrol = GetComponentInParent<EnemyPatrol>();
        myCollider = GetComponent<SphereCollider>();
        myCollider.radius = agroRad;
        myCollider.isTrigger = true; 

    }



    void OnTriggerEnter(Collider coll)  {
        if (coll.gameObject.tag == "Player")   {
            print("Danger danger high voltage");
            _enemypatrol.target = coll.gameObject;
        }
    }

    void OnTriggerExit(Collider coll)  {
        if (coll.gameObject.tag == "Player")  {
            print("freedom");
            _enemypatrol.target = null;
        }
    }


     

}
