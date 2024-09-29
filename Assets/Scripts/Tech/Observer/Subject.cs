using System;
using System.Collections.Generic;

namespace Tech.Observer
{
    public static class Subject
    {
        private static readonly Dictionary<EventID, Action<object[]>> observer = new ();
        
        public static void RegisterListener(EventID id,Action<object[]> listener)
        {
            observer.TryAdd(id, null);
            observer[id] += listener;
        }

        public static void RemoveListener(EventID id, Action<object[]> listener)
        {
            if (observer.ContainsKey(id))
            {
                observer[id] -= listener;
                return;
            }
            
            Logger.LogCommon.LogError("Listener Not Found");
        }
        
        public static void RemoveAllListener()
        {
            observer.Clear();
        }

        public static void Notify(EventID id, params object[] param)
        {
            if (!observer.ContainsKey(id))
            {
                Logger.LogCommon.LogError("Id Not Exist");
                return;
            }
            
            observer[id]?.Invoke(param);
        }
    }
}