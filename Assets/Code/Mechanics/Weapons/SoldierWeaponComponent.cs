using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierWeaponComponent : WeaponComponent
{
    #region Properties and Variables

    [SerializeField]
    private SoldierWeaponSchematic soldierWeaponSchematic;
    public SoldierWeaponSchematic SoldierWeaponSchematic { get => soldierWeaponSchematic; set => soldierWeaponSchematic = value; }

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

    [SerializeField] private Transform firePoint;
    public Transform FirePoint { get => firePoint; set => firePoint = value; }

    [SerializeField] private ParticleSystem particleEffect;
    public ParticleSystem ParticleEffect { get => particleEffect; set => particleEffect = value; }

    [SerializeField]
    private LayerMask layerMask;
    public LayerMask LayerMask { get => layerMask; set => layerMask = value; }

    #endregion

    public override void InitComponent()
    {
        soldierWeaponSchematic.Initialize(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitComponent();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!weaponReady)
            soldierWeaponSchematic.CooldownWeapon(this);
    }

    public override void Fire()
    {
        if (weaponReady)
        {
            FireRay();
            particleEffect.Play();
            weaponTimer = soldierWeaponSchematic.cooldownTime;
            weaponReady = false;
        }

    }

    public bool InSightLine()
    {
        Ray ray = new Ray
        {
            origin = firePoint.position,
            direction = firePoint.forward,
        };

        if (Physics.Raycast(ray, out RaycastHit rayHit, layerMask))
        {
            Vector3 hitPoint = rayHit.point;
            Vector3 targetDir = hitPoint - firePoint.position;
            Debug.DrawRay(ray.origin, targetDir);
            return true;
        }
        return false;
    }
    /// <summary>
    /// Fires Ray to Hit Enemy. This is redundent but I may make this a line renderer
    /// </summary>
    public void FireRay()
    {
        Ray ray = new Ray
        {
            origin = firePoint.position,
            direction = firePoint.forward,
        };

        if (Physics.Raycast(ray, out RaycastHit rayHit, layerMask))
        {
            DamageZone targetHit = rayHit.collider.GetComponent<DamageZone>();
            //Debug.Log( GetComponentInParent<UnitActor>().name + " Hit Target " + hitUnit.name );
            if (targetHit != null)
            {              
                targetHit.damageableBehaviour.TakeDamage(weaponDamage, GetComponent<SoldierUnit>().FactionProvider);
                Debug.DrawRay(firePoint.position, firePoint.forward);
                //int damage = weaponDamage + UnityEngine.Random.Range(10, 50);
                //Debug.Log(GetComponentInParent<UnitActor>().name + " Hit Target " + hitUnit.name + " for " + damage + " damage");
                //hitUnit.ApplyDamage(damage);
            }

        }
    }
}
