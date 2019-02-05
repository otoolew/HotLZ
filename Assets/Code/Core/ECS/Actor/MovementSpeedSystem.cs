using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

// This system updates all entities in the scene with both a MovementSpeed and Position component.
public class MovementSpeedSystem : JobComponentSystem
{
    // Use the [BurstCompile] attribute to compile a job with Burst.
    [BurstCompile]
    struct MovementSpeedJob : IJobProcessComponentData<Position, MovementSpeed>
    {
        // The [ReadOnly] attribute tells the job scheduler that this job cannot write to dT.
        [ReadOnly] public float dT;

        // Position is read/write
        public void Execute(ref Position Position, [ReadOnly] ref MovementSpeed movementSpeed)
        {
            // Move something in the +Z direction at the speed given by MovementSpeedJob.
            // If this thing's Z position is more than 2x its speed, reset Z position to 0.
            float moveSpeed = movementSpeed.Value * dT;
            float moveLimit = movementSpeed.Value * 2;
            Position.Value.z = Position.Value.z < moveLimit ? Position.Value.z + moveSpeed : 0;
        }
    }
    // OnUpdate runs on the main thread.
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new MovementSpeedJob()
        {
            dT = Time.deltaTime
        };

        return job.Schedule(this, inputDeps);
    }
}
