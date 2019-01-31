using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestActorTracking : ActorTracking
{
    #region Fields and Properties
    //[SerializeField] private Actor currentTarget;
    //public Actor CurrentTarget { get => currentTarget; set => currentTarget = value; }

    //[SerializeField]
    //private bool hadTarget;
    //public bool HadTarget { get => hadTarget; set => hadTarget = value; }

    //[SerializeField]private float searchRate;
    //public override float SearchRate { get => searchRate; set => searchRate = value; }

    //[SerializeField]private float searchTimer;
    //public override float SearchTimer { get => searchTimer; set => searchTimer = value; }

    public List<Actor> ActorsTrackedList;
    #endregion

    #region Events Actions and Handlers
    public EventActorEnteredRange OnActorEntersRange;
    public EventActorExitedRange OnActorExitsRange;
    public EventAcquiredActor OnAcquiredActor;
    public EventLostActor OnLostActor;

    protected override void OnActorRemoved(Actor actor)
    {
        for (int i = 0; i < ActorsTrackedList.Count; i++)
        {
            if (ActorsTrackedList[i] == actor)
            {
                ActorsTrackedList.RemoveAt(i);
                break;
            }
        }
    }
    #endregion
    void OnEnable()
    {
        ActorsTrackedList.Clear();
    }
    // Start is called before the first frame update
    void Start()
    {
        ActorsTrackedList = new List<Actor>();
    }

    // Update is called once per frame

    /// <summary>
    /// On entering the trigger, a valid targetable is added to the tracking list.
    /// </summary>
    /// <param name="other">The other collider in the collision</param>
    private void OnTriggerEnter(Collider other)
    {     
        Actor actor = other.transform.root.GetComponent<Actor>();
        if (actor == null)
            return;
        actor.OnActorRemoved += OnActorRemoved;
        ActorsTrackedList.Add(actor);
        OnActorEntersRange?.Invoke(actor);
    }
    /// <summary>
    /// On exiting the trigger, a valid targetable is removed from the tracking list.
    /// </summary>
    /// <param name="other">The other collider in the collision</param>
    private void OnTriggerExit(Collider other)
    {
        var actor = other.GetComponentInParent<Actor>();
        if (actor == null)
        {
            return;
        }
        ActorsTrackedList.Remove(actor);
        OnActorExitsRange?.Invoke(actor);
    }
}
