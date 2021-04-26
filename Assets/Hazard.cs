using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public int damageToGive;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Vector3 hitDirection = other.transform.position - transform.position;
            hitDirection = hitDirection.normalized;
            other.GetComponent<playerMotor>().Knockback(hitDirection);
        }
    }
}
