using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerTurret : Targetable
{

    [SerializeField] private DefenseTower defenseTower;
    public DefenseTower DefenseTower { get => defenseTower; set => defenseTower = value; }

    [SerializeField] private Transform turretTransform;
    public Transform TurretTransform { get => turretTransform; set => turretTransform = value; }

    [SerializeField] private float turretRotationSpeed;
    public float TurrentRotationSpeed { get => turretRotationSpeed; set => turretRotationSpeed = value; }

    [SerializeField] private Targetable currentTarget;
    public Targetable CurrentTarget { get => currentTarget; set => currentTarget = value; }

    //[SerializeField] private TargettingComponent targettingComponent;
    //public TargettingComponent TargettingComponent { get => targettingComponent; set => targettingComponent = value; }

    [SerializeField] private WeaponComponent weaponComponent;
    public WeaponComponent WeaponComponent { get => weaponComponent; set => weaponComponent = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(currentTarget != null)
        {
            AimAtTarget();
            //if (fieldOfView.TargetVisable(CurrentTarget))
            //{
            //    weaponComponent.Fire();
            //}
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

    public void UnitActorDeath()
    {
        Debug.Log("Tower Destroyed");
    }
}
