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

    [SerializeField] private float searchCooldown;
    public float SearchCooldown { get => searchCooldown; set => searchCooldown = value; }

    [SerializeField] private float searchTimer;
    public float SearchTimer { get => searchTimer; set => searchTimer = value; }

    [SerializeField] private bool searchReady;
    public bool SearchReady { get => searchReady; set => searchReady = value; }

    public List<Targetable> TargetList = new List<Targetable>();

    public event Action<Targetable> acquiredTarget;
    public event Action lostTarget;
    // Start is called before the first frame update
    void Start()
    {
        factionAlignment = GetComponentInParent<FactionComponent>().FactionAlignment;
    }

    // Update is called once per frame
    void Update()
    {
        if (searchReady)
        {
            CurrentTarget = GetNearestTarget();
            if(CurrentTarget != null)
                Debug.Log(GetComponentInParent<Targetable>().name + " Current Target is " + CurrentTarget.name);
            searchTimer = searchCooldown;
        }
        else
        {
            CooldownSearch();       
        }

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
                targetable.dead += OnTargetRemoved;
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
        OnTargetRemoved(targetable);
    }
    private void OnTargetRemoved(Targetable targetable)
    {
        targetable.dead -= OnTargetRemoved;
        if (targetable == CurrentTarget)
        {
            CurrentTarget = null;
            lostTarget?.Invoke();
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
        if (TargetList.Count == 0)      
            return null;      

        Targetable nearest = null;
        float distance = float.MaxValue;
        for (int i = TargetList.Count - 1; i >= 0; i--)
        {
            Targetable targetable = TargetList[i];
            if (targetable == null || targetable.IsDead)
            {
                OnTargetRemoved(targetable);
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
    public void ClearTargetList()
    {
        TargetList.Clear();
    }
    public void CooldownSearch()
    {
        if (searchTimer <= 0)
        {
            searchTimer = 0;
            searchReady = true;
        }
        else
        {
            searchTimer -= Time.deltaTime;
            searchReady = false;
        }
    }
}
