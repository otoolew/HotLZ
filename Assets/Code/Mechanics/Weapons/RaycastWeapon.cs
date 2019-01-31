using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : WeaponComponent
{
    private RaycastHit raycastHit;

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

    public void InitWeapon()
    {
        weaponDamage = weaponSchematic.WeaponDamage;
        weaponRange = weaponSchematic.WeaponRange;
        weaponTimer = weaponSchematic.WeaponCooldown;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponTimer >= 0)
            CooldownWeapon();
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


    public override void FireWeapon()
    {
        //lineRenderer.enabled = true;
        //lineRenderer.SetPosition(0, FirePoint.transform.forward);

        //Ray ray = new Ray
        //{
        //    origin = transform.position,
        //    direction = transform.forward,
        //};

        //if (Physics.Raycast(ray, out RaycastHit rayHit, layerMask))
        //{
        //    Vector3 hitPoint = rayHit.point;
        //    Vector3 targetDir = hitPoint - transform.position;
        //    Debug.DrawRay(ray.origin, targetDir);
        //    lineRenderer.SetPosition(0, rayHit.point);
        //    HealthController hitUnit = rayHit.collider.GetComponentInParent<HealthController>();
        //    if (hitUnit != null)
        //    {
        //        hitUnit.ApplyDamage(weaponDamage);
        //    }
        //}

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
