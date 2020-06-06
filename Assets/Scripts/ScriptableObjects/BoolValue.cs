using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class BoolValue : ResetableScriptableObject
{

    public bool initialValue;
    public bool RuntimeValue;

    public override void Reset()
    {
        RuntimeValue = initialValue;        
    }
}