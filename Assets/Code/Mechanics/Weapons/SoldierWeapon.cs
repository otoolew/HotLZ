using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierWeapon : WeaponComponent
{
    #region Properties and Variables
    [SerializeField]
    private TargetController targetController;
    public TargetController TargetController { get => targetController; set => targetController = value; }

    [SerializeField]
    private WeaponSchematic weaponSchematic;
    public WeaponSchematic WeaponSchematic { get => weaponSchematic; set => weaponSchematic = value; }

    [SerializeField]
    private int weaponDamage;
    public override int WeaponDamage { get => weaponDamage; set => weaponDamage = value; }

    [SerializeField]
    private float weaponCooldown;
    public override float WeaponCooldown { get => weaponCooldown; set => weaponCooldown = value; }

    [SerializeField]
    private float weaponRange;
    public override float WeaponRange { get => weaponRange; set => weaponRange = value; }

    [SerializeField]
    private float weaponTimer;
    public override float WeaponTimer { get => weaponTimer; set => weaponTimer = value; }

    [SerializeField]
    private bool weaponReady;
    public override bool WeaponReady { get => weaponReady; set => weaponReady = value; }

    [SerializeField]
    private ParticleSystem particleEffect;
    public ParticleSystem ParticleEffect { get => particleEffect; set => particleEffect = value; }

    [SerializeField]
    private LayerMask layerMask;
    public LayerMask LayerMask { get => layerMask; set => layerMask = value; }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        targetController = GetComponentInChildren<TargetController>();
        weaponSchematic.InitComponent(this);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!weaponReady)
            CooldownWeapon();
    }

    public override void CooldownWeapon()
    {
        if (weaponTimer <= 0)
        {
            weaponTimer = 0;
            weaponReady = true;
        }
        else
        {
            weaponTimer -= Time.deltaTime;
            weaponReady = false;
        }
    }

    public override void FireWeapon()
    {
        if (weaponReady)
        {
            weaponTimer = weaponCooldown;
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
                hitUnit.ApplyDamage(weaponDamage);
            }

        }
    }
}
