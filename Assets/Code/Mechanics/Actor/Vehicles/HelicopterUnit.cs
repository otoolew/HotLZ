using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterUnit : Targetable
{

    [SerializeField] private Enums.UnitType unitType;
    public Enums.UnitType UnitType { get => unitType; set => unitType = value; }

    //public override event Action<Targetable> targetRemoved;

    public void UnitActorDeath()
    {
        //dead = true;
        StartCoroutine("DeathSequence");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
    }
}
