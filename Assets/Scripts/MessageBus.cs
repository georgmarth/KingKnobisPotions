using System;
using System.Collections.Generic;
using UnityEngine;

public static class MessageBus
{
    private static readonly Dictionary<Type, List<object>> _subscriptions = new Dictionary<Type, List<object>>();
    
    public static void Subscribe<T>(Action<T> action)
    {
        var type = typeof(T);
        if (!_subscriptions.ContainsKey(type)) 
            _subscriptions.Add(type, new List<object>());
        _subscriptions[type].Add(action);
    }
    
    public static void Publish<T>(T message)
    {
        Debug.Log(message.ToString());
        if (_subscriptions.ContainsKey(typeof(T)))
        {
            foreach (var subscription in _subscriptions[typeof(T)])
            {
                (subscription as Action<T>)?.Invoke(message);
            }
        }
    }

    public static void ClearSubscriptions()
    {
        _subscriptions.Clear();
    }
}