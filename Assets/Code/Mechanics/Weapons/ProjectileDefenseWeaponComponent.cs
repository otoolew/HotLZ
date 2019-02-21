using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDefenseWeaponComponent : WeaponComponent
{
    #region Properties and Variables

    [SerializeField] private ProjectileDefenseWeaponSchematic projectileWeaponSchematic;
    public ProjectileDefenseWeaponSchematic ProjectileWeaponSchematic { get => projectileWeaponSchematic; set => projectileWeaponSchematic = value; }

    [SerializeField] private int weaponDamage;
    public int WeaponDamage { get => weaponDamage; set => weaponDamage = value; }

    [SerializeField] private float weaponCooldown;
    public float WeaponCooldown { get => weaponCooldown; set => weaponCooldown = value; }

    [SerializeField] private float weaponRange;
    public float WeaponRange { get => weaponRange; set => weaponRange = value; }

    [SerializeField] private float weaponTimer;
    public float WeaponTimer { get => weaponTimer; set => weaponTimer = value; }

    [SerializeField] private bool weaponReady;
    public bool WeaponReady { get => weaponReady; set => weaponReady = value; }

    [SerializeField] private ParticleSystem particleEffect;
    public ParticleSystem ParticleEffect { get => particleEffect; set => particleEffect = value; }

    [SerializeField] private Transform firePoint;
    public Transform FirePoint { get => firePoint; set => firePoint = value; }

    [SerializeField] private TargettingComponent targettingComponent;
    public TargettingComponent TargettingComponent { get => targettingComponent; set => targettingComponent = value; }

    [SerializeField] private Transform towerTurretTransform;
    public Transform TowerTurretTransform { get => towerTurretTransform; set => towerTurretTransform = value; }

    [SerializeField] private float turretRotationSpeed;
    public float TurretRotationSpeed { get => turretRotationSpeed; set => turretRotationSpeed = value; }

    [SerializeField]
    private LayerMask layerMask;
    public LayerMask LayerMask { get => layerMask; set => layerMask = value; }

    #endregion

    public override void InitComponent()
    {
        projectileWeaponSchematic.Initialize(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitComponent();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        AimAtTarget();
        if (!weaponReady)
        {
            projectileWeaponSchematic.CooldownWeapon(this);
        }
        else
        {
            if(targettingComponent.CurrentTarget != null)
                projectileWeaponSchematic.TriggerWeaponFire(this);
        }

    }

    public override void Fire()
    {
        if (weaponReady)
        {
            //particleEffect.Play();
            weaponReady = false;
            weaponTimer = projectileWeaponSchematic.cooldownTime;
            Instantiate(projectileWeaponSchematic.munitionPrefab, firePoint.transform);
        }
    }

    public void AimAtTarget()
    {
        if (targettingComponent.CurrentTarget != null)
        {
            var lookDirection = Quaternion.LookRotation(targettingComponent.CurrentTarget.transform.position - towerTurretTransform.position);
            towerTurretTransform.rotation = Quaternion.RotateTowards(towerTurretTransform.rotation, lookDirection, (TurretRotationSpeed * Time.deltaTime));
        }
    }
 
}
