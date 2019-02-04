using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerritoryTower : MonoBehaviour
{
    [SerializeField] private Territory territory;
    public Territory Territory { get => territory; set => territory = value; }

    [SerializeField] private FactionAlignment faction;
    public FactionAlignment Faction { get => faction; set => faction = value; }

    [SerializeField] private Transform towerTurretTransform;
    public Transform TowerTurretTransform { get => towerTurretTransform; set => towerTurretTransform = value; }

    [SerializeField] private ActorTargeter actorTargeter;
    public ActorTargeter ActorTargeter { get => actorTargeter; set => actorTargeter = value; }

    [SerializeField] private float turretRotationSpeed;
    public float TurretRotationSpeed { get => turretRotationSpeed; set => turretRotationSpeed = value; }

    #region Events and Handlers
    public void HandleTerritoryOwnerChange(FactionAlignment newfaction)
    {
        Faction = newfaction;
        actorTargeter.Faction = newfaction;
        actorTargeter.ResetTargetter();
    }
    #endregion  
    // Start is called before the first frame update
    void Start()
    {
        territory = GetComponentInParent<Territory>();
        faction = territory.Faction;
        actorTargeter.Faction = territory.Faction;
        territory.OnTerritoryOwnerChange.AddListener(HandleTerritoryOwnerChange);
    }

    // Update is called once per frame
    void Update()
    {
        AimAtTarget();
    }

    public void AimAtTarget()
    {
        if(actorTargeter.CurrentTarget != null)
        {
            var lookDirection = Quaternion.LookRotation(actorTargeter.CurrentTarget.transform.position - towerTurretTransform.position);
            lookDirection.x = 0;
            lookDirection.z = 0;
            towerTurretTransform.rotation = Quaternion.RotateTowards(towerTurretTransform.rotation, lookDirection, (TurretRotationSpeed * Time.deltaTime));
        }
    }
    
}
