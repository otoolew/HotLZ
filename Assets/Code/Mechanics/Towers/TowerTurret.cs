using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerTurret : Targetable
{
    [SerializeField] private TurretTransform turretTransform;
    public TurretTransform TurretTransform { get => turretTransform; set => turretTransform = value; }

    [SerializeField] private Transform turretBaseTransform;
    public Transform TurretBaseTransform { get => turretBaseTransform; set => turretBaseTransform = value; }

    [SerializeField] private float turretRotationSpeed;
    public float TurrentRotationSpeed { get => turretRotationSpeed; set => turretRotationSpeed = value; }

    [SerializeField] private AITargetingComponent targettingComponent;
    public AITargetingComponent TargettingComponent { get => targettingComponent; set => targettingComponent = value; }

    [SerializeField] private WeaponComponent weaponComponent;
    public WeaponComponent WeaponComponent { get => weaponComponent; set => weaponComponent = value; }

    public LayerMask targetLayer;

    private void InitializeComponents()
    {
        //if (parentDefensePosition == null)
        //    parentDefensePosition = GetComponentInParent<DefensePosition>();
        //if (turretTransform == null)
        //    turretTransform = GetComponentInChildren<TurretTransform>();
        //if (targettingComponent == null)
        //    targettingComponent = GetComponentInChildren<AITargetingComponent>();
        //if (weaponComponent == null)
        //    weaponComponent = GetComponent<WeaponComponent>();

        //targettingComponent.FactionAlignment = parentDefensePosition.FactionAlignment;
        //targettingComponent.ResetTargetter();

    }

    private void OnEnable()
    {
        //InitializeComponents();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (targettingComponent.CurrentTarget != null)
        {
            if (turretBaseTransform != null)
                AimTurretBase();
            AimTurret();
            if (TargetInSight(targettingComponent.CurrentTarget))
            {
                weaponComponent.Fire();
            }
        }
    }
    public void OnFactionAlignmentChange(FactionAlignment newFactionAlignment)
    {
        FactionAlignment = newFactionAlignment;
        targettingComponent.FactionAlignment = newFactionAlignment;
        targettingComponent.ResetTargetter();
    }

    public void AimTurretBase()
    {
        if (targettingComponent.CurrentTarget != null)
        {
            var lookDirection = Quaternion.LookRotation(targettingComponent.CurrentTarget.transform.position - turretTransform.transform.position);
            //lookDirection.x = 0;
            //lookDirection.z = 0;
            turretTransform.transform.rotation = Quaternion.RotateTowards(turretTransform.transform.rotation, lookDirection, (turretRotationSpeed * Time.deltaTime));
        }
    }
    public void AimTurret()
    {
        if (targettingComponent.CurrentTarget != null)
        {
            var lookDirection = Quaternion.LookRotation(targettingComponent.CurrentTarget.transform.position - turretTransform.transform.position);
            turretTransform.transform.rotation = Quaternion.RotateTowards(turretTransform.transform.rotation, lookDirection, (turretRotationSpeed * Time.deltaTime));
        }
    }
    public bool TargetInSight(Targetable targetable)
    {
        if ((targetable != null) && (!targetable.IsDead))
        {
            Vector3 directionToTargetable = (targetable.transform.position - transform.position).normalized;

            float distanceToTargetable = Vector3.Distance(transform.position, targetable.transform.position);

            if (!Physics.Raycast(transform.position, directionToTargetable, distanceToTargetable, targetLayer))
            {
                //No Obsticles in way
                return true;
            }         
        }
        return false;
    }
    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    public void TowerDestroyed()
    {
        Debug.Log("Tower Destroyed");
    }
}
