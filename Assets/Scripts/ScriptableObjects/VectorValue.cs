using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class VectorValue : ScriptableObject
{
    [Header("Value running in game")]
    public Vector2 initialValue;
    [Header("Value by default when starting")]
    public Vector2 defaultValue;

}
