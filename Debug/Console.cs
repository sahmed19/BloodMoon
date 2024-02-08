using System;
using System.Collections.Generic;

namespace BloodMoon.Debug
{
    public static class Console
    {
        public enum ConsoleStyle
        {
            Default,
            Warning,
            Error
        }

        public struct SingleLog
        {
            public string Message;
            public ConsoleStyle Style;

            public SingleLog(string m, ConsoleStyle s = ConsoleStyle.Default)
            {
                Message = m;
                Style = s;
            }
        }

        static bool ALSO_FIRE_UNITY_DEBUG_LOGS;
        
        static int STACK_LIMIT = 99;
        
        static Queue<SingleLog> mDEBUG_QUEUE;
        public static Queue<SingleLog> DEBUG_QUEUE
        {
            get
            {
                if (mDEBUG_QUEUE == null)
                    mDEBUG_QUEUE = new Queue<SingleLog>();
                return mDEBUG_QUEUE;
            }
        }

        public static event Action<SingleLog> OnLog = delegate { };

        public static void Clear() => DEBUG_QUEUE.Clear();
        
        
        public static void Log(string message, ConsoleStyle style = ConsoleStyle.Default)
        {
            SingleLog log = new(message, style);
            OnLog(log);
            
            DEBUG_QUEUE.Enqueue(log);
            if (DEBUG_QUEUE.Count >= STACK_LIMIT)
                DEBUG_QUEUE.Dequeue();

            if (ALSO_FIRE_UNITY_DEBUG_LOGS)
            {
                UnityEngine.Debug.Log(log.Message);
            }
        }

    }
}
