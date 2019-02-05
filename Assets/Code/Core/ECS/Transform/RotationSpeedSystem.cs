using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class RotationSpeedSystem : JobComponentSystem
{
    // Use the [BurstCompile] attribute to compile a job with Burst. You may see significant speed ups, so try it!
    [BurstCompile]
    struct RotationSpeedJob : IJobProcessComponentData<Rotation, RotationSpeed>
    {
        // The [ReadOnly] attribute tells the job scheduler that this job cannot write to dT.
        [ReadOnly] public float dT;

        // rotation is read/write in this job
        public void Execute(ref Rotation rotation, [ReadOnly] ref RotationSpeed rotSpeed)
        {
            // Rotate something about its up vector at the speed given by RotationSpeed.
            rotation.Value = math.mul(math.normalize(rotation.Value), quaternion.AxisAngle(math.up(), rotSpeed.Value * dT));
        }
    }

    // OnUpdate runs on the main thread.
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new RotationSpeedJob()
        {
            dT = Time.deltaTime
        };

        return job.Schedule(this, inputDependencies);
    }
}
