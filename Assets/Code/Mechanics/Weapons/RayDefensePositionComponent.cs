using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayDefensePositionComponent : WeaponComponent
{
    #region Properties and Variables

    [SerializeField]
    private RayDefensePositionWeaponSchematic rayWeaponSchematic;
    public RayDefensePositionWeaponSchematic RayWeaponSchematic { get => rayWeaponSchematic; set => rayWeaponSchematic = value; }

    [SerializeField]
    private int weaponDamage;
    public int WeaponDamage { get => weaponDamage; set => weaponDamage = value; }

    [SerializeField]
    private float weaponCooldown;
    public float WeaponCooldown { get => weaponCooldown; set => weaponCooldown = value; }

    [SerializeField]
    private float weaponRange;
    public float WeaponRange { get => weaponRange; set => weaponRange = value; }

    [SerializeField]
    private float weaponTimer;
    public float WeaponTimer { get => weaponTimer; set => weaponTimer = value; }

    [SerializeField]
    private bool weaponReady;
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
        rayWeaponSchematic.Initialize(this);
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
            rayWeaponSchematic.CooldownWeapon(this);
        }          
        else
        {
            Fire();
        }

    }

    public override void Fire()
    {
        if (weaponReady)
        {
            InSightLine();
            //particleEffect.Play();
            WeaponTimer = rayWeaponSchematic.cooldownTime;
            weaponReady = false;
        }
    }

    public void AimAtTarget()
    {
        if (targettingComponent.CurrentTarget != null)
        {
            var lookDirection = Quaternion.LookRotation(targettingComponent.CurrentTarget.transform.position - towerTurretTransform.position);
            //lookDirection.x = 0;
            //lookDirection.z = 0;
            towerTurretTransform.rotation = Quaternion.RotateTowards(towerTurretTransform.rotation, lookDirection, (TurretRotationSpeed * Time.deltaTime));
        }
    }
    public void InSightLine()
    {
        //var lookDirection = Quaternion.LookRotation(firePoint.position - towerTurretTransform.position);
        Ray ray = new Ray
        {
            origin = firePoint.position,
            direction = firePoint.forward,
        };

        if (Physics.Raycast(ray, out RaycastHit rayHit, layerMask))
        {
            Vector3 hitPoint = rayHit.point;
            Vector3 targetDir = hitPoint - transform.position;
            Debug.DrawRay(ray.origin, targetDir);

            HealthComponent hitUnit = rayHit.collider.GetComponentInParent<HealthComponent>();
            //Debug.Log( GetComponentInParent<UnitActor>().name + " Hit Target " + hitUnit.name );
            if (hitUnit != null)
            {
                int damage = weaponDamage + UnityEngine.Random.Range(1, 10);
                //Debug.Log(GetComponentInParent<UnitActor>().name + " Hit Target " + hitUnit.name + " for " + damage + " damage");
                hitUnit.ApplyDamage(damage);
            }

        }
    }

}
