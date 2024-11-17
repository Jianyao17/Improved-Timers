using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.LowLevel;

namespace ImprovedTimers.Utils
{
    public static class PlayerLoopUtils
    {
        /// <summary>
        /// Removes a specific PlayerLoopSystem from the given PlayerLoopSystem hierarchy.
        /// </summary>
        /// <typeparam name="T">The type of PlayerLoopSystem to identify the subsystem loop to inspect.</typeparam>
        /// <param name="loop">The PlayerLoopSystem to inspect and potentially modify.</param>
        /// <param name="systemToRemove">The PlayerLoopSystem to be removed from the hierarchy.</param>
        public static void RemoveSystem<T>(ref PlayerLoopSystem loop, in PlayerLoopSystem systemToRemove)
        {
            if (loop.subSystemList == null) return;
            
            var playerLoopSystemList = new List<PlayerLoopSystem>(loop.subSystemList);
            for (int i = 0; i < playerLoopSystemList.Count; i++)
            {
                if (playerLoopSystemList[i].type == systemToRemove.type &&
                    playerLoopSystemList[i].updateDelegate == systemToRemove.updateDelegate)
                {
                    playerLoopSystemList.RemoveAt(i);
                    loop.subSystemList = playerLoopSystemList.ToArray();
                }
            }
            HandleSubSystemLoopForRemoval<T>(ref loop, systemToRemove);
        }

        /// <summary>
        /// Traverses the subSystemList recursively to remove a specified PlayerLoopSystem.
        /// </summary>
        /// <typeparam name="T">The type of PlayerLoopSystem to identify the subsystem loop to inspect.</typeparam>
        /// <param name="loop">The PlayerLoopSystem to inspect and potentially modify.</param>
        /// <param name="systemToRemove">The PlayerLoopSystem to be removed from the hierarchy.</param>
        private static void HandleSubSystemLoopForRemoval<T>(ref PlayerLoopSystem loop, in PlayerLoopSystem systemToRemove)
        {
            if (loop.subSystemList == null) return;

            for (int i = 0; i < loop.subSystemList.Length; i++) 
                RemoveSystem<T>(ref loop.subSystemList[i], systemToRemove);
        }

        /// <summary>
        /// Inserts a PlayerLoopSystem into the hierarchy at
        /// a specified index if the current system matches the given type.
        /// </summary>
        /// <typeparam name="T">            The type of PlayerLoopSystem to find for insertion.</typeparam>
        /// <param name="loop">             The PlayerLoopSystem to inspect and potentially modify.</param>
        /// <param name="systemToInsert">   The PlayerLoopSystem to be inserted.</param>
        /// <param name="index">            The position to insert the new system.</param>
        /// <returns>                       True if the insertion was successful, otherwise false.</returns>
        public static bool InsertSystem<T>(ref PlayerLoopSystem loop, in PlayerLoopSystem systemToInsert, int index)
        {
            if (loop.type != typeof(T)) return HandleSubSystemLoop<T>(ref loop, systemToInsert, index);

            var playerLoopSystemList = new List<PlayerLoopSystem>();
            if (loop.subSystemList != null) playerLoopSystemList.AddRange(loop.subSystemList);
            
            playerLoopSystemList.Insert(index, systemToInsert);
            loop.subSystemList = playerLoopSystemList.ToArray();
            return true;
        }

        /// <summary>
        /// Traverses the subSystemList to find the target PlayerLoopSystem
        /// and attempts to insert the new system.
        /// </summary>
        /// <typeparam name="T">            The type of PlayerLoopSystem to find for insertion.</typeparam>
        /// <param name="loop">             The PlayerLoopSystem to inspect and potentially modify.</param>
        /// <param name="systemToInsert">   The PlayerLoopSystem to be inserted.</param>
        /// <param name="index">            The position to insert the new system.</param>
        /// <returns>                       True if the insertion was successful, otherwise false.</returns>
        private static bool HandleSubSystemLoop<T>(ref PlayerLoopSystem loop, in PlayerLoopSystem systemToInsert, int index)
        {
            if (loop.subSystemList == null) return false;
            
            for (var i = 0; i < loop.subSystemList.Length; i++)
            {
                if (!InsertSystem<T>(ref loop.subSystemList[i], in systemToInsert, index)) continue;
                return true;
            }
            return false;
        }


        /// <summary>
        /// Print the hierarchical structure of the Unity Player Loop system to the console.
        /// </summary>
        /// <param name="playerLoop">The PlayerLoopSystem to analyze and print.</param>
        public static void PrintPlayerLoop(PlayerLoopSystem playerLoop)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Unity Player Loop");

            // Iterate all subsystem in this player loop system
            foreach (var subSystem in playerLoop.subSystemList) 
                PrintSubSystems(subSystem, sb, 0);
            
            Debug.Log(sb.ToString());
        }

        /// <summary>
        /// Recursively appends the structure of a PlayerLoopSystem and its subsystems to a StringBuilder.
        /// </summary>
        /// <param name="system">The current PlayerLoopSystem to process.</param>
        /// <param name="sb">The StringBuilder to append the system hierarchy.</param>
        /// <param name="level">The current hierarchy level for indentation.</param>
        private static void PrintSubSystems(PlayerLoopSystem system, StringBuilder sb, int level)
        {
            sb.Append(' ', level * 4).AppendLine(system.type.ToString());
            if (system.subSystemList == null || system.subSystemList.Length == 0) return;

            foreach (var subSystem in system.subSystemList) 
                PrintSubSystems(subSystem, sb, level + 1);
        }
    }
}