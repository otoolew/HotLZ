using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class StringReference{

    public bool UseConstant = true;
    public string ConstantValue;
    public StringReference Variable;

    public StringReference()
    { }

    public StringReference(string value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public string Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }

    public static implicit operator string(StringReference reference)
    {
        return reference.Value;
    }
}
