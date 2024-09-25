using System;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Utility.DebugTool
{
    [Serializable]
    public class DebugLogger
    {
        [SerializeField] private bool _enableDebug = true;

        [SerializeField] private int _defaultFontSize = 12;

        private void DoLog(Action<string, Object> logFunction, string prefix, Object myObj, int size, params object[] message)
        {
#if UNITY_EDITOR
            if (!_enableDebug) return;

            var name = (myObj ? myObj.name : "NullObject").Color("lightblue");
            logFunction($"{prefix}[{name}]: {String.Join("; ", message).Size(size)}\n ", myObj);
#endif
        }

        #region FULL LOG

        public void Log(Object myObj, int size, params object[] message)
        {
            DoLog(Debug.Log, "", myObj, size, message);
        }

        public void LogError(Object myObj, int size, params object[] message)
        {
            DoLog(Debug.LogError, "<!>".Color("red"), myObj, size, message);
        }

        public void LogWarning(Object myObj, int size, params object[] message)
        {
            DoLog(Debug.LogWarning, "⚠️".Color("yellow"), myObj, size, message);
        }

        public void LogSuccess(Object myObj, int size, params object[] message)
        {
            DoLog(Debug.Log, "☻".Color("green"), myObj, size, message);
        }

        #endregion FULL LOG

        #region SHORT LOG

        public void Log(Object myObj, params object[] message)
        {
            DoLog(Debug.Log, "", myObj, _defaultFontSize, message);
        }

        public void LogError(Object myObj, params object[] message)
        {
            DoLog(Debug.LogError, "<!>".Color("red"), myObj, _defaultFontSize, message);
        }

        public void LogWarning(Object myObj, params object[] message)
        {
            DoLog(Debug.LogWarning, "⚠️".Color("yellow"), myObj, _defaultFontSize, message);
        }

        public void LogSuccess(Object myObj, params object[] message)
        {
            DoLog(Debug.Log, "☻".Color("green"), myObj, _defaultFontSize, message);
        }

        #endregion SHORT LOG
    }
}