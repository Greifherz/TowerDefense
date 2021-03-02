using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTriggerDetector : MonoBehaviour
{
    private Action<GameObject> EnterCallback;
    private Action<GameObject> ExitCallback;

    public void SetEnterCallback(Action<GameObject> callbackToSet)
    {
        EnterCallback = callbackToSet;
    }

    public void SetExitCallback(Action<GameObject> callbackToSet)
    {
        ExitCallback = callbackToSet;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        EnterCallback?.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        ExitCallback?.Invoke(other.gameObject);
    }
}
