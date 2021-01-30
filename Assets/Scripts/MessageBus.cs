using System;
using System.Collections.Generic;
using UnityEngine;

public class MessageBus : Singleton<MessageBus>
{
    private readonly Dictionary<Type, List<object>> _subscriptions = new Dictionary<Type, List<object>>();
    
    public void Subscribe<T>(Action<T> action)
    {
        var type = typeof(T);
        if (!_subscriptions.ContainsKey(type)) 
            _subscriptions.Add(type, new List<object>());
        _subscriptions[type].Add(action);
    }
    
    public void Publish<T>(T message)
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

    private void OnDestroy()
    {
        _subscriptions.Clear();
    }
}