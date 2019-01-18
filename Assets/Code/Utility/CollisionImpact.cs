using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionImpact : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log( gameObject.name + " " + collision.impulse.sqrMagnitude + " Collision Impulse -> " + collision.gameObject.name);
        Debug.Log( gameObject.name + " " + collision.relativeVelocity.sqrMagnitude + " Relative Velocity -> " + collision.gameObject.name);


    }
}
