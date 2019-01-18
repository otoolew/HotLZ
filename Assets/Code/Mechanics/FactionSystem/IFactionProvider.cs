using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactionProvider : ISerializableInterface
{
    bool CanHarm(IFactionProvider other);
}

[Serializable]
public class SerializableIAlignmentProvider : SerializableInterface<IFactionProvider>
{
}
