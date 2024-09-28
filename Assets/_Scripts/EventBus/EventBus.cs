using System;
using System.Collections.Generic;
using System.Linq;
using Core.Utility.DebugTool;
using UnityEngine;

using static Core.Utility.DebugTool.DebugColorOptions.HtmlColor;

namespace Core.GameEventSystem
{
    [Serializable]
    public class EventBus
    {
        private Dictionary<string, List<CallbackWithPriority>> _signalCallbacks = new Dictionary<string, List<CallbackWithPriority>>();
        [SerializeField] private DebugLogger _debugger = new();

#if UNITY_EDITOR

        public void GetAllData()
        {
            _debugger.Log(null, "EVENT BUS SIGNALS!!");
            foreach (KeyValuePair<string, List<CallbackWithPriority>> pair in _signalCallbacks)
            {
                foreach (CallbackWithPriority callback in _signalCallbacks[pair.Key])
                {
                    if (callback.Callback is Delegate d)
                        _debugger.Log(null, $" SIGNAL: {(pair.Key).Color(Cyan)} -- CALLBACK: {((d.Method.Name)).Color(Cyan)}");
                    else
                        _debugger.Log(null, $" SIGNAL: {(pair.Key).Color(Cyan)} -- CALLBACK: {((callback.Callback)).Color(Cyan)}");
                }
            }
        }

#endif

        public void Subscribe<T>(Action<T> callback, int priority = 0)
        {
            string key = typeof(T).Name;

            if (_signalCallbacks.ContainsKey(key))
            {
                _signalCallbacks[key].Add(new CallbackWithPriority(priority, callback));
            }
            else
            {
                _signalCallbacks.Add(key, new List<CallbackWithPriority>() { new(priority, callback) });
            }

            _debugger.Log(null, $"Action {(callback.Method.Name).ToString().Color(Green)} was subscribed " +
                                                    $"to signal {(typeof(T).Name).Color(Green)}");

            _signalCallbacks[key] = _signalCallbacks[key].OrderByDescending(x => x.Priority).ToList();
        }

        public void Invoke<T>(T signal)
        {
            string key = typeof(T).Name;

            if (_signalCallbacks.ContainsKey(key))
            {
                _debugger.Log(null, $"Signal {(key).Color(Green)} was Invoked");

                foreach (var obj in _signalCallbacks[key])
                {
                    var callback = obj.Callback as Action<T>;
                    callback?.Invoke(signal);
                }
            }
            else
            {
                Debug.LogWarning($"No any listeners to {(key).Color(Green)} signal! (possible missing eventBus instance)");
            }
        }

        public void Unsubscribe<T>(Action<T> callback)
        {
            string key = typeof(T).Name;

            if (_signalCallbacks.ContainsKey(key))
            {
                var callbackToDelete = _signalCallbacks[key].FirstOrDefault(x => x.Callback.Equals(callback));
                if (callbackToDelete != null)
                {
                    _signalCallbacks[key].Remove(callbackToDelete);
                    _debugger.Log(null, $"Action {(callback.Method.Name).Color(Red)} was unsubscribed " +
                                                    $"to signal {(typeof(T).Name).Color(Red)}");
                }
            }
            else
            {
                Debug.LogError($"Trying to unsubscribe for not existing key {(key).Color(Red)}!");
            }
        }

        public void UnsubscribeAll<T>()
        {
            string key = typeof(T).Name;

            if (_signalCallbacks.ContainsKey(key))
            {
                _signalCallbacks.Remove(key);

                _debugger.Log(null, $"Signal {(key).Color(Red)} was absolutely unsubscribed!".Color(Red));
            }
            else
            {
                Debug.LogError($"Trying to unsubscribe for not existing key {(key).Color(Red)}!");
            }
        }
    }
}