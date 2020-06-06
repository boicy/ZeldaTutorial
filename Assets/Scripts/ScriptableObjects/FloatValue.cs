using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class FloatValue : ResetableScriptableObject
{
    public float initialValue;

    public float RuntimeValue;

    public override void Reset()
    {
        RuntimeValue = initialValue;
    }
}
