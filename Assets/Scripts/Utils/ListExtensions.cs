using System.Collections.Generic;

namespace Utils
{
    public static class ListExtensions
    {
        public static List<T> AddMultiple<T>(this List<T> list, T item, int count)
        {
            for (int i = 0; i < count; i++)
            {
                list.Add(item);
            }

            return list;
        }
    }
}