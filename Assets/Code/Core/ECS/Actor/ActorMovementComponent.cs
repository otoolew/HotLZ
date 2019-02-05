
using Unity.Entities;

public struct ActorMovement : ISharedComponentData { }

public class ActorMovementComponent : SharedComponentDataWrapper<ActorMovement> { }
