using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Functions 
{
    public static void RelocateObjectsInArray<T>(T[] array, T newElement)
    {
        array[0] = array[1];
        array[1] = array[2];
        array[2] = newElement;
    }
}
