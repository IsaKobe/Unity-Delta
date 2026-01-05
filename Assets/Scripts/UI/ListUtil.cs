using System;
using System.Collections.Generic;
using System.Text;

public static class ListUtil
{
    public static void EnforceListLength<T>(this List<T> list, int length)
    {
        while (list.Count < length)
        {
            list.Add(Activator.CreateInstance<T>());
        }

        while (list.Count > length)
        {
            list.RemoveAt(list.Count - 1);
        }
    }
}