using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseWeapon : WeaponComponent
{
    #region Properties and Variables

    [SerializeField]
    private WeaponSchematic weaponSchematic;
    public WeaponSchematic WeaponSchematic { get => weaponSchematic; set => weaponSchematic = value; }

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
        weaponSchematic.Initialize(this);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!weaponReady)
            weaponSchematic.CooldownWeapon(this);
        if(weaponReady && InSightLine())
        {
            Fire();
        }
    }

    public override void Fire()
    {
        Debug.Log("Defense Weapon Fired!");
        weaponReady = false;
        weaponTimer = weaponSchematic.cooldownTime;      
    }
    public bool InSightLine()
    {
        Ray ray = new Ray
        {
            origin = transform.position,
            direction = transform.forward,
        };

        if (Physics.Raycast(ray, out RaycastHit rayHit, weaponRange, layerMask))
        {
            Vector3 hitPoint = rayHit.point;
            Vector3 targetDir = hitPoint - transform.position;
            Debug.DrawRay(ray.origin, targetDir);
            return true;
        }
        return false;
    }
}
