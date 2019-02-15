using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public LayerMask layerMask;

    [SerializeField] private WeaponComponent equippedWeapon;
    public WeaponComponent EquippedWeapon { get => equippedWeapon; set => equippedWeapon = value; }

    [SerializeField] private WeaponComponent rocketWeapon;
    public WeaponComponent RocketWeapon { get => rocketWeapon; set => rocketWeapon = value; }

    private void Start()
    {
        //equippedWeapon = weaponComponents[0];
        //for (int i = 0; i < weaponComponents.Length; i++)
        //{
        //    weaponComponents[i].FactionAlignment = GetComponent<Faction>().FactionAlignment;
        //}
    }
    private void Update()
    {
        AimPoint();
        if (Input.GetMouseButtonDown(0))
            equippedWeapon.Fire();
        if (Input.GetMouseButtonDown(1))
            rocketWeapon.Fire();

    }

    private void AimPoint()
    {
        //Vector3 mousePosition = new Vector3(Input.mousePosition.x, transform.position.y, Input.mousePosition.z);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit rayHit, layerMask))
        {
            Vector3 hitPoint = rayHit.point;

            Vector3 targetDir = hitPoint - equippedWeapon.transform.position;
            // The step size is equal to speed times frame time.
            //float step = runSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(equippedWeapon.transform.forward, targetDir, 10f, 0.0f);
            //newDir.y = 0;
            //Debug.DrawRay(transform.position, newDir, Color.red);
            // Move our position a step closer to the target.
            equippedWeapon.transform.rotation = Quaternion.LookRotation(targetDir);
            //Debug.DrawRay(equippedWeapon.transform.position, targetDir);
        }
    }
}
