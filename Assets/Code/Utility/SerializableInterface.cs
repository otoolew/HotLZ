using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SerializableInterface
{
    /// <summary>
    /// Unity component that gets serialized that is of our interface type
    /// </summary>
    public UnityEngine.Object unityObjectReference;


}
/// <summary>
/// A generic solution to allow the serialization of interfaces in Unity game objects
/// </summary>
/// <typeparam name="T">Any interface implementing ISerializableInterface</typeparam>
[Serializable]
public class SerializableInterface<T> : SerializableInterface where T : ISerializableInterface
{
    T interfaceReference;

    /// <summary>
    /// Retrieves the interface from the unity component and caches it
    /// </summary>
    public T GetInterface()
    {
        if (interfaceReference == null && unityObjectReference != null)
        {
            interfaceReference = (T)(ISerializableInterface)unityObjectReference;
        }

        return interfaceReference;
    }
}

/// <summary>
/// Base interface from which all serializable interfaces must derive
/// </summary>
public interface ISerializableInterface
{
}
