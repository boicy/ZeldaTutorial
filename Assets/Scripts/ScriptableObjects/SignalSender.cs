using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SignalSender : ScriptableObject
{
    public List<SignalListener> listeners = new List<SignalListener>();

    public void Raise()
    {
        Debug.Log("Raising event");

        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            Debug.Log("OnsignalRaised for " + i + "th listener");
            listeners[i].OnSignalRaised();
        }
    }

    public void RegisterListener(SignalListener listener)
    {

        Debug.Log("Registering Listener: " + listener.GetType());
        listeners.Add(listener);
    }

    public void DeRegisterListener(SignalListener listener)
    {
        Debug.Log("removing Listener: " + listener.GetType());
        listeners.Remove(listener);
    }
}
