// ----------------------------------------------------------------------------
//  William O'Toole 
//  Project: Starship
//  SEPT 2018
// ----------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// TODO: Make this a Pooled GO
/// </summary>
public class Munition : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    public float LifeTime { get => lifeTime; set => lifeTime = value; }

    [SerializeField] private float speed;
    public float Speed { get => speed; set => speed = value; }

    [SerializeField] private int damage;
    public int Damage { get => damage; set => damage = value; }

    // Use this for initialization
    private void Start()
    {
        transform.parent = null;            // Unparent the munition
        Destroy(gameObject, lifeTime);      // Destroy after a specified time.
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider collisonObject)
    {
        HealthComponent healthComponent = collisonObject.GetComponent<HealthComponent>();
        if (healthComponent != null)
        {           
            healthComponent.ApplyDamage(damage);          
        }
        Destroy(gameObject); // TODO: Deactivate and return to Pool
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

}
