using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    bool follow = false;

    GameObject player;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Toggle player follow
        if (Input.GetButton("Action") && Vector3.Distance(transform.position, player.transform.position) < 2.0f)
        {
            follow = !follow;
        }
        if (follow)
        {
            Debug.Log("Following player");
            agent.destination = player.transform.position;
        }
    }
}
