using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class FloatValue : ResetableScriptableObject
{
    public float initialValue;

    public float RuntimeValue;

    public override void Reset()
    {
        Debug.Log(this.name + " is resetting runtime value " + RuntimeValue + " to initial value: " + initialValue);
        RuntimeValue = initialValue;
    }
}
