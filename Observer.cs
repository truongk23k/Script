using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Observer : Singleton<Observer>
{
    Dictionary<string, List<Action<object>>> listAction = new Dictionary<string, List<Action<object>>>();

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public Observer AddListener(string key, Action<object> callback)
    {
        if (!listAction.ContainsKey(key))
            listAction.Add(key, new List<Action<object>>());

        listAction[key].Add(callback);
        return this;
    }

    public Observer RemoveListener(string key, Action<object> callback)
    {
        if (!listAction.ContainsKey(key))
            return this;

        if (!listAction[key].Contains(callback))
            return this;

        listAction[key].Remove(callback);
        return this;
    }

    public Observer Notify(string key, object data)
    {
        if (!listAction.ContainsKey(key))
            return this;

        foreach (var a in listAction[key])
        {
            a?.Invoke(data);
        }

        return this;
    }
}
