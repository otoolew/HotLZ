using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : WeaponComponent
{

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
    // Start is called before the first frame update
    void Start()
    {
        WeaponSchematic.InitComponent(this);
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
        Vector3 startPos = FirePoint.transform.localPosition;
        Vector3 endPos = -FirePoint.transform.right * weaponRange;
        //Vector3 endPos = FirePoint.transform.forward * weaponRange;
        //Debug.DrawRay(startPos, endPos);
        if (Physics.Raycast(startPos, endPos, out RaycastHit rayHit, weaponRange, layerMask))
        {
            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, rayHit.point);
            //lineRenderer.SetPosition(1, rayHit.collider.gameObject.GetComponent<Transform>().transform.position);
            Debug.Log(rayHit.collider.gameObject.name + " Hit");

        }
        else
        {
            lineRenderer.SetPosition(1, endPos);
        }
        StopCoroutine(FireFX());
        StartCoroutine(FireFX());
    }
    IEnumerator FireFX()
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(fxDuration);
        lineRenderer.enabled = false;
    }

}
