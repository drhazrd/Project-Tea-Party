using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class buddyAI : MonoBehaviour
{
    public float lookRadius = 12.0f;
    NavMeshAgent buddyAgent;
    public Transform target;
    public Transform friendlyTarget;
    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;

    void Start()
    {
        buddyAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(friendlyTarget.position, transform.position);

        if (distance <= lookRadius)
        {
            buddyAgent.SetDestination(friendlyTarget.position);
            if (distance <= buddyAgent.stoppingDistance)
            {
                FaceTarget();
            }
        }
    }
    void FaceTarget() {
        Vector3 direction = (friendlyTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    public void Knockback(Vector3 direction)
    {
        knockBackCounter = knockBackTime;

        //direction = new Vector3(10f, 10f, 10f);

         Vector3 buddyMove = buddyAgent.velocity;
            buddyMove = direction * knockBackForce;
        buddyMove.y = knockBackForce;
    }
}
