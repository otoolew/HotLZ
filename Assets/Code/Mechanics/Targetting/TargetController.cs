using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetController : MonoBehaviour
{
    #region Fields and Properties
    [SerializeField]
    private FactionAlignment faction;
    public FactionAlignment Faction { get => faction; set => faction = value; }

    [SerializeField]
    private float searchRate;
    public float SearchRate { get => searchRate; set => searchRate = value; }

    [SerializeField]
    private float searchTimer;
    public float SearchTimer { get => searchTimer; set => searchTimer = value; }

    [SerializeField]
    private UnitActor currentTarget;
    public UnitActor CurrentTarget { get => currentTarget; set => currentTarget = value; }

    [SerializeField]
    private bool hadTarget;
    public bool HadTarget { get => hadTarget; set => hadTarget = value; }

    #endregion
    /// <summary>
    /// Fires when a targetable enters the target collider
    /// </summary>
    public event Action<UnitActor> targetEntersRange;

    /// <summary>
    /// Fires when a targetable exits the target collider
    /// </summary>
    public event Action<UnitActor> targetExitsRange;

    /// <summary>
    /// Fires when an appropriate target is found
    /// </summary>
    public event Action<UnitActor> acquiredTarget;
    public Events.AcquiredTarget OnAcquiredTarget;
    /// <summary>
    /// Fires when the current target was lost
    /// </summary>
    public event Action lostTarget;
    public Events.LostTarget OnLostTarget;
    /// <summary>
    /// The current targetables in the collider
    /// </summary>
    public List<UnitActor> targetsInRange = new List<UnitActor>();
    /// <summary>
    /// Starts the search timer
    /// </summary>
    private void Start()
    {
        //Faction = GetComponentInParent<Faction>().FactionAlignment;
        searchTimer = searchRate;
        //GetComponent<SphereCollider>().radius = scanRadius;
    }

    /// <summary>
    /// Checks if any targets are destroyed and aquires a new targetable if appropriate
    /// </summary>
    private void Update()
    {
        //searchTimer -= Time.deltaTime;
        if (!(searchTimer <= 0.0f))
            searchTimer -= Time.deltaTime;
        if (searchTimer <= 0.0f && CurrentTarget == null && targetsInRange.Count > 0)
        {
            CurrentTarget = GetNearestTargetable();
            if (CurrentTarget != null)
            {
                OnAcquiredTarget.Invoke(CurrentTarget);
                acquiredTarget?.Invoke(CurrentTarget);
                searchTimer = searchRate;
            }
        }

        HadTarget = CurrentTarget != null;
    }
    /// <summary>
    /// On entering the trigger, a valid targetable is added to the tracking list.
    /// </summary>
    /// <param name="other">The other collider in the collision</param>
    private void OnTriggerEnter(Collider other)
    {
        try
        {
            var targetable = other.GetComponentInParent<UnitActor>();
            Debug.Log(gameObject.GetComponentInParent<UnitActor>().name + " is tracking "+ targetable.name);
            if (!IsTargetableValid(targetable))
            {
                return;
            }
            targetable.removed += OnTargetRemoved;
            targetsInRange.Add(targetable);
            targetEntersRange?.Invoke(targetable);
        }
        catch (NullReferenceException)
        {
            Debug.Log("No Controller to target");
        }
    }
    /// <summary>
    /// On exiting the trigger, a valid targetable is removed from the tracking list.
    /// </summary>
    /// <param name="other">The other collider in the collision</param>
    private void OnTriggerExit(Collider other)
    {
        var targetable = other.GetComponentInParent<UnitActor>();
        if (!IsTargetableValid(targetable))
        {
            return;
        }

        targetsInRange.Remove(targetable);
        targetExitsRange?.Invoke(targetable);
        if (targetable == CurrentTarget)
        {
            OnTargetRemoved(targetable);
        }
        else
        {
            // Only need to remove if we're not our actual target, otherwise OnTargetRemoved will do the work above
            targetable.removed -= OnTargetRemoved;
        }
    }

    /// <summary>
    /// Returns the current target
    /// </summary>
    public UnitActor GetTarget()
    {
        return CurrentTarget;
    }

    /// <summary>
    /// Clears the list of current targets and clears all events
    /// </summary>
    public void ResetTargetter()
    {
        targetsInRange.Clear();
        CurrentTarget = null;

        targetEntersRange = null;
        targetExitsRange = null;
        acquiredTarget = null;
        lostTarget = null;
    }

    /// <summary>
    /// Returns all the targets within the collider. This list must not be changed as it is the working
    /// list of the targetter. Changing it could break the targetter
    /// </summary>
    public List<UnitActor> GetAllTargets()
    {
        return targetsInRange;
    }

    /// <summary>
    /// Checks if the targetable is a valid target
    /// </summary>
    /// <param name="targetable"></param>
    /// <returns>true if targetable is vaild, false if not</returns>
    public bool IsTargetableValid(UnitActor targetable)
    {
        if (targetable == null)      
            return false;
        //if (targetable.GetComponent<Faction>() == null)
        //    return false;
        return Faction.CanHarm(targetable.Faction);
    }

    /// <summary>
    /// Returns the nearest targetable within the currently tracked targetables 
    /// </summary>
    /// <returns>The nearest targetable if there is one, null otherwise</returns>
    public UnitActor GetNearestTargetable()
    {
        int length = targetsInRange.Count;

        if (length == 0)
        {
            return null;
        }

        UnitActor nearest = null;
        float distance = float.MaxValue;
        for (int i = length - 1; i >= 0; i--)
        {
            UnitActor targetable = targetsInRange[i];
            if (targetable == null || targetable.Dead)
            {
                targetsInRange.RemoveAt(i);
                continue;
            }
            float currentDistance = Vector3.Distance(transform.position, targetable.transform.position);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                nearest = targetable;
            }
        }

        return nearest;
    }



    /// <summary>
    /// Fired by the agents died event or when the current target moves out of range,
    /// Fires the lostTarget event.
    /// </summary>
    void OnTargetRemoved(UnitActor target)
    {
        target.removed -= OnTargetRemoved;
        if (CurrentTarget != null && target == CurrentTarget)
        {
            OnLostTarget.Invoke();
            lostTarget?.Invoke();
            HadTarget = false;
            targetsInRange.Remove(CurrentTarget);
            CurrentTarget = null;
        }
        else //wasnt the current target, find and remove from targets list
        {
            for (int i = 0; i < targetsInRange.Count; i++)
            {
                if (targetsInRange[i] == target)
                {
                    targetsInRange.RemoveAt(i);
                    break;
                }
            }
        }
    }
}

