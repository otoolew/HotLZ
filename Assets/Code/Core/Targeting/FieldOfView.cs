using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private TargettingComponent targettingComponent;
    public TargettingComponent TargettingComponent { get => targettingComponent; set => targettingComponent = value; }

    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obsticleMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool TargetVisable(Targetable targetable)
    {
        if((targetable!= null) && (targetable.enabled))
        {
            Vector3 directionToTargetable = (targetable.transform.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, directionToTargetable) < viewAngle / 2)
            {
                float distanceToTargetable = Vector3.Distance(transform.position, targetable.transform.position);
                if(!Physics.Raycast(transform.position, directionToTargetable, distanceToTargetable, obsticleMask))
                {
                    //No Obsticles in way
                    return true;
                }
            }
        }
        return false;
    }
    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
