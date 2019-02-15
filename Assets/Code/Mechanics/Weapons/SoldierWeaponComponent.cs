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
            InSightLine();
            particleEffect.Play();
            weaponReady = false;
        }

    }
    public void InSightLine()
    {
        Ray ray = new Ray
        {
            origin = transform.position,
            direction = transform.forward,
        };

        if (Physics.Raycast(ray, out RaycastHit rayHit, layerMask))
        {
            Vector3 hitPoint = rayHit.point;
            Vector3 targetDir = hitPoint - transform.position;
            Debug.DrawRay(ray.origin, targetDir);

            HealthController hitUnit = rayHit.collider.GetComponentInParent<HealthController>();
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
