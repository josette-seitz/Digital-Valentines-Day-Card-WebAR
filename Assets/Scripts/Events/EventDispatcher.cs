using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Pub/sub event system to provide asynchronous messaging between classes and mitigate hard dependencies.
/// </summary>
public class EventDispatcher : SingletonBehavior<EventDispatcher>
{
    /// <summary>
    /// Returns true if an instance exists.
    /// </summary>
    public static bool IsInstantiated
    {
        get
        {
            return _instance != null;
        }
    }

    private UnityEvent<GameObject> _unityEvent = new UnityEvent<GameObject>();

    public void RegisterListener(UnityAction<GameObject> callback)
    {
        _unityEvent.AddListener(callback);
    }
    public void UnregisterListener(UnityAction<GameObject> callback)
    {
        _unityEvent.RemoveListener(callback);
    }

    public void Dispatch(GameObject eventArg)
    {
        _unityEvent.Invoke(eventArg);
    }
}
