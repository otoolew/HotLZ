using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SupplyTruckDriver : MonoBehaviour
{
    #region Fields and Properties
    
    [SerializeField]
    private NavMeshAgent navAgent;
    public NavMeshAgent NavAgent { get => navAgent; set => navAgent = value; }

    [SerializeField]
    private MissionCommand missionCommand;
    public MissionCommand MissionCommand { get => missionCommand; set => missionCommand = value; }

    [SerializeField]
    private MissionStatus.CollectorStatus collectorStatus;
    public MissionStatus.CollectorStatus CollectorStatus { get => collectorStatus; set => collectorStatus = value; }

    [SerializeField]
    private ResourceDepot resourceDepot;
    public ResourceDepot ResourceDepot { get => resourceDepot; set => resourceDepot = value; }

    [SerializeField]
    private ResourceField resourceField;
    public ResourceField ResourceField { get => resourceField; set => resourceField = value; }

    [SerializeField]
    private int maxResourceCapacity;
    public int MaxResourceCapacity { get => maxResourceCapacity; set => maxResourceCapacity = value; }

    [SerializeField]
    private int currentResourceAmount;
    public int CurrentResourceAmount { get => currentResourceAmount; set => currentResourceAmount = value; }

    [SerializeField]
    private float resourceLoadWait;
    public float ResourceLoadWait { get => resourceLoadWait; set => resourceLoadWait = value; }

    [SerializeField]
    private int currentWaypoint = 0;
    public int CurrentWaypoint { get => currentWaypoint; set => currentWaypoint = value; }

    public List<Transform> waypointList;



    #endregion

    // Start is called before the first frame update
    void Start()
    {
        collectorStatus = MissionStatus.CollectorStatus.DEPLOYING;
        RequestFieldAssignment();
    }

    // Update is called once per frame
    void Update()
    {
        switch (collectorStatus)
        {
            case MissionStatus.CollectorStatus.DEPLOYING:
                break;
            case MissionStatus.CollectorStatus.IDLE:
                RequestFieldAssignment();
                break;
            case MissionStatus.CollectorStatus.FETCHING:
                if (DistanceToDestination() <= navAgent.stoppingDistance)
                {
                    GoToNextWayPoint();
                }
                break;
            case MissionStatus.CollectorStatus.LOADING:
                // TODO: Stop in Field
                break;
            case MissionStatus.CollectorStatus.DELIVERING:
                if (!navAgent.hasPath)
                    navAgent.SetDestination(resourceDepot.transform.position);
                break;
            case MissionStatus.CollectorStatus.UNLOADING:
                // TODO: Stop in Depot
                break;
            default:
                Debug.Log("Break!");
                break;
        }

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
    public void GoToPosition(Vector3 position)
    {
        if (!navAgent.isActiveAndEnabled)
            return;

        navAgent.isStopped = false;
        navAgent.destination = position;
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
    #region IResourceCollector
    public void RequestFieldAssignment()
    {
        waypointList.Clear();
        resourceField = resourceDepot.RecieveFieldAssignment();
        if (resourceField != null)
        {
            foreach (var navPoint in resourceField.navPointArray)
            {
                waypointList.Add(navPoint.transform);
            }
            foreach (var navPoint in resourceDepot.navPointArray)
            {
                waypointList.Add(navPoint.transform);
            }

            navAgent.SetDestination(waypointList[0].transform.position);

            collectorStatus = MissionStatus.CollectorStatus.FETCHING;
        }
        else
        {
           collectorStatus = MissionStatus.CollectorStatus.IDLE;
        }           
    }

    public void ReturnToDepot()
    {
        //currentNavPoint = resourceDepot.DropPoint;
    }
    public void DeliverResource()
    {
        navAgent.SetDestination(resourceDepot.transform.position);
        collectorStatus = MissionStatus.CollectorStatus.DELIVERING;
    }

    public void UnloadResource()
    {
        collectorStatus = MissionStatus.CollectorStatus.UNLOADING;
    }

    public bool IsEmpty()
    {
        if (currentResourceAmount <= 0)
            return true;
        return false;
    }

    public bool IsFull()
    {
        if (currentResourceAmount >= maxResourceCapacity)
        {
            collectorStatus = MissionStatus.CollectorStatus.DELIVERING;
            return true;
        }
        return false;
    }

    public void LoadResources()
    {
        StopAllCoroutines();
        StartCoroutine(LoadingSequence());
    }
    public void UnloadResources()
    {
        StopAllCoroutines();
        StartCoroutine(UnloadingSequence());
    }

    IEnumerator LoadingSequence()
    {
        Debug.Log("Loading");
        navAgent.isStopped = true;
        yield return new WaitForSeconds(resourceLoadWait);
        currentResourceAmount++;
        navAgent.isStopped = false;
    }
    IEnumerator UnloadingSequence()
    {
        Debug.Log("Unloading");
        navAgent.isStopped = true;
        yield return new WaitForSeconds(resourceLoadWait);
        currentResourceAmount--;
        navAgent.isStopped = false;
    }

    #endregion

}
