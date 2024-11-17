using System.Collections.Generic;

namespace ImprovedTimers.Utils
{
    public static class ListExtensions
    {
        /// <summary>
        /// Refresh target list with new value IEnumerable 
        /// </summary>
        /// <param name="list">target list</param>
        /// <param name="items">IEnumerable value</param>
        public static void RefreshWith<T>(this List<T> list, IEnumerable<T> items)
        {
            list.Clear();
            list.AddRange(items);
        }
    }
}