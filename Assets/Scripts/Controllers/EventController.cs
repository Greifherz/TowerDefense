using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public static EventController Instance; //Really don't like singletons, need to keep an eye so it doesn't become an anti-pattern
    
    private event Action<IEvent> EventPipeline = (gameEvent) => { };
    
    private Queue<IEvent> EventPool = new Queue<IEvent>();

    public void RegisterListener(Action<IEvent> listenAction)
    {
        EventPipeline += listenAction;
    }

    public void UnregisterListener(Action<IEvent> listenAction)
    {
        EventPipeline -= listenAction;
    }

    public void Raise(IEvent paperEvent)
    {
        EventPool.Enqueue(paperEvent);
    }

    private void Awake() //Besides being in Awake, I've changed the script execution order so it runs before anything else
    {                    //This is something to keep an eye on, thanks to the singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        StartCoroutine(Pooling());
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    private IEnumerator Pooling()
    {
        while(gameObject != null)
        {
            for (int i = 0; i < EventPool.Count; i++)
            {
                EventPipeline(EventPool.Dequeue());
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
