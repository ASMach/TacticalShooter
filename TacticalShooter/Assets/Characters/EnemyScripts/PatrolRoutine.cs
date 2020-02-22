using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolRoutine : MonoBehaviour
{
    [field: SerializeField]
    public float VisionDistance
    {
        get;
        protected set;
    } = 10.0f;
    [SerializeField]
    protected GameObject[] waypoints;
    [field: SerializeField]
    public float WaypointThreshold
    {
        get;
        protected set;
    } = 2.0f;

    protected NavMeshAgent agent;
    protected HuntRoutine huntRoutine;

    int wantedWaypointIndex = -1; // Use to step through our waypoints

    // Helper Methods
    void SeekNextWaypoint()
    {
        wantedWaypointIndex++; // Step up before seeking
        //Debug.Log("Seeking waypoint " + wantedWaypointIndex + " of " + waypoints.Length);
        // Wrap around to prevent overflow
        if (wantedWaypointIndex >= waypoints.Length)
        {
            wantedWaypointIndex = 0;
        }
        agent.destination = waypoints[wantedWaypointIndex].transform.position;
    }

    // Object Lifecycle

    // Start is called before the first frame update
    void Start()
    {
        huntRoutine = GetComponent<HuntRoutine>();
        huntRoutine.enabled = false; // Turn off by default so we won't run both routines at once.
        agent = GetComponent<NavMeshAgent>();
        SeekNextWaypoint(); // Start seeking this
    }

    // Update is called once per physics tick
    void FixedUpdate()
    {
        // Check for squad members in field of vision
        RaycastHit hit;

        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit, VisionDistance) && hit.transform.gameObject.layer == LayerMask.NameToLayer("Squad"))
        {
            Debug.Log("Detected Retrieval Squad!");
            // Start the hunting routine and end this one, hunting down the detected object
            huntRoutine.target = hit.collider.gameObject;
            Debug.Log("Target location: " + agent.destination);
            huntRoutine.enabled = true;
            this.enabled = false;
        }

        // Go to the next waypoint if we're at a destination
        float waypointDistance = Vector3.Distance(this.transform.position, waypoints[wantedWaypointIndex].transform.position);
        //Debug.Log("Distance to go: " + waypointDistance + " threshold: " + WaypointThreshold);
        if (waypointDistance <= WaypointThreshold)
        {
            SeekNextWaypoint();
        }
    }
}
