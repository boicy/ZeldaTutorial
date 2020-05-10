using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public SignalSender signal;

    public UnityEvent signalEvent;

    public void OnSignalRaised()
    {
        Debug.Log("OnSignalRaised");
        signalEvent.Invoke();
    }

    private void OnEnable()
    {
        Debug.Log("SignalListener is registering with Signal");
        signal.RegisterListener(this);
    }

    private void OnDisable()
    {
        Debug.Log("SignalListener is deregistering with Signal");
        signal.DeRegisterListener(this);
    }
}
