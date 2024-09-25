using System;
using System.Collections.Generic;
using System.Linq;
using Core.Utility.DebugTool;
using UnityEngine;

#if UNITY_EDITOR

#endif

namespace Core.EventSystem
{
    [Serializable]
    public class EventBus
    {
        private Dictionary<string, List<CallbackWithPriority>> _signalCallbacks = new Dictionary<string, List<CallbackWithPriority>>();
        [SerializeField] private DebugLogger _debugger = new();

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

            _debugger.Log(new(), ($"Action {(callback).ToString().Color(DebugColorOptions.HtmlColor.Green)} was subscribed " +
                                                    $"to signal {(typeof(T).Name).Color(DebugColorOptions.HtmlColor.Green)}"));

            _signalCallbacks[key] = _signalCallbacks[key].OrderByDescending(x => x.Priority).ToList();
        }

        public void Invoke<T>(T signal)
        {
            string key = typeof(T).Name;

            if (_signalCallbacks.ContainsKey(key))
            {
                _debugger.Log(new(), $"Signal {(signal).Color(DebugColorOptions.HtmlColor.Green)} was Invoked");

                foreach (var obj in _signalCallbacks[key])
                {
                    var callback = obj.Callback as Action<T>;
                    callback?.Invoke(signal);
                }
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
                    _debugger.Log(new(), $"Action {(callback).Color(DebugColorOptions.HtmlColor.Red)} was unsubscribed " +
                                                    $"to signal {(typeof(T).Name).Color(DebugColorOptions.HtmlColor.Red)}");
                }
            }
            else
            {
                Debug.LogErrorFormat("Trying to unsubscribe for not existing key! {0} ", key);
            }
        }

        public void UnsubscribeAll<T>()
        {
            string key = typeof(T).Name;

            if (_signalCallbacks.ContainsKey(key))
            {
                _signalCallbacks.Remove(key);

                _debugger.Log(new(), $"Signal {key} was absolutely unsubscribed!".Color(DebugColorOptions.HtmlColor.Red));
            }
            else
            {
                Debug.LogErrorFormat("Trying to unsubscribe for not existing key! {0} ", key);
            }
        }
    }
}