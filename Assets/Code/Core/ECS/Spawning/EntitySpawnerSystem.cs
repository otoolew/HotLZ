using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class EntitySpawnerSystem : ComponentSystem
{
    ComponentGroup m_Spawners;

    protected override void OnCreateManager()
    {
        m_Spawners = GetComponentGroup(typeof(EntitySpawner), typeof(Position));
    }

    protected override void OnUpdate()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            Debug.Log("Spawn");
            // Get all the spawners in the scene.
            using (var spawners = m_Spawners.ToEntityArray(Allocator.TempJob))
            {
                foreach (var spawner in spawners)
                {
                    // Create an entity from the prefab set on the spawner component.
                    var prefab = EntityManager.GetSharedComponentData<EntitySpawner>(spawner).prefab;
                    var entity = EntityManager.Instantiate(prefab);

                    // Copy the position of the spawner to the new entity.
                    var position = EntityManager.GetComponentData<Position>(spawner);
                    EntityManager.SetComponentData(entity, position);

                    // Destroy the spawner so this system only runs once.
                    EntityManager.DestroyEntity(spawner);
                }
            }
        }
    }
}