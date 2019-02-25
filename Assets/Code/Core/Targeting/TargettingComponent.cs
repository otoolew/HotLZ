using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class EventTargetEnteredRange : UnityEvent<Targetable> { }
[Serializable] public class EventTargetExitedRange : UnityEvent<Targetable> { }
[Serializable] public class EventAcquiredTarget : UnityEvent<Targetable> { }
[Serializable] public class EventLostTarget : UnityEvent { }

[RequireComponent(typeof(SphereCollider))]
public class TargettingComponent : MonoBehaviour
{
    #region Fields and Properties
    [SerializeField]
    private FactionAlignment factionAlignment;
    public FactionAlignment FactionAlignment { get => factionAlignment; set => factionAlignment = value; }

    [SerializeField]
    private float searchRate;
    public float SearchRate { get => searchRate; set => searchRate = value; }

    [SerializeField]
    private float searchTimer;
    public float SearchTimer { get => searchTimer; set => searchTimer = value; }

    [SerializeField]
    private Targetable currentTarget;
    public Targetable CurrentTarget { get => currentTarget; set => currentTarget = value; }

    [SerializeField]
    private bool hadTarget;
    public bool HadTarget { get => hadTarget; set => hadTarget = value; }

    public List<Targetable> TargetsTrackedList;
    #endregion
    #region Events Actions and Handlers
    public EventTargetEnteredRange OnTargetEntersRange;
    public EventTargetExitedRange OnTargetExitsRange;
    public EventAcquiredTarget OnAcquiredTarget;
    public EventLostTarget OnLostTarget;

    public void OnTargetRemoved(Targetable target)
    {
        target.removed -= OnTargetRemoved;
        if (CurrentTarget != null && target == CurrentTarget)
        {
            OnLostTarget?.Invoke();
            HadTarget = false;
            TargetsTrackedList.Remove(CurrentTarget);
            CurrentTarget = null;
        }
        else //wasnt the current target, find and remove from targets list
        {
            for (int i = 0; i < TargetsTrackedList.Count; i++)
            {
                if (TargetsTrackedList[i] == target)
                {
                    TargetsTrackedList.RemoveAt(i);
                    break;
                }
            }
        }
    }

    #endregion

    #region Monobehaviour
    private void OnEnable()
    {
        ResetTargetter();
    }
    // Start is called before the first frame update
    void Start()
    {
        searchTimer = searchRate;
        TargetsTrackedList = new List<Targetable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(searchTimer <= 0.0f))
            searchTimer -= Time.deltaTime;
        if (searchTimer <= 0.0f && CurrentTarget == null && TargetsTrackedList.Count > 0)
        {
            CurrentTarget = GetNearestTarget();
            if (CurrentTarget != null)
            {
                OnAcquiredTarget?.Invoke(CurrentTarget);
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
        Targetable target = other.transform.root.GetComponent<Targetable>();
        if (target == null)
            return;
        ////Debug.Log(gameObject.GetComponentInParent<UnitActor>().name + " is tracking "+ targetable.name);
        if (!IsTargetableValid(target))
        {
            return;
        }

        target.removed += OnTargetRemoved;
        TargetsTrackedList.Add(target);
        OnTargetEntersRange?.Invoke(target);
    }
    public void HandleTargetDeath()
    {

    }
    /// <summary>
    /// On exiting the trigger, a valid targetable is removed from the tracking list.
    /// </summary>
    /// <param name="other">The other collider in the collision</param>
    private void OnTriggerExit(Collider other)
    {
        var targetable = other.GetComponentInParent<Targetable>();
        if (!IsTargetableValid(targetable))
        {
            return;
        }

        TargetsTrackedList.Remove(targetable);
        OnTargetExitsRange?.Invoke(targetable);
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
    #endregion
    /// <summary>
    /// Clears the list of current targets and clears all events
    /// </summary>
    public void ResetTargetter()
    {
        TargetsTrackedList.Clear();
        CurrentTarget = null;
        OnTargetEntersRange.RemoveAllListeners();
        OnTargetExitsRange.RemoveAllListeners();
        OnAcquiredTarget.RemoveAllListeners();
        OnLostTarget.RemoveAllListeners();
    }

    /// <summary>
    /// Checks if the targetable is a valid target
    /// </summary>
    /// <param name="targetable"></param>
    /// <returns>true if targetable is vaild, false if not</returns>
    public bool IsTargetableValid(Targetable target)
    {
        if (target == null)
            return false;
        //if (targetable.GetComponent<Faction>() == null)
        //    return false;
        return FactionAlignment.CanHarm(target.FactionAlignment);
    }
    /// <summary>
    /// Returns the nearest targetable within the currently tracked targetables 
    /// </summary>
    /// <returns>The nearest targetable if there is one, null otherwise</returns>
    public Targetable GetNearestTarget()
    {
        int length = TargetsTrackedList.Count;

        if (length == 0)
        {
            return null;
        }

        Targetable nearest = null;
        float distance = 20f;
        for (int i = length - 1; i >= 0; i--)
        {
            Targetable targetable = TargetsTrackedList[i];
            if (targetable == null || !targetable.gameObject.activeSelf)
            {
                Debug.Log("Removed Troop!");
                TargetsTrackedList.RemoveAt(i);
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
   
}
