using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponComponent : WeaponComponent
{
    [SerializeField] private ProjectileWeaponSchematic projectileWeaponSchematic;

    [SerializeField] private float weaponTimer;
    public float WeaponTimer { get => weaponTimer; set => weaponTimer = value; }

    [SerializeField] private bool weaponReady;
    public bool WeaponReady { get => weaponReady; set => weaponReady = value; }

    [SerializeField] private ParticleSystem particleEffect;
    public ParticleSystem ParticleEffect { get => particleEffect; set => particleEffect = value; }

    [SerializeField] private Transform firePoint;
    public Transform FirePoint { get => firePoint; set => firePoint = value; }

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
    void Update()
    {
        if (!weaponReady)
            projectileWeaponSchematic.CooldownWeapon(this);
    }

    public override void Fire()
    {
        weaponTimer = projectileWeaponSchematic.cooldownTime;
        Instantiate(projectileWeaponSchematic.munitionPrefab, firePoint.transform);
    }
}
