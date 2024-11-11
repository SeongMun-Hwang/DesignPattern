using System;
using System.Collections.Generic;
using UnityEngine;

public enum GlobalEvent
{
    AllDeath,
    AllRun,
    AllIdle,
    AllSpin,
    AllAttack,
}
public class EventBus : MonoBehaviour
{
    static bool isEventBusClosing = false;
    static EventBus instance;
    public static EventBus Instance
    {
        get
        {
            if(instance == null && !isEventBusClosing)
            {
                GameObject go = new GameObject("EventBus");
                instance=go.AddComponent<EventBus>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private Dictionary<GlobalEvent,Action> eventDictionary= new Dictionary<GlobalEvent,Action>();
    public void Subscribe(GlobalEvent eventType, Action listener)
    {
        if (eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] += listener;
        }
        else {
            eventDictionary[eventType] = listener;
        }
    }

    public void Unsubscribe(GlobalEvent eventType, Action listener)
    {
        if (eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] -= listener;
            if (eventDictionary == null)
            {
                eventDictionary.Remove(eventType);
            }
        }
    }
    public void Publish(GlobalEvent eventType)
    {
        if(eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType]?.Invoke();
        }
    }
    private void OnApplicationQuit()
    {
        isEventBusClosing = true;   
    }
}
