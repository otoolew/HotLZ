using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeaponController : WeaponController
{
    // Start is called before the first frame update
    private RaycastHit raycastHit;

    [SerializeField]
    private RaycastWeaponSchematic rayWeaponSchematic;
    //public RaycastWeaponSchematic RayWeaponSchematic { get => rayWeaponSchematic; set => rayWeaponSchematic = value; }

    [SerializeField]
    private int weaponDamage;
    public int WeaponDamage { get => weaponDamage; set => weaponDamage = value; }

    [SerializeField]
    private float weaponRange;
    public float WeaponRange { get => weaponRange; set => weaponRange = value; }

    [SerializeField]
    private float weaponCooldown;
    public float WeaponCooldown { get => weaponCooldown; set => weaponCooldown = value; }

    [SerializeField]
    private float weaponTimer;
    public float WeaponTimer { get => weaponTimer; set => weaponTimer = value; }

    [SerializeField]
    private bool weaponReady;
    public bool WeaponReady { get => weaponReady; set => weaponReady = value; }

    [SerializeField]
    private LineRenderer lineRenderer;
    public LineRenderer LineRenderer { get => lineRenderer; set => lineRenderer = value; }

    [SerializeField]
    private Transform firePoint;
    public Transform FirePoint { get => firePoint; set => firePoint = value; }

    [SerializeField]
    private float fxDuration;
    public float FxDuration { get => fxDuration; set => fxDuration = value; }

    [SerializeField]
    private LayerMask layerMask;
    public LayerMask LayerMask { get => layerMask; set => layerMask = value; }

    public override void InitComponent()
    {
        weaponDamage = rayWeaponSchematic.weaponDamage;
        weaponRange = rayWeaponSchematic.weaponRange;
        weaponCooldown = rayWeaponSchematic.cooldownTime;
        weaponTimer = rayWeaponSchematic.cooldownTime;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitComponent();
    }

    public override void CooldownWeapon()
    {
        if (WeaponTimer <= 0)
        {
            WeaponTimer = 0;
            WeaponReady = true;
        }
        else
        {
            WeaponTimer -= Time.deltaTime;
            WeaponReady = false;
        }
    }

    public override void Fire()
    {
        StopCoroutine(FireFX());
        StartCoroutine(FireFX());
    }
    IEnumerator FireFX()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        Ray ray = new Ray
        {
            origin = transform.position,
            direction = transform.forward,
        };

        if (Physics.Raycast(ray, out raycastHit, weaponRange, layerMask))
        {
            Vector3 hitPoint = raycastHit.point;
            Vector3 targetDir = hitPoint - transform.position;
            Debug.DrawRay(ray.origin, targetDir);
            lineRenderer.SetPosition(0, raycastHit.point);
            HealthController hitUnit = raycastHit.collider.GetComponentInParent<HealthController>();
            if (hitUnit != null)
            {
                hitUnit.ApplyDamage(weaponDamage);
            }

            lineRenderer.SetPosition(1, raycastHit.point);

            //Debug.Log("Hit Success " + raycastHit.collider.GetComponent<HealthController>());
        }
        else
        {
            lineRenderer.SetPosition(1, ray.origin + ray.direction * weaponRange);
        }

        yield return new WaitForSeconds(fxDuration);
        lineRenderer.enabled = false;
    }
}
