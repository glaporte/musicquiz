using System;
using System.Threading.Tasks;

namespace Utils
{
    public static class Common
    {
        public static void InvokeAfterDelay(int delay, Action callback)
        {
            UnityEngine.Debug.Log(delay);
            Task.Delay(delay).ContinueWith(t => UnityMainThreadDispatcher.Instance().Enqueue(callback));
        }
    }
}
