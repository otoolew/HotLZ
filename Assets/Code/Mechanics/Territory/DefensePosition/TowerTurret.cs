using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerTurret : UnitActor
{

    [SerializeField] private FactionAlignment factionAlignment;
    public override FactionAlignment FactionAlignment { get => factionAlignment; set => factionAlignment = value; }

    [SerializeField] private Enums.UnitType unitType;
    public override Enums.UnitType UnitType { get => unitType; set => unitType = value; }

    [SerializeField] private DefenseTower defenseTower;
    public DefenseTower DefenseTower { get => defenseTower; set => defenseTower = value; }

    [SerializeField] private Transform turretTransform;
    public Transform TurretTransform { get => turretTransform; set => turretTransform = value; }

    [SerializeField] private float turretRotationSpeed;
    public float TurrentRotationSpeed { get => turretRotationSpeed; set => turretRotationSpeed = value; }

    [SerializeField] private Targetable currentTarget;
    public Targetable CurrentTarget { get => currentTarget; set => currentTarget = value; }

    [SerializeField] private TargettingComponent targettingComponent;
    public TargettingComponent TargettingComponent { get => targettingComponent; set => targettingComponent = value; }

    [SerializeField] private FieldOfView fieldOfView;
    public FieldOfView FieldOfView { get => fieldOfView; set => fieldOfView = value; }

    [SerializeField] private WeaponComponent weaponComponent;
    public WeaponComponent WeaponComponent { get => weaponComponent; set => weaponComponent = value; }

    [SerializeField] private HealthComponent healthComponent;
    public override HealthComponent HealthComponent { get => healthComponent; set => healthComponent = value; }

    [SerializeField] private bool dead;
    public override bool Dead { get => dead; set => dead = value; }

    [SerializeField] private bool pooled;
    public override bool Pooled { get => pooled; set => pooled = value; }

    public override event Action<UnitActor> removed;

    // Start is called before the first frame update
    void Start()
    {
        targettingComponent.FactionAlignment = factionAlignment;
        healthComponent.OnDeath.AddListener(UnitActorDeath);
        targettingComponent.OnAcquiredTarget.AddListener(HandleTargetAquired);
        targettingComponent.OnLostTarget.AddListener(HandleTargetLost);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTarget != null && !Dead)
        {
            AimAtTarget();
            if (fieldOfView.TargetVisable(CurrentTarget))
            {
                weaponComponent.Fire();
            }
        }
    }

    public void HandleTargetAquired(Targetable targetable)
    {
        currentTarget = targetable;
    }
    public void HandleTargetLost()
    {
        currentTarget = null;
    }
    public void AimAtTarget()
    {
        if (CurrentTarget != null)
        {
            var lookDirection = Quaternion.LookRotation(currentTarget.transform.position - turretTransform.transform.position);
            //lookDirection.x = 0;
            //lookDirection.z = 0;
            turretTransform.transform.rotation = Quaternion.RotateTowards(turretTransform.transform.rotation, lookDirection, (turretRotationSpeed * Time.deltaTime));
        }
    }

    public override void UnitActorDeath()
    {
        removed?.Invoke(this);
        Debug.Log("Tower Destroyed");
    }
}
