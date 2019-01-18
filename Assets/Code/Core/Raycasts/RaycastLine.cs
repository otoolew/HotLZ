using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LineRenderer))]
public class RaycastLine : MonoBehaviour
{
    LineRenderer lineRenderer;
    Ray ray;
    RaycastHit rayHit;
    public float RayRange;
    public LayerMask LayerRayMask;
    public float effectDuration = 0.1f;
    public UnityEvent OnRayHit;
    public float damage;
    public bool readyToFire;
    public Transform FirePoint;
    private void Start()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    public void Fire()
    {
        if (readyToFire)
        {
            StopCoroutine(FireFX());
            StartCoroutine(FireFX());
        }
    }
    IEnumerator FireFX()
    {
        readyToFire = false;
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, FirePoint.transform.position);
        ray.origin = FirePoint.transform.position;
        ray.direction = FirePoint.transform.forward;

        if (Physics.Raycast(ray, out rayHit, RayRange, LayerRayMask))
        {
            Debug.Log("Debug RayHit: " + rayHit.collider.name);
            OnRayHit.Invoke();
            lineRenderer.SetPosition(1, rayHit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, ray.origin + ray.direction * RayRange);
            //Debug.Log("Debug RayHit: NOTHING");
        }
        yield return new WaitForSeconds(effectDuration);
        readyToFire = true;
        lineRenderer.enabled = false;
    }
}
