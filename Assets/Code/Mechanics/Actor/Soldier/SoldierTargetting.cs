using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierTargetting : ActorTracking
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
    private Actor currentTarget;
    public Actor CurrentTarget { get => currentTarget; set => currentTarget = value; }

    [SerializeField]
    private bool hadTarget;
    public bool HadTarget { get => hadTarget; set => hadTarget = value; }

    public List<Actor> ActorsTrackedList;
    #endregion
    #region Events Actions and Handlers

    public EventActorEnteredRange OnActorEntersRange;
    public EventActorExitedRange OnActorExitsRange;
    public EventAcquiredActor OnAcquiredActor;
    public EventLostActor OnLostActor;

    protected override void OnActorRemoved(Actor actor)
    {
        actor.OnActorRemoved -= OnActorRemoved;
        if (CurrentTarget != null && actor == CurrentTarget)
        {
            OnLostActor?.Invoke();
            HadTarget = false;
            ActorsTrackedList.Remove(CurrentTarget);
            CurrentTarget = null;
        }
        else //wasnt the current target, find and remove from targets list
        {
            for (int i = 0; i < ActorsTrackedList.Count; i++)
            {
                if (ActorsTrackedList[i] == actor)
                {
                    ActorsTrackedList.RemoveAt(i);
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
        ActorsTrackedList = new List<Actor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(searchTimer <= 0.0f))
            searchTimer -= Time.deltaTime;
        if (searchTimer <= 0.0f && CurrentTarget == null && ActorsTrackedList.Count > 0)
        {
            CurrentTarget = GetNearestActor();
            if (CurrentTarget != null)
            {
                OnAcquiredActor?.Invoke(CurrentTarget);
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
        Actor actor = other.transform.root.GetComponent<Actor>();
        if (actor == null)
            return;
        ////Debug.Log(gameObject.GetComponentInParent<UnitActor>().name + " is tracking "+ targetable.name);
        if (!IsTargetableValid(actor))
        {
            return;
        }
        actor.OnActorRemoved += OnActorRemoved;
        ActorsTrackedList.Add(actor);
        OnActorEntersRange?.Invoke(actor);

    }
    /// <summary>
    /// On exiting the trigger, a valid targetable is removed from the tracking list.
    /// </summary>
    /// <param name="other">The other collider in the collision</param>
    private void OnTriggerExit(Collider other)
    {
        var targetable = other.GetComponentInParent<Actor>();
        if (!IsTargetableValid(targetable))
        {
            return;
        }

        ActorsTrackedList.Remove(targetable);
        OnActorExitsRange?.Invoke(targetable);
        if (targetable == CurrentTarget)
        {
            OnActorRemoved(targetable);
        }
        else
        {
            // Only need to remove if we're not our actual target, otherwise OnTargetRemoved will do the work above
            targetable.OnActorRemoved -= OnActorRemoved;
        }
    }
    #endregion
    /// <summary>
    /// Clears the list of current targets and clears all events
    /// </summary>
    public void ResetTargetter()
    {
        ActorsTrackedList.Clear();
        CurrentTarget = null;
    }
    /// <summary>
    /// Checks if the targetable is a valid target
    /// </summary>
    /// <param name="targetable"></param>
    /// <returns>true if targetable is vaild, false if not</returns>
    public bool IsTargetableValid(Actor actor)
    {
        if (actor == null)
            return false;
        //if (targetable.GetComponent<Faction>() == null)
        //    return false;
        return Faction.CanHarm(actor.Faction);
    }
    /// <summary>
    /// Returns the nearest targetable within the currently tracked targetables 
    /// </summary>
    /// <returns>The nearest targetable if there is one, null otherwise</returns>
    public Actor GetNearestActor()
    {
        int length = ActorsTrackedList.Count;

        if (length == 0)
        {
            return null;
        }

        Actor nearest = null;
        float distance = float.MaxValue;
        for (int i = length - 1; i >= 0; i--)
        {
            Actor targetable = ActorsTrackedList[i];
            if (targetable == null || !targetable.isActiveAndEnabled)
            {
                ActorsTrackedList.RemoveAt(i);
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
