using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Special EventArgs class to hold information about game entities
/// </summary>
/// <typeparam name="T"></typeparam>
public class EntityEventArg<T> : EventArgs
{
    public T entity;

    public EntityEventArg()
    {
        entity = default(T);
    }

    public EntityEventArg(T info)
    {
        this.entity = info;
    }
}
