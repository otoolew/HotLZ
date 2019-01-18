using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class NavigationAgent : MonoBehaviour
{
    #region Fields and Properties

    [SerializeField]
    private NavMeshAgent navAgent;
    public NavMeshAgent NavAgent { get => navAgent; set => navAgent = value; }

    [SerializeField]
    private Vector3 currentDestination;
    public Vector3 CurrentDestination { get => currentDestination; set => currentDestination = value; }

    [SerializeField]
    private int currentWaypoint = 0;
    public int CurrentWaypoint { get => currentWaypoint; set => currentWaypoint = value; }
    public List<Transform> waypointList;

    [SerializeField]
    private Transform debugTransform;
    public Transform DebugTransform { get => debugTransform; set => debugTransform = value; }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        ActivateNavAgent();
        //currentDestination = debugTransform.position;
        //navAgent.destination = currentDestination;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float DistanceToDestination()
    {
        return Vector3.Distance(navAgent.destination, transform.position);
    }
    public void GoToNextWayPoint()
    {
        // Returns if no points have been set up
        if (waypointList.Count == 0)
            return;
        currentWaypoint = (currentWaypoint + 1) % waypointList.Count;
        // Set the agent to go to the currently selected destination.
        navAgent.destination = waypointList[currentWaypoint].position;

    }
    public void ContinueToPosition(Vector3 position)
    {
        if (!navAgent.isActiveAndEnabled)
            return;
        navAgent.isStopped = false;
        navAgent.destination = position;
    }
    public void GoToPosition(Vector3 position)
    {
        if (!navAgent.isActiveAndEnabled)
            return;
        navAgent.isStopped = false;
        navAgent.SetDestination(position);
    }
    public void ClearNavAgentPath()
    {
        navAgent.ResetPath();
    }
    public void ActivateNavAgent()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.enabled = true;
    }
}
