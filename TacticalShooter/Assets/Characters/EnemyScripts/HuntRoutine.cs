using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HuntRoutine : MonoBehaviour
{
    [SerializeField]
    protected float countdown = 30.0f;
    protected float countdownTimer = 0.0f;
    protected NavMeshAgent agent;
    protected PatrolRoutine patrolRoutine;
    [field: SerializeField]
    public float HuntingThreshold
    {
        get;
        protected set;
    } = 0.1f;

    public GameObject target;

    // Helper Methods



    // Object Lifecycle Methods

    // Start is called before the first frame update
    void Start()
    {
        countdown = countdownTimer; // Synchronize
        agent = GetComponent<NavMeshAgent>();
        patrolRoutine = GetComponent<PatrolRoutine>();
        patrolRoutine.enabled = false; // Delete this line if both scripts get deactivated at startup.
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        agent.destination = target.transform.position; // Follow squad member
        // Only countdown if you've run far enough away.
        if (Vector3.Distance(transform.position, agent.destination) > patrolRoutine.VisionDistance)
        {
            // Countdown our hunt
            if (countdownTimer >= 0.0f)
            {
                countdownTimer -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Lost track of squad!");
                // Reset timer for next time
                countdownTimer = countdown;
                // Return to patrol at previous post
                patrolRoutine.enabled = true;
                this.enabled = false;
            }
        }
        // Caught up to squad member
        else if (Vector3.Distance(transform.position, agent.destination) < HuntingThreshold)
        {
            // TODO: Implement melee attack behavior here
        }
    }
}
