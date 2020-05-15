using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver
{
    public bool initialValue;

    [HideInInspector]
    public bool RuntimeValue;

    public void OnBeforeSerialize()
    {
        RuntimeValue = initialValue;
    }

    public void OnAfterDeserialize()
    {

    }

}