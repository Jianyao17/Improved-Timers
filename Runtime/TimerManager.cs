using System.Collections.Generic;
using ImprovedTimers.Utils;

namespace ImprovedTimers
{
    public static class TimerManager
    {
        private static readonly List<Timer> timers = new();
        private static readonly List<Timer> sweep = new();
        
        public static void RegisterTimer(Timer timer) => timers.Add(timer);
        public static void DeregisterTimer(Timer timer) => timers.Remove(timer);

        public static void UpdateTimers()
        {
            if (timers.Count == 0) return;
            
            sweep.RefreshWith(timers);
            foreach (var timer in sweep) 
                timer.Tick();
        }

        public static void ClearTimers()
        {
            sweep.RefreshWith(timers);
            foreach (var timer in sweep) 
                timer.Dispose();
            
            timers.Clear();
            sweep.Clear();
        }
    }
}