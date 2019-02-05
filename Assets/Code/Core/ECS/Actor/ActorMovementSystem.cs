using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class ActorMovementSystem : JobComponentSystem
{
    [BurstCompile]
    struct MoveForwardRotation : IJobProcessComponentData<Position, Rotation, MovementSpeed>
    {
        public float dt;

        public void Execute(ref Position position, [ReadOnly] ref Rotation rotation, [ReadOnly] ref MovementSpeed speed)
        {
            position = new Position
            {
                Value = position.Value + (dt * speed.Value * math.forward(rotation.Value))
            };
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var moveForwardRotationJob = new MoveForwardRotation
        {
            dt = Time.deltaTime
        };
        var moveForwardRotationJobHandle = moveForwardRotationJob.Schedule(this, inputDeps);
        return moveForwardRotationJobHandle;
    }
}
