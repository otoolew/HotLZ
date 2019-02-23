using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[Serializable] public class EventTargetEnteredRange : UnityEvent<Targetable> { }
//[Serializable] public class EventTargetExitedRange : UnityEvent<Targetable> { }
//[Serializable] public class EventAcquiredTarget : UnityEvent<Targetable> { }
//[Serializable] public class EventLostTarget : UnityEvent { }

[RequireComponent(typeof(SphereCollider))]
public class TargettingComponent : MonoBehaviour
{
    #region Fields and Properties

    public IFactionProvider factionAlignment;

    [SerializeField]
    private float searchRate;
    protected float SearchRate { get => searchRate; set => searchRate = value; }

    [SerializeField]
    private float searchTimer;
    protected float SearchTimer { get => searchTimer; set => searchTimer = value; }

    [SerializeField]
    private Targetable currentTarget;
    public Targetable CurrentTarget { get => currentTarget; set => currentTarget = value; }

    [SerializeField]
    private bool hadTarget;
    protected bool HadTarget { get => hadTarget; set => hadTarget = value; }

    protected List<Targetable> TargetsInRangeList = new List<Targetable>();

    /// <summary>
    /// The collider attached to the targetter
    /// </summary>
    public Collider attachedCollider;
    /// <summary>
    /// returns the radius of the collider whether
    /// its a sphere or capsule
    /// </summary>
    public float targettingRadius
    {
        get
        {
            var sphere = attachedCollider as SphereCollider;
            if (sphere != null)
            {
                return sphere.radius;
            }
            var capsule = attachedCollider as CapsuleCollider;
            if (capsule != null)
            {
                return capsule.radius;
            }
            return 0;
        }
    }
    #endregion
    #region Events Actions and Handlers

    /// <summary>
    /// Fires when a targetable enters the target collider
    /// </summary>
    public event Action<Targetable> targetEntersRange;

    /// <summary>
    /// Fires when a targetable exits the target collider
    /// </summary>
    public event Action<Targetable> targetExitsRange;

    /// <summary>
    /// Fires when an appropriate target is found
    /// </summary>
    public event Action<Targetable> acquiredTarget;

    /// <summary>
    /// Fires when the current target was lost
    /// </summary>
    public event Action lostTarget;


    void OnTargetRemoved(DamageableBehaviour target)
    {
        target.removed -= OnTargetRemoved;
        if (currentTarget != null && target == currentTarget)
        {
            lostTarget?.Invoke();

            hadTarget = false;
            TargetsInRangeList.Remove(currentTarget);
            currentTarget = null;
        }
        else //wasnt the current target, find and remove from targets list
        {
            for (int i = 0; i < TargetsInRangeList.Count; i++)
            {
                if (TargetsInRangeList[i] == target)
                {
                    TargetsInRangeList.RemoveAt(i);
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
        TargetsInRangeList = new List<Targetable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(searchTimer <= 0.0f))
            searchTimer -= Time.deltaTime;
        if (searchTimer <= 0.0f && CurrentTarget == null && TargetsInRangeList.Count > 0)
        {
            CurrentTarget = GetNearestTarget();
            if (CurrentTarget != null)
            {
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
    protected virtual void OnTriggerEnter(Collider other)
    {
        Targetable targetable = other.transform.root.GetComponent<Targetable>();
        ////Debug.Log(gameObject.GetComponentInParent<UnitActor>().name + " is tracking "+ targetable.name);
        if (!IsTargetableValid(targetable))
        {
            return;
        }
        targetable.removed += OnTargetRemoved;
        TargetsInRangeList.Add(targetable);
        targetEntersRange?.Invoke(targetable);

    }
    /// <summary>
    /// On exiting the trigger, a valid targetable is removed from the tracking list.
    /// </summary>
    /// <param name="other">The other collider in the collision</param>
    protected virtual void OnTriggerExit(Collider other)
    {
        var targetable = other.GetComponentInParent<Targetable>();
        if (!IsTargetableValid(targetable))
        {
            return;
        }

        TargetsInRangeList.Remove(targetable);
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
    #endregion
    /// <summary>
    /// Clears the list of current targets and clears all events
    /// </summary>
    public void ResetTargetter()
    {
        TargetsInRangeList.Clear();
        CurrentTarget = null;
        targetEntersRange = null;
        targetExitsRange = null;
        acquiredTarget = null;
        lostTarget = null;
    }
    /// <summary>
    /// Checks if the targetable is a valid target
    /// </summary>
    /// <param name="targetable"></param>
    /// <returns>true if targetable is vaild, false if not</returns>
    protected virtual bool IsTargetableValid(Targetable targetable)
    {
        if (targetable == null)
            return false;
        IFactionProvider targetAlignment = targetable.FactionProvider;
        //if (targetable.GetComponent<Faction>() == null)
        //    return false;
        bool canDamage = factionAlignment == null || targetAlignment == null ||
                             factionAlignment.CanHarm(targetAlignment);
        return canDamage;
    }
    /// <summary>
    /// Returns the nearest targetable within the currently tracked targetables 
    /// </summary>
    /// <returns>The nearest targetable if there is one, null otherwise</returns>
    protected virtual Targetable GetNearestTarget()
    {
        int length = TargetsInRangeList.Count;

        if (length == 0)
        {
            return null;
        }

        Targetable nearest = null;
        float distance = float.MaxValue;
        for (int i = length - 1; i >= 0; i--)
        {
            Targetable targetable = TargetsInRangeList[i];
            if (targetable == null || !targetable.isActiveAndEnabled)
            {
                TargetsInRangeList.RemoveAt(i);
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
