using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(HealthController))]
public class UnitActor : MonoBehaviour
{
    [SerializeField]
    private Enums.UnitType unitType;
    public Enums.UnitType UnitType { get => unitType; set => unitType = value; }

    [SerializeField]
    private FactionAlignment faction;
    public FactionAlignment Faction { get => faction; set => faction = value; }

    [SerializeField]
    private bool dead;
    public bool Dead { get => dead; set => dead = value; }

    [SerializeField]
    private bool pooled;
    public bool Pooled { get => pooled; set => pooled = value; }

    #region Events and Handlers
    public event Action<UnitActor> removed;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<HealthController>().OnDeath.AddListener(UnitActorDeath);
    }

    private void UnitActorDeath()
    {
        dead = true;
        GetComponent<Animator>().SetBool("IsDead", true);
        GetComponent<Animator>().Play("Dead");
        if (removed != null)
        {
            removed(this);
        }
        StartCoroutine("DeathSequence");
    }

    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
    }
}
