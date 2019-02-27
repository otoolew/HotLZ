using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class AITargetingComponent : MonoBehaviour
{
    [SerializeField] private FactionAlignment factionAlignment;
    public FactionAlignment FactionAlignment { get => factionAlignment; set => factionAlignment = value; }

    [SerializeField] private float viewRadius;
    public float ViewRadius { get => viewRadius; set => viewRadius = value; }

    [Range(0, 360)]
    [SerializeField] private float viewAngle;
    public float ViewAngle { get => viewAngle; set => viewAngle = value; }

    public LayerMask targetMask;
    public LayerMask obsticleMask;

    [SerializeField] private Targetable currentTarget;
    public Targetable CurrentTarget { get => currentTarget; set => currentTarget = value; }

    [SerializeField] private float searchRate;
    public float SearchRate { get => searchRate; set => searchRate = value; }

    [SerializeField] private float searchTimer;

    [SerializeField] private bool searchReady;
    public bool SearchReady { get => searchReady; set => searchReady = value; }

    [SerializeField] private bool hadTarget;
    public bool HadTarget { get => hadTarget; set => hadTarget = value; }

    public List<Targetable> TargetList = new List<Targetable>();

    public event Action<Targetable> acquiredTarget, targetEnteredRange, targetExitedRange;
    public event Action lostTarget;

    private void OnEnable()
    {
        ResetTargetter();
    }

    // Start is called before the first frame update
    void Start()
    {
        factionAlignment = GetComponentInParent<FactionComponent>().FactionAlignment;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(searchTimer <= 0.0f))
        {
            searchTimer -= Time.deltaTime;
        }
        else
        {
            CurrentTarget = GetNearestTarget();
            if (CurrentTarget != null)
                acquiredTarget?.Invoke(CurrentTarget);
            searchTimer = searchRate;
        }
        hadTarget = CurrentTarget != null;
    }

    private void OnTriggerEnter(Collider other)
    {
        Targetable targetable = other.transform.root.GetComponent<Targetable>();
        if (targetable == null)
            return;
        if (IsTargetValid(targetable))
        {
            if (TargetVisable(targetable))
            {
                targetable.removed += OnTargetRemoved;
                TargetList.Add(targetable);
                acquiredTarget?.Invoke(targetable);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Targetable targetable = other.transform.root.GetComponent<Targetable>();
        if (targetable == null)
            return;
        if (!IsTargetValid(targetable))
            return;
        TargetList.Remove(targetable);
        targetExitedRange?.Invoke(targetable);

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
    private void OnTargetRemoved(Targetable targetable)
    {
        targetable.removed -= OnTargetRemoved;
        if (CurrentTarget != null && CurrentTarget == targetable)
        {
            lostTarget?.Invoke();
            CurrentTarget = null;
        }
        if (TargetList.Contains(targetable))
        {
            TargetList.Remove(targetable);
        }
    }
    public bool IsTargetValid(Targetable targetable)
    {
        if (targetable.FactionAlignment == null)
            return false;
        return factionAlignment.CanHarm(targetable.FactionAlignment);
    }

    public bool TargetVisable(Targetable targetable)
    {
        if ((targetable != null) && (targetable.enabled))
        {
            Vector3 directionToTargetable = (targetable.transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTargetable) < viewAngle / 2)
            {
                float distanceToTargetable = Vector3.Distance(transform.position, targetable.transform.position);
               
                if (!Physics.Raycast(transform.position, directionToTargetable, distanceToTargetable, obsticleMask))
                {
                    //No Obsticles in way
                    return true;
                }
            }
        }
        return false;
    }
    public Targetable GetNearestTarget()
    {
        int length = TargetList.Count;
        if (length == 0)       
            return null;
        if (length == 0)      
            return null;      

        Targetable nearest = null;
        float distance = float.MaxValue;
        for (int i = TargetList.Count - 1; i >= 0; i--)
        {
            Targetable targetable = TargetList[i];
            if (targetable == null || targetable.IsDead)
            {
                TargetList.RemoveAt(i);
                //OnTargetRemoved(targetable);
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
    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    public void ResetTargetter()
    {
        TargetList.Clear();
        CurrentTarget = null;
        acquiredTarget = null;
        lostTarget = null;
    }
    public void CooldownSearch()
    {
        if (searchTimer == 0f)
        {
            searchTimer = 0f;
            searchReady = false;
        }
        else
        {
            searchTimer -= Time.deltaTime;
            searchReady = false;
        }
    }
}
