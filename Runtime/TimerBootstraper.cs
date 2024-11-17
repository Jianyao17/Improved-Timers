using ImprovedTimers.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace ImprovedTimers
{
    internal static class TimerBootstrapper
    {
        private static PlayerLoopSystem timerSystem;
        
        /// <summary>
        /// Initializes the TimerManager by injecting it into the PlayerLoop system.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        internal static void Initialize()
        {
            PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
            
            if (!InsertTimerManager<Update>(ref currentPlayerLoop, 0))
            {
                Debug.LogWarning("ImprovedTimers not initialized, unable to register the TimerManager to the PlayerLoop.");
                return;
            }
            PlayerLoop.SetPlayerLoop(currentPlayerLoop);

#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeState;
            EditorApplication.playModeStateChanged += OnPlayModeState;
            
            static void OnPlayModeState(PlayModeStateChange state)
            {
                if (state == PlayModeStateChange.ExitingEditMode)
                {
                    var currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
                    RemoveTimerManager<Update>(ref currentPlayerLoop);
                    
                    PlayerLoop.SetPlayerLoop(currentPlayerLoop);
                    TimerManager.ClearTimers();
                }
            }
#endif
        }

        private static void RemoveTimerManager<T>(ref PlayerLoopSystem loop) 
            => PlayerLoopUtils.RemoveSystem<T>(ref loop, in timerSystem);

        private static bool InsertTimerManager<T>(ref PlayerLoopSystem loop, int index)
        {
            timerSystem = new PlayerLoopSystem()
            {
                type = typeof(TimerManager),
                updateDelegate = TimerManager.UpdateTimers,
                subSystemList = null,
            };
            return PlayerLoopUtils.InsertSystem<T>(ref loop, timerSystem, index);
        }
    }
}