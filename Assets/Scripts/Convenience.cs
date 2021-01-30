using System.Collections.Generic;
using UnityEngine;

public static class Convenience
{
    public static T SelectRandom<T>(this IList<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}